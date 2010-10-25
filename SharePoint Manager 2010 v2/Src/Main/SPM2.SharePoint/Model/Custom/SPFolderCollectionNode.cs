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
	[Title("Folders")]
	[AttachTo("SPM2.SharePoint.Model.SPFolderNode")]
	[AttachTo("SPM2.SharePoint.Model.SPWebNode")]
	public partial class SPFolderCollectionNode
	{
        public override void Setup(object spObject, ClassDescriptor descriptor)
        {
            base.Setup(spObject, descriptor);
        }
	}
}
