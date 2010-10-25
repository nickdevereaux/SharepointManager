using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows.Controls;
using System.ComponentModel;

using AvalonDock;

using SPM2.Framework;
using SPM2.Framework.WPF;
using SPM2.Framework.WPF.Commands;
using SPM2.Framework.WPF.Components;
using SPM2.SharePoint;
using SPM2.SharePoint.Model;

namespace SPM2.Main.GUI.Pads
{

    [Title("PropertyGrid")]
    [AttachTo("SPM2.Main.MainWindow.ContentPane", After = "SPM2.Main.GUI.Pads.PropertyGridPad")]
    public class BrowserPad : AbstractPadWindow
    {
        private const string NAME = "Browser";

        public BrowserControl BrowserContainer = new BrowserControl();

        private object SelectedObject { get; set; }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            this.Title = NAME;
            this.Loaded += new System.Windows.RoutedEventHandler(BrowserPad_Loaded);

            this.Content = this.BrowserContainer;
        }


        private void BrowserPad_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Workbench.MainWindow.CommandBindings.AddCommandExecutedHandler(SPM2Commands.ObjectSelected, ObjectSelected_Executed);
            
        }


        private void ObjectSelected_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.SelectedObject = e.Parameter;
            SetObject();
        }

        protected override void OnClosed()
        {
            base.OnClosed();

            Workbench.MainWindow.CommandBindings.RemoveCommandExecutedHandler(SPM2Commands.ObjectSelected, ObjectSelected_Executed);
        }

        private void SetObject()
        {
            ISPNode node = (ISPNode)this.SelectedObject;
            if (node != null && !String.IsNullOrEmpty(node.Url))
            {
                this.BrowserContainer.Browser.Url = new Uri(node.Url);
            }
        }


    }
}
