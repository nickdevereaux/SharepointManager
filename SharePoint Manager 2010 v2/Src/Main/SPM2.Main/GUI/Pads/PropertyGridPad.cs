using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Input;

using AvalonDock;

using SPM2.Framework;
using SPM2.Framework.WPF;
using SPM2.Framework.WPF.Commands;
using SPM2.Framework.WPF.Components;
using SPM2.SharePoint;
using SPM2.SharePoint.Model;
using System.Windows.Forms;
using System.Collections;

namespace SPM2.Main.GUI.Pads
{

    [Title("PropertyGrid")]
    [AttachTo("SPM2.Main.MainWindow.ContentPane")]
    public class PropertyGridPad : AbstractPadWindow
    {
        private const string PROPERTY_GRID_NAME = "PropertyGrid";

        public PropertyGridControl PGrid = new PropertyGridControl();

        public Dictionary<object, Hashtable> ChangedPropertyItems = new Dictionary<object, Hashtable>();
        public bool ValueChanged { get; set; }

        private object SelectedObject { get; set; }



        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            this.Title = PROPERTY_GRID_NAME;

            this.Loaded += new System.Windows.RoutedEventHandler(PropertyGridPad_Loaded);

            PGrid.propertyGrid.PropertyValueChanged += new PropertyValueChangedEventHandler(propertyGrid_PropertyValueChanged);
            
            this.Content = PGrid;
        }

        void PropertyGridPad_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Workbench.MainWindow.CommandBindings.AddCommandExecutedHandler(SPM2Commands.ObjectSelected, ObjectSelected_Executed);
            Workbench.MainWindow.CommandBindings.AddCommandExecutedHandler(ApplicationCommands.Save, Save_Executed);
            Workbench.MainWindow.CommandBindings.AddCommandCanExecuteHandler(ApplicationCommands.Save, Save_CanExecute);
        }



        void ObjectSelected_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.SelectedObject = e.Parameter;
            SetObject();
        }


        void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.ValueChanged;
        }

        void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (this.ValueChanged)
            {
                // Save the changes from the property grid on the object, is the object supports a "Update" method.
                this.SelectedObject.InvokeMethod("Update");
            }
        }

        void propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            //if (!CancelActive)
            //{
            Hashtable propertyItems = null;
            if (!ChangedPropertyItems.ContainsKey(this.SelectedObject))
            {
                propertyItems = new Hashtable();
                ChangedPropertyItems[this.SelectedObject] = propertyItems;
            }
            else
            {
                propertyItems = ChangedPropertyItems[this.SelectedObject];
            }
            propertyItems[e.ChangedItem] = e;

            ValueChanged = true;
                //ChangedNodes[node] = true;

                //UpdateMenu(node);
            //}
        }


        protected override void OnClosed()
        {
            base.OnClosed();

            Workbench.MainWindow.CommandBindings.RemoveCommandExecutedHandler(SPM2Commands.ObjectSelected, ObjectSelected_Executed);
        }

        private void SetObject()
        {
            ISPNode node = (ISPNode)this.SelectedObject;
            if (node != null)
            {
                PGrid.propertyGrid.SelectedObject = node.SPObject; 
            }
            
            //if (this.SelectedObject != null)
            //{
            //    this.Title = PROPERTY_GRID_NAME+ ": " +this.SelectedObject.GetType().Name;
            //}
            //else
            //{
            //    this.Title = PROPERTY_GRID_NAME;
            //}
        }


    }
}
