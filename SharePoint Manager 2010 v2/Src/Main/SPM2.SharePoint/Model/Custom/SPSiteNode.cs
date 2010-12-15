/* ---------------------------
 * SharePoint Manager 2010 v2
 * Created by Carsten Keutmann
 * ---------------------------
 */

using System;
using System.Linq;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using SPM2.Framework;
using System.Collections.Generic;

namespace SPM2.SharePoint.Model
{
	[Title("SPSite")]
    [Icon(Small = "ITS16.GIF")]
	[ExportToNode("SPM2.SharePoint.Model.SPSiteCollectionNode")]
	[ExportToNode("SPM2.SharePoint.Model.SPSiteSubscriptionSiteCollectionNode")]
	public partial class SPSiteNode
	{
        public override IEnumerable<SPNode> NodesToExpand()
        {
            return this.Children.OfType<SPWebNode>().Take(1).Cast<SPNode>();
        }

	}

}
