using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.ComponentModel;

using AvalonDock;

using SPM2.Framework;
using SPM2.Framework.WPF;
using SPM2.Framework.WPF.Commands;
using SPM2.Framework.WPF.Components;
using SPM2.SharePoint;
using SPM2.SharePoint.Model;
using System.Windows.Input;
using System.ComponentModel.Composition;
using System.Windows;

namespace SPM2.Main.GUI.Pads
{

    [Title("SharePoint Explorer")]
    [Export("SPM2.Main.MainWindow.LeftDockPane", typeof(DockableContent))]
    [ExportMetadata("ID", "SPM2.Main.GUI.Pads.SPModelExporerPad")]
    public class SPModelExporerPad : AbstractPadWindow, IPartImportsSatisfiedNotification
    {
        WPFTreeView wpfView = new WPFTreeView();

        [Import()]
        public SPModelProvider ModelProvider { get; set; }

        public SPModelExporerPad()
        {
            this.Title = "SharePoint Explorer";

            Application.Current.MainWindow.ContentRendered += new EventHandler(MainWindow_ContentRendered);

            wpfView.Explorer.SelectedItemChanged += new System.Windows.RoutedPropertyChangedEventHandler<object>(Explorer_SelectedItemChanged);
            this.Content = wpfView;
        }

        void MainWindow_ContentRendered(object sender, EventArgs e)
        {
            //SelectItem();
        }


        public void OnImportsSatisfied()
        {
            wpfView.DataContext = this.ModelProvider;
            this.ModelProvider.ExpandToDefault();
        }


        void Explorer_SelectedItemChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<object> e)
        {
            SelectItem();
        }

        private void SelectItem()
        {
            object item = wpfView.Explorer.SelectedItem;
            if (item != null)
            {
                if (item is MoreNode)
                {
                    // Excute the more 
                    MoreNode node = (MoreNode)item;
                    node.ParentNode.LoadNextBatch();
                }
                else
                {
                    // Select the node in the Window
                    ISPNode node = item as ISPNode;
                    SPM2Commands.ObjectSelected.Execute(node, null);
                }
            }
        }




    }
}
