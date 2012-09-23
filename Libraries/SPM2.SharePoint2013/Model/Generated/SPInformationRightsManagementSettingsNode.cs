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
	[AdapterItemType("Microsoft.SharePoint.SPInformationRightsManagementSettings, Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c")]
	public partial class SPInformationRightsManagementSettingsNode : SPNode
	{
		[XmlIgnore]
        public SPInformationRightsManagementSettings InformationRightsManagementSettings
        {
            get
            {
                return (SPInformationRightsManagementSettings)this.SPObject;
            }
            set
            {
                this.SPObject = value;
            }
        }
	}
}
