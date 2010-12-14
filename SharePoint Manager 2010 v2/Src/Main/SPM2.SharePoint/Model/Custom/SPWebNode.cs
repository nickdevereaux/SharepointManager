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
	[Title(PropertyName="Title")]
	[ExportToNode("SPM2.SharePoint.Model.SPGroupNode")]
	[ExportToNode("SPM2.SharePoint.Model.SPWebCollectionNode")]
	[ExportToNode("SPM2.SharePoint.Model.SPUserNode")]
	[ExportToNode("SPM2.SharePoint.Model.SPSiteNode")]
	[ExportToNode("SPM2.SharePoint.Model.SPHealthRulesListNode")]
	[ExportToNode("SPM2.SharePoint.Model.SPHealthReportsListNode")]
	public partial class SPWebNode
	{

        public override void Setup(object spObject)
        {
            base.Setup(spObject);

            this.Url = this.Web.Url;
            this.IconUri = SharePointContext.GetImagePath("CAT.GIF");
        }
	}
}
