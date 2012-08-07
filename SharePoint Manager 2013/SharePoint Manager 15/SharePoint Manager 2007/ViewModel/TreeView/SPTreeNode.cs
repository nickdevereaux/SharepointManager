using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Keutmann.SharePointManager.Components;
using Keutmann.SharePointManager.Library;
using SPM2.SharePoint;
using SPM2.SharePoint.Model;

namespace Keutmann.SharePointManager.ViewModel.TreeView
{
    public class SPTreeNode : ExplorerNodeBase
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
            this.Name = Model.ToolTipText;
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
    }
}
