/* ---------------------------
 * SharePoint Manager 2010 v2
 * Created by Carsten Keutmann
 * ---------------------------
 */

using System;

using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using SPM2.Framework;

namespace SPM2.SharePoint.Model
{
	[ExportToNode("SPM2.SharePoint.Model.SPWorkflowAssociationNode")]
	[ExportToNode("SPM2.SharePoint.Model.SPContentTypeCollectionNode")]
	[ExportToNode("SPM2.SharePoint.Model.SPListItemNode")]
	public partial class SPContentTypeNode
	{
        public SPContentTypeNode()
        {
            this.IconUri = SharePointContext.GetImagePath("MARR.GIF");
        }

        public override void Setup(ISPNode parent)
        {
            base.Setup(parent);

            this.Text = this.ContentType.Name;
            if (this.ContentType.Hidden)
            {
                this.Text = this.Text + " (Hidden)";
                this.State = "Gray";
            }
        }
        

        public override void LoadChildren()
        {
            base.LoadChildren();

            SPContentTypeUsageCollectionNode node = new SPContentTypeUsageCollectionNode();
            node.Setup(this);
            this.Children.Add(node);
        }
    }
}
