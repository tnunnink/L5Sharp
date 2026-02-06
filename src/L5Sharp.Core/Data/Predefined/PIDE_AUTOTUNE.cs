using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>PIDE_AUTOTUNE</c> data type structure.
/// </summary>
[LogixData("PIDE_AUTOTUNE")]
public sealed partial class PIDE_AUTOTUNE : StructureData
{
    /// <summary>
    /// Creates a new <see cref="PIDE_AUTOTUNE"/> instance initialized with default values.
    /// </summary>
    public PIDE_AUTOTUNE() : base("PIDE_AUTOTUNE")
    {
        ProcessType = new DINT();
        ResponseSpeed = new DINT();
        TestLength = new REAL();
        PVTuneLimit = new REAL();
        StepSize = new REAL();
        TunedGood = new BOOL();
        TunedUncertain = new BOOL();
        ATuneAcquired = new BOOL();
        UsedProcessType = new DINT();
        Gain = new REAL();
        TimeConstant = new REAL();
        DeadTime = new REAL();
        PGainTunedFast = new REAL();
        IGainTunedFast = new REAL();
        DGainTunedFast = new REAL();
        PGainTunedMed = new REAL();
        IGainTunedMed = new REAL();
        DGainTunedMed = new REAL();
        PGainTunedSlow = new REAL();
        IGainTunedSlow = new REAL();
        DGainTunedSlow = new REAL();
        StepSizeUsed = new REAL();
        AtuneStatus = new DINT();
        ATuneFault = new BOOL();
        PVOutOfLimit = new BOOL();
        ModeInv = new BOOL();
        CVWindupFault = new BOOL();
        StepSizeZero = new BOOL();
        CVLimitsFault = new BOOL();
        CVInitFault = new BOOL();
        EUSpanChanged = new BOOL();
        CVChanged = new BOOL();
        ATuneTimedOut = new BOOL();
        PVNotSettled = new BOOL();
        PVChangeTooSmall = new BOOL();
        StepSizeTooSmall = new BOOL();
        GainTooLarge = new BOOL();
        GainTooSmall = new BOOL();
        LongDeadTime = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="PIDE_AUTOTUNE"/> instance initialized with the provided element.
    /// </summary>
    public PIDE_AUTOTUNE(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 972;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        ProcessType.UpdateData(data, offset + 0);
        ResponseSpeed.UpdateData(data, offset + 4);
        TestLength.UpdateData(data, offset + 8);
        PVTuneLimit.UpdateData(data, offset + 12);
        StepSize.UpdateData(data, offset + 16);
        TunedGood.UpdateData((data[offset + 25] & (1 << 0)) != 0);
        TunedUncertain.UpdateData((data[offset + 25] & (1 << 1)) != 0);
        ATuneAcquired.UpdateData((data[offset + 25] & (1 << 2)) != 0);
        UsedProcessType.UpdateData(data, offset + 25);
        Gain.UpdateData(data, offset + 29);
        TimeConstant.UpdateData(data, offset + 33);
        DeadTime.UpdateData(data, offset + 37);
        PGainTunedFast.UpdateData(data, offset + 41);
        IGainTunedFast.UpdateData(data, offset + 45);
        DGainTunedFast.UpdateData(data, offset + 49);
        PGainTunedMed.UpdateData(data, offset + 53);
        IGainTunedMed.UpdateData(data, offset + 57);
        DGainTunedMed.UpdateData(data, offset + 61);
        PGainTunedSlow.UpdateData(data, offset + 65);
        IGainTunedSlow.UpdateData(data, offset + 69);
        DGainTunedSlow.UpdateData(data, offset + 73);
        StepSizeUsed.UpdateData(data, offset + 77);
        AtuneStatus.UpdateData(data, offset + 81);
        ATuneFault.UpdateData((data[offset + 85] & (1 << 3)) != 0);
        PVOutOfLimit.UpdateData((data[offset + 85] & (1 << 4)) != 0);
        ModeInv.UpdateData((data[offset + 85] & (1 << 5)) != 0);
        CVWindupFault.UpdateData((data[offset + 85] & (1 << 6)) != 0);
        StepSizeZero.UpdateData((data[offset + 85] & (1 << 7)) != 0);
        CVLimitsFault.UpdateData((data[offset + 86] & (1 << 0)) != 0);
        CVInitFault.UpdateData((data[offset + 86] & (1 << 1)) != 0);
        EUSpanChanged.UpdateData((data[offset + 86] & (1 << 2)) != 0);
        CVChanged.UpdateData((data[offset + 86] & (1 << 3)) != 0);
        ATuneTimedOut.UpdateData((data[offset + 86] & (1 << 4)) != 0);
        PVNotSettled.UpdateData((data[offset + 86] & (1 << 5)) != 0);
        PVChangeTooSmall.UpdateData((data[offset + 86] & (1 << 6)) != 0);
        StepSizeTooSmall.UpdateData((data[offset + 86] & (1 << 7)) != 0);
        GainTooLarge.UpdateData((data[offset + 87] & (1 << 0)) != 0);
        GainTooSmall.UpdateData((data[offset + 87] & (1 << 1)) != 0);
        LongDeadTime.UpdateData((data[offset + 87] & (1 << 2)) != 0);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>ProcessType</c> member of the <see cref="PIDE_AUTOTUNE"/> data type.
    /// </summary>
    public DINT ProcessType
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ResponseSpeed</c> member of the <see cref="PIDE_AUTOTUNE"/> data type.
    /// </summary>
    public DINT ResponseSpeed
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TestLength</c> member of the <see cref="PIDE_AUTOTUNE"/> data type.
    /// </summary>
    public REAL TestLength
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PVTuneLimit</c> member of the <see cref="PIDE_AUTOTUNE"/> data type.
    /// </summary>
    public REAL PVTuneLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>StepSize</c> member of the <see cref="PIDE_AUTOTUNE"/> data type.
    /// </summary>
    public REAL StepSize
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TunedGood</c> member of the <see cref="PIDE_AUTOTUNE"/> data type.
    /// </summary>
    public BOOL TunedGood
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TunedUncertain</c> member of the <see cref="PIDE_AUTOTUNE"/> data type.
    /// </summary>
    public BOOL TunedUncertain
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ATuneAcquired</c> member of the <see cref="PIDE_AUTOTUNE"/> data type.
    /// </summary>
    public BOOL ATuneAcquired
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>UsedProcessType</c> member of the <see cref="PIDE_AUTOTUNE"/> data type.
    /// </summary>
    public DINT UsedProcessType
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Gain</c> member of the <see cref="PIDE_AUTOTUNE"/> data type.
    /// </summary>
    public REAL Gain
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TimeConstant</c> member of the <see cref="PIDE_AUTOTUNE"/> data type.
    /// </summary>
    public REAL TimeConstant
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DeadTime</c> member of the <see cref="PIDE_AUTOTUNE"/> data type.
    /// </summary>
    public REAL DeadTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PGainTunedFast</c> member of the <see cref="PIDE_AUTOTUNE"/> data type.
    /// </summary>
    public REAL PGainTunedFast
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>IGainTunedFast</c> member of the <see cref="PIDE_AUTOTUNE"/> data type.
    /// </summary>
    public REAL IGainTunedFast
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DGainTunedFast</c> member of the <see cref="PIDE_AUTOTUNE"/> data type.
    /// </summary>
    public REAL DGainTunedFast
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PGainTunedMed</c> member of the <see cref="PIDE_AUTOTUNE"/> data type.
    /// </summary>
    public REAL PGainTunedMed
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>IGainTunedMed</c> member of the <see cref="PIDE_AUTOTUNE"/> data type.
    /// </summary>
    public REAL IGainTunedMed
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DGainTunedMed</c> member of the <see cref="PIDE_AUTOTUNE"/> data type.
    /// </summary>
    public REAL DGainTunedMed
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PGainTunedSlow</c> member of the <see cref="PIDE_AUTOTUNE"/> data type.
    /// </summary>
    public REAL PGainTunedSlow
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>IGainTunedSlow</c> member of the <see cref="PIDE_AUTOTUNE"/> data type.
    /// </summary>
    public REAL IGainTunedSlow
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DGainTunedSlow</c> member of the <see cref="PIDE_AUTOTUNE"/> data type.
    /// </summary>
    public REAL DGainTunedSlow
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>StepSizeUsed</c> member of the <see cref="PIDE_AUTOTUNE"/> data type.
    /// </summary>
    public REAL StepSizeUsed
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneStatus</c> member of the <see cref="PIDE_AUTOTUNE"/> data type.
    /// </summary>
    public DINT AtuneStatus
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ATuneFault</c> member of the <see cref="PIDE_AUTOTUNE"/> data type.
    /// </summary>
    public BOOL ATuneFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PVOutOfLimit</c> member of the <see cref="PIDE_AUTOTUNE"/> data type.
    /// </summary>
    public BOOL PVOutOfLimit
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ModeInv</c> member of the <see cref="PIDE_AUTOTUNE"/> data type.
    /// </summary>
    public BOOL ModeInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVWindupFault</c> member of the <see cref="PIDE_AUTOTUNE"/> data type.
    /// </summary>
    public BOOL CVWindupFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>StepSizeZero</c> member of the <see cref="PIDE_AUTOTUNE"/> data type.
    /// </summary>
    public BOOL StepSizeZero
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVLimitsFault</c> member of the <see cref="PIDE_AUTOTUNE"/> data type.
    /// </summary>
    public BOOL CVLimitsFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVInitFault</c> member of the <see cref="PIDE_AUTOTUNE"/> data type.
    /// </summary>
    public BOOL CVInitFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EUSpanChanged</c> member of the <see cref="PIDE_AUTOTUNE"/> data type.
    /// </summary>
    public BOOL EUSpanChanged
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVChanged</c> member of the <see cref="PIDE_AUTOTUNE"/> data type.
    /// </summary>
    public BOOL CVChanged
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ATuneTimedOut</c> member of the <see cref="PIDE_AUTOTUNE"/> data type.
    /// </summary>
    public BOOL ATuneTimedOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PVNotSettled</c> member of the <see cref="PIDE_AUTOTUNE"/> data type.
    /// </summary>
    public BOOL PVNotSettled
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PVChangeTooSmall</c> member of the <see cref="PIDE_AUTOTUNE"/> data type.
    /// </summary>
    public BOOL PVChangeTooSmall
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>StepSizeTooSmall</c> member of the <see cref="PIDE_AUTOTUNE"/> data type.
    /// </summary>
    public BOOL StepSizeTooSmall
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>GainTooLarge</c> member of the <see cref="PIDE_AUTOTUNE"/> data type.
    /// </summary>
    public BOOL GainTooLarge
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>GainTooSmall</c> member of the <see cref="PIDE_AUTOTUNE"/> data type.
    /// </summary>
    public BOOL GainTooSmall
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>LongDeadTime</c> member of the <see cref="PIDE_AUTOTUNE"/> data type.
    /// </summary>
    public BOOL LongDeadTime
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}