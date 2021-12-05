using System;
using System.Collections;
using System.Collections.Generic;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Types
{
    /// <summary>
    /// Represents a SINT Logix atomic data type.
    /// </summary>
    public sealed class Sint : IAtomic<byte>, IEquatable<Sint>, IComparable<Sint>
    {
        /// <summary>
        /// Creates a new default instance of a Sint.
        /// </summary>
        public Sint()
        {
            Name = nameof(Sint).ToUpper();
            Value = default;
        }

        /// <summary>
        /// Creates a new instance of a Sint with the provided value.
        /// </summary>
        /// <param name="value">The value to initialize the type with.</param>
        public Sint(byte value) : this()
        {
            Value = value;
        }

        /// <inheritdoc />
        public ComponentName Name { get; }

        /// <inheritdoc />
        public string Description => $"RSLogix representation of a {typeof(byte)}";

        /// <inheritdoc />
        public DataTypeFamily Family => DataTypeFamily.None;

        /// <inheritdoc />
        public DataTypeClass Class => DataTypeClass.Atomic;

        /// <inheritdoc />
        public byte Value { get; }

        object IAtomic.Value => Value;

        /// <inheritdoc />
        public IAtomic<byte> Update(byte value)
        {
            return new Sint(value);
        }

        /// <inheritdoc />
        public IAtomic Update(object value)
        {
            return value switch
            {
                null => throw new ArgumentNullException(nameof(value), "Value can not be null"),
                Sint atomic => new Sint(atomic),
                byte typed => new Sint(typed),
                string str => new Sint(Parse(str)),
                _ => throw new ArgumentException($"Value type '{value.GetType()}' is not a valid for {GetType()}")
            };
        }

        /// <inheritdoc />
        public IDataType Instantiate()
        {
            return new Sint();
        }

        /// <summary>
        /// Converts the provided <see cref="byte"/> to a <see cref="Sint"/> value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A <see cref="Sint"/> value.</returns>
        public static implicit operator Sint(byte value)
        {
            return new Sint(value);
        }

        /// <summary>
        /// Converts the provided <see cref="Sint"/> to a <see cref="byte"/> value.
        /// </summary>
        /// <param name="atomic">The value to convert.</param>
        /// <returns>A <see cref="byte"/> type value.</returns>
        public static implicit operator byte(Sint atomic)
        {
            return atomic.Value;
        }

        /// <summary>
        /// Converts the provided <see cref="string"/> to a <see cref="Sint"/> value. 
        /// </summary>
        /// <param name="input">The string value to convert.</param>
        /// <returns>
        /// If the string value is able to be parsed, a new instance of a <see cref="Sint"/> with the value
        /// provided. If not, then a default instance value.
        /// </returns>
        public static implicit operator Sint(string input)
        {
            return new Sint(Parse(input));
        }

        /// <inheritdoc />
        public bool Equals(Sint? other)
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
            return obj.GetType() == GetType() && Equals((Sint)obj);
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
        public static bool operator ==(Sint left, Sint right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Determines whether the objects are not equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the objects are not equal, otherwise, false.</returns>
        public static bool operator !=(Sint left, Sint right)
        {
            return !Equals(left, right);
        }

        /// <inheritdoc />
        public int CompareTo(Sint? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            return ReferenceEquals(null, other) ? 1 : Value.CompareTo(other.Value);
        }

        private static byte Parse(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            if (byte.TryParse(value, out var result))
                return result;

            return Radix.ParseValue<Sint>(value);

            //throw new ArgumentException($"Could not parse string '{value}' to {GetType()}");
        }
    }
}