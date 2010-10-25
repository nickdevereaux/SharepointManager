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
	[Title("ApplicationProxies")]
	[Icon(Small="BULLET.GIF")]
	[AttachTo("SPM2.SharePoint.Model.BdcServiceProxyNode")]
	[AttachTo("SPM2.SharePoint.Model.WordServiceProxyNode")]
	[AttachTo("SPM2.SharePoint.Model.StateServiceProxyNode")]
	[AttachTo("SPM2.SharePoint.Model.AccessServerWebServiceProxyNode")]
	[AttachTo("SPM2.SharePoint.Model.ApplicationRegistryServiceProxyNode")]
	[AttachTo("SPM2.SharePoint.Model.MetadataWebServiceProxyNode")]
	[AttachTo("SPM2.SharePoint.Model.SPSubscriptionSettingsServiceProxyNode")]
	[AttachTo("SPM2.SharePoint.Model.BIMonitoringServiceProxyNode")]
	[AttachTo("SPM2.SharePoint.Model.SecureStoreServiceProxyNode")]
	[AttachTo("SPM2.SharePoint.Model.SearchQueryAndSiteSettingsServiceProxyNode")]
	[AttachTo("SPM2.SharePoint.Model.WebAnalyticsServiceProxyNode")]
	[AttachTo("SPM2.SharePoint.Model.SPTopologyWebServiceProxyNode")]
	[AttachTo("SPM2.SharePoint.Model.NotesWebServiceProxyNode")]
	[AttachTo("SPM2.SharePoint.Model.UserProfileServiceProxyNode")]
	[AttachTo("SPM2.SharePoint.Model.VisioGraphicsServiceProxyNode")]
	[AttachTo("SPM2.SharePoint.Model.SearchServiceProxyNode")]
	[AttachTo("SPM2.SharePoint.Model.ExcelServerWebServiceProxyNode")]
	[AttachTo("SPM2.SharePoint.Model.SPUsageServiceProxyNode")]
	[AttachTo("SPM2.SharePoint.Model.SPServiceProxyNode")]
	public partial class SPServiceApplicationProxyCollectionNode
	{
	}
}
