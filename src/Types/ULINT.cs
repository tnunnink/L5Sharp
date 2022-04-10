using System;
using System.ComponentModel;
using L5Sharp.Converters;
using L5Sharp.Enums;

namespace L5Sharp.Types
{
    /// <summary>
    /// Represents a <b>ULINT</b> Logix atomic data type, or a type analogous to a <see cref="ulong"/>.
    /// </summary>
    [TypeConverter(typeof(ULintConverter))]
    public class ULINT : IAtomicType<ulong>, IEquatable<ULINT>, IComparable<ULINT>
    {
        /// <summary>
        /// Creates a new default <see cref="ULINT"/> type.
        /// </summary>
        public ULINT()
        {
            Name = nameof(ULINT);
            Value = default;
        }

        /// <summary>
        /// Creates a new <see cref="ULINT"/> with the provided value.
        /// </summary>
        /// <param name="value">The value to initialize the type with.</param>
        public ULINT(ulong value) : this()
        {
            Value = value;
        }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public string Description => $"Logix representation of a {typeof(ulong)}";

        /// <inheritdoc />
        public DataTypeFamily Family => DataTypeFamily.None;

        /// <inheritdoc />
        public DataTypeClass Class => DataTypeClass.Atomic;

        /// <inheritdoc />
        public ulong Value { get; private set; }

        object IAtomicType.Value => Value;

        /// <summary>
        /// Represents the largest possible value of <see cref="ULINT"/>.
        /// </summary>
        public const ulong MaxValue = ulong.MaxValue;

        /// <summary>
        /// Represents the smallest possible value of <see cref="ULINT"/>.
        /// </summary>
        public const ulong MinValue = ulong.MinValue;

        /// <inheritdoc />
        public void SetValue(ulong value) => Value = value;

        /// <inheritdoc />
        public void SetValue(object value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            var converter = TypeDescriptor.GetConverter(GetType());

            if (!converter.CanConvertFrom(value.GetType()))
                throw new ArgumentException($"Value of type '{value.GetType()}' is not a valid for {GetType()}");

            Value = (ULINT)converter.ConvertFrom(value)!;
        }

        /// <inheritdoc />
        public string Format(Radix? radix = null) =>
            radix is not null ? radix.Format(this) : Radix.Default(this).Format(this);

        /// <inheritdoc />
        public IDataType Instantiate() => new ULINT();

        /// <summary>
        /// Converts the provided <see cref="ulong"/> to a <see cref="ULINT"/> value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A <see cref="ULINT"/> value.</returns>
        public static implicit operator ULINT(ulong value) => new(value);

        /// <summary>
        /// Converts the provided <see cref="ULINT"/> to a <see cref="ulong"/> value.
        /// </summary>
        /// <param name="atomic">The value to convert.</param>
        /// <returns>A <see cref="ulong"/> type value.</returns>
        public static implicit operator ulong(ULINT atomic) => atomic.Value;

        /// <summary>
        /// Converts the provided <see cref="string"/> to a <see cref="ULINT"/> value. 
        /// </summary>
        /// <param name="input">The string value to convert.</param>
        /// <returns>
        /// If the string value is able to be parsed, a new instance of a <see cref="ULINT"/> with the value
        /// provided. If not, then a default instance value.
        /// </returns>
        public static implicit operator ULINT(string input) =>
            ulong.TryParse(input, out var result) ? new ULINT(result) : Radix.ParseValue<ULINT>(input);

        /// <inheritdoc />
        public bool Equals(ULINT? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value == other.Value;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj) => Equals(obj as ULINT);

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
        public static bool operator ==(ULINT left, ULINT right) => Equals(left, right);

        /// <summary>
        /// Determines whether the objects are not equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the objects are not equal, otherwise, false.</returns>
        public static bool operator !=(ULINT left, ULINT right) => !Equals(left, right);

        /// <inheritdoc />
        public int CompareTo(ULINT? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            return ReferenceEquals(null, other) ? 1 : Value.CompareTo(other.Value);
        }
    }
}