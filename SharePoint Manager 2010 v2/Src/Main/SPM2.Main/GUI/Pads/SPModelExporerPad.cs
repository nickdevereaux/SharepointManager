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

            //SPModelProvider ModelProvider = CompositionProvider.Current.GetExportedValue<SPModelProvider>(); 

            //wpfView.DataContext = ModelProvider;
            //wpfView.Explorer.SelectedItemChanged += new System.Windows.RoutedPropertyChangedEventHandler<object>(Explorer_SelectedItemChanged);
            //this.Content = wpfView;
        }


        public void OnImportsSatisfied()
        {
            wpfView.DataContext = this.ModelProvider;
            wpfView.Explorer.SelectedItemChanged += new System.Windows.RoutedPropertyChangedEventHandler<object>(Explorer_SelectedItemChanged);
            this.Content = wpfView;
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
                ISPNode node = item as ISPNode;
                SPM2Commands.ObjectSelected.Execute(node, null);
            }
        }




    }
}
