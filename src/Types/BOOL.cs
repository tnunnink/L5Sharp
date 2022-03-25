using System;
using System.ComponentModel;
using L5Sharp.Converters;
using L5Sharp.Enums;

namespace L5Sharp.Types
{
    /// <summary>
    /// Represents a <i>BOOL</i> Logix atomic data type, or a type analogous to a <see cref="bool"/>.
    /// </summary>
    [TypeConverter(typeof(BoolConverter))]
    public sealed class BOOL : IAtomicType<bool>, IEquatable<BOOL>, IComparable<BOOL>
    {
        /// <summary>
        /// Creates a new default <see cref="BOOL"/> type.
        /// </summary>
        public BOOL()
        {
            Name = nameof(BOOL);
            Value = default;
        }

        /// <summary>
        /// Creates a new <see cref="BOOL"/> with the provided value.
        /// </summary>
        /// <param name="value">The value to initialize the type with.</param>
        public BOOL(bool value) : this()
        {
            Value = value;
        }

        /// <summary>
        /// Creates a new instance of a BOOL with the provided number.
        /// </summary>
        /// <param name="number">A number that will be evaluated to true if greater that zero, otherwise false</param>
        public BOOL(int number) : this()
        {
            Value = number > 0;
        }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public string Description => $"Logix representation of a {typeof(bool)}";

        /// <inheritdoc />
        public DataTypeFamily Family => DataTypeFamily.None;

        /// <inheritdoc />
        public DataTypeClass Class => DataTypeClass.Atomic;

        /// <inheritdoc />
        public bool Value { get; private set; }

        object IAtomicType.Value => Value;

        /// <inheritdoc />
        public void SetValue(bool value) => Value = value;

        /// <inheritdoc />
        public void SetValue(object value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            var converter = TypeDescriptor.GetConverter(GetType());

            if (!converter.CanConvertFrom(value.GetType()))
                throw new ArgumentException($"Value of type '{value.GetType()}' is not a valid for {GetType()}");

            Value = (BOOL)converter.ConvertFrom(value)!;
        }

        /// <inheritdoc />
        public string Format(Radix? radix = null) =>
            radix is not null ? radix.Format(this) : Radix.Default(this).Format(this);

        /// <inheritdoc />
        public IDataType Instantiate() => new BOOL();

        /// <summary>
        /// Converts the provided <see cref="bool"/> to a <see cref="BOOL"/> value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A <see cref="BOOL"/> value.</returns>
        public static implicit operator BOOL(bool value) => new(value);

        /// <summary>
        /// Converts the provided <see cref="BOOL"/> to a <see cref="bool"/> value.
        /// </summary>
        /// <param name="atomic">The value to convert.</param>
        /// <returns>A <see cref="bool"/> type value.</returns>
        public static implicit operator bool(BOOL atomic) => atomic.Value;

        /// <summary>
        /// Converts the provided <see cref="string"/> to a <see cref="BOOL"/> value. 
        /// </summary>
        /// <param name="input">The string value to convert.</param>
        /// <returns>
        /// If the string value is able to be parsed, a new instance of a <see cref="BOOL"/> with the value
        /// provided. If not, then a default instance value.
        /// </returns>
        public static implicit operator BOOL(string input) => Radix.ParseValue<BOOL>(input);

        /// <inheritdoc />
        public bool Equals(BOOL? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value == other.Value;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj) => ReferenceEquals(this, obj) || obj is BOOL other && Equals(other);

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
        public static bool operator ==(BOOL? left, BOOL? right) => Equals(left, right);

        /// <summary>
        /// Determines whether the objects are not equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the objects are not equal, otherwise, false.</returns>
        public static bool operator !=(BOOL? left, BOOL? right) => !Equals(left, right);

        /// <inheritdoc />
        public int CompareTo(BOOL? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            return ReferenceEquals(null, other) ? 1 : Value.CompareTo(other.Value);
        }
    }
}