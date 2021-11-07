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
        public IEnumerable<IMember<IDataType>> Members => Enumerable.Empty<IMember<IDataType>>();

        public object Default => default(int);

        public int GetValue()
        {
            return _value;
        }

        object IAtomic.GetValue()
        {
            return GetValue();
        }

        public void SetValue(int value)
        {
            _value = value;
        }

        public void SetValue(object value)
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