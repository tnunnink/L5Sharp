using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>AB_5000_String16_Struct_I_0</c> data type structure.
/// </summary>
[LogixData("AB:5000_String16_Struct:I:0")]
public sealed partial class AB_5000_String16_Struct_I_0 : StringData
{
    /// <summary>
    /// Creates a new <see cref="AB_5000_String16_Struct_I_0"/> instance initialized with default values.
    /// </summary>
    public AB_5000_String16_Struct_I_0() : base("AB:5000_String16_Struct:I:0")
    {
    }
    
    /// <summary>
    /// Creates a new <see cref="AB_5000_String16_Struct_I_0"/> instance initialized with the provided value.
    /// </summary>
    public AB_5000_String16_Struct_I_0(string value) : base("AB:5000_String16_Struct:I:0", value)
    {
    }
    
    /// <summary>
    /// Creates a new <see cref="AB_5000_String16_Struct_I_0"/> instance initialized with the provided element.
    /// </summary>
    public AB_5000_String16_Struct_I_0(XElement element) : base(element)
    {
    }

    /// <inheritdoc />
    public override int Capacity => 16;
}