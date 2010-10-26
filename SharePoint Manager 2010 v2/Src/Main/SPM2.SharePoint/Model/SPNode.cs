using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Collections;

using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint;
using System.IO;

using SPM2.Framework;
using SPM2.Framework.Collections;
using SPM2.Framework.Reflection;
using SPM2.SharePoint;
using SPM2.Framework.ComponentModel;


namespace SPM2.SharePoint.Model
{
    public class SPNode : TreeViewItemModel, ISPNode
    {
        public virtual string SPTypeName { get; set; }
        public virtual string AddInID { get; set; }
        public virtual string Url { get; set; }

        private string _iconUri = Path.Combine(SharePointContext.ImagePath, "mbllistbullet.gif");
        public virtual string IconUri
        {
            get { return _iconUri; }
            set { _iconUri = value; }
        }


        public object _spObject = null;
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
        public ClassDescriptor Descriptor { get; set; }

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

                    PropertyGridTypeConverter.AddTo(this.SPObjectType);
                }
                return _spObjectType;
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




        public virtual void Setup(object spObject, ClassDescriptor descriptor)
        {
            this.SPParent = spObject;
            this.Descriptor = descriptor;
            this.AddInID = descriptor.AddInID;
        }

        public virtual object GetSPObject()
        {
            object result = null;
            if (this.SPParent != null)
            {
                PropertyDescriptorCollection des = TypeDescriptor.GetProperties(this.SPParent.GetType());
                foreach(PropertyDescriptor info in des)
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


        public virtual void Update()
        {
            this.SPObject.InvokeMethod("Update", true);
        }

        protected override void LoadChildren()
        {
            if (this.SPObject != null)
            {
                ClassDescriptorCollection descriptors = AddInProvider.Current.TypeAttachments.GetValue(this.Descriptor.AddInID);
                Dictionary<Type, ClassDescriptor> types = GetTypes(descriptors);

                PropertyDescriptorCollection propertyDescriptors = TypeDescriptor.GetProperties(this.SPObjectType);
                foreach (PropertyDescriptor info in propertyDescriptors)
                {

                    object obj = info.GetValue(this.SPObject);

                    if (obj != null)
                    {
                        if (types.ContainsKey(info.PropertyType))
                        {
                            ClassDescriptor descriptor = types[info.PropertyType];

                            ISPNode node = (ISPNode)Activator.CreateInstance(descriptor.ClassType);
                            node.Text = info.DisplayName;
                            node.SPObject = obj;
                            node.Setup(this.SPObject, descriptor);

                            this.Children.Add(node);
                        }
                    }
                }
            }
        }

        protected Dictionary<Type, ClassDescriptor> GetTypes(ClassDescriptorCollection descriptors)
        {
            Dictionary<Type, ClassDescriptor> types = new Dictionary<Type, ClassDescriptor>();
            if (descriptors != null)
            {
                foreach (ClassDescriptor descriptor in descriptors)
                {
                    if (descriptor.AdapterItemType != null)
                    {
                        types.AddOrReplace(descriptor.AdapterItemType, descriptor);
                    }
                }
            }
            return types;
        }

        //protected Type GetArgumentType(ClassDescriptor descriptor)
        //{
        //    Type result = descriptor.ClassType;
        //    Type[] argumentTypes = descriptor.ClassType.GetGenericArguments();
        //    if (argumentTypes.Length > 0)
        //    {
        //        result = argumentTypes[0];
        //    }
        //    return result;
        //}

        //protected Type CreateNodeType(Type spObjectType)
        //{
        //    Type nodeType = null;
        //    bool isAssignable = TypeExtensions.IEnumerableType.IsAssignableFrom(spObjectType);
        //    if (isAssignable && spObjectType != TypeExtensions.StringType && !spObjectType.IsArray)
        //    {
        //        Type collectionType = spObjectType;

        //        Type itemType = null;
        //        Type[] argTypes = spObjectType.GetBaseGenericArguments();
        //        if (argTypes != null && argTypes.Length > 0)
        //        {
        //            itemType = argTypes[0];
        //            nodeType = SharedTypes.SPNodeCollectionType.MakeGenericType(collectionType, itemType);
        //        }

        //    }
        //    else
        //    {
        //        nodeType = SharedTypes.SPNodeType.MakeGenericType(spObjectType);
        //    }
        //    return nodeType;
        //}

        public string GetResourceImagePath(string filename)
        {
            return "/SPM2.SharePoint;component/Resources/Images/"+filename;
        }




    }
}
