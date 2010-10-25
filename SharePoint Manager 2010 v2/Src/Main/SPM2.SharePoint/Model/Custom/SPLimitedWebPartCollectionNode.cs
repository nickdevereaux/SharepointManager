using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.SharePoint.WebPartPages;

using SPM2.Framework;

namespace SPM2.SharePoint.Model
{
    [AdapterItemType("Microsoft.SharePoint.WebPartPages.SPLimitedWebPartCollection, Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c")]
    //[AttachTo("SPM2.SharePoint.Model.SPFormNode")]
    public class SPLimitedWebPartCollectionNode : SPNodeCollection
    {

        public SPLimitedWebPartCollection WebParts
        {
            get
            {
                return (SPLimitedWebPartCollection)this.SPObject;
            }
            set
            {
                this.SPObject = value;
            }
        }

        public override void Setup(object spObject, ClassDescriptor descriptor)
        {
            base.Setup(spObject, descriptor);
            this.Text = "WebParts";
            this.IconUri = SharePointContext.GetImagePath("itgen.GIF");
        }
    }
}
