using System;
using System.ComponentModel;
using L5Sharp.Enums;
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
        /// Creates a new <see cref="UINT"/> value with the provided radix format.
        /// </summary>
        /// <param name="radix">The <see cref="Enums.Radix"/> number format of the value.</param>
        public UINT(Radix? radix) : base(nameof(UINT), radix)
        {
        }

        /// <summary>
        /// Creates a new <see cref="UINT"/> with the provided value.
        /// </summary>
        /// <param name="value">The value to initialize the type with.</param>
        /// <param name="radix">The optional radix format of the value.</param>
        public UINT(ushort value, Radix? radix = null) : this(radix)
        {
            _value = value;
        }

        /// <summary>
        /// Creates a new <see cref="UINT"/> with the provided string value.
        /// </summary>
        /// <param name="value">The string value to parse and convert to the value type.</param>
        /// <param name="radix">The optional radix format of the value. If not provided, will be inferred
        /// using <see cref="Enums.Radix.Infer"/>.</param>
        public UINT(string value, Radix? radix = null) : this(radix ?? Radix.Infer(value))
        {
            var converter = TypeDescriptor.GetConverter(GetType());
            _value = (ushort)(UINT)converter.ConvertFrom(value)!;
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
        
        /// <summary>
        /// Implicitly converts a <see cref="string"/> to a <see cref="UINT"/> value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A new <see cref="UINT"/> value.</returns>
        public static implicit operator UINT(string value) => new(value);
        
        /// <summary>
        /// Implicitly converts the provided <see cref="UINT"/> to a <see cref="string"/> value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A new <see cref="string"/> value.</returns>
        public static implicit operator UINT(LINT value) => value.ToString();

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