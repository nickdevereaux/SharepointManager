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
	[Title(PropertyName="DisplayName")]
    //[ExportToNode("SPM2.SharePoint.Model.SPWebApplicationNode")]
    //[ExportToNode("SPM2.SharePoint.Model.SPAdministrationWebApplicationNode")]
    //[ExportToNode("SPM2.SharePoint.Model.SPWebServiceInstanceNode")]
	[ExportToNode("SPM2.SharePoint.Model.SPServiceCollectionNode")]
	public partial class SPWebServiceNode
	{
        public SPWebServiceNode()
        {
            this.IconUri = SharePointContext.GetImagePath("SETTINGS.GIF");
        }
	}
}
