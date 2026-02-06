using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>PID_ENHANCED</c> data type structure.
/// </summary>
[LogixData("PID_ENHANCED")]
public sealed partial class PID_ENHANCED : StructureData
{
    /// <summary>
    /// Creates a new <see cref="PID_ENHANCED"/> instance initialized with default values.
    /// </summary>
    public PID_ENHANCED() : base("PID_ENHANCED")
    {
        EnableIn = new BOOL();
        PV = new REAL();
        PVFault = new BOOL();
        PVEUMax = new REAL();
        PVEUMin = new REAL();
        SPProg = new REAL();
        SPOper = new REAL();
        SPCascade = new REAL();
        SPHLimit = new REAL();
        SPLLimit = new REAL();
        UseRatio = new BOOL();
        RatioProg = new REAL();
        RatioOper = new REAL();
        RatioHLimit = new REAL();
        RatioLLimit = new REAL();
        CVFault = new BOOL();
        CVInitReq = new BOOL();
        CVInitValue = new REAL();
        CVProg = new REAL();
        CVOper = new REAL();
        CVOverride = new REAL();
        CVPrevious = new REAL();
        CVSetPrevious = new BOOL();
        CVManLimiting = new BOOL();
        CVEUMax = new REAL();
        CVEUMin = new REAL();
        CVHLimit = new REAL();
        CVLLimit = new REAL();
        CVROCLimit = new REAL();
        FF = new REAL();
        FFPrevious = new REAL();
        FFSetPrevious = new BOOL();
        HandFB = new REAL();
        HandFBFault = new BOOL();
        WindupHIn = new BOOL();
        WindupLIn = new BOOL();
        ControlAction = new BOOL();
        DependIndepend = new BOOL();
        PGain = new REAL();
        IGain = new REAL();
        DGain = new REAL();
        PVEProportional = new BOOL();
        PVEDerivative = new BOOL();
        DSmoothing = new BOOL();
        PVTracking = new BOOL();
        ZCDeadband = new REAL();
        ZCOff = new BOOL();
        PVHHLimit = new REAL();
        PVHLimit = new REAL();
        PVLLimit = new REAL();
        PVLLLimit = new REAL();
        PVDeadband = new REAL();
        PVROCPosLimit = new REAL();
        PVROCNegLimit = new REAL();
        PVROCPeriod = new REAL();
        DevHHLimit = new REAL();
        DevHLimit = new REAL();
        DevLLimit = new REAL();
        DevLLLimit = new REAL();
        DevDeadband = new REAL();
        AllowCasRat = new BOOL();
        ManualAfterInit = new BOOL();
        ProgProgReq = new BOOL();
        ProgOperReq = new BOOL();
        ProgCasRatReq = new BOOL();
        ProgAutoReq = new BOOL();
        ProgManualReq = new BOOL();
        ProgOverrideReq = new BOOL();
        ProgHandReq = new BOOL();
        OperProgReq = new BOOL();
        OperOperReq = new BOOL();
        OperCasRatReq = new BOOL();
        OperAutoReq = new BOOL();
        OperManualReq = new BOOL();
        ProgValueReset = new BOOL();
        TimingMode = new DINT();
        OversampleDT = new REAL();
        RTSTime = new DINT();
        RTSTimeStamp = new DINT();
        AtuneAcquire = new BOOL();
        AtuneStart = new BOOL();
        AtuneUseGains = new BOOL();
        AtuneAbort = new BOOL();
        AtuneUnacquire = new BOOL();
        EnableOut = new BOOL();
        CVEU = new REAL();
        CV = new REAL();
        CVInitializing = new BOOL();
        CVHAlarm = new BOOL();
        CVLAlarm = new BOOL();
        CVROCAlarm = new BOOL();
        SP = new REAL();
        SPPercent = new REAL();
        SPHAlarm = new BOOL();
        SPLAlarm = new BOOL();
        PVPercent = new REAL();
        E = new REAL();
        EPercent = new REAL();
        InitPrimary = new BOOL();
        WindupHOut = new BOOL();
        WindupLOut = new BOOL();
        Ratio = new REAL();
        RatioHAlarm = new BOOL();
        RatioLAlarm = new BOOL();
        ZCDeadbandOn = new BOOL();
        PVHHAlarm = new BOOL();
        PVHAlarm = new BOOL();
        PVLAlarm = new BOOL();
        PVLLAlarm = new BOOL();
        PVROCPosAlarm = new BOOL();
        PVROCNegAlarm = new BOOL();
        DevHHAlarm = new BOOL();
        DevHAlarm = new BOOL();
        DevLAlarm = new BOOL();
        DevLLAlarm = new BOOL();
        ProgOper = new BOOL();
        CasRat = new BOOL();
        Auto = new BOOL();
        Manual = new BOOL();
        Override = new BOOL();
        Hand = new BOOL();
        DeltaT = new REAL();
        AtuneReady = new BOOL();
        AtuneOn = new BOOL();
        AtuneDone = new BOOL();
        AtuneAborted = new BOOL();
        AtuneBusy = new BOOL();
        Status1 = new DINT();
        Status2 = new DINT();
        InstructFault = new BOOL();
        PVFaulted = new BOOL();
        CVFaulted = new BOOL();
        HandFBFaulted = new BOOL();
        PVSpanInv = new BOOL();
        SPProgInv = new BOOL();
        SPOperInv = new BOOL();
        SPCascadeInv = new BOOL();
        SPLimitsInv = new BOOL();
        RatioProgInv = new BOOL();
        RatioOperInv = new BOOL();
        RatioLimitsInv = new BOOL();
        CVProgInv = new BOOL();
        CVOperInv = new BOOL();
        CVOverrideInv = new BOOL();
        CVPreviousInv = new BOOL();
        CVEUSpanInv = new BOOL();
        CVLimitsInv = new BOOL();
        CVROCLimitInv = new BOOL();
        FFInv = new BOOL();
        FFPreviousInv = new BOOL();
        HandFBInv = new BOOL();
        PGainInv = new BOOL();
        IGainInv = new BOOL();
        DGainInv = new BOOL();
        ZCDeadbandInv = new BOOL();
        PVDeadbandInv = new BOOL();
        PVROCLimitsInv = new BOOL();
        DevHLLimitsInv = new BOOL();
        DevDeadbandInv = new BOOL();
        AtuneDataInv = new BOOL();
        TimingModeInv = new BOOL();
        RTSMissed = new BOOL();
        RTSTimeInv = new BOOL();
        RTSTimeStampInv = new BOOL();
        DeltaTInv = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="PID_ENHANCED"/> instance initialized with the provided element.
    /// </summary>
    public PID_ENHANCED(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 396;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        EnableIn.UpdateData((data[offset + 1] & (1 << 0)) != 0);
        PV.UpdateData(data, offset + 5);
        PVFault.UpdateData((data[offset + 9] & (1 << 1)) != 0);
        PVEUMax.UpdateData(data, offset + 9);
        PVEUMin.UpdateData(data, offset + 13);
        SPProg.UpdateData(data, offset + 17);
        SPOper.UpdateData(data, offset + 21);
        SPCascade.UpdateData(data, offset + 25);
        SPHLimit.UpdateData(data, offset + 29);
        SPLLimit.UpdateData(data, offset + 33);
        UseRatio.UpdateData((data[offset + 37] & (1 << 2)) != 0);
        RatioProg.UpdateData(data, offset + 37);
        RatioOper.UpdateData(data, offset + 41);
        RatioHLimit.UpdateData(data, offset + 45);
        RatioLLimit.UpdateData(data, offset + 49);
        CVFault.UpdateData((data[offset + 53] & (1 << 3)) != 0);
        CVInitReq.UpdateData((data[offset + 53] & (1 << 4)) != 0);
        CVInitValue.UpdateData(data, offset + 53);
        CVProg.UpdateData(data, offset + 57);
        CVOper.UpdateData(data, offset + 61);
        CVOverride.UpdateData(data, offset + 65);
        CVPrevious.UpdateData(data, offset + 69);
        CVSetPrevious.UpdateData((data[offset + 73] & (1 << 5)) != 0);
        CVManLimiting.UpdateData((data[offset + 73] & (1 << 6)) != 0);
        CVEUMax.UpdateData(data, offset + 73);
        CVEUMin.UpdateData(data, offset + 77);
        CVHLimit.UpdateData(data, offset + 81);
        CVLLimit.UpdateData(data, offset + 85);
        CVROCLimit.UpdateData(data, offset + 89);
        FF.UpdateData(data, offset + 93);
        FFPrevious.UpdateData(data, offset + 97);
        FFSetPrevious.UpdateData((data[offset + 101] & (1 << 7)) != 0);
        HandFB.UpdateData(data, offset + 101);
        HandFBFault.UpdateData((data[offset + 106] & (1 << 0)) != 0);
        WindupHIn.UpdateData((data[offset + 106] & (1 << 1)) != 0);
        WindupLIn.UpdateData((data[offset + 106] & (1 << 2)) != 0);
        ControlAction.UpdateData((data[offset + 106] & (1 << 3)) != 0);
        DependIndepend.UpdateData((data[offset + 106] & (1 << 4)) != 0);
        PGain.UpdateData(data, offset + 106);
        IGain.UpdateData(data, offset + 110);
        DGain.UpdateData(data, offset + 114);
        PVEProportional.UpdateData((data[offset + 118] & (1 << 5)) != 0);
        PVEDerivative.UpdateData((data[offset + 118] & (1 << 6)) != 0);
        DSmoothing.UpdateData((data[offset + 118] & (1 << 7)) != 0);
        PVTracking.UpdateData((data[offset + 119] & (1 << 0)) != 0);
        ZCDeadband.UpdateData(data, offset + 119);
        ZCOff.UpdateData((data[offset + 123] & (1 << 1)) != 0);
        PVHHLimit.UpdateData(data, offset + 123);
        PVHLimit.UpdateData(data, offset + 127);
        PVLLimit.UpdateData(data, offset + 131);
        PVLLLimit.UpdateData(data, offset + 135);
        PVDeadband.UpdateData(data, offset + 139);
        PVROCPosLimit.UpdateData(data, offset + 143);
        PVROCNegLimit.UpdateData(data, offset + 147);
        PVROCPeriod.UpdateData(data, offset + 151);
        DevHHLimit.UpdateData(data, offset + 155);
        DevHLimit.UpdateData(data, offset + 159);
        DevLLimit.UpdateData(data, offset + 163);
        DevLLLimit.UpdateData(data, offset + 167);
        DevDeadband.UpdateData(data, offset + 171);
        AllowCasRat.UpdateData((data[offset + 175] & (1 << 2)) != 0);
        ManualAfterInit.UpdateData((data[offset + 175] & (1 << 3)) != 0);
        ProgProgReq.UpdateData((data[offset + 175] & (1 << 4)) != 0);
        ProgOperReq.UpdateData((data[offset + 175] & (1 << 5)) != 0);
        ProgCasRatReq.UpdateData((data[offset + 175] & (1 << 6)) != 0);
        ProgAutoReq.UpdateData((data[offset + 175] & (1 << 7)) != 0);
        ProgManualReq.UpdateData((data[offset + 176] & (1 << 0)) != 0);
        ProgOverrideReq.UpdateData((data[offset + 176] & (1 << 1)) != 0);
        ProgHandReq.UpdateData((data[offset + 176] & (1 << 2)) != 0);
        OperProgReq.UpdateData((data[offset + 176] & (1 << 3)) != 0);
        OperOperReq.UpdateData((data[offset + 176] & (1 << 4)) != 0);
        OperCasRatReq.UpdateData((data[offset + 176] & (1 << 5)) != 0);
        OperAutoReq.UpdateData((data[offset + 176] & (1 << 6)) != 0);
        OperManualReq.UpdateData((data[offset + 176] & (1 << 7)) != 0);
        ProgValueReset.UpdateData((data[offset + 177] & (1 << 0)) != 0);
        TimingMode.UpdateData(data, offset + 177);
        OversampleDT.UpdateData(data, offset + 181);
        RTSTime.UpdateData(data, offset + 185);
        RTSTimeStamp.UpdateData(data, offset + 189);
        AtuneAcquire.UpdateData((data[offset + 197] & (1 << 1)) != 0);
        AtuneStart.UpdateData((data[offset + 197] & (1 << 2)) != 0);
        AtuneUseGains.UpdateData((data[offset + 197] & (1 << 3)) != 0);
        AtuneAbort.UpdateData((data[offset + 197] & (1 << 4)) != 0);
        AtuneUnacquire.UpdateData((data[offset + 197] & (1 << 5)) != 0);
        EnableOut.UpdateData((data[offset + 197] & (1 << 6)) != 0);
        CVEU.UpdateData(data, offset + 201);
        CV.UpdateData(data, offset + 205);
        CVInitializing.UpdateData((data[offset + 209] & (1 << 7)) != 0);
        CVHAlarm.UpdateData((data[offset + 210] & (1 << 0)) != 0);
        CVLAlarm.UpdateData((data[offset + 210] & (1 << 1)) != 0);
        CVROCAlarm.UpdateData((data[offset + 210] & (1 << 2)) != 0);
        SP.UpdateData(data, offset + 210);
        SPPercent.UpdateData(data, offset + 214);
        SPHAlarm.UpdateData((data[offset + 218] & (1 << 3)) != 0);
        SPLAlarm.UpdateData((data[offset + 218] & (1 << 4)) != 0);
        PVPercent.UpdateData(data, offset + 218);
        E.UpdateData(data, offset + 222);
        EPercent.UpdateData(data, offset + 226);
        InitPrimary.UpdateData((data[offset + 230] & (1 << 5)) != 0);
        WindupHOut.UpdateData((data[offset + 230] & (1 << 6)) != 0);
        WindupLOut.UpdateData((data[offset + 230] & (1 << 7)) != 0);
        Ratio.UpdateData(data, offset + 230);
        RatioHAlarm.UpdateData((data[offset + 235] & (1 << 0)) != 0);
        RatioLAlarm.UpdateData((data[offset + 235] & (1 << 1)) != 0);
        ZCDeadbandOn.UpdateData((data[offset + 235] & (1 << 2)) != 0);
        PVHHAlarm.UpdateData((data[offset + 235] & (1 << 3)) != 0);
        PVHAlarm.UpdateData((data[offset + 235] & (1 << 4)) != 0);
        PVLAlarm.UpdateData((data[offset + 235] & (1 << 5)) != 0);
        PVLLAlarm.UpdateData((data[offset + 235] & (1 << 6)) != 0);
        PVROCPosAlarm.UpdateData((data[offset + 235] & (1 << 7)) != 0);
        PVROCNegAlarm.UpdateData((data[offset + 236] & (1 << 0)) != 0);
        DevHHAlarm.UpdateData((data[offset + 236] & (1 << 1)) != 0);
        DevHAlarm.UpdateData((data[offset + 236] & (1 << 2)) != 0);
        DevLAlarm.UpdateData((data[offset + 236] & (1 << 3)) != 0);
        DevLLAlarm.UpdateData((data[offset + 236] & (1 << 4)) != 0);
        ProgOper.UpdateData((data[offset + 236] & (1 << 5)) != 0);
        CasRat.UpdateData((data[offset + 236] & (1 << 6)) != 0);
        Auto.UpdateData((data[offset + 236] & (1 << 7)) != 0);
        Manual.UpdateData((data[offset + 237] & (1 << 0)) != 0);
        Override.UpdateData((data[offset + 237] & (1 << 1)) != 0);
        Hand.UpdateData((data[offset + 237] & (1 << 2)) != 0);
        DeltaT.UpdateData(data, offset + 237);
        AtuneReady.UpdateData((data[offset + 245] & (1 << 3)) != 0);
        AtuneOn.UpdateData((data[offset + 245] & (1 << 4)) != 0);
        AtuneDone.UpdateData((data[offset + 245] & (1 << 5)) != 0);
        AtuneAborted.UpdateData((data[offset + 245] & (1 << 6)) != 0);
        AtuneBusy.UpdateData((data[offset + 245] & (1 << 7)) != 0);
        Status1.UpdateData(data, offset + 245);
        Status2.UpdateData(data, offset + 249);
        InstructFault.UpdateData((data[offset + 254] & (1 << 0)) != 0);
        PVFaulted.UpdateData((data[offset + 254] & (1 << 1)) != 0);
        CVFaulted.UpdateData((data[offset + 254] & (1 << 2)) != 0);
        HandFBFaulted.UpdateData((data[offset + 254] & (1 << 3)) != 0);
        PVSpanInv.UpdateData((data[offset + 254] & (1 << 4)) != 0);
        SPProgInv.UpdateData((data[offset + 254] & (1 << 5)) != 0);
        SPOperInv.UpdateData((data[offset + 254] & (1 << 6)) != 0);
        SPCascadeInv.UpdateData((data[offset + 254] & (1 << 7)) != 0);
        SPLimitsInv.UpdateData((data[offset + 255] & (1 << 0)) != 0);
        RatioProgInv.UpdateData((data[offset + 255] & (1 << 1)) != 0);
        RatioOperInv.UpdateData((data[offset + 255] & (1 << 2)) != 0);
        RatioLimitsInv.UpdateData((data[offset + 255] & (1 << 3)) != 0);
        CVProgInv.UpdateData((data[offset + 255] & (1 << 4)) != 0);
        CVOperInv.UpdateData((data[offset + 255] & (1 << 5)) != 0);
        CVOverrideInv.UpdateData((data[offset + 255] & (1 << 6)) != 0);
        CVPreviousInv.UpdateData((data[offset + 255] & (1 << 7)) != 0);
        CVEUSpanInv.UpdateData((data[offset + 256] & (1 << 0)) != 0);
        CVLimitsInv.UpdateData((data[offset + 256] & (1 << 1)) != 0);
        CVROCLimitInv.UpdateData((data[offset + 256] & (1 << 2)) != 0);
        FFInv.UpdateData((data[offset + 256] & (1 << 3)) != 0);
        FFPreviousInv.UpdateData((data[offset + 256] & (1 << 4)) != 0);
        HandFBInv.UpdateData((data[offset + 256] & (1 << 5)) != 0);
        PGainInv.UpdateData((data[offset + 256] & (1 << 6)) != 0);
        IGainInv.UpdateData((data[offset + 256] & (1 << 7)) != 0);
        DGainInv.UpdateData((data[offset + 257] & (1 << 0)) != 0);
        ZCDeadbandInv.UpdateData((data[offset + 257] & (1 << 1)) != 0);
        PVDeadbandInv.UpdateData((data[offset + 257] & (1 << 2)) != 0);
        PVROCLimitsInv.UpdateData((data[offset + 257] & (1 << 3)) != 0);
        DevHLLimitsInv.UpdateData((data[offset + 257] & (1 << 4)) != 0);
        DevDeadbandInv.UpdateData((data[offset + 257] & (1 << 5)) != 0);
        AtuneDataInv.UpdateData((data[offset + 257] & (1 << 6)) != 0);
        TimingModeInv.UpdateData((data[offset + 257] & (1 << 7)) != 0);
        RTSMissed.UpdateData((data[offset + 258] & (1 << 0)) != 0);
        RTSTimeInv.UpdateData((data[offset + 258] & (1 << 1)) != 0);
        RTSTimeStampInv.UpdateData((data[offset + 258] & (1 << 2)) != 0);
        DeltaTInv.UpdateData((data[offset + 258] & (1 << 3)) != 0);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PV</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public REAL PV
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PVFault</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL PVFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PVEUMax</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public REAL PVEUMax
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PVEUMin</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public REAL PVEUMin
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SPProg</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public REAL SPProg
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SPOper</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public REAL SPOper
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SPCascade</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public REAL SPCascade
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SPHLimit</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public REAL SPHLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SPLLimit</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public REAL SPLLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>UseRatio</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL UseRatio
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RatioProg</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public REAL RatioProg
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RatioOper</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public REAL RatioOper
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RatioHLimit</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public REAL RatioHLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RatioLLimit</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public REAL RatioLLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVFault</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL CVFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVInitReq</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL CVInitReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVInitValue</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public REAL CVInitValue
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVProg</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public REAL CVProg
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVOper</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public REAL CVOper
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVOverride</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public REAL CVOverride
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVPrevious</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public REAL CVPrevious
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVSetPrevious</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL CVSetPrevious
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVManLimiting</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL CVManLimiting
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVEUMax</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public REAL CVEUMax
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVEUMin</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public REAL CVEUMin
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVHLimit</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public REAL CVHLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVLLimit</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public REAL CVLLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVROCLimit</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public REAL CVROCLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FF</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public REAL FF
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FFPrevious</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public REAL FFPrevious
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FFSetPrevious</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL FFSetPrevious
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HandFB</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public REAL HandFB
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HandFBFault</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL HandFBFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>WindupHIn</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL WindupHIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>WindupLIn</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL WindupLIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ControlAction</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL ControlAction
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DependIndepend</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL DependIndepend
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PGain</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public REAL PGain
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>IGain</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public REAL IGain
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DGain</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public REAL DGain
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PVEProportional</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL PVEProportional
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PVEDerivative</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL PVEDerivative
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DSmoothing</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL DSmoothing
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PVTracking</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL PVTracking
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ZCDeadband</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public REAL ZCDeadband
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ZCOff</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL ZCOff
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PVHHLimit</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public REAL PVHHLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PVHLimit</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public REAL PVHLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PVLLimit</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public REAL PVLLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PVLLLimit</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public REAL PVLLLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PVDeadband</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public REAL PVDeadband
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PVROCPosLimit</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public REAL PVROCPosLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PVROCNegLimit</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public REAL PVROCNegLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PVROCPeriod</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public REAL PVROCPeriod
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DevHHLimit</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public REAL DevHHLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DevHLimit</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public REAL DevHLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DevLLimit</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public REAL DevLLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DevLLLimit</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public REAL DevLLLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DevDeadband</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public REAL DevDeadband
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AllowCasRat</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL AllowCasRat
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ManualAfterInit</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL ManualAfterInit
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgProgReq</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL ProgProgReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgOperReq</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL ProgOperReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgCasRatReq</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL ProgCasRatReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgAutoReq</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL ProgAutoReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgManualReq</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL ProgManualReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgOverrideReq</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL ProgOverrideReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgHandReq</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL ProgHandReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OperProgReq</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL OperProgReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OperOperReq</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL OperOperReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OperCasRatReq</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL OperCasRatReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OperAutoReq</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL OperAutoReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OperManualReq</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL OperManualReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgValueReset</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL ProgValueReset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TimingMode</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public DINT TimingMode
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OversampleDT</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public REAL OversampleDT
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RTSTime</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public DINT RTSTime
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RTSTimeStamp</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public DINT RTSTimeStamp
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneAcquire</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL AtuneAcquire
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneStart</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL AtuneStart
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneUseGains</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL AtuneUseGains
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneAbort</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL AtuneAbort
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneUnacquire</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL AtuneUnacquire
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVEU</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public REAL CVEU
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public REAL CV
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVInitializing</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL CVInitializing
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVHAlarm</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL CVHAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVLAlarm</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL CVLAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVROCAlarm</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL CVROCAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SP</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public REAL SP
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SPPercent</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public REAL SPPercent
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SPHAlarm</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL SPHAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SPLAlarm</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL SPLAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PVPercent</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public REAL PVPercent
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>E</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public REAL E
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EPercent</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public REAL EPercent
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InitPrimary</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL InitPrimary
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>WindupHOut</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL WindupHOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>WindupLOut</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL WindupLOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Ratio</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public REAL Ratio
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RatioHAlarm</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL RatioHAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RatioLAlarm</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL RatioLAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ZCDeadbandOn</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL ZCDeadbandOn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PVHHAlarm</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL PVHHAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PVHAlarm</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL PVHAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PVLAlarm</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL PVLAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PVLLAlarm</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL PVLLAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PVROCPosAlarm</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL PVROCPosAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PVROCNegAlarm</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL PVROCNegAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DevHHAlarm</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL DevHHAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DevHAlarm</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL DevHAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DevLAlarm</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL DevLAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DevLLAlarm</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL DevLLAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgOper</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL ProgOper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CasRat</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL CasRat
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Auto</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL Auto
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Manual</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL Manual
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Override</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL Override
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Hand</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL Hand
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DeltaT</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public REAL DeltaT
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneReady</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL AtuneReady
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneOn</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL AtuneOn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneDone</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL AtuneDone
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneAborted</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL AtuneAborted
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneBusy</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL AtuneBusy
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Status1</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public DINT Status1
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Status2</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public DINT Status2
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InstructFault</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL InstructFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PVFaulted</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL PVFaulted
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVFaulted</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL CVFaulted
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HandFBFaulted</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL HandFBFaulted
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PVSpanInv</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL PVSpanInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SPProgInv</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL SPProgInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SPOperInv</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL SPOperInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SPCascadeInv</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL SPCascadeInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SPLimitsInv</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL SPLimitsInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RatioProgInv</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL RatioProgInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RatioOperInv</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL RatioOperInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RatioLimitsInv</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL RatioLimitsInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVProgInv</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL CVProgInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVOperInv</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL CVOperInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVOverrideInv</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL CVOverrideInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVPreviousInv</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL CVPreviousInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVEUSpanInv</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL CVEUSpanInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVLimitsInv</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL CVLimitsInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVROCLimitInv</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL CVROCLimitInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FFInv</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL FFInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FFPreviousInv</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL FFPreviousInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HandFBInv</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL HandFBInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PGainInv</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL PGainInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>IGainInv</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL IGainInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DGainInv</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL DGainInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ZCDeadbandInv</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL ZCDeadbandInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PVDeadbandInv</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL PVDeadbandInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PVROCLimitsInv</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL PVROCLimitsInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DevHLLimitsInv</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL DevHLLimitsInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DevDeadbandInv</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL DevDeadbandInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneDataInv</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL AtuneDataInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TimingModeInv</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL TimingModeInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RTSMissed</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL RTSMissed
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RTSTimeInv</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL RTSTimeInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RTSTimeStampInv</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL RTSTimeStampInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DeltaTInv</c> member of the <see cref="PID_ENHANCED"/> data type.
    /// </summary>
    public BOOL DeltaTInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}