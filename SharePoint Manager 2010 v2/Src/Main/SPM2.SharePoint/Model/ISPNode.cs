using System;
using SPM2.Framework;
namespace SPM2.SharePoint.Model
{
    public interface ISPNode : INode
    {
        string AddInID { get; set; }
        string Url { get; set; }
        object SPObject { get; set; }
        Type SPObjectType { get;  }
    }
}
