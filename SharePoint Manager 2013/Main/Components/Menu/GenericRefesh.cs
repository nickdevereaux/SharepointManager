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
    [Export(typeof(ToolStripItem))]
    [ExportMetadata("Order", 30000)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class GenericRefesh : ToolStripMenuItem, ISPMenuItem
    {
        public SPTreeNode TreeNode { get; set; }

        public GenericRefesh()
        {
            Text = "Refesh";
            Size = new System.Drawing.Size(125, 22);
            Image = global::Keutmann.SharePointManager.Properties.Resources.refresh3;
        }

        protected override void OnClick(EventArgs e)
        {
            TreeNode.Refresh();
        }
    }
}
