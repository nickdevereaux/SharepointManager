using System;
using System.Web.UI.WebControls.WebParts;

using Microsoft.SharePoint;

using Keutmann.SharePointManager.Library;

namespace Keutmann.SharePointManager.Components
{
    class WebPartNode : ExplorerNodeBase
    {

        public WebPart ASPWebPart
        {
            get
            {
                return this.Tag as WebPart;
            }
        }

        public bool IsSharePointWebPart
        {
            get
            {
                return this.ASPWebPart is Microsoft.SharePoint.WebPartPages.WebPart;
            }
        }


        public WebPartNode(object spParent, WebPart webpart)
        {
            this.Tag = webpart;
            this.SPParent = spParent;

            int index = Program.Window.Explorer.AddImage(this.ImageUrl());
            this.ImageIndex = index;
            this.SelectedImageIndex = index;

            this.Setup();

        }

        public override void Setup()
        {

            string title = this.ASPWebPart.Title;
            if (title.Length == 0)
            {
                title = this.ASPWebPart.GetType().Name;
            }
            this.Text = title;

            this.ToolTipText = this.ASPWebPart.Description;
            this.Name = this.ASPWebPart.ID.ToString();
        }


        public override void LoadNodes()
        {
            base.LoadNodes();
        }

        public override string ImageUrl()
        {
            return SPMPaths.TemplateDirectory + ASPWebPart.CatalogIconImageUrl;
        }


        // TODO Fix that normal Update function do not work with webparts. Create a override function for this.
        // However on the ListViewWebPart, not all properties can be saved.
    }
}
