using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace SPM2.SharePoint.Model.Menu
{
    [Export("SPFeatureActionItems", typeof(ActionItem))]
    public class SPFeatureDeactivateActionItem : ActionItem
    {
        public SPFeatureDeactivateActionItem()
        {
            Text = "Deactivate";
            Click = DoClick;   
        }

        public override bool Enabled
        {
            get
            {
                if (Node != null)
                {
                    base.Enabled = ((SPFeatureNode)Node).Activated;
                }
                return base.Enabled;
            }
            set
            {
                base.Enabled = value;
            }
        }

        public void DoClick(object sender, EventArgs e)
        {
            var feature = (SPFeatureNode)Node;
            feature.DeactivateFeature();
        }
    }
}
