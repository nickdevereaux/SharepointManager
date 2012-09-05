using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Keutmann.SharePointManager.Forms;
using SPM2.Framework;
using SPM2.Framework.Validation;

namespace Keutmann.SharePointManager
{
    public class PreflightController 
    {
        ValidationService service = new ValidationService();


        public SplashScreen SplashForm { get; set; }



        public PreflightController(SplashScreen splashScreen)
        {
            SplashForm = splashScreen;
        }

        public bool Validate()
        {
            SplashForm.UpdateProgress("Preflight check...");
            var validators = CompositionProvider.Current.GetExportedValues<BaseValidator>();

            service.AddRange(validators);

            service.ValidatorSucceed += service_UpdateProgress;
            //service.ValidatorFailed += service_ValidatorFailed;
            //service.ValidatorSkippped += service_UpdateProgress;

            service.Run();

            if (service.Errors > 0)
            {
                HandleErrors();
                return false;
            }
            

            return true;
        }

        private void HandleErrors()
        {

            var messages = (from p in service.Validators
                          where p.Result == ValidationResult.Error
                          select p.ErrorString +".\r\n" + p.QuestionString + ".").ToList();



            messages.Add("Application terminated!");
            var message = string.Join("\r\n\r\n", messages);
            MessageBox.Show(message, "SharePoint Manager Preflight check Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        void service_UpdateProgress(BaseValidator validator)
        {
            SplashForm.UpdateProgress(validator.SuccessString);
        }


    }
}
