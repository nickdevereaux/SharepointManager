/* ---------------------------
 * SharePoint Manager 2010 v2
 * Created by Carsten Keutmann
 * ---------------------------
 */

using System;

using Microsoft.SharePoint;
using SPM2.Framework;

namespace SPM2.SharePoint.Model
{
	[AdapterItemType("Microsoft.SharePoint.SPThemeInfo, Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c")]
	public partial class SPThemeInfoNode : SPNode
	{
		[XmlIgnore]
        public SPThemeInfo ThemeInfo
        {
            get
            {
                return (SPThemeInfo)this.SPObject;
            }
            set
            {
                this.SPObject = value;
            }
        }
	}
}
