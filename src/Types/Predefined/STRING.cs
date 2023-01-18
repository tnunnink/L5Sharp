using System;
using System.Collections;
using System.Collections.Generic;
using L5Sharp.Enums;

namespace L5Sharp.Types.Predefined
{
    /// <summary>
    /// Represents a predefined String Logix data type.
    /// </summary>
    public sealed class STRING : StringType, IEquatable<STRING>, IComparable<STRING>, IEnumerable<char>
    {
        //This is the built in length of string types in Logix
        private const int PredefinedLength = 82;

        private readonly string _value;

        /// <summary>
        /// Creates a new empty <see cref="STRING"/> type.
        /// </summary>
        public STRING() : base(nameof(STRING), string.Empty)
        {
            _value = string.Empty;
        }

        /// <summary>
        /// Creates a new <see cref="STRING"/> with the provided value.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <exception cref="ArgumentNullException"><c>value</c> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <c>value</c> length is greater than the predefined Logix string length of 82 characters.
        /// </exception>
        public STRING(string value) : base(nameof(STRING), value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            if (value.Length > PredefinedLength)
                throw new ArgumentOutOfRangeException(
                    $"The length {value.Length} of value can not be greater than {PredefinedLength} characters.");

            _value = value;
        }

        /// <inheritdoc />
        public override DataTypeClass Class => DataTypeClass.Predefined;

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