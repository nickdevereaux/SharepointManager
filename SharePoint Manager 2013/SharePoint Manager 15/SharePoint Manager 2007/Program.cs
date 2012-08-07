using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Keutmann.SharePointManager.Forms;
using Keutmann.SharePointManager.Library;
using SPM2.Framework;

namespace Keutmann.SharePointManager
{
    static class Program
    {
        public class Win32
        {
            [DllImport("kernel32.dll")]
            public static extern Boolean AllocConsole();
            [DllImport("kernel32.dll")]
            public static extern Boolean FreeConsole();
        }


        public static MainWindow Window = null;

        public static Stopwatch Watch = new Stopwatch();
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
#if DEBUG
            Win32.AllocConsole();
#endif

            Trace.WriteLine("Application started");
            Watch.Start();

            SplashScreen.ShowSplashScreen();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            CompositionProvider.LoadAssemblies();
            
            Window = new MainWindow();
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            Application.Run(Window);
            
            Trace.WriteLine("Application ended");
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Cursor.Current = Cursors.Default;
            MessageBox.Show(e.Exception.Message, SPMLocalization.GetString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}