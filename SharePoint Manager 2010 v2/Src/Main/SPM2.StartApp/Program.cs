using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows;

namespace SPM2.StartApp
{
    class Program
    {

        [STAThread()]
        static void Main(string[] args)
        {
#if DEBUG
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = true;
#endif
            // Start the application
            using (ApplicationStarter appStarter = new ApplicationStarter())
            {
                appStarter.ShowSplashScreen();
                appStarter.Initialize();
                appStarter.ParseCommandline(args);
                appStarter.Execute(args);
            }
       
        }

    }
}
