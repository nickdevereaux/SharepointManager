/* ---------------------------
 * SharePoint Manager 2010 v2
 * Created by Carsten Keutmann
 * ---------------------------
 */

using System;

using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using SPM2.Framework;
using Microsoft.SharePoint.Utilities;

namespace SPM2.SharePoint.Model
{
	[Title(PropertyName="Title")]
	[Icon(Small="BULLET.GIF")]
	[ExportToNode("SPM2.SharePoint.Model.SPListItemNode")]
	[ExportToNode("SPM2.SharePoint.Model.SPFileCollectionNode")]
	[ExportToNode("SPM2.SharePoint.Model.SPContextNode")]
	public partial class SPFileNode
	{

        public override void Setup(object spObject)
        {
            base.Setup(spObject);

            this.Url = SPUrlUtility.CombineUrl(this.File.ParentFolder.ParentWeb.Url, this.File.Url);
        }
	}
}
