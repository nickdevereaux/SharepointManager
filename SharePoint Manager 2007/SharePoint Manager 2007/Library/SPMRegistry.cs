using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Win32;

namespace Keutmann.SharePointManager.Library
{
    public static class SPMRegistry
    {
        public const string Group = @"Software\SharePoint Manager 2007\";
        public static void SetValue(string subgroup, string name, string val)
        {
            RegistryKey key = Registry.LocalMachine.CreateSubKey(Group + subgroup);
            key.SetValue(name, val);
        }

        public static object GetValue(string subgroup, string name)
        {
            RegistryKey key = Registry.LocalMachine.CreateSubKey(Group + subgroup);
            
            return key.GetValue(name);
        }

        public static RegistryKey GetKey(string subgroup)
        {
            return Registry.LocalMachine.CreateSubKey(Group + subgroup);
        }
    }
}
