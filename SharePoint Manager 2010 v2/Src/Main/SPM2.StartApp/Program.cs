using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SPM2.StartApp
{
    class Program
    {

        [STAThread()]
        static void Main(string[] args)
        {
            try
            {
#if DEBUG
                Control.CheckForIllegalCrossThreadCalls = true;
#endif
                // Start the application
                ApplicationStarter app = new ApplicationStarter();
                app.Execute(args);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message+"\r\n"+ex.StackTrace);
            }
                   
        
        }

    }
}
