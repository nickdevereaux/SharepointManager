using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SPM2.Framework;

namespace SPM2.SharePoint.Model
{
    [Icon(Small="RECUR.GIF")]
    [Title("More ... ")]
    public class MoreNode : SPNode
    {
        public SPNodeCollection ParentNode { get; set; }

        public MoreNode(SPNodeCollection parentNode)
        {
            this.ParentNode = parentNode;
            this.ToolTipText = "Fetch more items...";
            this.SPObject = new object();
        }

        public override void Setup(object spParent)
        {
            base.Setup(spParent);

            // Remove the expand arrow, by forcing a LoadChildren() call that will turn out empty.
            this.IsExpanded = true;
        }

        public override void LoadChildren()
        {
            // Load nothing
        }
    }
}
