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
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 8;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        LLAlarmEn.UpdateData((data[offset + 3] & (1 << 0)) != 0);
        LAlarmEn.UpdateData((data[offset + 3] & (1 << 1)) != 0);
        HAlarmEn.UpdateData((data[offset + 3] & (1 << 2)) != 0);
        HHAlarmEn.UpdateData((data[offset + 3] & (1 << 3)) != 0);
        RateAlarmEn.UpdateData((data[offset + 3] & (1 << 4)) != 0);
        LLAlarmUnlatch.UpdateData((data[offset + 3] & (1 << 5)) != 0);
        LAlarmUnlatch.UpdateData((data[offset + 3] & (1 << 6)) != 0);
        HAlarmUnlatch.UpdateData((data[offset + 3] & (1 << 7)) != 0);
        HHAlarmUnlatch.UpdateData((data[offset + 4] & (1 << 0)) != 0);
        RateAlarmUnlatch.UpdateData((data[offset + 4] & (1 << 1)) != 0);
        Calibrate.UpdateData((data[offset + 4] & (1 << 2)) != 0);
        CalLowRef.UpdateData((data[offset + 4] & (1 << 3)) != 0);
        CalHighRef.UpdateData((data[offset + 4] & (1 << 4)) != 0);
        SensorOffset.UpdateData(data, offset + 6);
        
        return offset + GetSize();
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