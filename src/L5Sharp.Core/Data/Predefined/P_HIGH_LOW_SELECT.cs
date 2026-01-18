using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>P_HIGH_LOW_SELECT</c> data type structure.
/// </summary>
[LogixData("P_HIGH_LOW_SELECT")]
public sealed partial class P_HIGH_LOW_SELECT : StructureData
{
    /// <summary>
    /// Creates a new <see cref="P_HIGH_LOW_SELECT"/> instance initialized with default values.
    /// </summary>
    public P_HIGH_LOW_SELECT() : base("P_HIGH_LOW_SELECT")
    {
        EnableIn = new BOOL();
        EnableOut = new BOOL();
        Inp_InitializeReq = new BOOL();
        Inp_1 = new REAL();
        Inp_E1 = new REAL();
        Inp_PGain1 = new REAL();
        Inp_2 = new REAL();
        Inp_E2 = new REAL();
        Inp_PGain2 = new REAL();
        Inp_3 = new REAL();
        Inp_E3 = new REAL();
        Inp_PGain3 = new REAL();
        Inp_4 = new REAL();
        Inp_E4 = new REAL();
        Inp_PGain4 = new REAL();
        Inp_5 = new REAL();
        Inp_E5 = new REAL();
        Inp_PGain5 = new REAL();
        Inp_6 = new REAL();
        Inp_E6 = new REAL();
        Inp_PGain6 = new REAL();
        Inp_UseInitializeVal = new BOOL();
        Inp_InitializeVal = new REAL();
        Cfg_HiLoSel = new BOOL();
        Cfg_HasInp1 = new BOOL();
        Cfg_UseInp1 = new BOOL();
        Cfg_Inp1Offset = new BOOL();
        Cfg_HasInp2 = new BOOL();
        Cfg_UseInp2 = new BOOL();
        Cfg_Inp2Offset = new BOOL();
        Cfg_HasInp3 = new BOOL();
        Cfg_UseInp3 = new BOOL();
        Cfg_Inp3Offset = new BOOL();
        Cfg_HasInp4 = new BOOL();
        Cfg_UseInp4 = new BOOL();
        Cfg_Inp4Offset = new BOOL();
        Cfg_HasInp5 = new BOOL();
        Cfg_UseInp5 = new BOOL();
        Cfg_Inp5Offset = new BOOL();
        Cfg_HasInp6 = new BOOL();
        Cfg_UseInp6 = new BOOL();
        Cfg_Inp6Offset = new BOOL();
        Cfg_DecPlcs = new SINT();
        Cfg_HasOutNav = new BOOL();
        Cfg_HasNav = new SINT();
        Cfg_OutLoLim = new REAL();
        Cfg_OutHiLim = new REAL();
        Val = new REAL();
        Val_Inp1Prev = new REAL();
        Sts_UseInp1Prev = new BOOL();
        Val_Inp2Prev = new REAL();
        Sts_UseInp2Prev = new BOOL();
        Val_Inp3Prev = new REAL();
        Sts_UseInp3Prev = new BOOL();
        Val_Inp4Prev = new REAL();
        Sts_UseInp4Prev = new BOOL();
        Val_Inp5Prev = new REAL();
        Sts_UseInp5Prev = new BOOL();
        Val_Inp6Prev = new REAL();
        Sts_UseInp6Prev = new BOOL();
        Val_Out = new REAL();
        Val_Sel = new DINT();
        Sts_Initialized = new BOOL();
        Sts_MaintByp = new BOOL();
        Sts_Err = new BOOL();
        Sts_ErrHas = new BOOL();
        Sts_ErrLim = new BOOL();
        Cfg_HasMoreObj = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="P_HIGH_LOW_SELECT"/> instance initialized with the provided element.
    /// </summary>
    public P_HIGH_LOW_SELECT(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_InitializeReq</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public BOOL Inp_InitializeReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_1</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public REAL Inp_1
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_E1</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public REAL Inp_E1
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PGain1</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public REAL Inp_PGain1
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_2</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public REAL Inp_2
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_E2</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public REAL Inp_E2
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PGain2</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public REAL Inp_PGain2
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_3</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public REAL Inp_3
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_E3</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public REAL Inp_E3
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PGain3</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public REAL Inp_PGain3
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_4</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public REAL Inp_4
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_E4</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public REAL Inp_E4
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PGain4</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public REAL Inp_PGain4
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_5</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public REAL Inp_5
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_E5</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public REAL Inp_E5
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PGain5</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public REAL Inp_PGain5
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_6</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public REAL Inp_6
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_E6</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public REAL Inp_E6
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PGain6</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public REAL Inp_PGain6
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_UseInitializeVal</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public BOOL Inp_UseInitializeVal
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_InitializeVal</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public REAL Inp_InitializeVal
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HiLoSel</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public BOOL Cfg_HiLoSel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasInp1</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public BOOL Cfg_HasInp1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_UseInp1</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public BOOL Cfg_UseInp1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_Inp1Offset</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public BOOL Cfg_Inp1Offset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasInp2</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public BOOL Cfg_HasInp2
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_UseInp2</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public BOOL Cfg_UseInp2
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_Inp2Offset</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public BOOL Cfg_Inp2Offset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasInp3</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public BOOL Cfg_HasInp3
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_UseInp3</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public BOOL Cfg_UseInp3
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_Inp3Offset</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public BOOL Cfg_Inp3Offset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasInp4</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public BOOL Cfg_HasInp4
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_UseInp4</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public BOOL Cfg_UseInp4
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_Inp4Offset</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public BOOL Cfg_Inp4Offset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasInp5</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public BOOL Cfg_HasInp5
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_UseInp5</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public BOOL Cfg_UseInp5
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_Inp5Offset</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public BOOL Cfg_Inp5Offset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasInp6</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public BOOL Cfg_HasInp6
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_UseInp6</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public BOOL Cfg_UseInp6
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_Inp6Offset</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public BOOL Cfg_Inp6Offset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_DecPlcs</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public SINT Cfg_DecPlcs
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasOutNav</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public BOOL Cfg_HasOutNav
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasNav</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public SINT Cfg_HasNav
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OutLoLim</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public REAL Cfg_OutLoLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OutHiLim</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public REAL Cfg_OutHiLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public REAL Val
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_Inp1Prev</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public REAL Val_Inp1Prev
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_UseInp1Prev</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public BOOL Sts_UseInp1Prev
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_Inp2Prev</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public REAL Val_Inp2Prev
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_UseInp2Prev</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public BOOL Sts_UseInp2Prev
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_Inp3Prev</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public REAL Val_Inp3Prev
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_UseInp3Prev</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public BOOL Sts_UseInp3Prev
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_Inp4Prev</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public REAL Val_Inp4Prev
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_UseInp4Prev</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public BOOL Sts_UseInp4Prev
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_Inp5Prev</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public REAL Val_Inp5Prev
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_UseInp5Prev</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public BOOL Sts_UseInp5Prev
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_Inp6Prev</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public REAL Val_Inp6Prev
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_UseInp6Prev</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public BOOL Sts_UseInp6Prev
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_Out</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public REAL Val_Out
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_Sel</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public DINT Val_Sel
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Initialized</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public BOOL Sts_Initialized
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_MaintByp</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public BOOL Sts_MaintByp
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Err</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public BOOL Sts_Err
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrHas</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public BOOL Sts_ErrHas
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrLim</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public BOOL Sts_ErrLim
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasMoreObj</c> member of the <see cref="P_HIGH_LOW_SELECT"/> data type.
    /// </summary>
    public BOOL Cfg_HasMoreObj
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}