/* ---------------------------
 * SharePoint Manager 2010 v2
 * Created by Carsten Keutmann
 * ---------------------------
 */

using System;

using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using SPM2.Framework;
using Microsoft.SharePoint.Utilities;

namespace SPM2.SharePoint.Model
{
	[Title(PropertyName="Title")]
	[Icon(Small="BULLET.GIF")]
	[AttachTo("SPM2.SharePoint.Model.SPListItemNode")]
	[AttachTo("SPM2.SharePoint.Model.SPFileCollectionNode")]
	[AttachTo("SPM2.SharePoint.Model.SPContextNode")]
	public partial class SPFileNode
	{

        public override void Setup(object spObject, ClassDescriptor descriptor)
        {
            base.Setup(spObject, descriptor);

            this.Url = SPUrlUtility.CombineUrl(this.File.ParentFolder.ParentWeb.Url, this.File.Url);
        }
	}
}
