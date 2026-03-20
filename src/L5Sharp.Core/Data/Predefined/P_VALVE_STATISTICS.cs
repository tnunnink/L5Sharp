using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>P_VALVE_STATISTICS</c> data type structure.
/// </summary>
[LogixData("P_VALVE_STATISTICS")]
public sealed partial class P_VALVE_STATISTICS : StructureData
{
    /// <summary>
    /// Creates a new <see cref="P_VALVE_STATISTICS"/> instance initialized with default values.
    /// </summary>
    public P_VALVE_STATISTICS() : base("P_VALVE_STATISTICS")
    {
        EnableIn = new BOOL();
        EnableOut = new BOOL();
        Inp_InitializeReq = new BOOL();
        Inp_Closed = new BOOL();
        Inp_Opened = new BOOL();
        Inp_StopOther = new BOOL();
        Cfg_HasStopOther = new BOOL();
        Cfg_SlowOpenTime = new REAL();
        Cfg_SlowCloseTime = new REAL();
        PCmd_ClearTotTimes = new BOOL();
        PCmd_ClearMaxTimes = new BOOL();
        PCmd_ClearStrokeCounts = new BOOL();
        PCmd_ClearSlowCounts = new BOOL();
        PCmd_ClearMAvgs = new BOOL();
        Val_CurrClosedTime = new REAL();
        Val_LastClosedTime = new REAL();
        Val_TotClosedTime = new REAL();
        Val_MaxClosedTime = new REAL();
        Val_CurrOpeningTime = new REAL();
        Val_LastOpeningTime = new REAL();
        Val_TotOpeningTime = new REAL();
        Val_MaxOpeningTime = new REAL();
        Val_MAvgOpeningTime = new REAL();
        Val_CurrOpenedTime = new REAL();
        Val_LastOpenedTime = new REAL();
        Val_TotOpenedTime = new REAL();
        Val_MaxOpenedTime = new REAL();
        Val_CurrClosingTime = new REAL();
        Val_LastClosingTime = new REAL();
        Val_TotClosingTime = new REAL();
        Val_MaxClosingTime = new REAL();
        Val_MAvgClosingTime = new REAL();
        Val_CurrStopOtherTime = new REAL();
        Val_LastStopOtherTime = new REAL();
        Val_TotStopOtherTime = new REAL();
        Val_MaxStopOtherTime = new REAL();
        Val_CpltOpenCount = new DINT();
        Val_CpltCloseCount = new DINT();
        Val_IncpltOpenCount = new DINT();
        Val_IncpltCloseCount = new DINT();
        Val_StopOtherCount = new DINT();
        Val_SlowOpenCount = new DINT();
        Val_SlowCloseCount = new DINT();
        Sts_Initialized = new BOOL();
        Sts_SlowOpen = new BOOL();
        Sts_SlowClose = new BOOL();
        Sts_Err = new BOOL();
        Sts_ErrSlowCloseTime = new BOOL();
        Sts_ErrSlowOpenTime = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="P_VALVE_STATISTICS"/> instance initialized with the provided element.
    /// </summary>
    public P_VALVE_STATISTICS(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 464;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        EnableIn.UpdateData((data[offset + 5] & (1 << 0)) != 0);
        EnableOut.UpdateData((data[offset + 5] & (1 << 1)) != 0);
        Inp_InitializeReq.UpdateData((data[offset + 5] & (1 << 2)) != 0);
        Inp_Closed.UpdateData((data[offset + 5] & (1 << 3)) != 0);
        Inp_Opened.UpdateData((data[offset + 5] & (1 << 4)) != 0);
        Inp_StopOther.UpdateData((data[offset + 5] & (1 << 5)) != 0);
        Cfg_HasStopOther.UpdateData((data[offset + 5] & (1 << 6)) != 0);
        Cfg_SlowOpenTime.UpdateData(data, offset + 5);
        Cfg_SlowCloseTime.UpdateData(data, offset + 9);
        PCmd_ClearTotTimes.UpdateData((data[offset + 13] & (1 << 7)) != 0);
        PCmd_ClearMaxTimes.UpdateData((data[offset + 14] & (1 << 0)) != 0);
        PCmd_ClearStrokeCounts.UpdateData((data[offset + 14] & (1 << 1)) != 0);
        PCmd_ClearSlowCounts.UpdateData((data[offset + 14] & (1 << 2)) != 0);
        PCmd_ClearMAvgs.UpdateData((data[offset + 14] & (1 << 3)) != 0);
        Val_CurrClosedTime.UpdateData(data, offset + 14);
        Val_LastClosedTime.UpdateData(data, offset + 18);
        Val_TotClosedTime.UpdateData(data, offset + 22);
        Val_MaxClosedTime.UpdateData(data, offset + 26);
        Val_CurrOpeningTime.UpdateData(data, offset + 30);
        Val_LastOpeningTime.UpdateData(data, offset + 34);
        Val_TotOpeningTime.UpdateData(data, offset + 38);
        Val_MaxOpeningTime.UpdateData(data, offset + 42);
        Val_MAvgOpeningTime.UpdateData(data, offset + 46);
        Val_CurrOpenedTime.UpdateData(data, offset + 50);
        Val_LastOpenedTime.UpdateData(data, offset + 54);
        Val_TotOpenedTime.UpdateData(data, offset + 58);
        Val_MaxOpenedTime.UpdateData(data, offset + 62);
        Val_CurrClosingTime.UpdateData(data, offset + 66);
        Val_LastClosingTime.UpdateData(data, offset + 70);
        Val_TotClosingTime.UpdateData(data, offset + 74);
        Val_MaxClosingTime.UpdateData(data, offset + 78);
        Val_MAvgClosingTime.UpdateData(data, offset + 82);
        Val_CurrStopOtherTime.UpdateData(data, offset + 86);
        Val_LastStopOtherTime.UpdateData(data, offset + 90);
        Val_TotStopOtherTime.UpdateData(data, offset + 94);
        Val_MaxStopOtherTime.UpdateData(data, offset + 98);
        Val_CpltOpenCount.UpdateData(data, offset + 102);
        Val_CpltCloseCount.UpdateData(data, offset + 106);
        Val_IncpltOpenCount.UpdateData(data, offset + 110);
        Val_IncpltCloseCount.UpdateData(data, offset + 114);
        Val_StopOtherCount.UpdateData(data, offset + 118);
        Val_SlowOpenCount.UpdateData(data, offset + 122);
        Val_SlowCloseCount.UpdateData(data, offset + 126);
        Sts_Initialized.UpdateData((data[offset + 130] & (1 << 4)) != 0);
        Sts_SlowOpen.UpdateData((data[offset + 130] & (1 << 5)) != 0);
        Sts_SlowClose.UpdateData((data[offset + 130] & (1 << 6)) != 0);
        Sts_Err.UpdateData((data[offset + 130] & (1 << 7)) != 0);
        Sts_ErrSlowCloseTime.UpdateData((data[offset + 131] & (1 << 0)) != 0);
        Sts_ErrSlowOpenTime.UpdateData((data[offset + 131] & (1 << 1)) != 0);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="P_VALVE_STATISTICS"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="P_VALVE_STATISTICS"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_InitializeReq</c> member of the <see cref="P_VALVE_STATISTICS"/> data type.
    /// </summary>
    public BOOL Inp_InitializeReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Closed</c> member of the <see cref="P_VALVE_STATISTICS"/> data type.
    /// </summary>
    public BOOL Inp_Closed
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Opened</c> member of the <see cref="P_VALVE_STATISTICS"/> data type.
    /// </summary>
    public BOOL Inp_Opened
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_StopOther</c> member of the <see cref="P_VALVE_STATISTICS"/> data type.
    /// </summary>
    public BOOL Inp_StopOther
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasStopOther</c> member of the <see cref="P_VALVE_STATISTICS"/> data type.
    /// </summary>
    public BOOL Cfg_HasStopOther
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_SlowOpenTime</c> member of the <see cref="P_VALVE_STATISTICS"/> data type.
    /// </summary>
    public REAL Cfg_SlowOpenTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_SlowCloseTime</c> member of the <see cref="P_VALVE_STATISTICS"/> data type.
    /// </summary>
    public REAL Cfg_SlowCloseTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_ClearTotTimes</c> member of the <see cref="P_VALVE_STATISTICS"/> data type.
    /// </summary>
    public BOOL PCmd_ClearTotTimes
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_ClearMaxTimes</c> member of the <see cref="P_VALVE_STATISTICS"/> data type.
    /// </summary>
    public BOOL PCmd_ClearMaxTimes
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_ClearStrokeCounts</c> member of the <see cref="P_VALVE_STATISTICS"/> data type.
    /// </summary>
    public BOOL PCmd_ClearStrokeCounts
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_ClearSlowCounts</c> member of the <see cref="P_VALVE_STATISTICS"/> data type.
    /// </summary>
    public BOOL PCmd_ClearSlowCounts
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_ClearMAvgs</c> member of the <see cref="P_VALVE_STATISTICS"/> data type.
    /// </summary>
    public BOOL PCmd_ClearMAvgs
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_CurrClosedTime</c> member of the <see cref="P_VALVE_STATISTICS"/> data type.
    /// </summary>
    public REAL Val_CurrClosedTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_LastClosedTime</c> member of the <see cref="P_VALVE_STATISTICS"/> data type.
    /// </summary>
    public REAL Val_LastClosedTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_TotClosedTime</c> member of the <see cref="P_VALVE_STATISTICS"/> data type.
    /// </summary>
    public REAL Val_TotClosedTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_MaxClosedTime</c> member of the <see cref="P_VALVE_STATISTICS"/> data type.
    /// </summary>
    public REAL Val_MaxClosedTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_CurrOpeningTime</c> member of the <see cref="P_VALVE_STATISTICS"/> data type.
    /// </summary>
    public REAL Val_CurrOpeningTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_LastOpeningTime</c> member of the <see cref="P_VALVE_STATISTICS"/> data type.
    /// </summary>
    public REAL Val_LastOpeningTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_TotOpeningTime</c> member of the <see cref="P_VALVE_STATISTICS"/> data type.
    /// </summary>
    public REAL Val_TotOpeningTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_MaxOpeningTime</c> member of the <see cref="P_VALVE_STATISTICS"/> data type.
    /// </summary>
    public REAL Val_MaxOpeningTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_MAvgOpeningTime</c> member of the <see cref="P_VALVE_STATISTICS"/> data type.
    /// </summary>
    public REAL Val_MAvgOpeningTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_CurrOpenedTime</c> member of the <see cref="P_VALVE_STATISTICS"/> data type.
    /// </summary>
    public REAL Val_CurrOpenedTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_LastOpenedTime</c> member of the <see cref="P_VALVE_STATISTICS"/> data type.
    /// </summary>
    public REAL Val_LastOpenedTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_TotOpenedTime</c> member of the <see cref="P_VALVE_STATISTICS"/> data type.
    /// </summary>
    public REAL Val_TotOpenedTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_MaxOpenedTime</c> member of the <see cref="P_VALVE_STATISTICS"/> data type.
    /// </summary>
    public REAL Val_MaxOpenedTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_CurrClosingTime</c> member of the <see cref="P_VALVE_STATISTICS"/> data type.
    /// </summary>
    public REAL Val_CurrClosingTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_LastClosingTime</c> member of the <see cref="P_VALVE_STATISTICS"/> data type.
    /// </summary>
    public REAL Val_LastClosingTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_TotClosingTime</c> member of the <see cref="P_VALVE_STATISTICS"/> data type.
    /// </summary>
    public REAL Val_TotClosingTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_MaxClosingTime</c> member of the <see cref="P_VALVE_STATISTICS"/> data type.
    /// </summary>
    public REAL Val_MaxClosingTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_MAvgClosingTime</c> member of the <see cref="P_VALVE_STATISTICS"/> data type.
    /// </summary>
    public REAL Val_MAvgClosingTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_CurrStopOtherTime</c> member of the <see cref="P_VALVE_STATISTICS"/> data type.
    /// </summary>
    public REAL Val_CurrStopOtherTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_LastStopOtherTime</c> member of the <see cref="P_VALVE_STATISTICS"/> data type.
    /// </summary>
    public REAL Val_LastStopOtherTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_TotStopOtherTime</c> member of the <see cref="P_VALVE_STATISTICS"/> data type.
    /// </summary>
    public REAL Val_TotStopOtherTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_MaxStopOtherTime</c> member of the <see cref="P_VALVE_STATISTICS"/> data type.
    /// </summary>
    public REAL Val_MaxStopOtherTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_CpltOpenCount</c> member of the <see cref="P_VALVE_STATISTICS"/> data type.
    /// </summary>
    public DINT Val_CpltOpenCount
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_CpltCloseCount</c> member of the <see cref="P_VALVE_STATISTICS"/> data type.
    /// </summary>
    public DINT Val_CpltCloseCount
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_IncpltOpenCount</c> member of the <see cref="P_VALVE_STATISTICS"/> data type.
    /// </summary>
    public DINT Val_IncpltOpenCount
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_IncpltCloseCount</c> member of the <see cref="P_VALVE_STATISTICS"/> data type.
    /// </summary>
    public DINT Val_IncpltCloseCount
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_StopOtherCount</c> member of the <see cref="P_VALVE_STATISTICS"/> data type.
    /// </summary>
    public DINT Val_StopOtherCount
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_SlowOpenCount</c> member of the <see cref="P_VALVE_STATISTICS"/> data type.
    /// </summary>
    public DINT Val_SlowOpenCount
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_SlowCloseCount</c> member of the <see cref="P_VALVE_STATISTICS"/> data type.
    /// </summary>
    public DINT Val_SlowCloseCount
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Initialized</c> member of the <see cref="P_VALVE_STATISTICS"/> data type.
    /// </summary>
    public BOOL Sts_Initialized
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_SlowOpen</c> member of the <see cref="P_VALVE_STATISTICS"/> data type.
    /// </summary>
    public BOOL Sts_SlowOpen
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_SlowClose</c> member of the <see cref="P_VALVE_STATISTICS"/> data type.
    /// </summary>
    public BOOL Sts_SlowClose
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Err</c> member of the <see cref="P_VALVE_STATISTICS"/> data type.
    /// </summary>
    public BOOL Sts_Err
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrSlowCloseTime</c> member of the <see cref="P_VALVE_STATISTICS"/> data type.
    /// </summary>
    public BOOL Sts_ErrSlowCloseTime
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrSlowOpenTime</c> member of the <see cref="P_VALVE_STATISTICS"/> data type.
    /// </summary>
    public BOOL Sts_ErrSlowOpenTime
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}