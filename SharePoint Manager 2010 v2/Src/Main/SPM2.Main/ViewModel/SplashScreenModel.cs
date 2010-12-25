using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using System.ComponentModel.Composition;
using SPM2.Framework;
using SPM2.Framework.Validation;

namespace SPM2.Main.ViewModel
{
    public class SplashScreenModel : ViewModelBase, IPartImportsSatisfiedNotification
    {

        private ValidationService _validatorService = new ValidationService();
        public ValidationService ValidationService
        {
            get { return _validatorService; }
            set { _validatorService = value; }
        }


        [ImportMany(typeof(IValidator))]
        private List<BaseValidator> Validators { get; set; }


        public SplashScreenModel()
        {
            if (IsInDesignMode)
            {
                //this.Name = "InDesign";
            }
            else
            {
                // Code runs "for real": Connect to service, etc...
                //this.Name = "In Runtime : Test";
                CompositionProvider.Current.ComposeParts(this);
            }
        }


        public void OnImportsSatisfied()
        {
            foreach (var item in this.Validators)
            {
                this.ValidationService.Add(item);
            }
        }
    }
}
