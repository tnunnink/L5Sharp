using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>BUS_OBJ</c> data type structure.
/// </summary>
[LogixData("BUS_OBJ")]
public sealed partial class BUS_OBJ : StructureData
{
    /// <summary>
    /// Creates a new <see cref="BUS_OBJ"/> instance initialized with default values.
    /// </summary>
    public BUS_OBJ() : base("BUS_OBJ")
    {
        Inp_Cmd = new DINT();
        Out_Cmd = new DINT();
        Inp_CmdLLH = new DINT();
        Out_CmdLLH = new DINT();
        Inp_Sts = new DINT();
        Out_Sts = new DINT();
        Inp_CmdAck = new DINT();
        Out_CmdAck = new DINT();
        Inp_SeverityMax = new DINT();
        Out_SeverityMax = new DINT();
        Cfg_CmdMask = new DINT();
        Cfg_CmdLLHMask = new DINT();
        Cfg_StsMask = new DINT();
        Ref_Index = new DINT();
    }
    
    /// <summary>
    /// Creates a new <see cref="BUS_OBJ"/> instance initialized with the provided element.
    /// </summary>
    public BUS_OBJ(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 56;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        Inp_Cmd.UpdateData(data, offset + 0);
        Out_Cmd.UpdateData(data, offset + 4);
        Inp_CmdLLH.UpdateData(data, offset + 8);
        Out_CmdLLH.UpdateData(data, offset + 12);
        Inp_Sts.UpdateData(data, offset + 16);
        Out_Sts.UpdateData(data, offset + 20);
        Inp_CmdAck.UpdateData(data, offset + 24);
        Out_CmdAck.UpdateData(data, offset + 28);
        Inp_SeverityMax.UpdateData(data, offset + 32);
        Out_SeverityMax.UpdateData(data, offset + 36);
        Cfg_CmdMask.UpdateData(data, offset + 40);
        Cfg_CmdLLHMask.UpdateData(data, offset + 44);
        Cfg_StsMask.UpdateData(data, offset + 48);
        Ref_Index.UpdateData(data, offset + 52);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>Inp_Cmd</c> member of the <see cref="BUS_OBJ"/> data type.
    /// </summary>
    public DINT Inp_Cmd
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_Cmd</c> member of the <see cref="BUS_OBJ"/> data type.
    /// </summary>
    public DINT Out_Cmd
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_CmdLLH</c> member of the <see cref="BUS_OBJ"/> data type.
    /// </summary>
    public DINT Inp_CmdLLH
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_CmdLLH</c> member of the <see cref="BUS_OBJ"/> data type.
    /// </summary>
    public DINT Out_CmdLLH
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Sts</c> member of the <see cref="BUS_OBJ"/> data type.
    /// </summary>
    public DINT Inp_Sts
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_Sts</c> member of the <see cref="BUS_OBJ"/> data type.
    /// </summary>
    public DINT Out_Sts
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_CmdAck</c> member of the <see cref="BUS_OBJ"/> data type.
    /// </summary>
    public DINT Inp_CmdAck
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_CmdAck</c> member of the <see cref="BUS_OBJ"/> data type.
    /// </summary>
    public DINT Out_CmdAck
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_SeverityMax</c> member of the <see cref="BUS_OBJ"/> data type.
    /// </summary>
    public DINT Inp_SeverityMax
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_SeverityMax</c> member of the <see cref="BUS_OBJ"/> data type.
    /// </summary>
    public DINT Out_SeverityMax
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CmdMask</c> member of the <see cref="BUS_OBJ"/> data type.
    /// </summary>
    public DINT Cfg_CmdMask
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CmdLLHMask</c> member of the <see cref="BUS_OBJ"/> data type.
    /// </summary>
    public DINT Cfg_CmdLLHMask
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_StsMask</c> member of the <see cref="BUS_OBJ"/> data type.
    /// </summary>
    public DINT Cfg_StsMask
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Ref_Index</c> member of the <see cref="BUS_OBJ"/> data type.
    /// </summary>
    public DINT Ref_Index
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }
}