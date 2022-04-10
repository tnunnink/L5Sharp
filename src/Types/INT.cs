using System;
using System.ComponentModel;
using L5Sharp.Converters;
using L5Sharp.Enums;

namespace L5Sharp.Types
{
    /// <summary>
    /// Represents a <b>INT</b> Logix atomic data type, or a type analogous to a <see cref="short"/>.
    /// </summary>
    [TypeConverter(typeof(IntConverter))]
    public sealed class INT : IAtomicType<short>, IEquatable<INT>, IComparable<INT>
    {
        /// <summary>
        /// Creates a new default <see cref="INT"/> type.
        /// </summary>
        public INT()
        {
            Name = nameof(INT).ToUpper();
            Value = default;
        }

        /// <summary>
        /// Creates a new <see cref="INT"/> with the provided value.
        /// </summary>
        /// <param name="value">The value to initialize the type with.</param>
        public INT(short value) : this()
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
        
        /// <summary>
        /// Represents the largest possible value of <see cref="INT"/>.
        /// </summary>
        public const short MaxValue = short.MaxValue;
        
        /// <summary>
        /// Represents the smallest possible value of <see cref="INT"/>.
        /// </summary>
        public const short MinValue = short.MinValue;

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

            Value = (INT)converter.ConvertFrom(value)!;
        }

        /// <inheritdoc />
        public string Format(Radix? radix = null) =>
            radix is not null ? radix.Format(this) : Radix.Default(this).Format(this);

        /// <inheritdoc />
        public IDataType Instantiate() => new INT();

        /// <summary>
        /// Converts the provided <see cref="short"/> to a <see cref="INT"/> value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A <see cref="INT"/> value.</returns>
        public static implicit operator INT(short value) => new(value);

        /// <summary>
        /// Converts the provided <see cref="INT"/> to a <see cref="short"/> value.
        /// </summary>
        /// <param name="atomic">The value to convert.</param>
        /// <returns>A <see cref="short"/> type value.</returns>
        public static implicit operator short(INT atomic) => atomic.Value;

        /// <summary>
        /// Converts the provided <see cref="string"/> to a <see cref="INT"/> value. 
        /// </summary>
        /// <param name="input">The string value to convert.</param>
        /// <returns>
        /// If the string value is able to be parsed, a new instance of a <see cref="INT"/> with the value
        /// provided. If not, then a default instance value.
        /// </returns>
        public static implicit operator INT(string input) => Radix.ParseValue<INT>(input);

        /// <inheritdoc />
        public bool Equals(INT? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value == other.Value;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj) => Equals(obj as INT);

        /// <inheritdoc />
        // ReSharper disable once NonReadonlyMemberInGetHashCode
        // Not sure how else to handle since it needs to be settable and used for equality.
        // This would only be a problem if you created a hash table of atomic types.
        // Not sure anyone would need to do that.
        public override int GetHashCode() => Value.GetHashCode();

        /// <inheritdoc />
        public override string ToString() => Value.ToString();

        /// <summary>
        /// Determines whether the objects are equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the objects are equal, otherwise, false.</returns>
        public static bool operator ==(INT left, INT right) => Equals(left, right);

        /// <summary>
        /// Determines whether the objects are not equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the objects are not equal, otherwise, false.</returns>
        public static bool operator !=(INT left, INT right) => !Equals(left, right);

        /// <inheritdoc />
        public int CompareTo(INT? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            return ReferenceEquals(null, other) ? 1 : Value.CompareTo(other.Value);
        }
    }
}