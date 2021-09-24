using System;
using System.Linq;
using System.Text.RegularExpressions;
using LogixHelper.Exceptions;
using LogixHelper.Primitives;

namespace LogixHelper.Utilities
{
    public static class Validate
    {
        public static void TagName(string name)
        {
            if (!Regex.IsMatch(name, @"^[a-zA-Z_][a-zA-Z0-9_]{0,39}$"))
                throw new InvalidNameException();
        }
        
        public static void DataTypeName(string name)
        {
            var predefined = DataType.Predefined;
            if (predefined.Any(x => x.Name == name))
                throw new InvalidOperationException();
        }
    }
}