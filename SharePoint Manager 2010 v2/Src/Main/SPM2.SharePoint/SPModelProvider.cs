using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using SPM2.Framework;
using SPM2.Framework.Collections;
using SPM2.Framework.Reflection;
using Microsoft.SharePoint.Administration;
using SPM2.SharePoint.Model;

namespace SPM2.SharePoint
{
    [Title("SharePoint ExplorerNode")]
    [AddInID(SPModelProvider.AddInID)]
    public class SPModelProvider : TreeViewModel
    {
        public const string AddInID = "SPM2.SharePoint.SPModelProvider";

        private SPFarm _farm = null;

        public SPFarm Farm
        {
            get { return _farm; }
            set { _farm = value; }
        }

        public SPModelProvider() : this(SPFarm.Local)
        {
        }

        public SPModelProvider(SPFarm farm)
        {
            this.Farm = farm;
            LoadChildren();
        }

        protected override void LoadChildren()
        {
            AddInProvider.Current.CreateAttachments<SPNode>(AddInID,
                delegate(ClassDescriptor descriptor, SPNode node)
                {
                    node.Setup(this.Farm, descriptor);
                    this.Children.Add(node);
                    return true;
                });
        }
    }
}
