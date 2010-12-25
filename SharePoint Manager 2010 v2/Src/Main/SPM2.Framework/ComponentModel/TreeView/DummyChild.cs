using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPM2.Framework.ComponentModel
{
    public class DummyChild : TreeViewItemModel
    {
        public DummyChild()
        {
            this.LazyLoadChildren = false;

        }
    }
}
