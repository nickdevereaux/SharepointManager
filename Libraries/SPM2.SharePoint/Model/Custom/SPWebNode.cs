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
	[Title(PropertyName="Title")]
    [Icon(Small = "CAT.GIF")]
    [View(1)]
	[ExportToNode("SPM2.SharePoint.Model.SPWebCollectionNode")]
	[ExportToNode("SPM2.SharePoint.Model.SPSiteNode")]
	public partial class SPWebNode
	{

        public override void Setup(ISPNode parent)
        {
            base.Setup(parent);
            this.Text = this.Web.Title;
            this.Url = this.Web.Url;
        }

        public override bool IsDefaultSelected()
        {
            return true;
        }
	}
}
