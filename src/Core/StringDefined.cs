using System;
using L5Sharp.Abstractions;
using L5Sharp.Enums;
using L5Sharp.Extensions;

namespace L5Sharp.Core
{
    /// <summary>
    /// An <see cref="IStringType"/> object that represents a user defined data type.
    /// </summary>
    /// <remarks>
    /// This type allows the user to set the length of the string to to allow for longer or shorter string types.
    /// This class also implements the <see cref="IEquatable{T}"/> and <see cref="IComparable{T}"/> in order to
    /// resemble a value type.
    /// </remarks>
    public sealed class StringDefined : StringType, IEquatable<StringDefined>, IComparable<StringDefined>
    {
        /// <summary>
        /// Creates a new <see cref="StringDefined"/> with the provided arguments.
        /// </summary>
        /// <param name="name">The name of the string type.</param>
        /// <param name="dimensions">The dimensional length of the string type.</param>
        /// <param name="description">The description of the string type. Will default to empty string.</param>
        /// <remarks>
        /// This constructor uses the provided dimensions value to set the maximum length of the string.
        /// After construction, the length can not be altered, and the string value must always be less than or equal to
        /// the size of the dimensions.
        /// </remarks>
        public StringDefined(string name, ushort dimensions, string? description = null)
            : base(name, dimensions)
        {
            Description = description ?? string.Empty;
        }
        
        /// <summary>
        /// Creates a new <see cref="StringDefined"/> object with the provided name and value.
        /// </summary>
        /// <param name="name">The name of the string type.</param>
        /// <param name="value">The value ot the string type.</param>
        /// <param name="description">The description of the string type. Will default to empty string.</param>
        /// <remarks>
        /// This constructor will infer the maximum length or dimensions based on the current length of the value
        /// parameter. This means that attempting to set the string value to a longer string will fail after constructed.
        /// To specify the length of the string type use alternate <see cref="StringDefined"/> overload.
        /// </remarks>
        public StringDefined(string name, string value, string? description = null)
            : base(name, value.ToArrayType().Dimensions.X)
        {
            Description = description ?? string.Empty;
            SetValue(value);
        }

        /// <inheritdoc />
        public override string Description { get; }

        /// <inheritdoc />
        public override DataTypeClass Class => DataTypeClass.User;

        /// <inheritdoc />
        protected override IDataType New() =>
            new StringDefined(string.Copy(Name), (ushort)LEN.DataType.Value, string.Copy(Description));

        /// <inheritdoc />
        public bool Equals(StringDefined? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value == other.Value;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj) => Equals(obj as StringDefined);

        /// <inheritdoc />
        public override int GetHashCode() => Value.GetHashCode();

        /// <summary>
        /// Determines if the provided objects are equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the provided objects are equal; otherwise, false.</returns>
        public static bool operator ==(StringDefined? left, StringDefined? right) => Equals(left, right);

        /// <summary>
        /// Determines if the provided objects are not equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the provided objects are not equal; otherwise, false.</returns>
        public static bool operator !=(StringDefined? left, StringDefined? right) => !Equals(left, right);
        
        /// <inheritdoc />
        public int CompareTo(StringDefined? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            return ReferenceEquals(other, null) ? 1 : string.Compare(Value, other.Value, StringComparison.Ordinal);
        }
    }
}