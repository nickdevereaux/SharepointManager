using System;
using System.Collections.Generic;
using Microsoft.SharePoint.Administration;
using SPM2.SharePoint.Model;

namespace SPM2.SharePoint
{
    public interface ISPNodeProvider
    {
        int ViewLevel { get; set; }
        SPFarm Farm { get; set; }

        Func<string,string> GetLocalizedTextFunction { get; set; }
        string GetLocalizedText(string text);

        IEnumerable<ISPNode> LoadChildren(ISPNode node);
        IEnumerable<ISPNode> LoadUnorderedChildren(ISPNode sourceNode);
        IEnumerable<ISPNode> LoadCollectionChildren(ISPNodeCollection parentNode, int batchCount);

        ISPNode LoadFarmNode();
        string Serialize(ISPNode node);
        ISPNode Deserialize(string xml);
    }
}