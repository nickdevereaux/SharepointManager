using System;
using SPM2.Framework;
using SPM2.Main;
using System.Windows.Interop;
using System.ComponentModel.Composition.Hosting;
using System.Windows;
using System.Diagnostics;
using System.Windows.Threading;


namespace SPM2.StartApp
{
    public class ApplicationStarter : IDisposable
    {
       
        public ApplicationStarter()
        {
        }

        public void ShowSplashScreen()
        {
            SplashScreen splashScreen = new SplashScreen("SplashScreen.png");
            splashScreen.Show(true);
        }

        public void Initialize()
        {
            Workbench.Initialize();

            Workbench.Application.DispatcherUnhandledException += new DispatcherUnhandledExceptionEventHandler(Current_DispatcherUnhandledException);


            System.Windows.Forms.Integration.WindowsFormsHost.EnableWindowsFormsInterop();
            ComponentDispatcher.ThreadIdle -= ComponentDispatcher_ThreadIdle; // ensure we don't register twice
            ComponentDispatcher.ThreadIdle += ComponentDispatcher_ThreadIdle;

            //WorkbenchSingleton.InitializeWorkbench(new WpfWorkbench(), new AvalonDockLayout());
        }

        public void ParseCommandline(string[] args)
        {

        }


        public void Execute(string[] args)
        {
            Workbench.Run();
        }

        #region Eventhandlers

        void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            Trace.Fail(e.Exception.Message, e.Exception.StackTrace);

#if DEBUG
            MessageBox.Show(e.Exception.Message+ "\r\n -> "+ e.Exception.StackTrace, "Error! (Debug mode)", MessageBoxButton.OK, MessageBoxImage.Error);
#else
            MessageBox.Show(e.Exception.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
#endif
            e.Handled = true;
        }

        void ComponentDispatcher_ThreadIdle(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.RaiseIdle(e);
        }


        #endregion

        public void Dispose()
        {
            Workbench.Application.DispatcherUnhandledException -= Current_DispatcherUnhandledException;
            ComponentDispatcher.ThreadIdle -= ComponentDispatcher_ThreadIdle;
        }
    }
}
