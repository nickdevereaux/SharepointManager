/* ---------------------------
 * SharePoint Manager 2010 v2
 * Created by Carsten Keutmann
 * ---------------------------
 */

using System;

using Microsoft.SharePoint.DistributedCaching.Utilities;
using SPM2.Framework;

namespace SPM2.SharePoint.Model
{
	[AdapterItemType("Microsoft.SharePoint.DistributedCaching.Utilities.SPDistributedCacheCallUsageProvider, Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c")]
	public partial class SPDistributedCacheCallUsageProviderNode : SPUsageDefinitionNode
	{
		[XmlIgnore]
        public SPDistributedCacheCallUsageProvider DistributedCacheCallUsageProvider
        {
            get
            {
                return (SPDistributedCacheCallUsageProvider)this.SPObject;
            }
            set
            {
                this.SPObject = value;
            }
        }
	}
}
