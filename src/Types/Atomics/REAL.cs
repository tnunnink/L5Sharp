using System;
using System.ComponentModel;
using System.Globalization;
using L5Sharp.Enums;
using L5Sharp.Types.Atomics.Converters;

namespace L5Sharp.Types.Atomics
{
    /// <summary>
    /// Represents a <b>REAL</b> Logix atomic data type, or a type analogous to a <see cref="float"/>.
    /// </summary>
    [TypeConverter(typeof(RealConverter))]
    public sealed class REAL : AtomicType, IEquatable<REAL>, IComparable<REAL>
    {
        private readonly float _value;
        
        /// <summary>
        /// Creates a new default <see cref="REAL"/> type.
        /// </summary>
        public REAL() : base(nameof(REAL))
        {
        }
        
        /// <summary>
        /// Creates a new <see cref="REAL"/> value with the provided radix format.
        /// </summary>
        /// <param name="radix">The <see cref="Enums.Radix"/> number format of the value.</param>
        public REAL(Radix? radix) : base(nameof(REAL), radix)
        {
        }

        /// <summary>
        /// Creates a new <see cref="REAL"/> with the provided value.
        /// </summary>
        /// <param name="value">The value to initialize the type with.</param>
        /// <param name="radix">The optional radix format of the value.</param>
        public REAL(float value, Radix? radix = null) : this(radix)
        {
            _value = value;
        }

        /// <summary>
        /// Creates a new <see cref="REAL"/> with the provided string value.
        /// </summary>
        /// <param name="value">The string value to parse and convert to the value type.</param>
        /// <param name="radix">The optional radix format of the value. If not provided, will be inferred
        /// using <see cref="Enums.Radix.Infer"/>.</param>
        public REAL(string value, Radix? radix = null) : this(radix ?? Radix.Infer(value))
        {
            var converter = TypeDescriptor.GetConverter(GetType());
            _value = (float)(REAL)converter.ConvertFrom(value)!;
        }

        /// <summary>
        /// Represents the largest possible value of <see cref="REAL"/>.
        /// </summary>
        public const float MaxValue = float.MaxValue;
        
        /// <summary>
        /// Represents the smallest possible value of <see cref="REAL"/>.
        /// </summary>
        public const float MinValue = float.MinValue;
        
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
        public static implicit operator float(REAL atomic) => atomic._value;
        
        /// <summary>
        /// Converts the provided <see cref="bool"/> to a <see cref="BOOL"/> value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A <see cref="BOOL"/> value.</returns>
        public static implicit operator REAL(string value) => new(value);
        
        /// <summary>
        /// Converts the provided <see cref="bool"/> to a <see cref="BOOL"/> value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A <see cref="BOOL"/> value.</returns>
        public static implicit operator string(REAL value) => value.ToString();

        /// <inheritdoc />
        public bool Equals(REAL? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Math.Abs(_value - other._value) < float.Epsilon;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj) => Equals(obj as REAL);

        /// <inheritdoc />
        // ReSharper disable once NonReadonlyMemberInGetHashCode
        // Not sure how else to handle since it needs to be settable and used for equality.
        // This would only be a problem if you created a hash table of atomic types.
        // Not sure anyone would need to do that.
        public override int GetHashCode() => _value.GetHashCode();

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
            return ReferenceEquals(null, other) ? 1 : _value.CompareTo(other._value);
        }
    }
}