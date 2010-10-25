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
	[Title("ProcessAccount")]
	[Icon(Small="BULLET.GIF")]
	[AttachTo("SPM2.SharePoint.Model.SPProcessIdentityNode")]
	[AttachTo("SPM2.SharePoint.Model.SPApplicationPoolNode")]
	[AttachTo("SPM2.SharePoint.Model.SPFarmNode")]
	public partial class SPProcessAccountNode
	{
	}
}
