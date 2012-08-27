using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.Serialization;
using Microsoft.SharePoint.Client;
using SPM2.Framework;

namespace SPM2.SharePoint.Model
{

    public class SPNodeCollection : SPNode, ISPNodeCollection
    {
        private ISPNode _defaultNode;
        /// <summary>
        /// The default node used if the node type is unknown.
        /// </summary>
        [XmlIgnore]
        public ISPNode DefaultNode
        {
            get
            {
                if (_defaultNode == null)
                {
                    _defaultNode = NodeProvider.FindDefaultNode(this);
                }
                return _defaultNode;
            }
            set { _defaultNode = value; }
        }

        [XmlIgnore]
        public IEnumerator Pointer { get; set; }

        [XmlIgnore]
        public bool LoadingChildren { get; set; }

        public int TotalCount { get; set; }


        public SPNodeCollection()
        {
            LoadingChildren = true;
        }


        public override void LoadChildren()
        {
            Children.AddRange(NodeProvider.LoadCollectionChildren(this, int.MaxValue));
        }


        public override void ClearChildren()
        {
            Pointer = null;
            TotalCount = 0;
            LoadingChildren = true;
            Children.Clear();
        }
    }
}