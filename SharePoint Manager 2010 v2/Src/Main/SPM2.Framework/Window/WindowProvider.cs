using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SPM2.Framework.Reflection;

namespace SPM2.Framework
{


    public class WindowProvider : AbstractProvider<IWindowsProvider>, IWindowsProvider
    {

        public event WindowCreatedEventHandler WindowCreated;

        public IList<T> BuildWindows<T>(string id) where T : IAddInWindow
        {
            IList<T> result = AddInProvider.Current.CreateAttachments<T>(id,
                delegate(ClassDescriptor descriptor, T instance)
                {
                    instance.Title = descriptor.Title;
                    OnWindowCreated(instance);
                    return true;
                });

            return result;
        }


        public void OnWindowCreated(IAddInWindow window)
        {
            if (this.WindowCreated != null)
            {
                WindowEventArgs e = new WindowEventArgs();
                e.Window = window;
                this.WindowCreated.Invoke(this, e);
            }
        }
    }
}
