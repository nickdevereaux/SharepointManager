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

namespace SPM2.Main
{
    public class Workbench
    {
        public const string AddInID = "SPM2.Main.Workbench";

        public static Window MainWindow;

        //public static IWindowsProvider Windows;
        //public static DockingManager DockManager;

        public static void Initialize()
        {
            IList<Window> windows = AddInProvider.Current.CreateAttachments<Window>(AddInID, null);
            if (windows.Count > 0)
            {
                MainWindow = windows[0];
            }
            else
            {
                throw new ApplicationException("No Main Window found for the Application!");
            }

            //DockManager = MainWindow.DockManager;
            //Windows = WindowProvider.Current;
            PropertyGridTypeConverter.AddEditor(typeof(string), typeof(StringEditor)); 

            



        }
     }
}
