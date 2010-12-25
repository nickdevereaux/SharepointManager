using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using SPM2.Framework.Collections;
using System.IO;
using System.Xml.Serialization;

namespace SPM2.Framework.ComponentModel
{
    [Serializable()]
    public class SettingsModel : TreeViewItemModel, ISettingsModel
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

        private OrderingCollection<ISettingsModel> _importedNodes = null;
        [Browsable(false)]
        [XmlIgnore]
        public OrderingCollection<ISettingsModel> ImportedNodes
        {
            get
            {
                if (_importedNodes == null)
                {
                    _importedNodes = CompositionProvider.GetOrderedExports<ISettingsModel>(this.Descriptor.ClassType);
                }
                return _importedNodes;
            }
            set
            {
                _importedNodes = value;
            }
        }


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


        public SettingsModel() : base()
        {
            //this.LoadChildren();
        }


        public override void LoadChildren()
        {
            this.Children.Clear();

            foreach (var item in this.ImportedNodes)
	        {
                this.Children.Add(item.Value);
	        }
        }

        private const string ResourceImagePath = "/SPM2.Framework;component/Resources/Images/";
        private static string GetResourceImagePath(string filename)
        {
            return ResourceImagePath + filename;
        }



    }
}
