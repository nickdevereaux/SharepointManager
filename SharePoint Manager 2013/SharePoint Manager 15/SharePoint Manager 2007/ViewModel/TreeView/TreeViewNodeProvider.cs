using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SPM2.SharePoint;
using SPM2.SharePoint.Model;

namespace Keutmann.SharePointManager.ViewModel.TreeView
{
    //[Export()]
    //[PartCreationPolicy(CreationPolicy.Shared)]
    public class TreeViewNodeProvider : ITreeViewNodeProvider
    {
        public ISPNodeProvider SPProvider { get; set; }

        public TreeViewNodeProvider(ISPNodeProvider provider)
        {
            SPProvider = provider;
        }

        public SPTreeNode LoadFarmNode()
        {
            var spFarmNode = SPProvider.LoadFarmNode();
            var node = SPTreeNode.Create(this, spFarmNode);
            //node.IsExpanded = true;
            return node;
        }

        public SPTreeNode Load(ISPNode spNode)
        {
            var root = SPTreeNode.Create(this, spNode);
            LoadTreeNodes(root);

            return root;
        }

        private void LoadTreeNodes(SPTreeNode parent)
        {
            if (parent.Model.Children.Count == 0) return;

            parent.Nodes.AddRange(parent.Model.Children.Select(spNode => SPTreeNode.Create(this, spNode)).ToArray());

            foreach (SPTreeNode item in parent.Nodes)
            {
                LoadTreeNodes(item);
            }
        }

        public IEnumerable<SPTreeNode> LoadChildren(SPTreeNode parentNode)
        {
            parentNode.Model.LoadChildren();
            return parentNode.Model.Children.Select(spNode => SPTreeNode.Create(this, spNode));
        }
    }
}
