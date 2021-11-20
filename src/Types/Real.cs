using System;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Exceptions;

namespace L5Sharp.Types
{
    public sealed class Real : IAtomic<float>, IEquatable<Real>, IComparable<Real>
    {
        public Real()
        {
            Value = default;
            Radix = Radix.Float;
        }

        public Real(float value) : this()
        {
            Value = value;
        }

        public Real(Radix radix) : this()
        {
            SetRadix(radix);
        }

        /// <inheritdoc />
        public ComponentName Name => nameof(Real).ToUpper();
        
        public string Description => $"RSLogix representation of a {typeof(float)}";
        public DataTypeFamily Family => DataTypeFamily.None;
        public DataTypeClass Class => DataTypeClass.Atomic;
        public TagDataFormat DataFormat => TagDataFormat.Decorated;
        public Radix Radix { get; private set; }
        public float Value { get; private set; }
        public string FormattedValue => Radix.Format(this);
        object IAtomic.Value => Value;

        public void SetValue(float value)
        {
            Value = value;
        }

        public void SetValue(object value)
        {
            Value = value switch
            {
                null => throw new ArgumentNullException(nameof(value), "Value can not be null"),
                Real a => a,
                float v => v,
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
            return radix == Radix.Float || radix == Radix.Exponential;
        }

        public static implicit operator Real(float value)
        {
            return new Real(value);
        }

        public static implicit operator float(Real atomic)
        {
            return atomic.Value;
        }

        public int CompareTo(Real other)
        {
            return Value.CompareTo(other.Value);
        }

        public bool Equals(Real other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Radix, other.Radix) && Value.Equals(other.Value);
        }

        public IDataType Instantiate()
        {
            return new Real();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Real)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Radix, Value);
        }
        
        public override string ToString()
        {
            return Name;
        }

        public static bool operator ==(Real left, Real right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Real left, Real right)
        {
            return !Equals(left, right);
        }

        private float ParseValue(string value)
        {
            if (float.TryParse(value, out var result))
                return result;

            throw new ArgumentException($"Could not parse string '{value}' to {GetType()}");
        }
    }
}