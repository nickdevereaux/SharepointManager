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
	[AttachTo("SPM2.SharePoint.Model.SPServiceCollectionNode")]
	[AttachTo("SPM2.SharePoint.Model.SPFarmNode")]
	[AttachTo("SPM2.SharePoint.Model.SPDiagnosticsBlockingQueryProviderNode")]
	[AttachTo("SPM2.SharePoint.Model.SPDiagnosticsSqlDmvProviderNode")]
	[AttachTo("SPM2.SharePoint.Model.SPDiagnosticsULSProviderNode")]
	[AttachTo("SPM2.SharePoint.Model.SPDatabaseServerDiagnosticsPerformanceCounterProviderNode")]
	[AttachTo("SPM2.SharePoint.Model.SPWebFrontEndDiagnosticsPerformanceCounterProviderNode")]
	[AttachTo("SPM2.SharePoint.Model.SPDiagnosticsEventLogProviderNode")]
	[AttachTo("SPM2.SharePoint.Model.SPDiagnosticsSqlMemoryProviderNode")]
	public partial class SPTimerServiceNode
	{
	}
}
