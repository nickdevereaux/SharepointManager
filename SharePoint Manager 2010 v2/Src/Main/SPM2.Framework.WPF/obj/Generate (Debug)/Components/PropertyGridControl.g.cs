﻿#pragma checksum "..\..\..\Components\PropertyGridControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "2C4623D2F1E84D65754E0DEB819BFC20"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace SPM2.Framework.WPF.Components {
    
    
    /// <summary>
    /// PropertyGridControl
    /// </summary>
    public partial class PropertyGridControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 6 "..\..\..\Components\PropertyGridControl.xaml"
        internal System.Windows.Forms.Integration.WindowsFormsHost propertyGridHost;
        
        #line default
        #line hidden
        
        /// <summary>
        /// propertyGrid Name Field
        /// </summary>
        
        #line 7 "..\..\..\Components\PropertyGridControl.xaml"
        public System.Windows.Forms.PropertyGrid propertyGrid;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/SPM2.Framework.WPF;component/components/propertygridcontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Components\PropertyGridControl.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.propertyGridHost = ((System.Windows.Forms.Integration.WindowsFormsHost)(target));
            return;
            case 2:
            this.propertyGrid = ((System.Windows.Forms.PropertyGrid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
