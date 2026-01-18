using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>P_BOOLEAN_LOGIC</c> data type structure.
/// </summary>
[LogixData("P_BOOLEAN_LOGIC")]
public sealed partial class P_BOOLEAN_LOGIC : StructureData
{
    /// <summary>
    /// Creates a new <see cref="P_BOOLEAN_LOGIC"/> instance initialized with default values.
    /// </summary>
    public P_BOOLEAN_LOGIC() : base("P_BOOLEAN_LOGIC")
    {
        EnableIn = new BOOL();
        EnableOut = new BOOL();
        Inp_InitializeReq = new BOOL();
        Inp_0 = new BOOL();
        Inp_1 = new BOOL();
        Inp_2 = new BOOL();
        Inp_3 = new BOOL();
        Inp_4 = new BOOL();
        Inp_5 = new BOOL();
        Inp_6 = new BOOL();
        Inp_7 = new BOOL();
        Inp_Hold = new BOOL();
        Inp_Reset = new BOOL();
        Cfg_UseInpHold = new BOOL();
        Cfg_UsePCmd = new BOOL();
        Cfg_UseOCmd = new BOOL();
        Cfg_UseOut01 = new BOOL();
        Cfg_UseOut10 = new BOOL();
        Cfg_TimestampOnSnap = new BOOL();
        Cfg_SnapOver = new BOOL();
        Cfg_HasMoreObj = new BOOL();
        Cfg_OnDly = new REAL();
        Cfg_OffDly = new REAL();
        Cfg_CnfrmReqd = new SINT();
        PCmd_Snap = new BOOL();
        PCmd_Reset = new BOOL();
        Out_Live = new BOOL();
        Out_Snap = new BOOL();
        Val_DlyPctLive = new DINT();
        Val_DlyPctSnap = new DINT();
        Val_SnapInit = new DINT();
        Sts_Initialized = new BOOL();
        Sts_Snapped = new BOOL();
        Sts_RdyReset = new BOOL();
        Sts_InpLive = new SINT();
        Sts_GateLive = new SINT();
        Sts_InpSnap = new SINT();
        Sts_GateSnap = new SINT();
        Sts_GateSrc1Live = new SINT();
        Sts_GateSrc2Live = new SINT();
        Sts_GateSrc3Live = new SINT();
        Sts_GateSrc4Live = new SINT();
        Sts_GateSrc1Snap = new SINT();
        Sts_GateSrc2Snap = new SINT();
        Sts_GateSrc3Snap = new SINT();
        Sts_GateSrc4Snap = new SINT();
        Sts_OutInvertLive = new BOOL();
        Sts_OutInvertSnap = new BOOL();
        Sts_Err = new BOOL();
        Sts_ErrGateFunc = new SINT();
        Sts_ErrGateSrcPtr = new SINT();
        Sts_ErrGateSrcMask = new SINT();
        Sts_ErrOutSrcPtr = new BOOL();
        Sts_ErrTimer = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="P_BOOLEAN_LOGIC"/> instance initialized with the provided element.
    /// </summary>
    public P_BOOLEAN_LOGIC(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="P_BOOLEAN_LOGIC"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="P_BOOLEAN_LOGIC"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_InitializeReq</c> member of the <see cref="P_BOOLEAN_LOGIC"/> data type.
    /// </summary>
    public BOOL Inp_InitializeReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_0</c> member of the <see cref="P_BOOLEAN_LOGIC"/> data type.
    /// </summary>
    public BOOL Inp_0
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_1</c> member of the <see cref="P_BOOLEAN_LOGIC"/> data type.
    /// </summary>
    public BOOL Inp_1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_2</c> member of the <see cref="P_BOOLEAN_LOGIC"/> data type.
    /// </summary>
    public BOOL Inp_2
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_3</c> member of the <see cref="P_BOOLEAN_LOGIC"/> data type.
    /// </summary>
    public BOOL Inp_3
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_4</c> member of the <see cref="P_BOOLEAN_LOGIC"/> data type.
    /// </summary>
    public BOOL Inp_4
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_5</c> member of the <see cref="P_BOOLEAN_LOGIC"/> data type.
    /// </summary>
    public BOOL Inp_5
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_6</c> member of the <see cref="P_BOOLEAN_LOGIC"/> data type.
    /// </summary>
    public BOOL Inp_6
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_7</c> member of the <see cref="P_BOOLEAN_LOGIC"/> data type.
    /// </summary>
    public BOOL Inp_7
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Hold</c> member of the <see cref="P_BOOLEAN_LOGIC"/> data type.
    /// </summary>
    public BOOL Inp_Hold
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Reset</c> member of the <see cref="P_BOOLEAN_LOGIC"/> data type.
    /// </summary>
    public BOOL Inp_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_UseInpHold</c> member of the <see cref="P_BOOLEAN_LOGIC"/> data type.
    /// </summary>
    public BOOL Cfg_UseInpHold
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_UsePCmd</c> member of the <see cref="P_BOOLEAN_LOGIC"/> data type.
    /// </summary>
    public BOOL Cfg_UsePCmd
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_UseOCmd</c> member of the <see cref="P_BOOLEAN_LOGIC"/> data type.
    /// </summary>
    public BOOL Cfg_UseOCmd
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_UseOut01</c> member of the <see cref="P_BOOLEAN_LOGIC"/> data type.
    /// </summary>
    public BOOL Cfg_UseOut01
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_UseOut10</c> member of the <see cref="P_BOOLEAN_LOGIC"/> data type.
    /// </summary>
    public BOOL Cfg_UseOut10
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_TimestampOnSnap</c> member of the <see cref="P_BOOLEAN_LOGIC"/> data type.
    /// </summary>
    public BOOL Cfg_TimestampOnSnap
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_SnapOver</c> member of the <see cref="P_BOOLEAN_LOGIC"/> data type.
    /// </summary>
    public BOOL Cfg_SnapOver
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasMoreObj</c> member of the <see cref="P_BOOLEAN_LOGIC"/> data type.
    /// </summary>
    public BOOL Cfg_HasMoreObj
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OnDly</c> member of the <see cref="P_BOOLEAN_LOGIC"/> data type.
    /// </summary>
    public REAL Cfg_OnDly
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OffDly</c> member of the <see cref="P_BOOLEAN_LOGIC"/> data type.
    /// </summary>
    public REAL Cfg_OffDly
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CnfrmReqd</c> member of the <see cref="P_BOOLEAN_LOGIC"/> data type.
    /// </summary>
    public SINT Cfg_CnfrmReqd
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Snap</c> member of the <see cref="P_BOOLEAN_LOGIC"/> data type.
    /// </summary>
    public BOOL PCmd_Snap
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Reset</c> member of the <see cref="P_BOOLEAN_LOGIC"/> data type.
    /// </summary>
    public BOOL PCmd_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_Live</c> member of the <see cref="P_BOOLEAN_LOGIC"/> data type.
    /// </summary>
    public BOOL Out_Live
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_Snap</c> member of the <see cref="P_BOOLEAN_LOGIC"/> data type.
    /// </summary>
    public BOOL Out_Snap
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_DlyPctLive</c> member of the <see cref="P_BOOLEAN_LOGIC"/> data type.
    /// </summary>
    public DINT Val_DlyPctLive
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_DlyPctSnap</c> member of the <see cref="P_BOOLEAN_LOGIC"/> data type.
    /// </summary>
    public DINT Val_DlyPctSnap
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_SnapInit</c> member of the <see cref="P_BOOLEAN_LOGIC"/> data type.
    /// </summary>
    public DINT Val_SnapInit
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Initialized</c> member of the <see cref="P_BOOLEAN_LOGIC"/> data type.
    /// </summary>
    public BOOL Sts_Initialized
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Snapped</c> member of the <see cref="P_BOOLEAN_LOGIC"/> data type.
    /// </summary>
    public BOOL Sts_Snapped
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_RdyReset</c> member of the <see cref="P_BOOLEAN_LOGIC"/> data type.
    /// </summary>
    public BOOL Sts_RdyReset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_InpLive</c> member of the <see cref="P_BOOLEAN_LOGIC"/> data type.
    /// </summary>
    public SINT Sts_InpLive
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_GateLive</c> member of the <see cref="P_BOOLEAN_LOGIC"/> data type.
    /// </summary>
    public SINT Sts_GateLive
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_InpSnap</c> member of the <see cref="P_BOOLEAN_LOGIC"/> data type.
    /// </summary>
    public SINT Sts_InpSnap
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_GateSnap</c> member of the <see cref="P_BOOLEAN_LOGIC"/> data type.
    /// </summary>
    public SINT Sts_GateSnap
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_GateSrc1Live</c> member of the <see cref="P_BOOLEAN_LOGIC"/> data type.
    /// </summary>
    public SINT Sts_GateSrc1Live
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_GateSrc2Live</c> member of the <see cref="P_BOOLEAN_LOGIC"/> data type.
    /// </summary>
    public SINT Sts_GateSrc2Live
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_GateSrc3Live</c> member of the <see cref="P_BOOLEAN_LOGIC"/> data type.
    /// </summary>
    public SINT Sts_GateSrc3Live
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_GateSrc4Live</c> member of the <see cref="P_BOOLEAN_LOGIC"/> data type.
    /// </summary>
    public SINT Sts_GateSrc4Live
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_GateSrc1Snap</c> member of the <see cref="P_BOOLEAN_LOGIC"/> data type.
    /// </summary>
    public SINT Sts_GateSrc1Snap
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_GateSrc2Snap</c> member of the <see cref="P_BOOLEAN_LOGIC"/> data type.
    /// </summary>
    public SINT Sts_GateSrc2Snap
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_GateSrc3Snap</c> member of the <see cref="P_BOOLEAN_LOGIC"/> data type.
    /// </summary>
    public SINT Sts_GateSrc3Snap
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_GateSrc4Snap</c> member of the <see cref="P_BOOLEAN_LOGIC"/> data type.
    /// </summary>
    public SINT Sts_GateSrc4Snap
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_OutInvertLive</c> member of the <see cref="P_BOOLEAN_LOGIC"/> data type.
    /// </summary>
    public BOOL Sts_OutInvertLive
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_OutInvertSnap</c> member of the <see cref="P_BOOLEAN_LOGIC"/> data type.
    /// </summary>
    public BOOL Sts_OutInvertSnap
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Err</c> member of the <see cref="P_BOOLEAN_LOGIC"/> data type.
    /// </summary>
    public BOOL Sts_Err
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrGateFunc</c> member of the <see cref="P_BOOLEAN_LOGIC"/> data type.
    /// </summary>
    public SINT Sts_ErrGateFunc
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrGateSrcPtr</c> member of the <see cref="P_BOOLEAN_LOGIC"/> data type.
    /// </summary>
    public SINT Sts_ErrGateSrcPtr
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrGateSrcMask</c> member of the <see cref="P_BOOLEAN_LOGIC"/> data type.
    /// </summary>
    public SINT Sts_ErrGateSrcMask
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrOutSrcPtr</c> member of the <see cref="P_BOOLEAN_LOGIC"/> data type.
    /// </summary>
    public BOOL Sts_ErrOutSrcPtr
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrTimer</c> member of the <see cref="P_BOOLEAN_LOGIC"/> data type.
    /// </summary>
    public BOOL Sts_ErrTimer
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}