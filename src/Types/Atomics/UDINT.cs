using System;
using System.ComponentModel;
using L5Sharp.Enums;
using L5Sharp.Types.Atomics.Converters;

namespace L5Sharp.Types.Atomics
{
    /// <summary>
    /// Represents a <b>UDINT</b> Logix atomic data type, or a type analogous to a <see cref="uint"/>.
    /// </summary>
    [TypeConverter(typeof(UDintConverter))]
    public class UDINT : AtomicType, IEquatable<UDINT>, IComparable<UDINT>
    {
        private readonly uint _value;
        
        /// <summary>
        /// Creates a new default <see cref="UDINT"/> type.
        /// </summary>
        public UDINT() : base(nameof(UDINT))
        {
        }

        /// <summary>
        /// Creates a new <see cref="UDINT"/> with the provided value.
        /// </summary>
        /// <param name="value">The value to initialize the type with.</param>
        public UDINT(uint value) : this()
        {
            _value = value;
        }

        /// <summary>
        /// Represents the largest possible value of <see cref="UDINT"/>.
        /// </summary>
        public const uint MaxValue = uint.MaxValue;
        
        /// <summary>
        /// Represents the smallest possible value of <see cref="UDINT"/>.
        /// </summary>
        public const uint MinValue = uint.MinValue;

        /// <summary>
        /// Converts the provided <see cref="uint"/> to a <see cref="UDINT"/> value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A <see cref="UDINT"/> value.</returns>
        public static implicit operator UDINT(uint value) => new(value);

        /// <summary>
        /// Converts the provided <see cref="UDINT"/> to a <see cref="uint"/> value.
        /// </summary>
        /// <param name="atomic">The value to convert.</param>
        /// <returns>A <see cref="uint"/> type value.</returns>
        public static implicit operator uint(UDINT atomic) => atomic._value;
        
        /// <inheritdoc />
        public override string ToString() => _value.ToString();

        /// <inheritdoc />
        public bool Equals(UDINT? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _value == other._value;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj) => Equals(obj as UDINT);

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
        public static bool operator ==(UDINT left, UDINT right) => Equals(left, right);

        /// <summary>
        /// Determines whether the objects are not equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the objects are not equal, otherwise, false.</returns>
        public static bool operator !=(UDINT left, UDINT right) => !Equals(left, right);

        /// <inheritdoc />
        public int CompareTo(UDINT? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            return ReferenceEquals(null, other) ? 1 : _value.CompareTo(other._value);
        }
    }
}