using System;
using System.ComponentModel;
using L5Sharp.Converters;
using L5Sharp.Enums;

namespace L5Sharp.Types
{
    /// <summary>
    /// Represents a <b>DINT</b> Logix atomic data type, or a type analogous to a <see cref="int"/>.
    /// </summary>
    [TypeConverter(typeof(DintConverter))]
    public sealed class DINT : IAtomicType<int>, IEquatable<DINT>, IComparable<DINT>
    {
        /// <summary>
        /// Creates a new default <see cref="DINT"/> type.
        /// </summary>
        public DINT()
        {
            Name = nameof(DINT).ToUpper();
            Value = default;
        }

        /// <summary>
        /// Creates a new <see cref="DINT"/> with the provided value.
        /// </summary>
        /// <param name="value">The value to initialize the type with.</param>
        public DINT(int value) : this()
        {
            Value = value;
        }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public string Description => $"Logix representation of a {typeof(int)}";

        /// <inheritdoc />
        public DataTypeFamily Family => DataTypeFamily.None;

        /// <inheritdoc />
        public DataTypeClass Class => DataTypeClass.Atomic;

        /// <inheritdoc />
        public int Value { get; private set; }

        object IAtomicType.Value => Value;

        /// <summary>
        /// Represents the largest possible value of <see cref="DINT"/>.
        /// </summary>
        public const int MaxValue = int.MaxValue;

        /// <summary>
        /// Represents the smallest possible value of <see cref="DINT"/>.
        /// </summary>
        public const int MinValue = int.MinValue;

        /// <inheritdoc />
        public void SetValue(int value) => Value = value;

        /// <inheritdoc />
        public void SetValue(object value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            var converter = TypeDescriptor.GetConverter(GetType());

            if (!converter.CanConvertFrom(value.GetType()))
                throw new ArgumentException($"Value of type '{value.GetType()}' is not a valid for {GetType()}");

            Value = (DINT)converter.ConvertFrom(value)!;
        }

        /// <inheritdoc />
        public string Format(Radix? radix = null) =>
            radix is not null ? radix.Format(this) : Radix.Default(this).Format(this);

        /// <inheritdoc />
        public IDataType Instantiate() => new DINT();

        /// <summary>
        /// Converts the provided <see cref="int"/> to a <see cref="DINT"/> value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A <see cref="DINT"/> value.</returns>
        public static implicit operator DINT(int value) => new(value);

        /// <summary>
        /// Converts the provided <see cref="DINT"/> to a <see cref="int"/> value.
        /// </summary>
        /// <param name="atomic">The value to convert.</param>
        /// <returns>A <see cref="int"/> type value.</returns>
        public static implicit operator int(DINT atomic) => atomic.Value;

        /// <summary>
        /// Converts the provided <see cref="string"/> to a <see cref="DINT"/> value. 
        /// </summary>
        /// <param name="input">The string value to convert.</param>
        /// <returns>
        /// If the string value is able to be parsed, a new instance of a <see cref="DINT"/> with the value
        /// provided. If not, then a default instance value.
        /// </returns>
        public static implicit operator DINT(string input) => Radix.ParseValue<DINT>(input);

        /// <inheritdoc />
        public bool Equals(DINT? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value == other.Value;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj) => Equals(obj as DINT);

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
        public static bool operator ==(DINT left, DINT right) => Equals(left, right);

        /// <summary>
        /// Determines whether the objects are not equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the objects are not equal, otherwise, false.</returns>
        public static bool operator !=(DINT left, DINT right) => !Equals(left, right);

        /// <inheritdoc />
        public int CompareTo(DINT? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            return ReferenceEquals(null, other) ? 1 : Value.CompareTo(other.Value);
        }
    }
}