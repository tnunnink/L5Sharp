using System;
using L5Sharp.Abstractions;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class StringDefined : StringType, IEquatable<StringDefined>, IComparable<StringDefined>
    {
        /// <summary>
        /// Creates a new <see cref="StringDefined"/> with the provided arguments.
        /// </summary>
        /// <param name="name">The name of the type.</param>
        /// <param name="length">The dimensional length of the type.</param>
        /// <param name="description">The description of the type.</param>
        public StringDefined(string name, ushort length, string? description = null)
            : base(name, length)
        {
            Description = description ?? string.Empty;
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