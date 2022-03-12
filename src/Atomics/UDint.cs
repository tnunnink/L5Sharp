using System;
using System.ComponentModel;
using L5Sharp.Converters;
using L5Sharp.Enums;

namespace L5Sharp.Atomics
{
    /// <summary>
    /// Represents a <b>UDint</b> Logix atomic data type, or a type analogous to a <see cref="uint"/>.
    /// </summary>
    [TypeConverter(typeof(UDintConverter))]
    public class UDint : IAtomicType<uint>, IEquatable<UDint>, IComparable<UDint>
    {
        /// <summary>
        /// Creates a new default <see cref="UDint"/> type.
        /// </summary>
        public UDint()
        {
            Name = nameof(UDint).ToUpper();
            Value = default;
        }

        /// <summary>
        /// Creates a new <see cref="UDint"/> with the provided value.
        /// </summary>
        /// <param name="value">The value to initialize the type with.</param>
        public UDint(uint value) : this()
        {
            Value = value;
        }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public string Description => $"Logix representation of a {typeof(uint)}";

        /// <inheritdoc />
        public DataTypeFamily Family => DataTypeFamily.None;

        /// <inheritdoc />
        public DataTypeClass Class => DataTypeClass.Atomic;

        /// <inheritdoc />
        public uint Value { get; private set; }

        object IAtomicType.Value => Value;

        /// <inheritdoc />
        public void SetValue(uint value) => Value = value;

        /// <inheritdoc />
        public void SetValue(object value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            var converter = TypeDescriptor.GetConverter(GetType());

            if (!converter.CanConvertFrom(value.GetType()))
                throw new ArgumentException($"Value of type '{value.GetType()}' is not a valid for {GetType()}");

            Value = (UDint)converter.ConvertFrom(value)!;
        }

        /// <inheritdoc />
        public string Format(Radix? radix = null) =>
            radix is not null ? radix.Format(this) : Radix.Default(this).Format(this);

        /// <inheritdoc />
        public IDataType Instantiate() => new UDint();

        /// <summary>
        /// Converts the provided <see cref="uint"/> to a <see cref="UDint"/> value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A <see cref="UDint"/> value.</returns>
        public static implicit operator UDint(uint value) => new(value);

        /// <summary>
        /// Converts the provided <see cref="UDint"/> to a <see cref="uint"/> value.
        /// </summary>
        /// <param name="atomic">The value to convert.</param>
        /// <returns>A <see cref="uint"/> type value.</returns>
        public static implicit operator uint(UDint atomic) => atomic.Value;

        /// <summary>
        /// Converts the provided <see cref="string"/> to a <see cref="UDint"/> value. 
        /// </summary>
        /// <param name="input">The string value to convert.</param>
        /// <returns>
        /// If the string value is able to be parsed, a new instance of a <see cref="UDint"/> with the value
        /// provided. If not, then a default instance value.
        /// </returns>
        public static implicit operator UDint(string input) =>
            uint.TryParse(input, out var result) ? new UDint(result) : Radix.ParseValue<UDint>(input);

        /// <inheritdoc />
        public bool Equals(UDint? other)
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
            return obj.GetType() == GetType() && Equals((UDint)obj);
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
        public static bool operator ==(UDint left, UDint right) => Equals(left, right);

        /// <summary>
        /// Determines whether the objects are not equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the objects are not equal, otherwise, false.</returns>
        public static bool operator !=(UDint left, UDint right) => !Equals(left, right);

        /// <inheritdoc />
        public int CompareTo(UDint? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            return ReferenceEquals(null, other) ? 1 : Value.CompareTo(other.Value);
        }
    }
}