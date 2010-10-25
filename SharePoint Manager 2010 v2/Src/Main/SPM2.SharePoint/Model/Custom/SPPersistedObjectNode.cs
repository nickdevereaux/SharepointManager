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
	[AttachTo("SPM2.SharePoint.Model.SPManagedAccountNode")]
	[AttachTo("SPM2.SharePoint.Model.SPServiceApplicationProxyGroupNode")]
	[AttachTo("SPM2.SharePoint.Model.SPServiceApplicationNode")]
	[AttachTo("SPM2.SharePoint.Model.SPApplicationPoolNode")]
	[AttachTo("SPM2.SharePoint.Model.SPDeveloperDashboardSettingsNode")]
	[AttachTo("SPM2.SharePoint.Model.SPUsageIdentityTableNode")]
	[AttachTo("SPM2.SharePoint.Model.SPUsageSettingsNode")]
	[AttachTo("SPM2.SharePoint.Model.SPFeatureDefinitionNode")]
	[AttachTo("SPM2.SharePoint.Model.SPWebServiceNode")]
	[AttachTo("SPM2.SharePoint.Model.SPAlertTemplateNode")]
	[AttachTo("SPM2.SharePoint.Model.SPServiceInstanceNode")]
	[AttachTo("SPM2.SharePoint.Model.SPOutboundMailServiceInstanceNode")]
	[AttachTo("SPM2.SharePoint.Model.SPDocumentConverterNode")]
	[AttachTo("SPM2.SharePoint.Model.SPHttpThrottleSettingsNode")]
	[AttachTo("SPM2.SharePoint.Model.SPWebApplicationNode")]
	[AttachTo("SPM2.SharePoint.Model.SPJobDefinitionNode")]
	[AttachTo("SPM2.SharePoint.Model.SPServiceNode")]
	[AttachTo("SPM2.SharePoint.Model.SPTimerServiceInstanceNode")]
	[AttachTo("SPM2.SharePoint.Model.SPDatabaseNode")]
	[AttachTo("SPM2.SharePoint.Model.SPUsageReceiverDefinitionNode")]
	[AttachTo("SPM2.SharePoint.Model.SPUsageDefinitionNode")]
	[AttachTo("SPM2.SharePoint.Model.SPSolutionValidatorNode")]
	[AttachTo("SPM2.SharePoint.Model.DataConnectionFileNode")]
	[AttachTo("SPM2.SharePoint.Model.SessionStateDatabaseNode")]
	[AttachTo("SPM2.SharePoint.Model.SessionStateServiceApplicationNode")]
	[AttachTo("SPM2.SharePoint.Model.SPServiceApplicationProxyNode")]
	[AttachTo("SPM2.SharePoint.Model.SPServiceProxyNode")]
	[AttachTo("SPM2.SharePoint.Model.SPPersistedFileNode")]
	[AttachTo("SPM2.SharePoint.Model.SPSolutionLanguagePackNode")]
	[AttachTo("SPM2.SharePoint.Model.SPSolutionNode")]
	[AttachTo("SPM2.SharePoint.Model.SPDiagnosticsProviderNode")]
	public partial class SPPersistedObjectNode
	{
	}
}
