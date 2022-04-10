using System;
using System.ComponentModel;
using System.Globalization;
using L5Sharp.Converters;
using L5Sharp.Enums;

namespace L5Sharp.Types
{
    /// <summary>
    /// Represents a <b>REAL</b> Logix atomic data type, or a type analogous to a <see cref="float"/>.
    /// </summary>
    [TypeConverter(typeof(RealConverter))]
    public sealed class REAL : IAtomicType<float>, IEquatable<REAL>, IComparable<REAL>
    {
        /// <summary>
        /// Creates a new default <see cref="REAL"/> type.
        /// </summary>
        public REAL()
        {
            Name = nameof(REAL).ToUpper();
            Value = default;
        }

        /// <summary>
        /// Creates a new <see cref="REAL"/> with the provided value.
        /// </summary>
        /// <param name="value">The value to initialize the type with.</param>
        public REAL(float value) : this()
        {
            Value = value;
        }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public string Description => $"Logix representation of a {typeof(float)}";

        /// <inheritdoc />
        public DataTypeFamily Family => DataTypeFamily.None;

        /// <inheritdoc />
        public DataTypeClass Class => DataTypeClass.Atomic;

        /// <inheritdoc />
        public float Value { get; private set; }

        object IAtomicType.Value => Value;
        
        /// <summary>
        /// Represents the largest possible value of <see cref="REAL"/>.
        /// </summary>
        public const float MaxValue = float.MaxValue;
        
        /// <summary>
        /// Represents the smallest possible value of <see cref="REAL"/>.
        /// </summary>
        public const float MinValue = float.MinValue;

        /// <inheritdoc />
        public void SetValue(float value) => Value = value;

        /// <inheritdoc />
        public void SetValue(object value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            var converter = TypeDescriptor.GetConverter(GetType());

            if (!converter.CanConvertFrom(value.GetType()))
                throw new ArgumentException($"Value of type '{value.GetType()}' is not a valid for {GetType()}");

            Value = (REAL)converter.ConvertFrom(value)!;
        }

        /// <inheritdoc />
        public string Format(Radix? radix = null) =>
            radix is not null ? radix.Format(this) : Radix.Default(this).Format(this);

        /// <inheritdoc />
        public IDataType Instantiate() => new REAL();

        /// <summary>
        /// Converts the provided <see cref="float"/> to a <see cref="REAL"/> value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A <see cref="REAL"/> value.</returns>
        public static implicit operator REAL(float value) => new(value);

        /// <summary>
        /// Converts the provided <see cref="REAL"/> to a <see cref="float"/> value.
        /// </summary>
        /// <param name="atomic">The value to convert.</param>
        /// <returns>A <see cref="float"/> type value.</returns>
        public static implicit operator float(REAL atomic) => atomic.Value;

        /// <summary>
        /// Converts the provided <see cref="string"/> to a <see cref="REAL"/> value. 
        /// </summary>
        /// <param name="input">The string value to convert.</param>
        /// <returns>
        /// If the string value is able to be parsed, a new instance of a <see cref="REAL"/> with the value
        /// provided. If not, then a default instance value.
        /// </returns>
        public static implicit operator REAL(string input) => Radix.ParseValue<REAL>(input);

        /// <inheritdoc />
        public bool Equals(REAL? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Math.Abs(Value - other.Value) < float.Epsilon;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj) => Equals(obj as REAL);

        /// <inheritdoc />
        // ReSharper disable once NonReadonlyMemberInGetHashCode
        // Not sure how else to handle since it needs to be settable and used for equality.
        // This would only be a problem if you created a hash table of atomic types.
        // Not sure anyone would need to do that.
        public override int GetHashCode() => Value.GetHashCode();

        /// <inheritdoc />
        public override string ToString() => Value.ToString(CultureInfo.InvariantCulture);

        /// <summary>
        /// Determines whether the objects are equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the objects are equal, otherwise, false.</returns>
        public static bool operator ==(REAL left, REAL right) => Equals(left, right);

        /// <summary>
        /// Determines whether the objects are not equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the objects are not equal, otherwise, false.</returns>
        public static bool operator !=(REAL left, REAL right) => !Equals(left, right);

        /// <inheritdoc />
        public int CompareTo(REAL? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            return ReferenceEquals(null, other) ? 1 : Value.CompareTo(other.Value);
        }
    }
}