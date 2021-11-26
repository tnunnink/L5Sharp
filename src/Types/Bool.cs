using System;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Exceptions;

namespace L5Sharp.Types
{
    public sealed class Bool : IAtomic<bool>, IEquatable<Bool>, IComparable<Bool>
    {
        public Bool()
        {
            Name = new ComponentName(nameof(Bool).ToUpper());
            Value = default;
            Radix = Radix.Decimal;
        }

        public Bool(bool value) : this()
        {
            Value = value;
        }

        public Bool(Radix radix) : this()
        {
            SetRadix(radix);
        }

        /// <inheritdoc />
        public ComponentName Name { get; }

        /// <inheritdoc />
        public string Description => $"Logix representation of a {typeof(bool)}";

        /// <inheritdoc />
        public DataTypeFamily Family => DataTypeFamily.None;

        /// <inheritdoc />
        public DataTypeClass Class => DataTypeClass.Atomic;

        /// <inheritdoc />
        public DataFormat Format => DataFormat.Decorated;

        /// <inheritdoc />
        public Radix Radix { get; private set; }

        /// <inheritdoc />
        public bool Value { get; private set; }

        object IAtomic.Value => Value;

        /// <inheritdoc />
        public string FormattedValue => Radix.Format(this);

        /// <inheritdoc />
        public void SetValue(bool value)
        {
            Value = value;
        }

        /// <inheritdoc />
        public void SetValue(object value)
        {
            Value = value switch
            {
                null => throw new ArgumentNullException(nameof(value), "Value can not be null"),
                Bool a => a,
                bool b => b,
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
            return radix == Radix.Binary || radix == Radix.Octal || radix == Radix.Decimal || radix == Radix.Hex;
        }

        /// <inheritdoc />
        public IDataType Instantiate()
        {
            return new Bool();
        }

        public static implicit operator Bool(bool value)
        {
            return new Bool(value);
        }

        public static implicit operator bool(Bool atomic)
        {
            return atomic.Value;
        }

        /// <inheritdoc />
        public bool Equals(Bool other)
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
            return obj.GetType() == GetType() && Equals((Bool)obj);
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

        public static bool operator ==(Bool left, Bool right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Bool left, Bool right)
        {
            return !Equals(left, right);
        }

        /// <inheritdoc />
        public int CompareTo(Bool other)
        {
            return Value.CompareTo(other.Value);
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