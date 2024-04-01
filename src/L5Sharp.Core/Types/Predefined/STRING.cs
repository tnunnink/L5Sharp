using System;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// Represents a predefined String Logix data type.
/// </summary>
public sealed class STRING : StringType, ILogixParsable<STRING>
{
    /// <inheritdoc />
    public STRING(XElement element) : base(element)
    {
    }

    /// <summary>
    /// Creates a new empty <see cref="STRING"/> type.
    /// </summary>
    public STRING() : base(nameof(STRING), string.Empty)
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
    public STRING(string value) : base(nameof(STRING), value)
    {
    }

    /// <inheritdoc />
    //This is the length of the predefined STRING type in Logix
    protected override ushort MaxLength => 82;

    /// <summary>
    /// Parses the provided string into a <see cref="STRING"/> value.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <returns>A <see cref="STRING"/> representing the parsed value.</returns>
    public static STRING Parse(string value) => new(value);

    /// <summary>
    /// Tries to parse the provided string into a <see cref="STRING"/> value.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <returns>A <see cref="STRING"/> representing the parsed value if successful; Otherwise, <c>null</c>.</returns>
    public static STRING? TryParse(string? value) => value is not null ? new STRING(value) : null;

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
    public static implicit operator string(STRING input) => input.ToString();
}