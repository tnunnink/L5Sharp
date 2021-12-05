using System;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Types
{
    /// <summary>
    /// Represents a DINT Logix atomic data type.
    /// </summary>
    public sealed class Dint : IAtomic<int>, IEquatable<Dint>, IComparable<Dint>
    {
       /// <summary>
        /// Creates a new default instance of a Dint.
        /// </summary>
        public Dint()
        {
            Name = nameof(Dint).ToUpper();
            Value = default;
        }

        /// <summary>
        /// Creates a new instance of a Dint with the provided value.
        /// </summary>
        /// <param name="value">The value to initialize the type with.</param>
        public Dint(int value) : this()
        {
            Value = value;
        }

        /// <inheritdoc />
        public ComponentName Name { get; }

        /// <inheritdoc />
        public string Description => $"RSLogix representation of a {typeof(int)}";

        /// <inheritdoc />
        public DataTypeFamily Family => DataTypeFamily.None;

        /// <inheritdoc />
        public DataTypeClass Class => DataTypeClass.Atomic;


        /// <inheritdoc />
        public int Value { get; }

        object IAtomic.Value => Value;

        /// <inheritdoc />
        public IAtomic<int> Update(int value)
        {
            return new Dint(value);
        }

        /// <inheritdoc />
        public IAtomic Update(object value)
        {
            return value switch
            {
                null => throw new ArgumentNullException(nameof(value), "Value can not be null"),
                Dint atomic => new Dint(atomic),
                int typed => new Dint(typed),
                string str => new Dint(Parse(str)),
                _ => throw new ArgumentException($"Value type '{value.GetType()}' is not a valid for {GetType()}")
            };
        }

        /// <inheritdoc />
        public IDataType Instantiate()
        {
            return new Dint();
        }

        /// <summary>
        /// Converts the provided <see cref="int"/> to a <see cref="Dint"/> value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A <see cref="Dint"/> value.</returns>
        public static implicit operator Dint(int value)
        {
            return new Dint(value);
        }

        /// <summary>
        /// Converts the provided <see cref="Dint"/> to a <see cref="int"/> value.
        /// </summary>
        /// <param name="atomic">The value to convert.</param>
        /// <returns>A <see cref="int"/> type value.</returns>
        public static implicit operator int(Dint atomic)
        {
            return atomic.Value;
        }

        /// <summary>
        /// Converts the provided <see cref="string"/> to a <see cref="Dint"/> value. 
        /// </summary>
        /// <param name="input">The string value to convert.</param>
        /// <returns>
        /// If the string value is able to be parsed, a new instance of a <see cref="Dint"/> with the value
        /// provided. If not, then a default instance value.
        /// </returns>
        public static implicit operator Dint(string input)
        {
            return new Dint(Parse(input));
        }

        /// <inheritdoc />
        public bool Equals(Dint? other)
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
            return obj.GetType() == GetType() && Equals((Dint)obj);
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
        public static bool operator ==(Dint left, Dint right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Determines whether the objects are not equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the objects are not equal, otherwise, false.</returns>
        public static bool operator !=(Dint left, Dint right)
        {
            return !Equals(left, right);
        }

        /// <inheritdoc />
        public int CompareTo(Dint other)
        {
            return Value.CompareTo(other.Value);
        }

        private static int Parse(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            if (int.TryParse(value, out var result))
                return result;

            return Radix.ParseValue<Dint>(value);
        }
    }
}