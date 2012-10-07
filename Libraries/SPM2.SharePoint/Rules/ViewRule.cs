using System;
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
    [ExportMetadata("Order", int.MaxValue)]
    public class ViewRule : INodeIncludeRule
    {
        // Always accept this rule as it should be the laset one.
        public bool Accept(ISPNode node)
        {
 	        return true;
        }

        // Check the node
        public bool Check(ISPNode node)
        {
            if (node == null) throw new ArgumentNullException("node");

            if (node is IViewRule)
            {
                return ((IViewRule)node).IsVisible();
            }

            var type = node.GetType();
            var list = type.GetCustomAttributes(true).OfType<ViewAttribute>();
            if (list.Count() == 0) return true;

            return list.Any(p => node.NodeProvider.ViewLevel >= p.Level);
        }
    }
}
