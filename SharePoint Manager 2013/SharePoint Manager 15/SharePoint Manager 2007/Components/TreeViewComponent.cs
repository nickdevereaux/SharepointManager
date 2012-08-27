using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Windows.Forms;
using Keutmann.SharePointManager.ViewModel.TreeView;
using Microsoft.SharePoint.Administration;
using SPM2.Framework.Forms;
using SPM2.SharePoint;

namespace Keutmann.SharePointManager.Components
{
    public class TreeViewComponent : TreeViewExtended
    {
        public int oldNodeIndex = -1;
        public SPFarm CurrentFarm = SPFarm.Local;

        public string ViewName { get; set; }
        public ISPNodeProvider SPProvider { get; set; }

        public SPTreeNode FarmNode { get; set; }

        public TreeViewComponent()
        {
            this.ShowNodeToolTips = true;
            this.HideSelection = false;
            ViewName = "Full";
        }

        public int AddImage(string path)
        {
            int index = -1;
            if (path.Length > 0)
            {
                if (!this.ImageList.Images.ContainsKey(path))
                {
                    Image icon = null;
                    if (File.Exists(path))
                    {
                        icon = Image.FromFile(path);

                    }
                    else
                    {
                        icon = GetImageFromResource(path);
                    }

                    if (icon != null)
                    {
                        this.ImageList.Images.Add(path, icon);

                        index = this.ImageList.Images.Count - 1;
                    }

                }
                else
                {
                    index = this.ImageList.Images.IndexOfKey(path);
                }
            }
            return index;
        }

        private void DefaultExpand(ExplorerNodeBase parent)
        {

            if (parent.DefaultExpand)
            {
                parent.Expand();
                if (!Properties.Settings.Default.ShallowExpand)
                {
                    foreach (ExplorerNodeBase child in parent.Nodes)
                    {
                        DefaultExpand(child);
                    }
                }
            }
        }

        private List<string> DefaultExpandTypes = new List<string> { "SPServiceCollectionNode", "SPWebServiceNode", "SPWebApplicationCollectionNode", "SPWebApplicationNode", "SPSiteCollectionNode", "SPSiteNode", "SPWebNode" };

        public void Build()
        {
            Build(null);
        }


        public void Build(StuctureItemCollection list)
        {
            Cursor.Current = Cursors.WaitCursor;
            this.ImageList = Program.Window.SPMimageList;
            Nodes.Clear();
            BeginUpdate();

            SPProvider = new SPNodeProvider(SPFarm.Local);
            SPProvider.View = ViewName;

            var treeViewProvider = new TreeViewNodeProvider(SPProvider);
            FarmNode = treeViewProvider.LoadFarmNode();

            
            this.Nodes.Add(FarmNode);

            if (list != null)
            {
                FarmNode.Reload(FarmNode, list);
            }
            else
            {
                ExpandToDefault(FarmNode, DefaultExpandTypes);
            }
            
            EndUpdate();
            Cursor.Current = Cursors.Default;
        }

        private void ExpandToDefault(SPTreeNode parent, List<string> types)
        {
            if(types == null || types.Count <= 0)
            {
                this.SelectedNode = parent;
                return;
            }

            ExpandNode(parent);
            parent.Expand();
            var item = types[0];

            types.RemoveAt(0);

            foreach (SPTreeNode node in parent.Nodes)
            {
                if (item.Equals(node.Model.GetType().Name))
                {
                    ExpandToDefault(node, types);
                    break;
                }
            }
        }



        public void ExpandNode(ExplorerNodeBase node)
        {
            if (!node.HasChildrenLoaded)
            {
                Cursor.Current = Cursors.WaitCursor;

                node.HasChildrenLoaded = true;
                node.LoadNodes();

                Cursor.Current = Cursors.Default;
            }
        }

        protected override void OnBeforeExpand(TreeViewCancelEventArgs e)
        {
            ExplorerNodeBase node = e.Node as ExplorerNodeBase;

            ExpandNode(node);

            base.OnBeforeExpand(e);
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (SPTreeNode item in Nodes)
                {
                    item.Dispose();
                }
            }
            base.Dispose(disposing);
        }


        private Image GetImageFromResource(string path)
        {
            if (String.IsNullOrWhiteSpace(path)) return null;

            string[] parts = path.Split(';');
            if (parts.Length != 3) return null;

            AssemblyName name = new AssemblyName(parts[0]);
            
            var assembly = Assembly.Load(name);
            if (assembly == null) return null;

            // parts[1] = "SPM2.SharePoint.Properties.Resources"
            
            var manager = new global::System.Resources.ResourceManager(parts[1], assembly);
            manager.IgnoreCase = true;

            var bitmap = (Bitmap)manager.GetObject(parts[2]);

            return bitmap;
        }
        
    }
}
