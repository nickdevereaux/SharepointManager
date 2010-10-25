using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using SPM2.Framework;
using System.Windows;
using SPM2.Framework.WPF.Components;

namespace SPM2.Main.Commands
{
    [AttachTo(MainWindow.ToolBarTreyContainer_AddInID)]
    public class MainToolBar : ToolBar
    {
        public const string AddInID = "SPM2.Main.Commands.MainToolBar";

        public MainToolBar()
        {
            IList<UIElement> elements = AddInProvider.Current.CreateAttachments<UIElement>(AddInID, null);
            foreach (UIElement element in elements)
            {
                this.Items.Add(element);
            }
        }

    }
}
