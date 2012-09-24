using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Keutmann.SharePointManager.Components;
using Keutmann.SharePointManager.Library;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.WebControls;
using SPM2.Framework.Collections;
using SPM2.SharePoint;
using SPM2.SharePoint.Model;

namespace Keutmann.SharePointManager.ViewModel.TreeView
{
    public class SPTreeNode : ExplorerNodeBase, IDisposable, IBindableComponent
    {
        public ISPNode Model;

        public ITreeViewNodeProvider NodeProvider { get; set; }

        public override ContextMenuStrip ContextMenuStrip
        {
            get
            {

                if (base.ContextMenuStrip != null) return base.ContextMenuStrip;
                base.ContextMenuStrip = new SPContextMenu(this);
                return base.ContextMenuStrip;
            }
            set
            {
                base.ContextMenuStrip = value;
            }
        }


        public SPTreeNode(ISPNode modelNode)
        {
            Model = modelNode;
            this.Tag = Model.SPObject;
            this.DefaultExpand = false;

            int index = Program.Window.Explorer.AddImage(Model.IconUri);
            this.ImageIndex = index;
            this.SelectedImageIndex = index;

            this.DataBindings.Add("Text", Model, "Text");
            this.DataBindings.Add("ToolTipText", Model, "ToolTipText");
            this.DataBindings.Add("BrowserUrl", Model, "Url");
            this.Name = Model.SPObjectType.FullName;

            //if (Model.NodeTypes.Count > 0)
            //{
                this.Nodes.Add(new ExplorerNodeBase("Dummy"));
            //}
        }


        public static SPTreeNode Create(ITreeViewNodeProvider provider, ISPNode spNode)
        {
            var node = new SPTreeNode(spNode);
            node.NodeProvider = provider;

            return node;
        }


        public override void Setup()
        {
            Model.Setup(Model.Parent);
        }

           

        public override void LoadNodes()
        {
            base.LoadNodes();
            
            this.TreeView.BeginUpdate();

            if (NodeProvider != null)
            {
                var nodes = NodeProvider.LoadChildren(this);
                
                //foreach (var childnode in )
                //{
                //    Nodes.Add(childnode);
                //}
            }

            this.TreeView.EndUpdate();

        }

        public override void Refresh()
        {
            Trace.WriteLine("Refresh() called on node: " + this.Text);

            // Save the structure of open nodes.
            var list = SaveStucture();
            if (list.Count == 0) return;

            var treeView = (TreeViewComponent)this.TreeView;
            
            treeView.Build(list);
        }


        public StuctureItemCollection SaveStucture()
        {
            var list = new StuctureItemCollection();

            list.Add(CloneNode(this));
            var child = this.Parent as SPTreeNode;
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
            result.ID = (String.IsNullOrEmpty(source.Model.ID)) ? source.Index.ToString() : source.Model.ID;
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
                var nodeID = (String.IsNullOrEmpty(node.Model.ID)) ? node.Index.ToString() : node.Model.ID;
                if (nodeID == item.ID)
                {
                    Trace.WriteLine("Reload of node: " + item.ID);

                    Reload(node, list);
                    break;
                }
            }
        }

        public virtual TabPage[] GetTabPages()
        {
            ArrayList alPages = new ArrayList();

            alPages.Add(TabPages.GetPropertyPage(TabPages.PROPERTIES, Model.SPObject));

            if (this.BrowserUrl.Length > 0)
            {
                alPages.Add(TabPages.GetBrowserPage("Browser", this.BrowserUrl));
            }


            if (Model.SPObject != null)
            {

                PropertyInfo propInfo = Model.SPObjectType.GetProperty("SchemaXml", typeof(string));
                if (propInfo != null)
                {
                    alPages.Add(TabPages.GetXmlPage("Schema Xml", propInfo.GetValue(Model.SPObject, null) as string));
                }
            }

            return (TabPage[])alPages.ToArray(typeof(TabPage));

        }

        public void Dispose()
        {
            Model.Dispose();
        }


        #region IBindableComponent Members

        private ISite _site;
        private BindingContext bindingContext;
        private ControlBindingsCollection dataBindings;

        public event EventHandler Disposed;

        public System.ComponentModel.ISite Site
        {
            get
            {
                return _site;
            }
            set
            {
                _site = value;
            }
        }

        public BindingContext BindingContext
        {
            get
            {
                if (bindingContext == null)
                {
                    bindingContext = new BindingContext();
                }
                return bindingContext;
            }
            set
            {
                bindingContext = value;
            }
        }

        public ControlBindingsCollection DataBindings
        {
            get
            {
                if (dataBindings == null)
                {
                    dataBindings = new ControlBindingsCollection(this);
                }
                return dataBindings;
            }
        }

        #endregion



    }
}
