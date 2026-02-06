using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>AB_5000_HART_Command_Control_Struct_O_0</c> data type structure.
/// </summary>
[LogixData("AB:5000_HART_Command_Control_Struct:O:0")]
public sealed partial class AB_5000_HART_Command_Control_Struct_O_0 : StructureData
{
    /// <summary>
    /// Creates a new <see cref="AB_5000_HART_Command_Control_Struct_O_0"/> instance initialized with default values.
    /// </summary>
    public AB_5000_HART_Command_Control_Struct_O_0() : base("AB:5000_HART_Command_Control_Struct:O:0")
    {
        Execute = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="AB_5000_HART_Command_Control_Struct_O_0"/> instance initialized with the provided element.
    /// </summary>
    public AB_5000_HART_Command_Control_Struct_O_0(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 4;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        Execute.UpdateData((data[offset + 2] & (1 << 0)) != 0);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>Execute</c> member of the <see cref="AB_5000_HART_Command_Control_Struct_O_0"/> data type.
    /// </summary>
    public BOOL Execute
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}