using System;
using L5Sharp.Enums;
using L5Sharp.Exceptions;

namespace L5Sharp.Types
{
    public sealed class Int : IAtomic<short>, IEquatable<Int>, IComparable<Int>
    {
        public Int()
        {
            Value = default;
            Radix = Radix.Decimal;
        }

        public Int(short value) : this()
        {
            Value = value;
        }

        public Int(Radix radix) : this()
        {
            SetRadix(radix);
        }

        public string Name => nameof(Int).ToUpper();
        public string Description => $"RSLogix representation of a {typeof(short)}";
        public DataTypeFamily Family => DataTypeFamily.None;
        public DataTypeClass Class => DataTypeClass.Atomic;
        public TagDataFormat DataFormat => TagDataFormat.Decorated;
        public Radix Radix { get; private set; }
        public short Value { get; private set; }
        public string FormattedValue => Radix.Format(this);

        object IAtomic.Value => Value;

        public void SetValue(short value)
        {
            Value = value;
        }

        public void SetValue(object value)
        {
            Value = value switch
            {
                null => throw new ArgumentNullException(nameof(value), "Value can not be null"),
                short v => v,
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

        public static implicit operator Int(short value)
        {
            return new Int(value);
        }

        public static implicit operator short(Int atomic)
        {
            return atomic.Value;
        }

        public int CompareTo(Int other)
        {
            return Value.CompareTo(other.Value);
        }

        public bool Equals(Int other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Radix, other.Radix) && Value == other.Value;
        }

        public IDataType Instantiate()
        {
            return new Int();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Int)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Radix, Value);
        }

        public static bool operator ==(Int left, Int right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Int left, Int right)
        {
            return !Equals(left, right);
        }

        private short ParseValue(string value)
        {
            if (short.TryParse(value, out var result))
                return result;

            throw new ArgumentException($"Could not parse string '{value}' to {GetType()}");
        }
    }
}