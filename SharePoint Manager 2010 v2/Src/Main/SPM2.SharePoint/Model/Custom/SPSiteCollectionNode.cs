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
	[Title("Sites")]
	[AttachTo("SPM2.SharePoint.Model.SPWebApplicationNode")]
	[AttachTo("SPM2.SharePoint.Model.SPAdministrationWebApplicationNode")]
	[AttachTo("SPM2.SharePoint.Model.SPContentDatabaseNode")]
	public partial class SPSiteCollectionNode
	{
        public SPSiteCollectionNode()
        {
            this.IconUri = SharePointContext.GetImagePath("coll_site.gif");
        }
	}
}
