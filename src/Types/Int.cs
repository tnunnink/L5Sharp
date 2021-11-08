using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Enums;

namespace L5Sharp.Types
{
    public struct Int : IAtomic<short>
    {
        private short _value;
        
        public Int(short value = default)
        {
            _value = value;
        }

        public string Name => nameof(Int).ToUpper();
        public string Description => string.Empty;
        public DataTypeFamily Family => DataTypeFamily.None;
        public DataTypeClass Class => DataTypeClass.Atomic;
        public TagDataFormat DataFormat => TagDataFormat.Decorated;

        public object Default => default(short);

        public short Get()
        {
            return _value;
        }

        object IAtomic.Get()
        {
            return Get();
        }

        public void Set(short value)
        {
            _value = value;
        }

        public void Set(object value)
        {
            _value = value switch
            {
                null => throw new ArgumentNullException(nameof(value), "Value can not be null"),
                short v => v,   
                string str => ParseValue(str),
                _ => throw new ArgumentException($"Value not valid type for {Name}")
            };
        }
        
        public bool SupportsRadix(Radix radix)
        {
            return radix == Radix.Binary
                   || radix == Radix.Octal
                   || radix == Radix.Decimal
                   || radix == Radix.Hex
                   || radix == Radix.Ascii;
        }

        private static short ParseValue(string value)
        {
            short.TryParse(value, out var result);
            return result;
        }
    }
}