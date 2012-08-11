using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPM2.SharePoint.Model
{
    public class ActionItem : IActionItem
    {
        public virtual string Text { get; set; }
        public virtual string ToolTipText { get; set; }
        public virtual string Icon { get; set; }
        public virtual string Scope { get; set; }
        public virtual bool Enabled { get; set; }
        public virtual ISPNode Node { get; set; }

        public EventHandler Click { get; set; }
    }
}
