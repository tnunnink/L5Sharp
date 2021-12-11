using System;
using L5Sharp.Enums;

namespace L5Sharp.Types
{
    /// <summary>
    /// Represents a INT Logix atomic data type.
    /// </summary>
    public sealed class Int : IAtomic<short>, IEquatable<Int>, IComparable<Int>
    {
        /// <summary>
        /// Creates a new default instance of a Int.
        /// </summary>
        public Int()
        {
            Name = nameof(Int).ToUpper();
            Value = default;
        }

        /// <summary>
        /// Creates a new instance of a Int with the provided value.
        /// </summary>
        /// <param name="value">The value to initialize the type with.</param>
        public Int(short value) : this()
        {
            Value = value;
        }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public string Description => $"RSLogix representation of a {typeof(short)}";

        /// <inheritdoc />
        public DataTypeFamily Family => DataTypeFamily.None;

        /// <inheritdoc />
        public DataTypeClass Class => DataTypeClass.Atomic;

        /// <inheritdoc />
        public short Value { get; }

        object IAtomic.Value => Value;

        /// <inheritdoc />
        public IAtomic<short> Update(short value)
        {
            return new Int(value);
        }

        /// <inheritdoc />
        public IAtomic Update(object value)
        {
            return value switch
            {
                null => throw new ArgumentNullException(nameof(value), "Value can not be null"),
                Int atomic => new Int(atomic),
                byte n => new Int(n),
                short n => new Int(n),
                string str => new Int(Radix.ParseValue<Int>(str)),
                _ => throw new ArgumentException($"Value type '{value.GetType()}' is not a valid for {GetType()}")
            };
        }

        /// <inheritdoc />
        public IDataType Instantiate()
        {
            return new Int();
        }

        /// <summary>
        /// Converts the provided <see cref="short"/> to a <see cref="Int"/> value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A <see cref="Int"/> value.</returns>
        public static implicit operator Int(short value)
        {
            return new Int(value);
        }

        /// <summary>
        /// Converts the provided <see cref="Int"/> to a <see cref="short"/> value.
        /// </summary>
        /// <param name="atomic">The value to convert.</param>
        /// <returns>A <see cref="short"/> type value.</returns>
        public static implicit operator short(Int atomic)
        {
            return atomic.Value;
        }

        /// <summary>
        /// Converts the provided <see cref="string"/> to a <see cref="Int"/> value. 
        /// </summary>
        /// <param name="input">The string value to convert.</param>
        /// <returns>
        /// If the string value is able to be parsed, a new instance of a <see cref="Int"/> with the value
        /// provided. If not, then a default instance value.
        /// </returns>
        public static implicit operator Int(string input)
        {
            return new Int(Radix.ParseValue<Int>(input));
        }

        /// <inheritdoc />
        public bool Equals(Int? other)
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
            return obj.GetType() == GetType() && Equals((Int)obj);
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
        public static bool operator ==(Int left, Int right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Determines whether the objects are not equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the objects are not equal, otherwise, false.</returns>
        public static bool operator !=(Int left, Int right)
        {
            return !Equals(left, right);
        }

        /// <inheritdoc />
        public int CompareTo(Int? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            return ReferenceEquals(null, other) ? 1 : Value.CompareTo(other.Value);
        }
    }
}