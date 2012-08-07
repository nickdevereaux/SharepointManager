using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SPM2.SharePoint;

namespace Keutmann.SharePointManager.ViewModel.TreeView
{
    //[Export()]
    //[PartCreationPolicy(CreationPolicy.Shared)]
    public class TreeViewNodeProvider : ITreeViewNodeProvider
    {
        public SPNodeProvider SPProvider { get; set; }

        public TreeViewNodeProvider(SPNodeProvider provider)
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

        public IEnumerable<SPTreeNode> LoadChildren(SPTreeNode parentNode)
        {
            parentNode.Model.LoadChildren();
            return parentNode.Model.Children.Select(spNode => SPTreeNode.Create(this, spNode));
        }
    }
}
