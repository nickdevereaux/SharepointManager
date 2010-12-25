using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SPM2.Framework.Configuration;
using System.IO;

namespace SPM2.Framework.ComponentModel
{
    public class SettingsProvider : SettingsModel
    {
        public const string FILENAME = "Settings.xml";

        public SettingsProvider() : base()
        {
        }


        #region Singleton

        private static object lockObject = new object();
        private static SettingsProvider _current = null;

        public static SettingsProvider Current
        {
            get
            {
                if (_current == null)
                {
                    lock (lockObject)
                    {
                        if (_current == null)
                        {
                            _current = CreateProvider();
                        }
                    }
                }
                return _current;
            }
        }

        private static SettingsProvider CreateProvider()
        {
            SettingsProvider result = Load(FILENAME);
            if(result == null)
            {
                result = new SettingsProvider();
                result.IsExpanded = true;
            }
            return result;
        }


        public void Save(string filename = FILENAME)
        {
            string xml = Serializer.ObjectToXML(this);
            File.WriteAllText(filename, xml);
        }

        public static SettingsProvider Load(string filename)
        {
            SettingsProvider result = null;
            if (File.Exists(filename))
            {
                string xml = File.ReadAllText(filename);
                result = Serializer.XmlToObject<SettingsProvider>(xml);
            }
            return result;
        }

        public T GetSettings<T>()
        {
            return FindType<T>(this);
        }

        private T FindType<T>(ITreeViewItemModel model)
        {
            T result = default(T);

            if (model is T)
            {
                result = (T)model;
            }
            else
            {
                foreach (var item in model.Children)
                {
                    result = FindType<T>(item);
                    if (result != null)
                    {
                        break;
                    }
                }
            }

            return result;
        }


        #endregion
    }
}
