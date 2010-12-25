using System;
using SPM2.Framework.Collections;
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
        string TextColor { get; set; }
        ITreeViewItemModel Parent { get; }
        ObservableCollectionXML<ITreeViewItemModel> Children { get; }
   }
}
