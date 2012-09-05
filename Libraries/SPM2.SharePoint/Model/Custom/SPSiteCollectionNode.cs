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
using System.Collections;

namespace SPM2.SharePoint.Model
{
	[Title("Sites")]
    [Icon(Small = "coll_site.gif")]
	[ExportToNode("SPM2.SharePoint.Model.SPWebApplicationNode")]
	[ExportToNode("SPM2.SharePoint.Model.SPAdministrationWebApplicationNode")]
	[ExportToNode("SPM2.SharePoint.Model.SPContentDatabaseNode")]
	public partial class SPSiteCollectionNode
	{

        public override void LoadChildren()
        {
            //Children.AddRange(NodeProvider.LoadCollectionChildren(this, SPExplorerSettings.Current.BatchNodeLoad));
            Children.AddRange(NodeProvider.LoadCollectionChildren(this, int.MaxValue));
       }

        //private IEnumerable<ISPNode> LoadCollectionChildren(SPSiteCollection collection, int batchCount)
        //{
        //    var list = new List<ISPNode>();
        //    if (collection == null) return list;

        //    this.TotalCount = collection.Count;

        //    for (int i = 0; i < TotalCount; i++)
        //    {
        //        var site = collection[i];
        //        var node = NodeTypes[site.GetType()];

        //        // Always create a new node, because the object has to be unique for each item in the treeview.
        //        var instanceNode = (ISPNode)Activator.CreateInstance(node.GetType());
        //        instanceNode.SPObject = site;
        //        instanceNode.ID = i.ToString();

        //        // Save the index for finding the node again with having to get the SPObject
        //        instanceNode.Index = i;

        //        instanceNode.Setup(this);
        //        list.Add(instanceNode);
        //    }

        //    return list.OrderBy(p => p.Text); 
        //}
	}
}
