using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>P_DISCRETE_N_POSITION</c> data type structure.
/// </summary>
[LogixData("P_DISCRETE_N_POSITION")]
public sealed partial class P_DISCRETE_N_POSITION : StructureData
{
    /// <summary>
    /// Creates a new <see cref="P_DISCRETE_N_POSITION"/> instance initialized with default values.
    /// </summary>
    public P_DISCRETE_N_POSITION() : base("P_DISCRETE_N_POSITION")
    {
        EnableIn = new BOOL();
        EnableOut = new BOOL();
        Inp_InitializeReq = new BOOL();
        Inp_OwnerCmd = new DINT();
        Inp_Pos01FdbkData = new BOOL();
        Inp_Pos02FdbkData = new BOOL();
        Inp_Pos03FdbkData = new BOOL();
        Inp_Pos04FdbkData = new BOOL();
        Inp_Pos05FdbkData = new BOOL();
        Inp_Pos06FdbkData = new BOOL();
        Inp_Pos07FdbkData = new BOOL();
        Inp_Pos08FdbkData = new BOOL();
        Inp_Pos09FdbkData = new BOOL();
        Inp_Pos10FdbkData = new BOOL();
        Inp_Pos11FdbkData = new BOOL();
        Inp_Pos12FdbkData = new BOOL();
        Inp_Pos13FdbkData = new BOOL();
        Inp_Pos14FdbkData = new BOOL();
        Inp_Pos15FdbkData = new BOOL();
        Inp_Pos16FdbkData = new BOOL();
        Inp_Pos17FdbkData = new BOOL();
        Inp_Pos18FdbkData = new BOOL();
        Inp_Pos19FdbkData = new BOOL();
        Inp_Pos20FdbkData = new BOOL();
        Inp_Pos21FdbkData = new BOOL();
        Inp_Pos22FdbkData = new BOOL();
        Inp_Pos23FdbkData = new BOOL();
        Inp_Pos24FdbkData = new BOOL();
        Inp_Pos25FdbkData = new BOOL();
        Inp_Pos26FdbkData = new BOOL();
        Inp_Pos27FdbkData = new BOOL();
        Inp_Pos28FdbkData = new BOOL();
        Inp_Pos29FdbkData = new BOOL();
        Inp_Pos30FdbkData = new BOOL();
        Inp_LockFdbkData = new BOOL();
        Inp_UnlockFdbkData = new BOOL();
        Inp_CylExtFdbkData = new BOOL();
        Inp_CylRetrFdbkData = new BOOL();
        Inp_CylLeftFdbkData = new BOOL();
        Inp_CylRightFdbkData = new BOOL();
        Inp_IOFault = new BOOL();
        Inp_PermOK = new BOOL();
        Inp_NBPermOK = new BOOL();
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
        Cfg_NumPos = new SINT();
        Cfg_Circ = new BOOL();
        Cfg_CWOnly = new BOOL();
        Cfg_ViaPos01 = new BOOL();
        Cfg_IntlkToPos01 = new BOOL();
        Cfg_OutPosLatch = new BOOL();
        Cfg_HasLock = new BOOL();
        Cfg_HasPosFdbk = new BOOL();
        Cfg_UsePosFdbk = new BOOL();
        Cfg_HasLockFdbk = new BOOL();
        Cfg_UseLockFdbk = new BOOL();
        Cfg_HasCylFdbk = new BOOL();
        Cfg_UseCylFdbk = new BOOL();
        Cfg_HasPermObj = new BOOL();
        Cfg_HasIntlkObj = new BOOL();
        Cfg_HasMoreObj = new BOOL();
        Cfg_OperPos01Prio = new BOOL();
        Cfg_ExtPos01Prio = new BOOL();
        Cfg_OSetResets = new BOOL();
        Cfg_XSetResets = new BOOL();
        Cfg_SetDuringMove = new BOOL();
        Cfg_OvrdPermIntlk = new BOOL();
        Cfg_ShedOnPosFail = new BOOL();
        Cfg_ShedOnLockFail = new BOOL();
        Cfg_ShedOnIOFault = new BOOL();
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
        Cfg_Retries = new SINT();
        Cfg_ExtendDelay = new REAL();
        Cfg_VerifyDelay = new REAL();
        Cfg_PosCheckTime = new REAL();
        Cfg_LockCheckTime = new REAL();
        Cfg_VirtualPosTime = new REAL();
        Cfg_VirtualLockTime = new REAL();
        Cfg_VirtualCylTime = new REAL();
        Cfg_StartHornTime = new REAL();
        Cfg_CnfrmReqd = new SINT();
        PSet_Owner = new DINT();
        PSet_Pos = new SINT();
        PCmd_Virtual = new BOOL();
        PCmd_Physical = new BOOL();
        PCmd_Reset = new BOOL();
        PCmd_Prog = new BOOL();
        PCmd_Oper = new BOOL();
        PCmd_Lock = new BOOL();
        PCmd_Unlock = new BOOL();
        PCmd_Normal = new BOOL();
        XSet_Pos = new SINT();
        XCmd_Acq = new BOOL();
        XCmd_Rel = new BOOL();
        XCmd_Reset = new BOOL();
        XCmd_ResetAckAll = new BOOL();
        Out_Pos01Data = new BOOL();
        Out_Pos02Data = new BOOL();
        Out_Pos03Data = new BOOL();
        Out_Pos04Data = new BOOL();
        Out_Pos05Data = new BOOL();
        Out_Pos06Data = new BOOL();
        Out_Pos07Data = new BOOL();
        Out_Pos08Data = new BOOL();
        Out_Pos09Data = new BOOL();
        Out_Pos10Data = new BOOL();
        Out_Pos11Data = new BOOL();
        Out_Pos12Data = new BOOL();
        Out_Pos13Data = new BOOL();
        Out_Pos14Data = new BOOL();
        Out_Pos15Data = new BOOL();
        Out_Pos16Data = new BOOL();
        Out_Pos17Data = new BOOL();
        Out_Pos18Data = new BOOL();
        Out_Pos19Data = new BOOL();
        Out_Pos20Data = new BOOL();
        Out_Pos21Data = new BOOL();
        Out_Pos22Data = new BOOL();
        Out_Pos23Data = new BOOL();
        Out_Pos24Data = new BOOL();
        Out_Pos25Data = new BOOL();
        Out_Pos26Data = new BOOL();
        Out_Pos27Data = new BOOL();
        Out_Pos28Data = new BOOL();
        Out_Pos29Data = new BOOL();
        Out_Pos30Data = new BOOL();
        Out_IncData = new BOOL();
        Out_DecData = new BOOL();
        Out_UnlockData = new BOOL();
        Out_LockData = new BOOL();
        Out_CylExtendData = new BOOL();
        Out_CylRetractData = new BOOL();
        Out_CylLeftData = new BOOL();
        Out_CylRightData = new BOOL();
        Out_HornData = new BOOL();
        Out_Reset = new BOOL();
        Out_OwnerSts = new DINT();
        Sts_Initialized = new BOOL();
        Sts_Pos01 = new BOOL();
        Sts_Pos02 = new BOOL();
        Sts_Pos03 = new BOOL();
        Sts_Pos04 = new BOOL();
        Sts_Pos05 = new BOOL();
        Sts_Pos06 = new BOOL();
        Sts_Pos07 = new BOOL();
        Sts_Pos08 = new BOOL();
        Sts_Pos09 = new BOOL();
        Sts_Pos10 = new BOOL();
        Sts_Pos11 = new BOOL();
        Sts_Pos12 = new BOOL();
        Sts_Pos13 = new BOOL();
        Sts_Pos14 = new BOOL();
        Sts_Pos15 = new BOOL();
        Sts_Pos16 = new BOOL();
        Sts_Pos17 = new BOOL();
        Sts_Pos18 = new BOOL();
        Sts_Pos19 = new BOOL();
        Sts_Pos20 = new BOOL();
        Sts_Pos21 = new BOOL();
        Sts_Pos22 = new BOOL();
        Sts_Pos23 = new BOOL();
        Sts_Pos24 = new BOOL();
        Sts_Pos25 = new BOOL();
        Sts_Pos26 = new BOOL();
        Sts_Pos27 = new BOOL();
        Sts_Pos28 = new BOOL();
        Sts_Pos29 = new BOOL();
        Sts_Pos30 = new BOOL();
        Sts_Moving = new BOOL();
        Sts_Horn = new BOOL();
        Sts_Virtual = new BOOL();
        SrcQ_IO = new SINT();
        SrcQ = new SINT();
        Sts_eCmd = new SINT();
        Sts_eFdbk = new SINT();
        Sts_eSts = new SINT();
        Sts_eFault = new SINT();
        Sts_eState = new SINT();
        Sts_eOutPos = new SINT();
        Sts_eOutState = new SINT();
        Sts_eNotify = new SINT();
        Sts_eNotifyAll = new SINT();
        Sts_eNotifyIOFault = new SINT();
        Sts_eNotifyPosFail = new SINT();
        Sts_eNotifyLockFail = new SINT();
        Sts_eNotifyIntlkTrip = new SINT();
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
        Sts_NrdyPosFail = new BOOL();
        Sts_NrdyLockFail = new BOOL();
        Sts_NrdyIntlk = new BOOL();
        Sts_NrdyIOFault = new BOOL();
        Sts_NrdyOoS = new BOOL();
        Sts_NrdyPerm = new BOOL();
        Sts_NrdyPrioPos01 = new BOOL();
        Sts_Err = new BOOL();
        Sts_ErrAlm = new BOOL();
        Sts_ErrExtendDelay = new BOOL();
        Sts_ErrVerifyDelay = new BOOL();
        Sts_ErrPosCheckTime = new BOOL();
        Sts_ErrLockCheckTime = new BOOL();
        Sts_ErrVirtualPosTime = new BOOL();
        Sts_ErrVirtualLockTime = new BOOL();
        Sts_ErrVirtualCylTime = new BOOL();
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
        Sts_Alm = new BOOL();
        Sts_AlmInh = new BOOL();
        Sts_IOFault = new BOOL();
        Sts_PosFail = new BOOL();
        Sts_LockFail = new BOOL();
        Sts_IntlkTrip = new BOOL();
        Sts_RdyAck = new BOOL();
        Sts_RdyReset = new BOOL();
        XRdy_Acq = new BOOL();
        XRdy_Rel = new BOOL();
        XRdy_Pos = new BOOL();
        XRdy_Reset = new BOOL();
        XRdy_ResetAckAll = new BOOL();
        Val_Owner = new DINT();
    }
    
    /// <summary>
    /// Creates a new <see cref="P_DISCRETE_N_POSITION"/> instance initialized with the provided element.
    /// </summary>
    public P_DISCRETE_N_POSITION(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 348;
    
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
        Inp_Pos01FdbkData.UpdateData((data[offset + 9] & (1 << 3)) != 0);
        Inp_Pos02FdbkData.UpdateData((data[offset + 9] & (1 << 4)) != 0);
        Inp_Pos03FdbkData.UpdateData((data[offset + 9] & (1 << 5)) != 0);
        Inp_Pos04FdbkData.UpdateData((data[offset + 9] & (1 << 6)) != 0);
        Inp_Pos05FdbkData.UpdateData((data[offset + 9] & (1 << 7)) != 0);
        Inp_Pos06FdbkData.UpdateData((data[offset + 10] & (1 << 0)) != 0);
        Inp_Pos07FdbkData.UpdateData((data[offset + 10] & (1 << 1)) != 0);
        Inp_Pos08FdbkData.UpdateData((data[offset + 10] & (1 << 2)) != 0);
        Inp_Pos09FdbkData.UpdateData((data[offset + 10] & (1 << 3)) != 0);
        Inp_Pos10FdbkData.UpdateData((data[offset + 10] & (1 << 4)) != 0);
        Inp_Pos11FdbkData.UpdateData((data[offset + 10] & (1 << 5)) != 0);
        Inp_Pos12FdbkData.UpdateData((data[offset + 10] & (1 << 6)) != 0);
        Inp_Pos13FdbkData.UpdateData((data[offset + 10] & (1 << 7)) != 0);
        Inp_Pos14FdbkData.UpdateData((data[offset + 11] & (1 << 0)) != 0);
        Inp_Pos15FdbkData.UpdateData((data[offset + 11] & (1 << 1)) != 0);
        Inp_Pos16FdbkData.UpdateData((data[offset + 11] & (1 << 2)) != 0);
        Inp_Pos17FdbkData.UpdateData((data[offset + 11] & (1 << 3)) != 0);
        Inp_Pos18FdbkData.UpdateData((data[offset + 11] & (1 << 4)) != 0);
        Inp_Pos19FdbkData.UpdateData((data[offset + 11] & (1 << 5)) != 0);
        Inp_Pos20FdbkData.UpdateData((data[offset + 11] & (1 << 6)) != 0);
        Inp_Pos21FdbkData.UpdateData((data[offset + 11] & (1 << 7)) != 0);
        Inp_Pos22FdbkData.UpdateData((data[offset + 12] & (1 << 0)) != 0);
        Inp_Pos23FdbkData.UpdateData((data[offset + 12] & (1 << 1)) != 0);
        Inp_Pos24FdbkData.UpdateData((data[offset + 12] & (1 << 2)) != 0);
        Inp_Pos25FdbkData.UpdateData((data[offset + 12] & (1 << 3)) != 0);
        Inp_Pos26FdbkData.UpdateData((data[offset + 12] & (1 << 4)) != 0);
        Inp_Pos27FdbkData.UpdateData((data[offset + 12] & (1 << 5)) != 0);
        Inp_Pos28FdbkData.UpdateData((data[offset + 12] & (1 << 6)) != 0);
        Inp_Pos29FdbkData.UpdateData((data[offset + 12] & (1 << 7)) != 0);
        Inp_Pos30FdbkData.UpdateData((data[offset + 13] & (1 << 0)) != 0);
        Inp_LockFdbkData.UpdateData((data[offset + 13] & (1 << 1)) != 0);
        Inp_UnlockFdbkData.UpdateData((data[offset + 13] & (1 << 2)) != 0);
        Inp_CylExtFdbkData.UpdateData((data[offset + 13] & (1 << 3)) != 0);
        Inp_CylRetrFdbkData.UpdateData((data[offset + 13] & (1 << 4)) != 0);
        Inp_CylLeftFdbkData.UpdateData((data[offset + 13] & (1 << 5)) != 0);
        Inp_CylRightFdbkData.UpdateData((data[offset + 13] & (1 << 6)) != 0);
        Inp_IOFault.UpdateData((data[offset + 13] & (1 << 7)) != 0);
        Inp_PermOK.UpdateData((data[offset + 14] & (1 << 0)) != 0);
        Inp_NBPermOK.UpdateData((data[offset + 14] & (1 << 1)) != 0);
        Inp_IntlkOK.UpdateData((data[offset + 14] & (1 << 2)) != 0);
        Inp_NBIntlkOK.UpdateData((data[offset + 14] & (1 << 3)) != 0);
        Inp_IntlkAvailable.UpdateData((data[offset + 14] & (1 << 4)) != 0);
        Inp_IntlkTripInh.UpdateData((data[offset + 14] & (1 << 5)) != 0);
        Inp_RdyReset.UpdateData((data[offset + 14] & (1 << 6)) != 0);
        Inp_Hand.UpdateData((data[offset + 14] & (1 << 7)) != 0);
        Inp_Ovrd.UpdateData((data[offset + 15] & (1 << 0)) != 0);
        Inp_OvrdCmd.UpdateData(data, offset + 15);
        Inp_ExtInh.UpdateData((data[offset + 16] & (1 << 1)) != 0);
        Inp_HornInh.UpdateData((data[offset + 16] & (1 << 2)) != 0);
        Inp_Reset.UpdateData((data[offset + 16] & (1 << 3)) != 0);
        Cfg_AllowDisable.UpdateData((data[offset + 16] & (1 << 4)) != 0);
        Cfg_AllowShelve.UpdateData((data[offset + 16] & (1 << 5)) != 0);
        Cfg_NumPos.UpdateData(data, offset + 16);
        Cfg_Circ.UpdateData((data[offset + 17] & (1 << 6)) != 0);
        Cfg_CWOnly.UpdateData((data[offset + 17] & (1 << 7)) != 0);
        Cfg_ViaPos01.UpdateData((data[offset + 18] & (1 << 0)) != 0);
        Cfg_IntlkToPos01.UpdateData((data[offset + 18] & (1 << 1)) != 0);
        Cfg_OutPosLatch.UpdateData((data[offset + 18] & (1 << 2)) != 0);
        Cfg_HasLock.UpdateData((data[offset + 18] & (1 << 3)) != 0);
        Cfg_HasPosFdbk.UpdateData((data[offset + 18] & (1 << 4)) != 0);
        Cfg_UsePosFdbk.UpdateData((data[offset + 18] & (1 << 5)) != 0);
        Cfg_HasLockFdbk.UpdateData((data[offset + 18] & (1 << 6)) != 0);
        Cfg_UseLockFdbk.UpdateData((data[offset + 18] & (1 << 7)) != 0);
        Cfg_HasCylFdbk.UpdateData((data[offset + 19] & (1 << 0)) != 0);
        Cfg_UseCylFdbk.UpdateData((data[offset + 23] & (1 << 1)) != 0);
        Cfg_HasPermObj.UpdateData((data[offset + 23] & (1 << 2)) != 0);
        Cfg_HasIntlkObj.UpdateData((data[offset + 23] & (1 << 3)) != 0);
        Cfg_HasMoreObj.UpdateData((data[offset + 23] & (1 << 4)) != 0);
        Cfg_OperPos01Prio.UpdateData((data[offset + 23] & (1 << 5)) != 0);
        Cfg_ExtPos01Prio.UpdateData((data[offset + 23] & (1 << 6)) != 0);
        Cfg_OSetResets.UpdateData((data[offset + 23] & (1 << 7)) != 0);
        Cfg_XSetResets.UpdateData((data[offset + 24] & (1 << 0)) != 0);
        Cfg_SetDuringMove.UpdateData((data[offset + 24] & (1 << 1)) != 0);
        Cfg_OvrdPermIntlk.UpdateData((data[offset + 24] & (1 << 2)) != 0);
        Cfg_ShedOnPosFail.UpdateData((data[offset + 24] & (1 << 3)) != 0);
        Cfg_ShedOnLockFail.UpdateData((data[offset + 24] & (1 << 4)) != 0);
        Cfg_ShedOnIOFault.UpdateData((data[offset + 24] & (1 << 5)) != 0);
        Cfg_HornOnChange.UpdateData((data[offset + 24] & (1 << 6)) != 0);
        Cfg_HasOper.UpdateData((data[offset + 24] & (1 << 7)) != 0);
        Cfg_HasOperLocked.UpdateData((data[offset + 25] & (1 << 0)) != 0);
        Cfg_HasProg.UpdateData((data[offset + 25] & (1 << 1)) != 0);
        Cfg_HasProgLocked.UpdateData((data[offset + 25] & (1 << 2)) != 0);
        Cfg_HasExt.UpdateData((data[offset + 25] & (1 << 3)) != 0);
        Cfg_HasMaint.UpdateData((data[offset + 25] & (1 << 4)) != 0);
        Cfg_HasMaintOoS.UpdateData((data[offset + 25] & (1 << 5)) != 0);
        Cfg_OvrdOverLock.UpdateData((data[offset + 25] & (1 << 6)) != 0);
        Cfg_ExtOverLock.UpdateData((data[offset + 25] & (1 << 7)) != 0);
        Cfg_ProgPwrUp.UpdateData((data[offset + 26] & (1 << 0)) != 0);
        Cfg_ProgNormal.UpdateData((data[offset + 26] & (1 << 1)) != 0);
        Cfg_PCmdPriority.UpdateData((data[offset + 26] & (1 << 2)) != 0);
        Cfg_PCmdProgAsLevel.UpdateData((data[offset + 26] & (1 << 3)) != 0);
        Cfg_PCmdLockAsLevel.UpdateData((data[offset + 26] & (1 << 4)) != 0);
        Cfg_ExtAcqAsLevel.UpdateData((data[offset + 26] & (1 << 5)) != 0);
        Cfg_Retries.UpdateData(data, offset + 26);
        Cfg_ExtendDelay.UpdateData(data, offset + 27);
        Cfg_VerifyDelay.UpdateData(data, offset + 31);
        Cfg_PosCheckTime.UpdateData(data, offset + 35);
        Cfg_LockCheckTime.UpdateData(data, offset + 39);
        Cfg_VirtualPosTime.UpdateData(data, offset + 43);
        Cfg_VirtualLockTime.UpdateData(data, offset + 47);
        Cfg_VirtualCylTime.UpdateData(data, offset + 51);
        Cfg_StartHornTime.UpdateData(data, offset + 55);
        Cfg_CnfrmReqd.UpdateData(data, offset + 59);
        PSet_Owner.UpdateData(data, offset + 60);
        PSet_Pos.UpdateData(data, offset + 64);
        PCmd_Virtual.UpdateData((data[offset + 65] & (1 << 6)) != 0);
        PCmd_Physical.UpdateData((data[offset + 65] & (1 << 7)) != 0);
        PCmd_Reset.UpdateData((data[offset + 66] & (1 << 0)) != 0);
        PCmd_Prog.UpdateData((data[offset + 66] & (1 << 1)) != 0);
        PCmd_Oper.UpdateData((data[offset + 66] & (1 << 2)) != 0);
        PCmd_Lock.UpdateData((data[offset + 66] & (1 << 3)) != 0);
        PCmd_Unlock.UpdateData((data[offset + 66] & (1 << 4)) != 0);
        PCmd_Normal.UpdateData((data[offset + 66] & (1 << 5)) != 0);
        XSet_Pos.UpdateData(data, offset + 66);
        XCmd_Acq.UpdateData((data[offset + 67] & (1 << 6)) != 0);
        XCmd_Rel.UpdateData((data[offset + 67] & (1 << 7)) != 0);
        XCmd_Reset.UpdateData((data[offset + 68] & (1 << 0)) != 0);
        XCmd_ResetAckAll.UpdateData((data[offset + 68] & (1 << 1)) != 0);
        Out_Pos01Data.UpdateData((data[offset + 68] & (1 << 2)) != 0);
        Out_Pos02Data.UpdateData((data[offset + 68] & (1 << 3)) != 0);
        Out_Pos03Data.UpdateData((data[offset + 68] & (1 << 4)) != 0);
        Out_Pos04Data.UpdateData((data[offset + 68] & (1 << 5)) != 0);
        Out_Pos05Data.UpdateData((data[offset + 68] & (1 << 6)) != 0);
        Out_Pos06Data.UpdateData((data[offset + 68] & (1 << 7)) != 0);
        Out_Pos07Data.UpdateData((data[offset + 69] & (1 << 0)) != 0);
        Out_Pos08Data.UpdateData((data[offset + 69] & (1 << 1)) != 0);
        Out_Pos09Data.UpdateData((data[offset + 69] & (1 << 2)) != 0);
        Out_Pos10Data.UpdateData((data[offset + 69] & (1 << 3)) != 0);
        Out_Pos11Data.UpdateData((data[offset + 69] & (1 << 4)) != 0);
        Out_Pos12Data.UpdateData((data[offset + 69] & (1 << 5)) != 0);
        Out_Pos13Data.UpdateData((data[offset + 69] & (1 << 6)) != 0);
        Out_Pos14Data.UpdateData((data[offset + 69] & (1 << 7)) != 0);
        Out_Pos15Data.UpdateData((data[offset + 70] & (1 << 0)) != 0);
        Out_Pos16Data.UpdateData((data[offset + 70] & (1 << 1)) != 0);
        Out_Pos17Data.UpdateData((data[offset + 70] & (1 << 2)) != 0);
        Out_Pos18Data.UpdateData((data[offset + 70] & (1 << 3)) != 0);
        Out_Pos19Data.UpdateData((data[offset + 70] & (1 << 4)) != 0);
        Out_Pos20Data.UpdateData((data[offset + 70] & (1 << 5)) != 0);
        Out_Pos21Data.UpdateData((data[offset + 70] & (1 << 6)) != 0);
        Out_Pos22Data.UpdateData((data[offset + 70] & (1 << 7)) != 0);
        Out_Pos23Data.UpdateData((data[offset + 71] & (1 << 0)) != 0);
        Out_Pos24Data.UpdateData((data[offset + 71] & (1 << 1)) != 0);
        Out_Pos25Data.UpdateData((data[offset + 71] & (1 << 2)) != 0);
        Out_Pos26Data.UpdateData((data[offset + 71] & (1 << 3)) != 0);
        Out_Pos27Data.UpdateData((data[offset + 71] & (1 << 4)) != 0);
        Out_Pos28Data.UpdateData((data[offset + 71] & (1 << 5)) != 0);
        Out_Pos29Data.UpdateData((data[offset + 71] & (1 << 6)) != 0);
        Out_Pos30Data.UpdateData((data[offset + 71] & (1 << 7)) != 0);
        Out_IncData.UpdateData((data[offset + 72] & (1 << 0)) != 0);
        Out_DecData.UpdateData((data[offset + 72] & (1 << 1)) != 0);
        Out_UnlockData.UpdateData((data[offset + 72] & (1 << 2)) != 0);
        Out_LockData.UpdateData((data[offset + 72] & (1 << 3)) != 0);
        Out_CylExtendData.UpdateData((data[offset + 72] & (1 << 4)) != 0);
        Out_CylRetractData.UpdateData((data[offset + 72] & (1 << 5)) != 0);
        Out_CylLeftData.UpdateData((data[offset + 72] & (1 << 6)) != 0);
        Out_CylRightData.UpdateData((data[offset + 72] & (1 << 7)) != 0);
        Out_HornData.UpdateData((data[offset + 73] & (1 << 0)) != 0);
        Out_Reset.UpdateData((data[offset + 73] & (1 << 1)) != 0);
        Out_OwnerSts.UpdateData(data, offset + 73);
        Sts_Initialized.UpdateData((data[offset + 77] & (1 << 2)) != 0);
        Sts_Pos01.UpdateData((data[offset + 77] & (1 << 3)) != 0);
        Sts_Pos02.UpdateData((data[offset + 77] & (1 << 4)) != 0);
        Sts_Pos03.UpdateData((data[offset + 81] & (1 << 5)) != 0);
        Sts_Pos04.UpdateData((data[offset + 81] & (1 << 6)) != 0);
        Sts_Pos05.UpdateData((data[offset + 81] & (1 << 7)) != 0);
        Sts_Pos06.UpdateData((data[offset + 82] & (1 << 0)) != 0);
        Sts_Pos07.UpdateData((data[offset + 82] & (1 << 1)) != 0);
        Sts_Pos08.UpdateData((data[offset + 82] & (1 << 2)) != 0);
        Sts_Pos09.UpdateData((data[offset + 82] & (1 << 3)) != 0);
        Sts_Pos10.UpdateData((data[offset + 82] & (1 << 4)) != 0);
        Sts_Pos11.UpdateData((data[offset + 82] & (1 << 5)) != 0);
        Sts_Pos12.UpdateData((data[offset + 82] & (1 << 6)) != 0);
        Sts_Pos13.UpdateData((data[offset + 82] & (1 << 7)) != 0);
        Sts_Pos14.UpdateData((data[offset + 83] & (1 << 0)) != 0);
        Sts_Pos15.UpdateData((data[offset + 83] & (1 << 1)) != 0);
        Sts_Pos16.UpdateData((data[offset + 83] & (1 << 2)) != 0);
        Sts_Pos17.UpdateData((data[offset + 83] & (1 << 3)) != 0);
        Sts_Pos18.UpdateData((data[offset + 83] & (1 << 4)) != 0);
        Sts_Pos19.UpdateData((data[offset + 83] & (1 << 5)) != 0);
        Sts_Pos20.UpdateData((data[offset + 83] & (1 << 6)) != 0);
        Sts_Pos21.UpdateData((data[offset + 83] & (1 << 7)) != 0);
        Sts_Pos22.UpdateData((data[offset + 84] & (1 << 0)) != 0);
        Sts_Pos23.UpdateData((data[offset + 84] & (1 << 1)) != 0);
        Sts_Pos24.UpdateData((data[offset + 84] & (1 << 2)) != 0);
        Sts_Pos25.UpdateData((data[offset + 84] & (1 << 3)) != 0);
        Sts_Pos26.UpdateData((data[offset + 84] & (1 << 4)) != 0);
        Sts_Pos27.UpdateData((data[offset + 84] & (1 << 5)) != 0);
        Sts_Pos28.UpdateData((data[offset + 84] & (1 << 6)) != 0);
        Sts_Pos29.UpdateData((data[offset + 84] & (1 << 7)) != 0);
        Sts_Pos30.UpdateData((data[offset + 85] & (1 << 0)) != 0);
        Sts_Moving.UpdateData((data[offset + 85] & (1 << 1)) != 0);
        Sts_Horn.UpdateData((data[offset + 85] & (1 << 2)) != 0);
        Sts_Virtual.UpdateData((data[offset + 85] & (1 << 3)) != 0);
        SrcQ_IO.UpdateData(data, offset + 85);
        SrcQ.UpdateData(data, offset + 86);
        Sts_eCmd.UpdateData(data, offset + 87);
        Sts_eFdbk.UpdateData(data, offset + 88);
        Sts_eSts.UpdateData(data, offset + 89);
        Sts_eFault.UpdateData(data, offset + 90);
        Sts_eState.UpdateData(data, offset + 91);
        Sts_eOutPos.UpdateData(data, offset + 92);
        Sts_eOutState.UpdateData(data, offset + 93);
        Sts_eNotify.UpdateData(data, offset + 94);
        Sts_eNotifyAll.UpdateData(data, offset + 95);
        Sts_eNotifyIOFault.UpdateData(data, offset + 96);
        Sts_eNotifyPosFail.UpdateData(data, offset + 97);
        Sts_eNotifyLockFail.UpdateData(data, offset + 98);
        Sts_eNotifyIntlkTrip.UpdateData(data, offset + 99);
        Sts_UnackAlmCount.UpdateData(data, offset + 100);
        Sts_eSrc.UpdateData(data, offset + 104);
        Sts_bSrc.UpdateData(data, offset + 106);
        Sts_Available.UpdateData((data[offset + 108] & (1 << 4)) != 0);
        Sts_IntlkAvailable.UpdateData((data[offset + 112] & (1 << 5)) != 0);
        Sts_Bypass.UpdateData((data[offset + 112] & (1 << 6)) != 0);
        Sts_BypActive.UpdateData((data[offset + 112] & (1 << 7)) != 0);
        Sts_MaintByp.UpdateData((data[offset + 113] & (1 << 0)) != 0);
        Sts_NotRdy.UpdateData((data[offset + 113] & (1 << 1)) != 0);
        Sts_NrdyCfgErr.UpdateData((data[offset + 113] & (1 << 2)) != 0);
        Sts_NrdyPosFail.UpdateData((data[offset + 113] & (1 << 3)) != 0);
        Sts_NrdyLockFail.UpdateData((data[offset + 113] & (1 << 4)) != 0);
        Sts_NrdyIntlk.UpdateData((data[offset + 113] & (1 << 5)) != 0);
        Sts_NrdyIOFault.UpdateData((data[offset + 113] & (1 << 6)) != 0);
        Sts_NrdyOoS.UpdateData((data[offset + 113] & (1 << 7)) != 0);
        Sts_NrdyPerm.UpdateData((data[offset + 114] & (1 << 0)) != 0);
        Sts_NrdyPrioPos01.UpdateData((data[offset + 114] & (1 << 1)) != 0);
        Sts_Err.UpdateData((data[offset + 114] & (1 << 2)) != 0);
        Sts_ErrAlm.UpdateData((data[offset + 114] & (1 << 3)) != 0);
        Sts_ErrExtendDelay.UpdateData((data[offset + 114] & (1 << 4)) != 0);
        Sts_ErrVerifyDelay.UpdateData((data[offset + 114] & (1 << 5)) != 0);
        Sts_ErrPosCheckTime.UpdateData((data[offset + 114] & (1 << 6)) != 0);
        Sts_ErrLockCheckTime.UpdateData((data[offset + 114] & (1 << 7)) != 0);
        Sts_ErrVirtualPosTime.UpdateData((data[offset + 115] & (1 << 0)) != 0);
        Sts_ErrVirtualLockTime.UpdateData((data[offset + 115] & (1 << 1)) != 0);
        Sts_ErrVirtualCylTime.UpdateData((data[offset + 115] & (1 << 2)) != 0);
        Sts_Hand.UpdateData((data[offset + 115] & (1 << 3)) != 0);
        Sts_OoS.UpdateData((data[offset + 115] & (1 << 4)) != 0);
        Sts_Maint.UpdateData((data[offset + 115] & (1 << 5)) != 0);
        Sts_Ovrd.UpdateData((data[offset + 115] & (1 << 6)) != 0);
        Sts_Ext.UpdateData((data[offset + 115] & (1 << 7)) != 0);
        Sts_Prog.UpdateData((data[offset + 116] & (1 << 0)) != 0);
        Sts_ProgLocked.UpdateData((data[offset + 116] & (1 << 1)) != 0);
        Sts_Oper.UpdateData((data[offset + 116] & (1 << 2)) != 0);
        Sts_OperLocked.UpdateData((data[offset + 116] & (1 << 3)) != 0);
        Sts_ProgOperSel.UpdateData((data[offset + 116] & (1 << 4)) != 0);
        Sts_ProgOperLock.UpdateData((data[offset + 116] & (1 << 5)) != 0);
        Sts_Normal.UpdateData((data[offset + 116] & (1 << 6)) != 0);
        Sts_ExtReqInh.UpdateData((data[offset + 116] & (1 << 7)) != 0);
        Sts_ProgReqInh.UpdateData((data[offset + 117] & (1 << 0)) != 0);
        Sts_MAcqRcvd.UpdateData((data[offset + 117] & (1 << 1)) != 0);
        Sts_Alm.UpdateData((data[offset + 117] & (1 << 2)) != 0);
        Sts_AlmInh.UpdateData((data[offset + 117] & (1 << 3)) != 0);
        Sts_IOFault.UpdateData((data[offset + 117] & (1 << 4)) != 0);
        Sts_PosFail.UpdateData((data[offset + 117] & (1 << 5)) != 0);
        Sts_LockFail.UpdateData((data[offset + 117] & (1 << 6)) != 0);
        Sts_IntlkTrip.UpdateData((data[offset + 117] & (1 << 7)) != 0);
        Sts_RdyAck.UpdateData((data[offset + 118] & (1 << 0)) != 0);
        Sts_RdyReset.UpdateData((data[offset + 118] & (1 << 1)) != 0);
        XRdy_Acq.UpdateData((data[offset + 118] & (1 << 2)) != 0);
        XRdy_Rel.UpdateData((data[offset + 118] & (1 << 3)) != 0);
        XRdy_Pos.UpdateData((data[offset + 118] & (1 << 4)) != 0);
        XRdy_Reset.UpdateData((data[offset + 118] & (1 << 5)) != 0);
        XRdy_ResetAckAll.UpdateData((data[offset + 122] & (1 << 6)) != 0);
        Val_Owner.UpdateData(data, offset + 122);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_InitializeReq</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Inp_InitializeReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_OwnerCmd</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public DINT Inp_OwnerCmd
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Pos01FdbkData</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Inp_Pos01FdbkData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Pos02FdbkData</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Inp_Pos02FdbkData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Pos03FdbkData</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Inp_Pos03FdbkData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Pos04FdbkData</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Inp_Pos04FdbkData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Pos05FdbkData</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Inp_Pos05FdbkData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Pos06FdbkData</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Inp_Pos06FdbkData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Pos07FdbkData</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Inp_Pos07FdbkData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Pos08FdbkData</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Inp_Pos08FdbkData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Pos09FdbkData</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Inp_Pos09FdbkData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Pos10FdbkData</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Inp_Pos10FdbkData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Pos11FdbkData</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Inp_Pos11FdbkData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Pos12FdbkData</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Inp_Pos12FdbkData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Pos13FdbkData</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Inp_Pos13FdbkData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Pos14FdbkData</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Inp_Pos14FdbkData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Pos15FdbkData</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Inp_Pos15FdbkData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Pos16FdbkData</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Inp_Pos16FdbkData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Pos17FdbkData</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Inp_Pos17FdbkData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Pos18FdbkData</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Inp_Pos18FdbkData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Pos19FdbkData</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Inp_Pos19FdbkData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Pos20FdbkData</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Inp_Pos20FdbkData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Pos21FdbkData</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Inp_Pos21FdbkData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Pos22FdbkData</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Inp_Pos22FdbkData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Pos23FdbkData</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Inp_Pos23FdbkData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Pos24FdbkData</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Inp_Pos24FdbkData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Pos25FdbkData</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Inp_Pos25FdbkData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Pos26FdbkData</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Inp_Pos26FdbkData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Pos27FdbkData</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Inp_Pos27FdbkData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Pos28FdbkData</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Inp_Pos28FdbkData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Pos29FdbkData</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Inp_Pos29FdbkData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Pos30FdbkData</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Inp_Pos30FdbkData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_LockFdbkData</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Inp_LockFdbkData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_UnlockFdbkData</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Inp_UnlockFdbkData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_CylExtFdbkData</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Inp_CylExtFdbkData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_CylRetrFdbkData</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Inp_CylRetrFdbkData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_CylLeftFdbkData</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Inp_CylLeftFdbkData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_CylRightFdbkData</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Inp_CylRightFdbkData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_IOFault</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Inp_IOFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PermOK</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Inp_PermOK
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_NBPermOK</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Inp_NBPermOK
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_IntlkOK</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Inp_IntlkOK
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_NBIntlkOK</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Inp_NBIntlkOK
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_IntlkAvailable</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Inp_IntlkAvailable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_IntlkTripInh</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Inp_IntlkTripInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_RdyReset</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Inp_RdyReset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Hand</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Inp_Hand
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Ovrd</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Inp_Ovrd
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_OvrdCmd</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public SINT Inp_OvrdCmd
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_ExtInh</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Inp_ExtInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_HornInh</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Inp_HornInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Reset</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Inp_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_AllowDisable</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Cfg_AllowDisable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_AllowShelve</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Cfg_AllowShelve
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_NumPos</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public SINT Cfg_NumPos
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_Circ</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Cfg_Circ
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CWOnly</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Cfg_CWOnly
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ViaPos01</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Cfg_ViaPos01
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_IntlkToPos01</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Cfg_IntlkToPos01
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OutPosLatch</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Cfg_OutPosLatch
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasLock</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Cfg_HasLock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasPosFdbk</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Cfg_HasPosFdbk
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_UsePosFdbk</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Cfg_UsePosFdbk
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasLockFdbk</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Cfg_HasLockFdbk
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_UseLockFdbk</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Cfg_UseLockFdbk
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasCylFdbk</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Cfg_HasCylFdbk
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_UseCylFdbk</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Cfg_UseCylFdbk
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasPermObj</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Cfg_HasPermObj
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasIntlkObj</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Cfg_HasIntlkObj
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasMoreObj</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Cfg_HasMoreObj
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OperPos01Prio</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Cfg_OperPos01Prio
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ExtPos01Prio</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Cfg_ExtPos01Prio
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OSetResets</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Cfg_OSetResets
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_XSetResets</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Cfg_XSetResets
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_SetDuringMove</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Cfg_SetDuringMove
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OvrdPermIntlk</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Cfg_OvrdPermIntlk
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ShedOnPosFail</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Cfg_ShedOnPosFail
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ShedOnLockFail</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Cfg_ShedOnLockFail
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ShedOnIOFault</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Cfg_ShedOnIOFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HornOnChange</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Cfg_HornOnChange
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasOper</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Cfg_HasOper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasOperLocked</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Cfg_HasOperLocked
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasProg</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Cfg_HasProg
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasProgLocked</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Cfg_HasProgLocked
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasExt</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Cfg_HasExt
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasMaint</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Cfg_HasMaint
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasMaintOoS</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Cfg_HasMaintOoS
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OvrdOverLock</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Cfg_OvrdOverLock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ExtOverLock</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Cfg_ExtOverLock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ProgPwrUp</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Cfg_ProgPwrUp
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ProgNormal</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Cfg_ProgNormal
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PCmdPriority</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Cfg_PCmdPriority
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PCmdProgAsLevel</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Cfg_PCmdProgAsLevel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PCmdLockAsLevel</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Cfg_PCmdLockAsLevel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ExtAcqAsLevel</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Cfg_ExtAcqAsLevel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_Retries</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public SINT Cfg_Retries
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ExtendDelay</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public REAL Cfg_ExtendDelay
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_VerifyDelay</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public REAL Cfg_VerifyDelay
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PosCheckTime</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public REAL Cfg_PosCheckTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_LockCheckTime</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public REAL Cfg_LockCheckTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_VirtualPosTime</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public REAL Cfg_VirtualPosTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_VirtualLockTime</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public REAL Cfg_VirtualLockTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_VirtualCylTime</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public REAL Cfg_VirtualCylTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_StartHornTime</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public REAL Cfg_StartHornTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CnfrmReqd</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public SINT Cfg_CnfrmReqd
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PSet_Owner</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public DINT PSet_Owner
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PSet_Pos</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public SINT PSet_Pos
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Virtual</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL PCmd_Virtual
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Physical</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL PCmd_Physical
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Reset</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL PCmd_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Prog</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL PCmd_Prog
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Oper</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL PCmd_Oper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Lock</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL PCmd_Lock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Unlock</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL PCmd_Unlock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Normal</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL PCmd_Normal
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XSet_Pos</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public SINT XSet_Pos
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Acq</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL XCmd_Acq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Rel</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL XCmd_Rel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Reset</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL XCmd_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_ResetAckAll</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL XCmd_ResetAckAll
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_Pos01Data</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Out_Pos01Data
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_Pos02Data</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Out_Pos02Data
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_Pos03Data</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Out_Pos03Data
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_Pos04Data</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Out_Pos04Data
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_Pos05Data</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Out_Pos05Data
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_Pos06Data</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Out_Pos06Data
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_Pos07Data</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Out_Pos07Data
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_Pos08Data</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Out_Pos08Data
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_Pos09Data</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Out_Pos09Data
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_Pos10Data</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Out_Pos10Data
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_Pos11Data</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Out_Pos11Data
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_Pos12Data</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Out_Pos12Data
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_Pos13Data</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Out_Pos13Data
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_Pos14Data</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Out_Pos14Data
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_Pos15Data</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Out_Pos15Data
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_Pos16Data</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Out_Pos16Data
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_Pos17Data</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Out_Pos17Data
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_Pos18Data</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Out_Pos18Data
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_Pos19Data</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Out_Pos19Data
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_Pos20Data</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Out_Pos20Data
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_Pos21Data</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Out_Pos21Data
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_Pos22Data</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Out_Pos22Data
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_Pos23Data</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Out_Pos23Data
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_Pos24Data</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Out_Pos24Data
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_Pos25Data</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Out_Pos25Data
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_Pos26Data</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Out_Pos26Data
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_Pos27Data</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Out_Pos27Data
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_Pos28Data</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Out_Pos28Data
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_Pos29Data</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Out_Pos29Data
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_Pos30Data</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Out_Pos30Data
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_IncData</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Out_IncData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_DecData</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Out_DecData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_UnlockData</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Out_UnlockData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_LockData</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Out_LockData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_CylExtendData</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Out_CylExtendData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_CylRetractData</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Out_CylRetractData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_CylLeftData</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Out_CylLeftData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_CylRightData</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Out_CylRightData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_HornData</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Out_HornData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_Reset</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Out_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_OwnerSts</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public DINT Out_OwnerSts
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Initialized</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_Initialized
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Pos01</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_Pos01
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Pos02</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_Pos02
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Pos03</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_Pos03
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Pos04</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_Pos04
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Pos05</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_Pos05
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Pos06</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_Pos06
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Pos07</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_Pos07
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Pos08</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_Pos08
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Pos09</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_Pos09
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Pos10</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_Pos10
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Pos11</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_Pos11
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Pos12</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_Pos12
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Pos13</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_Pos13
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Pos14</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_Pos14
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Pos15</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_Pos15
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Pos16</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_Pos16
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Pos17</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_Pos17
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Pos18</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_Pos18
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Pos19</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_Pos19
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Pos20</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_Pos20
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Pos21</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_Pos21
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Pos22</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_Pos22
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Pos23</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_Pos23
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Pos24</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_Pos24
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Pos25</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_Pos25
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Pos26</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_Pos26
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Pos27</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_Pos27
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Pos28</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_Pos28
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Pos29</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_Pos29
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Pos30</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_Pos30
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Moving</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_Moving
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Horn</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_Horn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Virtual</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_Virtual
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SrcQ_IO</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public SINT SrcQ_IO
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SrcQ</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public SINT SrcQ
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eCmd</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public SINT Sts_eCmd
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eFdbk</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public SINT Sts_eFdbk
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eSts</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public SINT Sts_eSts
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eFault</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public SINT Sts_eFault
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eState</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public SINT Sts_eState
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eOutPos</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public SINT Sts_eOutPos
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eOutState</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public SINT Sts_eOutState
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotify</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public SINT Sts_eNotify
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyAll</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public SINT Sts_eNotifyAll
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyIOFault</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public SINT Sts_eNotifyIOFault
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyPosFail</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public SINT Sts_eNotifyPosFail
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyLockFail</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public SINT Sts_eNotifyLockFail
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyIntlkTrip</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public SINT Sts_eNotifyIntlkTrip
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_UnackAlmCount</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public DINT Sts_UnackAlmCount
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eSrc</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public INT Sts_eSrc
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_bSrc</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public INT Sts_bSrc
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Available</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_Available
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_IntlkAvailable</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_IntlkAvailable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Bypass</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_Bypass
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_BypActive</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_BypActive
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_MaintByp</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_MaintByp
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NotRdy</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_NotRdy
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyCfgErr</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_NrdyCfgErr
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyPosFail</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_NrdyPosFail
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyLockFail</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_NrdyLockFail
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyIntlk</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_NrdyIntlk
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyIOFault</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_NrdyIOFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyOoS</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_NrdyOoS
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyPerm</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_NrdyPerm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyPrioPos01</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_NrdyPrioPos01
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Err</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_Err
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrAlm</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_ErrAlm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrExtendDelay</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_ErrExtendDelay
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrVerifyDelay</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_ErrVerifyDelay
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrPosCheckTime</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_ErrPosCheckTime
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrLockCheckTime</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_ErrLockCheckTime
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrVirtualPosTime</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_ErrVirtualPosTime
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrVirtualLockTime</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_ErrVirtualLockTime
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrVirtualCylTime</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_ErrVirtualCylTime
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Hand</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_Hand
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_OoS</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_OoS
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Maint</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_Maint
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Ovrd</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_Ovrd
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Ext</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_Ext
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Prog</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_Prog
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ProgLocked</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_ProgLocked
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Oper</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_Oper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_OperLocked</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_OperLocked
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ProgOperSel</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_ProgOperSel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ProgOperLock</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_ProgOperLock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Normal</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_Normal
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ExtReqInh</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_ExtReqInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ProgReqInh</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_ProgReqInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_MAcqRcvd</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_MAcqRcvd
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Alm</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_Alm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_AlmInh</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_AlmInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_IOFault</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_IOFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_PosFail</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_PosFail
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_LockFail</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_LockFail
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_IntlkTrip</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_IntlkTrip
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_RdyAck</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_RdyAck
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_RdyReset</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL Sts_RdyReset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Acq</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL XRdy_Acq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Rel</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL XRdy_Rel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Pos</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL XRdy_Pos
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Reset</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL XRdy_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_ResetAckAll</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public BOOL XRdy_ResetAckAll
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_Owner</c> member of the <see cref="P_DISCRETE_N_POSITION"/> data type.
    /// </summary>
    public DINT Val_Owner
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }
}