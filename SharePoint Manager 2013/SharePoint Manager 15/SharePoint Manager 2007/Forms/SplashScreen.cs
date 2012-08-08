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
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
            worker.RunWorkerAsync();

            this.Update();
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            result = Setup.Invoke();
        } 


        static public Form ShowSplashScreen(Func<Form> setup)
        {
            var screen = new SplashScreen(setup);
            Application.Run(screen);

            return screen.result;
        }
    }
}
