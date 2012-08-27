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
            node.Setup();
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
            var index = parentNode.Model.Children.Count;

            var moreNode = IsThereMoreNodes(parentNode.Model.Children) ? 1 : 0;

            parentNode.Model.LoadChildren();
            
            var moreNodeFound = false;

            var list = new List<SPTreeNode>();
            for (var count = index - moreNode; count < parentNode.Model.Children.Count; count++)
            {
                var spNode = parentNode.Model.Children[count];
                if (spNode is MoreNode)
                {
                    moreNodeFound = true;
                    if (moreNode > 0)
                    {
                        continue;
                    }
                }

                var treeNode = SPTreeNode.Create(this, spNode);
                parentNode.Nodes.Insert(count, treeNode);
                list.Add(treeNode);
            }

            if (moreNode > 0 && !moreNodeFound)
            {
                parentNode.Nodes.RemoveAt(parentNode.Nodes.Count - 1);
            }

            return list;
        }

        private bool IsThereMoreNodes(List<ISPNode> nodes)
        {
            return nodes.Count > 0 && nodes[nodes.Count - 1] is MoreNode;
        }
    }
}
