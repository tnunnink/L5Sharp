using System;
using System.ComponentModel;
using L5Sharp.Converters;
using L5Sharp.Enums;

namespace L5Sharp.Types
{
    /// <summary>
    /// Represents a <b>SINT</b> Logix atomic data type, or a type analogous to <see cref="sbyte"/>.
    /// </summary>
    [TypeConverter(typeof(SintConverter))]
    public sealed class SINT : IAtomicType<sbyte>, IEquatable<SINT>, IComparable<SINT>
    {
        /// <summary>
        /// Creates a new default <see cref="SINT"/> type.
        /// </summary>
        public SINT()
        {
            Name = nameof(SINT).ToUpper();
            Value = default;
        }

        /// <summary>
        /// Creates a new <see cref="SINT"/> with the provided value.
        /// </summary>
        /// <param name="value">The value to initialize the type with.</param>
        public SINT(sbyte value) : this()
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

        /// <summary>
        /// Represents the largest possible value of <see cref="SINT"/>.
        /// </summary>
        public const sbyte MaxValue = sbyte.MaxValue;

        /// <summary>
        /// Represents the smallest possible value of <see cref="SINT"/>.
        /// </summary>
        public const sbyte MinValue = sbyte.MinValue;

        /// <inheritdoc />
        public void SetValue(sbyte value) => Value = value;

        /// <inheritdoc />
        public void SetValue(object value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            var converter = TypeDescriptor.GetConverter(GetType());

            if (!converter.CanConvertFrom(value.GetType()))
                throw new ArgumentException($"Can not convert type {value.GetType()} to value type {GetType()}");

            Value = (SINT)converter.ConvertFrom(value)!;
        }

        /// <inheritdoc />
        public string Format(Radix? radix = null) =>
            radix is not null ? radix.Format(this) : Radix.Default(this).Format(this);

        /// <inheritdoc />
        public IDataType Instantiate() => new SINT();

        /// <summary>
        /// Parses the provided input string value into a <see cref="SINT"/> atomic value.
        /// </summary>
        /// <param name="value">The string value to parse.</param>
        /// <returns>A new <see cref="SINT"/> that represents the parsed value.</returns>
        public static SINT Parse(string value) => sbyte.Parse(value);

        /// <summary>
        /// Parses the provided input string value into a <see cref="SINT"/> atomic value.
        /// </summary>
        /// <param name="value">The string value to parse.</param>
        /// <param name="radix"></param>
        /// <returns>A new <see cref="SINT"/> that represents the parsed value.</returns>
        public static SINT Parse(string value, Radix radix) => radix is not null
            ? (SINT)radix.Parse(value)
            : throw new ArgumentNullException(nameof(radix));

        /// <summary>
        /// Tries to convert the string representation of a number to its <see cref="SINT"/> equivalent,
        /// and returns a value that indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="value">The string value to parse.</param>
        /// <param name="result">When this method returns, contains the 8-bit signed integer value that is equivalent
        /// to the number contained in s if the conversion succeeded, or zero if the conversion failed.
        /// The conversion fails if the value parameter is null or <see cref="string.Empty"/>, is not in the correct format,
        /// or represents a number that is less than <see cref="MinValue"/> or greater than <see cref="MaxValue"/>.
        /// This parameter is passed uninitialized; any value originally supplied in result will be overwritten.</param>
        /// <returns>true if value was converted successfully; otherwise, false.</returns>
        public static bool TryParse(string value, out SINT result)
        {
            if (sbyte.TryParse(value, out var parsed))
            {
                result = new SINT(parsed);
                return true;
            }

            var atomic = Radix.TryParse<SINT>(value);
            if (atomic is not null)
            {
                result = atomic;
                return true;
            }

            result = new SINT();
            return false;
        }

        /// <summary>
        /// Converts the provided <see cref="byte"/> to a <see cref="SINT"/> value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A <see cref="SINT"/> value.</returns>
        public static implicit operator SINT(sbyte value) => new(value);

        /// <summary>
        /// Converts the provided <see cref="SINT"/> to a <see cref="byte"/> value.
        /// </summary>
        /// <param name="atomic">The value to convert.</param>
        /// <returns>A <see cref="byte"/> type value.</returns>
        public static implicit operator sbyte(SINT atomic) => atomic.Value;

        /// <inheritdoc />
        public bool Equals(SINT? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value == other.Value;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj) => Equals(obj as SINT);

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
        public static bool operator ==(SINT left, SINT right) => Equals(left, right);

        /// <summary>
        /// Determines whether the objects are not equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the objects are not equal, otherwise, false.</returns>
        public static bool operator !=(SINT left, SINT right) => !Equals(left, right);

        /// <inheritdoc />
        public int CompareTo(SINT? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            return ReferenceEquals(null, other) ? 1 : Value.CompareTo(other.Value);
        }
    }
}