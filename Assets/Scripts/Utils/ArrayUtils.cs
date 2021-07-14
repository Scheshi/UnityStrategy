using System;
using System.Collections.Generic;
using System.Linq;

namespace Utils
{
    public static class ArrayUtils
    {
        public static bool Has<T>(this IEnumerable<T> array, out T data, Func<T, bool> func)
        {
            data = array.FirstOrDefault(func);
            return data != null;
        }
    }
}