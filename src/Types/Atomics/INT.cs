using System;
using System.ComponentModel;
using System.Runtime.InteropServices.ComTypes;
using L5Sharp.Enums;
using L5Sharp.Types.Atomics.Converters;

namespace L5Sharp.Types.Atomics
{
    /// <summary>
    /// Represents a <b>INT</b> Logix atomic data type, or a type analogous to a <see cref="short"/>.
    /// </summary>
    [TypeConverter(typeof(IntConverter))]
    public sealed class INT : AtomicType, IEquatable<INT>, IComparable<INT>
    {
        private readonly short _value;

        /// <summary>
        /// Creates a new default <see cref="INT"/> type.
        /// </summary>
        /// <param name="radix">The optional radix format of the value.</param>
        public INT(Radix? radix = null) : base(nameof(INT), radix)
        {
        }

        /// <summary>
        /// Creates a new <see cref="INT"/> with the provided value.
        /// </summary>
        /// <param name="value">The value to initialize the type with.</param>
        /// <param name="radix">The optional radix format of the value.</param>
        public INT(short value, Radix? radix = null) : this(radix)
        {
            _value = value;
        }

        /// <summary>
        /// Creates a new <see cref="INT"/> with the provided string value.
        /// </summary>
        /// <param name="value">The string value to parse and convert to the value type.</param>
        /// <param name="radix">The optional radix format of the value. If not provided, will be inferred
        /// using <see cref="Enums.Radix.Infer"/>.</param>
        public INT(string value, Radix? radix = null) : this(radix ?? Radix.Infer(value))
        {
            var converter = TypeDescriptor.GetConverter(GetType());
            _value = (short)(INT)converter.ConvertFrom(value)!;
        }

        /// <summary>
        /// Represents the largest possible value of <see cref="INT"/>.
        /// </summary>
        public const short MaxValue = short.MaxValue;

        /// <summary>
        /// Represents the smallest possible value of <see cref="INT"/>.
        /// </summary>
        public const short MinValue = short.MinValue;

        /// <summary>
        /// Converts the provided <see cref="short"/> to a <see cref="INT"/> value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A <see cref="INT"/> value.</returns>
        public static implicit operator INT(short value) => new(value);

        /// <summary>
        /// Converts the provided <see cref="INT"/> to a <see cref="short"/> value.
        /// </summary>
        /// <param name="atomic">The value to convert.</param>
        /// <returns>A <see cref="short"/> type value.</returns>
        public static implicit operator short(INT atomic) => atomic._value;
        
        /// <summary>
        /// Implicitly converts a <see cref="string"/> to a <see cref="INT"/> value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A new <see cref="INT"/> value.</returns>
        public static implicit operator INT(string value) => new(value);
        
        /// <summary>
        /// Implicitly converts the provided <see cref="INT"/> to a <see cref="string"/> value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A new <see cref="string"/> value.</returns>
        public static implicit operator string(INT value) => value.ToString();

        /// <inheritdoc />
        public bool Equals(INT? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _value == other._value;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj) => Equals(obj as INT);

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
        public static bool operator ==(INT left, INT right) => Equals(left, right);

        /// <summary>
        /// Determines whether the objects are not equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the objects are not equal, otherwise, false.</returns>
        public static bool operator !=(INT left, INT right) => !Equals(left, right);

        /// <inheritdoc />
        public int CompareTo(INT? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            return ReferenceEquals(null, other) ? 1 : _value.CompareTo(other._value);
        }
    }
}