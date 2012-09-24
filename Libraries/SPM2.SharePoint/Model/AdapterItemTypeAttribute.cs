using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace SPM2.SharePoint.Model
{
    [AttributeUsage(System.AttributeTargets.Class, AllowMultiple=false)]
    public class AdapterItemTypeAttribute : ExportAttribute
    {
        public string Name { get; set; }
        public string Key { get; set; }

        public AdapterItemTypeAttribute()
        {
        }

        public AdapterItemTypeAttribute(string name)
            : base(name.IndexOf(",") > 0 ? name.Substring(0, name.IndexOf(",")) : name, typeof(ISPNode))
        {
            Name = name;
        }
    }
}
