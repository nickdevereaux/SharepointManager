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
	[Title("Services")]
	[AttachTo("SPM2.SharePoint.Model.SPFarmNode")]
	public partial class SPServiceCollectionNode
	{
        public SPServiceCollectionNode()
        {
            this.IconUri = SharePointContext.GetImagePath("SERVICES.GIF");
        }
	}
}
