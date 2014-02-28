using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWGP3Squared.Helper
{
    static class Methods
    {
        /// <summary>
        /// returns T[] from object[] by looping through object[] and finding if object elements are of type T[].
        /// This only works currently if element of objArray are arrays can be edited for non array elements easily but not needed currently
        /// </summary>
        /// <param name="objArray"> Object[] you want to get T from</param>
        ///</param>
        //convert to searching for non array elements by getting the type of T
        public static T[] GetObjectArrayOf<T>(Object[] objArray)
        {
            List<T> tList = new List<T>();
            if (objArray != null)
            {
                foreach (Object obj in objArray)
                {
                    if (obj.GetType() == tList.ToArray().GetType())
                    {
                        tList.AddRange(obj as T[]);
                    }
                }
            }
            else
            {
                throw new ArgumentNullException();
            }
            return tList.ToArray();
        }

        public static T GetObjectOf<T>(Object[] objArray)
        {
            foreach (Object obj in objArray)
            {
                Type x = obj.GetType();
                Type y = typeof(T);
                if (obj.GetType() == typeof(T))
                {
                    return (T)obj;
                }
            }
            return default(T);
        }
    }
}
