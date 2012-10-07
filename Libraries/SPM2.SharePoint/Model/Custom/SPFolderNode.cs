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
using SPM2.SharePoint.Rules;

namespace SPM2.SharePoint.Model
{
    [Icon(Small = "Folder.gif")]
    [View(50)]
	[ExportToNode("SPM2.SharePoint.Model.SPFolderCollectionNode")]
	[ExportToNode("SPM2.SharePoint.Model.SPMobileContextNode")]
	[ExportToNode("SPM2.SharePoint.Model.SPListNode")]
    [ExportToNode(typeof(SPDocumentLibraryNode))]
    [ExportToNode("SPM2.SharePoint.Model.SPWebNode")]
	[ExportToNode("SPM2.SharePoint.Model.SPHealthRulesListNode")]
	[ExportToNode("SPM2.SharePoint.Model.SPHealthReportsListNode")]
    [ExportToNode("SPM2.SharePoint.Model.SPContentTypeNode")]
    //[ExportToNode("SPM2.SharePoint.Model.SPFileNode")]
    //[ExportToNode("SPM2.SharePoint.Model.SPListItemNode")]
    [RecursiveRule(IsRecursiveVisible = true)]
    public partial class SPFolderNode : IRecursiveRule, IViewRule
	{
        public bool IsRecursiveVisible()
        {
            if (this.Parent.SPObjectType.IsOfType(typeof(SPFolderCollection)))
                return true;

            return false;
        }

        public bool IsVisible()
        {
            return !(ParentPropertyDescriptor.Name == "ParentFolder");
        }
    }

}
