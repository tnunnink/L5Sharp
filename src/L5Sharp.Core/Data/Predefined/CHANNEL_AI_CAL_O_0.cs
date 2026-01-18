using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>CHANNEL_AI_CAL_O_0</c> data type structure.
/// </summary>
[LogixData("CHANNEL_AI_CAL:O:0")]
public sealed partial class CHANNEL_AI_CAL_O_0 : StructureData
{
    /// <summary>
    /// Creates a new <see cref="CHANNEL_AI_CAL_O_0"/> instance initialized with default values.
    /// </summary>
    public CHANNEL_AI_CAL_O_0() : base("CHANNEL_AI_CAL:O:0")
    {
        LLAlarmEn = new BOOL();
        LAlarmEn = new BOOL();
        HAlarmEn = new BOOL();
        HHAlarmEn = new BOOL();
        RateAlarmEn = new BOOL();
        LLAlarmUnlatch = new BOOL();
        LAlarmUnlatch = new BOOL();
        HAlarmUnlatch = new BOOL();
        HHAlarmUnlatch = new BOOL();
        RateAlarmUnlatch = new BOOL();
        Calibrate = new BOOL();
        CalLowRef = new BOOL();
        CalHighRef = new BOOL();
        SensorOffset = new REAL();
    }
    
    /// <summary>
    /// Creates a new <see cref="CHANNEL_AI_CAL_O_0"/> instance initialized with the provided element.
    /// </summary>
    public CHANNEL_AI_CAL_O_0(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>LLAlarmEn</c> member of the <see cref="CHANNEL_AI_CAL_O_0"/> data type.
    /// </summary>
    public BOOL LLAlarmEn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>LAlarmEn</c> member of the <see cref="CHANNEL_AI_CAL_O_0"/> data type.
    /// </summary>
    public BOOL LAlarmEn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HAlarmEn</c> member of the <see cref="CHANNEL_AI_CAL_O_0"/> data type.
    /// </summary>
    public BOOL HAlarmEn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HHAlarmEn</c> member of the <see cref="CHANNEL_AI_CAL_O_0"/> data type.
    /// </summary>
    public BOOL HHAlarmEn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RateAlarmEn</c> member of the <see cref="CHANNEL_AI_CAL_O_0"/> data type.
    /// </summary>
    public BOOL RateAlarmEn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>LLAlarmUnlatch</c> member of the <see cref="CHANNEL_AI_CAL_O_0"/> data type.
    /// </summary>
    public BOOL LLAlarmUnlatch
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>LAlarmUnlatch</c> member of the <see cref="CHANNEL_AI_CAL_O_0"/> data type.
    /// </summary>
    public BOOL LAlarmUnlatch
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HAlarmUnlatch</c> member of the <see cref="CHANNEL_AI_CAL_O_0"/> data type.
    /// </summary>
    public BOOL HAlarmUnlatch
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HHAlarmUnlatch</c> member of the <see cref="CHANNEL_AI_CAL_O_0"/> data type.
    /// </summary>
    public BOOL HHAlarmUnlatch
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RateAlarmUnlatch</c> member of the <see cref="CHANNEL_AI_CAL_O_0"/> data type.
    /// </summary>
    public BOOL RateAlarmUnlatch
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Calibrate</c> member of the <see cref="CHANNEL_AI_CAL_O_0"/> data type.
    /// </summary>
    public BOOL Calibrate
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CalLowRef</c> member of the <see cref="CHANNEL_AI_CAL_O_0"/> data type.
    /// </summary>
    public BOOL CalLowRef
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CalHighRef</c> member of the <see cref="CHANNEL_AI_CAL_O_0"/> data type.
    /// </summary>
    public BOOL CalHighRef
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SensorOffset</c> member of the <see cref="CHANNEL_AI_CAL_O_0"/> data type.
    /// </summary>
    public REAL SensorOffset
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }
}