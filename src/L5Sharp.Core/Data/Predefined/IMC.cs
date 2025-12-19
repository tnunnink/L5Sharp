using System.Xml.Linq;
// Auto-generated file
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>IMC</c> data type structure.
/// </summary>
[LogixData("IMC")]
public sealed partial class IMC : StructureData
{
    /// <summary>
    /// Creates a new <see cref="IMC"/> instance initialized with default values.
    /// </summary>
    public IMC() : base("IMC")
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
        CVOverrideValue = new REAL();
        CVTrackValue = new REAL();
        CVManLimiting = new BOOL();
        CVEUMax = new REAL();
        CVEUMin = new REAL();
        CVHLimit = new REAL();
        CVLLimit = new REAL();
        CVROCPosLimit = new REAL();
        CVROCNegLimit = new REAL();
        HandFB = new REAL();
        HandFBFault = new BOOL();
        WindupHIn = new BOOL();
        WindupLIn = new BOOL();
        GainEUSpan = new BOOL();
        ProcessGainSign = new BOOL();
        ProcessType = new DINT();
        ModelGain = new REAL();
        ModelTC = new REAL();
        ModelDT = new REAL();
        RespTC = new REAL();
        PVTracking = new BOOL();
        CVTrackReq = new BOOL();
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
        PVTuneLimit = new REAL();
        AtuneTimeLimit = new REAL();
        NoiseLevel = new DINT();
        CVStepSize = new REAL();
        ResponseSpeed = new DINT();
        ModelInit = new BOOL();
        Factor = new REAL();
        AtuneStart = new BOOL();
        AtuneUseModel = new BOOL();
        AtuneAbort = new BOOL();
        EnableOut = new BOOL();
        CVEU = new REAL();
        CV = new REAL();
        DeltaCV = new REAL();
        CVInitializing = new BOOL();
        CVHAlarm = new BOOL();
        CVLAlarm = new BOOL();
        CVROCPosAlarm = new BOOL();
        CVROCNegAlarm = new BOOL();
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
        ProgOper = new BOOL();
        CasRat = new BOOL();
        Auto = new BOOL();
        Manual = new BOOL();
        Override = new BOOL();
        Hand = new BOOL();
        DeltaT = new REAL();
        StepSizeUsed = new REAL();
        GainTuned = new REAL();
        TCTuned = new REAL();
        DTTuned = new REAL();
        RespTCTunedS = new REAL();
        RespTCTunedM = new REAL();
        RespTCTunedF = new REAL();
        AtuneOn = new BOOL();
        AtuneDone = new BOOL();
        AtuneAborted = new BOOL();
        AtuneStatus = new DINT();
        AtuneFault = new BOOL();
        AtunePVOutOfLimit = new BOOL();
        AtuneModeInv = new BOOL();
        AtuneCVWindupFault = new BOOL();
        AtuneStepSize0 = new BOOL();
        AtuneCVLimitsFault = new BOOL();
        AtuneCVInitFault = new BOOL();
        AtuneEUSpanChanged = new BOOL();
        AtuneCVChanged = new BOOL();
        AtuneTimeout = new BOOL();
        AtunePVNotSettled = new BOOL();
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
        RatioLimitsInv = new BOOL();
        RatioProgInv = new BOOL();
        RatioOperInv = new BOOL();
        CVProgInv = new BOOL();
        CVOperInv = new BOOL();
        CVOverrideValueInv = new BOOL();
        CVTrackValueInv = new BOOL();
        CVEUSpanInv = new BOOL();
        CVLimitsInv = new BOOL();
        CVROCLimitInv = new BOOL();
        HandFBInv = new BOOL();
        SampleTimeTooSmall = new BOOL();
        FactorInv = new BOOL();
        ModelGainInv = new BOOL();
        ModelTCInv = new BOOL();
        ModelDTInv = new BOOL();
        RespTCInv = new BOOL();
        TimingModeInv = new BOOL();
        RTSMissed = new BOOL();
        RTSTimeInv = new BOOL();
        RTSTimeStampInv = new BOOL();
        DeltaTInv = new BOOL();
    }

    /// <summary>
    /// Creates a new <see cref="IMC"/> instance initialized with the provided element.
    /// </summary>
    public IMC(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PV</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public REAL PV
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PVFault</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL PVFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PVEUMax</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public REAL PVEUMax
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PVEUMin</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public REAL PVEUMin
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SPProg</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public REAL SPProg
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SPOper</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public REAL SPOper
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SPCascade</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public REAL SPCascade
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SPHLimit</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public REAL SPHLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SPLLimit</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public REAL SPLLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>UseRatio</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL UseRatio
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RatioProg</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public REAL RatioProg
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RatioOper</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public REAL RatioOper
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RatioHLimit</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public REAL RatioHLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RatioLLimit</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public REAL RatioLLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVFault</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL CVFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVInitReq</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL CVInitReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVInitValue</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public REAL CVInitValue
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVProg</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public REAL CVProg
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVOper</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public REAL CVOper
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVOverrideValue</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public REAL CVOverrideValue
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVTrackValue</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public REAL CVTrackValue
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVManLimiting</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL CVManLimiting
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVEUMax</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public REAL CVEUMax
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVEUMin</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public REAL CVEUMin
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVHLimit</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public REAL CVHLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVLLimit</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public REAL CVLLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVROCPosLimit</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public REAL CVROCPosLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVROCNegLimit</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public REAL CVROCNegLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HandFB</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public REAL HandFB
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HandFBFault</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL HandFBFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>WindupHIn</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL WindupHIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>WindupLIn</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL WindupLIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>GainEUSpan</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL GainEUSpan
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProcessGainSign</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL ProcessGainSign
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProcessType</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public DINT ProcessType
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ModelGain</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public REAL ModelGain
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ModelTC</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public REAL ModelTC
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ModelDT</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public REAL ModelDT
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RespTC</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public REAL RespTC
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PVTracking</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL PVTracking
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVTrackReq</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL CVTrackReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AllowCasRat</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL AllowCasRat
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ManualAfterInit</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL ManualAfterInit
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgProgReq</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL ProgProgReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgOperReq</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL ProgOperReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgCasRatReq</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL ProgCasRatReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgAutoReq</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL ProgAutoReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgManualReq</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL ProgManualReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgOverrideReq</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL ProgOverrideReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgHandReq</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL ProgHandReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OperProgReq</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL OperProgReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OperOperReq</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL OperOperReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OperCasRatReq</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL OperCasRatReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OperAutoReq</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL OperAutoReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OperManualReq</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL OperManualReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgValueReset</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL ProgValueReset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TimingMode</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public DINT TimingMode
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OversampleDT</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public REAL OversampleDT
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RTSTime</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public DINT RTSTime
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RTSTimeStamp</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public DINT RTSTimeStamp
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PVTuneLimit</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public REAL PVTuneLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneTimeLimit</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public REAL AtuneTimeLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>NoiseLevel</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public DINT NoiseLevel
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVStepSize</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public REAL CVStepSize
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ResponseSpeed</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public DINT ResponseSpeed
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ModelInit</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL ModelInit
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Factor</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public REAL Factor
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneStart</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL AtuneStart
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneUseModel</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL AtuneUseModel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneAbort</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL AtuneAbort
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVEU</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public REAL CVEU
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CV</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public REAL CV
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DeltaCV</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public REAL DeltaCV
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVInitializing</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL CVInitializing
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVHAlarm</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL CVHAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVLAlarm</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL CVLAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVROCPosAlarm</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL CVROCPosAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVROCNegAlarm</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL CVROCNegAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SP</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public REAL SP
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SPPercent</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public REAL SPPercent
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SPHAlarm</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL SPHAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SPLAlarm</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL SPLAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PVPercent</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public REAL PVPercent
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>E</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public REAL E
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EPercent</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public REAL EPercent
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InitPrimary</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL InitPrimary
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>WindupHOut</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL WindupHOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>WindupLOut</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL WindupLOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Ratio</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public REAL Ratio
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RatioHAlarm</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL RatioHAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RatioLAlarm</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL RatioLAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgOper</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL ProgOper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CasRat</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL CasRat
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Auto</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL Auto
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Manual</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL Manual
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Override</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL Override
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Hand</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL Hand
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DeltaT</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public REAL DeltaT
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>StepSizeUsed</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public REAL StepSizeUsed
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>GainTuned</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public REAL GainTuned
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TCTuned</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public REAL TCTuned
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DTTuned</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public REAL DTTuned
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RespTCTunedS</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public REAL RespTCTunedS
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RespTCTunedM</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public REAL RespTCTunedM
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RespTCTunedF</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public REAL RespTCTunedF
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneOn</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL AtuneOn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneDone</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL AtuneDone
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneAborted</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL AtuneAborted
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneStatus</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public DINT AtuneStatus
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneFault</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL AtuneFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtunePVOutOfLimit</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL AtunePVOutOfLimit
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneModeInv</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL AtuneModeInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCVWindupFault</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL AtuneCVWindupFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneStepSize0</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL AtuneStepSize0
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCVLimitsFault</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL AtuneCVLimitsFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCVInitFault</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL AtuneCVInitFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneEUSpanChanged</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL AtuneEUSpanChanged
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneCVChanged</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL AtuneCVChanged
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtuneTimeout</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL AtuneTimeout
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AtunePVNotSettled</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL AtunePVNotSettled
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Status1</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public DINT Status1
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Status2</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public DINT Status2
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InstructFault</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL InstructFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PVFaulted</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL PVFaulted
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVFaulted</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL CVFaulted
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HandFBFaulted</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL HandFBFaulted
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PVSpanInv</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL PVSpanInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SPProgInv</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL SPProgInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SPOperInv</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL SPOperInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SPCascadeInv</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL SPCascadeInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SPLimitsInv</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL SPLimitsInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RatioLimitsInv</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL RatioLimitsInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RatioProgInv</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL RatioProgInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RatioOperInv</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL RatioOperInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVProgInv</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL CVProgInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVOperInv</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL CVOperInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVOverrideValueInv</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL CVOverrideValueInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVTrackValueInv</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL CVTrackValueInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVEUSpanInv</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL CVEUSpanInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVLimitsInv</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL CVLimitsInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CVROCLimitInv</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL CVROCLimitInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HandFBInv</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL HandFBInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SampleTimeTooSmall</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL SampleTimeTooSmall
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FactorInv</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL FactorInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ModelGainInv</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL ModelGainInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ModelTCInv</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL ModelTCInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ModelDTInv</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL ModelDTInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RespTCInv</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL RespTCInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TimingModeInv</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL TimingModeInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RTSMissed</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL RTSMissed
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RTSTimeInv</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL RTSTimeInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RTSTimeStampInv</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL RTSTimeStampInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DeltaTInv</c> member of the <see cref="IMC"/> data type.
    /// </summary>
    public BOOL DeltaTInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

}
