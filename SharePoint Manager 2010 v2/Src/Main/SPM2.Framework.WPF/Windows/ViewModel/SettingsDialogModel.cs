using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using GalaSoft.MvvmLight;
using System.ComponentModel.Composition;
using SPM2.Framework;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using SPM2.Framework.ComponentModel;
using System.Diagnostics;

namespace SPM2.Framework.WPF.Windows.ViewModel
{
    public class SettingsDialogModel : ViewModelBase, IPartImportsSatisfiedNotification
    {

        public ICommand OKCommand { get; private set; }
        public ICommand CancelCommand {get; private set;}

        public SettingsDialogModel()
        {
            //_cancelCommand = ApplicationCommands.CancelPrint;

            if (IsInDesignMode)
            {
                //this.Name = "InDesign";
            }
            else
            {
                this.OKCommand = new RelayCommand(OKHandler);
                this.CancelCommand = new RelayCommand(CancelHandler);
                // Code runs "for real": Connect to service, etc...
                //this.Name = "In Runtime : Test";
                CompositionProvider.Current.ComposeParts(this);
                
                //_okCommand = new RelayCommand(new Action(OKHandler));
            }
        }


        public void OnImportsSatisfied()
        {
        }

        private void OKHandler()
        {
            try
            {

                Stopwatch watch = new Stopwatch();
                watch.Start();

                Action save = new Action(Save);
                save.BeginInvoke(null, null);


                watch.Stop();
                Trace.WriteLine("Call to SettingsProvider.Current.Save(); = " + watch.ElapsedMilliseconds + " Milliseconds");
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void CancelHandler()
        {

        }

        private void Save()
        {
            SettingsProvider.Current.Save();
        }


    }
}
