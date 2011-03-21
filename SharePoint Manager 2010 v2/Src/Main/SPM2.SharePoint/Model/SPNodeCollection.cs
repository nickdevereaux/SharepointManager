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
using ICSharpCode.TreeView;


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

        private IEnumerator _pointer = null;
        protected IEnumerator Pointer
        {
            get 
            {
                return _pointer; 
            }
        }

        
        private int totalCount = 0;
        private bool moveNext = true;


        public override void LoadChildren()
        {
            if (this.SPObject == null)
            {
                return;
            }

            int batchCount = SPExplorerSettings.Current.BatchNodeLoad;

            List<SharpTreeNode> list = new List<SharpTreeNode>();
            int count = 0;

            if (_pointer == null)
            {
                this.Children.Clear();
                totalCount = 0;
                IEnumerable collection = (IEnumerable)this.SPObject;
                _pointer = collection.GetEnumerator();
                //_pointer.Reset();
                moveNext = _pointer.MoveNext();
            }

            while (count <= batchCount && moveNext)
            {

                Type instanceType = this.Pointer.Current.GetType();
                SPNode node = null;

                if (this.NodeDictionary.ContainsKey(instanceType))
                {
                    node = this.NodeDictionary[instanceType];
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
                    node.SPObject = this.Pointer.Current;
                    node.Setup(this.SPObject);
                    list.Add(node);
                }

                moveNext = this.Pointer.MoveNext();
                count++;
                totalCount++;
            }

            // If there is more nodes in the collection, add a "More" item.
            if (count >= batchCount && moveNext)
            {
                MoreNode node = new MoreNode(this);
                node.Setup(this.SPObject);
                list.Add(node);
            }

            if (totalCount <= batchCount)
            {
                // There are a low number of nodes, therefore sort nodes by Text.
                this.Children.AddRange(list.OrderBy(p => p.Text));
            }
            else
            {
                // Just add the elements without any sort, because of the high number of nodes.
                this.Children.AddRange(list);
            }
        }


        public void LoadNextBatch()
        {
            // Ensure that the last node is the "MoreNode".
            int nodeIndex = this.Children.Count - 1;
            SharpTreeNode node = this.Children[nodeIndex];


            if (node is MoreNode)
            {
                // Remove the "MoreNode" from this.Children.
                this.Children.RemoveAt(nodeIndex);

                // Load the next batch!
                this.LoadChildren();
            }
        }

    }
}
