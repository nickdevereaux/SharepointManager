using System;
using System.Collections.Generic;
using System.Linq;

namespace SPM2.Framework
{
    public static class ArrayExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> obj)
        {
            return !(obj != null && obj.Count() > 0);
        }

        /// <summary>
        /// Returns an array of objects and if the array is null an empty Array is then returned.
        /// This will prevent the "Object is not set to an instance of an object" exception.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>An array</returns>
        public static IEnumerable<T> AsSafeEnumable<T>(this IEnumerable<T> arrayObj)
        {
            IEnumerable<T> result = IsNullOrEmpty(arrayObj) ? new T[0] : arrayObj;
            return result;
        }


        public static bool IsAllTrue<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> selector)
        {
            return source.AsSafeEnumable().Where(selector).AsSafeEnumable().Count() == source.AsSafeEnumable().Count();
        }

        public static T FindTypeOf<T>(this object[] objArray)
        {
            T result = default(T);

            foreach (object item in objArray)
            {
                if (item is T)
                {
                    result = (T)item;
                    break;
                }
            }

            return result;
        }


    }
}
