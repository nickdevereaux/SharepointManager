/* ---------------------------
 * SharePoint Manager 2010 v2
 * Created by Carsten Keutmann
 * ---------------------------
 */

using System;
using System.Linq;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using SPM2.Framework;
using System.Collections.Generic;

namespace SPM2.SharePoint.Model
{
    [Title(PropertyName = "DisplayName")]
    [Icon(Small = "SPM2.SharePoint.Properties.Resources.Internet", Source = IconSource.Assembly)]
    [View(1)]
    [ExportToNode("SPM2.SharePoint.Model.SPJobDefinitionNode")]
    [ExportToNode("SPM2.SharePoint.Model.SPWebApplicationCollectionNode")]
    [ExportToNode("SPM2.SharePoint.Model.SPDiagnosticsBlockingQueryProviderNode")]
    [ExportToNode("SPM2.SharePoint.Model.SPDiagnosticsSqlDmvProviderNode")]
    [ExportToNode("SPM2.SharePoint.Model.SPDiagnosticsULSProviderNode")]
    [ExportToNode("SPM2.SharePoint.Model.SPDatabaseServerDiagnosticsPerformanceCounterProviderNode")]
    [ExportToNode("SPM2.SharePoint.Model.SPWebFrontEndDiagnosticsPerformanceCounterProviderNode")]
    [ExportToNode("SPM2.SharePoint.Model.SPDiagnosticsEventLogProviderNode")]
    [ExportToNode("SPM2.SharePoint.Model.SPDiagnosticsSqlMemoryProviderNode")]
    [ExportToNode("SPM2.SharePoint.Model.SPDiagnosticsProviderNode")]
    public partial class SPWebApplicationNode
    {
    }
}
