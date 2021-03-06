﻿using System;
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

namespace SPM2.Framework.WPF.Components
{
    /// <summary>
    /// Interaction logic for BrowserControl.xaml
    /// </summary>
    public partial class BrowserControl : UserControl
    {
        public BrowserControl()
        {
            InitializeComponent();

            this.Browser.AllowNavigation = true;
        }

        public string Url
        {
            get
            {
                return this.Browser.Url.AbsoluteUri;
            }
            set
            {
                this.Browser.Url = new Uri(value);
            }
        }

    }
}
