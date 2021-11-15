using System;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Exceptions;

namespace L5Sharp.Types
{
    public sealed class Dint : IAtomic<int>, IEquatable<Dint>, IComparable<Dint>
    {
        public Dint()
        {
            Value = default;
            Radix = Radix.Decimal;
        }

        public Dint(int value) : this()
        {
            Value = value;
        }

        public Dint(Radix radix) : this()
        {
            SetRadix(radix);
        }

        public ComponentName Name => nameof(Dint).ToUpper();
        public string Description => $"RSLogix representation of a {typeof(int)}";
        public DataTypeFamily Family => DataTypeFamily.None;
        public DataTypeClass Class => DataTypeClass.Atomic;
        public TagDataFormat DataFormat => TagDataFormat.Decorated;
        public Radix Radix { get; private set; }
        public int Value { get; private set; }
        public string FormattedValue => Radix.Format(this);
        object IAtomic.Value => Value;

        public void SetValue(int value)
        {
            Value = value;
        }

        public void SetValue(object value)
        {
            Value = value switch
            {
                null => throw new ArgumentNullException(nameof(value), "Value can not be null"),
                Dint a => a,
                int v => v,   
                string str => ParseValue(str),
                _ => throw new ArgumentException($"Value type '{value.GetType()}' is not a valid for {GetType()}")
            };
        }

        public void SetRadix(Radix radix)
        {
            if (radix == null)
                throw new ArgumentNullException(nameof(radix));

            if (!SupportsRadix(radix))
                throw new RadixNotSupportedException(radix, this);

            Radix = radix;
        }

        public bool SupportsRadix(Radix radix)
        {
            return radix == Radix.Binary
                   || radix == Radix.Octal
                   || radix == Radix.Decimal
                   || radix == Radix.Hex
                   || radix == Radix.Ascii;
        }

        public static implicit operator Dint(int value)
        {
            return new Dint(value);
        }

        public static implicit operator int(Dint atomic)
        {
            return atomic.Value;
        }

        public int CompareTo(Dint other)
        {
            return Value.CompareTo(other.Value);
        }

        public IDataType Instantiate()
        {
            return new Dint();
        }

        public bool Equals(Dint other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Radix, other.Radix) && Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Dint)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Radix, Value);
        }
        
        public override string ToString()
        {
            return Name;
        }

        public static bool operator ==(Dint left, Dint right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Dint left, Dint right)
        {
            return !Equals(left, right);
        }

        private int ParseValue(string value)
        {
            if (int.TryParse(value, out var result))
                return result;

            throw new ArgumentException($"Could not parse string '{value}' to {GetType()}");
        }
    }
}