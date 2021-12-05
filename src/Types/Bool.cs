using System;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Types
{
    /// <summary>
    /// Represents a BOOL Logix atomic data type.
    /// </summary>
    public sealed class Bool : IAtomic<bool>, IEquatable<Bool>, IComparable<Bool>
    {
        /// <summary>
        /// Creates a new default instance of a Bool.
        /// </summary>
        public Bool()
        {
            Name = new ComponentName(nameof(Bool).ToUpper());
            Value = default;
        }

        /// <summary>
        /// Creates a new instance of a Bool with the provided value.
        /// </summary>
        /// <param name="value">The value to initialize the type with.</param>
        public Bool(bool value) : this()
        {
            Value = value;
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
        public bool Value { get; }

        object IAtomic.Value => Value;

        /// <inheritdoc />
        public IAtomic<bool> Update(bool value)
        {
            return new Bool(value);
        }

        /// <inheritdoc />
        public IAtomic Update(object value)
        {
            return value switch
            {
                null => throw new ArgumentNullException(nameof(value), "Value can not be null"),
                Bool atomic => new Bool(atomic),
                bool typed => new Bool(typed),   
                string str => new Bool(Parse(str)),
                _ => throw new ArgumentException($"Value type '{value.GetType()}' is not a valid for {GetType()}")
            };
        }

        /// <inheritdoc />
        public IDataType Instantiate()
        {
            return new Bool();
        }

        /// <summary>
        /// Converts the provided <see cref="bool"/> to a <see cref="Bool"/> value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A <see cref="Bool"/> value.</returns>
        public static implicit operator Bool(bool value)
        {
            return new Bool(value);
        }

        /// <summary>
        /// Converts the provided <see cref="Bool"/> to a <see cref="bool"/> value.
        /// </summary>
        /// <param name="atomic">The value to convert.</param>
        /// <returns>A <see cref="bool"/> type value.</returns>
        public static implicit operator bool(Bool atomic)
        {
            return atomic.Value;
        }

        /// <summary>
        /// Converts the provided <see cref="string"/> to a <see cref="Bool"/> value. 
        /// </summary>
        /// <param name="input">The string value to convert.</param>
        /// <returns>
        /// If the string value is able to be parsed, a new instance of a <see cref="Bool"/> with the value
        /// provided. If not, then a default instance value.
        /// </returns>
        public static implicit operator Bool(string input)
        {
            return new Bool(Parse(input));
        }

        /// <inheritdoc />
        public bool Equals(Bool? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value == other.Value;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            return ReferenceEquals(this, obj) || obj is Bool other && Equals(other);
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
        public static bool operator ==(Bool? left, Bool? right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Determines whether the objects are not equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the objects are not equal, otherwise, false.</returns>
        public static bool operator !=(Bool? left, Bool? right)
        {
            return !Equals(left, right);
        }

        /// <inheritdoc />
        public int CompareTo(Bool other)
        {
            return Value.CompareTo(other.Value);
        }

        private static bool Parse(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            if (bool.TryParse(value, out var result))
                return result;

            if (string.Equals(value, "1", StringComparison.OrdinalIgnoreCase)
                || string.Equals(value, "true", StringComparison.OrdinalIgnoreCase)
                || string.Equals(value, "yes", StringComparison.OrdinalIgnoreCase))
                return true;

            return Radix.ParseValue<Bool>(value);
        }
    }
}