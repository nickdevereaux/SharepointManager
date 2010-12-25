using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;

using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint;

using SPM2.Framework;
using SPM2.Framework.Collections;
using SPM2.Framework.Reflection;
using SPM2.Framework.ComponentModel;
using SPM2.SharePoint;
using System.Collections.ObjectModel;


namespace SPM2.SharePoint.Model
{
    public class SPNode : TreeViewItemModel, ISPNode
    {
        public virtual string SPTypeName { get; set; }
        public virtual string AddInID { get; set; }
        public virtual string Url { get; set; }

        private object _spObject = null;
        public object SPObject 
        { 
            get
            {
                if(this._spObject == null)
                {
                    this._spObject = GetSPObject();
                }
                return this._spObject;
            }
            set
            {
                this._spObject = value;
            }
        }


        public object SPParent { get; set; }

        private ClassDescriptor _descriptor = null;
        public ClassDescriptor Descriptor 
        {
            get
            {
                if (_descriptor == null)
                {
                    _descriptor = new ClassDescriptor(this.GetType());
                }
                return _descriptor;
            }
            set
            {
                this._descriptor = value;
            }
        }

        private Type _spObjectType = null;
        public Type SPObjectType
        {
            get
            {
                if (_spObjectType == null)
                {
                    if (this._spObject != null)
                    {
                        _spObjectType = this._spObject.GetType();
                    }

                    if (_spObjectType == null)
                    {
                        AdapterItemTypeAttribute attrib = this.GetType().GetAttribute<AdapterItemTypeAttribute>(true);
                        if (attrib != null)
                        {
                            _spObjectType = Type.GetType(attrib.Name, true, false);
                        }
                    }

                    if (_spObjectType != null)
                    {
                        PropertyGridTypeConverter.AddTo(_spObjectType);
                    }
                }
                return _spObjectType;
            }
            set
            {
                this._spObjectType = value;
            }
        }


        public override string Text
        {
            get
            {
                if (String.IsNullOrEmpty(base.Text))
                {
                    base.Text = this.Descriptor.GetTitle(this.SPObject);
                }
                return base.Text;
            }
            set
            {
                base.Text = value;
            }
        }

        public override string ToolTipText
        {
            get
            {
                if (String.IsNullOrEmpty(base.ToolTipText))
                {
                    base.ToolTipText = this.Descriptor.ClassType.Name;
                }
                return base.ToolTipText;
            }
            set
            {
                base.ToolTipText = value;
            }
        }

        private string _iconUri = null;
        public virtual string IconUri
        {
            get 
            {
                if (String.IsNullOrEmpty(_iconUri))
                {
                    if (this.Descriptor.Icon != null)
                    {
                        string name = this.Descriptor.Icon.Small;
                        if (!String.IsNullOrEmpty(name) && !"BULLET.GIF".Equals(name, StringComparison.Ordinal))
                        {
                            if (this.Descriptor.Icon.Source == IconSource.File)
                            {
                                _iconUri = SharePointContext.GetImagePath(this.Descriptor.Icon.Small);
                            }
                            else
                            {
                                _iconUri = GetResourceImagePath(this.Descriptor.Icon.Small);
                            }
                        }
                    }

                    if (String.IsNullOrEmpty(_iconUri))
                    {
                        _iconUri = Path.Combine(SharePointContext.ImagePath, "mbllistbullet.gif");
                    }
                }
                return _iconUri; 
            }
            set 
            {
                if (_iconUri != value)
                {
                    _iconUri = value;
                    this.OnPropertyChanged("IconUri");
                }
            }
        }


        private IEnumerable<Lazy<SPNode>> _importedNodes = null;
        public IEnumerable<Lazy<SPNode>> ImportedNodes
        {
            get
            {
                if (_importedNodes == null)
                {
                    _importedNodes = CompositionProvider.GetExports<SPNode>(this.Descriptor.ClassType);
                }
                return _importedNodes;
            }
            set
            {
                _importedNodes = value;
            }
        }

        private Dictionary<Type, SPNode> _nodeDictionary = null;
        protected Dictionary<Type, SPNode> NodeDictionary
        {
            get
            {
                if (_nodeDictionary == null)
                {
                    _nodeDictionary = GetNodeDictionary();
                }
                return _nodeDictionary;
            }
            set
            {
                _nodeDictionary = value;
            }
        }



        public virtual void Setup(object spParent)
        {
            this.SPParent = spParent;

            try
            {
                SetupContextMenu();

            }
            catch (Exception ex)
            {
#if DEBUG
                Trace.Fail(ex.Message, ex.StackTrace);
#else
                Trace.Fail(ex.Massage);
#endif

                throw ex; 
            }
        }

        private void SetupContextMenu()
        {
            this.ContextMenuItems = new ObservableCollection<IContextMenuItem>();
            LoadContextMenuNodes(typeof(SPNode));
            LoadContextMenuNodes(this.GetType());
        }

        private void LoadContextMenuNodes(Type fromType)
        {
            OrderingCollection<IContextMenuItem> orderedItems = CompositionProvider.GetOrderedExports<IContextMenuItem>(fromType);
            foreach (var item in orderedItems)
            {
                IContextMenuItem menuItem = item.Value;
                menuItem.SetupItem(this);
                this.ContextMenuItems.Add(item.Value);
            }
        }


        public virtual void Update()
        {
            if (this.SPObject != null)
            {
                this.SPObject.InvokeMethod("Update", true);
            }
        }

        public virtual SPNode Clone()
        {
            SPNode result = (SPNode)Activator.CreateInstance(this.GetType());
            //result.AddInID = this.AddInID;
            //result.IconUri = this.IconUri;
            //result.SPParent = this.SPParent;
            //result.SPObject = this.SPObject;
            //result.SPObjectType = this.SPObjectType;
            //result.Descriptor = this.Descriptor;
            //result.Text = this.Text;
            //result.ToolTipText = this.ToolTipText;
            //result.SPTypeName = this.SPTypeName;
            //result.Url = this.Url;

            return result;
        }

        public virtual object GetSPObject()
        {
            object result = null;
            if (this.SPParent != null)
            {
                PropertyDescriptorCollection des = TypeDescriptor.GetProperties(this.SPParent.GetType());
                foreach (PropertyDescriptor info in des)
                {

                    if (this.SPObjectType == info.PropertyType)
                    {
                        // Use the name from the Property in the object model.
                        this.Descriptor.Title = info.DisplayName;

                        result = info.GetValue(this.SPParent);

                        break;
                    }
                }
            }

            return result;
        }

        public virtual Type GetSPObjectType()
        {
            return this.SPObjectType;
        }

        public const string ResourceImagePath = "/SPM2.SharePoint;component/Resources/Images/";
        public static string GetResourceImagePath(string filename)
        {
            return  ResourceImagePath + filename;
        }


        public override void LoadChildren()
        {
            if (this.SPObject != null)
            {
                this.Children.Clear();

#if DEBUG
                Stopwatch watch = new Stopwatch();
                watch.Start();
#endif
                
                this.Children.AddRange(LoadUnorderedChildren().OrderBy(p => p.Text));

#if DEBUG
                watch.Stop();
                Trace.WriteLine(String.Format("Load Properties: Type:{0} - Time {1} milliseconds.", this.SPObjectType.Name, watch.ElapsedMilliseconds));
#endif
            }
        }


        private IEnumerable<ITreeViewItemModel> LoadUnorderedChildren()
        {
            PropertyDescriptorCollection propertyDescriptors = TypeDescriptor.GetProperties(this.SPObjectType);
            foreach (PropertyDescriptor info in propertyDescriptors)
            {

                if (this.NodeDictionary.ContainsKey(info.PropertyType))
                {
                    SPNode node = this.NodeDictionary[info.PropertyType];

                    //Ensure that the child node instance is unique in the TreeView
                    node = node.Clone();
                    node.Text = info.DisplayName;
                    node.Setup(this.SPObject);

                    yield return node;
                }
            }
        }


        public virtual IEnumerable<SPNode> NodesToExpand()
        {
            return null;
        }

        public virtual bool IsDefaultSelected()
        {
            return false;
        }

        protected Dictionary<Type, SPNode> GetNodeDictionary()
        {
            Dictionary<Type, SPNode> types = new Dictionary<Type, SPNode>();
            foreach (var lazyItem in this.ImportedNodes)
            {
                SPNode node = lazyItem.Value;

                if (node.Descriptor.AdapterItemType != null)
                {
                    types.AddOrReplace(node.Descriptor.AdapterItemType, node);
                }
            }
            return types;
        }



    }
}
