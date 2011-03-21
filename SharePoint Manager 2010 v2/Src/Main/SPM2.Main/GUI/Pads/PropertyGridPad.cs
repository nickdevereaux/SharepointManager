using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Input;
using System.Collections;
using System.ComponentModel.Composition;

using AvalonDock;

using SPM2.Framework;
using SPM2.Framework.WPF;
using SPM2.Framework.WPF.Commands;
using SPM2.Framework.WPF.Components;
using SPM2.SharePoint;
using SPM2.SharePoint.Model;
using System.Windows.Threading;
using System.Diagnostics;
using System.Windows;
using System.Threading;


namespace SPM2.Main.GUI.Pads
{

    [Title("PropertyGrid")]
    [Export(MainWindow.ContentPane_AddInID, typeof(DockableContent))]
    [ExportMetadata("ID", "SPM2.Main.GUI.Pads.PropertyGridPad")]
    public class PropertyGridPad : AbstractPadWindow
    {
        private const string PROPERTY_GRID_NAME = "PropertyGrid";

        public PropertyGridControl PGrid = new PropertyGridControl();

        public Dictionary<object, Hashtable> ChangedPropertyItems = new Dictionary<object, Hashtable>();
        public bool ValueChanged { get; set; }

        private object SelectedObject { get; set; }
        private bool GridUpdated { get; set; }


        private object PreviousSelectedObject { get; set; }
        
        /// <summary>
        /// Save the time of the Object Selected event.
        /// Used to deside if to update the window.
        /// </summary>
        //private DateTime SelectedObjectTimeStamp { get; set; }

        //private DispatcherTimer UpdateTimer { get;set;}


        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            this.Title = PROPERTY_GRID_NAME;

            this.IsActiveDocumentChanged += new EventHandler(PropertyGridPad_IsActiveDocumentChanged);
            this.PGrid.propertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(propertyGrid_PropertyValueChanged);

            this.Content = PGrid;

            //this.UpdateTimer = new DispatcherTimer();
            //this.UpdateTimer.Interval = TimeSpan.FromMilliseconds(100);
            //this.UpdateTimer.Tick += new EventHandler(updateTimer_Tick);
            //this.UpdateTimer.Start();

            

            Application.Current.MainWindow.CommandBindings.AddCommandExecutedHandler(SPM2Commands.ObjectSelected, ObjectSelected_Executed);
            Application.Current.MainWindow.CommandBindings.AddCommandExecutedHandler(ApplicationCommands.Save, Save_Executed);
            Application.Current.MainWindow.CommandBindings.AddCommandCanExecuteHandler(ApplicationCommands.Save, Save_CanExecute);
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

        DispatcherOperation operation = null;

        void ObjectSelected_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.PreviousSelectedObject = this.SelectedObject;
            this.SelectedObject = e.Parameter;

            InvokeSetObject();
            //if (!this.UpdateTimer.IsEnabled)
            //{
            //    Dispatcher.BeginInvoke(new Action(SetObject), DispatcherPriority.Normal);
            //    this.UpdateTimer.Start();
            //}
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


        void propertyGrid_PropertyValueChanged(object s, System.Windows.Forms.PropertyValueChangedEventArgs e)
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

        //void updateTimer_Tick(object sender, EventArgs e)
        //{
        //    if (this.IsWindowVisible)
        //    {
        //        if (this.SelectedObject != null && this.SelectedObject == this.PreviousSelectedObject)
        //        {
        //            SetObject();
        //            this.UpdateTimer.Stop();
        //        }
        //        else
        //        {
        //            this.PreviousSelectedObject = this.SelectedObject;
        //        }
        //    }
        //}

        private void InvokeSetObject()
        {
            if (operation == null || operation.Status == DispatcherOperationStatus.Completed || operation.Status == DispatcherOperationStatus.Aborted)
            {
                operation = Dispatcher.BeginInvoke(new Action(SetObject), DispatcherPriority.Normal);
            }

            
        }

        private void SetObject()
        {
            if (this.IsWindowVisible)
            {
                ISPNode node = (ISPNode)this.SelectedObject;
                if (node != null && PGrid.propertyGrid.SelectedObject != node.SPObject)
                {
#if DEBUG
                    Stopwatch watch = new Stopwatch();
                    watch.Start();
#endif  
        
                    Mouse.OverrideCursor = Cursors.Wait;

                    PGrid.propertyGrid.SelectedObject = node.SPObject;

                    Mouse.OverrideCursor = Cursors.Arrow;

#if DEBUG
                    watch.Stop();
                    Trace.WriteLine(String.Format("PropertyGrid load: Type:{0} - Time {1} milliseconds.", node.SPObjectType.Name, watch.ElapsedMilliseconds));
#endif

                }
            }
        }

        //private void SetPropertyGrid(System.Windows.Forms.PropertyGrid grid, object obj)
        //{
        //    grid.SelectedObject = obj;

        //}
    }
}
