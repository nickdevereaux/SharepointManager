using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SPM2.Main.GUI;
using SPM2.Framework.Collections;
using SPM2.Framework;
using System.Windows;

namespace SPM2.Main.ViewModel
{
    public delegate void AsynchronousLoadDelegate(SPM2.Main.GUI.SplashScreen splashWindow);

    public class AppModel
    {
        public const string SPLASHSCREEN_CONTRACT_NAME = "SplashScreen";
        public const string MAINWINDOW_CONTRACT_NAME = "MainWindow";

        
        //private Window  _splashScreenWindow  = null;
        //public Window SplashScreenWindow 
        //{
        //    get
        //    {
        //        if (_splashScreenWindow  == null)
        //        {
        //            // Setup code here!
        //            OrderingCollection<Window> orderedItems = CompositionProvider.GetOrderedExports<Window>(SPLASHSCREEN_CONTRACT_NAME);
        //            if (orderedItems.Count > 0)
        //            {
        //                _splashScreenWindow = orderedItems[0].Value;
        //            }
        //        }
        //        return _splashScreenWindow ;
        //    }
        //    set
        //    {
        //        _splashScreenWindow  = value;
        //    }
        //}

        private Window _mainWindow = null;
        public Window MainWindow
        {
            get
            {
                if (_mainWindow == null)
                {
                    // Setup code here!
                    OrderingCollection<Window> orderedItems = CompositionProvider.GetOrderedExports<Window>(MAINWINDOW_CONTRACT_NAME);
                    if (orderedItems.Count > 0)
                    {
                        _mainWindow = orderedItems[0].Value;
                    }
                }
                return _mainWindow;
            }
            set
            {
                _mainWindow = value;
            }
        }

        public AppModel()
        {
        }







    }
}
