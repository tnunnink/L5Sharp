using System;
using System.ComponentModel;
using L5Sharp.Converters;
using L5Sharp.Enums;

namespace L5Sharp.Atomics
{
    /// <summary>
    /// Represents a <b>UInt</b> Logix atomic data type, or a type analogous to a <see cref="ushort"/>.
    /// </summary>
    [TypeConverter(typeof(UIntConverter))]
    public class UInt : IAtomicType<ushort>, IEquatable<UInt>, IComparable<UInt>
    {
        /// <summary>
        /// Creates a new default <see cref="UInt"/> type.
        /// </summary>
        public UInt()
        {
            Name = nameof(UInt).ToUpper();
            Value = default;
        }

        /// <summary>
        /// Creates a new <see cref="UInt"/> with the provided value.
        /// </summary>
        /// <param name="value">The value to initialize the type with.</param>
        public UInt(ushort value) : this()
        {
            Value = value;
        }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public string Description => $"Logix representation of a {typeof(ushort)}";

        /// <inheritdoc />
        public DataTypeFamily Family => DataTypeFamily.None;

        /// <inheritdoc />
        public DataTypeClass Class => DataTypeClass.Atomic;

        /// <inheritdoc />
        public ushort Value { get; private set; }

        object IAtomicType.Value => Value;

        /// <inheritdoc />
        public void SetValue(ushort value) => Value = value;

        /// <inheritdoc />
        public void SetValue(object value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            var converter = TypeDescriptor.GetConverter(GetType());

            if (!converter.CanConvertFrom(value.GetType()))
                throw new ArgumentException($"Value of type '{value.GetType()}' can not be set for type {GetType()}");

            Value = (UInt)converter.ConvertFrom(value)!;
        }

        /// <inheritdoc />
        public bool TrySetValue(object? value)
        {
            var converter = TypeDescriptor.GetConverter(GetType());

            if (value is null || !converter.CanConvertFrom(value.GetType()))
                return false;

            Value = (converter.ConvertFrom(value) as UInt)!;
            return true;
        }

        /// <inheritdoc />
        public string Format(Radix? radix = null) =>
            radix is not null ? radix.Format(this) : Radix.Default(this).Format(this);

        /// <inheritdoc />
        public IDataType Instantiate() => new UInt();

        /// <summary>
        /// Converts the provided <see cref="ushort"/> to a <see cref="UInt"/> value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A <see cref="UInt"/> value.</returns>
        public static implicit operator UInt(ushort value) => new(value);

        /// <summary>
        /// Converts the provided <see cref="UInt"/> to a <see cref="ushort"/> value.
        /// </summary>
        /// <param name="atomic">The value to convert.</param>
        /// <returns>A <see cref="ushort"/> type value.</returns>
        public static implicit operator ushort(UInt atomic) => atomic.Value;

        /// <summary>
        /// Converts the provided <see cref="string"/> to a <see cref="UInt"/> value. 
        /// </summary>
        /// <param name="input">The string value to convert.</param>
        /// <returns>
        /// If the string value is able to be parsed, a new instance of a <see cref="UInt"/> with the value
        /// provided. If not, then a default instance value.
        /// </returns>
        public static implicit operator UInt(string input) =>
            ushort.TryParse(input, out var result) ? new UInt(result) : Radix.ParseValue<UInt>(input);

        /// <inheritdoc />
        public bool Equals(UInt? other)
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
            return obj.GetType() == GetType() && Equals((UInt)obj);
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
        public static bool operator ==(UInt left, UInt right) => Equals(left, right);

        /// <summary>
        /// Determines whether the objects are not equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the objects are not equal, otherwise, false.</returns>
        public static bool operator !=(UInt left, UInt right) => !Equals(left, right);

        /// <inheritdoc />
        public int CompareTo(UInt? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            return ReferenceEquals(null, other) ? 1 : Value.CompareTo(other.Value);
        }
    }
}