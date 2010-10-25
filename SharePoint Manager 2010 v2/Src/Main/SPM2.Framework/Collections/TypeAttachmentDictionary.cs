using System;
using System.Collections.Generic;
using SPM2.Framework;
using SPM2.Framework.Reflection;

namespace SPM2.Framework.Collections
{
    public class TypeAttachmentDictionary : Dictionary<string, ClassDescriptorCollection>
    {

        public void Add(ClassDescriptor descriptor, AttachToAttribute attribute)
        {
            if (this.ContainsKey(attribute.Id))
            {
                ClassDescriptorCollection list = this[attribute.Id];
                list.Add(descriptor, attribute);
            }
            else
            {
                this.Add(attribute.Id, new ClassDescriptorCollection(descriptor, attribute));
            }
        }

    }
}
