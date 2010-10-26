using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.ObjectModel;
using System.Reflection;

using Microsoft.SharePoint.Client;

using SPM2.Framework;
using SPM2.Framework.Collections;
using SPM2.Framework.Reflection;


namespace SPM2.SharePoint.Model
{
    public class SPNodeCollection : SPNode
    {
        private ClassDescriptor _collectionItem = null;
        public ClassDescriptor CollectionItem
        {
            get 
            {
                if (this._collectionItem == null)
                {
                    this._collectionItem = FindCollectionItem();
                }
                return _collectionItem; 
            }
            set { _collectionItem = value; }
        }


        private ClassDescriptor FindCollectionItem()
        {
            ClassDescriptor result = null;
            if (this.SPObjectType != null)
            {
                Type spType = null;

                ClientCallableTypeAttribute attr = this.SPObjectType.GetAttribute<ClientCallableTypeAttribute>(true);
                if (attr != null)
                {
                    spType = attr.CollectionChildItemType;
                }

                if (spType == null)
                {
                    Type[] argTypes = this.SPObjectType.GetBaseGenericArguments();
                    if (argTypes != null && argTypes.Length == 1)
                    {
                        spType = argTypes[0];
                    }
                }

                if (spType != null && AddInProvider.Current.ClassDescriptorLookup.ContainsKey(spType))
                {
                    result = AddInProvider.Current.ClassDescriptorLookup[spType];
                }                
            }

            return result;
        }

        protected override void LoadChildren()
        {
            List<INode> children = new List<INode>();

            ClassDescriptorCollection descriptors = AddInProvider.Current.TypeAttachments.GetValue(this.Descriptor.AddInID);

            IEnumerable collection = (IEnumerable)this.SPObject;
            foreach (object instance in collection)
            {
                Type instanceType = instance.GetType();
                ClassDescriptor descriptor = null;
                ISPNode node = node = null;

                if (AddInProvider.Current.ClassDescriptorLookup.ContainsKey(instanceType))
                {
                    descriptor = AddInProvider.Current.ClassDescriptorLookup[instanceType];
                }
                else
                {
                    if (this.CollectionItem != null)
                    {
                        descriptor = this.CollectionItem;
                    }
                }

                if (descriptor != null)
                {
                    node = descriptor.CreateInstance<ISPNode>();
                    node.SPObject = instance;
                    node.Setup(this.SPObject, descriptor);

                    children.Add(node);
                }

            }

            var orderedList = from p in children.Cast<ITreeViewItemModel>()
                                orderby p.Text
                                select p;

            foreach (ITreeViewItemModel item in orderedList)
            {
                this.Children.Add(item);
            }

        }
    }
}
