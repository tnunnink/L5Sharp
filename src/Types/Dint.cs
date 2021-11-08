using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Enums;

namespace L5Sharp.Types
{
    public struct Dint : IAtomic<int>
    {
        private int _value;
        
        public Dint(int value = default)
        {
            _value = value;
        }

        public string Name => nameof(Dint).ToUpper();
        public string Description => string.Empty;
        public DataTypeFamily Family => DataTypeFamily.None;
        public DataTypeClass Class => DataTypeClass.Atomic;
        public TagDataFormat DataFormat => TagDataFormat.Decorated;

        public object Default => default(int);

        public int Get()
        {
            return _value;
        }

        object IAtomic.Get()
        {
            return Get();
        }

        public void Set(int value)
        {
            _value = value;
        }

        public void Set(object value)
        {
            _value = value switch
            {
                null => throw new ArgumentNullException(nameof(value), "Value can not be null"),
                int v => v,   
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

        private static int ParseValue(string value)
        {
            int.TryParse(value, out var result);
            return result;
        }
    }
}