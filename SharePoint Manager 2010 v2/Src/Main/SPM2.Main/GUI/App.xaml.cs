using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using SPM2.Framework;
using System.ComponentModel.Composition;
using SPM2.Framework.Collections;

namespace SPM2.Main
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    [Export()]
    public partial class App : Application
    {
        [ImportMany("MainWindow", typeof(Window))]
        private OrderingCollection<Window> WindowCandidates { get; set; }
        
        void Main(object sender, StartupEventArgs e)
        {
            if (this.WindowCandidates.Count > 0)
            {
                this.MainWindow = this.WindowCandidates[0].Value;
                this.MainWindow.Show();
            }
            else
            {
                throw new ApplicationException("No main window found for the application!");
            }
        }


    }
}
