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
using SPM2.SharePoint;
using SPM2.SharePoint.Model;

namespace Keutmann.SharePointManager.ViewModel.TreeView
{
    public class SPTreeNode : ExplorerNodeBase, IDisposable
    {
        public ISPNode Model;

        public ITreeViewNodeProvider NodeProvider { get; set; }


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
            //this.Dispose();
            //Model.LoadChildren();            
            if (this.Parent != null)
            {
                if (Model.SPObject is SPPersistedObject)
                {
                    RefreshSPPersistedObject();
                }
                else if (Model.SPObject is SPBaseCollection)
                {
                    RefreshSPPersistedObject();
                }
                else
                {
                    Type tagType = this.Model.SPObjectType;
                    Type baseType = tagType.BaseType;
                    string baseName = baseType.Name;
                    if (baseName.IndexOf("SPPersistedObjectCollection") >= 0 ||
                        baseName.IndexOf("SPPersistedChildCollection") >= 0)
                    {
                        RefreshSPPersistedObjectCollection();
                    }
                    else
                    {
                        RefreshWss2Objects();
                    }
                }
            }
            else
            {
                // Reload the TreeView, because there is no parent to the node.
                // It is properly the root that haven been selected.
                ClearNodes(Program.Window.Explorer.Nodes);
                Program.Window.Explorer.Build();
            }
        }

        class StuctureItem
        {
            //public Type ObjectType;
            public string ID;
        }

        class StuctureItemCollection : List<StuctureItem>
        {
        }

        private StuctureItemCollection SaveStucture(SPTreeNode currentNode)
        {
            var list = new StuctureItemCollection();

            list.Add(CloneNode(currentNode));
            var child = currentNode.Parent as SPTreeNode;
            while (child != null)
            {
                list.Insert(0, CloneNode(child));
                if (child.Tag is SPSiteCollection)
                {
                    break;
                }
                child = child.Parent as SPTreeNode;
            }

            return list;
        }

        private StuctureItem CloneNode(SPTreeNode source)
        {
            var result = new StuctureItem();
            result.ID = String.Concat(source.Model.SPObjectType.FullName, source.Text);

            return result;
        }

        private void Reload(SPTreeNode parent, StuctureItemCollection list)
        {
            if (list.Count > 0)
            {
                parent.HasChildrenLoaded = false;
                Program.Window.Explorer.ExpandNode(parent);

                var item = list[0];
                list.RemoveAt(0);
                foreach (SPTreeNode node in parent.Nodes)
                {
                    if (GetNodeID(node) == item.ID)
                    {
                        Reload(node, list);
                        break;
                    }
                }
            }
            Program.Window.Explorer.SelectedNode = parent;
        }

        private string GetNodeID(SPTreeNode node)
        {
            var nodeID = String.Concat(node.Model.SPObjectType.FullName, node.Text);
            return nodeID;
        }

        protected void RefreshSPPersistedObject()
        {
            var currentID = GetNodeID(this);
            var parent = this.Parent as SPTreeNode;
            Program.Window.Explorer.SelectedNode = null;
            ClearNodes(parent.Nodes);
            parent.HasChildrenLoaded = false;
            Program.Window.Explorer.ExpandNode(parent);

            foreach (SPTreeNode node in parent.Nodes)
            {

                if (GetNodeID(node) == currentID)
                {
                    Program.Window.Explorer.SelectedNode = node;
                    //Program.Window.propertyGrid.SelectedObject = node.Tag;

                    if (this.IsExpanded)
                    {
                        node.HasChildrenLoaded = false;
                        //Program.Window.Explorer.ExpandNode(node);
                        this.Expand();
                        //                        this.Toggle();
                    }
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

        protected void RefreshSPPersistedObjectCollection()
        {
            ClearNodes(Nodes);

            this.HasChildrenLoaded = false;
            Program.Window.Explorer.ExpandNode(this);
        }

        protected void RefreshWss2Objects()
        {
            var list = SaveStucture(this);

            if (list.Count > 0)
            {
                var appNode = GetWebApp(this);
                if (appNode != null && appNode.Model.SPObject is SPWebApplication)
                {
                    appNode.Dispose();

                    //Program.Window.Explorer.DisposeObjectModelNodes(appNode);
                    //((SPPersistedObject)appNode.Tag).Uncache();

                    Reload(appNode, list);

                    if (this.IsExpanded)
                    {
                        this.HasChildrenLoaded = false;
                        //Program.Window.Explorer.ExpandNode(CurrentNode);
                        this.Expand();
                    }
                }
                else
                {
                    
                    //Reload(appNode, list);
                }
            }
            else
            {
                throw new ApplicationException(SPMLocalization.GetString("ExplorerBase_Error"));
            }
        }

        protected void RefreshSPBaseCollection()
        {
        }

        private SPTreeNode GetWebApp(SPTreeNode start)
        {
            var child = start;
            while (child != null)
            {
                if (child.Model.SPObject is SPWebApplication)
                {
                    return child;
                }
                child = child.Parent as SPTreeNode;
            }
            return null;
        }



        public void Dispose()
        {
            Model.Dispose();
        }
    }
}
