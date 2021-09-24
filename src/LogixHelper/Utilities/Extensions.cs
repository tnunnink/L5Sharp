using System;
using System.Xml.Linq;

namespace LogixHelper.Utilities
{
    public static class Extensions
    {
        public static bool Contains(this string source, string value, StringComparison comparison)
        {
            return source?.IndexOf(value, comparison) >= 0;
        }
        
        /*public static T Materialize<T>(this T obj, XElement element) where T : new()
        {
            return new T(element);
        }*/
    }
}