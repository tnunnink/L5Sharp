using System;
using System.ComponentModel;
using L5Sharp.Converters;
using L5Sharp.Enums;

namespace L5Sharp.Atomics
{
    /// <summary>
    /// Represents a <b>SINT</b> Logix atomic data type, or a type analogous to a <see cref="sbyte"/>.
    /// </summary>
    [TypeConverter(typeof(SintConverter))]
    public sealed class Sint : IAtomicType<sbyte>, IEquatable<Sint>, IComparable<Sint>
    {
        /// <summary>
        /// Creates a new default <see cref="Sint"/> type.
        /// </summary>
        public Sint()
        {
            Name = nameof(Sint).ToUpper();
            Value = default;
        }

        /// <summary>
        /// Creates a new <see cref="Sint"/> with the provided value.
        /// </summary>
        /// <param name="value">The value to initialize the type with.</param>
        public Sint(sbyte value) : this()
        {
            Value = value;
        }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public string Description => $"Logix representation of a {typeof(sbyte)}";

        /// <inheritdoc />
        public DataTypeFamily Family => DataTypeFamily.None;

        /// <inheritdoc />
        public DataTypeClass Class => DataTypeClass.Atomic;

        /// <inheritdoc />
        public sbyte Value { get; private set; }

        object IAtomicType.Value => Value;

        /// <inheritdoc />
        public void SetValue(sbyte value) => Value = value;

        /// <inheritdoc />
        public void SetValue(object value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));
            
            var converter = TypeDescriptor.GetConverter(GetType());

            if (!converter.CanConvertFrom(value.GetType()))
                throw new ArgumentException($"Value of type '{value.GetType()}' is not a valid for {GetType()}");

            Value = (Sint)converter.ConvertFrom(value)!;
        }

        /// <inheritdoc />
        public bool TrySetValue(object? value)
        {
            var converter = TypeDescriptor.GetConverter(GetType());

            if (value is null || !converter.CanConvertFrom(value.GetType()))
                return false;

            Value = (converter.ConvertFrom(value) as Sint)!;
            return true;
        }

        /// <inheritdoc />
        public string Format(Radix? radix = null) =>
            radix is not null ? radix.Format(this) : Radix.Default(this).Format(this);

        /// <inheritdoc />
        public IDataType Instantiate() => new Sint();

        /// <summary>
        /// Converts the provided <see cref="byte"/> to a <see cref="Sint"/> value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A <see cref="Sint"/> value.</returns>
        public static implicit operator Sint(sbyte value) => new(value);

        /// <summary>
        /// Converts the provided <see cref="Sint"/> to a <see cref="byte"/> value.
        /// </summary>
        /// <param name="atomic">The value to convert.</param>
        /// <returns>A <see cref="byte"/> type value.</returns>
        public static implicit operator sbyte(Sint atomic) => atomic.Value;

        /// <summary>
        /// Converts the provided <see cref="string"/> to a <see cref="Sint"/> value. 
        /// </summary>
        /// <param name="input">The string value to convert.</param>
        /// <returns>
        /// If the string value is able to be parsed, a new instance of a <see cref="Sint"/> with the value
        /// provided. If not, then a default instance value.
        /// </returns>
        public static implicit operator Sint(string input) => Radix.ParseValue<Sint>(input);

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
        public override int GetHashCode() => Name.GetHashCode();
        
        /// <inheritdoc />
        public override string ToString() => Name;

        /// <summary>
        /// Determines whether the objects are equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the objects are equal, otherwise, false.</returns>
        public static bool operator ==(Sint left, Sint right) => Equals(left, right);

        /// <summary>
        /// Determines whether the objects are not equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the objects are not equal, otherwise, false.</returns>
        public static bool operator !=(Sint left, Sint right) => !Equals(left, right);

        /// <inheritdoc />
        public int CompareTo(Sint? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            return ReferenceEquals(null, other) ? 1 : Value.CompareTo(other.Value);
        }
    }
}