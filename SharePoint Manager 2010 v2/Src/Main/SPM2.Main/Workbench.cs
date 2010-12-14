using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using AvalonDock;
using SPM2.Framework;
using System.ComponentModel;
using SPM2.Framework.ComponentModel;
using SPM2.SharePoint.Model;
using SPM2.Main.ComponentModel;
using System.ComponentModel.Composition.Hosting;

namespace SPM2.Main
{
    public class Workbench
    {
        public const string MainWindowName = "MainWindow";

        public static App Application;
        public static Window MainWindow
        {
            get
            {
                return Workbench.Application.MainWindow;
            }
        }

        //private static CompositionProvider AddinProvider = new CompositionProvider();
        //public static CompositionContainer AddIns { get; set; }

        //public static IWindowsProvider Windows;
        //public static DockingManager DockManager;

        public static void Initialize()
        {
            //AddinProvider.Load("addins");
            //AddIns = AddinProvider.Container;
            
            PropertyGridTypeConverter.AddEditor(typeof(string), typeof(StringEditor));

            Application = CompositionProvider.Current.GetExportedValue<App>();
            Application.InitializeComponent();
        }

        public static void Run()
        {
            Application.Run();
        }
     }
}
