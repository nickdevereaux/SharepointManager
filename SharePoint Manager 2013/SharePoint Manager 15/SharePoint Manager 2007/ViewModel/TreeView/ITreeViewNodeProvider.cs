using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keutmann.SharePointManager.ViewModel.TreeView
{
    public interface ITreeViewNodeProvider
    {
        IEnumerable<SPTreeNode> LoadChildren(SPTreeNode parentNode);
        SPTreeNode LoadFarmNode();
    }
}
