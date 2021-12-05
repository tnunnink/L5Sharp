using System;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Exceptions;

namespace L5Sharp.Types
{
    /// <summary>
    /// Represents a INT Logix atomic data type.
    /// </summary>
    public sealed class Lint : IAtomic<long>, IEquatable<Lint>, IComparable<Lint>
    {
        /// <summary>
        /// Creates a new default instance of a Lint.
        /// </summary>
        public Lint()
        {
            Name = nameof(Lint).ToUpper();
            Value = default;
        }

        /// <summary>
        /// Creates a new instance of a Lint with the provided value.
        /// </summary>
        /// <param name="value">The value to initialize the type with.</param>
        public Lint(long value) : this()
        {
            Value = value;
        }

        /// <inheritdoc />
        public ComponentName Name { get; }

        /// <inheritdoc />
        public string Description => $"RSLogix representation of a {typeof(long)}";

        /// <inheritdoc />
        public DataTypeFamily Family => DataTypeFamily.None;

        /// <inheritdoc />
        public DataTypeClass Class => DataTypeClass.Atomic;

        /// <inheritdoc />
        public long Value { get; }

        object IAtomic.Value => Value;

        /// <inheritdoc />
        public IAtomic<long> Update(long value)
        {
            return new Lint(value);
        }

        /// <inheritdoc />
        public IAtomic Update(object value)
        {
            return value switch
            {
                null => throw new ArgumentNullException(nameof(value), "Value can not be null"),
                Lint atomic => new Lint(atomic),
                long typed => new Lint(typed),
                string str => new Lint(Parse(str)),
                _ => throw new ArgumentException($"Value type '{value.GetType()}' is not a valid for {GetType()}")
            };
        }

        /// <inheritdoc />
        public IDataType Instantiate()
        {
            return new Lint();
        }
        
        /// <summary>
        /// Converts the provided <see cref="long"/> to a <see cref="Lint"/> value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A <see cref="Lint"/> value.</returns>
        public static implicit operator Lint(long value)
        {
            return new Lint(value);
        }

        /// <summary>
        /// Converts the provided <see cref="Lint"/> to a <see cref="long"/> value.
        /// </summary>
        /// <param name="atomic">The value to convert.</param>
        /// <returns>A <see cref="long"/> type value.</returns>
        public static implicit operator long(Lint atomic)
        {
            return atomic.Value;
        }

        /// <summary>
        /// Converts the provided <see cref="string"/> to a <see cref="Lint"/> value. 
        /// </summary>
        /// <param name="input">The string value to convert.</param>
        /// <returns>
        /// If the string value is able to be parsed, a new instance of a <see cref="Lint"/> with the value
        /// provided. If not, then a default instance value.
        /// </returns>
        public static implicit operator Lint(string input)
        {
            return new Lint(Parse(input));
        }

        /// <inheritdoc />
        public bool Equals(Lint? other)
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
            return obj.GetType() == GetType() && Equals((Lint)obj);
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
        public static bool operator ==(Lint left, Lint right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Determines whether the objects are not equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the objects are not equal, otherwise, false.</returns>
        public static bool operator !=(Lint left, Lint right)
        {
            return !Equals(left, right);
        }

        /// <inheritdoc />
        public int CompareTo(Lint? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            return ReferenceEquals(null, other) ? 1 : Value.CompareTo(other.Value);
        }

        private static long Parse(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            return long.TryParse(value, out var result) ? result : Radix.ParseValue<Lint>(value);
        }
    }
}