using System;
using System.ComponentModel;
using L5Sharp.Enums;
using L5Sharp.Types.Atomics.Converters;

namespace L5Sharp.Types.Atomics
{
    /// <summary>
    /// Represents a <b>LINT</b> Logix atomic data type, or a type analogous to a <see cref="long"/>.
    /// </summary>
    [TypeConverter(typeof(LintConverter))]
    public sealed class LINT : AtomicType, IEquatable<LINT>, IComparable<LINT>
    {
        private readonly long _value;
        
        /// <summary>
        /// Creates a new default <see cref="LINT"/> type.
        /// </summary>
        public LINT() : base(nameof(LINT))
        {
        }
        
        /// <summary>
        /// Creates a new <see cref="LINT"/> value with the provided radix format.
        /// </summary>
        /// <param name="radix">The <see cref="Enums.Radix"/> number format of the value.</param>
        public LINT(Radix? radix) : base(nameof(LINT), radix)
        {
        }

        /// <summary>
        /// Creates a new <see cref="LINT"/> with the provided value.
        /// </summary>
        /// <param name="value">The value to initialize the type with.</param>
        /// <param name="radix">The optional radix format of the value.</param>
        public LINT(long value, Radix? radix = null) : this(radix)
        {
            _value = value;
        }

        /// <summary>
        /// Creates a new <see cref="LINT"/> with the provided string value.
        /// </summary>
        /// <param name="value">The string value to parse and convert to the value type.</param>
        /// <param name="radix">The optional radix format of the value. If not provided, will be inferred
        /// using <see cref="Enums.Radix.Infer"/>.</param>
        public LINT(string value, Radix? radix = null) : this(radix ?? Radix.Infer(value))
        {
            var converter = TypeDescriptor.GetConverter(GetType());
            _value = (long)(LINT)converter.ConvertFrom(value)!;
        }

        /// <summary>
        /// Represents the largest possible value of <see cref="LINT"/>.
        /// </summary>
        public const long MaxValue = long.MaxValue;
        
        /// <summary>
        /// Represents the smallest possible value of <see cref="LINT"/>.
        /// </summary>
        public const long MinValue = long.MinValue;

        /// <summary>
        /// Converts the provided <see cref="long"/> to a <see cref="LINT"/> value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A <see cref="LINT"/> value.</returns>
        public static implicit operator LINT(long value) => new(value);

        /// <summary>
        /// Converts the provided <see cref="LINT"/> to a <see cref="long"/> value.
        /// </summary>
        /// <param name="atomic">The value to convert.</param>
        /// <returns>A <see cref="long"/> type value.</returns>
        public static implicit operator long(LINT atomic) => atomic._value;
        
        /// <summary>
        /// Implicitly converts a <see cref="string"/> to a <see cref="LINT"/> value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A new <see cref="LINT"/> value.</returns>
        public static implicit operator LINT(string value) => new(value);
        
        /// <summary>
        /// Implicitly converts the provided <see cref="LINT"/> to a <see cref="string"/> value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A new <see cref="string"/> value.</returns>
        public static implicit operator string(LINT value) => value.ToString();

        /// <inheritdoc />
        public bool Equals(LINT? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _value == other._value;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj) => Equals(obj as LINT);

        /// <inheritdoc />
        // ReSharper disable once NonReadonlyMemberInGetHashCode
        // NOT sure how else to handle since it needs to be settable and used for equality.
        // This would only be a problem if you created a hash table of atomic types.
        // NOT sure anyone would need to do that.
        public override int GetHashCode() => _value.GetHashCode();

        /// <summary>
        /// Determines whether the objects are equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the objects are equal, otherwise, false.</returns>
        public static bool operator ==(LINT left, LINT right) => Equals(left, right);

        /// <summary>
        /// Determines whether the objects are not equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the objects are not equal, otherwise, false.</returns>
        public static bool operator !=(LINT left, LINT right) => !Equals(left, right);

        /// <inheritdoc />
        public int CompareTo(LINT? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            return ReferenceEquals(null, other) ? 1 : _value.CompareTo(other._value);
        }
    }
}