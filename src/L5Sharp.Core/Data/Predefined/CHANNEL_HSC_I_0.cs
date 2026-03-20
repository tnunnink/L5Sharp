using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>CHANNEL_HSC_I_0</c> data type structure.
/// </summary>
[LogixData("CHANNEL_HSC:I:0")]
public sealed partial class CHANNEL_HSC_I_0 : StructureData
{
    /// <summary>
    /// Creates a new <see cref="CHANNEL_HSC_I_0"/> instance initialized with default values.
    /// </summary>
    public CHANNEL_HSC_I_0() : base("CHANNEL_HSC:I:0")
    {
        Fault = new BOOL();
        Uncertain = new BOOL();
        RolloverLeqRollunder = new BOOL();
        NotANumber = new BOOL();
        MissingPulseAlarm = new BOOL();
        ZeroFrequencyAlarm = new BOOL();
        ZeroFrequencyAvgAlarm = new BOOL();
        FrequencyAlarm = new BOOL();
        FrequencyAvgAlarm = new BOOL();
        PulseWidthAlarm = new BOOL();
        PulseWidthAvgAlarm = new BOOL();
        AccelAlarm = new BOOL();
        AccelAvgAlarm = new BOOL();
        DecelAlarm = new BOOL();
        DecelAvgAlarm = new BOOL();
        FrequencyOverrange = new BOOL();
        PartialAvgFrequency = new BOOL();
        PartialAvgPulseWidth = new BOOL();
        Direction = new BOOL();
        StoredDirection = new BOOL();
        Rollover = new BOOL();
        Rollunder = new BOOL();
        DataA = new BOOL();
        DataB = new BOOL();
        DataZ = new BOOL();
        DataAOverridden = new BOOL();
        DataBOverridden = new BOOL();
        DataZOverridden = new BOOL();
        Count = new DINT();
        StoredCount = new DINT();
        ScaledCount = new REAL();
        ScaledStoredCount = new REAL();
        RevolutionCount = new INT();
        StoredRevolutionCount = new INT();
        Frequency = new REAL();
        FrequencyAvg = new REAL();
        StoredFrequency = new REAL();
        ScaledFrequency = new REAL();
        ScaledFrequencyAvg = new REAL();
        ScaledStoredFrequency = new REAL();
        PulseWidth = new REAL();
        PulseWidthAvg = new REAL();
        StoredPulseWidth = new REAL();
        QuadratureErrorCount = new SINT();
        CountChangeIndicator = new SINT();
        Accel = new REAL();
        AccelAvg = new REAL();
        StoredAccel = new REAL();
        ScaledAccel = new REAL();
        ScaledAccelAvg = new REAL();
        ScaledStoredAccel = new REAL();
    }
    
    /// <summary>
    /// Creates a new <see cref="CHANNEL_HSC_I_0"/> instance initialized with the provided element.
    /// </summary>
    public CHANNEL_HSC_I_0(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 92;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        Fault.UpdateData((data[offset + 3] & (1 << 0)) != 0);
        Uncertain.UpdateData((data[offset + 3] & (1 << 1)) != 0);
        RolloverLeqRollunder.UpdateData((data[offset + 3] & (1 << 2)) != 0);
        NotANumber.UpdateData((data[offset + 3] & (1 << 3)) != 0);
        MissingPulseAlarm.UpdateData((data[offset + 5] & (1 << 4)) != 0);
        ZeroFrequencyAlarm.UpdateData((data[offset + 5] & (1 << 5)) != 0);
        ZeroFrequencyAvgAlarm.UpdateData((data[offset + 5] & (1 << 6)) != 0);
        FrequencyAlarm.UpdateData((data[offset + 5] & (1 << 7)) != 0);
        FrequencyAvgAlarm.UpdateData((data[offset + 6] & (1 << 0)) != 0);
        PulseWidthAlarm.UpdateData((data[offset + 6] & (1 << 1)) != 0);
        PulseWidthAvgAlarm.UpdateData((data[offset + 6] & (1 << 2)) != 0);
        AccelAlarm.UpdateData((data[offset + 6] & (1 << 3)) != 0);
        AccelAvgAlarm.UpdateData((data[offset + 6] & (1 << 4)) != 0);
        DecelAlarm.UpdateData((data[offset + 6] & (1 << 5)) != 0);
        DecelAvgAlarm.UpdateData((data[offset + 6] & (1 << 6)) != 0);
        FrequencyOverrange.UpdateData((data[offset + 6] & (1 << 7)) != 0);
        PartialAvgFrequency.UpdateData((data[offset + 7] & (1 << 0)) != 0);
        PartialAvgPulseWidth.UpdateData((data[offset + 7] & (1 << 1)) != 0);
        Direction.UpdateData((data[offset + 11] & (1 << 2)) != 0);
        StoredDirection.UpdateData((data[offset + 11] & (1 << 3)) != 0);
        Rollover.UpdateData((data[offset + 11] & (1 << 4)) != 0);
        Rollunder.UpdateData((data[offset + 11] & (1 << 5)) != 0);
        DataA.UpdateData((data[offset + 11] & (1 << 6)) != 0);
        DataB.UpdateData((data[offset + 11] & (1 << 7)) != 0);
        DataZ.UpdateData((data[offset + 12] & (1 << 0)) != 0);
        DataAOverridden.UpdateData((data[offset + 12] & (1 << 1)) != 0);
        DataBOverridden.UpdateData((data[offset + 12] & (1 << 2)) != 0);
        DataZOverridden.UpdateData((data[offset + 12] & (1 << 3)) != 0);
        Count.UpdateData(data, offset + 12);
        StoredCount.UpdateData(data, offset + 16);
        ScaledCount.UpdateData(data, offset + 20);
        ScaledStoredCount.UpdateData(data, offset + 24);
        RevolutionCount.UpdateData(data, offset + 28);
        StoredRevolutionCount.UpdateData(data, offset + 30);
        Frequency.UpdateData(data, offset + 32);
        FrequencyAvg.UpdateData(data, offset + 36);
        StoredFrequency.UpdateData(data, offset + 40);
        ScaledFrequency.UpdateData(data, offset + 44);
        ScaledFrequencyAvg.UpdateData(data, offset + 48);
        ScaledStoredFrequency.UpdateData(data, offset + 52);
        PulseWidth.UpdateData(data, offset + 56);
        PulseWidthAvg.UpdateData(data, offset + 60);
        StoredPulseWidth.UpdateData(data, offset + 64);
        QuadratureErrorCount.UpdateData(data, offset + 68);
        CountChangeIndicator.UpdateData(data, offset + 69);
        Accel.UpdateData(data, offset + 72);
        AccelAvg.UpdateData(data, offset + 76);
        StoredAccel.UpdateData(data, offset + 80);
        ScaledAccel.UpdateData(data, offset + 84);
        ScaledAccelAvg.UpdateData(data, offset + 88);
        ScaledStoredAccel.UpdateData(data, offset + 92);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>Fault</c> member of the <see cref="CHANNEL_HSC_I_0"/> data type.
    /// </summary>
    public BOOL Fault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Uncertain</c> member of the <see cref="CHANNEL_HSC_I_0"/> data type.
    /// </summary>
    public BOOL Uncertain
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RolloverLeqRollunder</c> member of the <see cref="CHANNEL_HSC_I_0"/> data type.
    /// </summary>
    public BOOL RolloverLeqRollunder
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>NotANumber</c> member of the <see cref="CHANNEL_HSC_I_0"/> data type.
    /// </summary>
    public BOOL NotANumber
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>MissingPulseAlarm</c> member of the <see cref="CHANNEL_HSC_I_0"/> data type.
    /// </summary>
    public BOOL MissingPulseAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ZeroFrequencyAlarm</c> member of the <see cref="CHANNEL_HSC_I_0"/> data type.
    /// </summary>
    public BOOL ZeroFrequencyAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ZeroFrequencyAvgAlarm</c> member of the <see cref="CHANNEL_HSC_I_0"/> data type.
    /// </summary>
    public BOOL ZeroFrequencyAvgAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FrequencyAlarm</c> member of the <see cref="CHANNEL_HSC_I_0"/> data type.
    /// </summary>
    public BOOL FrequencyAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FrequencyAvgAlarm</c> member of the <see cref="CHANNEL_HSC_I_0"/> data type.
    /// </summary>
    public BOOL FrequencyAvgAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PulseWidthAlarm</c> member of the <see cref="CHANNEL_HSC_I_0"/> data type.
    /// </summary>
    public BOOL PulseWidthAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PulseWidthAvgAlarm</c> member of the <see cref="CHANNEL_HSC_I_0"/> data type.
    /// </summary>
    public BOOL PulseWidthAvgAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AccelAlarm</c> member of the <see cref="CHANNEL_HSC_I_0"/> data type.
    /// </summary>
    public BOOL AccelAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AccelAvgAlarm</c> member of the <see cref="CHANNEL_HSC_I_0"/> data type.
    /// </summary>
    public BOOL AccelAvgAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DecelAlarm</c> member of the <see cref="CHANNEL_HSC_I_0"/> data type.
    /// </summary>
    public BOOL DecelAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DecelAvgAlarm</c> member of the <see cref="CHANNEL_HSC_I_0"/> data type.
    /// </summary>
    public BOOL DecelAvgAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FrequencyOverrange</c> member of the <see cref="CHANNEL_HSC_I_0"/> data type.
    /// </summary>
    public BOOL FrequencyOverrange
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PartialAvgFrequency</c> member of the <see cref="CHANNEL_HSC_I_0"/> data type.
    /// </summary>
    public BOOL PartialAvgFrequency
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PartialAvgPulseWidth</c> member of the <see cref="CHANNEL_HSC_I_0"/> data type.
    /// </summary>
    public BOOL PartialAvgPulseWidth
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Direction</c> member of the <see cref="CHANNEL_HSC_I_0"/> data type.
    /// </summary>
    public BOOL Direction
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>StoredDirection</c> member of the <see cref="CHANNEL_HSC_I_0"/> data type.
    /// </summary>
    public BOOL StoredDirection
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Rollover</c> member of the <see cref="CHANNEL_HSC_I_0"/> data type.
    /// </summary>
    public BOOL Rollover
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Rollunder</c> member of the <see cref="CHANNEL_HSC_I_0"/> data type.
    /// </summary>
    public BOOL Rollunder
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DataA</c> member of the <see cref="CHANNEL_HSC_I_0"/> data type.
    /// </summary>
    public BOOL DataA
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DataB</c> member of the <see cref="CHANNEL_HSC_I_0"/> data type.
    /// </summary>
    public BOOL DataB
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DataZ</c> member of the <see cref="CHANNEL_HSC_I_0"/> data type.
    /// </summary>
    public BOOL DataZ
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DataAOverridden</c> member of the <see cref="CHANNEL_HSC_I_0"/> data type.
    /// </summary>
    public BOOL DataAOverridden
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DataBOverridden</c> member of the <see cref="CHANNEL_HSC_I_0"/> data type.
    /// </summary>
    public BOOL DataBOverridden
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DataZOverridden</c> member of the <see cref="CHANNEL_HSC_I_0"/> data type.
    /// </summary>
    public BOOL DataZOverridden
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Count</c> member of the <see cref="CHANNEL_HSC_I_0"/> data type.
    /// </summary>
    public new DINT Count
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>StoredCount</c> member of the <see cref="CHANNEL_HSC_I_0"/> data type.
    /// </summary>
    public DINT StoredCount
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ScaledCount</c> member of the <see cref="CHANNEL_HSC_I_0"/> data type.
    /// </summary>
    public REAL ScaledCount
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ScaledStoredCount</c> member of the <see cref="CHANNEL_HSC_I_0"/> data type.
    /// </summary>
    public REAL ScaledStoredCount
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RevolutionCount</c> member of the <see cref="CHANNEL_HSC_I_0"/> data type.
    /// </summary>
    public INT RevolutionCount
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>StoredRevolutionCount</c> member of the <see cref="CHANNEL_HSC_I_0"/> data type.
    /// </summary>
    public INT StoredRevolutionCount
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Frequency</c> member of the <see cref="CHANNEL_HSC_I_0"/> data type.
    /// </summary>
    public REAL Frequency
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FrequencyAvg</c> member of the <see cref="CHANNEL_HSC_I_0"/> data type.
    /// </summary>
    public REAL FrequencyAvg
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>StoredFrequency</c> member of the <see cref="CHANNEL_HSC_I_0"/> data type.
    /// </summary>
    public REAL StoredFrequency
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ScaledFrequency</c> member of the <see cref="CHANNEL_HSC_I_0"/> data type.
    /// </summary>
    public REAL ScaledFrequency
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ScaledFrequencyAvg</c> member of the <see cref="CHANNEL_HSC_I_0"/> data type.
    /// </summary>
    public REAL ScaledFrequencyAvg
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ScaledStoredFrequency</c> member of the <see cref="CHANNEL_HSC_I_0"/> data type.
    /// </summary>
    public REAL ScaledStoredFrequency
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PulseWidth</c> member of the <see cref="CHANNEL_HSC_I_0"/> data type.
    /// </summary>
    public REAL PulseWidth
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PulseWidthAvg</c> member of the <see cref="CHANNEL_HSC_I_0"/> data type.
    /// </summary>
    public REAL PulseWidthAvg
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>StoredPulseWidth</c> member of the <see cref="CHANNEL_HSC_I_0"/> data type.
    /// </summary>
    public REAL StoredPulseWidth
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>QuadratureErrorCount</c> member of the <see cref="CHANNEL_HSC_I_0"/> data type.
    /// </summary>
    public SINT QuadratureErrorCount
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CountChangeIndicator</c> member of the <see cref="CHANNEL_HSC_I_0"/> data type.
    /// </summary>
    public SINT CountChangeIndicator
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Accel</c> member of the <see cref="CHANNEL_HSC_I_0"/> data type.
    /// </summary>
    public REAL Accel
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AccelAvg</c> member of the <see cref="CHANNEL_HSC_I_0"/> data type.
    /// </summary>
    public REAL AccelAvg
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>StoredAccel</c> member of the <see cref="CHANNEL_HSC_I_0"/> data type.
    /// </summary>
    public REAL StoredAccel
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ScaledAccel</c> member of the <see cref="CHANNEL_HSC_I_0"/> data type.
    /// </summary>
    public REAL ScaledAccel
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ScaledAccelAvg</c> member of the <see cref="CHANNEL_HSC_I_0"/> data type.
    /// </summary>
    public REAL ScaledAccelAvg
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ScaledStoredAccel</c> member of the <see cref="CHANNEL_HSC_I_0"/> data type.
    /// </summary>
    public REAL ScaledStoredAccel
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }
}