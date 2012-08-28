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
        Func<Form> Setup;
        Form result;

        public SplashScreen(Func<Form> setup)
        {
            InitializeComponent();
            Setup = setup;
            Shown += SplashScreen_Shown;
        }

        void SplashScreen_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();

            result = Setup.Invoke();

            this.Close();
        }

        static public Form ShowSplashScreen(Func<Form> setup)
        {
            var screen = new SplashScreen(setup);
            Application.Run(screen);

            return screen.result;
        }
    }
}
