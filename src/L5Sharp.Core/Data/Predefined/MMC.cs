using System.Xml.Linq;
// Auto-generated file
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>MMC</c> data type structure.
/// </summary>
[LogixData("MMC")]
public sealed partial class MMC : StructureData
{
    /// <summary>
    /// Creates a new <see cref="MMC"/> instance initialized with default values.
    /// </summary>
    public MMC() : base("MMC")
    {
        EnableIn = new BOOL();
        PV1 = new REAL();
        PV2 = new REAL();
        PV1Fault = new BOOL();
        PV2Fault = new BOOL();
        PV1EUMax = new REAL();
        PV2EUMax = new REAL();
        PV1EUMin = new REAL();
        PV2EUMin = new REAL();
        SP1Prog = new REAL();
        SP2Prog = new REAL();
        SP1Oper = new REAL();
        SP2Oper = new REAL();
        SP1HLimit = new REAL();
        SP2HLimit = new REAL();
        SP1LLimit = new REAL();
        SP2LLimit = new REAL();
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
        CV1PV1ProcessGainSign = new BOOL();
        CV2PV1ProcessGainSign = new BOOL();
        CV3PV1ProcessGainSign = new BOOL();
        CV1PV2ProcessGainSign = new BOOL();
        CV2PV2ProcessGainSign = new BOOL();
        CV3PV2ProcessGainSign = new BOOL();
        ProcessType = new DINT();
        CV1PV1ModelGain = new REAL();
        CV2PV1ModelGain = new REAL();
        CV3PV1ModelGain = new REAL();
        CV1PV2ModelGain = new REAL();
        CV2PV2ModelGain = new REAL();
        CV3PV2ModelGain = new REAL();
        CV1PV1ModelTC = new REAL();
        CV2PV1ModelTC = new REAL();
        CV3PV1ModelTC = new REAL();
        CV1PV2ModelTC = new REAL();
        CV2PV2ModelTC = new REAL();
        CV3PV2ModelTC = new REAL();
        CV1PV1ModelDT = new REAL();
        CV2PV1ModelDT = new REAL();
        CV3PV1ModelDT = new REAL();
        CV1PV2ModelDT = new REAL();
        CV2PV2ModelDT = new REAL();
        CV3PV2ModelDT = new REAL();
        CV1PV1RespTC = new REAL();
        CV2PV1RespTC = new REAL();
        CV3PV1RespTC = new REAL();
        CV1PV2RespTC = new REAL();
        CV2PV2RespTC = new REAL();
        CV3PV2RespTC = new REAL();
        PV1Act1stCV = new DINT();
        PV1Act2ndCV = new DINT();
        PV1Act3rdCV = new DINT();
        PV2Act1stCV = new DINT();
        PV2Act2ndCV = new DINT();
        PV2Act3rdCV = new DINT();
        TargetCV = new DINT();
        TargetRespTC = new REAL();
        PVTracking = new BOOL();
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
        PV1TuneLimit = new REAL();
        PV2TuneLimit = new REAL();
        PV1AtuneTimeLimit = new REAL();
        PV2AtuneTimeLimit = new REAL();
        PV1NoiseLevel = new DINT();
        PV2NoiseLevel = new DINT();
        CV1StepSize = new REAL();
        CV2StepSize = new REAL();
        CV3StepSize = new REAL();
        CV1PV1ResponseSpeed = new DINT();
        CV2PV1ResponseSpeed = new DINT();
        CV3PV1ResponseSpeed = new DINT();
        CV1PV2ResponseSpeed = new DINT();
        CV2PV2ResponseSpeed = new DINT();
        CV3PV2ResponseSpeed = new DINT();
        CV1PV1ModelInit = new BOOL();
        CV2PV1ModelInit = new BOOL();
        CV3PV1ModelInit = new BOOL();
        CV1PV2ModelInit = new BOOL();
        CV2PV2ModelInit = new BOOL();
        CV3PV2ModelInit = new BOOL();
        PV1Factor = new REAL();
        PV2Factor = new REAL();
        AtuneCV1Start = new BOOL();
        AtuneCV2Start = new BOOL();
        AtuneCV3Start = new BOOL();
        AtuneCV1PV1UseModel = new BOOL();
        AtuneCV2PV1UseModel = new BOOL();
        AtuneCV3PV1UseModel = new BOOL();
        AtuneCV1PV2UseModel = new BOOL();
        AtuneCV2PV2UseModel = new BOOL();
        AtuneCV3PV2UseModel = new BOOL();
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
        SP1 = new REAL();
        SP2 = new REAL();
        SP1Percent = new REAL();
        SP2Percent = new REAL();
        SP1HAlarm = new BOOL();
        SP2HAlarm = new BOOL();
        SP1LAlarm = new BOOL();
        SP2LAlarm = new BOOL();
        PV1Percent = new REAL();
        PV2Percent = new REAL();
        E1 = new REAL();
        E2 = new REAL();
        E1Percent = new REAL();
        E2Percent = new REAL();
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
        CV1PV1GainTuned = new REAL();
        CV2PV1GainTuned = new REAL();
        CV3PV1GainTuned = new REAL();
        CV1PV2GainTuned = new REAL();
        CV2PV2GainTuned = new REAL();
        CV3PV2GainTuned = new REAL();
        CV1PV1TCTuned = new REAL();
        CV2PV1TCTuned = new REAL();
        CV3PV1TCTuned = new REAL();
        CV1PV2TCTuned = new REAL();
        CV2PV2TCTuned = new REAL();
        CV3PV2TCTuned = new REAL();
        CV1PV1DTTuned = new REAL();
        CV2PV1DTTuned = new REAL();
        CV3PV1DTTuned = new REAL();
        CV1PV2DTTuned = new REAL();
        CV2PV2DTTuned = new REAL();
        CV3PV2DTTuned = new REAL();
        CV1PV1RespTCTunedS = new REAL();
        CV2PV1RespTCTunedS = new REAL();
        CV3PV1RespTCTunedS = new REAL();
        CV1PV2RespTCTunedS = new REAL();
        CV2PV2RespTCTunedS = new REAL();
        CV3PV2RespTCTunedS = new REAL();
        CV1PV1RespTCTunedM = new REAL();
        CV2PV1RespTCTunedM = new REAL();
        CV3PV1RespTCTunedM = new REAL();
        CV1PV2RespTCTunedM = new REAL();
        CV2PV2RespTCTunedM = new REAL();
        CV3PV2RespTCTunedM = new REAL();
        CV1PV1RespTCTunedF = new REAL();
        CV2PV1RespTCTunedF = new REAL();
        CV3PV1RespTCTunedF = new REAL();
        CV1PV2RespTCTunedF = new REAL();
        CV2PV2RespTCTunedF = new REAL();
        CV3PV2RespTCTunedF = new REAL();
        AtuneCV1PV1On = new BOOL();
        AtuneCV2PV1On = new BOOL();
        AtuneCV3PV1On = new BOOL();
        AtuneCV1PV1Done = new BOOL();
        AtuneCV2PV1Done = new BOOL();
        AtuneCV3PV1Done = new BOOL();
        AtuneCV1PV1Aborted = new BOOL();
        AtuneCV2PV1Aborted = new BOOL();
        AtuneCV3PV1Aborted = new BOOL();
        AtuneCV1PV2On = new BOOL();
        AtuneCV2PV2On = new BOOL();
        AtuneCV3PV2On = new BOOL();
        AtuneCV1PV2Done = new BOOL();
        AtuneCV2PV2Done = new BOOL();
        AtuneCV3PV2Done = new BOOL();
        AtuneCV1PV2Aborted = new BOOL();
        AtuneCV2PV2Aborted = new BOOL();
        AtuneCV3PV2Aborted = new BOOL();
        AtuneCV1PV1Status = new DINT();
        AtuneCV2PV1Status = new DINT();
        AtuneCV3PV1Status = new DINT();
        AtuneCV1PV2Status = new DINT();
        AtuneCV2PV2Status = new DINT();
        AtuneCV3PV2Status = new DINT();
        AtuneCV1PV1Fault = new BOOL();
        AtuneCV1PV1OutOfLimit = new BOOL();
        AtuneCV1PV1ModeInv = new BOOL();
        AtuneCV1PV1WindupFault = new BOOL();
        AtuneCV1PV1StepSize0 = new BOOL();
        AtuneCV1PV1LimitsFault = new BOOL();
        AtuneCV1PV1InitFault = new BOOL();
        AtuneCV1PV1EUSpanChanged = new BOOL();
        AtuneCV1PV1Changed = new BOOL();
        AtuneCV1PV1Timeout = new BOOL();
        AtuneCV1PV1NotSettled = new BOOL();
        AtuneCV2PV1Fault = new BOOL();
        AtuneCV2PV1OutOfLimit = new BOOL();
        AtuneCV2PV1ModeInv = new BOOL();
        AtuneCV2PV1WindupFault = new BOOL();
        AtuneCV2PV1StepSize0 = new BOOL();
        AtuneCV2PV1LimitsFault = new BOOL();
        AtuneCV2PV1InitFault = new BOOL();
        AtuneCV2PV1EUSpanChanged = new BOOL();
        AtuneCV2PV1Changed = new BOOL();
        AtuneCV2PV1Timeout = new BOOL();
        AtuneCV2PV1NotSettled = new BOOL();
        AtuneCV3PV1Fault = new BOOL();
        AtuneCV3PV1OutOfLimit = new BOOL();
        AtuneCV3PV1ModeInv = new BOOL();
        AtuneCV3PV1WindupFault = new BOOL();
        AtuneCV3PV1StepSize0 = new BOOL();
        AtuneCV3PV1LimitsFault = new BOOL();
        AtuneCV3PV1InitFault = new BOOL();
        AtuneCV3PV1EUSpanChanged = new BOOL();
        AtuneCV3PV1Changed = new BOOL();
        AtuneCV3PV1Timeout = new BOOL();
        AtuneCV3PV1NotSettled = new BOOL();
        AtuneCV1PV2Fault = new BOOL();
        AtuneCV1PV2OutOfLimit = new BOOL();
        AtuneCV1PV2ModeInv = new BOOL();
        AtuneCV1PV2WindupFault = new BOOL();
        AtuneCV1PV2StepSize0 = new BOOL();
        AtuneCV1PV2LimitsFault = new BOOL();
        AtuneCV1PV2InitFault = new BOOL();
        AtuneCV1PV2EUSpanChanged = new BOOL();
        AtuneCV1PV2Changed = new BOOL();
        AtuneCV1PV2Timeout = new BOOL();
        AtuneCV1PV2NotSettled = new BOOL();
        AtuneCV2PV2Fault = new BOOL();
        AtuneCV2PV2OutOfLimit = new BOOL();
        AtuneCV2PV2ModeInv = new BOOL();
        AtuneCV2PV2WindupFault = new BOOL();
        AtuneCV2PV2StepSize0 = new BOOL();
        AtuneCV2PV2LimitsFault = new BOOL();
        AtuneCV2PV2InitFault = new BOOL();
        AtuneCV2PV2EUSpanChanged = new BOOL();
        AtuneCV2PV2Changed = new BOOL();
        AtuneCV2PV2Timeout = new BOOL();
        AtuneCV2PV2NotSettled = new BOOL();
        AtuneCV3PV2Fault = new BOOL();
        AtuneCV3PV2OutOfLimit = new BOOL();
        AtuneCV3PV2ModeInv = new BOOL();
        AtuneCV3PV2WindupFault = new BOOL();
        AtuneCV3PV2StepSize0 = new BOOL();
        AtuneCV3PV2LimitsFault = new BOOL();
        AtuneCV3PV2InitFault = new BOOL();
        AtuneCV3PV2EUSpanChanged = new BOOL();
        AtuneCV3PV2Changed = new BOOL();
        AtuneCV3PV2Timeout = new BOOL();
        AtuneCV3PV2NotSettled = new BOOL();
        Status1 = new DINT();
        Status2 = new DINT();
        Status3CV1 = new DINT();
        Status3CV2 = new DINT();
        Status3CV3 = new DINT();
        InstructFault = new BOOL();
        PV1Faulted = new BOOL();
        PV2Faulted = new BOOL();
        PV1SpanInv = new BOOL();
        PV2SpanInv = new BOOL();
        SP1ProgInv = new BOOL();
        SP2ProgInv = new BOOL();
        SP1OperInv = new BOOL();
        SP2OperInv = new BOOL();
        SP1LimitsInv = new BOOL();
        SP2LimitsInv = new BOOL();
        SampleTimeTooSmall = new BOOL();
        PV1FactorInv = new BOOL();
        PV2FactorInv = new BOOL();
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
        CV1EUSpanInv = new BOOL();
        CV1LimitsInv = new BOOL();
        CV1ROCLimitInv = new BOOL();
        CV1HandFBInv = new BOOL();
        CV1PV1ModelGainInv = new BOOL();
        CV1PV2ModelGainInv = new BOOL();
        CV1PV1ModelTCInv = new BOOL();
        CV1PV2ModelTCInv = new BOOL();
        CV1PV1ModelDTInv = new BOOL();
        CV1PV2ModelDTInv = new BOOL();
        CV1PV1RespTCInv = new BOOL();
        CV1PV2RespTCInv = new BOOL();
        CV1TargetInv = new BOOL();
        CV2Faulted = new BOOL();
        CV2HandFBFaulted = new BOOL();
        CV2ProgInv = new BOOL();
        CV2OperInv = new BOOL();
        CV2OverrideValueInv = new BOOL();
        CV2EUSpanInv = new BOOL();
        CV2LimitsInv = new BOOL();
        CV2ROCLimitInv = new BOOL();
        CV2HandFBInv = new BOOL();
        CV2PV1ModelGainInv = new BOOL();
        CV2PV2ModelGainInv = new BOOL();
        CV2PV1ModelTCInv = new BOOL();
        CV2PV2ModelTCInv = new BOOL();
        CV2PV1ModelDTInv = new BOOL();
        CV2PV2ModelDTInv = new BOOL();
        CV2PV1RespTCInv = new BOOL();
        CV2PV2RespTCInv = new BOOL();
        CV2TargetInv = new BOOL();
        CV3Faulted = new BOOL();
        CV3HandFBFaulted = new BOOL();
        CV3ProgInv = new BOOL();
        CV3OperInv = new BOOL();
        CV3OverrideValueInv = new BOOL();
        CV3EUSpanInv = new BOOL();
        CV3LimitsInv = new BOOL();
        CV3ROCLimitInv = new BOOL();
        CV3HandFBInv = new BOOL();
        CV3PV1ModelGainInv = new BOOL();
        CV3PV2ModelGainInv = new BOOL();
        CV3PV1ModelTCInv = new BOOL();
        CV3PV2ModelTCInv = new BOOL();
        CV3PV1ModelDTInv = new BOOL();
        CV3PV2ModelDTInv = new BOOL();
        CV3PV1RespTCInv = new BOOL();
        CV3PV2RespTCInv = new BOOL();
        CV3TargetInv = new BOOL();
    }

    /// <summary>
    /// Creates a new <see cref="MMC"/> instance initialized with the provided element.
    /// </summary>
    public MMC(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PV1</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL PV1
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PV2</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL PV2
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PV1Fault</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL PV1Fault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PV2Fault</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL PV2Fault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PV1EUMax</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL PV1EUMax
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PV2EUMax</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL PV2EUMax
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PV1EUMin</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL PV1EUMin
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PV2EUMin</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL PV2EUMin
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SP1Prog</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL SP1Prog
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SP2Prog</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL SP2Prog
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SP1Oper</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL SP1Oper
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SP2Oper</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL SP2Oper
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SP1HLimit</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL SP1HLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SP2HLimit</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL SP2HLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SP1LLimit</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL SP1LLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SP2LLimit</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL SP2LLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1Fault</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV1Fault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2Fault</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV2Fault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3Fault</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV3Fault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1InitReq</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV1InitReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2InitReq</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV2InitReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3InitReq</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV3InitReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1InitValue</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV1InitValue
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2InitValue</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV2InitValue
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3InitValue</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV3InitValue
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1Prog</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV1Prog
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2Prog</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV2Prog
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3Prog</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV3Prog
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1Oper</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV1Oper
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2Oper</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV2Oper
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3Oper</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV3Oper
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1OverrideValue</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV1OverrideValue
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2OverrideValue</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV2OverrideValue
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3OverrideValue</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV3OverrideValue
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVManLimiting</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CVManLimiting
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1EUMax</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV1EUMax
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2EUMax</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV2EUMax
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3EUMax</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV3EUMax
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1EUMin</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV1EUMin
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2EUMin</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV2EUMin
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3EUMin</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV3EUMin
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1HLimit</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV1HLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2HLimit</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV2HLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3HLimit</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV3HLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1LLimit</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV1LLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2LLimit</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV2LLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3LLimit</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV3LLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1ROCPosLimit</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV1ROCPosLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2ROCPosLimit</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV2ROCPosLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3ROCPosLimit</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV3ROCPosLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1ROCNegLimit</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV1ROCNegLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2ROCNegLimit</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV2ROCNegLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3ROCNegLimit</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV3ROCNegLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1HandFB</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV1HandFB
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2HandFB</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV2HandFB
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3HandFB</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV3HandFB
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1HandFBFault</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV1HandFBFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2HandFBFault</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV2HandFBFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3HandFBFault</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV3HandFBFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1Target</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV1Target
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2Target</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV2Target
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3Target</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV3Target
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1WindupHIn</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV1WindupHIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2WindupHIn</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV2WindupHIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3WindupHIn</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV3WindupHIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1WindupLIn</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV1WindupLIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2WindupLIn</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV2WindupLIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3WindupLIn</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV3WindupLIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>GainEUSpan</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL GainEUSpan
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1PV1ProcessGainSign</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV1PV1ProcessGainSign
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2PV1ProcessGainSign</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV2PV1ProcessGainSign
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3PV1ProcessGainSign</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV3PV1ProcessGainSign
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1PV2ProcessGainSign</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV1PV2ProcessGainSign
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2PV2ProcessGainSign</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV2PV2ProcessGainSign
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3PV2ProcessGainSign</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV3PV2ProcessGainSign
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProcessType</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public DINT ProcessType
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1PV1ModelGain</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV1PV1ModelGain
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2PV1ModelGain</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV2PV1ModelGain
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3PV1ModelGain</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV3PV1ModelGain
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1PV2ModelGain</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV1PV2ModelGain
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2PV2ModelGain</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV2PV2ModelGain
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3PV2ModelGain</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV3PV2ModelGain
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1PV1ModelTC</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV1PV1ModelTC
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2PV1ModelTC</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV2PV1ModelTC
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3PV1ModelTC</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV3PV1ModelTC
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1PV2ModelTC</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV1PV2ModelTC
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2PV2ModelTC</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV2PV2ModelTC
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3PV2ModelTC</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV3PV2ModelTC
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1PV1ModelDT</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV1PV1ModelDT
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2PV1ModelDT</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV2PV1ModelDT
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3PV1ModelDT</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV3PV1ModelDT
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1PV2ModelDT</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV1PV2ModelDT
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2PV2ModelDT</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV2PV2ModelDT
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3PV2ModelDT</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV3PV2ModelDT
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1PV1RespTC</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV1PV1RespTC
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2PV1RespTC</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV2PV1RespTC
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3PV1RespTC</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV3PV1RespTC
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1PV2RespTC</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV1PV2RespTC
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2PV2RespTC</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV2PV2RespTC
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3PV2RespTC</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV3PV2RespTC
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PV1Act1stCV</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public DINT PV1Act1stCV
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PV1Act2ndCV</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public DINT PV1Act2ndCV
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PV1Act3rdCV</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public DINT PV1Act3rdCV
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PV2Act1stCV</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public DINT PV2Act1stCV
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PV2Act2ndCV</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public DINT PV2Act2ndCV
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PV2Act3rdCV</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public DINT PV2Act3rdCV
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TargetCV</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public DINT TargetCV
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TargetRespTC</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL TargetRespTC
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PVTracking</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL PVTracking
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ManualAfterInit</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL ManualAfterInit
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgProgReq</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL ProgProgReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgOperReq</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL ProgOperReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgCV1AutoReq</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL ProgCV1AutoReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgCV2AutoReq</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL ProgCV2AutoReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgCV3AutoReq</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL ProgCV3AutoReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgCV1ManualReq</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL ProgCV1ManualReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgCV2ManualReq</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL ProgCV2ManualReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgCV3ManualReq</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL ProgCV3ManualReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgCV1OverrideReq</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL ProgCV1OverrideReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgCV2OverrideReq</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL ProgCV2OverrideReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgCV3OverrideReq</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL ProgCV3OverrideReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgCV1HandReq</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL ProgCV1HandReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgCV2HandReq</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL ProgCV2HandReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgCV3HandReq</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL ProgCV3HandReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OperProgReq</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL OperProgReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OperOperReq</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL OperOperReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OperCV1AutoReq</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL OperCV1AutoReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OperCV2AutoReq</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL OperCV2AutoReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OperCV3AutoReq</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL OperCV3AutoReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OperCV1ManualReq</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL OperCV1ManualReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OperCV2ManualReq</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL OperCV2ManualReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OperCV3ManualReq</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL OperCV3ManualReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgValueReset</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL ProgValueReset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TimingMode</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public DINT TimingMode
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OversampleDT</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL OversampleDT
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RTSTime</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public DINT RTSTime
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RTSTimeStamp</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public DINT RTSTimeStamp
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PV1TuneLimit</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL PV1TuneLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PV2TuneLimit</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL PV2TuneLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PV1AtuneTimeLimit</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL PV1AtuneTimeLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PV2AtuneTimeLimit</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL PV2AtuneTimeLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PV1NoiseLevel</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public DINT PV1NoiseLevel
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PV2NoiseLevel</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public DINT PV2NoiseLevel
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1StepSize</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV1StepSize
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2StepSize</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV2StepSize
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3StepSize</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV3StepSize
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1PV1ResponseSpeed</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public DINT CV1PV1ResponseSpeed
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2PV1ResponseSpeed</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public DINT CV2PV1ResponseSpeed
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3PV1ResponseSpeed</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public DINT CV3PV1ResponseSpeed
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1PV2ResponseSpeed</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public DINT CV1PV2ResponseSpeed
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2PV2ResponseSpeed</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public DINT CV2PV2ResponseSpeed
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3PV2ResponseSpeed</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public DINT CV3PV2ResponseSpeed
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1PV1ModelInit</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV1PV1ModelInit
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2PV1ModelInit</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV2PV1ModelInit
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3PV1ModelInit</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV3PV1ModelInit
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1PV2ModelInit</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV1PV2ModelInit
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2PV2ModelInit</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV2PV2ModelInit
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3PV2ModelInit</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV3PV2ModelInit
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PV1Factor</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL PV1Factor
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PV2Factor</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL PV2Factor
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV1Start</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV1Start
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV2Start</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV2Start
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV3Start</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV3Start
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV1PV1UseModel</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV1PV1UseModel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV2PV1UseModel</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV2PV1UseModel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV3PV1UseModel</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV3PV1UseModel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV1PV2UseModel</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV1PV2UseModel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV2PV2UseModel</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV2PV2UseModel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV3PV2UseModel</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV3PV2UseModel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV1Abort</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV1Abort
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV2Abort</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV2Abort
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV3Abort</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV3Abort
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1EU</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV1EU
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2EU</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV2EU
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3EU</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV3EU
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV1
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV2
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV3
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1Initializing</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV1Initializing
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2Initializing</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV2Initializing
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3Initializing</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV3Initializing
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1HAlarm</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV1HAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2HAlarm</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV2HAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3HAlarm</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV3HAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1LAlarm</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV1LAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2LAlarm</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV2LAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3LAlarm</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV3LAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1ROCPosAlarm</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV1ROCPosAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2ROCPosAlarm</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV2ROCPosAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3ROCPosAlarm</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV3ROCPosAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1ROCNegAlarm</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV1ROCNegAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2ROCNegAlarm</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV2ROCNegAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3ROCNegAlarm</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV3ROCNegAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SP1</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL SP1
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SP2</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL SP2
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SP1Percent</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL SP1Percent
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SP2Percent</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL SP2Percent
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SP1HAlarm</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL SP1HAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SP2HAlarm</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL SP2HAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SP1LAlarm</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL SP1LAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SP2LAlarm</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL SP2LAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PV1Percent</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL PV1Percent
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PV2Percent</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL PV2Percent
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>E1</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL E1
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>E2</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL E2
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>E1Percent</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL E1Percent
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>E2Percent</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL E2Percent
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1WindupHOut</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV1WindupHOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2WindupHOut</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV2WindupHOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3WindupHOut</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV3WindupHOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1WindupLOut</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV1WindupLOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2WindupLOut</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV2WindupLOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3WindupLOut</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV3WindupLOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgOper</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL ProgOper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1Auto</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV1Auto
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2Auto</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV2Auto
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3Auto</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV3Auto
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1Manual</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV1Manual
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2Manual</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV2Manual
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3Manual</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV3Manual
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1Override</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV1Override
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2Override</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV2Override
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3Override</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV3Override
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1Hand</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV1Hand
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2Hand</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV2Hand
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3Hand</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV3Hand
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DeltaT</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL DeltaT
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1StepSizeUsed</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV1StepSizeUsed
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2StepSizeUsed</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV2StepSizeUsed
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3StepSizeUsed</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV3StepSizeUsed
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1PV1GainTuned</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV1PV1GainTuned
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2PV1GainTuned</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV2PV1GainTuned
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3PV1GainTuned</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV3PV1GainTuned
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1PV2GainTuned</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV1PV2GainTuned
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2PV2GainTuned</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV2PV2GainTuned
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3PV2GainTuned</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV3PV2GainTuned
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1PV1TCTuned</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV1PV1TCTuned
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2PV1TCTuned</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV2PV1TCTuned
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3PV1TCTuned</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV3PV1TCTuned
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1PV2TCTuned</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV1PV2TCTuned
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2PV2TCTuned</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV2PV2TCTuned
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3PV2TCTuned</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV3PV2TCTuned
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1PV1DTTuned</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV1PV1DTTuned
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2PV1DTTuned</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV2PV1DTTuned
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3PV1DTTuned</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV3PV1DTTuned
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1PV2DTTuned</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV1PV2DTTuned
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2PV2DTTuned</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV2PV2DTTuned
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3PV2DTTuned</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV3PV2DTTuned
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1PV1RespTCTunedS</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV1PV1RespTCTunedS
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2PV1RespTCTunedS</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV2PV1RespTCTunedS
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3PV1RespTCTunedS</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV3PV1RespTCTunedS
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1PV2RespTCTunedS</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV1PV2RespTCTunedS
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2PV2RespTCTunedS</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV2PV2RespTCTunedS
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3PV2RespTCTunedS</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV3PV2RespTCTunedS
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1PV1RespTCTunedM</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV1PV1RespTCTunedM
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2PV1RespTCTunedM</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV2PV1RespTCTunedM
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3PV1RespTCTunedM</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV3PV1RespTCTunedM
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1PV2RespTCTunedM</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV1PV2RespTCTunedM
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2PV2RespTCTunedM</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV2PV2RespTCTunedM
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3PV2RespTCTunedM</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV3PV2RespTCTunedM
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1PV1RespTCTunedF</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV1PV1RespTCTunedF
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2PV1RespTCTunedF</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV2PV1RespTCTunedF
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3PV1RespTCTunedF</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV3PV1RespTCTunedF
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1PV2RespTCTunedF</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV1PV2RespTCTunedF
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2PV2RespTCTunedF</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV2PV2RespTCTunedF
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3PV2RespTCTunedF</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public REAL CV3PV2RespTCTunedF
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV1PV1On</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV1PV1On
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV2PV1On</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV2PV1On
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV3PV1On</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV3PV1On
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV1PV1Done</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV1PV1Done
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV2PV1Done</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV2PV1Done
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV3PV1Done</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV3PV1Done
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV1PV1Aborted</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV1PV1Aborted
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV2PV1Aborted</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV2PV1Aborted
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV3PV1Aborted</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV3PV1Aborted
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV1PV2On</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV1PV2On
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV2PV2On</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV2PV2On
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV3PV2On</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV3PV2On
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV1PV2Done</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV1PV2Done
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV2PV2Done</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV2PV2Done
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV3PV2Done</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV3PV2Done
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV1PV2Aborted</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV1PV2Aborted
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV2PV2Aborted</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV2PV2Aborted
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV3PV2Aborted</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV3PV2Aborted
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV1PV1Status</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public DINT AtuneCV1PV1Status
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV2PV1Status</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public DINT AtuneCV2PV1Status
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV3PV1Status</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public DINT AtuneCV3PV1Status
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV1PV2Status</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public DINT AtuneCV1PV2Status
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV2PV2Status</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public DINT AtuneCV2PV2Status
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV3PV2Status</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public DINT AtuneCV3PV2Status
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV1PV1Fault</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV1PV1Fault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV1PV1OutOfLimit</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV1PV1OutOfLimit
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV1PV1ModeInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV1PV1ModeInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV1PV1WindupFault</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV1PV1WindupFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV1PV1StepSize0</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV1PV1StepSize0
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV1PV1LimitsFault</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV1PV1LimitsFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV1PV1InitFault</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV1PV1InitFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV1PV1EUSpanChanged</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV1PV1EUSpanChanged
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV1PV1Changed</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV1PV1Changed
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV1PV1Timeout</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV1PV1Timeout
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV1PV1NotSettled</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV1PV1NotSettled
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV2PV1Fault</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV2PV1Fault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV2PV1OutOfLimit</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV2PV1OutOfLimit
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV2PV1ModeInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV2PV1ModeInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV2PV1WindupFault</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV2PV1WindupFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV2PV1StepSize0</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV2PV1StepSize0
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV2PV1LimitsFault</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV2PV1LimitsFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV2PV1InitFault</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV2PV1InitFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV2PV1EUSpanChanged</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV2PV1EUSpanChanged
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV2PV1Changed</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV2PV1Changed
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV2PV1Timeout</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV2PV1Timeout
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV2PV1NotSettled</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV2PV1NotSettled
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV3PV1Fault</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV3PV1Fault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV3PV1OutOfLimit</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV3PV1OutOfLimit
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV3PV1ModeInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV3PV1ModeInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV3PV1WindupFault</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV3PV1WindupFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV3PV1StepSize0</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV3PV1StepSize0
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV3PV1LimitsFault</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV3PV1LimitsFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV3PV1InitFault</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV3PV1InitFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV3PV1EUSpanChanged</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV3PV1EUSpanChanged
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV3PV1Changed</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV3PV1Changed
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV3PV1Timeout</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV3PV1Timeout
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV3PV1NotSettled</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV3PV1NotSettled
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV1PV2Fault</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV1PV2Fault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV1PV2OutOfLimit</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV1PV2OutOfLimit
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV1PV2ModeInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV1PV2ModeInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV1PV2WindupFault</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV1PV2WindupFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV1PV2StepSize0</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV1PV2StepSize0
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV1PV2LimitsFault</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV1PV2LimitsFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV1PV2InitFault</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV1PV2InitFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV1PV2EUSpanChanged</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV1PV2EUSpanChanged
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV1PV2Changed</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV1PV2Changed
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV1PV2Timeout</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV1PV2Timeout
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV1PV2NotSettled</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV1PV2NotSettled
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV2PV2Fault</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV2PV2Fault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV2PV2OutOfLimit</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV2PV2OutOfLimit
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV2PV2ModeInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV2PV2ModeInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV2PV2WindupFault</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV2PV2WindupFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV2PV2StepSize0</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV2PV2StepSize0
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV2PV2LimitsFault</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV2PV2LimitsFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV2PV2InitFault</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV2PV2InitFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV2PV2EUSpanChanged</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV2PV2EUSpanChanged
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV2PV2Changed</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV2PV2Changed
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV2PV2Timeout</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV2PV2Timeout
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV2PV2NotSettled</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV2PV2NotSettled
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV3PV2Fault</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV3PV2Fault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV3PV2OutOfLimit</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV3PV2OutOfLimit
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV3PV2ModeInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV3PV2ModeInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV3PV2WindupFault</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV3PV2WindupFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV3PV2StepSize0</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV3PV2StepSize0
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV3PV2LimitsFault</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV3PV2LimitsFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV3PV2InitFault</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV3PV2InitFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV3PV2EUSpanChanged</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV3PV2EUSpanChanged
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV3PV2Changed</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV3PV2Changed
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV3PV2Timeout</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV3PV2Timeout
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCV3PV2NotSettled</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL AtuneCV3PV2NotSettled
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Status1</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public DINT Status1
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Status2</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public DINT Status2
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Status3CV1</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public DINT Status3CV1
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Status3CV2</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public DINT Status3CV2
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Status3CV3</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public DINT Status3CV3
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InstructFault</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL InstructFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PV1Faulted</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL PV1Faulted
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PV2Faulted</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL PV2Faulted
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PV1SpanInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL PV1SpanInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PV2SpanInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL PV2SpanInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SP1ProgInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL SP1ProgInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SP2ProgInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL SP2ProgInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SP1OperInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL SP1OperInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SP2OperInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL SP2OperInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SP1LimitsInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL SP1LimitsInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SP2LimitsInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL SP2LimitsInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SampleTimeTooSmall</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL SampleTimeTooSmall
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PV1FactorInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL PV1FactorInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PV2FactorInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL PV2FactorInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TimingModeInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL TimingModeInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RTSMissed</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL RTSMissed
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RTSTimeInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL RTSTimeInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RTSTimeStampInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL RTSTimeStampInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DeltaTInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL DeltaTInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1Faulted</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV1Faulted
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1HandFBFaulted</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV1HandFBFaulted
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1ProgInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV1ProgInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1OperInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV1OperInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1OverrideValueInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV1OverrideValueInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1EUSpanInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV1EUSpanInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1LimitsInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV1LimitsInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1ROCLimitInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV1ROCLimitInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1HandFBInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV1HandFBInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1PV1ModelGainInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV1PV1ModelGainInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1PV2ModelGainInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV1PV2ModelGainInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1PV1ModelTCInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV1PV1ModelTCInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1PV2ModelTCInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV1PV2ModelTCInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1PV1ModelDTInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV1PV1ModelDTInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1PV2ModelDTInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV1PV2ModelDTInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1PV1RespTCInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV1PV1RespTCInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1PV2RespTCInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV1PV2RespTCInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV1TargetInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV1TargetInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2Faulted</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV2Faulted
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2HandFBFaulted</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV2HandFBFaulted
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2ProgInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV2ProgInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2OperInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV2OperInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2OverrideValueInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV2OverrideValueInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2EUSpanInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV2EUSpanInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2LimitsInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV2LimitsInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2ROCLimitInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV2ROCLimitInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2HandFBInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV2HandFBInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2PV1ModelGainInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV2PV1ModelGainInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2PV2ModelGainInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV2PV2ModelGainInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2PV1ModelTCInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV2PV1ModelTCInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2PV2ModelTCInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV2PV2ModelTCInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2PV1ModelDTInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV2PV1ModelDTInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2PV2ModelDTInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV2PV2ModelDTInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2PV1RespTCInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV2PV1RespTCInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2PV2RespTCInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV2PV2RespTCInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV2TargetInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV2TargetInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3Faulted</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV3Faulted
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3HandFBFaulted</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV3HandFBFaulted
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3ProgInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV3ProgInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3OperInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV3OperInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3OverrideValueInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV3OverrideValueInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3EUSpanInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV3EUSpanInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3LimitsInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV3LimitsInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3ROCLimitInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV3ROCLimitInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3HandFBInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV3HandFBInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3PV1ModelGainInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV3PV1ModelGainInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3PV2ModelGainInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV3PV2ModelGainInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3PV1ModelTCInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV3PV1ModelTCInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3PV2ModelTCInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV3PV2ModelTCInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3PV1ModelDTInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV3PV1ModelDTInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3PV2ModelDTInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV3PV2ModelDTInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3PV1RespTCInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV3PV1RespTCInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3PV2RespTCInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV3PV2RespTCInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV3TargetInv</c> member of the <see cref="MMC"/> data type.
    /// </summary>
    public BOOL CV3TargetInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

}
