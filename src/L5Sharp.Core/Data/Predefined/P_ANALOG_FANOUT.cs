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
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 444;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        EnableIn.UpdateData((data[offset + 5] & (1 << 0)) != 0);
        EnableOut.UpdateData((data[offset + 5] & (1 << 1)) != 0);
        Inp_InitializeReq.UpdateData((data[offset + 5] & (1 << 2)) != 0);
        Inp_CV.UpdateData(data, offset + 5);
        Inp_CV1InitializationVal.UpdateData(data, offset + 9);
        Inp_CV2InitializationVal.UpdateData(data, offset + 13);
        Inp_CV3InitializationVal.UpdateData(data, offset + 17);
        Inp_CV4InitializationVal.UpdateData(data, offset + 21);
        Inp_CV5InitializationVal.UpdateData(data, offset + 25);
        Inp_CV6InitializationVal.UpdateData(data, offset + 29);
        Inp_CV7InitializationVal.UpdateData(data, offset + 33);
        Inp_CV8InitializationVal.UpdateData(data, offset + 37);
        Inp_CV1InitializeReq.UpdateData((data[offset + 41] & (1 << 3)) != 0);
        Inp_CV2InitializeReq.UpdateData((data[offset + 41] & (1 << 4)) != 0);
        Inp_CV3InitializeReq.UpdateData((data[offset + 41] & (1 << 5)) != 0);
        Inp_CV4InitializeReq.UpdateData((data[offset + 41] & (1 << 6)) != 0);
        Inp_CV5InitializeReq.UpdateData((data[offset + 41] & (1 << 7)) != 0);
        Inp_CV6InitializeReq.UpdateData((data[offset + 42] & (1 << 0)) != 0);
        Inp_CV7InitializeReq.UpdateData((data[offset + 42] & (1 << 1)) != 0);
        Inp_CV8InitializeReq.UpdateData((data[offset + 42] & (1 << 2)) != 0);
        Cfg_HasCV2.UpdateData((data[offset + 42] & (1 << 3)) != 0);
        Cfg_HasCV3.UpdateData((data[offset + 42] & (1 << 4)) != 0);
        Cfg_HasCV4.UpdateData((data[offset + 42] & (1 << 5)) != 0);
        Cfg_HasCV5.UpdateData((data[offset + 42] & (1 << 6)) != 0);
        Cfg_HasCV6.UpdateData((data[offset + 42] & (1 << 7)) != 0);
        Cfg_HasCV7.UpdateData((data[offset + 43] & (1 << 0)) != 0);
        Cfg_HasCV8.UpdateData((data[offset + 43] & (1 << 1)) != 0);
        Cfg_FixedInitializationVal.UpdateData(data, offset + 43);
        Cfg_UseFixedInitialization.UpdateData((data[offset + 47] & (1 << 2)) != 0);
        Cfg_ShedHold.UpdateData((data[offset + 47] & (1 << 3)) != 0);
        Cfg_HasCVNav.UpdateData((data[offset + 47] & (1 << 4)) != 0);
        Cfg_HasNav.UpdateData(data, offset + 47);
        Cfg_CVEUMin.UpdateData(data, offset + 48);
        Cfg_CVEUMax.UpdateData(data, offset + 52);
        Cfg_CVLoLim.UpdateData(data, offset + 56);
        Cfg_CVHiLim.UpdateData(data, offset + 60);
        Cfg_CVRoCLim.UpdateData(data, offset + 64);
        Cfg_CV1Ratio.UpdateData(data, offset + 68);
        Cfg_CV1Offset.UpdateData(data, offset + 72);
        Cfg_CV1LoLim.UpdateData(data, offset + 76);
        Cfg_CV1HiLim.UpdateData(data, offset + 80);
        Cfg_CV1TakeupRate.UpdateData(data, offset + 84);
        Cfg_CV2Ratio.UpdateData(data, offset + 88);
        Cfg_CV2Offset.UpdateData(data, offset + 92);
        Cfg_CV2LoLim.UpdateData(data, offset + 96);
        Cfg_CV2HiLim.UpdateData(data, offset + 100);
        Cfg_CV2TakeupRate.UpdateData(data, offset + 104);
        Cfg_CV3Ratio.UpdateData(data, offset + 108);
        Cfg_CV3Offset.UpdateData(data, offset + 112);
        Cfg_CV3LoLim.UpdateData(data, offset + 116);
        Cfg_CV3HiLim.UpdateData(data, offset + 120);
        Cfg_CV3TakeupRate.UpdateData(data, offset + 124);
        Cfg_CV4Ratio.UpdateData(data, offset + 128);
        Cfg_CV4Offset.UpdateData(data, offset + 132);
        Cfg_CV4LoLim.UpdateData(data, offset + 136);
        Cfg_CV4HiLim.UpdateData(data, offset + 140);
        Cfg_CV4TakeupRate.UpdateData(data, offset + 144);
        Cfg_CV5Ratio.UpdateData(data, offset + 148);
        Cfg_CV5Offset.UpdateData(data, offset + 152);
        Cfg_CV5LoLim.UpdateData(data, offset + 156);
        Cfg_CV5HiLim.UpdateData(data, offset + 160);
        Cfg_CV5TakeupRate.UpdateData(data, offset + 164);
        Cfg_CV6Ratio.UpdateData(data, offset + 168);
        Cfg_CV6Offset.UpdateData(data, offset + 172);
        Cfg_CV6LoLim.UpdateData(data, offset + 176);
        Cfg_CV6HiLim.UpdateData(data, offset + 180);
        Cfg_CV6TakeupRate.UpdateData(data, offset + 184);
        Cfg_CV7Ratio.UpdateData(data, offset + 188);
        Cfg_CV7Offset.UpdateData(data, offset + 192);
        Cfg_CV7LoLim.UpdateData(data, offset + 196);
        Cfg_CV7HiLim.UpdateData(data, offset + 200);
        Cfg_CV7TakeupRate.UpdateData(data, offset + 204);
        Cfg_CV8Ratio.UpdateData(data, offset + 208);
        Cfg_CV8Offset.UpdateData(data, offset + 212);
        Cfg_CV8LoLim.UpdateData(data, offset + 216);
        Cfg_CV8HiLim.UpdateData(data, offset + 220);
        Cfg_CV8TakeupRate.UpdateData(data, offset + 224);
        Cfg_CVDecPlcs.UpdateData(data, offset + 228);
        Out_CV1.UpdateData(data, offset + 229);
        Out_CV2.UpdateData(data, offset + 233);
        Out_CV3.UpdateData(data, offset + 237);
        Out_CV4.UpdateData(data, offset + 241);
        Out_CV5.UpdateData(data, offset + 245);
        Out_CV6.UpdateData(data, offset + 249);
        Out_CV7.UpdateData(data, offset + 253);
        Out_CV8.UpdateData(data, offset + 257);
        Out_CVInitializationVal.UpdateData(data, offset + 261);
        Out_CVInitializeReq.UpdateData((data[offset + 265] & (1 << 5)) != 0);
        Val_CVEUMin.UpdateData(data, offset + 265);
        Val_CVEUMax.UpdateData(data, offset + 269);
        Val_InpCV.UpdateData(data, offset + 273);
        Val_CV.UpdateData(data, offset + 277);
        Val_MinCVIn1.UpdateData(data, offset + 281);
        Val_MaxCVIn1.UpdateData(data, offset + 285);
        Val_MinCVIn2.UpdateData(data, offset + 289);
        Val_MaxCVIn2.UpdateData(data, offset + 293);
        Val_MinCVIn3.UpdateData(data, offset + 297);
        Val_MaxCVIn3.UpdateData(data, offset + 301);
        Val_MinCVIn4.UpdateData(data, offset + 305);
        Val_MaxCVIn4.UpdateData(data, offset + 309);
        Val_MinCVIn5.UpdateData(data, offset + 313);
        Val_MaxCVIn5.UpdateData(data, offset + 317);
        Val_MinCVIn6.UpdateData(data, offset + 321);
        Val_MaxCVIn6.UpdateData(data, offset + 325);
        Val_MinCVIn7.UpdateData(data, offset + 329);
        Val_MaxCVIn7.UpdateData(data, offset + 333);
        Val_MinCVIn8.UpdateData(data, offset + 337);
        Val_MaxCVIn8.UpdateData(data, offset + 341);
        Sts_Initialized.UpdateData((data[offset + 345] & (1 << 6)) != 0);
        Sts_CVInfNaN.UpdateData((data[offset + 345] & (1 << 7)) != 0);
        Sts_CVLimited.UpdateData((data[offset + 346] & (1 << 0)) != 0);
        Sts_CV1InitializationInfNaN.UpdateData((data[offset + 346] & (1 << 1)) != 0);
        Sts_CV1Limited.UpdateData((data[offset + 346] & (1 << 2)) != 0);
        Sts_CV2InitializationInfNaN.UpdateData((data[offset + 346] & (1 << 3)) != 0);
        Sts_CV2Limited.UpdateData((data[offset + 346] & (1 << 4)) != 0);
        Sts_CV3InitializationInfNaN.UpdateData((data[offset + 346] & (1 << 5)) != 0);
        Sts_CV3Limited.UpdateData((data[offset + 346] & (1 << 6)) != 0);
        Sts_CV4InitializationInfNaN.UpdateData((data[offset + 346] & (1 << 7)) != 0);
        Sts_CV4Limited.UpdateData((data[offset + 351] & (1 << 0)) != 0);
        Sts_CV5InitializationInfNaN.UpdateData((data[offset + 351] & (1 << 1)) != 0);
        Sts_CV5Limited.UpdateData((data[offset + 351] & (1 << 2)) != 0);
        Sts_CV6InitializationInfNaN.UpdateData((data[offset + 351] & (1 << 3)) != 0);
        Sts_CV6Limited.UpdateData((data[offset + 351] & (1 << 4)) != 0);
        Sts_CV7InitializationInfNaN.UpdateData((data[offset + 351] & (1 << 5)) != 0);
        Sts_CV7Limited.UpdateData((data[offset + 351] & (1 << 6)) != 0);
        Sts_CV8InitializationInfNaN.UpdateData((data[offset + 351] & (1 << 7)) != 0);
        Sts_CV8Limited.UpdateData((data[offset + 352] & (1 << 0)) != 0);
        Sts_Err.UpdateData((data[offset + 352] & (1 << 1)) != 0);
        Sts_ErrLim.UpdateData((data[offset + 352] & (1 << 2)) != 0);
        Sts_ErrEU.UpdateData((data[offset + 352] & (1 << 3)) != 0);
        Sts_ErrCV1Lim.UpdateData((data[offset + 352] & (1 << 4)) != 0);
        Sts_ErrCV2Lim.UpdateData((data[offset + 352] & (1 << 5)) != 0);
        Sts_ErrCV3Lim.UpdateData((data[offset + 352] & (1 << 6)) != 0);
        Sts_ErrCV4Lim.UpdateData((data[offset + 352] & (1 << 7)) != 0);
        Sts_ErrCV5Lim.UpdateData((data[offset + 353] & (1 << 0)) != 0);
        Sts_ErrCV6Lim.UpdateData((data[offset + 353] & (1 << 1)) != 0);
        Sts_ErrCV7Lim.UpdateData((data[offset + 353] & (1 << 2)) != 0);
        Sts_ErrCV8Lim.UpdateData((data[offset + 353] & (1 << 3)) != 0);
        
        return offset + GetSize();
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