using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.ComponentModel;

using SPM2.Framework;
using SPM2.Framework.WPF;

namespace SPM2.Main.Commands
{
    [AttachTo(File.AddInID)]
    public class Exit : MenuItem
    {
        public Exit()
        {
            this.Command = ApplicationCommands.Close;
            this.Icon = ImageExtensions.LoadImage("/SPM2.Main;component/resources/images\\delete.png");
        }
    }
}
