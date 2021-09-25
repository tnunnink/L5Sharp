using System;
using System.Linq;
using System.Text.RegularExpressions;
using L5Sharp.Exceptions;
using L5Sharp.Primitives;

namespace L5Sharp.Utilities
{
    public static class Validate
    {
        public static void TagName(string name)
        {
            if (!Regex.IsMatch(name, @"^[a-zA-Z_][a-zA-Z0-9_]{0,39}$"))
                throw new InvalidTagNameException();
        }
        
        public static void DataTypeName(string name)
        {
            var predefined = DataType.Predefined;
            if (predefined.Any(x => x.Name == name))
                throw new InvalidOperationException();
        }
    }
}