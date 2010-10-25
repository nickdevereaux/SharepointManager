using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SPM2.Framework;
using SPM2.Framework.Reflection;

namespace SPM2.Framework.Collections
{
    public class ClassDescriptorCollection : List<ClassDescriptor>
    {
        private AttachToAttribute Root = null;
        private List<AttachToAttribute> AttributeList = new List<AttachToAttribute>();
        
        public bool IsSorted = true;

        public ClassDescriptorCollection()
        {
        }

        public ClassDescriptorCollection(ClassDescriptor descriptor, AttachToAttribute attribute)
        {
            this.Add(descriptor, attribute);
        }

        public void Add(ClassDescriptor descriptor, AttachToAttribute attribute)
        {
            attribute.Descriptor = descriptor;

            AttachToAttribute replaceAttribute = null;
            bool add = true;
            foreach (AttachToAttribute item in this.AttributeList)
            {
                if (descriptor.AddInID == item.Replace)
                {
                    add = false;
                }
                if (attribute.Replace == item.Descriptor.AddInID)
                {
                    replaceAttribute = item;
                }
            }

            if (replaceAttribute != null)
            {
                this.AttributeList.Remove(replaceAttribute);
                this.Remove(replaceAttribute.Descriptor);
                add = true;
            }

            if (add)
            {
                this.AttributeList.Add(attribute);
                this.Add(attribute.Descriptor);
            }
        }

        public new List<ClassDescriptor>.Enumerator GetEnumerator()
        {
            Sort();
            return base.GetEnumerator();
        }

        /// <summary>
        /// Ensure that the descriptors are sorted according to the AttachTo attributes.
        /// </summary>
        public new void Sort()
        {
            if (!this.IsSorted)
            {
                BuildTree();

                List<AttachToAttribute> list = this.Root.GetSortedAttachToAttributes();
                this.Clear();
                this.AddRange(from p in list select p.Descriptor);
                this.IsSorted = true;
            }
        }

        private void BuildTree()
        {
            this.Root = new AttachToAttribute();

            foreach (AttachToAttribute attribute in AttributeList)
            {
                bool found = false;
                foreach (AttachToAttribute item in AttributeList)
                {
                    bool added = attribute.Add(item);
                    if (added)
                    {
                        found = true;
                    }
                }

                if (!found)
                {
                    this.Root.Children.Add(attribute);
                }
            }
        }

        private new void Add(ClassDescriptor descriptor)
        {
            this.IsSorted = false;
            base.Add(descriptor);
        }



        
    }
}
