using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPM2.Framework
{
    [AttributeUsage(System.AttributeTargets.Class, AllowMultiple=false)]
    public class IconAttribute : Attribute
    {
        public string Large { get; set; }
        public string Small { get; set; }

        public IconAttribute()
        {
        }

        public IconAttribute(string small)
        {
            this.Small = small;
        }


    }
}
