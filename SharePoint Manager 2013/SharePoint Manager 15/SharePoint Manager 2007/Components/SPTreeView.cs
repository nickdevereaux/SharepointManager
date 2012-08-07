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
    public class SPTreeView : TreeView
    {
        public NodeDisplayLevelType DisplayLevel = NodeDisplayLevelType.Advanced | NodeDisplayLevelType.Medium | NodeDisplayLevelType.Simple;
        public int oldNodeIndex = -1;
        public SPFarm CurrentFarm = SPFarm.Local;

        public SPTreeView()
        {
            this.ShowNodeToolTips = true;
            this.HideSelection = false;
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

            //ExplorerNodeBase root = null;
            //root = new FarmNode(CurrentFarm);


            var provider = new TreeViewNodeProvider(new SPNodeProvider());
            var root = provider.LoadFarmNode();
            this.Nodes.Add(root);

            Sort();

            //DefaultExpand(root);

            this.SelectedNode = root;

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


        public void DisposeObjectModelNodes(TreeNode parent)
        {
            foreach (TreeNode child in parent.Nodes)
            {
                DisposeObjectModelNodes(child);
                if (child.Tag != null)
                {
                    if (child.Tag is IDisposable)
                    {
                        ((IDisposable)child.Tag).Dispose();
                    }
                    else if (child.Tag is SPPersistedObject)
                    {
                        ((SPPersistedObject)child.Tag).Uncache();
                    }
                }
            }
        }

        public void DisposeObjectModel()
        {
            foreach (TreeNode node in this.Nodes)
            {
                DisposeObjectModelNodes(node as ExplorerNodeBase);
            }
            this.Nodes.Clear();

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DisposeObjectModel();
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
