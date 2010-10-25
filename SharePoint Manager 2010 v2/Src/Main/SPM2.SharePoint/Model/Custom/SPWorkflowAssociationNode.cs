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
	[Title("SPWorkflowAssociation")]
	[Icon(Small="BULLET.GIF")]
	[AttachTo("SPM2.SharePoint.Model.SPWorkflowAssociationCollectionNode")]
	[AttachTo("SPM2.SharePoint.Model.SPListWorkflowAssociationCollectionNode")]
	public partial class SPWorkflowAssociationNode
	{
	}
}