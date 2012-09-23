/* ---------------------------
 * SharePoint Manager 2010 v2
 * Created by Carsten Keutmann
 * ---------------------------
 */

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using SPM2.Framework;

namespace SPM2.SharePoint.Model
{
    [Title("AllProperties")]
    [Icon(Small = "EXITGRID.GIF")]
    [View(50)]
    [ExportToNode("SPM2.SharePoint.Model.SPFarmNode")]
    [ExportToNode("SPM2.SharePoint.Model.SPWebNode")]
    [ExportToNode("SPM2.SharePoint.Model.SPFolderNode")]
    [ExportToNode("SPM2.SharePoint.Model.SPFileNode")]
    [ExportToNode("SPM2.SharePoint.Model.SPFileVersionNode")]
    [ExportToNode("SPM2.SharePoint.Model.SPProcessIdentityNode")]
    [ExportToNode(typeof (SPListItemNode))]
    [ExportToNode(typeof (SPJobDefinitionNode))]
    [ExportToNode(typeof (SPServerNode))]
    [ExportToNode(typeof (SPServiceNode))]
    [ExportToNode(typeof (SPWebServiceNode))]
    [ExportToNode(typeof (SPSolutionNode))]
    [AdapterItemType("System.Collections.Hashtable")]
    public class SPPropertyCollectionNode : SPNodeCollection
    {
        [XmlIgnore]
        public Hashtable AllProperties
        {
            get { return (Hashtable) SPObject; }
        }


        public override bool Accept()
        {
            if (NodeProvider.ViewLevel >= 100)
                return true;

            if (ParentPropertyDescriptor != null && ParentPropertyDescriptor.Name == "UpgradedPersistedProperties")
                return false;
            
            return base.Accept();
        }

        public override void LoadChildren()
        {
            var list = new List<ISPNode>();

            foreach (DictionaryEntry entry in AllProperties)
            {
                var node = new SPPropertyNode();
                node.SPObject = entry;
                node.Setup(this);
                list.Add(node);
            }

            Children.AddRange(list.OrderBy(p => p.Text));
        }
    }
}