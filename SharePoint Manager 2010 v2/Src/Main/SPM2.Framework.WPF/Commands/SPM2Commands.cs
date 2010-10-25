using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace SPM2.Framework.WPF.Commands
{
    public class SPM2Commands
    {
        private static RoutedCommand _objectSelected;
        public static RoutedCommand ObjectSelected
        {
            get { return _objectSelected; }
        }

        private static RoutedCommand _editString;
        public static RoutedCommand EditString
        {
            get { return SPM2Commands._editString; }
        }

        static SPM2Commands()
        {
            // Initialize the command.
            _objectSelected = new RoutedCommand("ObjectSelected", typeof(SPM2Commands));
            _editString = new RoutedCommand("EditString", typeof(EditStringCommand));
        }

    }
}
