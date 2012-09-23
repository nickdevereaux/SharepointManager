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
    [Icon(Small = "ICSMRTPG.GIF")]
    [View(50)]
	[ExportToNode("SPM2.SharePoint.Model.SPListNode")]
	[ExportToNode("SPM2.SharePoint.Model.SPWebNode")]
	[ExportToNode("SPM2.SharePoint.Model.SPHealthRulesListNode")]
	[ExportToNode("SPM2.SharePoint.Model.SPHealthReportsListNode")]
	public partial class SPFieldCollectionNode
	{
        public override bool Accept()
        {
            if (NodeProvider.ViewLevel >= 100)
                return true;

            if (Parent.SPObject is SPWeb)
            {
                var web = (SPWeb)Parent.SPObject;
                if (web.IsRootWeb && ParentPropertyDescriptor.Name != "Fields")
                    return false;

                if (!web.IsRootWeb && ParentPropertyDescriptor.Name == "Fields")
                    return false;
            }
            return true;
        }
	}
}
