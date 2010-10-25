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
	[AttachTo("SPM2.SharePoint.Model.SPSearchServiceNode")]
	[AttachTo("SPM2.SharePoint.Model.SPDatabaseServiceNode")]
	[AttachTo("SPM2.SharePoint.Model.SearchServiceNode")]
	[AttachTo("SPM2.SharePoint.Model.ProfileSynchronizationServiceNode")]
	[AttachTo("SPM2.SharePoint.Model.SPWindowsTokenServiceNode")]
	[AttachTo("SPM2.SharePoint.Model.SPTracingServiceNode")]
	[AttachTo("SPM2.SharePoint.Model.SPUserCodeServiceNode")]
	[AttachTo("SPM2.SharePoint.Model.LauncherServiceNode")]
	[AttachTo("SPM2.SharePoint.Model.LoadBalancerServiceNode")]
	[AttachTo("SPM2.SharePoint.Model.SPAdministrationServiceNode")]
	[AttachTo("SPM2.SharePoint.Model.WebAnalyticsServiceNode")]
	[AttachTo("SPM2.SharePoint.Model.SPTimerServiceNode")]
	public partial class SPProcessIdentityNode
	{
	}
}
