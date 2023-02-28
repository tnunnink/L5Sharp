using System;
using System.ComponentModel;
using L5Sharp.Enums;
using L5Sharp.Types.Atomics.Converters;

namespace L5Sharp.Types.Atomics
{
    /// <summary>
    /// Represents a <b>ULINT</b> Logix atomic data type, or a type analogous to a <see cref="ulong"/>.
    /// </summary>
    [TypeConverter(typeof(ULintConverter))]
    public class ULINT : AtomicType, IEquatable<ULINT>, IComparable<ULINT>
    {
        private readonly ulong _value;
        
        /// <summary>
        /// Creates a new default <see cref="ULINT"/> type.
        /// </summary>
        /// <param name="radix">The optional radix format of the value.</param>
        public ULINT(Radix? radix = null) : base(nameof(ULINT), radix)
        {
        }

        /// <summary>
        /// Creates a new <see cref="ULINT"/> with the provided value.
        /// </summary>
        /// <param name="value">The value to initialize the type with.</param>
        /// <param name="radix">The optional radix format of the value.</param>
        public ULINT(ulong value, Radix? radix = null) : this(radix)
        {
            _value = value;
        }

        /// <summary>
        /// Creates a new <see cref="ULINT"/> with the provided string value.
        /// </summary>
        /// <param name="value">The string value to parse and convert to the value type.</param>
        /// <param name="radix">The optional radix format of the value. If not provided, will be inferred
        /// using <see cref="Enums.Radix.Infer"/>.</param>
        public ULINT(string value, Radix? radix = null) : this(radix ?? Radix.Infer(value))
        {
            var converter = TypeDescriptor.GetConverter(GetType());
            _value = (ulong)(ULINT)converter.ConvertFrom(value)!;
        }

        /// <summary>
        /// Represents the largest possible value of <see cref="ULINT"/>.
        /// </summary>
        public const ulong MaxValue = ulong.MaxValue;

        /// <summary>
        /// Represents the smallest possible value of <see cref="ULINT"/>.
        /// </summary>
        public const ulong MinValue = ulong.MinValue;

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
        public static implicit operator ulong(ULINT atomic) => atomic._value;
        
        /// <summary>
        /// Implicitly converts a <see cref="string"/> to a <see cref="ULINT"/> value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A new <see cref="ULINT"/> value.</returns>
        public static implicit operator ULINT(string value) => new(value);
        
        /// <summary>
        /// Implicitly converts the provided <see cref="ULINT"/> to a <see cref="string"/> value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A new <see cref="string"/> value.</returns>
        public static implicit operator ULINT(LINT value) => value.ToString();

        /// <inheritdoc />
        public bool Equals(ULINT? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _value == other._value;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj) => Equals(obj as ULINT);

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
            return ReferenceEquals(null, other) ? 1 : _value.CompareTo(other._value);
        }
    }
}