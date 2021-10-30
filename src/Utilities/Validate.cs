using System;
using System.Linq;
using System.Text.RegularExpressions;
using L5Sharp.Abstractions;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Exceptions;

namespace L5Sharp.Utilities
{
    public static class Validate
    {
        public static void Name(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Name property can not be null or empty");

            if (!Regex.IsMatch(name, @"^[a-zA-Z_][a-zA-Z0-9_]{0,39}$"))
                throw new ComponentNameInvalidException(name);
        }
        
        public static void DataTypeName(string name)
        {
            if (Predefined.Types.Any(t => t.Name == name))
                Throw.PredefinedCollisionException(name);
        }

        public static void Radix(Radix radix, IDataType type)
        {
            if (radix == null) throw new ArgumentNullException(nameof(radix), "Radix property can not be null");

            if (!radix.IsValidForType(type))
                throw new RadixNotSupportedException(radix, type);
        }
    }
}