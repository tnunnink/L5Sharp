using System;
using L5Sharp.Enums;
using L5Sharp.Exceptions;

namespace L5Sharp.Types
{
    public sealed class Sint : IAtomic<byte>, IEquatable<Sint>, IComparable<Sint>
    {
        public Sint()
        {
            Value = default;
            Radix = Radix.Decimal;
        }

        public Sint(byte value) : this()
        {
            Value = value;
        }

        public Sint(Radix radix) : this()
        {
            SetRadix(radix);
        }

        public string Name => nameof(Sint).ToUpper();
        public string Description => $"RSLogix representation of a {typeof(byte)}";
        public DataTypeFamily Family => DataTypeFamily.None;
        public DataTypeClass Class => DataTypeClass.Atomic;
        public TagDataFormat DataFormat => TagDataFormat.Decorated;
        public Radix Radix { get; private set; }
        public byte Value { get; private set; }
        public string FormattedValue => Radix.Format(this);
        object IAtomic.Value => Value;
        
        public void SetValue(byte value)
        {
            Value = value;
        }

        public void SetValue(object value)
        {
            Value = value switch
            {
                null => throw new ArgumentNullException(nameof(value), "Value can not be null"),
                byte b => b,
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
        
        public static implicit operator Sint(byte value)
        {
            return new Sint(value);
        }

        public static implicit operator byte(Sint atomic)
        {
            return atomic.Value;
        }

        public int CompareTo(Sint other)
        {
            return Value.CompareTo(other.Value);
        }

        public bool Equals(Sint other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Radix, other.Radix) && Value == other.Value;
        }

        public IDataType Instantiate()
        {
            return new Sint();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Sint)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Radix, Value);
        }

        public static bool operator ==(Sint left, Sint right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Sint left, Sint right)
        {
            return !Equals(left, right);
        }

        private byte ParseValue(string value)
        {
            if (byte.TryParse(value, out var result))
                return result;

            throw new ArgumentException($"Could not parse string '{value}' to {GetType()}");
        }
    }
}