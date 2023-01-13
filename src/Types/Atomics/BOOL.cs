﻿using System;
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
        /// Creates a new <see cref="BOOL"/> with the provided value.
        /// </summary>
        /// <param name="value">The value to initialize the type with.</param>
        public BOOL(bool value) : this()
        {
            _value = value;
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
        /// Converts the provided <see cref="bool"/> to a <see cref="BOOL"/> value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A <see cref="BOOL"/> value.</returns>
        public static implicit operator BOOL(bool value) => new(value);

        /// <summary>
        /// Converts the provided <see cref="BOOL"/> to a <see cref="bool"/> value.
        /// </summary>
        /// <param name="atomic">The value to convert.</param>
        /// <returns>A <see cref="bool"/> type value.</returns>
        public static implicit operator bool(BOOL atomic) => atomic._value;

        public static BOOL Parse(string value, Radix? radix = null)
        {
            radix ??= Radix.Decimal;

            var atomic = radix.Parse(value);

            var converter = TypeDescriptor.GetConverter(typeof(BOOL));

            return (BOOL)converter.ConvertFrom(atomic)!;
        }

        /// <inheritdoc />
        public override string ToString() => _value ? "1" : "0";

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