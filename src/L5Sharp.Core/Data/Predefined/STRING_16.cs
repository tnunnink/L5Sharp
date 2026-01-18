using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>STRING_16</c> data type structure.
/// </summary>
[LogixData("STRING_16")]
public sealed partial class STRING_16 : StringData
{
    /// <summary>
    /// Creates a new <see cref="STRING_16"/> instance initialized with default values.
    /// </summary>
    public STRING_16() : base("STRING_16")
    {
    }
    
    /// <summary>
    /// Creates a new <see cref="STRING_16"/> instance initialized with the provided value.
    /// </summary>
    public STRING_16(string value) : base("STRING_16", value)
    {
    }
    
    /// <summary>
    /// Creates a new <see cref="STRING_16"/> instance initialized with the provided element.
    /// </summary>
    public STRING_16(XElement element) : base(element)
    {
    }

    /// <inheritdoc />
    public override int Capacity => 16;
}