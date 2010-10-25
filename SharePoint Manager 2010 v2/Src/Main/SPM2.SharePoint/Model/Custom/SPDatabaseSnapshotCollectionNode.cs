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
	[Title("Snapshots")]
	[Icon(Small="BULLET.GIF")]
	[AttachTo("SPM2.SharePoint.Model.StateDatabaseNode")]
	[AttachTo("SPM2.SharePoint.Model.SecureStoreServiceDatabaseNode")]
	[AttachTo("SPM2.SharePoint.Model.QueueDatabaseNode")]
	[AttachTo("SPM2.SharePoint.Model.SocialDatabaseNode")]
	[AttachTo("SPM2.SharePoint.Model.BdcServiceDatabaseNode")]
	[AttachTo("SPM2.SharePoint.Model.WebAnalyticsStagerDatabaseNode")]
	[AttachTo("SPM2.SharePoint.Model.SPConfigurationDatabaseNode")]
	[AttachTo("SPM2.SharePoint.Model.BIMonitoringServiceDatabaseNode")]
	[AttachTo("SPM2.SharePoint.Model.SearchGathererDatabaseNode")]
	[AttachTo("SPM2.SharePoint.Model.SearchPropertyStoreDatabaseNode")]
	[AttachTo("SPM2.SharePoint.Model.ApplicationRegistryServiceDatabaseNode")]
	[AttachTo("SPM2.SharePoint.Model.SPContentDatabaseNode")]
	[AttachTo("SPM2.SharePoint.Model.SearchAdminDatabaseNode")]
	[AttachTo("SPM2.SharePoint.Model.MetadataWebServiceDatabaseNode")]
	[AttachTo("SPM2.SharePoint.Model.ProfileDatabaseNode")]
	[AttachTo("SPM2.SharePoint.Model.SPUsageDatabaseNode")]
	[AttachTo("SPM2.SharePoint.Model.SynchronizationDatabaseNode")]
	[AttachTo("SPM2.SharePoint.Model.WebAnalyticsWarehouseDatabaseNode")]
	[AttachTo("SPM2.SharePoint.Model.SPDatabaseNode")]
	[AttachTo("SPM2.SharePoint.Model.SPSearchDatabaseNode")]
	[AttachTo("SPM2.SharePoint.Model.SessionStateDatabaseNode")]
	public partial class SPDatabaseSnapshotCollectionNode
	{
	}
}
