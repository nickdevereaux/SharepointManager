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
	[Title("Receivers")]
	[Icon(Small="BULLET.GIF")]
	[AttachTo("SPM2.SharePoint.Model.SPImportUsageDefinitionNode")]
	[AttachTo("SPM2.SharePoint.Model.SPClickthroughUsageDefinitionNode")]
	[AttachTo("SPM2.SharePoint.Model.SPExportUsageDefinitionNode")]
	[AttachTo("SPM2.SharePoint.Model.SPRequestUsageDefinitionNode")]
	[AttachTo("SPM2.SharePoint.Model.SPFeatureUsageDefinitionNode")]
	[AttachTo("SPM2.SharePoint.Model.SPQueryUsageDefinitionNode")]
	[AttachTo("SPM2.SharePoint.Model.SPSiteInventoryUsageProviderNode")]
	[AttachTo("SPM2.SharePoint.Model.SPTimerJobUsageDefinitionNode")]
	[AttachTo("SPM2.SharePoint.Model.SPRatingUsageDefinitionNode")]
	[AttachTo("SPM2.SharePoint.Model.SPUsageDefinitionNode")]
	public partial class SPUsageReceiverDefinitionCollectionNode
	{
	}
}
