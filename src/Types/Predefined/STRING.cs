using System;
using System.Collections;
using System.Collections.Generic;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Types.Atomics;
// ReSharper disable InconsistentNaming

namespace L5Sharp.Types.Predefined
{
    /// <summary>
    /// Represents a predefined String Logix data type.
    /// </summary>
    public sealed class STRING : StructureType, IEquatable<STRING>, IComparable<STRING>, IEnumerable<char>
    {
        //This is the built in length of string types in Logix
        private const int PredefinedLength = 82;

        private readonly string _value;

        /// <summary>
        /// Creates a new empty <see cref="STRING"/> type.
        /// </summary>
        public STRING() : base(nameof(STRING))
        {
            _value = string.Empty;

            LEN = new DINT();
            DATA = new SINT[]{};
        }

        /// <summary>
        /// Creates a new <see cref="STRING"/> with the provided value.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <exception cref="ArgumentNullException"><c>value</c> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <c>value</c> length is greater than the predefined Logix string length of 82 characters.
        /// </exception>
        public STRING(string value) : base(nameof(STRING))
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));
            
            if (value.Length > PredefinedLength)
                throw new ArgumentOutOfRangeException("");
            
            _value = value;

            LEN = new DINT(_value.Length);
            DATA = _value.ToSintArray();
        }
        
        /// <inheritdoc />
        public override DataTypeFamily Family => DataTypeFamily.String;

        /// <inheritdoc />
        public override DataTypeClass Class => DataTypeClass.Predefined;
        
        /// <summary>
        /// Gets the character length value of the string. 
        /// </summary>
        /// <returns>A <see cref="DINT"/> logix atomic value representing the integer length of the string.</returns>
        public DINT LEN { get; }
        
        /// <summary>
        /// Gets the array of bytes that represent the ASCII encoded string value.
        /// </summary>
        /// <returns>An array of <see cref="SINT"/> logix atomic values representing the bytes of the string.</returns>
        public SINT[] DATA { get; }

        /// <summary>
        /// Converts the provided <see cref="string"/> to a <see cref="STRING"/> value.
        /// </summary>
        /// <param name="input">The value to convert.</param>
        /// <returns>A <see cref="STRING"/> type value.</returns>
        public static implicit operator STRING(string input) => new(input);

        /// <summary>
        /// Converts the provided <see cref="STRING"/> to a <see cref="string"/> value.
        /// </summary>
        /// <param name="input">The value to convert.</param>
        /// <returns>A <see cref="string"/> type value.</returns>
        public static implicit operator string(STRING input) => input._value;

        /// <inheritdoc />
        public bool Equals(STRING? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _value == other._value;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj) => Equals(obj as STRING);

        /// <inheritdoc />
        public override int GetHashCode() => _value.GetHashCode();

        /// <inheritdoc />
        public override string ToString() => Name;

        /// <summary>
        /// Determines if the provided objects are equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the provided objects are equal; otherwise, false.</returns>
        public static bool operator ==(STRING left, STRING right) => Equals(left, right);

        /// <summary>
        /// Determines if the provided objects not are equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the provided objects are not equal; otherwise, false.</returns>
        public static bool operator !=(STRING left, STRING right) => !Equals(left, right);

        /// <inheritdoc />
        public int CompareTo(STRING? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            return ReferenceEquals(other, null) ? 1 : string.Compare(_value, other._value, StringComparison.Ordinal);
        }

        /// <inheritdoc />
        public IEnumerator<char> GetEnumerator() => _value.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}