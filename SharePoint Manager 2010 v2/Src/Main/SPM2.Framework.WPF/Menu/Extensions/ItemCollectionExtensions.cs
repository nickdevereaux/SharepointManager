using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Collections;

namespace SPM2.Framework.WPF
{
    public static class ItemCollectionExtensions
    {
        public static void AddList(this ItemCollection collection, IEnumerable menuItems)
        {
            foreach (object item in menuItems)
            {
                collection.Add(item);
            }
        }

        public static void LoadChildren(this ItemCollection collection, string addInId)
        {
            collection.AddList(AddInProvider.Current.CreateAttachments<MenuItem>(addInId, null));

            foreach (MenuItem child in collection)
            {
                string childAddInID = child.GetType().GetAddInID();

                child.Items.LoadChildren(childAddInID);
            }
        }

    }
}
