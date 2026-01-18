using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>CHANNEL_AO_CAL_O_0</c> data type structure.
/// </summary>
[LogixData("CHANNEL_AO_CAL:O:0")]
public sealed partial class CHANNEL_AO_CAL_O_0 : StructureData
{
    /// <summary>
    /// Creates a new <see cref="CHANNEL_AO_CAL_O_0"/> instance initialized with default values.
    /// </summary>
    public CHANNEL_AO_CAL_O_0() : base("CHANNEL_AO_CAL:O:0")
    {
        LLimitAlarmUnlatch = new BOOL();
        HLimitAlarmUnlatch = new BOOL();
        RampAlarmUnlatch = new BOOL();
        Calibrate = new BOOL();
        CalOutputLowRef = new BOOL();
        CalOutputHighRef = new BOOL();
        CalLowRefPassed = new BOOL();
        CalHighRefPassed = new BOOL();
        CalFinished = new BOOL();
        Data = new REAL();
    }
    
    /// <summary>
    /// Creates a new <see cref="CHANNEL_AO_CAL_O_0"/> instance initialized with the provided element.
    /// </summary>
    public CHANNEL_AO_CAL_O_0(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>LLimitAlarmUnlatch</c> member of the <see cref="CHANNEL_AO_CAL_O_0"/> data type.
    /// </summary>
    public BOOL LLimitAlarmUnlatch
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HLimitAlarmUnlatch</c> member of the <see cref="CHANNEL_AO_CAL_O_0"/> data type.
    /// </summary>
    public BOOL HLimitAlarmUnlatch
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RampAlarmUnlatch</c> member of the <see cref="CHANNEL_AO_CAL_O_0"/> data type.
    /// </summary>
    public BOOL RampAlarmUnlatch
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Calibrate</c> member of the <see cref="CHANNEL_AO_CAL_O_0"/> data type.
    /// </summary>
    public BOOL Calibrate
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CalOutputLowRef</c> member of the <see cref="CHANNEL_AO_CAL_O_0"/> data type.
    /// </summary>
    public BOOL CalOutputLowRef
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CalOutputHighRef</c> member of the <see cref="CHANNEL_AO_CAL_O_0"/> data type.
    /// </summary>
    public BOOL CalOutputHighRef
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CalLowRefPassed</c> member of the <see cref="CHANNEL_AO_CAL_O_0"/> data type.
    /// </summary>
    public BOOL CalLowRefPassed
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CalHighRefPassed</c> member of the <see cref="CHANNEL_AO_CAL_O_0"/> data type.
    /// </summary>
    public BOOL CalHighRefPassed
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CalFinished</c> member of the <see cref="CHANNEL_AO_CAL_O_0"/> data type.
    /// </summary>
    public BOOL CalFinished
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Data</c> member of the <see cref="CHANNEL_AO_CAL_O_0"/> data type.
    /// </summary>
    public REAL Data
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }
}