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
	[Title("SPSite")]
	[ExportToNode("SPM2.SharePoint.Model.SPSiteCollectionNode")]
	[ExportToNode("SPM2.SharePoint.Model.SPSiteSubscriptionSiteCollectionNode")]
	public partial class SPSiteNode
	{
        public SPSiteNode()
        {
            this.IconUri = SharePointContext.GetImagePath("ITS16.GIF");
        }
	}

}
