using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>P_DISCRETE_4STATE</c> data type structure.
/// </summary>
[LogixData("P_DISCRETE_4STATE")]
public sealed partial class P_DISCRETE_4STATE : StructureData
{
    /// <summary>
    /// Creates a new <see cref="P_DISCRETE_4STATE"/> instance initialized with default values.
    /// </summary>
    public P_DISCRETE_4STATE() : base("P_DISCRETE_4STATE")
    {
        EnableIn = new BOOL();
        EnableOut = new BOOL();
        Inp_InitializeReq = new BOOL();
        Inp_OwnerCmd = new DINT();
        Inp_AFdbkData = new BOOL();
        Inp_BFdbkData = new BOOL();
        Inp_CFdbkData = new BOOL();
        Inp_DFdbkData = new BOOL();
        Inp_EqpFaultData = new BOOL();
        Inp_IOFault = new BOOL();
        Inp_St0PermOK = new BOOL();
        Inp_St0NBPermOK = new BOOL();
        Inp_St1PermOK = new BOOL();
        Inp_St1NBPermOK = new BOOL();
        Inp_St2PermOK = new BOOL();
        Inp_St2NBPermOK = new BOOL();
        Inp_St3PermOK = new BOOL();
        Inp_St3NBPermOK = new BOOL();
        Inp_IntlkOK = new BOOL();
        Inp_NBIntlkOK = new BOOL();
        Inp_IntlkAvailable = new BOOL();
        Inp_IntlkTripInh = new BOOL();
        Inp_RdyReset = new BOOL();
        Inp_Hand = new BOOL();
        Inp_Ovrd = new BOOL();
        Inp_OvrdCmd = new SINT();
        Inp_ExtInh = new BOOL();
        Inp_HornInh = new BOOL();
        Inp_Reset = new BOOL();
        Cfg_AllowDisable = new BOOL();
        Cfg_AllowShelve = new BOOL();
        Cfg_NumStates = new SINT();
        Cfg_bSt0OutWrite = new SINT();
        Cfg_bSt0OutState = new SINT();
        Cfg_bSt1OutWrite = new SINT();
        Cfg_bSt1OutState = new SINT();
        Cfg_bSt2OutWrite = new SINT();
        Cfg_bSt2OutState = new SINT();
        Cfg_bSt3OutWrite = new SINT();
        Cfg_bSt3OutState = new SINT();
        Cfg_bSt0FdbkCheck = new SINT();
        Cfg_bSt0FdbkState = new SINT();
        Cfg_bSt1FdbkCheck = new SINT();
        Cfg_bSt1FdbkState = new SINT();
        Cfg_bSt2FdbkCheck = new SINT();
        Cfg_bSt2FdbkState = new SINT();
        Cfg_bSt3FdbkCheck = new SINT();
        Cfg_bSt3FdbkState = new SINT();
        Cfg_ePwrUpState = new SINT();
        Cfg_St0onShed = new BOOL();
        Cfg_HasSt0PermObj = new BOOL();
        Cfg_HasSt1PermObj = new BOOL();
        Cfg_HasSt2PermObj = new BOOL();
        Cfg_HasSt3PermObj = new BOOL();
        Cfg_HasIntlkObj = new BOOL();
        Cfg_HasMoreObj = new BOOL();
        Cfg_OperSt0Prio = new BOOL();
        Cfg_ExtSt0Prio = new BOOL();
        Cfg_OCmdResets = new BOOL();
        Cfg_XCmdResets = new BOOL();
        Cfg_OvrdPermIntlk = new BOOL();
        Cfg_ShedOnFail = new BOOL();
        Cfg_ShedOnIOFault = new BOOL();
        Cfg_ShedOnEqpFault = new BOOL();
        Cfg_HornOnChange = new BOOL();
        Cfg_HasOper = new BOOL();
        Cfg_HasOperLocked = new BOOL();
        Cfg_HasProg = new BOOL();
        Cfg_HasProgLocked = new BOOL();
        Cfg_HasExt = new BOOL();
        Cfg_HasMaint = new BOOL();
        Cfg_HasMaintOoS = new BOOL();
        Cfg_OvrdOverLock = new BOOL();
        Cfg_ExtOverLock = new BOOL();
        Cfg_ProgPwrUp = new BOOL();
        Cfg_ProgNormal = new BOOL();
        Cfg_PCmdPriority = new BOOL();
        Cfg_PCmdProgAsLevel = new BOOL();
        Cfg_PCmdLockAsLevel = new BOOL();
        Cfg_ExtAcqAsLevel = new BOOL();
        Cfg_OutAPulseTime = new REAL();
        Cfg_OutBPulseTime = new REAL();
        Cfg_OutCPulseTime = new REAL();
        Cfg_OutDPulseTime = new REAL();
        Cfg_StartHornTime = new REAL();
        Cfg_VirtualFdbkTime = new REAL();
        Cfg_FailTime = new REAL();
        Cfg_CnfrmReqd = new SINT();
        PSet_Owner = new DINT();
        PCmd_Virtual = new BOOL();
        PCmd_Physical = new BOOL();
        PCmd_St0 = new BOOL();
        PCmd_St1 = new BOOL();
        PCmd_St2 = new BOOL();
        PCmd_St3 = new BOOL();
        PCmd_Reset = new BOOL();
        PCmd_Prog = new BOOL();
        PCmd_Oper = new BOOL();
        PCmd_Lock = new BOOL();
        PCmd_Unlock = new BOOL();
        PCmd_Normal = new BOOL();
        XCmd_St0 = new BOOL();
        XCmd_St1 = new BOOL();
        XCmd_St2 = new BOOL();
        XCmd_St3 = new BOOL();
        XCmd_Acq = new BOOL();
        XCmd_Rel = new BOOL();
        XCmd_Reset = new BOOL();
        XCmd_ResetAckAll = new BOOL();
        Out_AData = new BOOL();
        Out_BData = new BOOL();
        Out_CData = new BOOL();
        Out_DData = new BOOL();
        Out_HornData = new BOOL();
        Out_Reset = new BOOL();
        Out_OwnerSts = new DINT();
        Sts_Initialized = new BOOL();
        Sts_St0 = new BOOL();
        Sts_St1 = new BOOL();
        Sts_St2 = new BOOL();
        Sts_St3 = new BOOL();
        Sts_Moving = new BOOL();
        Sts_Horn = new BOOL();
        Sts_Virtual = new BOOL();
        SrcQ_IO = new SINT();
        SrcQ = new SINT();
        Sts_eFdbk = new SINT();
        Sts_eCmd = new SINT();
        Sts_eSts = new SINT();
        Sts_eFault = new SINT();
        Sts_eOutState = new SINT();
        Sts_eNotify = new SINT();
        Sts_eNotifyAll = new SINT();
        Sts_eNotifyIOFault = new SINT();
        Sts_eNotifyFail = new SINT();
        Sts_eNotifyIntlkTrip = new SINT();
        Sts_eNotifyEqpFault = new SINT();
        Sts_UnackAlmCount = new DINT();
        Sts_eSrc = new INT();
        Sts_bSrc = new INT();
        Sts_Available = new BOOL();
        Sts_IntlkAvailable = new BOOL();
        Sts_Bypass = new BOOL();
        Sts_BypActive = new BOOL();
        Sts_MaintByp = new BOOL();
        Sts_NotRdy = new BOOL();
        Sts_NrdyCfgErr = new BOOL();
        Sts_NrdyEqpFault = new BOOL();
        Sts_NrdyFail = new BOOL();
        Sts_NrdyIntlk = new BOOL();
        Sts_NrdyIOFault = new BOOL();
        Sts_NrdyOoS = new BOOL();
        Sts_NrdyPerm = new BOOL();
        Sts_NrdyPrioSt0 = new BOOL();
        Sts_Err = new BOOL();
        Sts_ErrAlm = new BOOL();
        Sts_ErrOutAPulseTime = new BOOL();
        Sts_ErrOutBPulseTime = new BOOL();
        Sts_ErrOutCPulseTime = new BOOL();
        Sts_ErrOutDPulseTime = new BOOL();
        Sts_ErrVirtualFdbkTime = new BOOL();
        Sts_ErrFailTime = new BOOL();
        Sts_Hand = new BOOL();
        Sts_OoS = new BOOL();
        Sts_Maint = new BOOL();
        Sts_Ovrd = new BOOL();
        Sts_Ext = new BOOL();
        Sts_Prog = new BOOL();
        Sts_ProgLocked = new BOOL();
        Sts_Oper = new BOOL();
        Sts_OperLocked = new BOOL();
        Sts_ProgOperSel = new BOOL();
        Sts_ProgOperLock = new BOOL();
        Sts_Normal = new BOOL();
        Sts_ExtReqInh = new BOOL();
        Sts_ProgReqInh = new BOOL();
        Sts_MAcqRcvd = new BOOL();
        Sts_CmdConflict = new BOOL();
        Sts_Alm = new BOOL();
        Sts_AlmInh = new BOOL();
        Sts_IOFault = new BOOL();
        Sts_Fail = new BOOL();
        Sts_IntlkTrip = new BOOL();
        Sts_EqpFault = new BOOL();
        Sts_RdyAck = new BOOL();
        Sts_RdyReset = new BOOL();
        XRdy_Acq = new BOOL();
        XRdy_Rel = new BOOL();
        XRdy_St0 = new BOOL();
        XRdy_St1 = new BOOL();
        XRdy_St2 = new BOOL();
        XRdy_St3 = new BOOL();
        XRdy_Reset = new BOOL();
        XRdy_ResetAckAll = new BOOL();
        Val_Owner = new DINT();
    }
    
    /// <summary>
    /// Creates a new <see cref="P_DISCRETE_4STATE"/> instance initialized with the provided element.
    /// </summary>
    public P_DISCRETE_4STATE(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 284;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        EnableIn.UpdateData((data[offset + 5] & (1 << 0)) != 0);
        EnableOut.UpdateData((data[offset + 5] & (1 << 1)) != 0);
        Inp_InitializeReq.UpdateData((data[offset + 5] & (1 << 2)) != 0);
        Inp_OwnerCmd.UpdateData(data, offset + 5);
        Inp_AFdbkData.UpdateData((data[offset + 9] & (1 << 3)) != 0);
        Inp_BFdbkData.UpdateData((data[offset + 9] & (1 << 4)) != 0);
        Inp_CFdbkData.UpdateData((data[offset + 9] & (1 << 5)) != 0);
        Inp_DFdbkData.UpdateData((data[offset + 9] & (1 << 6)) != 0);
        Inp_EqpFaultData.UpdateData((data[offset + 9] & (1 << 7)) != 0);
        Inp_IOFault.UpdateData((data[offset + 10] & (1 << 0)) != 0);
        Inp_St0PermOK.UpdateData((data[offset + 10] & (1 << 1)) != 0);
        Inp_St0NBPermOK.UpdateData((data[offset + 10] & (1 << 2)) != 0);
        Inp_St1PermOK.UpdateData((data[offset + 10] & (1 << 3)) != 0);
        Inp_St1NBPermOK.UpdateData((data[offset + 10] & (1 << 4)) != 0);
        Inp_St2PermOK.UpdateData((data[offset + 10] & (1 << 5)) != 0);
        Inp_St2NBPermOK.UpdateData((data[offset + 10] & (1 << 6)) != 0);
        Inp_St3PermOK.UpdateData((data[offset + 10] & (1 << 7)) != 0);
        Inp_St3NBPermOK.UpdateData((data[offset + 11] & (1 << 0)) != 0);
        Inp_IntlkOK.UpdateData((data[offset + 11] & (1 << 1)) != 0);
        Inp_NBIntlkOK.UpdateData((data[offset + 11] & (1 << 2)) != 0);
        Inp_IntlkAvailable.UpdateData((data[offset + 11] & (1 << 3)) != 0);
        Inp_IntlkTripInh.UpdateData((data[offset + 11] & (1 << 4)) != 0);
        Inp_RdyReset.UpdateData((data[offset + 11] & (1 << 5)) != 0);
        Inp_Hand.UpdateData((data[offset + 11] & (1 << 6)) != 0);
        Inp_Ovrd.UpdateData((data[offset + 11] & (1 << 7)) != 0);
        Inp_OvrdCmd.UpdateData(data, offset + 11);
        Inp_ExtInh.UpdateData((data[offset + 13] & (1 << 0)) != 0);
        Inp_HornInh.UpdateData((data[offset + 13] & (1 << 1)) != 0);
        Inp_Reset.UpdateData((data[offset + 13] & (1 << 2)) != 0);
        Cfg_AllowDisable.UpdateData((data[offset + 13] & (1 << 3)) != 0);
        Cfg_AllowShelve.UpdateData((data[offset + 13] & (1 << 4)) != 0);
        Cfg_NumStates.UpdateData(data, offset + 13);
        Cfg_bSt0OutWrite.UpdateData(data, offset + 14);
        Cfg_bSt0OutState.UpdateData(data, offset + 15);
        Cfg_bSt1OutWrite.UpdateData(data, offset + 16);
        Cfg_bSt1OutState.UpdateData(data, offset + 17);
        Cfg_bSt2OutWrite.UpdateData(data, offset + 18);
        Cfg_bSt2OutState.UpdateData(data, offset + 19);
        Cfg_bSt3OutWrite.UpdateData(data, offset + 20);
        Cfg_bSt3OutState.UpdateData(data, offset + 21);
        Cfg_bSt0FdbkCheck.UpdateData(data, offset + 22);
        Cfg_bSt0FdbkState.UpdateData(data, offset + 23);
        Cfg_bSt1FdbkCheck.UpdateData(data, offset + 24);
        Cfg_bSt1FdbkState.UpdateData(data, offset + 25);
        Cfg_bSt2FdbkCheck.UpdateData(data, offset + 26);
        Cfg_bSt2FdbkState.UpdateData(data, offset + 27);
        Cfg_bSt3FdbkCheck.UpdateData(data, offset + 28);
        Cfg_bSt3FdbkState.UpdateData(data, offset + 29);
        Cfg_ePwrUpState.UpdateData(data, offset + 30);
        Cfg_St0onShed.UpdateData((data[offset + 31] & (1 << 5)) != 0);
        Cfg_HasSt0PermObj.UpdateData((data[offset + 31] & (1 << 6)) != 0);
        Cfg_HasSt1PermObj.UpdateData((data[offset + 31] & (1 << 7)) != 0);
        Cfg_HasSt2PermObj.UpdateData((data[offset + 32] & (1 << 0)) != 0);
        Cfg_HasSt3PermObj.UpdateData((data[offset + 32] & (1 << 1)) != 0);
        Cfg_HasIntlkObj.UpdateData((data[offset + 32] & (1 << 2)) != 0);
        Cfg_HasMoreObj.UpdateData((data[offset + 36] & (1 << 3)) != 0);
        Cfg_OperSt0Prio.UpdateData((data[offset + 36] & (1 << 4)) != 0);
        Cfg_ExtSt0Prio.UpdateData((data[offset + 36] & (1 << 5)) != 0);
        Cfg_OCmdResets.UpdateData((data[offset + 36] & (1 << 6)) != 0);
        Cfg_XCmdResets.UpdateData((data[offset + 36] & (1 << 7)) != 0);
        Cfg_OvrdPermIntlk.UpdateData((data[offset + 37] & (1 << 0)) != 0);
        Cfg_ShedOnFail.UpdateData((data[offset + 37] & (1 << 1)) != 0);
        Cfg_ShedOnIOFault.UpdateData((data[offset + 37] & (1 << 2)) != 0);
        Cfg_ShedOnEqpFault.UpdateData((data[offset + 37] & (1 << 3)) != 0);
        Cfg_HornOnChange.UpdateData((data[offset + 37] & (1 << 4)) != 0);
        Cfg_HasOper.UpdateData((data[offset + 37] & (1 << 5)) != 0);
        Cfg_HasOperLocked.UpdateData((data[offset + 37] & (1 << 6)) != 0);
        Cfg_HasProg.UpdateData((data[offset + 37] & (1 << 7)) != 0);
        Cfg_HasProgLocked.UpdateData((data[offset + 38] & (1 << 0)) != 0);
        Cfg_HasExt.UpdateData((data[offset + 38] & (1 << 1)) != 0);
        Cfg_HasMaint.UpdateData((data[offset + 38] & (1 << 2)) != 0);
        Cfg_HasMaintOoS.UpdateData((data[offset + 38] & (1 << 3)) != 0);
        Cfg_OvrdOverLock.UpdateData((data[offset + 38] & (1 << 4)) != 0);
        Cfg_ExtOverLock.UpdateData((data[offset + 38] & (1 << 5)) != 0);
        Cfg_ProgPwrUp.UpdateData((data[offset + 38] & (1 << 6)) != 0);
        Cfg_ProgNormal.UpdateData((data[offset + 38] & (1 << 7)) != 0);
        Cfg_PCmdPriority.UpdateData((data[offset + 39] & (1 << 0)) != 0);
        Cfg_PCmdProgAsLevel.UpdateData((data[offset + 39] & (1 << 1)) != 0);
        Cfg_PCmdLockAsLevel.UpdateData((data[offset + 39] & (1 << 2)) != 0);
        Cfg_ExtAcqAsLevel.UpdateData((data[offset + 39] & (1 << 3)) != 0);
        Cfg_OutAPulseTime.UpdateData(data, offset + 39);
        Cfg_OutBPulseTime.UpdateData(data, offset + 43);
        Cfg_OutCPulseTime.UpdateData(data, offset + 47);
        Cfg_OutDPulseTime.UpdateData(data, offset + 51);
        Cfg_StartHornTime.UpdateData(data, offset + 55);
        Cfg_VirtualFdbkTime.UpdateData(data, offset + 59);
        Cfg_FailTime.UpdateData(data, offset + 63);
        Cfg_CnfrmReqd.UpdateData(data, offset + 67);
        PSet_Owner.UpdateData(data, offset + 68);
        PCmd_Virtual.UpdateData((data[offset + 72] & (1 << 4)) != 0);
        PCmd_Physical.UpdateData((data[offset + 72] & (1 << 5)) != 0);
        PCmd_St0.UpdateData((data[offset + 72] & (1 << 6)) != 0);
        PCmd_St1.UpdateData((data[offset + 72] & (1 << 7)) != 0);
        PCmd_St2.UpdateData((data[offset + 73] & (1 << 0)) != 0);
        PCmd_St3.UpdateData((data[offset + 73] & (1 << 1)) != 0);
        PCmd_Reset.UpdateData((data[offset + 73] & (1 << 2)) != 0);
        PCmd_Prog.UpdateData((data[offset + 73] & (1 << 3)) != 0);
        PCmd_Oper.UpdateData((data[offset + 73] & (1 << 4)) != 0);
        PCmd_Lock.UpdateData((data[offset + 73] & (1 << 5)) != 0);
        PCmd_Unlock.UpdateData((data[offset + 73] & (1 << 6)) != 0);
        PCmd_Normal.UpdateData((data[offset + 73] & (1 << 7)) != 0);
        XCmd_St0.UpdateData((data[offset + 74] & (1 << 0)) != 0);
        XCmd_St1.UpdateData((data[offset + 74] & (1 << 1)) != 0);
        XCmd_St2.UpdateData((data[offset + 74] & (1 << 2)) != 0);
        XCmd_St3.UpdateData((data[offset + 74] & (1 << 3)) != 0);
        XCmd_Acq.UpdateData((data[offset + 74] & (1 << 4)) != 0);
        XCmd_Rel.UpdateData((data[offset + 74] & (1 << 5)) != 0);
        XCmd_Reset.UpdateData((data[offset + 74] & (1 << 6)) != 0);
        XCmd_ResetAckAll.UpdateData((data[offset + 74] & (1 << 7)) != 0);
        Out_AData.UpdateData((data[offset + 75] & (1 << 0)) != 0);
        Out_BData.UpdateData((data[offset + 75] & (1 << 1)) != 0);
        Out_CData.UpdateData((data[offset + 75] & (1 << 2)) != 0);
        Out_DData.UpdateData((data[offset + 75] & (1 << 3)) != 0);
        Out_HornData.UpdateData((data[offset + 75] & (1 << 4)) != 0);
        Out_Reset.UpdateData((data[offset + 75] & (1 << 5)) != 0);
        Out_OwnerSts.UpdateData(data, offset + 75);
        Sts_Initialized.UpdateData((data[offset + 79] & (1 << 6)) != 0);
        Sts_St0.UpdateData((data[offset + 79] & (1 << 7)) != 0);
        Sts_St1.UpdateData((data[offset + 80] & (1 << 0)) != 0);
        Sts_St2.UpdateData((data[offset + 84] & (1 << 1)) != 0);
        Sts_St3.UpdateData((data[offset + 84] & (1 << 2)) != 0);
        Sts_Moving.UpdateData((data[offset + 84] & (1 << 3)) != 0);
        Sts_Horn.UpdateData((data[offset + 84] & (1 << 4)) != 0);
        Sts_Virtual.UpdateData((data[offset + 84] & (1 << 5)) != 0);
        SrcQ_IO.UpdateData(data, offset + 84);
        SrcQ.UpdateData(data, offset + 85);
        Sts_eFdbk.UpdateData(data, offset + 86);
        Sts_eCmd.UpdateData(data, offset + 87);
        Sts_eSts.UpdateData(data, offset + 88);
        Sts_eFault.UpdateData(data, offset + 89);
        Sts_eOutState.UpdateData(data, offset + 90);
        Sts_eNotify.UpdateData(data, offset + 91);
        Sts_eNotifyAll.UpdateData(data, offset + 92);
        Sts_eNotifyIOFault.UpdateData(data, offset + 93);
        Sts_eNotifyFail.UpdateData(data, offset + 94);
        Sts_eNotifyIntlkTrip.UpdateData(data, offset + 95);
        Sts_eNotifyEqpFault.UpdateData(data, offset + 96);
        Sts_UnackAlmCount.UpdateData(data, offset + 97);
        Sts_eSrc.UpdateData(data, offset + 101);
        Sts_bSrc.UpdateData(data, offset + 103);
        Sts_Available.UpdateData((data[offset + 105] & (1 << 6)) != 0);
        Sts_IntlkAvailable.UpdateData((data[offset + 105] & (1 << 7)) != 0);
        Sts_Bypass.UpdateData((data[offset + 106] & (1 << 0)) != 0);
        Sts_BypActive.UpdateData((data[offset + 106] & (1 << 1)) != 0);
        Sts_MaintByp.UpdateData((data[offset + 106] & (1 << 2)) != 0);
        Sts_NotRdy.UpdateData((data[offset + 106] & (1 << 3)) != 0);
        Sts_NrdyCfgErr.UpdateData((data[offset + 106] & (1 << 4)) != 0);
        Sts_NrdyEqpFault.UpdateData((data[offset + 106] & (1 << 5)) != 0);
        Sts_NrdyFail.UpdateData((data[offset + 106] & (1 << 6)) != 0);
        Sts_NrdyIntlk.UpdateData((data[offset + 106] & (1 << 7)) != 0);
        Sts_NrdyIOFault.UpdateData((data[offset + 107] & (1 << 0)) != 0);
        Sts_NrdyOoS.UpdateData((data[offset + 107] & (1 << 1)) != 0);
        Sts_NrdyPerm.UpdateData((data[offset + 107] & (1 << 2)) != 0);
        Sts_NrdyPrioSt0.UpdateData((data[offset + 107] & (1 << 3)) != 0);
        Sts_Err.UpdateData((data[offset + 107] & (1 << 4)) != 0);
        Sts_ErrAlm.UpdateData((data[offset + 107] & (1 << 5)) != 0);
        Sts_ErrOutAPulseTime.UpdateData((data[offset + 107] & (1 << 6)) != 0);
        Sts_ErrOutBPulseTime.UpdateData((data[offset + 107] & (1 << 7)) != 0);
        Sts_ErrOutCPulseTime.UpdateData((data[offset + 108] & (1 << 0)) != 0);
        Sts_ErrOutDPulseTime.UpdateData((data[offset + 108] & (1 << 1)) != 0);
        Sts_ErrVirtualFdbkTime.UpdateData((data[offset + 108] & (1 << 2)) != 0);
        Sts_ErrFailTime.UpdateData((data[offset + 108] & (1 << 3)) != 0);
        Sts_Hand.UpdateData((data[offset + 108] & (1 << 4)) != 0);
        Sts_OoS.UpdateData((data[offset + 108] & (1 << 5)) != 0);
        Sts_Maint.UpdateData((data[offset + 108] & (1 << 6)) != 0);
        Sts_Ovrd.UpdateData((data[offset + 108] & (1 << 7)) != 0);
        Sts_Ext.UpdateData((data[offset + 109] & (1 << 0)) != 0);
        Sts_Prog.UpdateData((data[offset + 109] & (1 << 1)) != 0);
        Sts_ProgLocked.UpdateData((data[offset + 109] & (1 << 2)) != 0);
        Sts_Oper.UpdateData((data[offset + 109] & (1 << 3)) != 0);
        Sts_OperLocked.UpdateData((data[offset + 109] & (1 << 4)) != 0);
        Sts_ProgOperSel.UpdateData((data[offset + 109] & (1 << 5)) != 0);
        Sts_ProgOperLock.UpdateData((data[offset + 109] & (1 << 6)) != 0);
        Sts_Normal.UpdateData((data[offset + 109] & (1 << 7)) != 0);
        Sts_ExtReqInh.UpdateData((data[offset + 110] & (1 << 0)) != 0);
        Sts_ProgReqInh.UpdateData((data[offset + 110] & (1 << 1)) != 0);
        Sts_MAcqRcvd.UpdateData((data[offset + 110] & (1 << 2)) != 0);
        Sts_CmdConflict.UpdateData((data[offset + 110] & (1 << 3)) != 0);
        Sts_Alm.UpdateData((data[offset + 110] & (1 << 4)) != 0);
        Sts_AlmInh.UpdateData((data[offset + 110] & (1 << 5)) != 0);
        Sts_IOFault.UpdateData((data[offset + 110] & (1 << 6)) != 0);
        Sts_Fail.UpdateData((data[offset + 110] & (1 << 7)) != 0);
        Sts_IntlkTrip.UpdateData((data[offset + 115] & (1 << 0)) != 0);
        Sts_EqpFault.UpdateData((data[offset + 115] & (1 << 1)) != 0);
        Sts_RdyAck.UpdateData((data[offset + 115] & (1 << 2)) != 0);
        Sts_RdyReset.UpdateData((data[offset + 115] & (1 << 3)) != 0);
        XRdy_Acq.UpdateData((data[offset + 115] & (1 << 4)) != 0);
        XRdy_Rel.UpdateData((data[offset + 115] & (1 << 5)) != 0);
        XRdy_St0.UpdateData((data[offset + 115] & (1 << 6)) != 0);
        XRdy_St1.UpdateData((data[offset + 115] & (1 << 7)) != 0);
        XRdy_St2.UpdateData((data[offset + 116] & (1 << 0)) != 0);
        XRdy_St3.UpdateData((data[offset + 116] & (1 << 1)) != 0);
        XRdy_Reset.UpdateData((data[offset + 116] & (1 << 2)) != 0);
        XRdy_ResetAckAll.UpdateData((data[offset + 116] & (1 << 3)) != 0);
        Val_Owner.UpdateData(data, offset + 116);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_InitializeReq</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Inp_InitializeReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_OwnerCmd</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public DINT Inp_OwnerCmd
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_AFdbkData</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Inp_AFdbkData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_BFdbkData</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Inp_BFdbkData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_CFdbkData</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Inp_CFdbkData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_DFdbkData</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Inp_DFdbkData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_EqpFaultData</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Inp_EqpFaultData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_IOFault</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Inp_IOFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_St0PermOK</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Inp_St0PermOK
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_St0NBPermOK</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Inp_St0NBPermOK
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_St1PermOK</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Inp_St1PermOK
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_St1NBPermOK</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Inp_St1NBPermOK
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_St2PermOK</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Inp_St2PermOK
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_St2NBPermOK</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Inp_St2NBPermOK
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_St3PermOK</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Inp_St3PermOK
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_St3NBPermOK</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Inp_St3NBPermOK
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_IntlkOK</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Inp_IntlkOK
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_NBIntlkOK</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Inp_NBIntlkOK
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_IntlkAvailable</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Inp_IntlkAvailable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_IntlkTripInh</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Inp_IntlkTripInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_RdyReset</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Inp_RdyReset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Hand</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Inp_Hand
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Ovrd</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Inp_Ovrd
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_OvrdCmd</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public SINT Inp_OvrdCmd
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_ExtInh</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Inp_ExtInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_HornInh</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Inp_HornInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Reset</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Inp_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_AllowDisable</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Cfg_AllowDisable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_AllowShelve</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Cfg_AllowShelve
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_NumStates</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public SINT Cfg_NumStates
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_bSt0OutWrite</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public SINT Cfg_bSt0OutWrite
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_bSt0OutState</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public SINT Cfg_bSt0OutState
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_bSt1OutWrite</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public SINT Cfg_bSt1OutWrite
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_bSt1OutState</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public SINT Cfg_bSt1OutState
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_bSt2OutWrite</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public SINT Cfg_bSt2OutWrite
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_bSt2OutState</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public SINT Cfg_bSt2OutState
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_bSt3OutWrite</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public SINT Cfg_bSt3OutWrite
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_bSt3OutState</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public SINT Cfg_bSt3OutState
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_bSt0FdbkCheck</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public SINT Cfg_bSt0FdbkCheck
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_bSt0FdbkState</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public SINT Cfg_bSt0FdbkState
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_bSt1FdbkCheck</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public SINT Cfg_bSt1FdbkCheck
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_bSt1FdbkState</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public SINT Cfg_bSt1FdbkState
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_bSt2FdbkCheck</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public SINT Cfg_bSt2FdbkCheck
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_bSt2FdbkState</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public SINT Cfg_bSt2FdbkState
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_bSt3FdbkCheck</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public SINT Cfg_bSt3FdbkCheck
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_bSt3FdbkState</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public SINT Cfg_bSt3FdbkState
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ePwrUpState</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public SINT Cfg_ePwrUpState
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_St0onShed</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Cfg_St0onShed
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasSt0PermObj</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Cfg_HasSt0PermObj
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasSt1PermObj</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Cfg_HasSt1PermObj
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasSt2PermObj</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Cfg_HasSt2PermObj
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasSt3PermObj</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Cfg_HasSt3PermObj
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasIntlkObj</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Cfg_HasIntlkObj
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasMoreObj</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Cfg_HasMoreObj
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OperSt0Prio</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Cfg_OperSt0Prio
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ExtSt0Prio</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Cfg_ExtSt0Prio
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OCmdResets</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Cfg_OCmdResets
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_XCmdResets</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Cfg_XCmdResets
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OvrdPermIntlk</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Cfg_OvrdPermIntlk
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ShedOnFail</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Cfg_ShedOnFail
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ShedOnIOFault</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Cfg_ShedOnIOFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ShedOnEqpFault</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Cfg_ShedOnEqpFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HornOnChange</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Cfg_HornOnChange
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasOper</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Cfg_HasOper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasOperLocked</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Cfg_HasOperLocked
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasProg</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Cfg_HasProg
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasProgLocked</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Cfg_HasProgLocked
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasExt</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Cfg_HasExt
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasMaint</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Cfg_HasMaint
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasMaintOoS</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Cfg_HasMaintOoS
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OvrdOverLock</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Cfg_OvrdOverLock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ExtOverLock</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Cfg_ExtOverLock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ProgPwrUp</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Cfg_ProgPwrUp
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ProgNormal</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Cfg_ProgNormal
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PCmdPriority</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Cfg_PCmdPriority
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PCmdProgAsLevel</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Cfg_PCmdProgAsLevel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PCmdLockAsLevel</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Cfg_PCmdLockAsLevel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ExtAcqAsLevel</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Cfg_ExtAcqAsLevel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OutAPulseTime</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public REAL Cfg_OutAPulseTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OutBPulseTime</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public REAL Cfg_OutBPulseTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OutCPulseTime</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public REAL Cfg_OutCPulseTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OutDPulseTime</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public REAL Cfg_OutDPulseTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_StartHornTime</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public REAL Cfg_StartHornTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_VirtualFdbkTime</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public REAL Cfg_VirtualFdbkTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_FailTime</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public REAL Cfg_FailTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CnfrmReqd</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public SINT Cfg_CnfrmReqd
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PSet_Owner</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public DINT PSet_Owner
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Virtual</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL PCmd_Virtual
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Physical</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL PCmd_Physical
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_St0</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL PCmd_St0
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_St1</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL PCmd_St1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_St2</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL PCmd_St2
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_St3</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL PCmd_St3
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Reset</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL PCmd_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Prog</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL PCmd_Prog
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Oper</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL PCmd_Oper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Lock</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL PCmd_Lock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Unlock</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL PCmd_Unlock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Normal</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL PCmd_Normal
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_St0</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL XCmd_St0
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_St1</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL XCmd_St1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_St2</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL XCmd_St2
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_St3</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL XCmd_St3
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Acq</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL XCmd_Acq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Rel</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL XCmd_Rel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Reset</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL XCmd_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_ResetAckAll</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL XCmd_ResetAckAll
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_AData</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Out_AData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_BData</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Out_BData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_CData</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Out_CData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_DData</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Out_DData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_HornData</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Out_HornData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_Reset</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Out_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_OwnerSts</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public DINT Out_OwnerSts
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Initialized</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Sts_Initialized
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_St0</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Sts_St0
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_St1</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Sts_St1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_St2</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Sts_St2
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_St3</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Sts_St3
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Moving</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Sts_Moving
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Horn</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Sts_Horn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Virtual</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Sts_Virtual
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SrcQ_IO</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public SINT SrcQ_IO
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SrcQ</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public SINT SrcQ
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eFdbk</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public SINT Sts_eFdbk
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eCmd</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public SINT Sts_eCmd
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eSts</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public SINT Sts_eSts
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eFault</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public SINT Sts_eFault
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eOutState</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public SINT Sts_eOutState
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotify</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public SINT Sts_eNotify
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyAll</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public SINT Sts_eNotifyAll
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyIOFault</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public SINT Sts_eNotifyIOFault
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyFail</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public SINT Sts_eNotifyFail
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyIntlkTrip</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public SINT Sts_eNotifyIntlkTrip
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyEqpFault</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public SINT Sts_eNotifyEqpFault
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_UnackAlmCount</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public DINT Sts_UnackAlmCount
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eSrc</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public INT Sts_eSrc
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_bSrc</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public INT Sts_bSrc
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Available</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Sts_Available
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_IntlkAvailable</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Sts_IntlkAvailable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Bypass</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Sts_Bypass
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_BypActive</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Sts_BypActive
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_MaintByp</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Sts_MaintByp
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NotRdy</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Sts_NotRdy
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyCfgErr</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Sts_NrdyCfgErr
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyEqpFault</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Sts_NrdyEqpFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyFail</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Sts_NrdyFail
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyIntlk</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Sts_NrdyIntlk
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyIOFault</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Sts_NrdyIOFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyOoS</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Sts_NrdyOoS
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyPerm</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Sts_NrdyPerm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyPrioSt0</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Sts_NrdyPrioSt0
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Err</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Sts_Err
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrAlm</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Sts_ErrAlm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrOutAPulseTime</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Sts_ErrOutAPulseTime
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrOutBPulseTime</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Sts_ErrOutBPulseTime
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrOutCPulseTime</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Sts_ErrOutCPulseTime
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrOutDPulseTime</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Sts_ErrOutDPulseTime
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrVirtualFdbkTime</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Sts_ErrVirtualFdbkTime
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrFailTime</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Sts_ErrFailTime
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Hand</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Sts_Hand
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_OoS</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Sts_OoS
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Maint</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Sts_Maint
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Ovrd</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Sts_Ovrd
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Ext</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Sts_Ext
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Prog</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Sts_Prog
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ProgLocked</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Sts_ProgLocked
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Oper</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Sts_Oper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_OperLocked</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Sts_OperLocked
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ProgOperSel</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Sts_ProgOperSel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ProgOperLock</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Sts_ProgOperLock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Normal</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Sts_Normal
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ExtReqInh</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Sts_ExtReqInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ProgReqInh</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Sts_ProgReqInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_MAcqRcvd</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Sts_MAcqRcvd
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_CmdConflict</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Sts_CmdConflict
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Alm</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Sts_Alm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_AlmInh</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Sts_AlmInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_IOFault</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Sts_IOFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Fail</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Sts_Fail
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_IntlkTrip</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Sts_IntlkTrip
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_EqpFault</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Sts_EqpFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_RdyAck</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Sts_RdyAck
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_RdyReset</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL Sts_RdyReset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Acq</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL XRdy_Acq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Rel</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL XRdy_Rel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_St0</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL XRdy_St0
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_St1</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL XRdy_St1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_St2</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL XRdy_St2
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_St3</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL XRdy_St3
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Reset</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL XRdy_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_ResetAckAll</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public BOOL XRdy_ResetAckAll
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_Owner</c> member of the <see cref="P_DISCRETE_4STATE"/> data type.
    /// </summary>
    public DINT Val_Owner
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }
}