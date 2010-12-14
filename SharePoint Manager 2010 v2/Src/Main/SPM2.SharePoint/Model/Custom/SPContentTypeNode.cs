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
	[Title("ParentContentType")]
	[ExportToNode("SPM2.SharePoint.Model.SPWorkflowAssociationNode")]
	[ExportToNode("SPM2.SharePoint.Model.SPContentTypeCollectionNode")]
	[ExportToNode("SPM2.SharePoint.Model.SPListItemNode")]
	public partial class SPContentTypeNode
	{
        public SPContentTypeNode()
        {
            this.IconUri = SharePointContext.GetImagePath("MARR.GIF");
        }
    }
}
