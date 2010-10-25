using System;
using System.Windows.Forms;
using System.ComponentModel;

using SPM2.Framework;
using SPM2.Framework.WPF;
using SPM2.Main;
using SPM2.Main.GUI;

namespace TestAddIn
{
    [Title("My Test Command")]
    [Description("Bla bla")]
    [Icon("hans")]
    [AttachTo(SPM2.Main.GUI.MainMenu.AddInID)]
    public class MyTestCommand : MenuItem
    {
        public const string AddinId = "TestAddIn.MyTestCommand";

        protected override void  OnClick(EventArgs e)
        {
            MessageBox.Show("MyTestCommand pushed!");
        }

    }
}
