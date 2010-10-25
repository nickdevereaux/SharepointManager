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
	[Title("ChangeSchedule")]
	[Icon(Small="BULLET.GIF")]
	[AttachTo("SPM2.SharePoint.Model.SPManagedAccountNode")]
	[AttachTo("SPM2.SharePoint.Model.SPJobDefinitionNode")]
	[AttachTo("SPM2.SharePoint.Model.SPDiagnosticsProviderNode")]
	public partial class SPScheduleNode
	{
	}
}