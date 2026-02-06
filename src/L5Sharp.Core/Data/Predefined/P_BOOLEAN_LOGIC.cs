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
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 472;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        EnableIn.UpdateData((data[offset + 5] & (1 << 0)) != 0);
        EnableOut.UpdateData((data[offset + 5] & (1 << 1)) != 0);
        Inp_InitializeReq.UpdateData((data[offset + 5] & (1 << 2)) != 0);
        Inp_0.UpdateData((data[offset + 5] & (1 << 3)) != 0);
        Inp_1.UpdateData((data[offset + 5] & (1 << 4)) != 0);
        Inp_2.UpdateData((data[offset + 5] & (1 << 5)) != 0);
        Inp_3.UpdateData((data[offset + 5] & (1 << 6)) != 0);
        Inp_4.UpdateData((data[offset + 5] & (1 << 7)) != 0);
        Inp_5.UpdateData((data[offset + 6] & (1 << 0)) != 0);
        Inp_6.UpdateData((data[offset + 6] & (1 << 1)) != 0);
        Inp_7.UpdateData((data[offset + 6] & (1 << 2)) != 0);
        Inp_Hold.UpdateData((data[offset + 6] & (1 << 3)) != 0);
        Inp_Reset.UpdateData((data[offset + 6] & (1 << 4)) != 0);
        Cfg_UseInpHold.UpdateData((data[offset + 6] & (1 << 5)) != 0);
        Cfg_UsePCmd.UpdateData((data[offset + 6] & (1 << 6)) != 0);
        Cfg_UseOCmd.UpdateData((data[offset + 6] & (1 << 7)) != 0);
        Cfg_UseOut01.UpdateData((data[offset + 7] & (1 << 0)) != 0);
        Cfg_UseOut10.UpdateData((data[offset + 7] & (1 << 1)) != 0);
        Cfg_TimestampOnSnap.UpdateData((data[offset + 7] & (1 << 2)) != 0);
        Cfg_SnapOver.UpdateData((data[offset + 7] & (1 << 3)) != 0);
        Cfg_HasMoreObj.UpdateData((data[offset + 7] & (1 << 4)) != 0);
        Cfg_OnDly.UpdateData(data, offset + 7);
        Cfg_OffDly.UpdateData(data, offset + 11);
        Cfg_CnfrmReqd.UpdateData(data, offset + 15);
        PCmd_Snap.UpdateData((data[offset + 16] & (1 << 5)) != 0);
        PCmd_Reset.UpdateData((data[offset + 16] & (1 << 6)) != 0);
        Out_Live.UpdateData((data[offset + 16] & (1 << 7)) != 0);
        Out_Snap.UpdateData((data[offset + 17] & (1 << 0)) != 0);
        Val_DlyPctLive.UpdateData(data, offset + 17);
        Val_DlyPctSnap.UpdateData(data, offset + 21);
        Val_SnapInit.UpdateData(data, offset + 25);
        Sts_Initialized.UpdateData((data[offset + 29] & (1 << 1)) != 0);
        Sts_Snapped.UpdateData((data[offset + 29] & (1 << 2)) != 0);
        Sts_RdyReset.UpdateData((data[offset + 29] & (1 << 3)) != 0);
        Sts_InpLive.UpdateData(data, offset + 29);
        Sts_GateLive.UpdateData(data, offset + 30);
        Sts_InpSnap.UpdateData(data, offset + 31);
        Sts_GateSnap.UpdateData(data, offset + 32);
        Sts_GateSrc1Live.UpdateData(data, offset + 33);
        Sts_GateSrc2Live.UpdateData(data, offset + 34);
        Sts_GateSrc3Live.UpdateData(data, offset + 35);
        Sts_GateSrc4Live.UpdateData(data, offset + 36);
        Sts_GateSrc1Snap.UpdateData(data, offset + 37);
        Sts_GateSrc2Snap.UpdateData(data, offset + 38);
        Sts_GateSrc3Snap.UpdateData(data, offset + 39);
        Sts_GateSrc4Snap.UpdateData(data, offset + 40);
        Sts_OutInvertLive.UpdateData((data[offset + 41] & (1 << 4)) != 0);
        Sts_OutInvertSnap.UpdateData((data[offset + 41] & (1 << 5)) != 0);
        Sts_Err.UpdateData((data[offset + 41] & (1 << 6)) != 0);
        Sts_ErrGateFunc.UpdateData(data, offset + 41);
        Sts_ErrGateSrcPtr.UpdateData(data, offset + 42);
        Sts_ErrGateSrcMask.UpdateData(data, offset + 43);
        Sts_ErrOutSrcPtr.UpdateData((data[offset + 44] & (1 << 7)) != 0);
        Sts_ErrTimer.UpdateData((data[offset + 45] & (1 << 0)) != 0);
        
        return offset + GetSize();
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