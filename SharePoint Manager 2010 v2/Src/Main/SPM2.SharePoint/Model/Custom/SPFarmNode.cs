/* ---------------------------
 * SharePoint Manager 2010 v2
 * Created by Carsten Keutmann
 * ---------------------------
 */

using System;

using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using SPM2.Framework;
using SPM2.Framework.ComponentModel;

namespace SPM2.SharePoint.Model
{
	[Title(PropertyName="DisplayName")]
	[Icon(Small="BULLET.GIF")]
    [ExportToNode(SPModelProvider.AddInID)]
	public partial class SPFarmNode
	{
        public SPFarmNode()
        {
            this.IconUri = this.GetResourceImagePath("actionssettings.gif");
        }
        
	}
}
