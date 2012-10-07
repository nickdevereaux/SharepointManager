using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Keutmann.SharePointManager.ViewModel.TreeView;
using SPM2.SharePoint.Model;

namespace Keutmann.SharePointManager.Components.Menu
{
    [Export("SPM2.SharePoint.Model.SPFeatureNode", typeof(ToolStripItem))]
    [ExportMetadata("Order", 200)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SPFeatureDeactivate : ToolStripMenuItem, ISPMenuItem
    {
        public SPTreeNode TreeNode { get; set; }

        public SPFeatureDeactivate()
        {
            Text = "Deactivate";
        }

        public override bool CanSelect
        {
            get
            {
                if (TreeNode.Model is SPFeatureNode)
                {
                    Enabled = ((SPFeatureNode)TreeNode.Model).Activated;
                    return Enabled;
                }
                return false;
            }
        }


        protected override void OnClick(EventArgs e)
        {
            var feature = (SPFeatureNode)TreeNode.Model;
            feature.DeactivateFeature();
            feature.UpdateIconUri();
            TreeNode.ImageIndex = Program.Window.Explorer.AddImage(feature.IconUri);
            TreeNode.SelectedImageIndex = TreeNode.ImageIndex;
        }
    }
}
