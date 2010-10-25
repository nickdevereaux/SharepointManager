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
	[Title("RunningJobs")]
	[Icon(Small="BULLET.GIF")]
	[AttachTo("SPM2.SharePoint.Model.SPWebServiceNode")]
	[AttachTo("SPM2.SharePoint.Model.SPWebApplicationNode")]
	[AttachTo("SPM2.SharePoint.Model.SPServiceNode")]
	[AttachTo("SPM2.SharePoint.Model.SPAdministrationWebApplicationNode")]
	[AttachTo("SPM2.SharePoint.Model.SPDatabaseServiceNode")]
	[AttachTo("SPM2.SharePoint.Model.SPSearchServiceNode")]
	[AttachTo("SPM2.SharePoint.Model.PolicyConfigServiceNode")]
	[AttachTo("SPM2.SharePoint.Model.SearchServiceNode")]
	[AttachTo("SPM2.SharePoint.Model.MetadataWebServiceNode")]
	[AttachTo("SPM2.SharePoint.Model.ProfileSynchronizationServiceNode")]
	[AttachTo("SPM2.SharePoint.Model.SPUsageServiceNode")]
	[AttachTo("SPM2.SharePoint.Model.BdcServiceNode")]
	[AttachTo("SPM2.SharePoint.Model.SearchAdminWebServiceNode")]
	[AttachTo("SPM2.SharePoint.Model.SecureStoreServiceNode")]
	[AttachTo("SPM2.SharePoint.Model.SPWindowsTokenServiceNode")]
	[AttachTo("SPM2.SharePoint.Model.SPTracingServiceNode")]
	[AttachTo("SPM2.SharePoint.Model.SPWorkflowTimerServiceNode")]
	[AttachTo("SPM2.SharePoint.Model.BIMonitoringServiceNode")]
	[AttachTo("SPM2.SharePoint.Model.ApplicationRegistryServiceNode")]
	[AttachTo("SPM2.SharePoint.Model.OfficeServerServiceNode")]
	[AttachTo("SPM2.SharePoint.Model.SPUserCodeServiceNode")]
	[AttachTo("SPM2.SharePoint.Model.PortalServiceNode")]
	[AttachTo("SPM2.SharePoint.Model.VisioGraphicsServiceNode")]
	[AttachTo("SPM2.SharePoint.Model.LauncherServiceNode")]
	[AttachTo("SPM2.SharePoint.Model.LoadBalancerServiceNode")]
	[AttachTo("SPM2.SharePoint.Model.SearchQueryAndSiteSettingsServiceNode")]
	[AttachTo("SPM2.SharePoint.Model.WebAnalyticsWebServiceNode")]
	[AttachTo("SPM2.SharePoint.Model.SPSecurityTokenServiceNode")]
	[AttachTo("SPM2.SharePoint.Model.ExcelServerWebServiceNode")]
	[AttachTo("SPM2.SharePoint.Model.SPIncomingEmailServiceNode")]
	[AttachTo("SPM2.SharePoint.Model.UserProfileServiceNode")]
	[AttachTo("SPM2.SharePoint.Model.AccessServerWebServiceNode")]
	[AttachTo("SPM2.SharePoint.Model.SPSubscriptionSettingsServiceNode")]
	[AttachTo("SPM2.SharePoint.Model.SPAdministrationServiceNode")]
	[AttachTo("SPM2.SharePoint.Model.NotesWebServiceNode")]
	[AttachTo("SPM2.SharePoint.Model.WebAnalyticsServiceNode")]
	[AttachTo("SPM2.SharePoint.Model.WordServiceNode")]
	[AttachTo("SPM2.SharePoint.Model.SPTopologyWebServiceNode")]
	[AttachTo("SPM2.SharePoint.Model.FormsServiceNode")]
	[AttachTo("SPM2.SharePoint.Model.StateServiceNode")]
	[AttachTo("SPM2.SharePoint.Model.DiagnosticsServiceNode")]
	[AttachTo("SPM2.SharePoint.Model.SPTimerServiceNode")]
	[AttachTo("SPM2.SharePoint.Model.SPOutboundMailServiceNode")]
	[AttachTo("SPM2.SharePoint.Model.SessionStateServiceNode")]
	[AttachTo("SPM2.SharePoint.Model.SPDiagnosticsServiceNode")]
	public partial class SPRunningJobCollectionNode
	{
	}
}
