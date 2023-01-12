using System;
using System.ComponentModel;
using L5Sharp.Types.Atomics.Converters;

namespace L5Sharp.Types.Atomics
{
    /// <summary>
    /// Represents a <b>UINT</b> Logix atomic data type, or a type analogous to a <see cref="ushort"/>.
    /// </summary>
    [TypeConverter(typeof(UIntConverter))]
    public class UINT : AtomicType, IEquatable<UINT>, IComparable<UINT>
    {
        private readonly ushort _value;
        
        /// <summary>
        /// Creates a new default <see cref="UINT"/> type.
        /// </summary>
        public UINT() : base(nameof(UINT))
        {
        }

        /// <summary>
        /// Creates a new <see cref="UINT"/> with the provided value.
        /// </summary>
        /// <param name="value">The value to initialize the type with.</param>
        public UINT(ushort value) : this()
        {
            _value = value;
        }

        /// <summary>
        /// Represents the largest possible value of <see cref="UINT"/>.
        /// </summary>
        public const ushort MaxValue = ushort.MaxValue;
        
        /// <summary>
        /// Represents the smallest possible value of <see cref="UINT"/>.
        /// </summary>
        public const ushort MinValue = ushort.MinValue;

        /// <summary>
        /// Converts the provided <see cref="ushort"/> to a <see cref="UINT"/> value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A <see cref="UINT"/> value.</returns>
        public static implicit operator UINT(ushort value) => new(value);

        /// <summary>
        /// Converts the provided <see cref="UINT"/> to a <see cref="ushort"/> value.
        /// </summary>
        /// <param name="atomic">The value to convert.</param>
        /// <returns>A <see cref="ushort"/> type value.</returns>
        public static implicit operator ushort(UINT atomic) => atomic._value;
        
        /// <inheritdoc />
        public override string ToString() => _value.ToString();

        /// <inheritdoc />
        public bool Equals(UINT? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _value == other._value;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj) => Equals(obj as UINT);

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
        public static bool operator ==(UINT left, UINT right) => Equals(left, right);

        /// <summary>
        /// Determines whether the objects are not equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the objects are not equal, otherwise, false.</returns>
        public static bool operator !=(UINT left, UINT right) => !Equals(left, right);

        /// <inheritdoc />
        public int CompareTo(UINT? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            return ReferenceEquals(null, other) ? 1 : _value.CompareTo(other._value);
        }
    }
}