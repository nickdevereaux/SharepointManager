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
using SPM2.SharePoint;

namespace Keutmann.SharePointManager.Components
{
    public class TreeViewComponent : TreeView
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

        public void Build()
        {

            Cursor.Current = Cursors.WaitCursor;
            this.ImageList = Program.Window.SPMimageList;
            Nodes.Clear();
            BeginUpdate();
            TreeViewNodeSorter = new NodeSorter();

            SPProvider = new SPNodeProvider();
            SPProvider.View = ViewName;

            var treeViewProvider = new TreeViewNodeProvider(SPProvider);
            FarmNode = treeViewProvider.LoadFarmNode();
            this.Nodes.Add(FarmNode);

            Sort();
            
            //DefaultExpand(root);

            this.SelectedNode = FarmNode;

            EndUpdate();
            Cursor.Current = Cursors.Default;

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
