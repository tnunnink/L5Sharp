using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>CHANNEL_HSC_O_0</c> data type structure.
/// </summary>
[LogixData("CHANNEL_HSC:O:0")]
public sealed partial class CHANNEL_HSC_O_0 : StructureData
{
    /// <summary>
    /// Creates a new <see cref="CHANNEL_HSC_O_0"/> instance initialized with default values.
    /// </summary>
    public CHANNEL_HSC_O_0() : base("CHANNEL_HSC:O:0")
    {
        Reset = new BOOL();
        Hold = new BOOL();
        Load = new BOOL();
        Store = new BOOL();
        Direction = new BOOL();
        RolloverAck = new BOOL();
        RollunderAck = new BOOL();
        FrequencyAlarmUnlatch = new BOOL();
        FrequencyAvgAlarmUnlatch = new BOOL();
        PulseWidthAlarmUnlatch = new BOOL();
        PulseWidthAvgAlarmUnlatch = new BOOL();
        ZeroFrequencyAlarmUnlatch = new BOOL();
        ZeroFrequencyAvgAlarmUnlatch = new BOOL();
        MissingPulseAlarmEn = new BOOL();
        MissingPulseAlarmUnlatch = new BOOL();
        AccelAlarmUnlatch = new BOOL();
        DecelAlarmUnlatch = new BOOL();
        AccelAvgAlarmUnlatch = new BOOL();
        DecelAvgAlarmUnlatch = new BOOL();
        ResetFrequencyOverrange = new BOOL();
        ResetQuadratureErrorCount = new BOOL();
        RolloverValue = new DINT();
        RollunderValue = new DINT();
        ZeroFrequencyAlarmLimit = new REAL();
        LoadCountValue = new DINT();
        LoadRevolutionValue = new INT();
        OverrideDataAEn = new BOOL();
        OverrideDataBEn = new BOOL();
        OverrideDataZEn = new BOOL();
        OverrideDataAValue = new BOOL();
        OverrideDataBValue = new BOOL();
        OverrideDataZValue = new BOOL();
        MissingPulseAlarmLimit = new DINT();
    }
    
    /// <summary>
    /// Creates a new <see cref="CHANNEL_HSC_O_0"/> instance initialized with the provided element.
    /// </summary>
    public CHANNEL_HSC_O_0(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>Reset</c> member of the <see cref="CHANNEL_HSC_O_0"/> data type.
    /// </summary>
    public BOOL Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Hold</c> member of the <see cref="CHANNEL_HSC_O_0"/> data type.
    /// </summary>
    public BOOL Hold
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Load</c> member of the <see cref="CHANNEL_HSC_O_0"/> data type.
    /// </summary>
    public BOOL Load
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Store</c> member of the <see cref="CHANNEL_HSC_O_0"/> data type.
    /// </summary>
    public BOOL Store
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Direction</c> member of the <see cref="CHANNEL_HSC_O_0"/> data type.
    /// </summary>
    public BOOL Direction
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RolloverAck</c> member of the <see cref="CHANNEL_HSC_O_0"/> data type.
    /// </summary>
    public BOOL RolloverAck
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RollunderAck</c> member of the <see cref="CHANNEL_HSC_O_0"/> data type.
    /// </summary>
    public BOOL RollunderAck
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FrequencyAlarmUnlatch</c> member of the <see cref="CHANNEL_HSC_O_0"/> data type.
    /// </summary>
    public BOOL FrequencyAlarmUnlatch
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FrequencyAvgAlarmUnlatch</c> member of the <see cref="CHANNEL_HSC_O_0"/> data type.
    /// </summary>
    public BOOL FrequencyAvgAlarmUnlatch
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PulseWidthAlarmUnlatch</c> member of the <see cref="CHANNEL_HSC_O_0"/> data type.
    /// </summary>
    public BOOL PulseWidthAlarmUnlatch
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PulseWidthAvgAlarmUnlatch</c> member of the <see cref="CHANNEL_HSC_O_0"/> data type.
    /// </summary>
    public BOOL PulseWidthAvgAlarmUnlatch
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ZeroFrequencyAlarmUnlatch</c> member of the <see cref="CHANNEL_HSC_O_0"/> data type.
    /// </summary>
    public BOOL ZeroFrequencyAlarmUnlatch
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ZeroFrequencyAvgAlarmUnlatch</c> member of the <see cref="CHANNEL_HSC_O_0"/> data type.
    /// </summary>
    public BOOL ZeroFrequencyAvgAlarmUnlatch
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>MissingPulseAlarmEn</c> member of the <see cref="CHANNEL_HSC_O_0"/> data type.
    /// </summary>
    public BOOL MissingPulseAlarmEn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>MissingPulseAlarmUnlatch</c> member of the <see cref="CHANNEL_HSC_O_0"/> data type.
    /// </summary>
    public BOOL MissingPulseAlarmUnlatch
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AccelAlarmUnlatch</c> member of the <see cref="CHANNEL_HSC_O_0"/> data type.
    /// </summary>
    public BOOL AccelAlarmUnlatch
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DecelAlarmUnlatch</c> member of the <see cref="CHANNEL_HSC_O_0"/> data type.
    /// </summary>
    public BOOL DecelAlarmUnlatch
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AccelAvgAlarmUnlatch</c> member of the <see cref="CHANNEL_HSC_O_0"/> data type.
    /// </summary>
    public BOOL AccelAvgAlarmUnlatch
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DecelAvgAlarmUnlatch</c> member of the <see cref="CHANNEL_HSC_O_0"/> data type.
    /// </summary>
    public BOOL DecelAvgAlarmUnlatch
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ResetFrequencyOverrange</c> member of the <see cref="CHANNEL_HSC_O_0"/> data type.
    /// </summary>
    public BOOL ResetFrequencyOverrange
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ResetQuadratureErrorCount</c> member of the <see cref="CHANNEL_HSC_O_0"/> data type.
    /// </summary>
    public BOOL ResetQuadratureErrorCount
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RolloverValue</c> member of the <see cref="CHANNEL_HSC_O_0"/> data type.
    /// </summary>
    public DINT RolloverValue
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RollunderValue</c> member of the <see cref="CHANNEL_HSC_O_0"/> data type.
    /// </summary>
    public DINT RollunderValue
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ZeroFrequencyAlarmLimit</c> member of the <see cref="CHANNEL_HSC_O_0"/> data type.
    /// </summary>
    public REAL ZeroFrequencyAlarmLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>LoadCountValue</c> member of the <see cref="CHANNEL_HSC_O_0"/> data type.
    /// </summary>
    public DINT LoadCountValue
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>LoadRevolutionValue</c> member of the <see cref="CHANNEL_HSC_O_0"/> data type.
    /// </summary>
    public INT LoadRevolutionValue
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OverrideDataAEn</c> member of the <see cref="CHANNEL_HSC_O_0"/> data type.
    /// </summary>
    public BOOL OverrideDataAEn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OverrideDataBEn</c> member of the <see cref="CHANNEL_HSC_O_0"/> data type.
    /// </summary>
    public BOOL OverrideDataBEn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OverrideDataZEn</c> member of the <see cref="CHANNEL_HSC_O_0"/> data type.
    /// </summary>
    public BOOL OverrideDataZEn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OverrideDataAValue</c> member of the <see cref="CHANNEL_HSC_O_0"/> data type.
    /// </summary>
    public BOOL OverrideDataAValue
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OverrideDataBValue</c> member of the <see cref="CHANNEL_HSC_O_0"/> data type.
    /// </summary>
    public BOOL OverrideDataBValue
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OverrideDataZValue</c> member of the <see cref="CHANNEL_HSC_O_0"/> data type.
    /// </summary>
    public BOOL OverrideDataZValue
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>MissingPulseAlarmLimit</c> member of the <see cref="CHANNEL_HSC_O_0"/> data type.
    /// </summary>
    public DINT MissingPulseAlarmLimit
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }
}