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
            if (SPObject == null) return;

            if (Children.Count == 0 || Children[0].Parent != null)
            {
                LoadNewChildren();
            }
            else
            {
                // Not functional
                //InitializeChildren();
            }
        }

        private void LoadNewChildren()
        {
           
            if (Children.Count > 0)
            {
                // Ensure that the last node is the "MoreNode".
                int nodeIndex = Children.Count - 1;
                ISPNode node = Children[nodeIndex];

                if (node is MoreNode)
                {
                    // Remove the "MoreNode" from this.Children.
                    Children.RemoveAt(nodeIndex);
                }
            }
            else
            {
                EnsureNodeTypes();
            }
#if DEBUG
            var watch = new Stopwatch();
            watch.Start();
#endif
            // Load the next batch!
            Children.AddRange(NodeProvider.LoadCollectionChildren(this));

#if DEBUG
            watch.Stop();
            Trace.WriteLine(String.Format("Load Properties: Type:{0} - Time {1} milliseconds.",
                                          SPObjectType.Name, watch.ElapsedMilliseconds));
#endif
        }

        private void InitializeChildren()
        {
            EnsureNodeTypes();

            foreach (var item in Children)
            {
                item.Setup(this);
                if (item.Children.Count > 0)
                {
                    item.LoadChildren();
                }
            }
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