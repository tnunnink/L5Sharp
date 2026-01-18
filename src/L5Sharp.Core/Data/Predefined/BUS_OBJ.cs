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