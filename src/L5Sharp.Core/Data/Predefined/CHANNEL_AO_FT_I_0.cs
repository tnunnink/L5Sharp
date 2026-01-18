using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>CHANNEL_AO_FT_I_0</c> data type structure.
/// </summary>
[LogixData("CHANNEL_AO_FT:I:0")]
public sealed partial class CHANNEL_AO_FT_I_0 : StructureData
{
    /// <summary>
    /// Creates a new <see cref="CHANNEL_AO_FT_I_0"/> instance initialized with default values.
    /// </summary>
    public CHANNEL_AO_FT_I_0() : base("CHANNEL_AO_FT:I:0")
    {
        Fault = new BOOL();
        Uncertain = new BOOL();
        NoLoad = new BOOL();
        ShortCircuit = new BOOL();
        OverTemperature = new BOOL();
        FieldPowerOff = new BOOL();
        NotANumber = new BOOL();
        Underrange = new BOOL();
        Overrange = new BOOL();
        Data = new REAL();
        RollingTimestamp = new INT();
    }
    
    /// <summary>
    /// Creates a new <see cref="CHANNEL_AO_FT_I_0"/> instance initialized with the provided element.
    /// </summary>
    public CHANNEL_AO_FT_I_0(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>Fault</c> member of the <see cref="CHANNEL_AO_FT_I_0"/> data type.
    /// </summary>
    public BOOL Fault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Uncertain</c> member of the <see cref="CHANNEL_AO_FT_I_0"/> data type.
    /// </summary>
    public BOOL Uncertain
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>NoLoad</c> member of the <see cref="CHANNEL_AO_FT_I_0"/> data type.
    /// </summary>
    public BOOL NoLoad
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ShortCircuit</c> member of the <see cref="CHANNEL_AO_FT_I_0"/> data type.
    /// </summary>
    public BOOL ShortCircuit
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OverTemperature</c> member of the <see cref="CHANNEL_AO_FT_I_0"/> data type.
    /// </summary>
    public BOOL OverTemperature
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FieldPowerOff</c> member of the <see cref="CHANNEL_AO_FT_I_0"/> data type.
    /// </summary>
    public BOOL FieldPowerOff
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>NotANumber</c> member of the <see cref="CHANNEL_AO_FT_I_0"/> data type.
    /// </summary>
    public BOOL NotANumber
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Underrange</c> member of the <see cref="CHANNEL_AO_FT_I_0"/> data type.
    /// </summary>
    public BOOL Underrange
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Overrange</c> member of the <see cref="CHANNEL_AO_FT_I_0"/> data type.
    /// </summary>
    public BOOL Overrange
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Data</c> member of the <see cref="CHANNEL_AO_FT_I_0"/> data type.
    /// </summary>
    public REAL Data
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RollingTimestamp</c> member of the <see cref="CHANNEL_AO_FT_I_0"/> data type.
    /// </summary>
    public INT RollingTimestamp
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }
}