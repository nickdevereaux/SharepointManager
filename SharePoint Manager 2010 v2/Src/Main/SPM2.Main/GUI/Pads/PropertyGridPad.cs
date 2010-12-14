using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Forms;
using System.Collections;
using System.ComponentModel.Composition;

using AvalonDock;

using SPM2.Framework;
using SPM2.Framework.WPF;
using SPM2.Framework.WPF.Commands;
using SPM2.Framework.WPF.Components;
using SPM2.SharePoint;
using SPM2.SharePoint.Model;

namespace SPM2.Main.GUI.Pads
{

    [Title("PropertyGrid")]
    [Export(MainWindow.ContentPane_AddInID, typeof(DockableContent))]
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

            this.IsActiveDocumentChanged += new EventHandler(PropertyGridPad_IsActiveDocumentChanged);
            this.PGrid.propertyGrid.PropertyValueChanged += new PropertyValueChangedEventHandler(propertyGrid_PropertyValueChanged);

            this.Content = PGrid;

            Workbench.MainWindow.CommandBindings.AddCommandExecutedHandler(SPM2Commands.ObjectSelected, ObjectSelected_Executed);
            Workbench.MainWindow.CommandBindings.AddCommandExecutedHandler(ApplicationCommands.Save, Save_Executed);
            Workbench.MainWindow.CommandBindings.AddCommandCanExecuteHandler(ApplicationCommands.Save, Save_CanExecute);
        }

        //void PropertyGridPad_Closed(object sender, EventArgs e)
        //{
        //    Workbench.MainWindow.CommandBindings.RemoveCommandExecutedHandler(SPM2Commands.ObjectSelected, ObjectSelected_Executed);
        //    Workbench.MainWindow.CommandBindings.RemoveCommandExecutedHandler(ApplicationCommands.Save, Save_Executed);
        //    Workbench.MainWindow.CommandBindings.RemoveCommandCanExecuteHandler(ApplicationCommands.Save, Save_CanExecute);
        //}
        

        void PropertyGridPad_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
        }

        void PropertyGridPad_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
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
        }

        void PropertyGridPad_IsActiveDocumentChanged(object sender, EventArgs e)
        {
            SetObject();
        }

        private void SetObject()
        {
            if (this.IsWindowVisible)
            {
                ISPNode node = (ISPNode)this.SelectedObject;
                if (node != null && PGrid.propertyGrid.SelectedObject != node.SPObject)
                {
                    PGrid.propertyGrid.SelectedObject = node.SPObject;
                }
            }
        }


    }
}
