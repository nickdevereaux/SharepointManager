using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.ComponentModel;
using SPM2.Main.GUI;
using SPM2.Framework;
using SPM2.Framework.WPF;
using System.Windows.Controls;

namespace SPM2.Main.Commands
{
    [AttachTo(MainMenu.AddInID, Index = uint.MaxValue-100)]
    public class Help : MenuItem
    {
        public const string AddInID = "SPM2.Main.Commands.Help";


        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            this.Header = "_Help";
        }
    }
}
