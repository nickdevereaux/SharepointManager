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
	[Title("InitialSweepSchedule")]
	[Icon(Small="BULLET.GIF")]
	[AttachTo("SPM2.SharePoint.Model.SPTimerServiceNode")]
	[AttachTo("SPM2.SharePoint.Model.SPDiagnosticsSqlDmvProviderNode")]
	[AttachTo("SPM2.SharePoint.Model.SPDiagnosticsULSProviderNode")]
	[AttachTo("SPM2.SharePoint.Model.SPDatabaseServerDiagnosticsPerformanceCounterProviderNode")]
	[AttachTo("SPM2.SharePoint.Model.SPWebFrontEndDiagnosticsPerformanceCounterProviderNode")]
	[AttachTo("SPM2.SharePoint.Model.SPDiagnosticsEventLogProviderNode")]
	public partial class SPMinuteScheduleNode
	{
	}
}
