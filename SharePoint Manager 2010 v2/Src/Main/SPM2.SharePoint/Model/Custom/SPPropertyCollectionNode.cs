/* ---------------------------
 * SharePoint Manager 2010 v2
 * Created by Carsten Keutmann
 * ---------------------------
 */

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using SPM2.Framework;

namespace SPM2.SharePoint.Model
{
	[Title("AllProperties")]
    [Icon(Small = "EXITGRID.GIF")]
    [ExportToNode("SPM2.SharePoint.Model.SPFarmNode")]
    [ExportToNode("SPM2.SharePoint.Model.SPWebNode")]
    [ExportToNode("SPM2.SharePoint.Model.SPFolderNode")]
    [ExportToNode("SPM2.SharePoint.Model.SPFileNode")]
    [ExportToNode("SPM2.SharePoint.Model.SPFileVersionNode")]
    [ExportToNode(typeof(SPListItemNode))]
    [ExportToNode(typeof(SPJobDefinitionNode))]
    [ExportToNode(typeof(SPServerNode))]
    [ExportToNode(typeof(SPServiceNode))]
    [ExportToNode(typeof(SPWebServiceNode))]
    [ExportToNode(typeof(SPSolutionNode))]
    [AdapterItemType("System.Collections.Hashtable")]
	public partial class SPPropertyCollectionNode : SPNodeCollection
	{

        public Hashtable AllProperties
        {
            get
            {
                return (Hashtable)this.SPObject;
            }
        }


        public override void LoadChildren()
        {
            List<ITreeViewItemModel> list = new List<ITreeViewItemModel>();

            foreach (DictionaryEntry entry in this.AllProperties)
            {
                SPPropertyNode node = new SPPropertyNode();
                node.SPObject = entry;
                node.Setup(this.SPObject);
                list.Add(node);
            }

            this.Children.AddRange(list.OrderBy(p => p.Text));
        }

	}
}
