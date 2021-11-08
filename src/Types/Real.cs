using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Enums;

namespace L5Sharp.Types
{
    public struct Real : IAtomic<float>
    {
        private float _value;
        
        public Real(float value = default)
        {
            _value = value;
        }

        public string Name => nameof(Real).ToUpper();
        public string Description => string.Empty;
        public DataTypeFamily Family => DataTypeFamily.None;
        public DataTypeClass Class => DataTypeClass.Atomic;
        public TagDataFormat DataFormat => TagDataFormat.Decorated;

        public object Default => default(float);

        public float Get()
        {
            return _value;
        }

        object IAtomic.Get()
        {
            return Get();
        }

        public void Set(float value)
        {
            _value = value;
        }

        public void Set(object value)
        {
            _value = value switch
            {
                null => throw new ArgumentNullException(nameof(value), "Value can not be null"),
                float v => v,   
                string str => ParseValue(str),
                _ => throw new ArgumentException($"Value not valid type for {Name}")
            };
        }
        
        public bool SupportsRadix(Radix radix)
        {
            return radix == Radix.Float || radix == Radix.Exponential;
        }

        private static float ParseValue(string value)
        {
            float.TryParse(value, out var result);
            return result;
        }
    }
}