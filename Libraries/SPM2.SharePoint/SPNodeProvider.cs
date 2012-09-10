using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.Client;
using SPM2.Framework;
using SPM2.Framework.Components;
using SPM2.Framework.Xml;
using SPM2.SharePoint.Model;
using SPM2.SharePoint.Rules;

namespace SPM2.SharePoint
{
    [Export()]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class SPNodeProvider : ISPNodeProvider
    {
        public const string AddInId = "SPM2.SharePoint.SPNodeProvider";

        public int ViewLevel { get; set; }

        public SPFarm Farm { get; set; }

        private FirstAcceptRuleEngine<ISPNode> _ruleEngine;

        public SPNodeProvider(SPFarm farm, IEnumerable<INodeIncludeRule> rules)
        {
            ViewLevel = 100;
            Farm = farm;

          
            _ruleEngine = new FirstAcceptRuleEngine<ISPNode>(rules);
        }

        public ISPNode LoadFarmNode()
        {
            var node = Create("Farm", typeof(SPFarm), typeof(SPFarmNode), null, 0);
            node.SPObject = Farm;

            return node;
        }

        public IEnumerable<ISPNode> LoadCollectionChildren(ISPNodeCollection parentNode, int batchCount)
        {
            var list = new List<ISPNode>();
            if (parentNode.SPObject == null) return list;

            int count = 0;
            
 
            if (parentNode.Pointer == null)
            {
                parentNode.ClearChildren();
                parentNode.TotalCount = 0;
                var collection = (IEnumerable)parentNode.SPObject;
                parentNode.Pointer = collection.GetEnumerator();
                //parentNode.Pointer.Reset();
                parentNode.LoadingChildren = parentNode.Pointer.MoveNext();
            }

            while (count < batchCount && parentNode.LoadingChildren)
            {
                var current = parentNode.Pointer.Current;
                Type instanceType = current.GetType();
                ISPNode node = null;

                if (parentNode.NodeTypes.ContainsKey(instanceType))
                {
                    node = parentNode.NodeTypes[instanceType];
                }
                else
                {
                    if (parentNode.DefaultNode != null)
                    {
                        node = parentNode.DefaultNode;
                    }
                }

                if (node != null && RunIncludeRules(node))
                {
                    // Always create a new node, because the object has to be unique for each item in the treeview.
                    var instanceNode = (ISPNode) Activator.CreateInstance(node.GetType());
                    instanceNode.SPObject = parentNode.Pointer.Current;
                    instanceNode.ID = GetCollectionItemID(instanceNode.SPObject, parentNode.TotalCount);

                    // Save the index for finding the node again with having to get the SPObject
                    instanceNode.Index = parentNode.TotalCount;

                    instanceNode.Setup(parentNode);
                    list.Add(instanceNode);
                }
                
                parentNode.LoadingChildren = parentNode.Pointer.MoveNext();
                
                count++;
                parentNode.TotalCount++;
            }
            
            // If there is more nodes in the collection, add a "More" item.
            //if (count >= batchCount && parentNode.LoadingChildren)
            //{
            //    var node = new MoreNode(parentNode);
            //    node.Setup(parentNode);
            //    list.Add(node);
            //}

            if (parentNode.TotalCount <= batchCount)
            {
                // There are a low number of nodes, therefore sort nodes by Text.
                return list.OrderBy(p => p.Text);
            }
            // Just add the elements without any sort, because of the high number of nodes.
            return list;
        }

        private string GetCollectionItemID(object spObject, int index)
        {
            var flags = BindingFlags.IgnoreCase | BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Public;
            if (spObject.PropertyExist("id", flags))
            {
                return spObject.GetPropertyValue<object>("id", flags).ToString();
            }

            if (spObject.PropertyExist("uniqueid", BindingFlags.IgnoreCase))
            {
                return spObject.GetPropertyValue<object>("uniqueid", flags).ToString();
            }

            if (spObject.PropertyExist("name", BindingFlags.IgnoreCase))
            {
                return spObject.GetPropertyValue<object>("name", flags).ToString();
            }

            return spObject.GetType().FullName + index;
        }


        public  IEnumerable<ISPNode> LoadChildren(ISPNode node)
        {
            return LoadUnorderedChildren(node).OrderBy(p => p.Text);
        }

        public  IEnumerable<ISPNode> LoadUnorderedChildren(ISPNode sourceNode)
        {
            var list = new List<ISPNode>();

            if (sourceNode.SPObject == null) return list;

            var propertyDescriptors = TypeDescriptor.GetProperties(sourceNode.SPObjectType);
            try
            {
                // ReSharper disable LoopCanBeConvertedToQuery
                foreach (PropertyDescriptor descriptor in propertyDescriptors)
                {
                    if (sourceNode.NodeTypes.ContainsKey(descriptor.PropertyType))
                    {
                        ISPNode node = sourceNode.NodeTypes[descriptor.PropertyType];

                        // Exclude the node if it do not match the correct view
                        if(!RunIncludeRules(node)) continue;

                        //Ensure that the child node instance is unique in the TreeView
                        node = Create(descriptor.DisplayName, descriptor.PropertyType, node.GetType(), sourceNode, list.Count);
                        list.Add(node);
                    }
                }
                // ReSharper restore LoopCanBeConvertedToQuery
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            return list;
        }

        private bool RunIncludeRules(ISPNode node)
        {
            return _ruleEngine.Check(node, true);
        }

        public  Dictionary<Type, ISPNode> GetChildrenTypes(ISPNode parentNode)
        {
            IEnumerable<Lazy<SPNode>> importedNodes = CompositionProvider.GetExports<SPNode>(parentNode.Descriptor.ClassType);
            var types = new Dictionary<Type, ISPNode>();
            foreach (var lazyItem in importedNodes)
            {
                SPNode node = lazyItem.Value;
                node.NodeProvider = parentNode.NodeProvider;
    
                if (node.Descriptor.AdapterItemType != null)
                {
                    types.AddOrReplace(node.Descriptor.AdapterItemType, node);
                }
            }
            return types;
        }


        public ISPNode FindDefaultNode(ISPNode node)
        {
            ISPNode result = null;
            if (node.SPObjectType != null)
            {
                Type spType = null;

                var attr = node.SPObjectType.GetAttribute<ClientCallableTypeAttribute>(true);
                if (attr != null)
                {
                    spType = attr.CollectionChildItemType;
                }

                if (spType == null)
                {
                    Type[] argTypes = node.SPObjectType.GetBaseGenericArguments();
                    if (argTypes != null && argTypes.Length == 1)
                    {
                        spType = argTypes[0];
                    }
                }

                if (spType != null && node.NodeTypes.ContainsKey(spType))
                {
                    result = node.NodeTypes[spType];
                }
            }

            return result;
        }

        private ISPNode Create(string text, Type spObjectType, Type nodeType, ISPNode parent, int index)
        {
            var node = (ISPNode) Activator.CreateInstance(nodeType);
            node.NodeProvider = this;
            node.Text = text;
            node.SPObjectType = spObjectType;
            node.ID = spObjectType.FullName + index;
            node.Index = index;
            node.Setup(parent);
            return node;
        }

        public string Serialize(ISPNode node)
        {
            string xml = Serializer.ObjectToXML(node);
            return xml;
        }

        public ISPNode Deserialize(string xml)
        {
            if (String.IsNullOrEmpty(xml)) return null;
            return Serializer.XmlToObject<SPFarmNode>(xml);
        }
    }
}