using System;
using System.Xml.Linq;
using L5Sharp.Enums;

namespace L5Sharp.Types.Predefined;

/// <summary>
/// Represents a predefined String Logix data type.
/// </summary>
public sealed class STRING : StringType, IEquatable<STRING>, IComparable<STRING>
{
    //This is the built in length of string types in Logix
    private const int PredefinedLength = 82;

    /// <summary>
    /// Creates a new empty <see cref="STRING"/> type.
    /// </summary>
    public STRING() : base(nameof(STRING), PredefinedLength, string.Empty)
    {
    }

    /// <inheritdoc />
    public STRING(XElement element) : base(element)
    {
    }

    /// <summary>
    /// Creates a new <see cref="STRING"/> with the provided value.
    /// </summary>
    /// <param name="value">The string value.</param>
    /// <exception cref="ArgumentNullException"><c>value</c> is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <c>value</c> length is greater than the predefined Logix string length of 82 characters.
    /// </exception>
    public STRING(string value) : base(nameof(STRING), PredefinedLength, value)
    {
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
    public static implicit operator string(STRING input) => input.Value;

    /// <inheritdoc />
    public bool Equals(STRING? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Value == other.Value;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj) => Equals(obj as STRING);

    /// <inheritdoc />
    public override int GetHashCode() => Value.GetHashCode();

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
        return ReferenceEquals(other, null) ? 1 : string.Compare(Value, Value, StringComparison.Ordinal);
    }
}