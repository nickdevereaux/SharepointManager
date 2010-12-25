using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Threading;
using System.ComponentModel.Composition;
using SPM2.Main.ViewModel;
using System.Windows.Threading;
using SPM2.Framework;
using System.ComponentModel.Composition.Hosting;

namespace SPM2.Main.GUI
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    [Export(AppModel.SPLASHSCREEN_CONTRACT_NAME, typeof(Window))]
    public partial class SplashScreen : Window
    {

        IAsyncResult result = null;
        internal AsynchronousLoadDelegate AsynchronousLoad;

        public SplashScreen(AsynchronousLoadDelegate asynchronousLoad)
        {
            InitializeComponent();

            this.AsynchronousLoad = asynchronousLoad;

            this.Loaded += new RoutedEventHandler(SplashScreen_Loaded);
        }

        void SplashScreen_Loaded(object sender, RoutedEventArgs e)
        {
            // This starts the initialization process on the Application
            result = this.AsynchronousLoad.BeginInvoke(this, new AsyncCallback(AsynchronousLoadCompleted), null);
        }


        public void SetProgress(double progress)
        {
            // Ensure we update on the UI Thread.
            //Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(delegate { progBar.Value = progress; }));

        }

        /// <summary>
        /// This is an method that will be called when the initialization has COMPLETED
        /// </summary>
        /// <param name="ar"></param>
        private void AsynchronousLoadCompleted(IAsyncResult ar)
        {
            this.AsynchronousLoad.EndInvoke(result);

            // Ensure we call close on the UI Thread.
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(Close));
        }
        


    }
}
