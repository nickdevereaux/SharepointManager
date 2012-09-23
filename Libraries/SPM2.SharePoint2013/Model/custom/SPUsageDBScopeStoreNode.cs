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
	[Title("DataStore")]
	[Icon(Small="BULLET.GIF")]
	[AttachTo("SPM2.SharePoint.Model.SPDiagnosticsMetricsProviderNode")]
	[AttachTo("SPM2.SharePoint.Model.SPDiagnosticsXEventQueryDataProviderNode")]
	public partial class SPUsageDBScopeStoreNode
	{
	}
}