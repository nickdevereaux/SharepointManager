using System;
using SPM2.Framework.Collections;
using System.Collections.ObjectModel;
using ICSharpCode.TreeView;
namespace SPM2.Framework
{
    public interface IItemNode : ISharpTreeNode
    {
        string ToolTipText { get; set; }
        bool IsSelected { get; set; }
        bool IsHidden { get; set; }
        string TextColor { get; set; }
   }
}
