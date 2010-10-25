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
using System.Windows.Navigation;
using System.Windows.Shapes;

using SPM2.Framework;

namespace SPM2.Framework.WPF.Components
{
    /// <summary>
    /// Interaction logic for WPFTreeView.xaml
    /// </summary>
    public partial class WPFTreeView : UserControl
    {

        public TreeView Explorer = null;

         public WPFTreeView()
        {
            InitializeComponent();

            this.Explorer = this.TV;

        }
    }
}
