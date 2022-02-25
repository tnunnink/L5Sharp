using System;
using L5Sharp.Abstractions;
using L5Sharp.Enums;

namespace L5Sharp.Predefined
{
    /// <summary>
    /// Represents a predefined String Logix data type.
    /// </summary>
    public sealed class String : StringType, IEquatable<String>, IComparable<String>
    {
        //This is the built in length of string types in Logix
        private const int PredefinedLength = 82; 

        /// <summary>
        /// Creates a new <see cref="String"/> default value type.
        /// </summary>
        public String() : base(nameof(String).ToUpper(), PredefinedLength)
        {
        }

        /// <summary>
        /// Creates a new <see cref="string"/> with the provided string value.
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public String(string value) : this()
        {
            SetValue(value);
        }

        /// <inheritdoc />
        public override string Description => $"Logix representation of a {typeof(string)}";

        /// <inheritdoc />
        public override DataTypeClass Class => DataTypeClass.Predefined;

        /// <inheritdoc />
        protected override IDataType New() => new String();

        /// <summary>
        /// Converts the provided <see cref="string"/> to a <see cref="String"/> value.
        /// </summary>
        /// <param name="input">The value to convert.</param>
        /// <returns>A <see cref="String"/> type value.</returns>
        public static implicit operator String(string input) => new(input);

        /// <summary>
        /// Converts the provided <see cref="String"/> to a <see cref="string"/> value.
        /// </summary>
        /// <param name="input">The value to convert.</param>
        /// <returns>A <see cref="string"/> type value.</returns>
        public static implicit operator string(String input) => input.Value;

        /// <inheritdoc />
        public bool Equals(String? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value == other.Value;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj) => Equals(obj as String);

        /// <inheritdoc />
        public override int GetHashCode() => Value.GetHashCode();

        /// <summary>
        /// Determines if the provided objects are equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the provided objects are equal; otherwise, false.</returns>
        public static bool operator ==(String left, String right) => Equals(left, right);

        /// <summary>
        /// Determines if the provided objects not are equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the provided objects are not equal; otherwise, false.</returns>
        public static bool operator !=(String left, String right) => !Equals(left, right);

        /// <inheritdoc />
        public int CompareTo(String? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            return ReferenceEquals(other, null) ? 1 : string.Compare(Value, other.Value, StringComparison.Ordinal);
        }
    }
}