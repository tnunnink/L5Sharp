using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>CHANNEL_AI_DIAG_CAL_I_0</c> data type structure.
/// </summary>
[LogixData("CHANNEL_AI_DIAG_CAL:I:0")]
public sealed partial class CHANNEL_AI_DIAG_CAL_I_0 : StructureData
{
    /// <summary>
    /// Creates a new <see cref="CHANNEL_AI_DIAG_CAL_I_0"/> instance initialized with default values.
    /// </summary>
    public CHANNEL_AI_DIAG_CAL_I_0() : base("CHANNEL_AI_DIAG_CAL:I:0")
    {
        Fault = new BOOL();
        Uncertain = new BOOL();
        OpenWire = new BOOL();
        OverTemperature = new BOOL();
        FieldPowerOff = new BOOL();
        NotANumber = new BOOL();
        Underrange = new BOOL();
        Overrange = new BOOL();
        LLAlarm = new BOOL();
        LAlarm = new BOOL();
        HAlarm = new BOOL();
        HHAlarm = new BOOL();
        RateAlarm = new BOOL();
        CalFault = new BOOL();
        Calibrating = new BOOL();
        CalGoodLowRef = new BOOL();
        CalBadLowRef = new BOOL();
        CalGoodHighRef = new BOOL();
        CalBadHighRef = new BOOL();
        CalSuccessful = new BOOL();
        Data = new REAL();
        RollingTimestamp = new INT();
    }
    
    /// <summary>
    /// Creates a new <see cref="CHANNEL_AI_DIAG_CAL_I_0"/> instance initialized with the provided element.
    /// </summary>
    public CHANNEL_AI_DIAG_CAL_I_0(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>Fault</c> member of the <see cref="CHANNEL_AI_DIAG_CAL_I_0"/> data type.
    /// </summary>
    public BOOL Fault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Uncertain</c> member of the <see cref="CHANNEL_AI_DIAG_CAL_I_0"/> data type.
    /// </summary>
    public BOOL Uncertain
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OpenWire</c> member of the <see cref="CHANNEL_AI_DIAG_CAL_I_0"/> data type.
    /// </summary>
    public BOOL OpenWire
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OverTemperature</c> member of the <see cref="CHANNEL_AI_DIAG_CAL_I_0"/> data type.
    /// </summary>
    public BOOL OverTemperature
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FieldPowerOff</c> member of the <see cref="CHANNEL_AI_DIAG_CAL_I_0"/> data type.
    /// </summary>
    public BOOL FieldPowerOff
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>NotANumber</c> member of the <see cref="CHANNEL_AI_DIAG_CAL_I_0"/> data type.
    /// </summary>
    public BOOL NotANumber
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Underrange</c> member of the <see cref="CHANNEL_AI_DIAG_CAL_I_0"/> data type.
    /// </summary>
    public BOOL Underrange
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Overrange</c> member of the <see cref="CHANNEL_AI_DIAG_CAL_I_0"/> data type.
    /// </summary>
    public BOOL Overrange
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>LLAlarm</c> member of the <see cref="CHANNEL_AI_DIAG_CAL_I_0"/> data type.
    /// </summary>
    public BOOL LLAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>LAlarm</c> member of the <see cref="CHANNEL_AI_DIAG_CAL_I_0"/> data type.
    /// </summary>
    public BOOL LAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HAlarm</c> member of the <see cref="CHANNEL_AI_DIAG_CAL_I_0"/> data type.
    /// </summary>
    public BOOL HAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HHAlarm</c> member of the <see cref="CHANNEL_AI_DIAG_CAL_I_0"/> data type.
    /// </summary>
    public BOOL HHAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RateAlarm</c> member of the <see cref="CHANNEL_AI_DIAG_CAL_I_0"/> data type.
    /// </summary>
    public BOOL RateAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CalFault</c> member of the <see cref="CHANNEL_AI_DIAG_CAL_I_0"/> data type.
    /// </summary>
    public BOOL CalFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Calibrating</c> member of the <see cref="CHANNEL_AI_DIAG_CAL_I_0"/> data type.
    /// </summary>
    public BOOL Calibrating
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CalGoodLowRef</c> member of the <see cref="CHANNEL_AI_DIAG_CAL_I_0"/> data type.
    /// </summary>
    public BOOL CalGoodLowRef
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CalBadLowRef</c> member of the <see cref="CHANNEL_AI_DIAG_CAL_I_0"/> data type.
    /// </summary>
    public BOOL CalBadLowRef
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CalGoodHighRef</c> member of the <see cref="CHANNEL_AI_DIAG_CAL_I_0"/> data type.
    /// </summary>
    public BOOL CalGoodHighRef
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CalBadHighRef</c> member of the <see cref="CHANNEL_AI_DIAG_CAL_I_0"/> data type.
    /// </summary>
    public BOOL CalBadHighRef
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CalSuccessful</c> member of the <see cref="CHANNEL_AI_DIAG_CAL_I_0"/> data type.
    /// </summary>
    public BOOL CalSuccessful
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Data</c> member of the <see cref="CHANNEL_AI_DIAG_CAL_I_0"/> data type.
    /// </summary>
    public REAL Data
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RollingTimestamp</c> member of the <see cref="CHANNEL_AI_DIAG_CAL_I_0"/> data type.
    /// </summary>
    public INT RollingTimestamp
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }
}