﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using SPM2.Framework;
using SPM2.SharePoint.Model;


namespace SPM2.SharePoint.Rules
{
    [Export(typeof(INodeIncludeRule))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    [ExportMetadata("Order", int.MaxValue-1)]
    public class RecursiveRule : INodeIncludeRule
    {

        public bool Accept(ISPNode node)
        {
            if (node == null) throw new ArgumentNullException("node");

            if (IsRecursiveVisible(node))
                return false;

            if (PreviousExist(node.Parent, node.GetType().FullName))
                return true;

            return false;
        }

        // Check the node
        public bool Check(ISPNode node)
        {
            return false;
        }

        private bool PreviousExist(ISPNode node, string typeName)
        {
            if (node == null) return false;

            if (node.GetType().FullName == typeName) return true;

            return PreviousExist(node.Parent, typeName);
        }

        public static bool IsRecursiveVisible(ISPNode node)
        {
            //return node.Descriptor.Attributes.OfType<ExportMetadataAttribute>().Any(p => RecursiveVisibleKey.EqualsIgnorecase(p.Name) && true.Equals(p.Value));
            return node.Descriptor.Attributes.OfType<RecursiveRuleAttribute>().Any(p => p.IsRecursiveVisible);
        }
    }
}