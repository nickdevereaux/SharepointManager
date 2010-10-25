using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SPM2.Framework.Collections;
using System.Reflection;
using System.Diagnostics;
using SPM2.Framework.Reflection;
using SPM2.Framework;
using SPM2.Framework.Configuration;

namespace SPM2.Framework
{
    public delegate bool InitializerDelegate<T>(ClassDescriptor descriptor, T instance);

    public class AddInProvider : AbstractProvider<IAddInProvider>, IAddInProvider
    {
        public TypeAttachmentDictionary TypeAttachments { get; set; }


        AddInProvider()
        {
            Load();
        }


        public IList<T> CreateAttachments<T>(string id, InitializerDelegate<T> initializer)
        {
            List<T> result = new List<T>();

            ClassDescriptorCollection descriptors = TypeAttachments.GetValue(id);
            

            if (descriptors != null)
            {
                foreach (ClassDescriptor descriptor in descriptors)
                {
                    T instance = descriptor.CreateInstance<T>();
                    if (instance != null)
                    {
                        if (initializer != null)
                        {
                            if (initializer.Invoke(descriptor, instance))
                            {
                                result.Add((T)instance);
                            }
                        }
                        else
                        {
                            result.Add((T)instance);
                        }
                    }
                }
            }

            return result;
        }


        #region Private Methods

        private void Load()
        {
            Clear();
            LoadTypes();
        }

        private void Clear()
        {
            // Clear all
            TypeAttachments = new TypeAttachmentDictionary();
        }

        private void LoadTypes()
        {
            foreach (Assembly assem in AssemblyProvider.Current.Assemblies)
            {
                if (LoadTypes(assem))
                {
                    LoadAssembly(assem);
                }
            }
        }

        private bool LoadTypes(Assembly assem)
        {
            bool result = true;

            LoadAddInTypesAttribute attribute = assem.GetCustomAttributes(typeof(LoadAddInTypesAttribute), false).Cast<LoadAddInTypesAttribute>().FirstOrDefault();
            if (attribute != null)
            {
                result = attribute.Load;
            }

            return result;
        }

        private void LoadAssembly(Assembly assem)
        {
            Type[] types = assem.GetTypes();

            foreach (Type type in types)
            {
                ClassDescriptor descriptor = new ClassDescriptor(type);

                if (descriptor.IsAddIn)
                {
                    foreach (AttachToAttribute attribute in descriptor.AttachTo)
                    {
                        TypeAttachments.Add(descriptor, attribute);
                    }
                }
            }
        }

        #endregion
    }
}
