using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>P_DEADBAND</c> data type structure.
/// </summary>
[LogixData("P_DEADBAND")]
public sealed partial class P_DEADBAND : StructureData
{
    /// <summary>
    /// Creates a new <see cref="P_DEADBAND"/> instance initialized with default values.
    /// </summary>
    public P_DEADBAND() : base("P_DEADBAND")
    {
        EnableIn = new BOOL();
        EnableOut = new BOOL();
        Inp_InitializeReq = new BOOL();
        Inp_PV = new REAL();
        Inp_PVSrcQ = new SINT();
        Inp_PVNotify = new SINT();
        Inp_PVBad = new BOOL();
        Inp_OvrdCmd = new SINT();
        Inp_OvrdRaiseSP = new REAL();
        Inp_OvrdLowerSP = new REAL();
        Inp_HiDevGate = new BOOL();
        Inp_LoDevGate = new BOOL();
        Inp_HiRoCIncrGate = new BOOL();
        Inp_HiRoCDecrGate = new BOOL();
        Inp_OwnerCmd = new DINT();
        Inp_ExtInh = new BOOL();
        Inp_Hand = new BOOL();
        Inp_Ovrd = new BOOL();
        Cfg_PVDecPlcs = new SINT();
        Cfg_SetTrack = new BOOL();
        Cfg_HasMoreObj = new BOOL();
        Cfg_HasPVNav = new BOOL();
        Cfg_HasOutNav = new BOOL();
        Cfg_PVEUMin = new REAL();
        Cfg_PVEUMax = new REAL();
        Cfg_SPHiLim = new REAL();
        Cfg_SPLoLim = new REAL();
        Cfg_RaiseDB = new REAL();
        Cfg_LowerDB = new REAL();
        Cfg_RateTime = new REAL();
        Cfg_HiDevLim = new REAL();
        Cfg_HiDevDB = new REAL();
        Cfg_LoDevLim = new REAL();
        Cfg_LoDevDB = new REAL();
        Cfg_HiDevGateDly = new REAL();
        Cfg_LoDevGateDly = new REAL();
        Cfg_HiRoCIncrLim = new REAL();
        Cfg_HiRoCIncrDB = new REAL();
        Cfg_HiRoCIncrGateDly = new REAL();
        Cfg_HiRoCDecrLim = new REAL();
        Cfg_HiRoCDecrDB = new REAL();
        Cfg_HiRoCDecrGateDly = new REAL();
        Cfg_ExtAcqAsLevel = new BOOL();
        Cfg_ExtOverLock = new BOOL();
        Cfg_HasExt = new BOOL();
        Cfg_HasMaint = new BOOL();
        Cfg_HasMaintOoS = new BOOL();
        Cfg_HasOper = new BOOL();
        Cfg_HasOperLocked = new BOOL();
        Cfg_HasProg = new BOOL();
        Cfg_HasProgLocked = new BOOL();
        Cfg_OvrdOverLock = new BOOL();
        Cfg_PCmdLockAsLevel = new BOOL();
        Cfg_AllowDisable = new BOOL();
        Cfg_AllowShelve = new BOOL();
        Cfg_PCmdPriority = new BOOL();
        Cfg_PCmdProgAsLevel = new BOOL();
        Cfg_ProgNormal = new BOOL();
        Cfg_ProgPwrUp = new BOOL();
        Cfg_CnfrmReqd = new SINT();
        PSet_LowerSP = new REAL();
        PSet_RaiseSP = new REAL();
        PSet_Owner = new DINT();
        XSet_LowerSP = new REAL();
        XSet_RaiseSP = new REAL();
        PCmd_Raise = new BOOL();
        PCmd_Lower = new BOOL();
        PCmd_None = new BOOL();
        PCmd_Auto = new BOOL();
        PCmd_Man = new BOOL();
        PCmd_Reset = new BOOL();
        PCmd_Lock = new BOOL();
        PCmd_Normal = new BOOL();
        PCmd_Oper = new BOOL();
        PCmd_Prog = new BOOL();
        PCmd_Unlock = new BOOL();
        XCmd_Raise = new BOOL();
        XCmd_Lower = new BOOL();
        XCmd_None = new BOOL();
        XCmd_Auto = new BOOL();
        XCmd_Man = new BOOL();
        XCmd_Reset = new BOOL();
        XCmd_ResetAckAll = new BOOL();
        XCmd_Acq = new BOOL();
        XCmd_Rel = new BOOL();
        Out_Raise = new BOOL();
        Out_Lower = new BOOL();
        Out_Q = new BOOL();
        Out_QNot = new BOOL();
        Out_OwnerSts = new DINT();
        Val_PV = new REAL();
        Val_RoC = new REAL();
        Val_LowerSP = new REAL();
        Val_RaiseSP = new REAL();
        Val_PVEUMin = new REAL();
        Val_PVEUMax = new REAL();
        Val_Owner = new DINT();
        SrcQ_IO = new DINT();
        SrcQ = new DINT();
        Sts_Initialized = new BOOL();
        Sts_Raise = new BOOL();
        Sts_Lower = new BOOL();
        Sts_Auto = new BOOL();
        Sts_Man = new BOOL();
        Sts_Q = new BOOL();
        Sts_Available = new BOOL();
        Sts_NotRdy = new BOOL();
        Sts_NrdyCfgErr = new BOOL();
        Sts_NrdyOoS = new BOOL();
        Sts_AlmInh = new BOOL();
        Sts_Err = new BOOL();
        Sts_ErrEU = new BOOL();
        Sts_ErrRateTime = new BOOL();
        Sts_ErrAlm = new BOOL();
        Sts_OoS = new BOOL();
        Sts_Prog = new BOOL();
        Sts_RdyReset = new BOOL();
        Sts_RdyAck = new BOOL();
        Sts_HiDevCmp = new BOOL();
        Sts_HiDevGate = new BOOL();
        Sts_HiDev = new BOOL();
        Sts_LoDevCmp = new BOOL();
        Sts_LoDevGate = new BOOL();
        Sts_LoDev = new BOOL();
        Sts_HiRoCIncrCmp = new BOOL();
        Sts_HiRoCIncrGate = new BOOL();
        Sts_HiRoCIncr = new BOOL();
        Sts_HiRoCDecrCmp = new BOOL();
        Sts_HiRoCDecrGate = new BOOL();
        Sts_HiRoCDecr = new BOOL();
        Sts_eNotify = new SINT();
        Sts_eNotifyAll = new SINT();
        Sts_eNotifyHiDev = new SINT();
        Sts_eNotifyHiRoCDecr = new SINT();
        Sts_eNotifyHiRoCIncr = new SINT();
        Sts_eNotifyLoDev = new SINT();
        Sts_UnackAlmCount = new DINT();
        Sts_Alm = new BOOL();
        Sts_ErrHiDevGateDly = new BOOL();
        Sts_ErrLoDevGateDly = new BOOL();
        Sts_ErrHiRoCIncrGateDly = new BOOL();
        Sts_ErrHiRoCDecrGateDly = new BOOL();
        Sts_Oper = new BOOL();
        Sts_Maint = new BOOL();
        Sts_Ext = new BOOL();
        Sts_Ovrd = new BOOL();
        Sts_eFault = new INT();
        Sts_eSts = new INT();
        Sts_bSrc = new INT();
        Sts_eSrc = new INT();
        Sts_ExtReqInh = new BOOL();
        Sts_Hand = new BOOL();
        Sts_MAcqRcvd = new BOOL();
        Sts_Normal = new BOOL();
        Sts_OperLocked = new BOOL();
        Sts_ProgLocked = new BOOL();
        Sts_ProgOperLock = new BOOL();
        Sts_ProgOperSel = new BOOL();
        Sts_ProgReqInh = new BOOL();
        XRdy_Acq = new BOOL();
        XRdy_Rel = new BOOL();
        XRdy_Reset = new BOOL();
        XRdy_ResetAckAll = new BOOL();
        XRdy_Auto = new BOOL();
        XRdy_Lower = new BOOL();
        XRdy_Man = new BOOL();
        XRdy_None = new BOOL();
        XRdy_Raise = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="P_DEADBAND"/> instance initialized with the provided element.
    /// </summary>
    public P_DEADBAND(XElement element) : base(element)
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
        EnableIn.UpdateData((data[offset + 5] & (1 << 0)) != 0);
        EnableOut.UpdateData((data[offset + 5] & (1 << 1)) != 0);
        Inp_InitializeReq.UpdateData((data[offset + 5] & (1 << 2)) != 0);
        Inp_PV.UpdateData(data, offset + 5);
        Inp_PVSrcQ.UpdateData(data, offset + 9);
        Inp_PVNotify.UpdateData(data, offset + 10);
        Inp_PVBad.UpdateData((data[offset + 11] & (1 << 3)) != 0);
        Inp_OvrdCmd.UpdateData(data, offset + 11);
        Inp_OvrdRaiseSP.UpdateData(data, offset + 12);
        Inp_OvrdLowerSP.UpdateData(data, offset + 16);
        Inp_HiDevGate.UpdateData((data[offset + 20] & (1 << 4)) != 0);
        Inp_LoDevGate.UpdateData((data[offset + 20] & (1 << 5)) != 0);
        Inp_HiRoCIncrGate.UpdateData((data[offset + 20] & (1 << 6)) != 0);
        Inp_HiRoCDecrGate.UpdateData((data[offset + 20] & (1 << 7)) != 0);
        Inp_OwnerCmd.UpdateData(data, offset + 20);
        Inp_ExtInh.UpdateData((data[offset + 25] & (1 << 0)) != 0);
        Inp_Hand.UpdateData((data[offset + 25] & (1 << 1)) != 0);
        Inp_Ovrd.UpdateData((data[offset + 25] & (1 << 2)) != 0);
        Cfg_PVDecPlcs.UpdateData(data, offset + 25);
        Cfg_SetTrack.UpdateData((data[offset + 26] & (1 << 3)) != 0);
        Cfg_HasMoreObj.UpdateData((data[offset + 26] & (1 << 4)) != 0);
        Cfg_HasPVNav.UpdateData((data[offset + 26] & (1 << 5)) != 0);
        Cfg_HasOutNav.UpdateData((data[offset + 26] & (1 << 6)) != 0);
        Cfg_PVEUMin.UpdateData(data, offset + 26);
        Cfg_PVEUMax.UpdateData(data, offset + 30);
        Cfg_SPHiLim.UpdateData(data, offset + 34);
        Cfg_SPLoLim.UpdateData(data, offset + 38);
        Cfg_RaiseDB.UpdateData(data, offset + 42);
        Cfg_LowerDB.UpdateData(data, offset + 46);
        Cfg_RateTime.UpdateData(data, offset + 50);
        Cfg_HiDevLim.UpdateData(data, offset + 54);
        Cfg_HiDevDB.UpdateData(data, offset + 58);
        Cfg_LoDevLim.UpdateData(data, offset + 62);
        Cfg_LoDevDB.UpdateData(data, offset + 66);
        Cfg_HiDevGateDly.UpdateData(data, offset + 70);
        Cfg_LoDevGateDly.UpdateData(data, offset + 74);
        Cfg_HiRoCIncrLim.UpdateData(data, offset + 78);
        Cfg_HiRoCIncrDB.UpdateData(data, offset + 82);
        Cfg_HiRoCIncrGateDly.UpdateData(data, offset + 86);
        Cfg_HiRoCDecrLim.UpdateData(data, offset + 90);
        Cfg_HiRoCDecrDB.UpdateData(data, offset + 94);
        Cfg_HiRoCDecrGateDly.UpdateData(data, offset + 98);
        Cfg_ExtAcqAsLevel.UpdateData((data[offset + 102] & (1 << 7)) != 0);
        Cfg_ExtOverLock.UpdateData((data[offset + 103] & (1 << 0)) != 0);
        Cfg_HasExt.UpdateData((data[offset + 103] & (1 << 1)) != 0);
        Cfg_HasMaint.UpdateData((data[offset + 103] & (1 << 2)) != 0);
        Cfg_HasMaintOoS.UpdateData((data[offset + 103] & (1 << 3)) != 0);
        Cfg_HasOper.UpdateData((data[offset + 103] & (1 << 4)) != 0);
        Cfg_HasOperLocked.UpdateData((data[offset + 103] & (1 << 5)) != 0);
        Cfg_HasProg.UpdateData((data[offset + 103] & (1 << 6)) != 0);
        Cfg_HasProgLocked.UpdateData((data[offset + 103] & (1 << 7)) != 0);
        Cfg_OvrdOverLock.UpdateData((data[offset + 104] & (1 << 0)) != 0);
        Cfg_PCmdLockAsLevel.UpdateData((data[offset + 104] & (1 << 1)) != 0);
        Cfg_AllowDisable.UpdateData((data[offset + 104] & (1 << 2)) != 0);
        Cfg_AllowShelve.UpdateData((data[offset + 104] & (1 << 3)) != 0);
        Cfg_PCmdPriority.UpdateData((data[offset + 104] & (1 << 4)) != 0);
        Cfg_PCmdProgAsLevel.UpdateData((data[offset + 104] & (1 << 5)) != 0);
        Cfg_ProgNormal.UpdateData((data[offset + 104] & (1 << 6)) != 0);
        Cfg_ProgPwrUp.UpdateData((data[offset + 104] & (1 << 7)) != 0);
        Cfg_CnfrmReqd.UpdateData(data, offset + 104);
        PSet_LowerSP.UpdateData(data, offset + 105);
        PSet_RaiseSP.UpdateData(data, offset + 109);
        PSet_Owner.UpdateData(data, offset + 113);
        XSet_LowerSP.UpdateData(data, offset + 117);
        XSet_RaiseSP.UpdateData(data, offset + 121);
        PCmd_Raise.UpdateData((data[offset + 126] & (1 << 0)) != 0);
        PCmd_Lower.UpdateData((data[offset + 126] & (1 << 1)) != 0);
        PCmd_None.UpdateData((data[offset + 126] & (1 << 2)) != 0);
        PCmd_Auto.UpdateData((data[offset + 126] & (1 << 3)) != 0);
        PCmd_Man.UpdateData((data[offset + 126] & (1 << 4)) != 0);
        PCmd_Reset.UpdateData((data[offset + 126] & (1 << 5)) != 0);
        PCmd_Lock.UpdateData((data[offset + 126] & (1 << 6)) != 0);
        PCmd_Normal.UpdateData((data[offset + 126] & (1 << 7)) != 0);
        PCmd_Oper.UpdateData((data[offset + 127] & (1 << 0)) != 0);
        PCmd_Prog.UpdateData((data[offset + 127] & (1 << 1)) != 0);
        PCmd_Unlock.UpdateData((data[offset + 127] & (1 << 2)) != 0);
        XCmd_Raise.UpdateData((data[offset + 127] & (1 << 3)) != 0);
        XCmd_Lower.UpdateData((data[offset + 127] & (1 << 4)) != 0);
        XCmd_None.UpdateData((data[offset + 127] & (1 << 5)) != 0);
        XCmd_Auto.UpdateData((data[offset + 127] & (1 << 6)) != 0);
        XCmd_Man.UpdateData((data[offset + 127] & (1 << 7)) != 0);
        XCmd_Reset.UpdateData((data[offset + 128] & (1 << 0)) != 0);
        XCmd_ResetAckAll.UpdateData((data[offset + 128] & (1 << 1)) != 0);
        XCmd_Acq.UpdateData((data[offset + 128] & (1 << 2)) != 0);
        XCmd_Rel.UpdateData((data[offset + 128] & (1 << 3)) != 0);
        Out_Raise.UpdateData((data[offset + 128] & (1 << 4)) != 0);
        Out_Lower.UpdateData((data[offset + 128] & (1 << 5)) != 0);
        Out_Q.UpdateData((data[offset + 128] & (1 << 6)) != 0);
        Out_QNot.UpdateData((data[offset + 128] & (1 << 7)) != 0);
        Out_OwnerSts.UpdateData(data, offset + 128);
        Val_PV.UpdateData(data, offset + 132);
        Val_RoC.UpdateData(data, offset + 136);
        Val_LowerSP.UpdateData(data, offset + 140);
        Val_RaiseSP.UpdateData(data, offset + 144);
        Val_PVEUMin.UpdateData(data, offset + 148);
        Val_PVEUMax.UpdateData(data, offset + 152);
        Val_Owner.UpdateData(data, offset + 156);
        SrcQ_IO.UpdateData(data, offset + 160);
        SrcQ.UpdateData(data, offset + 164);
        Sts_Initialized.UpdateData((data[offset + 169] & (1 << 0)) != 0);
        Sts_Raise.UpdateData((data[offset + 169] & (1 << 1)) != 0);
        Sts_Lower.UpdateData((data[offset + 169] & (1 << 2)) != 0);
        Sts_Auto.UpdateData((data[offset + 173] & (1 << 3)) != 0);
        Sts_Man.UpdateData((data[offset + 173] & (1 << 4)) != 0);
        Sts_Q.UpdateData((data[offset + 173] & (1 << 5)) != 0);
        Sts_Available.UpdateData((data[offset + 173] & (1 << 6)) != 0);
        Sts_NotRdy.UpdateData((data[offset + 173] & (1 << 7)) != 0);
        Sts_NrdyCfgErr.UpdateData((data[offset + 174] & (1 << 0)) != 0);
        Sts_NrdyOoS.UpdateData((data[offset + 174] & (1 << 1)) != 0);
        Sts_AlmInh.UpdateData((data[offset + 174] & (1 << 2)) != 0);
        Sts_Err.UpdateData((data[offset + 174] & (1 << 3)) != 0);
        Sts_ErrEU.UpdateData((data[offset + 174] & (1 << 4)) != 0);
        Sts_ErrRateTime.UpdateData((data[offset + 174] & (1 << 5)) != 0);
        Sts_ErrAlm.UpdateData((data[offset + 174] & (1 << 6)) != 0);
        Sts_OoS.UpdateData((data[offset + 174] & (1 << 7)) != 0);
        Sts_Prog.UpdateData((data[offset + 175] & (1 << 0)) != 0);
        Sts_RdyReset.UpdateData((data[offset + 175] & (1 << 1)) != 0);
        Sts_RdyAck.UpdateData((data[offset + 175] & (1 << 2)) != 0);
        Sts_HiDevCmp.UpdateData((data[offset + 175] & (1 << 3)) != 0);
        Sts_HiDevGate.UpdateData((data[offset + 175] & (1 << 4)) != 0);
        Sts_HiDev.UpdateData((data[offset + 175] & (1 << 5)) != 0);
        Sts_LoDevCmp.UpdateData((data[offset + 175] & (1 << 6)) != 0);
        Sts_LoDevGate.UpdateData((data[offset + 175] & (1 << 7)) != 0);
        Sts_LoDev.UpdateData((data[offset + 176] & (1 << 0)) != 0);
        Sts_HiRoCIncrCmp.UpdateData((data[offset + 176] & (1 << 1)) != 0);
        Sts_HiRoCIncrGate.UpdateData((data[offset + 176] & (1 << 2)) != 0);
        Sts_HiRoCIncr.UpdateData((data[offset + 176] & (1 << 3)) != 0);
        Sts_HiRoCDecrCmp.UpdateData((data[offset + 176] & (1 << 4)) != 0);
        Sts_HiRoCDecrGate.UpdateData((data[offset + 176] & (1 << 5)) != 0);
        Sts_HiRoCDecr.UpdateData((data[offset + 176] & (1 << 6)) != 0);
        Sts_eNotify.UpdateData(data, offset + 176);
        Sts_eNotifyAll.UpdateData(data, offset + 177);
        Sts_eNotifyHiDev.UpdateData(data, offset + 178);
        Sts_eNotifyHiRoCDecr.UpdateData(data, offset + 179);
        Sts_eNotifyHiRoCIncr.UpdateData(data, offset + 180);
        Sts_eNotifyLoDev.UpdateData(data, offset + 181);
        Sts_UnackAlmCount.UpdateData(data, offset + 182);
        Sts_Alm.UpdateData((data[offset + 186] & (1 << 7)) != 0);
        Sts_ErrHiDevGateDly.UpdateData((data[offset + 187] & (1 << 0)) != 0);
        Sts_ErrLoDevGateDly.UpdateData((data[offset + 187] & (1 << 1)) != 0);
        Sts_ErrHiRoCIncrGateDly.UpdateData((data[offset + 187] & (1 << 2)) != 0);
        Sts_ErrHiRoCDecrGateDly.UpdateData((data[offset + 187] & (1 << 3)) != 0);
        Sts_Oper.UpdateData((data[offset + 187] & (1 << 4)) != 0);
        Sts_Maint.UpdateData((data[offset + 187] & (1 << 5)) != 0);
        Sts_Ext.UpdateData((data[offset + 187] & (1 << 6)) != 0);
        Sts_Ovrd.UpdateData((data[offset + 187] & (1 << 7)) != 0);
        Sts_eFault.UpdateData(data, offset + 187);
        Sts_eSts.UpdateData(data, offset + 189);
        Sts_bSrc.UpdateData(data, offset + 191);
        Sts_eSrc.UpdateData(data, offset + 193);
        Sts_ExtReqInh.UpdateData((data[offset + 196] & (1 << 0)) != 0);
        Sts_Hand.UpdateData((data[offset + 196] & (1 << 1)) != 0);
        Sts_MAcqRcvd.UpdateData((data[offset + 196] & (1 << 2)) != 0);
        Sts_Normal.UpdateData((data[offset + 196] & (1 << 3)) != 0);
        Sts_OperLocked.UpdateData((data[offset + 196] & (1 << 4)) != 0);
        Sts_ProgLocked.UpdateData((data[offset + 196] & (1 << 5)) != 0);
        Sts_ProgOperLock.UpdateData((data[offset + 196] & (1 << 6)) != 0);
        Sts_ProgOperSel.UpdateData((data[offset + 196] & (1 << 7)) != 0);
        Sts_ProgReqInh.UpdateData((data[offset + 197] & (1 << 0)) != 0);
        XRdy_Acq.UpdateData((data[offset + 197] & (1 << 1)) != 0);
        XRdy_Rel.UpdateData((data[offset + 197] & (1 << 2)) != 0);
        XRdy_Reset.UpdateData((data[offset + 197] & (1 << 3)) != 0);
        XRdy_ResetAckAll.UpdateData((data[offset + 201] & (1 << 4)) != 0);
        XRdy_Auto.UpdateData((data[offset + 201] & (1 << 5)) != 0);
        XRdy_Lower.UpdateData((data[offset + 201] & (1 << 6)) != 0);
        XRdy_Man.UpdateData((data[offset + 201] & (1 << 7)) != 0);
        XRdy_None.UpdateData((data[offset + 202] & (1 << 0)) != 0);
        XRdy_Raise.UpdateData((data[offset + 202] & (1 << 1)) != 0);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_InitializeReq</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Inp_InitializeReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PV</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public REAL Inp_PV
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVSrcQ</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public SINT Inp_PVSrcQ
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVNotify</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public SINT Inp_PVNotify
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVBad</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Inp_PVBad
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_OvrdCmd</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public SINT Inp_OvrdCmd
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_OvrdRaiseSP</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public REAL Inp_OvrdRaiseSP
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_OvrdLowerSP</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public REAL Inp_OvrdLowerSP
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_HiDevGate</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Inp_HiDevGate
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_LoDevGate</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Inp_LoDevGate
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_HiRoCIncrGate</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Inp_HiRoCIncrGate
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_HiRoCDecrGate</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Inp_HiRoCDecrGate
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_OwnerCmd</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public DINT Inp_OwnerCmd
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_ExtInh</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Inp_ExtInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Hand</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Inp_Hand
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Ovrd</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Inp_Ovrd
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PVDecPlcs</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public SINT Cfg_PVDecPlcs
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_SetTrack</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Cfg_SetTrack
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasMoreObj</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Cfg_HasMoreObj
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasPVNav</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Cfg_HasPVNav
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasOutNav</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Cfg_HasOutNav
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PVEUMin</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public REAL Cfg_PVEUMin
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PVEUMax</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public REAL Cfg_PVEUMax
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_SPHiLim</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public REAL Cfg_SPHiLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_SPLoLim</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public REAL Cfg_SPLoLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_RaiseDB</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public REAL Cfg_RaiseDB
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_LowerDB</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public REAL Cfg_LowerDB
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_RateTime</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public REAL Cfg_RateTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HiDevLim</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public REAL Cfg_HiDevLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HiDevDB</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public REAL Cfg_HiDevDB
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_LoDevLim</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public REAL Cfg_LoDevLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_LoDevDB</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public REAL Cfg_LoDevDB
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HiDevGateDly</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public REAL Cfg_HiDevGateDly
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_LoDevGateDly</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public REAL Cfg_LoDevGateDly
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HiRoCIncrLim</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public REAL Cfg_HiRoCIncrLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HiRoCIncrDB</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public REAL Cfg_HiRoCIncrDB
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HiRoCIncrGateDly</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public REAL Cfg_HiRoCIncrGateDly
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HiRoCDecrLim</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public REAL Cfg_HiRoCDecrLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HiRoCDecrDB</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public REAL Cfg_HiRoCDecrDB
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HiRoCDecrGateDly</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public REAL Cfg_HiRoCDecrGateDly
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ExtAcqAsLevel</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Cfg_ExtAcqAsLevel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ExtOverLock</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Cfg_ExtOverLock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasExt</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Cfg_HasExt
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasMaint</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Cfg_HasMaint
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasMaintOoS</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Cfg_HasMaintOoS
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasOper</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Cfg_HasOper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasOperLocked</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Cfg_HasOperLocked
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasProg</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Cfg_HasProg
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasProgLocked</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Cfg_HasProgLocked
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OvrdOverLock</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Cfg_OvrdOverLock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PCmdLockAsLevel</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Cfg_PCmdLockAsLevel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_AllowDisable</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Cfg_AllowDisable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_AllowShelve</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Cfg_AllowShelve
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PCmdPriority</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Cfg_PCmdPriority
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PCmdProgAsLevel</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Cfg_PCmdProgAsLevel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ProgNormal</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Cfg_ProgNormal
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ProgPwrUp</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Cfg_ProgPwrUp
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CnfrmReqd</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public SINT Cfg_CnfrmReqd
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PSet_LowerSP</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public REAL PSet_LowerSP
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PSet_RaiseSP</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public REAL PSet_RaiseSP
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PSet_Owner</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public DINT PSet_Owner
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XSet_LowerSP</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public REAL XSet_LowerSP
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XSet_RaiseSP</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public REAL XSet_RaiseSP
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Raise</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL PCmd_Raise
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Lower</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL PCmd_Lower
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_None</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL PCmd_None
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Auto</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL PCmd_Auto
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Man</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL PCmd_Man
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Reset</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL PCmd_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Lock</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL PCmd_Lock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Normal</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL PCmd_Normal
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Oper</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL PCmd_Oper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Prog</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL PCmd_Prog
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Unlock</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL PCmd_Unlock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Raise</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL XCmd_Raise
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Lower</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL XCmd_Lower
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_None</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL XCmd_None
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Auto</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL XCmd_Auto
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Man</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL XCmd_Man
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Reset</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL XCmd_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_ResetAckAll</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL XCmd_ResetAckAll
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Acq</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL XCmd_Acq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Rel</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL XCmd_Rel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_Raise</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Out_Raise
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_Lower</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Out_Lower
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_Q</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Out_Q
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_QNot</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Out_QNot
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_OwnerSts</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public DINT Out_OwnerSts
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_PV</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public REAL Val_PV
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_RoC</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public REAL Val_RoC
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_LowerSP</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public REAL Val_LowerSP
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_RaiseSP</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public REAL Val_RaiseSP
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_PVEUMin</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public REAL Val_PVEUMin
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_PVEUMax</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public REAL Val_PVEUMax
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_Owner</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public DINT Val_Owner
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SrcQ_IO</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public DINT SrcQ_IO
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SrcQ</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public DINT SrcQ
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Initialized</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Sts_Initialized
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Raise</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Sts_Raise
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Lower</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Sts_Lower
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Auto</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Sts_Auto
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Man</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Sts_Man
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Q</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Sts_Q
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Available</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Sts_Available
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NotRdy</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Sts_NotRdy
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyCfgErr</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Sts_NrdyCfgErr
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyOoS</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Sts_NrdyOoS
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_AlmInh</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Sts_AlmInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Err</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Sts_Err
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrEU</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Sts_ErrEU
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrRateTime</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Sts_ErrRateTime
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrAlm</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Sts_ErrAlm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_OoS</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Sts_OoS
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Prog</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Sts_Prog
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_RdyReset</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Sts_RdyReset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_RdyAck</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Sts_RdyAck
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_HiDevCmp</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Sts_HiDevCmp
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_HiDevGate</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Sts_HiDevGate
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_HiDev</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Sts_HiDev
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_LoDevCmp</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Sts_LoDevCmp
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_LoDevGate</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Sts_LoDevGate
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_LoDev</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Sts_LoDev
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_HiRoCIncrCmp</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Sts_HiRoCIncrCmp
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_HiRoCIncrGate</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Sts_HiRoCIncrGate
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_HiRoCIncr</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Sts_HiRoCIncr
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_HiRoCDecrCmp</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Sts_HiRoCDecrCmp
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_HiRoCDecrGate</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Sts_HiRoCDecrGate
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_HiRoCDecr</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Sts_HiRoCDecr
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotify</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public SINT Sts_eNotify
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyAll</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public SINT Sts_eNotifyAll
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyHiDev</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public SINT Sts_eNotifyHiDev
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyHiRoCDecr</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public SINT Sts_eNotifyHiRoCDecr
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyHiRoCIncr</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public SINT Sts_eNotifyHiRoCIncr
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyLoDev</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public SINT Sts_eNotifyLoDev
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_UnackAlmCount</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public DINT Sts_UnackAlmCount
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Alm</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Sts_Alm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrHiDevGateDly</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Sts_ErrHiDevGateDly
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrLoDevGateDly</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Sts_ErrLoDevGateDly
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrHiRoCIncrGateDly</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Sts_ErrHiRoCIncrGateDly
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrHiRoCDecrGateDly</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Sts_ErrHiRoCDecrGateDly
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Oper</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Sts_Oper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Maint</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Sts_Maint
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Ext</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Sts_Ext
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Ovrd</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Sts_Ovrd
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eFault</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public INT Sts_eFault
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eSts</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public INT Sts_eSts
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_bSrc</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public INT Sts_bSrc
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eSrc</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public INT Sts_eSrc
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ExtReqInh</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Sts_ExtReqInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Hand</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Sts_Hand
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_MAcqRcvd</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Sts_MAcqRcvd
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Normal</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Sts_Normal
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_OperLocked</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Sts_OperLocked
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ProgLocked</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Sts_ProgLocked
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ProgOperLock</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Sts_ProgOperLock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ProgOperSel</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Sts_ProgOperSel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ProgReqInh</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL Sts_ProgReqInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Acq</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL XRdy_Acq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Rel</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL XRdy_Rel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Reset</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL XRdy_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_ResetAckAll</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL XRdy_ResetAckAll
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Auto</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL XRdy_Auto
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Lower</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL XRdy_Lower
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Man</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL XRdy_Man
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_None</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL XRdy_None
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Raise</c> member of the <see cref="P_DEADBAND"/> data type.
    /// </summary>
    public BOOL XRdy_Raise
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}