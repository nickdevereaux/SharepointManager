using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace SPM2.Framework
{

    /// <summary>
    /// Base class for all ViewModel classes displayed by TreeViewItems.  
    /// This acts as an adapter between a raw data object and a TreeViewItem.
    /// </summary>
    public class TreeViewItemModel : INotifyPropertyChanged, ITreeViewItemModel
    {

        #region Data


        private string _text = null;
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
        public virtual string ToolTipText
        {
            get { return _toolTipText; }
            set { _toolTipText = value; }
        }


        static readonly ITreeViewItemModel DummyChild = new TreeViewItemModel(null, false);
        /// <summary>
        /// Returns true if this object's Children have not yet been populated.
        /// </summary>
        public bool HasDummyChild
        {
            get { return this.Children.Count == 1 && this.Children[0] == DummyChild; }
        }





        ObservableCollection<ITreeViewItemModel> _children = null;
        /// <summary>
        /// Returns the logical child items of this object.
        /// </summary>
        public ObservableCollection<ITreeViewItemModel> Children
        {
            get { return _children; }
            set { _children = value; }
        }



        bool _isExpanded;
        /// <summary>
        /// Gets/sets whether the TreeViewItem 
        /// associated with this object is expanded.
        /// </summary>
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

                // Expand all the way up to the root.
                if (_isExpanded && _parent != null)
                {
                    _parent.IsExpanded = true;
                }

                // Lazy load the child items, if necessary.
                if (this.HasDummyChild)
                {
                    this.Children.Remove(DummyChild);
                    this.LoadChildren();
                }
            }
        }

        bool _isSelected;
        /// <summary>
        /// Gets/sets whether the TreeViewItem 
        /// associated with this object is selected.
        /// </summary>
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
        /// Gets/sets whether the TreeViewItem is 
        /// </summary>
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


        readonly TreeViewItemModel _parent;
        public TreeViewItemModel Parent
        {
            get { return _parent; }
        }


        #endregion // Data

        #region Constructors

        protected TreeViewItemModel() : this(null, true)
        {

        }

        protected TreeViewItemModel(TreeViewItemModel parent, bool lazyLoadChildren)
        {
            _parent = parent;

            _children = new ObservableCollection<ITreeViewItemModel>();

            if (lazyLoadChildren)
            {
                _children.Add(DummyChild);
            }
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
