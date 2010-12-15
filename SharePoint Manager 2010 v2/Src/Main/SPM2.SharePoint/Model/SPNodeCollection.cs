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
using Microsoft.SharePoint.Administration;


namespace SPM2.SharePoint.Model
{
    public class SPNodeCollection : SPNode
    {
        private SPNode _defaultNode = null;
        public SPNode DefaultNode
        {
            get 
            {
                if (this._defaultNode == null)
                {
                    this._defaultNode = FindDefaultNode();
                }
                return _defaultNode; 
            }
            set { _defaultNode = value; }
        }


        private SPNode FindDefaultNode()
        {
            SPNode result = null;
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

                if (spType != null && this.NodeDictionary.ContainsKey(spType))
                {
                    result = this.NodeDictionary[spType];
                }                
            }

            return result;
        }

        public override void LoadChildren()
        {
            if (this.SPObject == null)
            {
                return;
            }

            this.Children.Clear();

            List<INode> children = new List<INode>();

            Dictionary<Type, SPNode> nodeDictionary = GetNodeDictionary();

            IEnumerable collection = (IEnumerable)this.SPObject;
            foreach (object instance in collection)
            {
                Type instanceType = instance.GetType();
                SPNode node = null;

                if (nodeDictionary.ContainsKey(instanceType))
                {
                    node = nodeDictionary[instanceType];
                }
                else
                {
                    if (this.DefaultNode != null)
                    {
                        node = this.DefaultNode;
                    }
                }

                if (node != null)
                {
                    // Always create a new node, because the object has to be unique for each item in the treeview.
                    node = node.Clone();
                    node.SPObject = instance;
                    node.Setup(this.SPObject);
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
