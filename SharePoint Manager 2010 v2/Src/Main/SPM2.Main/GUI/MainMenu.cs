using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using SPM2.Framework;
using SPM2.Framework.WPF;

namespace SPM2.Main.GUI
{
    [AttachTo(MainWindow.MenuContainer_AddInID)]
    public class MainMenu : Menu
    {
        public const string AddInID = "SPM2.Main.GUI.MainMenu";

        public MainMenu()
        {
            this.Items.LoadChildren(AddInID);
        }


    }
}
