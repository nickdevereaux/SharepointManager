using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

using SPM2.Framework;
using SPM2.Framework.WPF;
using SPM2.Main.GUI;

namespace SPM2.Main.Commands
{
    [AttachTo(Help.AddInID)]
    public class About : MenuItem
    {
        
        public About()
        {
            this.Header = "About";
            this.Icon = ImageExtensions.LoadImage("/SPM2.Main;component/resources/images/about.gif");
            this.Click += new RoutedEventHandler(About_Click);
        }

        void About_Click(object sender, RoutedEventArgs e)
        {
            AboutBox dialog = new AboutBox(Workbench.MainWindow);
            
            dialog.ShowDialog();
        }
    }
}
