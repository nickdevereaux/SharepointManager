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
        private SPFarmNode farmNode;

        public SPFarm Farm
        {
            get { return _farm; }
            set { _farm = value; }
        }

        public SPModelProvider()
            : this(SharePointContext.Instance.Farm)
        {
            
        }

        public SPModelProvider(SPFarm farm)
        {
            this.Farm = farm;
            LoadChildren();
            //ExpandToDefault();
        }

        public override void LoadChildren()
        {
            this.Children.Clear();

            OrderingCollection<SPNode> orderedItems = CompositionProvider.GetOrderedExports<SPNode>(this.GetType());

            // now add the items to the menu child items collection in a ordered list
            foreach (var item in orderedItems)
            {
                SPNode node = item.Value;
                node.Setup(this.Farm);
                this.Children.Add(node);
            }
        }

        public void ExpandToDefault()
        {
            SPNode node = (SPNode)this.Children[0];
            node.IsExpanded = true;
            ExpandNode(node.NodesToExpand());
                        
        }

        private void ExpandNode(IEnumerable<SPNode> collection)
        {
            if (collection == null)
            {
                return;
            }

            foreach (var node in collection)
            {
                node.LoadChildren();
                node.IsExpanded = true;
                node.IsSelected = node.IsDefaultSelected();

                ExpandNode(node.NodesToExpand());
            }
        }
    }
}
