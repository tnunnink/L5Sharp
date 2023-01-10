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
        /// Creates a new <see cref="LINT"/> with the provided value.
        /// </summary>
        /// <param name="value">The value to initialize the type with.</param>
        public LINT(long value) : this()
        {
            _value = value;
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