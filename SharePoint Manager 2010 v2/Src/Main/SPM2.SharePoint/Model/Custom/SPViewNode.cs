/* ---------------------------
 * SharePoint Manager 2010 v2
 * Created by Carsten Keutmann
 * ---------------------------
 */

using System;

using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using SPM2.Framework;
using Microsoft.SharePoint.Utilities;

namespace SPM2.SharePoint.Model
{
	[Title(PropertyName="Title")]
	[Icon(Small="BULLET.GIF")]
	[AttachTo("SPM2.SharePoint.Model.SPViewCollectionNode")]
	[AttachTo("SPM2.SharePoint.Model.SPDocumentLibraryNode")]
	[AttachTo("SPM2.SharePoint.Model.SPViewContextNode")]
	[AttachTo("SPM2.SharePoint.Model.SPMobileContextNode")]
	[AttachTo("SPM2.SharePoint.Model.SPMobileBaseFieldControlNode")]
	[AttachTo("SPM2.SharePoint.Model.SPListNode")]
	[AttachTo("SPM2.SharePoint.Model.SPHealthRulesListNode")]
	[AttachTo("SPM2.SharePoint.Model.SPHealthReportsListNode")]
	public partial class SPViewNode
	{
        public override void Setup(object spObject, ClassDescriptor descriptor)
        {
            base.Setup(spObject, descriptor);

            this.Url = SPUrlUtility.CombineUrl(View.ParentList.ParentWeb.Url, View.Url);
        }
	}
}
