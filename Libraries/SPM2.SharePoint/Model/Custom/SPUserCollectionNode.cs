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
	[Title("Users")]
    [Icon(Small = "MNGATT.GIF")]
    [View(50)]
	[ExportToNode("SPM2.SharePoint.Model.SPGroupNode")]
	[ExportToNode("SPM2.SharePoint.Model.SPWebNode")]
	public partial class SPUserCollectionNode
	{
        public override bool Accept()
        {
            if (ParentPropertyDescriptor.Name == "Users")
                return true;

            return false;
        }
	}
}
