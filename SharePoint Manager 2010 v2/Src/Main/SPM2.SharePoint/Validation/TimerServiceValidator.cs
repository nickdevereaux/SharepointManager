// From: http://sharepointinstaller.codeplex.com/

using System;
using System.ComponentModel;
using System.ServiceProcess;
using System.Diagnostics;
using Microsoft.SharePoint.Administration;
using SPM2.Framework;
using SPM2.Framework.Validation;

namespace SPM2.SharePoint.Validation
{
    public class TimerServiceValidator : BaseValidator, IValidator
    {
        public TimerServiceValidator(String id) : base(id)
        {
        }

        protected override ValidationResult Validate()
        {
            try
            {
                if (SPFarm.Local == null)
                {
                    ErrorString = "Insufficient rights to access configuration database.";
                    return ValidationResult.Error;
                }

                foreach (var server in SPFarm.Local.Servers)
                {
                    foreach (var service in server.ServiceInstances)
                    {
                        if (service.TypeName == "Microsoft SharePoint Foundation Timer")
                        {
                            try
                            {
                                var serviceController = new ServiceController("SPTimerV4", server.Name);
                                if (serviceController.Status != ServiceControllerStatus.Running)
                                {
                                    Trace.WriteLine(String.Format("Microsoft SharePoint Foundation Timer is not running on {0}", server.Name));
                                    return ValidationResult.Error;
                                }

                            }
                            catch (UnauthorizedAccessException ex)
                            {
                                Trace.Fail(ex.Message, ex.StackTrace);
                                QuestionString = String.Format("Failed to access Microsoft SharePoint Foundation Timer on {0}.", server.Name);
                                return ValidationResult.Inconclusive;
                            }
                        }
                    }
                }

                return ValidationResult.Success;
                
                //
                // LFN 2009-06-21: Do not restart the time service anymore. First it does
                // not always work with Windows Server 2008 where it seems a local 
                // admin may not necessarily be allowed to start and stop the service.
                // Secondly, the timer service has become more stable with WSS SP1 and SP2.
                //
                /*TimeSpan timeout = new TimeSpan(0, 0, 60);
                ServiceController sc = new ServiceController("SPTimerV3");
                if (sc.Status == ServiceControllerStatus.Running)
                {
                  sc.Stop();
                  sc.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
                }

                sc.Start();
                sc.WaitForStatus(ServiceControllerStatus.Running, timeout);

                return SystemCheckResult.Success;*/
            }
            catch (System.ServiceProcess.TimeoutException ex)
            {
                Trace.TraceError(ex.GetMessages());
            }
            catch (Win32Exception ex)
            {
                Trace.TraceError(ex.GetMessages());
            }
            catch (InvalidOperationException ex)
            {
                Trace.TraceError(ex.GetMessages());
            }

            return ValidationResult.Inconclusive; 
        }

        protected override bool CanRun
        {
            get
            {
                return new SPFInstalledValidator(String.Empty).RunValidator() == ValidationResult.Success;
            }
        }
    }
}
