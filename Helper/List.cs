using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public static class List
    {

        public static List<T> CopyList<T>(this List<T> ListIn)
        {
            T[] ArrayOut = new T[ListIn.Count()];
            ListIn.CopyTo(ArrayOut);
            return ArrayOut.ToList();
        }

        /// <summary>
        /// list == null || list.Count ==0 --> true
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this List<T> list)
        {
            if (list?.Any() != true)
                return true;
            return false;
        }

        /// <summary>
        /// uses the .Equals function from the base class to check if this list contains this item
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="o"></param>
        /// <returns></returns>
        public static bool Contains<T>(this List<T> list, T o)
        {
            if (IsNullOrEmpty(list))
                //throw new Exception("Empty list!");
                return false;

            foreach (var t in list)
                if (t.Equals(o))
                    return true;

            return false;
        }

        /// <summary>
        /// Returns the index of the object in the list. If the list doesn't contains the object the result is -1. An empty list returns -2.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="o"></param>
        /// <returns></returns>
        public static int FindIndex<T>(this List<T> list, T o)
        {
            if (IsNullOrEmpty(list))
                return -2;

            for (int i = 0; i < list.Count(); i++)
                if (list[i].Equals(o))
                {
                    return i;
                }

            return -1;
        }

        /// <summary>
        /// uses the .Equals function from the base class to check if this list contains this item. Ifso it gets removed
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="o"></param>
        /// <returns></returns>
        public static void Remove<T>(this List<T> list, T o) 
        {
            if (IsNullOrEmpty(list))
                //throw new Exception("Empty list!");
                return;

            for (int i = 0; i < list.Count(); i++)
                if (list[i].Equals(o))
                {
                    list.RemoveAt(i);
                    return;
                }
        }

    }
}
