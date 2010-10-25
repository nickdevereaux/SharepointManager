using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.ObjectModel;
using System.Reflection;

using SPM2.Framework;
using SPM2.Framework.Collections;
using SPM2.Framework.Reflection;


namespace SPM2.SharePoint.Model
{
    public class SPNodeCollection : SPNode
    {
        protected override void LoadChildren()
        {
            List<INode> children = new List<INode>();

            ClassDescriptorCollection descriptors = AddInProvider.Current.TypeAttachments.GetValue(this.AddInID);
            Dictionary<Type, ClassDescriptor> types = GetTypes(descriptors);

            IEnumerable collection = (IEnumerable)this.SPObject;
            foreach (object instance in collection)
            {
                Type instanceType = instance.GetType();

                if (types.ContainsKey(instanceType))
                {
                    // Use defined SPNode
                    ClassDescriptor descriptor = types[instanceType];

                    ISPNode node = (ISPNode)Activator.CreateInstance(descriptor.ClassType);
                    node.SPObject = instance;
                    node.Setup(this.SPObject, descriptor);
                    children.Add(node);
                }
                else
                {
                    //// Auto create the SPNode 
                    //Type nodeType = CreateNodeType(instanceType);
                    ClassDescriptor descriptor = new ClassDescriptor(instance.GetType());

                    SPNode node = new SPNode();
                    //INode node = (INode)Activator.CreateInstance(descriptor.ClassType);
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
