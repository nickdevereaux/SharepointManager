using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.Generic;

using System.Reflection;
using System.Resources;
using System.Globalization;
using System.Collections;
using System.Windows.Media.Imaging;

using AvalonDock;

using SPM2.Framework.WPF;
using SPM2.Framework.Collections;
using SPM2.Framework;
using SPM2.Framework.Reflection;
using SPM2.Framework.WPF.Commands;
using SPM2.SharePoint.Model;
using System.ComponentModel.Composition;
using SPM2.Main.ViewModel;

namespace SPM2.Main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [Export(Workbench.MainWindowName, typeof(Window))]
    public partial class MainWindow : Window
    {
        public const string ToolBarTreyContainer_AddInID = "SPM2.Main.MainWindow.ToolBarTreyContainer";
        public const string MenuContainer_AddInID = "SPM2.Main.MainWindow.MenuContainer";
        public const string LeftDockPane_AddInID = "SPM2.Main.MainWindow.LeftDockPane";
        public const string ContentPane_AddInID = "SPM2.Main.MainWindow.ContentPane";
        public const string BottomDockPane_AddInID = "SPM2.Main.MainWindow.BottomDockPane";


        
        public MainWindow()
        {
            InitializeComponent();
            Build();
            CommandBinding();

        }

        protected override void OnInitialized(System.EventArgs e)
        {
            base.OnInitialized(e);

            MainWindowModel model = (MainWindowModel)this.Resources["Model"];

            if (model.Menus != null)
            {
                foreach (var item in model.Menus)
                {
                    this.MenuContainer.Children.Add(item.Value as Menu);
                }

                foreach (var item in model.ToolBars)
                {
                    this.ToolBarTrayControl.ToolBars.Add(item.Value);
                }


                foreach (var item in model.LeftDockableContents)
                {
                    this.LeftDockPane.Items.Add(item.Value);
                }

                foreach (var item in model.CenterDockableContents)
                {
                    this.ContentPane.Items.Add(item.Value);
                }

                foreach (var item in model.BottomDockableContents)
                {
                    this.BottomDockPane.Items.Add(item.Value);
                }
            }
        }




        private void Build()
        {
            //IList<Menu> menus = AddInProvider.Current.CreateAttachments<Menu>(MenuContainer_AddInID, null);
            //this.MenuContainer.Children.AddRange(menus);


            //IList<ToolBar> toolbars = AddInProvider.Current.CreateAttachments<ToolBar>(ToolBarTreyContainer_AddInID, null);
            //foreach(ToolBar bar in toolbars)
            //{
            //    this.ToolBarTrayControl.ToolBars.Add(bar);
            //}


            //this.LeftDockPane.Items.AddList(WindowProvider.Current.BuildWindows<IPadWindow>(LeftDockPane_AddInID));
            //this.ContentPane.Items.AddList(WindowProvider.Current.BuildWindows<IPadWindow>(ContentPane_AddInID));
            //this.BottomDockPane.Items.AddList(WindowProvider.Current.BuildWindows<IPadWindow>(BottomDockPane_AddInID));

            
        }

        private void CommandBinding()
        {
            this.CommandBindings.AddCommandExecutedHandler(ApplicationCommands.Close, new ExecutedRoutedEventHandler(Exit));
            this.CommandBindings.AddCommandExecutedHandler(SPM2Commands.ObjectSelected, new ExecutedRoutedEventHandler(Edit));
        }

        private void Exit(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        private void Edit(object sender, ExecutedRoutedEventArgs e)
        {
            FirstTextBlock.Text = string.Empty;
            if (e.Parameter != null && e.Parameter is ISPNode)
            {
                ISPNode node = (ISPNode)e.Parameter;
                string name = node.SPObjectType.Name;
                if (FirstTextBlock.Text != name)
                {
                    FirstTextBlock.Text = node.SPObjectType.Name;
                }
            }
        }

    }
}
