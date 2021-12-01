using System;
using L5Sharp.Core;
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

        /// <inheritdoc />
        public ComponentName Name => nameof(Sint).ToUpper();

        /// <inheritdoc />
        public string Description => $"RSLogix representation of a {typeof(byte)}";

        /// <inheritdoc />
        public DataTypeFamily Family => DataTypeFamily.None;

        /// <inheritdoc />
        public DataTypeClass Class => DataTypeClass.Atomic;

        /// <inheritdoc />
        public DataFormat Format => DataFormat.Decorated;

        /// <inheritdoc />
        public Radix Radix { get; private set; }

        /// <inheritdoc />
        public byte Value { get; private set; }

        /// <inheritdoc />
        public string FormattedValue => Radix.Format(this);
        object IAtomic.Value => Value;

        /// <inheritdoc />
        public void SetValue(byte value)
        {
            Value = value;
        }

        /// <inheritdoc />
        public void SetValue(object value)
        {
            Value = value switch
            {
                null => throw new ArgumentNullException(nameof(value), "Value can not be null"),
                Sint a => a,
                byte b => b,
                string str => ParseValue(str),
                _ => throw new ArgumentException($"Value type '{value.GetType()}' is not a valid for {GetType()}")
            };
        }

        /// <inheritdoc />
        public void SetRadix(Radix radix)
        {
            if (radix == null)
                throw new ArgumentNullException(nameof(radix));

            if (!SupportsRadix(radix))
                throw new RadixNotSupportedException(radix, this);

            Radix = radix;
        }

        /// <inheritdoc />
        public bool SupportsRadix(Radix radix)
        {
            return radix == Radix.Binary
                   || radix == Radix.Octal
                   || radix == Radix.Decimal
                   || radix == Radix.Hex
                   || radix == Radix.Ascii;
        }

        /// <inheritdoc />
        public IDataType Instantiate()
        {
            return new Sint();
        }

        public static implicit operator Sint(byte value)
        {
            return new Sint(value);
        }

        public static implicit operator byte(Sint atomic)
        {
            return atomic.Value;
        }

        /// <inheritdoc />
        public int CompareTo(Sint other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <inheritdoc />
        public bool Equals(Sint other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Radix, other.Radix) && Value == other.Value;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Sint)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return HashCode.Combine(Radix, Value);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Name;
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