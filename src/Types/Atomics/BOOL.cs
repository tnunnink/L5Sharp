using System;
using System.ComponentModel;
using L5Sharp.Enums;
using L5Sharp.Types.Atomics.Converters;

namespace L5Sharp.Types.Atomics
{
    /// <summary>
    /// Represents a <i>BOOL</i> Logix atomic data type, or a type analogous to a <see cref="bool"/>.
    /// </summary>
    [TypeConverter(typeof(BoolConverter))]
    public sealed class BOOL : AtomicType, IEquatable<BOOL>, IComparable<BOOL>
    {
        private readonly bool _value;

        /// <summary>
        /// Creates a new default <see cref="BOOL"/> type.
        /// </summary>
        public BOOL() : base(nameof(BOOL))
        {
        }
        
        /// <summary>
        /// Creates a new <see cref="BOOL"/> value with the provided radix format.
        /// </summary>
        /// <param name="radix">The <see cref="Enums.Radix"/> number format of the value.</param>
        public BOOL(Radix? radix) : base(nameof(BOOL), radix)
        {
        }

        /// <summary>
        /// Creates a new <see cref="BOOL"/> with the provided value.
        /// </summary>
        /// <param name="value">The value to initialize the type with.</param>
        /// <param name="radix"></param>
        public BOOL(bool value, Radix? radix = null) : this(radix)
        {
            _value = value;
        }
        
        /// <summary>
        /// Creates a new <see cref="BOOL"/> with the provided value.
        /// </summary>
        /// <param name="value">The value to initialize the type with.</param>
        /// <param name="radix"></param>
        public BOOL(string value, Radix? radix = null) : this(radix ?? Radix.Infer(value))
        {
            var converter = TypeDescriptor.GetConverter(GetType());
            var converted = converter.ConvertFrom(value);

            _value = converted switch
            {
                BOOL b => b,
                SINT v => v != 0,
                INT v => v != 0,
                DINT v => v != 0,
                LINT v => v != 0,
                _ => _value
            };
        }

        /// <summary>
        /// Creates a new instance of a BOOL with the provided number.
        /// </summary>
        /// <param name="number">A number that will be evaluated to true if greater that zero, otherwise false</param>
        public BOOL(int number) : this()
        {
            _value = number != 0;
        }

        /// <summary>
        /// Implicitly converts the provided <see cref="bool"/> to a <see cref="BOOL"/> value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A <see cref="BOOL"/> value.</returns>
        public static implicit operator BOOL(bool value) => new(value);

        /// <summary>
        /// Implicitly converts the provided <see cref="BOOL"/> to a <see cref="bool"/> value.
        /// </summary>
        /// <param name="atomic">The value to convert.</param>
        /// <returns>A <see cref="bool"/> type value.</returns>
        public static implicit operator bool(BOOL atomic) => atomic._value;
        
        /// <summary>
        /// Implicitly converts the provided <see cref="bool"/> to a <see cref="BOOL"/> value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A <see cref="BOOL"/> value.</returns>
        public static implicit operator BOOL(int value) => new(value);

        /// <summary>
        /// Implicitly converts the provided <see cref="BOOL"/> to a <see cref="bool"/> value.
        /// </summary>
        /// <param name="atomic">The value to convert.</param>
        /// <returns>A <see cref="bool"/> type value.</returns>
        public static implicit operator int(BOOL atomic) => atomic._value ? 1 : 0;
        
        /// <summary>
        /// Implicitly converts a <see cref="string"/> to a <see cref="BOOL"/> value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A new <see cref="BOOL"/> value.</returns>
        public static implicit operator BOOL(string value) => new(value);
        
        /// <summary>
        /// Implicitly converts the provided <see cref="BOOL"/> to a <see cref="string"/> value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A new <see cref="string"/> value.</returns>
        public static implicit operator string(BOOL value) => value.ToString();

        /// <inheritdoc />
        public bool Equals(BOOL? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _value == other._value;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj) => Equals(obj as BOOL);

        /// <inheritdoc />
        public override int GetHashCode() => _value.GetHashCode();

        /// <summary>
        /// Determines whether the objects are equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the objects are equal, otherwise, false.</returns>
        public static bool operator ==(BOOL? left, BOOL? right) => Equals(left, right);

        /// <summary>
        /// Determines whether the objects are not equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the objects are not equal, otherwise, false.</returns>
        public static bool operator !=(BOOL? left, BOOL? right) => !Equals(left, right);

        /// <inheritdoc />
        public int CompareTo(BOOL? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            return ReferenceEquals(null, other) ? 1 : _value.CompareTo(other._value);
        }
    }
}