using System;
using SPM2.Framework;
using SPM2.Main;
using System.Windows.Interop;

namespace SPM2.StartApp
{
    public class ApplicationStarter
    {

        App app;

        public ApplicationStarter()
        {
            app = new App();
            Workbench.Initialize();

            System.Windows.Forms.Integration.WindowsFormsHost.EnableWindowsFormsInterop();
            ComponentDispatcher.ThreadIdle -= ComponentDispatcher_ThreadIdle; // ensure we don't register twice
            ComponentDispatcher.ThreadIdle += ComponentDispatcher_ThreadIdle;
            //WorkbenchSingleton.InitializeWorkbench(new WpfWorkbench(), new AvalonDockLayout());
        }

        static void ComponentDispatcher_ThreadIdle(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.RaiseIdle(e);
        }

        public void Execute(string[] args)
        {
            app.Run(Workbench.MainWindow);
            
        }
    }
}
