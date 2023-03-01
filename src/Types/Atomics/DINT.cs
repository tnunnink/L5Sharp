using System;
using System.ComponentModel;
using L5Sharp.Enums;
using L5Sharp.Types.Atomics.Converters;

namespace L5Sharp.Types.Atomics
{
    /// <summary>
    /// Represents a <b>DINT</b> Logix atomic data type, or a type analogous to a <see cref="int"/>.
    /// </summary>
    [TypeConverter(typeof(DintConverter))]
    public sealed class DINT : AtomicType, IEquatable<DINT>, IComparable<DINT>
    {
        private readonly int _value;
        
        /// <summary>
        /// Creates a new default <see cref="DINT"/> type.
        /// </summary>
        public DINT() : base(nameof(DINT))
        {
        }
        
        /// <summary>
        /// Creates a new <see cref="DINT"/> value with the provided radix format.
        /// </summary>
        /// <param name="radix">The <see cref="Enums.Radix"/> number format of the value.</param>
        public DINT(Radix? radix) : base(nameof(DINT), radix)
        {
        }

        /// <summary>
        /// Creates a new <see cref="DINT"/> with the provided value.
        /// </summary>
        /// <param name="value">The value to initialize the type with.</param>
        /// <param name="radix">The optional radix format of the value.</param>
        public DINT(int value, Radix? radix = null) : this(radix)
        {
            _value = value;
        }

        /// <summary>
        /// Creates a new <see cref="DINT"/> with the provided string value.
        /// </summary>
        /// <param name="value">The string value to parse and convert to the value type.</param>
        /// <param name="radix">The optional radix format of the value. If not provided, will be inferred
        /// using <see cref="Enums.Radix.Infer"/>.</param>
        public DINT(string value, Radix? radix = null) : this(radix ?? Radix.Infer(value))
        {
            var converter = TypeDescriptor.GetConverter(GetType());
            _value = (int)(DINT)converter.ConvertFrom(value)!;
        }

        /// <summary>
        /// Represents the largest possible value of <see cref="DINT"/>.
        /// </summary>
        public const int MaxValue = int.MaxValue;

        /// <summary>
        /// Represents the smallest possible value of <see cref="DINT"/>.
        /// </summary>
        public const int MinValue = int.MinValue;

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
        public static implicit operator int(DINT atomic) => atomic._value;
        
        /// <summary>
        /// Implicitly converts a <see cref="string"/> to a <see cref="DINT"/> value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A new <see cref="DINT"/> value.</returns>
        public static implicit operator DINT(string value) => new(value);
        
        /// <summary>
        /// Implicitly converts the provided <see cref="DINT"/> to a <see cref="string"/> value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A new <see cref="string"/> value.</returns>
        public static implicit operator string(DINT value) => value.ToString();

        /// <inheritdoc />
        public bool Equals(DINT? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _value == other._value;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj) => Equals(obj as DINT);

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
            return ReferenceEquals(null, other) ? 1 : _value.CompareTo(other._value);
        }
    }
}