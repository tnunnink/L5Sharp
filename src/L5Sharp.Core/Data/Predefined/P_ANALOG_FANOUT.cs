using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>P_ANALOG_FANOUT</c> data type structure.
/// </summary>
[LogixData("P_ANALOG_FANOUT")]
public sealed partial class P_ANALOG_FANOUT : StructureData
{
    /// <summary>
    /// Creates a new <see cref="P_ANALOG_FANOUT"/> instance initialized with default values.
    /// </summary>
    public P_ANALOG_FANOUT() : base("P_ANALOG_FANOUT")
    {
        EnableIn = new BOOL();
        EnableOut = new BOOL();
        Inp_InitializeReq = new BOOL();
        Inp_CV = new REAL();
        Inp_CV1InitializationVal = new REAL();
        Inp_CV2InitializationVal = new REAL();
        Inp_CV3InitializationVal = new REAL();
        Inp_CV4InitializationVal = new REAL();
        Inp_CV5InitializationVal = new REAL();
        Inp_CV6InitializationVal = new REAL();
        Inp_CV7InitializationVal = new REAL();
        Inp_CV8InitializationVal = new REAL();
        Inp_CV1InitializeReq = new BOOL();
        Inp_CV2InitializeReq = new BOOL();
        Inp_CV3InitializeReq = new BOOL();
        Inp_CV4InitializeReq = new BOOL();
        Inp_CV5InitializeReq = new BOOL();
        Inp_CV6InitializeReq = new BOOL();
        Inp_CV7InitializeReq = new BOOL();
        Inp_CV8InitializeReq = new BOOL();
        Cfg_HasCV2 = new BOOL();
        Cfg_HasCV3 = new BOOL();
        Cfg_HasCV4 = new BOOL();
        Cfg_HasCV5 = new BOOL();
        Cfg_HasCV6 = new BOOL();
        Cfg_HasCV7 = new BOOL();
        Cfg_HasCV8 = new BOOL();
        Cfg_FixedInitializationVal = new REAL();
        Cfg_UseFixedInitialization = new BOOL();
        Cfg_ShedHold = new BOOL();
        Cfg_HasCVNav = new BOOL();
        Cfg_HasNav = new SINT();
        Cfg_CVEUMin = new REAL();
        Cfg_CVEUMax = new REAL();
        Cfg_CVLoLim = new REAL();
        Cfg_CVHiLim = new REAL();
        Cfg_CVRoCLim = new REAL();
        Cfg_CV1Ratio = new REAL();
        Cfg_CV1Offset = new REAL();
        Cfg_CV1LoLim = new REAL();
        Cfg_CV1HiLim = new REAL();
        Cfg_CV1TakeupRate = new REAL();
        Cfg_CV2Ratio = new REAL();
        Cfg_CV2Offset = new REAL();
        Cfg_CV2LoLim = new REAL();
        Cfg_CV2HiLim = new REAL();
        Cfg_CV2TakeupRate = new REAL();
        Cfg_CV3Ratio = new REAL();
        Cfg_CV3Offset = new REAL();
        Cfg_CV3LoLim = new REAL();
        Cfg_CV3HiLim = new REAL();
        Cfg_CV3TakeupRate = new REAL();
        Cfg_CV4Ratio = new REAL();
        Cfg_CV4Offset = new REAL();
        Cfg_CV4LoLim = new REAL();
        Cfg_CV4HiLim = new REAL();
        Cfg_CV4TakeupRate = new REAL();
        Cfg_CV5Ratio = new REAL();
        Cfg_CV5Offset = new REAL();
        Cfg_CV5LoLim = new REAL();
        Cfg_CV5HiLim = new REAL();
        Cfg_CV5TakeupRate = new REAL();
        Cfg_CV6Ratio = new REAL();
        Cfg_CV6Offset = new REAL();
        Cfg_CV6LoLim = new REAL();
        Cfg_CV6HiLim = new REAL();
        Cfg_CV6TakeupRate = new REAL();
        Cfg_CV7Ratio = new REAL();
        Cfg_CV7Offset = new REAL();
        Cfg_CV7LoLim = new REAL();
        Cfg_CV7HiLim = new REAL();
        Cfg_CV7TakeupRate = new REAL();
        Cfg_CV8Ratio = new REAL();
        Cfg_CV8Offset = new REAL();
        Cfg_CV8LoLim = new REAL();
        Cfg_CV8HiLim = new REAL();
        Cfg_CV8TakeupRate = new REAL();
        Cfg_CVDecPlcs = new SINT();
        Out_CV1 = new REAL();
        Out_CV2 = new REAL();
        Out_CV3 = new REAL();
        Out_CV4 = new REAL();
        Out_CV5 = new REAL();
        Out_CV6 = new REAL();
        Out_CV7 = new REAL();
        Out_CV8 = new REAL();
        Out_CVInitializationVal = new REAL();
        Out_CVInitializeReq = new BOOL();
        Val_CVEUMin = new REAL();
        Val_CVEUMax = new REAL();
        Val_InpCV = new REAL();
        Val_CV = new REAL();
        Val_MinCVIn1 = new REAL();
        Val_MaxCVIn1 = new REAL();
        Val_MinCVIn2 = new REAL();
        Val_MaxCVIn2 = new REAL();
        Val_MinCVIn3 = new REAL();
        Val_MaxCVIn3 = new REAL();
        Val_MinCVIn4 = new REAL();
        Val_MaxCVIn4 = new REAL();
        Val_MinCVIn5 = new REAL();
        Val_MaxCVIn5 = new REAL();
        Val_MinCVIn6 = new REAL();
        Val_MaxCVIn6 = new REAL();
        Val_MinCVIn7 = new REAL();
        Val_MaxCVIn7 = new REAL();
        Val_MinCVIn8 = new REAL();
        Val_MaxCVIn8 = new REAL();
        Sts_Initialized = new BOOL();
        Sts_CVInfNaN = new BOOL();
        Sts_CVLimited = new BOOL();
        Sts_CV1InitializationInfNaN = new BOOL();
        Sts_CV1Limited = new BOOL();
        Sts_CV2InitializationInfNaN = new BOOL();
        Sts_CV2Limited = new BOOL();
        Sts_CV3InitializationInfNaN = new BOOL();
        Sts_CV3Limited = new BOOL();
        Sts_CV4InitializationInfNaN = new BOOL();
        Sts_CV4Limited = new BOOL();
        Sts_CV5InitializationInfNaN = new BOOL();
        Sts_CV5Limited = new BOOL();
        Sts_CV6InitializationInfNaN = new BOOL();
        Sts_CV6Limited = new BOOL();
        Sts_CV7InitializationInfNaN = new BOOL();
        Sts_CV7Limited = new BOOL();
        Sts_CV8InitializationInfNaN = new BOOL();
        Sts_CV8Limited = new BOOL();
        Sts_Err = new BOOL();
        Sts_ErrLim = new BOOL();
        Sts_ErrEU = new BOOL();
        Sts_ErrCV1Lim = new BOOL();
        Sts_ErrCV2Lim = new BOOL();
        Sts_ErrCV3Lim = new BOOL();
        Sts_ErrCV4Lim = new BOOL();
        Sts_ErrCV5Lim = new BOOL();
        Sts_ErrCV6Lim = new BOOL();
        Sts_ErrCV7Lim = new BOOL();
        Sts_ErrCV8Lim = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="P_ANALOG_FANOUT"/> instance initialized with the provided element.
    /// </summary>
    public P_ANALOG_FANOUT(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_InitializeReq</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public BOOL Inp_InitializeReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_CV</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Inp_CV
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_CV1InitializationVal</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Inp_CV1InitializationVal
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_CV2InitializationVal</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Inp_CV2InitializationVal
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_CV3InitializationVal</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Inp_CV3InitializationVal
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_CV4InitializationVal</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Inp_CV4InitializationVal
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_CV5InitializationVal</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Inp_CV5InitializationVal
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_CV6InitializationVal</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Inp_CV6InitializationVal
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_CV7InitializationVal</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Inp_CV7InitializationVal
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_CV8InitializationVal</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Inp_CV8InitializationVal
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_CV1InitializeReq</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public BOOL Inp_CV1InitializeReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_CV2InitializeReq</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public BOOL Inp_CV2InitializeReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_CV3InitializeReq</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public BOOL Inp_CV3InitializeReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_CV4InitializeReq</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public BOOL Inp_CV4InitializeReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_CV5InitializeReq</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public BOOL Inp_CV5InitializeReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_CV6InitializeReq</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public BOOL Inp_CV6InitializeReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_CV7InitializeReq</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public BOOL Inp_CV7InitializeReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_CV8InitializeReq</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public BOOL Inp_CV8InitializeReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasCV2</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public BOOL Cfg_HasCV2
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasCV3</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public BOOL Cfg_HasCV3
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasCV4</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public BOOL Cfg_HasCV4
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasCV5</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public BOOL Cfg_HasCV5
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasCV6</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public BOOL Cfg_HasCV6
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasCV7</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public BOOL Cfg_HasCV7
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasCV8</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public BOOL Cfg_HasCV8
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_FixedInitializationVal</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Cfg_FixedInitializationVal
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_UseFixedInitialization</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public BOOL Cfg_UseFixedInitialization
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ShedHold</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public BOOL Cfg_ShedHold
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasCVNav</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public BOOL Cfg_HasCVNav
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasNav</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public SINT Cfg_HasNav
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CVEUMin</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Cfg_CVEUMin
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CVEUMax</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Cfg_CVEUMax
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CVLoLim</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Cfg_CVLoLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CVHiLim</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Cfg_CVHiLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CVRoCLim</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Cfg_CVRoCLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CV1Ratio</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Cfg_CV1Ratio
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CV1Offset</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Cfg_CV1Offset
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CV1LoLim</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Cfg_CV1LoLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CV1HiLim</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Cfg_CV1HiLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CV1TakeupRate</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Cfg_CV1TakeupRate
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CV2Ratio</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Cfg_CV2Ratio
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CV2Offset</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Cfg_CV2Offset
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CV2LoLim</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Cfg_CV2LoLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CV2HiLim</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Cfg_CV2HiLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CV2TakeupRate</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Cfg_CV2TakeupRate
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CV3Ratio</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Cfg_CV3Ratio
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CV3Offset</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Cfg_CV3Offset
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CV3LoLim</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Cfg_CV3LoLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CV3HiLim</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Cfg_CV3HiLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CV3TakeupRate</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Cfg_CV3TakeupRate
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CV4Ratio</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Cfg_CV4Ratio
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CV4Offset</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Cfg_CV4Offset
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CV4LoLim</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Cfg_CV4LoLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CV4HiLim</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Cfg_CV4HiLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CV4TakeupRate</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Cfg_CV4TakeupRate
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CV5Ratio</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Cfg_CV5Ratio
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CV5Offset</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Cfg_CV5Offset
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CV5LoLim</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Cfg_CV5LoLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CV5HiLim</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Cfg_CV5HiLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CV5TakeupRate</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Cfg_CV5TakeupRate
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CV6Ratio</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Cfg_CV6Ratio
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CV6Offset</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Cfg_CV6Offset
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CV6LoLim</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Cfg_CV6LoLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CV6HiLim</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Cfg_CV6HiLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CV6TakeupRate</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Cfg_CV6TakeupRate
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CV7Ratio</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Cfg_CV7Ratio
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CV7Offset</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Cfg_CV7Offset
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CV7LoLim</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Cfg_CV7LoLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CV7HiLim</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Cfg_CV7HiLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CV7TakeupRate</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Cfg_CV7TakeupRate
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CV8Ratio</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Cfg_CV8Ratio
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CV8Offset</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Cfg_CV8Offset
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CV8LoLim</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Cfg_CV8LoLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CV8HiLim</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Cfg_CV8HiLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CV8TakeupRate</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Cfg_CV8TakeupRate
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CVDecPlcs</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public SINT Cfg_CVDecPlcs
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_CV1</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Out_CV1
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_CV2</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Out_CV2
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_CV3</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Out_CV3
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_CV4</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Out_CV4
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_CV5</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Out_CV5
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_CV6</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Out_CV6
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_CV7</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Out_CV7
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_CV8</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Out_CV8
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_CVInitializationVal</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Out_CVInitializationVal
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_CVInitializeReq</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public BOOL Out_CVInitializeReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_CVEUMin</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Val_CVEUMin
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_CVEUMax</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Val_CVEUMax
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_InpCV</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Val_InpCV
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_CV</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Val_CV
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_MinCVIn1</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Val_MinCVIn1
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_MaxCVIn1</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Val_MaxCVIn1
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_MinCVIn2</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Val_MinCVIn2
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_MaxCVIn2</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Val_MaxCVIn2
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_MinCVIn3</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Val_MinCVIn3
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_MaxCVIn3</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Val_MaxCVIn3
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_MinCVIn4</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Val_MinCVIn4
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_MaxCVIn4</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Val_MaxCVIn4
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_MinCVIn5</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Val_MinCVIn5
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_MaxCVIn5</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Val_MaxCVIn5
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_MinCVIn6</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Val_MinCVIn6
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_MaxCVIn6</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Val_MaxCVIn6
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_MinCVIn7</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Val_MinCVIn7
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_MaxCVIn7</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Val_MaxCVIn7
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_MinCVIn8</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Val_MinCVIn8
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_MaxCVIn8</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public REAL Val_MaxCVIn8
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Initialized</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public BOOL Sts_Initialized
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_CVInfNaN</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public BOOL Sts_CVInfNaN
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_CVLimited</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public BOOL Sts_CVLimited
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_CV1InitializationInfNaN</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public BOOL Sts_CV1InitializationInfNaN
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_CV1Limited</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public BOOL Sts_CV1Limited
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_CV2InitializationInfNaN</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public BOOL Sts_CV2InitializationInfNaN
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_CV2Limited</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public BOOL Sts_CV2Limited
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_CV3InitializationInfNaN</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public BOOL Sts_CV3InitializationInfNaN
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_CV3Limited</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public BOOL Sts_CV3Limited
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_CV4InitializationInfNaN</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public BOOL Sts_CV4InitializationInfNaN
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_CV4Limited</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public BOOL Sts_CV4Limited
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_CV5InitializationInfNaN</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public BOOL Sts_CV5InitializationInfNaN
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_CV5Limited</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public BOOL Sts_CV5Limited
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_CV6InitializationInfNaN</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public BOOL Sts_CV6InitializationInfNaN
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_CV6Limited</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public BOOL Sts_CV6Limited
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_CV7InitializationInfNaN</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public BOOL Sts_CV7InitializationInfNaN
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_CV7Limited</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public BOOL Sts_CV7Limited
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_CV8InitializationInfNaN</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public BOOL Sts_CV8InitializationInfNaN
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_CV8Limited</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public BOOL Sts_CV8Limited
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Err</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public BOOL Sts_Err
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrLim</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrLim
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrEU</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrEU
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrCV1Lim</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrCV1Lim
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrCV2Lim</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrCV2Lim
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrCV3Lim</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrCV3Lim
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrCV4Lim</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrCV4Lim
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrCV5Lim</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrCV5Lim
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrCV6Lim</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrCV6Lim
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrCV7Lim</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrCV7Lim
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrCV8Lim</c> member of the <see cref="P_ANALOG_FANOUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrCV8Lim
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}