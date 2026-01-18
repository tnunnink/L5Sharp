using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>STRING</c> data type structure.
/// </summary>
[LogixData("STRING")]
public sealed partial class STRING : StringData
{
    /// <summary>
    /// Creates a new <see cref="STRING"/> instance initialized with default values.
    /// </summary>
    public STRING() : base("STRING")
    {
    }
    
    /// <summary>
    /// Creates a new <see cref="STRING"/> instance initialized with the provided value.
    /// </summary>
    public STRING(string value) : base("STRING", value)
    {
    }
    
    /// <summary>
    /// Creates a new <see cref="STRING"/> instance initialized with the provided element.
    /// </summary>
    public STRING(XElement element) : base(element)
    {
    }

    /// <inheritdoc />
    public override int Capacity => 82;
    
    /// <summary>
    /// Defines an implicit conversion from a <see cref="StringData"/> instance to a <see cref="string"/>.
    /// </summary>
    /// <param name="value">The <see cref="StringData"/> instance to be converted.</param>
    /// <returns>A <see cref="string"/> representation of the <see cref="StringData"/> value.</returns>
    public static implicit operator string(STRING value) => value.ToString();

    /// <summary>
    /// Defines an implicit conversion from a <see cref="STRING"/> instance to a <see cref="string"/>.
    /// </summary>
    /// <param name="value">The <see cref="STRING"/> instance to be converted.</param>
    /// <returns>A <see cref="string"/> representation of the <see cref="STRING"/> value.</returns>
    public static implicit operator STRING(string value) => new(value);
}