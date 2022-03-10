using System;
using System.ComponentModel;
using L5Sharp.Converters;
using L5Sharp.Enums;

namespace L5Sharp.Atomics
{
    /// <summary>
    /// Represents a <b>INT</b> Logix atomic data type, or a type analogous to a <see cref="short"/>.
    /// </summary>
    [TypeConverter(typeof(IntConverter))]
    public sealed class Int : IAtomicType<short>, IEquatable<Int>, IComparable<Int>
    {
        /// <summary>
        /// Creates a new default <see cref="Int"/> type.
        /// </summary>
        public Int()
        {
            Name = nameof(Int).ToUpper();
            Value = default;
        }

        /// <summary>
        /// Creates a new <see cref="Int"/> with the provided value.
        /// </summary>
        /// <param name="value">The value to initialize the type with.</param>
        public Int(short value) : this()
        {
            Value = value;
        }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public string Description => $"Logix representation of a {typeof(short)}";

        /// <inheritdoc />
        public DataTypeFamily Family => DataTypeFamily.None;

        /// <inheritdoc />
        public DataTypeClass Class => DataTypeClass.Atomic;

        /// <inheritdoc />
        public short Value { get; private set; }

        object IAtomicType.Value => Value;

        /// <inheritdoc />
        public void SetValue(short value) => Value = value;

        /// <inheritdoc />
        public void SetValue(object value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));
            
            var converter = TypeDescriptor.GetConverter(GetType());

            if (!converter.CanConvertFrom(value.GetType()))
                throw new ArgumentException($"Value of type '{value.GetType()}' can not be set for type {GetType()}");

            Value = (Int)converter.ConvertFrom(value)!;
        }

        /// <inheritdoc />
        public bool TrySetValue(object? value)
        {
            var converter = TypeDescriptor.GetConverter(GetType());

            if (value is null || !converter.CanConvertFrom(value.GetType()))
                return false;

            Value = (converter.ConvertFrom(value) as Int)!;
            return true;
        }

        /// <inheritdoc />
        public string Format(Radix? radix = null) =>
            radix is not null ? radix.Format(this) : Radix.Default(this).Format(this);

        /// <inheritdoc />
        public IDataType Instantiate() => new Int();

        /// <summary>
        /// Converts the provided <see cref="short"/> to a <see cref="Int"/> value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A <see cref="Int"/> value.</returns>
        public static implicit operator Int(short value) => new(value);

        /// <summary>
        /// Converts the provided <see cref="Int"/> to a <see cref="short"/> value.
        /// </summary>
        /// <param name="atomic">The value to convert.</param>
        /// <returns>A <see cref="short"/> type value.</returns>
        public static implicit operator short(Int atomic) => atomic.Value;

        /// <summary>
        /// Converts the provided <see cref="string"/> to a <see cref="Int"/> value. 
        /// </summary>
        /// <param name="input">The string value to convert.</param>
        /// <returns>
        /// If the string value is able to be parsed, a new instance of a <see cref="Int"/> with the value
        /// provided. If not, then a default instance value.
        /// </returns>
        public static implicit operator Int(string input) => Radix.ParseValue<Int>(input);

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
        public override int GetHashCode() => Name.GetHashCode();

        /// <inheritdoc />
        public override string ToString() => Name;

        /// <summary>
        /// Determines whether the objects are equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the objects are equal, otherwise, false.</returns>
        public static bool operator ==(Int left, Int right) => Equals(left, right);

        /// <summary>
        /// Determines whether the objects are not equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the objects are not equal, otherwise, false.</returns>
        public static bool operator !=(Int left, Int right) => !Equals(left, right);

        /// <inheritdoc />
        public int CompareTo(Int? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            return ReferenceEquals(null, other) ? 1 : Value.CompareTo(other.Value);
        }
    }
}