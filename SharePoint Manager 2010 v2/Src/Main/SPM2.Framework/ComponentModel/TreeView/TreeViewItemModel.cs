using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using SPM2.Framework.ComponentModel;
using System.Xml.Serialization;
using SPM2.Framework.Collections;

namespace SPM2.Framework
{

    /// <summary>
    /// Base class for all ViewModel classes displayed by TreeViewItems.  
    /// This acts as an adapter between a raw data object and a TreeViewItem.
    /// </summary>
    [Serializable]
    public class TreeViewItemModel : INotifyPropertyChanged, ITreeViewItemModel
    {

        #region Data


        private string _text = null;
        [Browsable(false)]
        public virtual string Text
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

        private string _toolTipText = null;
        [Browsable(false)]
        public virtual string ToolTipText
        {
            get { return _toolTipText; }
            set { _toolTipText = value; }
        }


        static readonly ITreeViewItemModel DummyChild = new DummyChild();

        /// <summary>
        /// Returns true if this object's Children have not yet been populated.
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        public bool HasDummyChild
        {
            get { return this.Children.Count == 1 && this.Children[0] is DummyChild; }
        }


        bool _isExpanded;
        /// <summary>
        /// Gets/sets whether the TreeViewItem 
        /// associated with this object is expanded.
        /// </summary>
        [Browsable(false)]
        [XmlAttribute]
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                if (value != _isExpanded)
                {
                    _isExpanded = value;
                    this.OnPropertyChanged("IsExpanded");
                }

                // Lazy load the child items, if necessary.
                if (this.HasDummyChild)
                {
                    this.Children.Remove(DummyChild);
                    this.LoadChildren();
                    this.LazyLoadChildren = false;
                }
            }
        }

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
                    this.OnPropertyChanged("IsSelected");
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
                    this.OnPropertyChanged("IsHidden");
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

        private ITreeViewItemModel _parent = null;
        [Browsable(false)]
        [XmlIgnore]
        public ITreeViewItemModel Parent
        {
            get { return _parent; }
        }

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
                    this.OnPropertyChanged("TextColor");
                }
            }
        }


        private ObservableCollection<IContextMenuItem> _contextMenuItems = null;
        [Browsable(false)]
        [XmlIgnore]
        public virtual ObservableCollection<IContextMenuItem> ContextMenuItems
        {
            get
            {
                //if (_contextMenuItems.Count == 0)
                //{
                //    MenuItem item = new MenuItem(DateTime.Now.ToString());
                //    _contextMenuItems.Add(item);
                //}
                return _contextMenuItems;
            }
            set
            {
                _contextMenuItems = value;
            }

        }

        [Browsable(false)]
        [XmlAttribute]
        public string ContextMenuVisible
        {
            get 
            { 
                return (this.ContextMenuItems != null && this.ContextMenuItems.Count > 0) ? "Visible" : "Hidden"; 
            }
        }

        private bool _lazyLoadChildren = true;
        [Browsable(false)]
        [XmlAttribute]
        public bool LazyLoadChildren
        {
            get { return _lazyLoadChildren; }
            set { _lazyLoadChildren = value; }
        }


        private ObservableCollection<ITreeViewItemModel> _children = null;
        /// <summary>
        /// Returns the logical child items of this object.
        /// </summary>
        [Browsable(false)]
        public ObservableCollection<ITreeViewItemModel> Children
        {
            get
            {
                if (_children == null)
                {
                    _children = new ObservableCollection<ITreeViewItemModel>();
                    if (this.LazyLoadChildren)
                    {
                        _children.Add(DummyChild);
                    }
                }
                return _children; 
            }
            set { _children = value; }
        }



        #endregion // Data

        #region Constructors

        protected TreeViewItemModel() : this(null, true)
        {

        }

        protected TreeViewItemModel(TreeViewItemModel parent, bool lazyLoadChildren)
        {
            _parent = parent;
            _lazyLoadChildren = lazyLoadChildren;
        }

        #endregion // Constructors

        /// <summary>
        /// Invoked when the child items need to be loaded on demand.
        /// Subclasses can override this to populate the Children collection.
        /// </summary>
        public virtual void LoadChildren()
        {
            
        }


        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion // INotifyPropertyChanged Members

    }
}
