using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using SPM2.Framework;
using SPM2.Framework.Collections;
namespace SPM2.SharePoint.Model
{
    [XmlInclude(typeof(SPNode))]
    public interface ISPNode : IDisposable
    {
        string Text { get; set; }
        string ToolTipText { get; set; }
        string State { get; set; }
        string IconUri { get; set; }
        string AddInID { get; set; }
        string Url { get; set; }
        object SPObject { get; set; }
        
        ClassDescriptor Descriptor { get; set; }
        SerializableList<ISPNode> Children { get; set; }
        Dictionary<Type, ISPNode> NodeTypes { get; set; }
        ISPNodeProvider NodeProvider { get; set; }
        
        ISPNode Parent { get; set; }
        string ID { get; set; }

        OrderingCollection<ActionItem> MenuItemCollection { get; set; }

        Type SPObjectType { get; set; }
        void LoadChildren();
        void ClearChildren();

        object GetSPObject();
        Type GetSPObjectType();

        void Setup(ISPNode parent);


        void Dispose();
    }
}
