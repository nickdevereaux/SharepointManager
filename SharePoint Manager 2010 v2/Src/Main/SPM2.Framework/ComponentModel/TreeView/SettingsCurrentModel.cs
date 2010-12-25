using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPM2.Framework.ComponentModel
{
    public class SettingsCurrentModel<T> : SettingsModel
    {

        private static object lockObject = new object();
        private static T _current = default(T);

        public static T Current
        {
            get
            {
                if (object.Equals(_current, default(T)))
                {
                    lock (lockObject)
                    {
                        if (object.Equals(_current, default(T)))
                        {
                            _current = SettingsProvider.Current.GetSettings<T>();
                        }
                    }
                }
                return _current;
            }
        }
        
    }
}
