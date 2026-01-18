using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>STRING_32</c> data type structure.
/// </summary>
[LogixData("STRING_32")]
public sealed partial class STRING_32 : StringData
{
    /// <summary>
    /// Creates a new <see cref="STRING_32"/> instance initialized with default values.
    /// </summary>
    public STRING_32() : base("STRING_32")
    {
    }
    
    /// <summary>
    /// Creates a new <see cref="STRING_32"/> instance initialized with the provided value.
    /// </summary>
    public STRING_32(string value) : base("STRING_32", value)
    {
    }
    
    /// <summary>
    /// Creates a new <see cref="STRING_32"/> instance initialized with the provided element.
    /// </summary>
    public STRING_32(XElement element) : base(element)
    {
    }

    /// <inheritdoc />
    public override int Capacity => 32;
}