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
	[Title("WorkflowAssociations")]
	[Icon(Small="BULLET.GIF")]
	[AttachTo("SPM2.SharePoint.Model.SPDocumentLibraryNode")]
	[AttachTo("SPM2.SharePoint.Model.SPListNode")]
	[AttachTo("SPM2.SharePoint.Model.SPWebNode")]
	public partial class SPWorkflowAssociationCollectionNode
	{
	}
}
