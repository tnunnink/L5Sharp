using System;
using System.ComponentModel;
using L5Sharp.Types.Atomics.Converters;

namespace L5Sharp.Types.Atomics
{
    /// <summary>
    /// Represents a <b>USINT</b> Logix atomic data type, or a type analogous to a <see cref="byte"/>.
    /// </summary>
    [TypeConverter(typeof(USintConverter))]
    public sealed class USINT  : AtomicType, IEquatable<USINT>, IComparable<USINT>
    {
        private readonly byte _value;
        
        /// <summary>
        /// Creates a new default <see cref="USINT"/> type.
        /// </summary>
        public USINT() : base(nameof(USINT))
        {
        }

        /// <summary>
        /// Creates a new <see cref="USINT"/> with the provided value.
        /// </summary>
        /// <param name="value">The value to initialize the type with.</param>
        public USINT(byte value) : this()
        {
            _value = value;
        }

        /// <summary>
        /// Represents the largest possible value of <see cref="USINT"/>.
        /// </summary>
        public const byte MaxValue = byte.MaxValue;
        
        /// <summary>
        /// Represents the smallest possible value of <see cref="USINT"/>.
        /// </summary>
        public const byte MinValue = byte.MinValue;

        /// <summary>
        /// Converts the provided <see cref="byte"/> to a <see cref="SINT"/> value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A <see cref="SINT"/> value.</returns>
        public static implicit operator USINT(byte value) => new(value);

        /// <summary>
        /// Converts the provided <see cref="SINT"/> to a <see cref="byte"/> value.
        /// </summary>
        /// <param name="atomic">The value to convert.</param>
        /// <returns>A <see cref="byte"/> type value.</returns>
        public static implicit operator byte(USINT atomic) => atomic._value;

        /// <inheritdoc />
        public bool Equals(USINT? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _value == other._value;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj) => Equals(obj as USINT);
        
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
        public static bool operator ==(USINT left, USINT right) => Equals(left, right);

        /// <summary>
        /// Determines whether the objects are not equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the objects are not equal, otherwise, false.</returns>
        public static bool operator !=(USINT left, USINT right) => !Equals(left, right);

        /// <inheritdoc />
        public int CompareTo(USINT? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            return ReferenceEquals(null, other) ? 1 : _value.CompareTo(other._value);
        }
    }
}