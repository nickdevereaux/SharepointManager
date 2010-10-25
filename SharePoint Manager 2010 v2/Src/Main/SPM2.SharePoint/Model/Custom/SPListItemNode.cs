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
	[Title(PropertyName="DisplayName")]
	[Icon(Small="BULLET.GIF")]
	[AttachTo("SPM2.SharePoint.Model.SPContextNode")]
	[AttachTo("SPM2.SharePoint.Model.SPListItemCollectionNode")]
	[AttachTo("SPM2.SharePoint.Model.SPListItemVersionNode")]
	[AttachTo("SPM2.SharePoint.Model.BaseFieldControlNode")]
	[AttachTo("SPM2.SharePoint.Model.SPMobileBaseFieldControlNode")]
	public partial class SPListItemNode
	{
	}
}
