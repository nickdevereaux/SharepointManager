using System;
using System.IO;
using System.Security;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;


namespace Keutmann.SharePointManager.Library
{
    public sealed class SPMPaths
    {

        private string _SPDirectory = string.Empty;
        private string SPDirectory
        {
            get
            {
                if (_SPDirectory.Length == 0)
                {
                    _SPDirectory = GetSharePointDirectory();
                }
                return _SPDirectory;
            }
        }

        private string _SPImageDirectory = string.Empty;
        private string SPImageDirectory
        {
            get
            {
                if (_SPImageDirectory.Length == 0)
                {
                    _SPImageDirectory = Path.Combine(SharePointDirectory, @"TEMPLATE\IMAGES\");
                }
                return _SPImageDirectory;
            }
        }

        private string _SPFeatureDirectory = string.Empty;
        private string SPFeatureDirectory
        {
            get
            {
                if (_SPFeatureDirectory.Length == 0)
                {
                    _SPFeatureDirectory = Path.Combine(SharePointDirectory, @"TEMPLATE\FEATURE\");
                }
                return _SPFeatureDirectory;
            }
        }

        private string _SPTemplateDirectory = string.Empty;
        private string SPTemplateDirectory
        {
            get
            {
                if (_SPTemplateDirectory.Length == 0)
                {
                    _SPTemplateDirectory = Path.Combine(SharePointDirectory, @"TEMPLATE");
                }
                return _SPTemplateDirectory;
            }
        }

        private string GetSharePointDirectory()
        {
            string setupDir = Microsoft.SharePoint.Utilities.SPUtility.GetGenericSetupPath("");
            return setupDir;
        }

        #region Static methods

        public static string SharePointDirectory
        {
            get
            {
                return Instance.SPDirectory;
            }
        }

        public static string FeatureDirectory
        {
            get
            {
                return Instance.SPFeatureDirectory;
            }
        }

        public static string ImageDirectory
        {
            get
            {
                return Instance.SPImageDirectory;
            }
        }

        public static string TemplateDirectory
        {
            get
            {
                return Instance.SPTemplateDirectory;
            }
        }

        #endregion 

        #region Singleton

        SPMPaths()
        {
        }

        public static SPMPaths Instance
        {
            get
            {
                return Nested.instance;
            }
        }

        class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested()
            {
            }

            internal static readonly SPMPaths instance = new SPMPaths();
        }
        #endregion
    }

}
