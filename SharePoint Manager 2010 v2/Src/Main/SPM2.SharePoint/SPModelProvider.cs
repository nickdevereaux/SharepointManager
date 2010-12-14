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
using System.ComponentModel.Composition;

namespace SPM2.SharePoint
{
    //[Title("SharePoint ExplorerNode")]
    [Export()]
    [PartCreationPolicy(CreationPolicy.Shared)]
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
            OrderingCollection<SPNode> orderedItems = CompositionProvider.GetOrderedExports<SPNode>(this.GetType());

            // now add the items to the menu child items collection in a ordered list
            foreach (var item in orderedItems)
            {
                SPNode node = item.Value;
                node.Setup(this.Farm);
                this.Children.Add(item.Value);
            }

            //AddInProvider.Current.CreateAttachments<SPNode>(AddInID,
            //    delegate(ClassDescriptor descriptor, SPNode node)
            //    {
            //        node.Setup(this.Farm, descriptor);
            //        this.Children.Add(node);
            //        return true;
            //    });
        }
    }
}
