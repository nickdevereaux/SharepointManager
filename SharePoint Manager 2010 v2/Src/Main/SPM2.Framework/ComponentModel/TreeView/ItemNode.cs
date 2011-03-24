using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using SPM2.Framework.ComponentModel;
using System.Xml.Serialization;
using SPM2.Framework.Collections;
using ICSharpCode.TreeView;

namespace SPM2.Framework
{

    /// <summary>
    /// Base class for all ViewModel classes displayed by TreeViewItems.  
    /// This acts as an adapter between a raw data object and a TreeViewItem.
    /// </summary>
    [Serializable]
    public class ItemNode : SharpTreeNode, IItemNode
    {

        #region Data


        private object _text = null;

        [Browsable(false)]
        public override object Text
        {
            get { return _text; }
            set { _text = value; }
        }

        //private string _description = null;
        //public string Description
        //{
        //    get { return _description; }
        //    set { _description = value; }
        //}

        [Browsable(false)]
        public virtual string ToolTipText
        {
            get { return this.ToolTip.ToString(); }
            set { this.ToolTip = value; }
        }


        //static readonly ITreeViewItemModel DummyChild = new DummyChild();

        ///// <summary>
        ///// Returns true if this object's Children have not yet been populated.
        ///// </summary>
        //[Browsable(false)]
        //[XmlIgnore]
        //public bool HasDummyChild
        //{
        //    get { return this.Children.Count == 1 && this.Children[0] is DummyChild; }
        //}


        //bool _isExpanded;
        ///// <summary>
        ///// Gets/sets whether the TreeViewItem 
        ///// associated with this object is expanded.
        ///// </summary>
        //[Browsable(false)]
        //[XmlAttribute]
        //public override bool IsExpanded
        //{
        //    get { return _isExpanded; }
        //    set
        //    {
        //        if (value != _isExpanded)
        //        {
        //            _isExpanded = value;
        //            this.OnPropertyChanged("IsExpanded");
        //        }

        //        // Lazy load the child items, if necessary.
        //        if (this.HasDummyChild)
        //        {
        //            this.LoadChildren();
        //            this.LazyLoading = false;
        //        }
        //    }
        //}

        bool _isSelected;
        /// <summary>
        /// Gets/sets whether the TreeViewItem 
        /// associated with this object is selected.
        /// </summary>
        [Browsable(false)]
        [XmlAttribute]
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value != _isSelected)
                {
                    _isSelected = value;
                    this.RaisePropertyChanged("IsSelected");
                }
            }
        }

        private bool _isHidden;
        /// <summary>
        /// Gets/sets whether the TreeViewItem is hidden
        /// </summary>
        [Browsable(false)]
        [XmlAttribute]
        public bool IsHidden
        {
            get { return _isHidden; }
            set 
            { 
                if (value != _isHidden)
                {
                    _isHidden = value;
                    this.RaisePropertyChanged("IsHidden");
                }
                 
            }
        }

        //private bool _isFocused = false;
        ///// <summary>
        ///// Gets/Sets whether the TreeViewItem is in focus.
        ///// </summary>
        //public bool IsFocused
        //{
        //    get { return _isFocused; }
        //    set 
        //    {
        //        if (value != _isFocused)
        //        {
        //            _isFocused = value;
        //            this.OnPropertyChanged("IsFocused");
        //        }
        //    }
        //}

        //private IItemNode _parent = null;
        //[Browsable(false)]
        //[XmlIgnore]
        //public IItemNode Parent
        //{
        //    get { return _parent; }
        //}

        private string _textColor = "Black";
        /// <summary>
        /// Sets the color of the text.
        /// </summary>
        [Browsable(false)]
        [XmlAttribute]
        public string TextColor
        {
            get 
            {
                return _textColor; 
            }
            set 
            {
                if (_textColor != value)
                {
                    _textColor = value;
                    this.RaisePropertyChanged("TextColor");
                }
            }
        }


        //[Browsable(false)]
        //[XmlAttribute]
        //public string ContextMenuVisible
        //{
        //    get 
        //    { 
        //        return (this.ContextMenuItems != null && this.ContextMenuItems.Count > 0) ? "Visible" : "Hidden"; 
        //    }
        //}

        //private bool _lazyLoadChildren = true;
        //[Browsable(false)]
        //[XmlAttribute]
        //public bool LazyLoadChildren
        //{
        //    get { return _lazyLoadChildren; }
        //    set { _lazyLoadChildren = value; }
        //}


        //private ObservableCollection<ITreeViewItemModel> _children = null;
        ///// <summary>
        ///// Returns the logical child items of this object.
        ///// </summary>
        //[Browsable(false)]
        //public ObservableCollection<ITreeViewItemModel> Children
        //{
        //    get
        //    {
        //        if (_children == null)
        //        {
        //            _children = new ObservableCollection<ITreeViewItemModel>();
        //            if (this.LazyLoadChildren)
        //            {
        //                _children.Add(DummyChild);
        //            }
        //        }
        //        return _children; 
        //    }
        //    set { _children = value; }
        //}



        #endregion // Data

        #region Constructors

        protected ItemNode() : this(null, true)
        {

        }

        protected ItemNode(ISharpTreeNode parent, bool lazyLoadChildren)
        {
            //this.Parent = parent;
            this.LazyLoading = lazyLoadChildren;
        }

        #endregion // Constructors

        ///// <summary>
        ///// Invoked when the child items need to be loaded on demand.
        ///// Subclasses can override this to populate the Children collection.
        ///// </summary>
        //public virtual void LoadChildren()
        //{
            
        //}


        public virtual void ResetChildren(bool lazy)
        {
            ItemNode clone = this.Clone();
            ReloadChildren(this, clone);
            this.Children.Clear();
            foreach (var child in clone.Children)
            {
                this.Children.Add(child);
            }

            //this.LazyLoading = lazy;
            //if (this.LazyLoading)
            //{
            //    //this.Children.Add(DummyChild);
            //}

            //if (this.IsExpanded)
            //{
            //    this.LoadChildren();
            //}
        }


        public virtual ItemNode Clone()
        {
            ItemNode result = (ItemNode)Activator.CreateInstance(this.GetType());
            foreach (var prop in this.GetType().GetProperties())
            {
                if (prop.CanWrite && prop.CanRead)
                {
                    //object value = prop.GetValue(this, null);
                    //if(value != null)
                    //{
                    //    prop.SetValue(result, value, null);
                    //}
                }
            }


            return result;
        }


        private void ReloadChildren(ItemNode source, ItemNode target)
        {
            if (source.IsExpanded)
            {
                target.EnsureLazyChildren();
                if (source.Children.Count == target.Children.Count)
                {
                    for (int i = 0; i < source.Children.Count; i++)
                    {
                        ItemNode childSource = source.Children[i] as ItemNode;
                        ItemNode childTarget = target.Children[i] as ItemNode;
                        ReloadChildren(childSource, childTarget);
                    }
                }
            }

            
        }

        //#region INotifyPropertyChanged Members

        //public event PropertyChangedEventHandler PropertyChanged;

        //protected virtual void OnPropertyChanged(string propertyName)
        //{
        //    if (this.PropertyChanged != null)
        //        this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        //}

        //#endregion // INotifyPropertyChanged Members

    }
}
