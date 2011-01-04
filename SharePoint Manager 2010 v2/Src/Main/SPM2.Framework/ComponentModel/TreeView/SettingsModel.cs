using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using SPM2.Framework.Collections;
using System.IO;
using System.Xml.Serialization;
using SPM2.Framework.Configuration;

namespace SPM2.Framework.ComponentModel
{
    [Serializable()]
    public class SettingsModel : TreeViewItemModel
    {

        [Browsable(false)]
        public override string Text
        {
            get
            {
                if (String.IsNullOrEmpty(base.Text))
                {
                    base.Text = this.Descriptor.Title;
                }
                return base.Text;
            }
            set
            {
                base.Text = value;
            }
        }

        public override string ToolTipText
        {
            get
            {
                if (String.IsNullOrEmpty(base.ToolTipText))
                {
                    base.ToolTipText = this.Descriptor.ClassType.Name;
                }
                return base.ToolTipText;
            }
            set
            {
                base.ToolTipText = value;
            }
        }

        private ClassDescriptor _descriptor = null;
        [Browsable(false)]
        [XmlIgnore]
        public ClassDescriptor Descriptor
        {
            get
            {
                if (_descriptor == null)
                {
                    _descriptor = new ClassDescriptor(this.GetType());
                }
                return _descriptor;
            }
            set
            {
                this._descriptor = value;
            }
        }

        //private OrderingCollection<ISettings> _importedNodes = null;
        //[Browsable(false)]
        //[XmlIgnore]
        //public OrderingCollection<ISettings> ImportedNodes
        //{
        //    get
        //    {
        //        if (_importedNodes == null)
        //        {
        //            _importedNodes = CompositionProvider.GetOrderedExports<ISettings>(this.Descriptor.ClassType);
        //        }
        //        return _importedNodes;
        //    }
        //    set
        //    {
        //        _importedNodes = value;
        //    }
        //}


        private string _iconUri = null;
        [Browsable(false)]
        public virtual string IconUri
        {
            get
            {
                if (String.IsNullOrEmpty(_iconUri))
                {
                    _iconUri = GetResourceImagePath("mbllistbullet.gif");
                }
                return _iconUri;
            }
            set
            {
                if (_iconUri != value)
                {
                    _iconUri = value;
                    this.OnPropertyChanged("IconUri");
                }
            }
        }

        [XmlIgnore]
        public ISettings SettingsObject { get; set; }


        public SettingsModel() : base()
        {
            //this.LoadChildren();
        }


        public override void LoadChildren()
        {
            this.Children.Clear();

            foreach (ISettings item in this.SettingsObject.Children.AsSafeEnumable())
	        {
                SettingsModel modelItem = new SettingsModel();
                //modelItem.Parent = this;
                modelItem.SettingsObject = item;
                ClassDescriptor descriptor = new ClassDescriptor(item.GetType());
                modelItem.Text = descriptor.Title;
                modelItem.ToolTipText = descriptor.Description;
                
                this.Children.Add(modelItem);
	        }
        }

        private const string ResourceImagePath = "/SPM2.Framework;component/Resources/Images/";
        private static string GetResourceImagePath(string filename)
        {
            return ResourceImagePath + filename;
        }



    }
}
