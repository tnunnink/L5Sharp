using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>TOTALIZER</c> data type structure.
/// </summary>
[LogixData("TOTALIZER")]
public sealed partial class TOTALIZER : StructureData
{
    /// <summary>
    /// Creates a new <see cref="TOTALIZER"/> instance initialized with default values.
    /// </summary>
    public TOTALIZER() : base("TOTALIZER")
    {
        EnableIn = new BOOL();
        In = new REAL();
        InFault = new BOOL();
        TimeBase = new DINT();
        Gain = new REAL();
        ResetValue = new REAL();
        Target = new REAL();
        TargetDev1 = new REAL();
        TargetDev2 = new REAL();
        LowInCutoff = new REAL();
        ProgProgReq = new BOOL();
        ProgOperReq = new BOOL();
        ProgStartReq = new BOOL();
        ProgStopReq = new BOOL();
        ProgResetReq = new BOOL();
        OperProgReq = new BOOL();
        OperOperReq = new BOOL();
        OperStartReq = new BOOL();
        OperStopReq = new BOOL();
        OperResetReq = new BOOL();
        ProgValueReset = new BOOL();
        TimingMode = new DINT();
        OversampleDT = new REAL();
        RTSTime = new DINT();
        RTSTimeStamp = new DINT();
        EnableOut = new BOOL();
        Total = new REAL();
        OldTotal = new REAL();
        ProgOper = new BOOL();
        RunStop = new BOOL();
        ProgResetDone = new BOOL();
        TargetFlag = new BOOL();
        TargetDev1Flag = new BOOL();
        TargetDev2Flag = new BOOL();
        LowInCutoffFlag = new BOOL();
        DeltaT = new REAL();
        Status = new DINT();
        InstructFault = new BOOL();
        InFaulted = new BOOL();
        TimeBaseInv = new BOOL();
        TimingModeInv = new BOOL();
        RTSMissed = new BOOL();
        RTSTimeInv = new BOOL();
        RTSTimeStampInv = new BOOL();
        DeltaTInv = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="TOTALIZER"/> instance initialized with the provided element.
    /// </summary>
    public TOTALIZER(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="TOTALIZER"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>In</c> member of the <see cref="TOTALIZER"/> data type.
    /// </summary>
    public REAL In
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InFault</c> member of the <see cref="TOTALIZER"/> data type.
    /// </summary>
    public BOOL InFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TimeBase</c> member of the <see cref="TOTALIZER"/> data type.
    /// </summary>
    public DINT TimeBase
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Gain</c> member of the <see cref="TOTALIZER"/> data type.
    /// </summary>
    public REAL Gain
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ResetValue</c> member of the <see cref="TOTALIZER"/> data type.
    /// </summary>
    public REAL ResetValue
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Target</c> member of the <see cref="TOTALIZER"/> data type.
    /// </summary>
    public REAL Target
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TargetDev1</c> member of the <see cref="TOTALIZER"/> data type.
    /// </summary>
    public REAL TargetDev1
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TargetDev2</c> member of the <see cref="TOTALIZER"/> data type.
    /// </summary>
    public REAL TargetDev2
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>LowInCutoff</c> member of the <see cref="TOTALIZER"/> data type.
    /// </summary>
    public REAL LowInCutoff
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgProgReq</c> member of the <see cref="TOTALIZER"/> data type.
    /// </summary>
    public BOOL ProgProgReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgOperReq</c> member of the <see cref="TOTALIZER"/> data type.
    /// </summary>
    public BOOL ProgOperReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgStartReq</c> member of the <see cref="TOTALIZER"/> data type.
    /// </summary>
    public BOOL ProgStartReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgStopReq</c> member of the <see cref="TOTALIZER"/> data type.
    /// </summary>
    public BOOL ProgStopReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgResetReq</c> member of the <see cref="TOTALIZER"/> data type.
    /// </summary>
    public BOOL ProgResetReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OperProgReq</c> member of the <see cref="TOTALIZER"/> data type.
    /// </summary>
    public BOOL OperProgReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OperOperReq</c> member of the <see cref="TOTALIZER"/> data type.
    /// </summary>
    public BOOL OperOperReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OperStartReq</c> member of the <see cref="TOTALIZER"/> data type.
    /// </summary>
    public BOOL OperStartReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OperStopReq</c> member of the <see cref="TOTALIZER"/> data type.
    /// </summary>
    public BOOL OperStopReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OperResetReq</c> member of the <see cref="TOTALIZER"/> data type.
    /// </summary>
    public BOOL OperResetReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgValueReset</c> member of the <see cref="TOTALIZER"/> data type.
    /// </summary>
    public BOOL ProgValueReset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TimingMode</c> member of the <see cref="TOTALIZER"/> data type.
    /// </summary>
    public DINT TimingMode
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OversampleDT</c> member of the <see cref="TOTALIZER"/> data type.
    /// </summary>
    public REAL OversampleDT
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RTSTime</c> member of the <see cref="TOTALIZER"/> data type.
    /// </summary>
    public DINT RTSTime
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RTSTimeStamp</c> member of the <see cref="TOTALIZER"/> data type.
    /// </summary>
    public DINT RTSTimeStamp
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="TOTALIZER"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Total</c> member of the <see cref="TOTALIZER"/> data type.
    /// </summary>
    public REAL Total
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OldTotal</c> member of the <see cref="TOTALIZER"/> data type.
    /// </summary>
    public REAL OldTotal
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgOper</c> member of the <see cref="TOTALIZER"/> data type.
    /// </summary>
    public BOOL ProgOper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RunStop</c> member of the <see cref="TOTALIZER"/> data type.
    /// </summary>
    public BOOL RunStop
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgResetDone</c> member of the <see cref="TOTALIZER"/> data type.
    /// </summary>
    public BOOL ProgResetDone
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TargetFlag</c> member of the <see cref="TOTALIZER"/> data type.
    /// </summary>
    public BOOL TargetFlag
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TargetDev1Flag</c> member of the <see cref="TOTALIZER"/> data type.
    /// </summary>
    public BOOL TargetDev1Flag
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TargetDev2Flag</c> member of the <see cref="TOTALIZER"/> data type.
    /// </summary>
    public BOOL TargetDev2Flag
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>LowInCutoffFlag</c> member of the <see cref="TOTALIZER"/> data type.
    /// </summary>
    public BOOL LowInCutoffFlag
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DeltaT</c> member of the <see cref="TOTALIZER"/> data type.
    /// </summary>
    public REAL DeltaT
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Status</c> member of the <see cref="TOTALIZER"/> data type.
    /// </summary>
    public DINT Status
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InstructFault</c> member of the <see cref="TOTALIZER"/> data type.
    /// </summary>
    public BOOL InstructFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InFaulted</c> member of the <see cref="TOTALIZER"/> data type.
    /// </summary>
    public BOOL InFaulted
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TimeBaseInv</c> member of the <see cref="TOTALIZER"/> data type.
    /// </summary>
    public BOOL TimeBaseInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TimingModeInv</c> member of the <see cref="TOTALIZER"/> data type.
    /// </summary>
    public BOOL TimingModeInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RTSMissed</c> member of the <see cref="TOTALIZER"/> data type.
    /// </summary>
    public BOOL RTSMissed
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RTSTimeInv</c> member of the <see cref="TOTALIZER"/> data type.
    /// </summary>
    public BOOL RTSTimeInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RTSTimeStampInv</c> member of the <see cref="TOTALIZER"/> data type.
    /// </summary>
    public BOOL RTSTimeStampInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DeltaTInv</c> member of the <see cref="TOTALIZER"/> data type.
    /// </summary>
    public BOOL DeltaTInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}