using System;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Exceptions;

namespace L5Sharp.Types
{
    /// <summary>
    /// Represents a INT Logix atomic data type.
    /// </summary>
    public sealed class Real : IAtomic<float>, IEquatable<Real>, IComparable<Real>
    {
        /// <summary>
        /// Creates a new default instance of a Real.
        /// </summary>
        public Real()
        {
            Name = nameof(Real).ToUpper();
            Value = default;
        }

        /// <summary>
        /// Creates a new instance of a Real with the provided value.
        /// </summary>
        /// <param name="value">The value to initialize the type with.</param>
        public Real(float value) : this()
        {
            Value = value;
        }

        /// <inheritdoc />
        public ComponentName Name { get; }

        /// <inheritdoc />
        public string Description => $"RSLogix representation of a {typeof(float)}";

        /// <inheritdoc />
        public DataTypeFamily Family => DataTypeFamily.None;

        /// <inheritdoc />
        public DataTypeClass Class => DataTypeClass.Atomic;

        /// <inheritdoc />
        public float Value { get; }

        object IAtomic.Value => Value;

        /// <inheritdoc />
        public IAtomic<float> Update(float value)
        {
            return new Real(value);
        }

        /// <inheritdoc />
        public IAtomic Update(object value)
        {
            return value switch
            {
                null => throw new ArgumentNullException(nameof(value), "Value can not be null"),
                Real atomic => new Real(atomic),
                float typed => new Real(typed),
                string str => new Real(Parse(str)),
                _ => throw new ArgumentException($"Value type '{value.GetType()}' is not a valid for {GetType()}")
            };
        }

        /// <inheritdoc />
        public IDataType Instantiate()
        {
            return new Real();
        }
        
        /// <summary>
        /// Converts the provided <see cref="float"/> to a <see cref="Real"/> value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A <see cref="Real"/> value.</returns>
        public static implicit operator Real(float value)
        {
            return new Real(value);
        }

        /// <summary>
        /// Converts the provided <see cref="Real"/> to a <see cref="float"/> value.
        /// </summary>
        /// <param name="atomic">The value to convert.</param>
        /// <returns>A <see cref="float"/> type value.</returns>
        public static implicit operator float(Real atomic)
        {
            return atomic.Value;
        }

        /// <summary>
        /// Converts the provided <see cref="string"/> to a <see cref="Real"/> value. 
        /// </summary>
        /// <param name="input">The string value to convert.</param>
        /// <returns>
        /// If the string value is able to be parsed, a new instance of a <see cref="Real"/> with the value
        /// provided. If not, then a default instance value.
        /// </returns>
        public static implicit operator Real(string input)
        {
            return new Real(Parse(input));
        }

        /// <inheritdoc />
        public bool Equals(Real? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value == other.Value;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Real)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        /// <summary>
        /// Determines whether the objects are equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the objects are equal, otherwise, false.</returns>
        public static bool operator ==(Real left, Real right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Determines whether the objects are not equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the objects are not equal, otherwise, false.</returns>
        public static bool operator !=(Real left, Real right)
        {
            return !Equals(left, right);
        }

        /// <inheritdoc />
        public int CompareTo(Real? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            return ReferenceEquals(null, other) ? 1 : Value.CompareTo(other.Value);
        }

        private static float Parse(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            return float.TryParse(value, out var result) ? result : Radix.ParseValue<Real>(value);
        }
    }
}