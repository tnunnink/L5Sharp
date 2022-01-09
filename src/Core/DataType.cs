using System;
using System.Collections.Generic;
using L5Sharp.Enums;
using L5Sharp.Types;

namespace L5Sharp.Core
{
    public static class DataType
    {
        private static readonly Dictionary<string, Func<IAtomicType>> Atomic = new(StringComparer.OrdinalIgnoreCase)
        {
            {nameof(Bool), () => new Bool()}
        };

        private static readonly Dictionary<string, IComplexType> Predefined = new()
        {
        };

        public static IAtomicType ParseValue(string input)
        {
            return Radix.ParseValue(input);
        }

        public static IAtomicType<T> ParseValue<T>(string input) where T : struct
        {
            throw new NotImplementedException();
        }

        public static IDataType FromName(string name)
        {
            if (Atomic.ContainsKey(name))
                return Atomic[name].Invoke();

            if (Predefined.ContainsKey(name))
                return Atomic[name].Invoke();

            throw new InvalidOperationException();
        }
    }
}