using System;
namespace SPM2.Framework
{
    public interface ITreeViewItemModel
    {
        event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        string Text { get; set; }
        string ToolTipText { get; set; }
        bool HasDummyChild { get; }
        bool IsExpanded { get; set; }
        bool IsSelected { get; set; }
        bool IsHidden { get; set; }

        SPM2.Framework.TreeViewItemModel Parent { get; }
        System.Collections.ObjectModel.ObservableCollection<SPM2.Framework.ITreeViewItemModel> Children { get; }
   }
}
