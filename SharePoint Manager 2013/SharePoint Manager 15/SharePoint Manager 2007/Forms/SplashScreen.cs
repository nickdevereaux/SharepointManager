using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Keutmann.SharePointManager.Forms
{
    public partial class SplashScreen : Form
    {
        static Thread thread = null;
        static SplashScreen frmSplash = null;

        public SplashScreen()
        {
            InitializeComponent();
        }
       static public void ShowSplashScreen()
        {
            // Make sure it is only launched once.
            if (frmSplash != null)
                return;
            thread = new Thread(new ThreadStart(SplashScreen.ShowForm));
            thread.IsBackground = false ;
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();

        }



        static private void ShowForm()
        {
            frmSplash = new SplashScreen();
            Application.Run(frmSplash);
        }

        static public SplashScreen SplashForm
        {
            get
            {
                return frmSplash;
            }
        }

        static public void CloseForm()
        {
            if (frmSplash != null && !frmSplash.IsDisposed)
            {
                // Make it start going away.
                frmSplash.Hide();
                frmSplash.Dispose();
                frmSplash = null;   
            }
            if (thread.IsAlive)
            {
                thread.Abort();
                thread = null;  // we do not need these any more.
            }
        }



    }
}
