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
using ICSharpCode.TreeView;
using GalaSoft.MvvmLight.Messaging;

namespace SPM2.Main.GUI.Pads
{

    [Title("SharePoint Explorer")]
    [Export("SPM2.Main.MainWindow.LeftDockPane", typeof(DockableContent))]
    [ExportMetadata("ID", "SPM2.Main.GUI.Pads.SPTreeViewPad")]
    public class SPTreeViewPad : AbstractPadWindow, IPartImportsSatisfiedNotification
    {
        private bool DoSelect { get; set; }

        SharpTreeView wpfView = new SharpTreeView();

        [Import()]
        public SPModelProvider ModelProvider { get; set; }

        public SPTreeViewPad()
        {
            this.Title = "SharePoint Explorer";
            wpfView.ShowLines = false;

            Application.Current.MainWindow.ContentRendered += new EventHandler((s,e) => SelectItem());

            wpfView.PreviewMouseDown += new MouseButtonEventHandler((s,e) => this.DoSelect = true);
            wpfView.PreviewStylusDown += new StylusDownEventHandler((s, e) => this.DoSelect = true);
            wpfView.PreviewKeyDown += new KeyEventHandler(wpfView_PreviewKeyDown);
            wpfView.SelectionChanged += new SelectionChangedEventHandler(wpfView_SelectionChanged);

            this.Content = wpfView;
        }


        void wpfView_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                SelectItem();
            }
        }


        void wpfView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.DoSelect)
            {
                SelectItem();
                this.DoSelect = false;
            }
        }


        public void OnImportsSatisfied()
        {
            wpfView.Root = this.ModelProvider;
           
            this.ModelProvider.ExpandToDefault();
        }

        private void SelectItem()
        {
            object item = wpfView.SelectedItem;
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
                    if (SPM2Commands.ObjectSelected.CanExecute(node, this))
                    {
                        SPM2Commands.ObjectSelected.Execute(node, this);
                    }
                }
            }
        }




    }
}
