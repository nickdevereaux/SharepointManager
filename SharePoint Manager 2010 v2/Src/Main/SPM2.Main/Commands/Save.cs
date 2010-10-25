using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using SPM2.Framework.WPF;
using System.Windows.Input;
using SPM2.Framework;

namespace SPM2.Main.Commands
{
    [AttachTo(Id=File.AddInID, Index= 0)]
    public class Save : MenuItem
    {
        public Save()
        {
            this.Command = ApplicationCommands.Save;
            this.Icon = ImageExtensions.LoadImage("/SPM2.Main;component/resources/images/save.png");
            
        }
    }
}
