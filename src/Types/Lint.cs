using System;
using L5Sharp.Enums;
using L5Sharp.Exceptions;

namespace L5Sharp.Types
{
    public sealed class Lint : IAtomic<long>, IEquatable<Lint>, IComparable<Lint>
    {
        public Lint()
        {
            Value = default;
            Radix = Radix.Decimal;
        }

        public Lint(long value) : this()
        {
            Value = value;
        }

        public Lint(Radix radix) : this()
        {
            SetRadix(radix);
        }

        public string Name => nameof(Lint).ToUpper();
        public string Description => $"RSLogix representation of a {typeof(long)}";
        public DataTypeFamily Family => DataTypeFamily.None;
        public DataTypeClass Class => DataTypeClass.Atomic;
        public TagDataFormat DataFormat => TagDataFormat.Decorated;
        public Radix Radix { get; private set; }
        public long Value { get; private set; }
        public string FormattedValue => Radix.Format(this);
        object IAtomic.Value => Value;

        public void SetValue(long value)
        {
            Value = value;
        }

        public void SetValue(object value)
        {
            Value = value switch
            {
                null => throw new ArgumentNullException(nameof(value), "Value can not be null"),
                Lint a => a,
                long v => v,
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
                   || radix == Radix.Ascii
                   || radix == Radix.DateTime
                   || radix == Radix.DateTimeNs;
        }

        public static implicit operator Lint(long value)
        {
            return new Lint(value);
        }

        public static implicit operator long(Lint atomic)
        {
            return atomic.Value;
        }

        public int CompareTo(Lint other)
        {
            return Value.CompareTo(other.Value);
        }

        public bool Equals(Lint other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name.Equals(other.Name) && Equals(Radix, other.Radix) && Value == other.Value;
        }

        public IDataType Create()
        {
            return new Lint();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Lint)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Radix, Value);
        }
        
        public override string ToString()
        {
            return Name;
        }

        public static bool operator ==(Lint left, Lint right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Lint left, Lint right)
        {
            return !Equals(left, right);
        }

        private long ParseValue(string value)
        {
            if (long.TryParse(value, out var result))
                return result;

            throw new ArgumentException($"Could not parse string '{value}' to {GetType()}");
        }
    }
}