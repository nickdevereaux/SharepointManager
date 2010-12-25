// From: http://sharepointinstaller.codeplex.com/

using System;
using System.Security;
using Microsoft.Win32;
using SPM2.Framework.Validation;

namespace SPM2.SharePoint.Validation
{
    public class SPFInstalledValidator : BaseValidator, IValidator
    {
        public const String SpfPath = @"SOFTWARE\Microsoft\Shared Tools\Web Server Extensions\14.0";

        public SPFInstalledValidator(String id) : base(id)
        {
        }

        protected override ValidationResult Validate()
        {        
            try
            {
                var key = Registry.LocalMachine.OpenSubKey(SpfPath);
                if (key != null)
                {
                    var value = key.GetValue("SharePoint");
                    if (value != null && value.Equals("Installed"))
                    {
                        return ValidationResult.Success;
                    }
                }
            }
            catch (SecurityException ex)
            {
                throw new ValidatorExcpetion(String.Format("Registry access denied: {0}", SpfPath), ex);
            }
            return ValidationResult.Error;
        }
    }
}
