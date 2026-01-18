using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>CHANNEL_DO_FT_I_0</c> data type structure.
/// </summary>
[LogixData("CHANNEL_DO_FT:I:0")]
public sealed partial class CHANNEL_DO_FT_I_0 : StructureData
{
    /// <summary>
    /// Creates a new <see cref="CHANNEL_DO_FT_I_0"/> instance initialized with default values.
    /// </summary>
    public CHANNEL_DO_FT_I_0() : base("CHANNEL_DO_FT:I:0")
    {
        Readback = new BOOL();
        Fault = new BOOL();
        Uncertain = new BOOL();
        NoLoad = new BOOL();
        ShortCircuit = new BOOL();
        FieldPowerOff = new BOOL();
        ReturnCurrentMismatch = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="CHANNEL_DO_FT_I_0"/> instance initialized with the provided element.
    /// </summary>
    public CHANNEL_DO_FT_I_0(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>Readback</c> member of the <see cref="CHANNEL_DO_FT_I_0"/> data type.
    /// </summary>
    public BOOL Readback
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Fault</c> member of the <see cref="CHANNEL_DO_FT_I_0"/> data type.
    /// </summary>
    public BOOL Fault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Uncertain</c> member of the <see cref="CHANNEL_DO_FT_I_0"/> data type.
    /// </summary>
    public BOOL Uncertain
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>NoLoad</c> member of the <see cref="CHANNEL_DO_FT_I_0"/> data type.
    /// </summary>
    public BOOL NoLoad
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ShortCircuit</c> member of the <see cref="CHANNEL_DO_FT_I_0"/> data type.
    /// </summary>
    public BOOL ShortCircuit
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FieldPowerOff</c> member of the <see cref="CHANNEL_DO_FT_I_0"/> data type.
    /// </summary>
    public BOOL FieldPowerOff
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ReturnCurrentMismatch</c> member of the <see cref="CHANNEL_DO_FT_I_0"/> data type.
    /// </summary>
    public BOOL ReturnCurrentMismatch
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}