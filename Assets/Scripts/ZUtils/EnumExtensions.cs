using System;
using System.Collections.Generic;
using System.Linq;

namespace ZUtils
{
    public static class EnumExtensions
    {
        public static IEnumerable<T> GetValues<T>() where T : Enum {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}