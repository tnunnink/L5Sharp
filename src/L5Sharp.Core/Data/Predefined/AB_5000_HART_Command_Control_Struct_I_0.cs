using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>AB_5000_HART_Command_Control_Struct_I_0</c> data type structure.
/// </summary>
[LogixData("AB:5000_HART_Command_Control_Struct:I:0")]
public sealed partial class AB_5000_HART_Command_Control_Struct_I_0 : StructureData
{
    /// <summary>
    /// Creates a new <see cref="AB_5000_HART_Command_Control_Struct_I_0"/> instance initialized with default values.
    /// </summary>
    public AB_5000_HART_Command_Control_Struct_I_0() : base("AB:5000_HART_Command_Control_Struct:I:0")
    {
        ReadyToExecute = new BOOL();
        Completed = new BOOL();
        Active = new BOOL();
        Overlap = new BOOL();
        ERR = new BOOL();
        Warning = new BOOL();
        ParameterError = new BOOL();
        ParameterErrorNumber = new SINT();
        ResponseCode = new SINT();
    }
    
    /// <summary>
    /// Creates a new <see cref="AB_5000_HART_Command_Control_Struct_I_0"/> instance initialized with the provided element.
    /// </summary>
    public AB_5000_HART_Command_Control_Struct_I_0(XElement element) : base(element)
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
        ReadyToExecute.UpdateData((data[offset + 2] & (1 << 0)) != 0);
        Completed.UpdateData((data[offset + 2] & (1 << 1)) != 0);
        Active.UpdateData((data[offset + 2] & (1 << 2)) != 0);
        Overlap.UpdateData((data[offset + 2] & (1 << 3)) != 0);
        ERR.UpdateData((data[offset + 2] & (1 << 4)) != 0);
        Warning.UpdateData((data[offset + 2] & (1 << 5)) != 0);
        ParameterError.UpdateData((data[offset + 2] & (1 << 6)) != 0);
        ParameterErrorNumber.UpdateData(data, offset + 2);
        ResponseCode.UpdateData(data, offset + 3);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>ReadyToExecute</c> member of the <see cref="AB_5000_HART_Command_Control_Struct_I_0"/> data type.
    /// </summary>
    public BOOL ReadyToExecute
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Completed</c> member of the <see cref="AB_5000_HART_Command_Control_Struct_I_0"/> data type.
    /// </summary>
    public BOOL Completed
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Active</c> member of the <see cref="AB_5000_HART_Command_Control_Struct_I_0"/> data type.
    /// </summary>
    public BOOL Active
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Overlap</c> member of the <see cref="AB_5000_HART_Command_Control_Struct_I_0"/> data type.
    /// </summary>
    public BOOL Overlap
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ERR</c> member of the <see cref="AB_5000_HART_Command_Control_Struct_I_0"/> data type.
    /// </summary>
    public BOOL ERR
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Warning</c> member of the <see cref="AB_5000_HART_Command_Control_Struct_I_0"/> data type.
    /// </summary>
    public BOOL Warning
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ParameterError</c> member of the <see cref="AB_5000_HART_Command_Control_Struct_I_0"/> data type.
    /// </summary>
    public BOOL ParameterError
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ParameterErrorNumber</c> member of the <see cref="AB_5000_HART_Command_Control_Struct_I_0"/> data type.
    /// </summary>
    public SINT ParameterErrorNumber
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ResponseCode</c> member of the <see cref="AB_5000_HART_Command_Control_Struct_I_0"/> data type.
    /// </summary>
    public SINT ResponseCode
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }
}