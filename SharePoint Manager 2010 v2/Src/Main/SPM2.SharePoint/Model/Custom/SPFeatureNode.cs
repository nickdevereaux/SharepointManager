/* ---------------------------
 * SharePoint Manager 2010 v2
 * Created by Carsten Keutmann
 * ---------------------------
 */

using System;

using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using SPM2.Framework;

namespace SPM2.SharePoint.Model
{
	[Title("SPFeature")]
	[Icon(Small="BULLET.GIF")]
	[AttachTo("SPM2.SharePoint.Model.SPFeatureCollectionNode")]
	public partial class SPFeatureNode
	{
        private string _customText = null;

        //private SPFeature _feature = null;
        //public SPFeature Feature
        //{
        //    get 
        //    {
        //        if (_feature == null)
        //        {
        //            _feature = (SPFeature)this.SPObject;
        //        }
        //        return _feature; 
        //    }
        //    set { _feature = value; }
        //}

        public override string Text
        {
            get
            {
                if (_customText == null)
                {
                    if (this.Feature.Definition != null)
                    {
                        _customText = this.Feature.Definition.DisplayName;
                    }
                    else
                    {
                        _customText = base.Text;
                    }
                }
                return _customText;
            }
            set
            {
                _customText = value;
            }
        }

	}
}
