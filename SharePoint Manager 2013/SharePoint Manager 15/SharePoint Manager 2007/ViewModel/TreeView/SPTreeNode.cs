using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Keutmann.SharePointManager.Components;
using Keutmann.SharePointManager.Library;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.WebControls;
using SPM2.SharePoint;
using SPM2.SharePoint.Model;

namespace Keutmann.SharePointManager.ViewModel.TreeView
{
    public class SPTreeNode : ExplorerNodeBase, IDisposable
    {
        public ISPNode Model;

        public ITreeViewNodeProvider NodeProvider { get; set; }


        public override ContextMenuStrip ContextMenuStrip
        {
            get
            {
                if (base.ContextMenuStrip == MenuStripBase && !InReadOnlyMode)
                {
                    var menu = new MenuStripRefresh();

                    if (Model.MenuItemCollection != null)
                    {
                        foreach (Lazy<ActionItem> item in Model.MenuItemCollection)
                        {
                            menu.Items.Add(Create(item.Value));
                        }
                    }

                    //menu.Items.Add(2, new ToolStripSeparator());

                    base.ContextMenuStrip = menu;
                }
                return base.ContextMenuStrip;
            }
            set
            {
                base.ContextMenuStrip = value;
            }
        }

        private ToolStripItem Create(ActionItem item)
        {
            var result = new ToolStripButton();
            result.Text = item.Text;
            result.ToolTipText = item.ToolTipText;
            result.Click += item.Click;
            result.Enabled = item.Enabled;
            return result;
        }


        public SPTreeNode(ISPNode modelNode)
        {
            Model = modelNode;
            this.Tag = Model.SPObject;
            this.DefaultExpand = false;

            var imageUrl = Model.IconUri;
            int index = Program.Window.Explorer.AddImage(imageUrl);
            this.ImageIndex = index;
            this.SelectedImageIndex = index;
            
            //Trace.WriteLine(String.Format("Node.ImageUrl: {0}, Index: {1}",imageUrl, index));

            this.Setup();

            this.Nodes.Add(new ExplorerNodeBase("Dummy"));
        }


        public static SPTreeNode Create(ITreeViewNodeProvider provider, ISPNode spNode)
        {
            var node = new SPTreeNode(spNode);
            node.NodeProvider = provider;

            return node;
        }


        public override void Setup()
        {
            this.Text = Model.Text + string.Empty;
            
            this.Name = Model.SPObjectType.FullName;
            this.ToolTipText = Model.ToolTipText;
        }

        //public override string ImageUrl()
        //{
        //    return Model.IconUri;
        //}
            

        public override void LoadNodes()
        {
            base.LoadNodes();

            this.Nodes.Clear();
            if (NodeProvider != null)
            {
                foreach (var childnode in NodeProvider.LoadChildren(this))
                {
                    Nodes.Add(childnode);
                }
            }
        }

        public override void Refresh()
        {

            Trace.WriteLine("Refresh() called on node: " + this.Text);

            // Save the structure of open nodes.
            var list = SaveStucture(this);
            if (list.Count == 0) return;

            var treeView = (TreeViewComponent)this.TreeView;
            
            // Dispose all objects
            ClearNodes(this.TreeView.Nodes);

            treeView.Build(list);
        }

        private StuctureItemCollection SaveStucture(SPTreeNode currentNode)
        {
            var list = new StuctureItemCollection();

            list.Add(CloneNode(currentNode));
            var child = currentNode.Parent as SPTreeNode;
            while (child != null)
            {
                list.Insert(0, CloneNode(child));
                child = child.Parent as SPTreeNode;
            }

            return list;
        }

        private StuctureItem CloneNode(SPTreeNode source)
        {
            var result = new StuctureItem();
            result.ID = source.Model.ID;
            return result;
        }

        public void Reload(SPTreeNode parent, StuctureItemCollection list)
        {
            if (list == null || list.Count <= 1)
            {
                // End of the line, set the selectedNode and return
                Program.Window.Explorer.SelectedNode = parent;
                return;
            }

            list.RemoveAt(0);

            var item = list[0];

            Trace.WriteLine("Expand node: " + parent.Text);

            parent.HasChildrenLoaded = false;
            Program.Window.Explorer.ExpandNode(parent);

            foreach (SPTreeNode node in parent.Nodes)
            {
                if (node.Model.ID == item.ID)
                {
                    Trace.WriteLine("Reload of node: " + item.ID);

                    Reload(node, list);
                    break;
                }
            }
        }

        private void ClearNodes(TreeNodeCollection collection)
        {
            foreach (SPTreeNode item in collection.OfType<SPTreeNode>())
            {
                item.Dispose();
            }
            collection.Clear();
        }



        public void Dispose()
        {
            Model.Dispose();
        }
    }
}
