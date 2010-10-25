/* ---------------------------
 * SharePoint Manager 2010 v2
 * Created by Carsten Keutmann
 * ---------------------------
 */

using System;

using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using SPM2.Framework;
using System.IO;

namespace SPM2.SharePoint.Model
{
	[Title(PropertyName="DisplayName")]
	[AttachTo("SPM2.SharePoint.Model.SPServerCollectionNode")]
	[AttachTo("SPM2.SharePoint.Model.SessionStateDatabaseNode")]
	[AttachTo("SPM2.SharePoint.Model.SPDiagnosticsBlockingQueryProviderNode")]
	[AttachTo("SPM2.SharePoint.Model.SPDiagnosticsSqlDmvProviderNode")]
	[AttachTo("SPM2.SharePoint.Model.SPDiagnosticsULSProviderNode")]
	[AttachTo("SPM2.SharePoint.Model.SPDatabaseServerDiagnosticsPerformanceCounterProviderNode")]
	[AttachTo("SPM2.SharePoint.Model.SPWebFrontEndDiagnosticsPerformanceCounterProviderNode")]
	[AttachTo("SPM2.SharePoint.Model.SPDiagnosticsEventLogProviderNode")]
	[AttachTo("SPM2.SharePoint.Model.SPDiagnosticsSqlMemoryProviderNode")]
	[AttachTo("SPM2.SharePoint.Model.SPDiagnosticsProviderNode")]
	public partial class SPServerNode
	{
        public SPServerNode()
        {
            this.IconUri = this.GetResourceImagePath("server.gif");
        }
	}
}
