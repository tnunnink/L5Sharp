using System.Xml.Linq;
// Auto-generated file
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>CC</c> data type structure.
/// </summary>
[LogixData("CC")]
public sealed partial class CC : StructureData
{
    /// <summary>
    /// Creates a new <see cref="CC"/> instance initialized with default values.
    /// </summary>
    public CC() : base("CC")
    {
        EnableIn = new BOOL();
        PV = new REAL();
        PVFault = new BOOL();
        PVEUMax = new REAL();
        PVEUMin = new REAL();
        SPProg = new REAL();
        SPOper = new REAL();
        SPHLimit = new REAL();
        SPLLimit = new REAL();
        CV1Fault = new BOOL();
        CV2Fault = new BOOL();
        CV3Fault = new BOOL();
        CV1InitReq = new BOOL();
        CV2InitReq = new BOOL();
        CV3InitReq = new BOOL();
        CV1InitValue = new REAL();
        CV2InitValue = new REAL();
        CV3InitValue = new REAL();
        CV1Prog = new REAL();
        CV2Prog = new REAL();
        CV3Prog = new REAL();
        CV1Oper = new REAL();
        CV2Oper = new REAL();
        CV3Oper = new REAL();
        CV1OverrideValue = new REAL();
        CV2OverrideValue = new REAL();
        CV3OverrideValue = new REAL();
        CV1TrackValue = new REAL();
        CV2TrackValue = new REAL();
        CV3TrackValue = new REAL();
        CVManLimiting = new BOOL();
        CV1EUMax = new REAL();
        CV2EUMax = new REAL();
        CV3EUMax = new REAL();
        CV1EUMin = new REAL();
        CV2EUMin = new REAL();
        CV3EUMin = new REAL();
        CV1HLimit = new REAL();
        CV2HLimit = new REAL();
        CV3HLimit = new REAL();
        CV1LLimit = new REAL();
        CV2LLimit = new REAL();
        CV3LLimit = new REAL();
        CV1ROCPosLimit = new REAL();
        CV2ROCPosLimit = new REAL();
        CV3ROCPosLimit = new REAL();
        CV1ROCNegLimit = new REAL();
        CV2ROCNegLimit = new REAL();
        CV3ROCNegLimit = new REAL();
        CV1HandFB = new REAL();
        CV2HandFB = new REAL();
        CV3HandFB = new REAL();
        CV1HandFBFault = new BOOL();
        CV2HandFBFault = new BOOL();
        CV3HandFBFault = new BOOL();
        CV1Target = new REAL();
        CV2Target = new REAL();
        CV3Target = new REAL();
        CV1WindupHIn = new BOOL();
        CV2WindupHIn = new BOOL();
        CV3WindupHIn = new BOOL();
        CV1WindupLIn = new BOOL();
        CV2WindupLIn = new BOOL();
        CV3WindupLIn = new BOOL();
        GainEUSpan = new BOOL();
        CV1ProcessGainSign = new BOOL();
        CV2ProcessGainSign = new BOOL();
        CV3ProcessGainSign = new BOOL();
        ProcessType = new DINT();
        CV1ModelGain = new REAL();
        CV2ModelGain = new REAL();
        CV3ModelGain = new REAL();
        CV1ModelTC = new REAL();
        CV2ModelTC = new REAL();
        CV3ModelTC = new REAL();
        CV1ModelDT = new REAL();
        CV2ModelDT = new REAL();
        CV3ModelDT = new REAL();
        CV1RespTC = new REAL();
        CV2RespTC = new REAL();
        CV3RespTC = new REAL();
        Act1stCV = new DINT();
        Act2ndCV = new DINT();
        Act3rdCV = new DINT();
        Target1stCV = new DINT();
        Target2ndCV = new DINT();
        Target3rdCV = new DINT();
        TargetRespTC = new REAL();
        PVTracking = new BOOL();
        CVTrackReq = new BOOL();
        ManualAfterInit = new BOOL();
        ProgProgReq = new BOOL();
        ProgOperReq = new BOOL();
        ProgCV1AutoReq = new BOOL();
        ProgCV2AutoReq = new BOOL();
        ProgCV3AutoReq = new BOOL();
        ProgCV1ManualReq = new BOOL();
        ProgCV2ManualReq = new BOOL();
        ProgCV3ManualReq = new BOOL();
        ProgCV1OverrideReq = new BOOL();
        ProgCV2OverrideReq = new BOOL();
        ProgCV3OverrideReq = new BOOL();
        ProgCV1HandReq = new BOOL();
        ProgCV2HandReq = new BOOL();
        ProgCV3HandReq = new BOOL();
        OperProgReq = new BOOL();
        OperOperReq = new BOOL();
        OperCV1AutoReq = new BOOL();
        OperCV2AutoReq = new BOOL();
        OperCV3AutoReq = new BOOL();
        OperCV1ManualReq = new BOOL();
        OperCV2ManualReq = new BOOL();
        OperCV3ManualReq = new BOOL();
        ProgValueReset = new BOOL();
        TimingMode = new DINT();
        OversampleDT = new REAL();
        RTSTime = new DINT();
        RTSTimeStamp = new DINT();
        PVTuneLimit = new REAL();
        AtuneTimeLimit = new REAL();
        NoiseLevel = new DINT();
        CV1StepSize = new REAL();
        CV2StepSize = new REAL();
        CV3StepSize = new REAL();
        CV1ResponseSpeed = new DINT();
        CV2ResponseSpeed = new DINT();
        CV3ResponseSpeed = new DINT();
        CV1ModelInit = new BOOL();
        CV2ModelInit = new BOOL();
        CV3ModelInit = new BOOL();
        Factor = new REAL();
        AtuneCV1Start = new BOOL();
        AtuneCV2Start = new BOOL();
        AtuneCV3Start = new BOOL();
        AtuneCV1UseModel = new BOOL();
        AtuneCV2UseModel = new BOOL();
        AtuneCV3UseModel = new BOOL();
        AtuneCV1Abort = new BOOL();
        AtuneCV2Abort = new BOOL();
        AtuneCV3Abort = new BOOL();
        EnableOut = new BOOL();
        CV1EU = new REAL();
        CV2EU = new REAL();
        CV3EU = new REAL();
        CV1 = new REAL();
        CV2 = new REAL();
        CV3 = new REAL();
        DeltaCV1 = new REAL();
        DeltaCV2 = new REAL();
        DeltaCV3 = new REAL();
        CV1Initializing = new BOOL();
        CV2Initializing = new BOOL();
        CV3Initializing = new BOOL();
        CV1HAlarm = new BOOL();
        CV2HAlarm = new BOOL();
        CV3HAlarm = new BOOL();
        CV1LAlarm = new BOOL();
        CV2LAlarm = new BOOL();
        CV3LAlarm = new BOOL();
        CV1ROCPosAlarm = new BOOL();
        CV2ROCPosAlarm = new BOOL();
        CV3ROCPosAlarm = new BOOL();
        CV1ROCNegAlarm = new BOOL();
        CV2ROCNegAlarm = new BOOL();
        CV3ROCNegAlarm = new BOOL();
        SP = new REAL();
        SPPercent = new REAL();
        SPHAlarm = new BOOL();
        SPLAlarm = new BOOL();
        PVPercent = new REAL();
        E = new REAL();
        EPercent = new REAL();
        CV1WindupHOut = new BOOL();
        CV2WindupHOut = new BOOL();
        CV3WindupHOut = new BOOL();
        CV1WindupLOut = new BOOL();
        CV2WindupLOut = new BOOL();
        CV3WindupLOut = new BOOL();
        ProgOper = new BOOL();
        CV1Auto = new BOOL();
        CV2Auto = new BOOL();
        CV3Auto = new BOOL();
        CV1Manual = new BOOL();
        CV2Manual = new BOOL();
        CV3Manual = new BOOL();
        CV1Override = new BOOL();
        CV2Override = new BOOL();
        CV3Override = new BOOL();
        CV1Hand = new BOOL();
        CV2Hand = new BOOL();
        CV3Hand = new BOOL();
        DeltaT = new REAL();
        CV1StepSizeUsed = new REAL();
        CV2StepSizeUsed = new REAL();
        CV3StepSizeUsed = new REAL();
        CV1GainTuned = new REAL();
        CV2GainTuned = new REAL();
        CV3GainTuned = new REAL();
        CV1TCTuned = new REAL();
        CV2TCTuned = new REAL();
        CV3TCTuned = new REAL();
        CV1DTTuned = new REAL();
        CV2DTTuned = new REAL();
        CV3DTTuned = new REAL();
        CV1RespTCTunedS = new REAL();
        CV2RespTCTunedS = new REAL();
        CV3RespTCTunedS = new REAL();
        CV1RespTCTunedM = new REAL();
        CV2RespTCTunedM = new REAL();
        CV3RespTCTunedM = new REAL();
        CV1RespTCTunedF = new REAL();
        CV2RespTCTunedF = new REAL();
        CV3RespTCTunedF = new REAL();
        AtuneCV1On = new BOOL();
        AtuneCV2On = new BOOL();
        AtuneCV3On = new BOOL();
        AtuneCV1Done = new BOOL();
        AtuneCV2Done = new BOOL();
        AtuneCV3Done = new BOOL();
        AtuneCV1Aborted = new BOOL();
        AtuneCV2Aborted = new BOOL();
        AtuneCV3Aborted = new BOOL();
        AtuneCV1Status = new DINT();
        AtuneCV2Status = new DINT();
        AtuneCV3Status = new DINT();
        AtuneCV1Fault = new BOOL();
        AtuneCV1PVOutOfLimit = new BOOL();
        AtuneCV1ModeInv = new BOOL();
        AtuneCV1WindupFault = new BOOL();
        AtuneCV1StepSize0 = new BOOL();
        AtuneCV1LimitsFault = new BOOL();
        AtuneCV1InitFault = new BOOL();
        AtuneCV1EUSpanChanged = new BOOL();
        AtuneCV1Changed = new BOOL();
        AtuneCV1Timeout = new BOOL();
        AtuneCV1PVNotSettled = new BOOL();
        AtuneCV2Fault = new BOOL();
        AtuneCV2PVOutOfLimit = new BOOL();
        AtuneCV2ModeInv = new BOOL();
        AtuneCV2WindupFault = new BOOL();
        AtuneCV2StepSize0 = new BOOL();
        AtuneCV2LimitsFault = new BOOL();
        AtuneCV2InitFault = new BOOL();
        AtuneCV2EUSpanChanged = new BOOL();
        AtuneCV2Changed = new BOOL();
        AtuneCV2Timeout = new BOOL();
        AtuneCV2PVNotSettled = new BOOL();
        AtuneCV3Fault = new BOOL();
        AtuneCV3PVOutOfLimit = new BOOL();
        AtuneCV3ModeInv = new BOOL();
        AtuneCV3WindupFault = new BOOL();
        AtuneCV3StepSize0 = new BOOL();
        AtuneCV3LimitsFault = new BOOL();
        AtuneCV3InitFault = new BOOL();
        AtuneCV3EUSpanChanged = new BOOL();
        AtuneCV3Changed = new BOOL();
        AtuneCV3Timeout = new BOOL();
        AtuneCV3PVNotSettled = new BOOL();
        Status1 = new DINT();
        Status2 = new DINT();
        Status3CV1 = new DINT();
        Status3CV2 = new DINT();
        Status3CV3 = new DINT();
        InstructFault = new BOOL();
        PVFaulted = new BOOL();
        PVSpanInv = new BOOL();
        SPProgInv = new BOOL();
        SPOperInv = new BOOL();
        SPLimitsInv = new BOOL();
        SampleTimeTooSmall = new BOOL();
        FactorInv = new BOOL();
        TimingModeInv = new BOOL();
        RTSMissed = new BOOL();
        RTSTimeInv = new BOOL();
        RTSTimeStampInv = new BOOL();
        DeltaTInv = new BOOL();
        CV1Faulted = new BOOL();
        CV1HandFBFaulted = new BOOL();
        CV1ProgInv = new BOOL();
        CV1OperInv = new BOOL();
        CV1OverrideValueInv = new BOOL();
        CV1TrackValueInv = new BOOL();
        CV1EUSpanInv = new BOOL();
        CV1LimitsInv = new BOOL();
        CV1ROCLimitInv = new BOOL();
        CV1HandFBInv = new BOOL();
        CV1ModelGainInv = new BOOL();
        CV1ModelTCInv = new BOOL();
        CV1ModelDTInv = new BOOL();
        CV1RespTCInv = new BOOL();
        CV1TargetInv = new BOOL();
        CV2Faulted = new BOOL();
        CV2HandFBFaulted = new BOOL();
        CV2ProgInv = new BOOL();
        CV2OperInv = new BOOL();
        CV2OverrideValueInv = new BOOL();
        CV2TrackValueInv = new BOOL();
        CV2EUSpanInv = new BOOL();
        CV2LimitsInv = new BOOL();
        CV2ROCLimitInv = new BOOL();
        CV2HandFBInv = new BOOL();
        CV2ModelGainInv = new BOOL();
        CV2ModelTCInv = new BOOL();
        CV2ModelDTInv = new BOOL();
        CV2RespTCInv = new BOOL();
        CV2TargetInv = new BOOL();
        CV3Faulted = new BOOL();
        CV3HandFBFaulted = new BOOL();
        CV3ProgInv = new BOOL();
        CV3OperInv = new BOOL();
        CV3OverrideValueInv = new BOOL();
        CV3TrackValueInv = new BOOL();
        CV3EUSpanInv = new BOOL();
        CV3LimitsInv = new BOOL();
        CV3ROCLimitInv = new BOOL();
        CV3HandFBInv = new BOOL();
        CV3ModelGainInv = new BOOL();
        CV3ModelTCInv = new BOOL();
        CV3ModelDTInv = new BOOL();
        CV3RespTCInv = new BOOL();
        CV3TargetInv = new BOOL();
    }

    /// <summary>
    /// Creates a new <see cref="CC"/> instance initialized with the provided element.
    /// </summary>
    public CC(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PV</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL PV
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PVFault</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL PVFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PVEUMax</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL PVEUMax
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PVEUMin</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL PVEUMin
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SPProg</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL SPProg
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SPOper</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL SPOper
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SPHLimit</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL SPHLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SPLLimit</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL SPLLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1Fault</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV1Fault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2Fault</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV2Fault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3Fault</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV3Fault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1InitReq</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV1InitReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2InitReq</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV2InitReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3InitReq</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV3InitReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1InitValue</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV1InitValue
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2InitValue</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV2InitValue
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3InitValue</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV3InitValue
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1Prog</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV1Prog
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2Prog</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV2Prog
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3Prog</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV3Prog
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1Oper</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV1Oper
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2Oper</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV2Oper
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3Oper</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV3Oper
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1OverrideValue</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV1OverrideValue
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2OverrideValue</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV2OverrideValue
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3OverrideValue</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV3OverrideValue
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1TrackValue</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV1TrackValue
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2TrackValue</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV2TrackValue
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3TrackValue</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV3TrackValue
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVManLimiting</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CVManLimiting
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1EUMax</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV1EUMax
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2EUMax</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV2EUMax
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3EUMax</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV3EUMax
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1EUMin</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV1EUMin
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2EUMin</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV2EUMin
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3EUMin</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV3EUMin
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1HLimit</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV1HLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2HLimit</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV2HLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3HLimit</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV3HLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1LLimit</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV1LLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2LLimit</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV2LLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3LLimit</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV3LLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1ROCPosLimit</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV1ROCPosLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2ROCPosLimit</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV2ROCPosLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3ROCPosLimit</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV3ROCPosLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1ROCNegLimit</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV1ROCNegLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2ROCNegLimit</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV2ROCNegLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3ROCNegLimit</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV3ROCNegLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1HandFB</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV1HandFB
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2HandFB</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV2HandFB
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3HandFB</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV3HandFB
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1HandFBFault</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV1HandFBFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2HandFBFault</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV2HandFBFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3HandFBFault</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV3HandFBFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1Target</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV1Target
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2Target</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV2Target
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3Target</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV3Target
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1WindupHIn</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV1WindupHIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2WindupHIn</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV2WindupHIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3WindupHIn</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV3WindupHIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1WindupLIn</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV1WindupLIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2WindupLIn</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV2WindupLIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3WindupLIn</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV3WindupLIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>GainEUSpan</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL GainEUSpan
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1ProcessGainSign</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV1ProcessGainSign
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2ProcessGainSign</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV2ProcessGainSign
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3ProcessGainSign</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV3ProcessGainSign
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProcessType</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public DINT ProcessType
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1ModelGain</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV1ModelGain
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2ModelGain</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV2ModelGain
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3ModelGain</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV3ModelGain
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1ModelTC</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV1ModelTC
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2ModelTC</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV2ModelTC
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3ModelTC</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV3ModelTC
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1ModelDT</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV1ModelDT
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2ModelDT</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV2ModelDT
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3ModelDT</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV3ModelDT
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1RespTC</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV1RespTC
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2RespTC</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV2RespTC
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3RespTC</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV3RespTC
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Act1stCV</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public DINT Act1stCV
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Act2ndCV</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public DINT Act2ndCV
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Act3rdCV</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public DINT Act3rdCV
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Target1stCV</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public DINT Target1stCV
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Target2ndCV</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public DINT Target2ndCV
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Target3rdCV</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public DINT Target3rdCV
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TargetRespTC</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL TargetRespTC
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PVTracking</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL PVTracking
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVTrackReq</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CVTrackReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ManualAfterInit</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL ManualAfterInit
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgProgReq</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL ProgProgReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgOperReq</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL ProgOperReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgCV1AutoReq</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL ProgCV1AutoReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgCV2AutoReq</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL ProgCV2AutoReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgCV3AutoReq</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL ProgCV3AutoReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgCV1ManualReq</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL ProgCV1ManualReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgCV2ManualReq</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL ProgCV2ManualReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgCV3ManualReq</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL ProgCV3ManualReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgCV1OverrideReq</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL ProgCV1OverrideReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgCV2OverrideReq</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL ProgCV2OverrideReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgCV3OverrideReq</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL ProgCV3OverrideReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgCV1HandReq</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL ProgCV1HandReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgCV2HandReq</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL ProgCV2HandReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgCV3HandReq</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL ProgCV3HandReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OperProgReq</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL OperProgReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OperOperReq</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL OperOperReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OperCV1AutoReq</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL OperCV1AutoReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OperCV2AutoReq</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL OperCV2AutoReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OperCV3AutoReq</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL OperCV3AutoReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OperCV1ManualReq</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL OperCV1ManualReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OperCV2ManualReq</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL OperCV2ManualReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OperCV3ManualReq</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL OperCV3ManualReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgValueReset</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL ProgValueReset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TimingMode</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public DINT TimingMode
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OversampleDT</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL OversampleDT
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RTSTime</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public DINT RTSTime
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RTSTimeStamp</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public DINT RTSTimeStamp
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PVTuneLimit</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL PVTuneLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneTimeLimit</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL AtuneTimeLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>NoiseLevel</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public DINT NoiseLevel
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1StepSize</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV1StepSize
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2StepSize</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV2StepSize
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3StepSize</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV3StepSize
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1ResponseSpeed</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public DINT CV1ResponseSpeed
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2ResponseSpeed</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public DINT CV2ResponseSpeed
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3ResponseSpeed</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public DINT CV3ResponseSpeed
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1ModelInit</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV1ModelInit
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2ModelInit</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV2ModelInit
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3ModelInit</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV3ModelInit
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Factor</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL Factor
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV1Start</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL AtuneCV1Start
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV2Start</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL AtuneCV2Start
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV3Start</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL AtuneCV3Start
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV1UseModel</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL AtuneCV1UseModel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV2UseModel</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL AtuneCV2UseModel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV3UseModel</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL AtuneCV3UseModel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV1Abort</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL AtuneCV1Abort
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV2Abort</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL AtuneCV2Abort
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV3Abort</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL AtuneCV3Abort
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1EU</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV1EU
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2EU</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV2EU
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3EU</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV3EU
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV1
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV2
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV3
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DeltaCV1</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL DeltaCV1
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DeltaCV2</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL DeltaCV2
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DeltaCV3</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL DeltaCV3
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1Initializing</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV1Initializing
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2Initializing</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV2Initializing
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3Initializing</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV3Initializing
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1HAlarm</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV1HAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2HAlarm</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV2HAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3HAlarm</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV3HAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1LAlarm</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV1LAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2LAlarm</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV2LAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3LAlarm</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV3LAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1ROCPosAlarm</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV1ROCPosAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2ROCPosAlarm</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV2ROCPosAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3ROCPosAlarm</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV3ROCPosAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1ROCNegAlarm</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV1ROCNegAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2ROCNegAlarm</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV2ROCNegAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3ROCNegAlarm</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV3ROCNegAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SP</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL SP
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SPPercent</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL SPPercent
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SPHAlarm</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL SPHAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SPLAlarm</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL SPLAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PVPercent</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL PVPercent
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>E</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL E
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EPercent</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL EPercent
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1WindupHOut</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV1WindupHOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2WindupHOut</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV2WindupHOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3WindupHOut</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV3WindupHOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1WindupLOut</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV1WindupLOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2WindupLOut</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV2WindupLOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3WindupLOut</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV3WindupLOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgOper</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL ProgOper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1Auto</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV1Auto
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2Auto</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV2Auto
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3Auto</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV3Auto
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1Manual</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV1Manual
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2Manual</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV2Manual
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3Manual</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV3Manual
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1Override</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV1Override
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2Override</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV2Override
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3Override</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV3Override
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1Hand</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV1Hand
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2Hand</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV2Hand
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3Hand</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV3Hand
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DeltaT</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL DeltaT
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1StepSizeUsed</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV1StepSizeUsed
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2StepSizeUsed</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV2StepSizeUsed
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3StepSizeUsed</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV3StepSizeUsed
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1GainTuned</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV1GainTuned
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2GainTuned</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV2GainTuned
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3GainTuned</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV3GainTuned
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1TCTuned</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV1TCTuned
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2TCTuned</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV2TCTuned
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3TCTuned</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV3TCTuned
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1DTTuned</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV1DTTuned
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2DTTuned</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV2DTTuned
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3DTTuned</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV3DTTuned
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1RespTCTunedS</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV1RespTCTunedS
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2RespTCTunedS</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV2RespTCTunedS
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3RespTCTunedS</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV3RespTCTunedS
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1RespTCTunedM</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV1RespTCTunedM
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2RespTCTunedM</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV2RespTCTunedM
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3RespTCTunedM</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV3RespTCTunedM
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1RespTCTunedF</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV1RespTCTunedF
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2RespTCTunedF</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV2RespTCTunedF
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3RespTCTunedF</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public REAL CV3RespTCTunedF
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV1On</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL AtuneCV1On
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV2On</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL AtuneCV2On
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV3On</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL AtuneCV3On
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV1Done</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL AtuneCV1Done
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV2Done</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL AtuneCV2Done
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV3Done</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL AtuneCV3Done
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV1Aborted</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL AtuneCV1Aborted
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV2Aborted</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL AtuneCV2Aborted
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV3Aborted</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL AtuneCV3Aborted
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV1Status</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public DINT AtuneCV1Status
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV2Status</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public DINT AtuneCV2Status
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV3Status</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public DINT AtuneCV3Status
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV1Fault</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL AtuneCV1Fault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV1PVOutOfLimit</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL AtuneCV1PVOutOfLimit
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV1ModeInv</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL AtuneCV1ModeInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV1WindupFault</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL AtuneCV1WindupFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV1StepSize0</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL AtuneCV1StepSize0
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV1LimitsFault</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL AtuneCV1LimitsFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV1InitFault</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL AtuneCV1InitFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV1EUSpanChanged</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL AtuneCV1EUSpanChanged
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV1Changed</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL AtuneCV1Changed
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV1Timeout</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL AtuneCV1Timeout
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV1PVNotSettled</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL AtuneCV1PVNotSettled
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV2Fault</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL AtuneCV2Fault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV2PVOutOfLimit</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL AtuneCV2PVOutOfLimit
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV2ModeInv</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL AtuneCV2ModeInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV2WindupFault</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL AtuneCV2WindupFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV2StepSize0</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL AtuneCV2StepSize0
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV2LimitsFault</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL AtuneCV2LimitsFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV2InitFault</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL AtuneCV2InitFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV2EUSpanChanged</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL AtuneCV2EUSpanChanged
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV2Changed</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL AtuneCV2Changed
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV2Timeout</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL AtuneCV2Timeout
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV2PVNotSettled</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL AtuneCV2PVNotSettled
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV3Fault</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL AtuneCV3Fault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV3PVOutOfLimit</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL AtuneCV3PVOutOfLimit
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV3ModeInv</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL AtuneCV3ModeInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV3WindupFault</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL AtuneCV3WindupFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV3StepSize0</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL AtuneCV3StepSize0
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV3LimitsFault</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL AtuneCV3LimitsFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV3InitFault</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL AtuneCV3InitFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV3EUSpanChanged</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL AtuneCV3EUSpanChanged
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV3Changed</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL AtuneCV3Changed
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV3Timeout</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL AtuneCV3Timeout
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV3PVNotSettled</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL AtuneCV3PVNotSettled
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Status1</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public DINT Status1
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Status2</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public DINT Status2
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Status3CV1</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public DINT Status3CV1
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Status3CV2</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public DINT Status3CV2
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Status3CV3</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public DINT Status3CV3
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InstructFault</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL InstructFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PVFaulted</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL PVFaulted
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PVSpanInv</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL PVSpanInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SPProgInv</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL SPProgInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SPOperInv</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL SPOperInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SPLimitsInv</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL SPLimitsInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SampleTimeTooSmall</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL SampleTimeTooSmall
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FactorInv</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL FactorInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TimingModeInv</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL TimingModeInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RTSMissed</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL RTSMissed
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RTSTimeInv</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL RTSTimeInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RTSTimeStampInv</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL RTSTimeStampInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DeltaTInv</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL DeltaTInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1Faulted</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV1Faulted
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1HandFBFaulted</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV1HandFBFaulted
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1ProgInv</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV1ProgInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1OperInv</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV1OperInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1OverrideValueInv</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV1OverrideValueInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1TrackValueInv</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV1TrackValueInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1EUSpanInv</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV1EUSpanInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1LimitsInv</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV1LimitsInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1ROCLimitInv</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV1ROCLimitInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1HandFBInv</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV1HandFBInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1ModelGainInv</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV1ModelGainInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1ModelTCInv</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV1ModelTCInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1ModelDTInv</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV1ModelDTInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1RespTCInv</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV1RespTCInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1TargetInv</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV1TargetInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2Faulted</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV2Faulted
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2HandFBFaulted</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV2HandFBFaulted
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2ProgInv</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV2ProgInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2OperInv</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV2OperInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2OverrideValueInv</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV2OverrideValueInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2TrackValueInv</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV2TrackValueInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2EUSpanInv</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV2EUSpanInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2LimitsInv</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV2LimitsInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2ROCLimitInv</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV2ROCLimitInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2HandFBInv</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV2HandFBInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2ModelGainInv</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV2ModelGainInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2ModelTCInv</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV2ModelTCInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2ModelDTInv</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV2ModelDTInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2RespTCInv</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV2RespTCInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2TargetInv</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV2TargetInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3Faulted</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV3Faulted
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3HandFBFaulted</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV3HandFBFaulted
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3ProgInv</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV3ProgInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3OperInv</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV3OperInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3OverrideValueInv</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV3OverrideValueInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3TrackValueInv</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV3TrackValueInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3EUSpanInv</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV3EUSpanInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3LimitsInv</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV3LimitsInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3ROCLimitInv</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV3ROCLimitInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3HandFBInv</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV3HandFBInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3ModelGainInv</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV3ModelGainInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3ModelTCInv</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV3ModelTCInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3ModelDTInv</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV3ModelDTInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3RespTCInv</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV3RespTCInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3TargetInv</c> member of the <see cref="CC"/> data type.
    /// </summary>
    public BOOL CV3TargetInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

}
