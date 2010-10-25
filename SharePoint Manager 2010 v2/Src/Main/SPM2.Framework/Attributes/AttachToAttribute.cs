using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using SPM2.Framework.Collections;

namespace SPM2.Framework
{
    [AttributeUsage(System.AttributeTargets.Class, AllowMultiple=true)]
    public class AttachToAttribute : Attribute, IComparer<AttachToAttribute>
    {
        public string Id { get; set; }
        public string Title { get; set; }

        public string Before { get; set; }
        public string After { get; set; }
        public string Replace { get; set; }

        /// <summary>
        /// Defines the order of the attachment, the lower number comes first. 
        /// </summary>
        public uint Index { get; set; }

        public AttachToAttribute()
        {
            this.Index = uint.MaxValue / 2;
        }

        public AttachToAttribute(string id) :  this ()
        {
            this.Id = id;
        }

        #region Internal stuff for the AddInProvider

        internal ClassDescriptor Descriptor { get; set; }

        internal List<AttachToAttribute> Children = new List<AttachToAttribute>();
        internal AttachToAttribute Parent = null;

        internal bool Visited { get; set; }


        internal bool Add(AttachToAttribute attribute)
        {
            if (this == attribute)
            {
                return false;
            }

            if (this.Descriptor == null || attribute.Descriptor == null)
            {
                this.Children.Add(attribute);
                attribute.Parent = this;
                return true;
            }

            if (this.Before == attribute.Descriptor.AddInID)
            {
                if (attribute.Parent != null)
                {
                    attribute.Parent.Children.Remove(attribute);
                    attribute.Parent.Children.Add(this);
                    this.Parent = attribute.Parent;
                }
                this.Children.AddOrReplace(attribute);
                attribute.Parent = this;
                return true;
            }
            else
            {
                if (this.After == attribute.Descriptor.AddInID)
                {
                    attribute.Children.AddOrReplace(this);
                    this.Parent = attribute;
                    return true;
                }
            }

            return false;
        }


        internal List<AttachToAttribute> GetSortedAttachToAttributes()
        {
            List<AttachToAttribute> result = new List<AttachToAttribute>();

            if (!this.Visited)
            {
                this.Visited = true;

                this.Children.Sort(this.Compare);

                foreach (AttachToAttribute item in this.Children)
                {
                    result.Add(item);
                    result.AddRange(item.GetSortedAttachToAttributes());
                }
            }
            return result;

        }

        public int Compare(AttachToAttribute x, AttachToAttribute y)
        {
            int result = 0;

            if (x != null && y != null)
            {
                result = x.Index.CompareTo(y.Index);
            }
            return result;
        }
        #endregion

    }
}
