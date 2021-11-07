using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Enums;

namespace L5Sharp.Types
{
    public struct Bool : IAtomic<bool>
    {
        private bool _value;
        
        public Bool(bool value = default)
        {
            _value = value;
        }

        public string Name => nameof(Bool).ToUpper();
        public string Description => string.Empty;
        public DataTypeFamily Family => DataTypeFamily.None;
        public DataTypeClass Class => DataTypeClass.Atomic;
        public TagDataFormat DataFormat => TagDataFormat.Decorated;
        public IEnumerable<IMember<IDataType>> Members => Enumerable.Empty<IMember<IDataType>>();

        public object Default => default(bool);

        public bool GetValue()
        {
            return _value;
        }

        object IAtomic.GetValue()
        {
            return GetValue();
        }

        public void SetValue(bool value)
        {
            _value = value;
        }

        public void SetValue(object value)
        {
            _value = value switch
            {
                null => throw new ArgumentNullException(nameof(value), "Value can not be null"),
                bool b => b,
                string str => ParseValue(str),
                _ => throw new ArgumentException($"Value not valid type for {Name}")
            };
        }

        public static implicit operator Bool(bool value)
        {
            return new Bool(value);
        }

        public static implicit operator bool(Bool atomic)
        {
            return atomic.GetValue();
        }

        public bool SupportsRadix(Radix radix)
        {
            return radix == Radix.Binary || radix == Radix.Octal || radix == Radix.Decimal || radix == Radix.Hex;
        }

        private static bool ParseValue(string value)
        {
            if (bool.TryParse(value, out var result))
                return result;

            return string.Equals(value, "1", StringComparison.OrdinalIgnoreCase)
                   || string.Equals(value, "True", StringComparison.OrdinalIgnoreCase)
                   || string.Equals(value, "Yes", StringComparison.OrdinalIgnoreCase);
        }
    }
}