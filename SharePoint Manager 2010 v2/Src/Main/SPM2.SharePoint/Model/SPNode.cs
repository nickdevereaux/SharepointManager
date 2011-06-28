﻿using System;
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
using ICSharpCode.TreeView;


namespace SPM2.SharePoint.Model
{
    public class SPNode : ItemNode, ISPNode
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


        public override object Text
        {
            get
            {
                if (String.IsNullOrEmpty(base.Text+string.Empty))
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
        public override string IconUri
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
                    this.RaisePropertyChanged("IconUri");
                }
            }
        }


        //public override object Icon
        //{
        //    get
        //    {
        //        return ImageExtensions;
        //    }
        //}

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


        }

        public override IEnumerable<object> GetContextMenuItems()
        {
            List<object> result = new List<object>();

            try
            {
                result.AddRange(LoadContextMenuNodes(typeof(SPNode)));
                result.AddRange(LoadContextMenuNodes(this.GetType()));
            }
            catch (Exception ex)
            {
#if DEBUG
                Trace.Fail(ex.Message, ex.StackTrace);
#else
                Trace.Fail(ex.Message);
#endif

                throw ex;
            }

            return result;
        }

        private IEnumerable<object> LoadContextMenuNodes(Type fromType)
        {
            List<object> result = new List<object>();
            OrderingCollection<IContextMenuItem> orderedItems = CompositionProvider.GetOrderedExports<IContextMenuItem>(fromType);
            foreach (var item in orderedItems)
            {
                IContextMenuItem menuItem = item.Value;
                menuItem.SetupItem(this);
                result.Add(menuItem);
            }
            return result;
        }


        public virtual void Update()
        {
            if (this.SPObject != null)
            {
                this.SPObject.InvokeMethod("Update", true);
            }
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

                this.Children.AddRange(LoadUnorderedChildren().OrderBy(p => p.Text).OfType<SharpTreeNode>());

#if DEBUG
                watch.Stop();
                Trace.WriteLine(String.Format("Load Properties: Type:{0} - Time {1} milliseconds.", this.SPObjectType.Name, watch.ElapsedMilliseconds));
#endif
            }
        }


        public virtual void ClearChildren()
        {
            this.Children.Clear();
            
        }

        private IEnumerable<IItemNode> LoadUnorderedChildren()
        {
            PropertyDescriptorCollection propertyDescriptors = TypeDescriptor.GetProperties(this.SPObjectType);
            foreach (PropertyDescriptor info in propertyDescriptors)
            {

                if (this.NodeDictionary.ContainsKey(info.PropertyType))
                {
                    SPNode node = this.NodeDictionary[info.PropertyType];

                    //Ensure that the child node instance is unique in the TreeView
                    node = Create(info.DisplayName, node.GetType(), this.SPObject);

                    yield return node;
                }
            }
        }

        private static SPNode Create(string name, Type nodeType, object spObject)
        {
            SPNode node = (SPNode)Activator.CreateInstance(nodeType);
            node.Text = name;
            node.Setup(spObject);
            return node;
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

        public virtual SPNode Refresh()
        {
            SPNode result = this;
            Type webType = typeof(SPWeb);
            Type siteType = typeof(SPSite);

            List<int> indexs = new List<int>();

            SPNode target = this;
            SPNode parent = target.Parent as SPNode;
            if (parent == null)
            {
                return result;
            }

            int selectedIndex = target.Parent.Children.IndexOf(target);

            indexs.Add(selectedIndex);

            foreach (var node in target.Ancestors().OfType<SPNode>())
            {
                if (node.Parent == null)
                {
                    break;
                }

                indexs.Insert(0, node.Parent.Children.IndexOf(node));

                if (node.SPObjectType == webType || node.SPObjectType == siteType)
                {
                    target = node;
                    parent = target.Parent as SPNode;

                    break;
                }
            }

            if (target == this)
            {
                indexs = new List<int> { selectedIndex }; 
            }


            var enumerator = indexs.GetEnumerator();
            if (enumerator.MoveNext())
            {
                foreach (var node in parent.Descendants())
                {
                    node.Text += " Old";
                }
                parent.LazyLoading = true;
                parent.ClearChildren();
                parent.IsExpanded = true;
                result = parent.RefreshNodes(enumerator);
            }
            return result;
        }


        internal SPNode RefreshNodes(IEnumerator<int> enumerator)
        {
            SPNode result = null;
            int index = enumerator.Current;
            bool lastIndex = !enumerator.MoveNext();

            if (this.Children.Count > index)
            {
                SPNode child = this.Children[index] as SPNode;
                
                if (lastIndex)
                {
                    child.IsSelected = true;
                    result = child;
                }
                else
                {
                    // IsExpanded = true; Auto load of new nodes
                    child.IsExpanded = true;
                    result = child.RefreshNodes(enumerator);
                }

            }
            return result;
        }

    }
}
