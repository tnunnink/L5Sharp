using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>CHANNEL_AO_DIAG_CAL_I_0</c> data type structure.
/// </summary>
[LogixData("CHANNEL_AO_DIAG_CAL:I:0")]
public sealed partial class CHANNEL_AO_DIAG_CAL_I_0 : StructureData
{
    /// <summary>
    /// Creates a new <see cref="CHANNEL_AO_DIAG_CAL_I_0"/> instance initialized with default values.
    /// </summary>
    public CHANNEL_AO_DIAG_CAL_I_0() : base("CHANNEL_AO_DIAG_CAL:I:0")
    {
        Fault = new BOOL();
        Uncertain = new BOOL();
        NoLoad = new BOOL();
        ShortCircuit = new BOOL();
        OverTemperature = new BOOL();
        FieldPowerOff = new BOOL();
        InHold = new BOOL();
        NotANumber = new BOOL();
        Underrange = new BOOL();
        Overrange = new BOOL();
        LLimitAlarm = new BOOL();
        HLimitAlarm = new BOOL();
        RampAlarm = new BOOL();
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
    /// Creates a new <see cref="CHANNEL_AO_DIAG_CAL_I_0"/> instance initialized with the provided element.
    /// </summary>
    public CHANNEL_AO_DIAG_CAL_I_0(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 12;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        Fault.UpdateData((data[offset + 3] & (1 << 0)) != 0);
        Uncertain.UpdateData((data[offset + 3] & (1 << 1)) != 0);
        NoLoad.UpdateData((data[offset + 3] & (1 << 2)) != 0);
        ShortCircuit.UpdateData((data[offset + 3] & (1 << 3)) != 0);
        OverTemperature.UpdateData((data[offset + 3] & (1 << 4)) != 0);
        FieldPowerOff.UpdateData((data[offset + 3] & (1 << 5)) != 0);
        InHold.UpdateData((data[offset + 3] & (1 << 6)) != 0);
        NotANumber.UpdateData((data[offset + 3] & (1 << 7)) != 0);
        Underrange.UpdateData((data[offset + 5] & (1 << 0)) != 0);
        Overrange.UpdateData((data[offset + 5] & (1 << 1)) != 0);
        LLimitAlarm.UpdateData((data[offset + 5] & (1 << 2)) != 0);
        HLimitAlarm.UpdateData((data[offset + 5] & (1 << 3)) != 0);
        RampAlarm.UpdateData((data[offset + 5] & (1 << 4)) != 0);
        CalFault.UpdateData((data[offset + 6] & (1 << 5)) != 0);
        Calibrating.UpdateData((data[offset + 6] & (1 << 6)) != 0);
        CalGoodLowRef.UpdateData((data[offset + 6] & (1 << 7)) != 0);
        CalBadLowRef.UpdateData((data[offset + 7] & (1 << 0)) != 0);
        CalGoodHighRef.UpdateData((data[offset + 7] & (1 << 1)) != 0);
        CalBadHighRef.UpdateData((data[offset + 7] & (1 << 2)) != 0);
        CalSuccessful.UpdateData((data[offset + 7] & (1 << 3)) != 0);
        Data.UpdateData(data, offset + 7);
        RollingTimestamp.UpdateData(data, offset + 11);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>Fault</c> member of the <see cref="CHANNEL_AO_DIAG_CAL_I_0"/> data type.
    /// </summary>
    public BOOL Fault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Uncertain</c> member of the <see cref="CHANNEL_AO_DIAG_CAL_I_0"/> data type.
    /// </summary>
    public BOOL Uncertain
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>NoLoad</c> member of the <see cref="CHANNEL_AO_DIAG_CAL_I_0"/> data type.
    /// </summary>
    public BOOL NoLoad
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ShortCircuit</c> member of the <see cref="CHANNEL_AO_DIAG_CAL_I_0"/> data type.
    /// </summary>
    public BOOL ShortCircuit
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OverTemperature</c> member of the <see cref="CHANNEL_AO_DIAG_CAL_I_0"/> data type.
    /// </summary>
    public BOOL OverTemperature
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FieldPowerOff</c> member of the <see cref="CHANNEL_AO_DIAG_CAL_I_0"/> data type.
    /// </summary>
    public BOOL FieldPowerOff
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InHold</c> member of the <see cref="CHANNEL_AO_DIAG_CAL_I_0"/> data type.
    /// </summary>
    public BOOL InHold
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>NotANumber</c> member of the <see cref="CHANNEL_AO_DIAG_CAL_I_0"/> data type.
    /// </summary>
    public BOOL NotANumber
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Underrange</c> member of the <see cref="CHANNEL_AO_DIAG_CAL_I_0"/> data type.
    /// </summary>
    public BOOL Underrange
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Overrange</c> member of the <see cref="CHANNEL_AO_DIAG_CAL_I_0"/> data type.
    /// </summary>
    public BOOL Overrange
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>LLimitAlarm</c> member of the <see cref="CHANNEL_AO_DIAG_CAL_I_0"/> data type.
    /// </summary>
    public BOOL LLimitAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HLimitAlarm</c> member of the <see cref="CHANNEL_AO_DIAG_CAL_I_0"/> data type.
    /// </summary>
    public BOOL HLimitAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RampAlarm</c> member of the <see cref="CHANNEL_AO_DIAG_CAL_I_0"/> data type.
    /// </summary>
    public BOOL RampAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CalFault</c> member of the <see cref="CHANNEL_AO_DIAG_CAL_I_0"/> data type.
    /// </summary>
    public BOOL CalFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Calibrating</c> member of the <see cref="CHANNEL_AO_DIAG_CAL_I_0"/> data type.
    /// </summary>
    public BOOL Calibrating
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CalGoodLowRef</c> member of the <see cref="CHANNEL_AO_DIAG_CAL_I_0"/> data type.
    /// </summary>
    public BOOL CalGoodLowRef
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CalBadLowRef</c> member of the <see cref="CHANNEL_AO_DIAG_CAL_I_0"/> data type.
    /// </summary>
    public BOOL CalBadLowRef
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CalGoodHighRef</c> member of the <see cref="CHANNEL_AO_DIAG_CAL_I_0"/> data type.
    /// </summary>
    public BOOL CalGoodHighRef
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CalBadHighRef</c> member of the <see cref="CHANNEL_AO_DIAG_CAL_I_0"/> data type.
    /// </summary>
    public BOOL CalBadHighRef
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CalSuccessful</c> member of the <see cref="CHANNEL_AO_DIAG_CAL_I_0"/> data type.
    /// </summary>
    public BOOL CalSuccessful
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Data</c> member of the <see cref="CHANNEL_AO_DIAG_CAL_I_0"/> data type.
    /// </summary>
    public REAL Data
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RollingTimestamp</c> member of the <see cref="CHANNEL_AO_DIAG_CAL_I_0"/> data type.
    /// </summary>
    public INT RollingTimestamp
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }
}