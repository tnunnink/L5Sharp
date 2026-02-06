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
    protected override int Capacity => 16;
    
    /// <summary>
    /// Defines an implicit conversion to a native <c>string</c> type.
    /// </summary>
    /// <param name="value">The instance to be converted.</param>
    /// <returns>A <see cref="string"/> representation of the value.</returns>
    public static implicit operator string(AB_5000_String16_Struct_I_0 value) => value.ToString();
    
    /// <summary>
    /// Defines an implicit conversion from a native <c>string</c> type.
    /// </summary>
    /// <param name="value">The instance to be converted.</param>
    /// <returns>A <see cref="AB_5000_String16_Struct_I_0"/> representation of the value.</returns>
    public static implicit operator AB_5000_String16_Struct_I_0(string value) => new(value);
}