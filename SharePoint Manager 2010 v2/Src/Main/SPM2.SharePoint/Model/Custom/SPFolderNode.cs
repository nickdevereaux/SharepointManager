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
	[Icon(Small="BULLET.GIF")]
	//[AttachTo("SPM2.SharePoint.Model.SPFileNode")]
	[AttachTo("SPM2.SharePoint.Model.SPFolderCollectionNode")]
	//[AttachTo("SPM2.SharePoint.Model.SPListItemNode")]
	[AttachTo("SPM2.SharePoint.Model.SPMobileContextNode")]
	[AttachTo("SPM2.SharePoint.Model.SPListNode")]
	[AttachTo("SPM2.SharePoint.Model.SPWebNode")]
	[AttachTo("SPM2.SharePoint.Model.SPHealthRulesListNode")]
	[AttachTo("SPM2.SharePoint.Model.SPHealthReportsListNode")]
	public partial class SPFolderNode
	{
        public override void Setup(object spObject, ClassDescriptor descriptor)
        {
            base.Setup(spObject, descriptor);

            this.Text = this.Folder.Name;
        }
	}

}
