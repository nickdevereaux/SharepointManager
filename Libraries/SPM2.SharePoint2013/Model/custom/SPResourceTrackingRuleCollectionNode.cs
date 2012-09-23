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
	[Title("Rules")]
	[Icon(Small="BULLET.GIF")]
	[ExportToNode("SPM2.SharePoint.Model.SPResourceTrackingSettingsNode")]
	[ExportToNode("SPM2.SharePoint.Model.SPUserResourceTrackingSettingsNode")]
	[ExportToNode("SPM2.SharePoint.Model.SPAppResourceTrackingSettingsNode")]
	public partial class SPResourceTrackingRuleCollectionNode
	{
	}
}
