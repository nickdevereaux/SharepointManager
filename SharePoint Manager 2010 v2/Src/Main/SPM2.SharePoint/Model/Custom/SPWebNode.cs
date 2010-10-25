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
	[AttachTo("SPM2.SharePoint.Model.SPGroupNode")]
	[AttachTo("SPM2.SharePoint.Model.SPWebCollectionNode")]
	[AttachTo("SPM2.SharePoint.Model.SPUserNode")]
	[AttachTo("SPM2.SharePoint.Model.SPSiteNode")]
	[AttachTo("SPM2.SharePoint.Model.SPHealthRulesListNode")]
	[AttachTo("SPM2.SharePoint.Model.SPHealthReportsListNode")]
	public partial class SPWebNode
	{

        public override void Setup(object spObject, ClassDescriptor descriptor)
        {
            base.Setup(spObject, descriptor);

            this.Url = this.Web.Url;
            this.IconUri = SharePointContext.GetImagePath("CAT.GIF");
        }
	}
}
