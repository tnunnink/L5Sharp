using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>P_ANALOG_OUTPUT</c> data type structure.
/// </summary>
[LogixData("P_ANALOG_OUTPUT")]
public sealed partial class P_ANALOG_OUTPUT : StructureData
{
    /// <summary>
    /// Creates a new <see cref="P_ANALOG_OUTPUT"/> instance initialized with default values.
    /// </summary>
    public P_ANALOG_OUTPUT() : base("P_ANALOG_OUTPUT")
    {
        EnableIn = new BOOL();
        EnableOut = new BOOL();
        Inp_InitializeReq = new BOOL();
        Inp_OwnerCmd = new DINT();
        Inp_OpenedFdbkData = new BOOL();
        Inp_ClosedFdbkData = new BOOL();
        Inp_PosFdbk = new REAL();
        Inp_HandFdbk = new REAL();
        Inp_IntlkOK = new BOOL();
        Inp_NBIntlkOK = new BOOL();
        Inp_IntlkAvailable = new BOOL();
        Inp_IntlkTripInh = new BOOL();
        Inp_SmartDvcSts = new DINT();
        Inp_SmartDvcDiagAvailable = new BOOL();
        Inp_IOFault = new BOOL();
        Inp_DeviceFault = new BOOL();
        Inp_Hand = new BOOL();
        Inp_Ovrd = new BOOL();
        Inp_OvrdCV = new REAL();
        Inp_ExtInh = new BOOL();
        Inp_RdyReset = new BOOL();
        Inp_Reset = new BOOL();
        Cfg_AllowDisable = new BOOL();
        Cfg_AllowShelve = new BOOL();
        Cfg_StuckTime = new REAL();
        Cfg_HasSmartDvc = new BOOL();
        Cfg_SetTrack = new BOOL();
        Cfg_ShedHold = new BOOL();
        Cfg_SkipRoCLim = new BOOL();
        Cfg_SetTrackOvrdHand = new BOOL();
        Cfg_FdbkFail = new BOOL();
        Cfg_HasOpenedFdbk = new BOOL();
        Cfg_HasClosedFdbk = new BOOL();
        Cfg_HasPosFdbk = new BOOL();
        Cfg_UseOpenedFdbk = new BOOL();
        Cfg_UseClosedFdbk = new BOOL();
        Cfg_UsePosFdbk = new BOOL();
        Cfg_HasCombinedFdbk = new BOOL();
        Cfg_UseCombinedFdbk = new BOOL();
        Cfg_HasPulseOut = new BOOL();
        Cfg_HasOutNav = new BOOL();
        Cfg_OvrdIntlk = new BOOL();
        Cfg_ShedOnDeviceFault = new BOOL();
        Cfg_ShedOnIOFault = new BOOL();
        Cfg_CVLoLim = new REAL();
        Cfg_CVHiLim = new REAL();
        Cfg_CVRoCIncrLim = new REAL();
        Cfg_CVRoCDecrLim = new REAL();
        Cfg_CVIntlk = new REAL();
        Cfg_CVEUMin = new REAL();
        Cfg_CVEUMax = new REAL();
        Cfg_CVRawMin = new REAL();
        Cfg_CVRawMax = new REAL();
        Cfg_MaxInactiveCV = new REAL();
        Cfg_HiDevLim = new REAL();
        Cfg_LoDevLim = new REAL();
        Cfg_DevDly = new REAL();
        Cfg_CycleTime = new REAL();
        Cfg_OpenRate = new REAL();
        Cfg_CloseRate = new REAL();
        Cfg_MaxOnTime = new REAL();
        Cfg_MinOnTime = new REAL();
        Cfg_BumpTime = new REAL();
        Cfg_DeadTime = new REAL();
        Cfg_MaxClosedPos = new REAL();
        Cfg_HasIntlkObj = new BOOL();
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
        Cfg_CVDecPlcs = new SINT();
        Cfg_CnfrmReqd = new SINT();
        Cfg_CVPwrUpSel = new SINT();
        Cfg_CVPwrUp = new REAL();
        Cfg_HasMoreObj = new BOOL();
        Cfg_HasPosFdbkNav = new BOOL();
        Cfg_HasHistTrend = new SINT();
        PSet_CV = new REAL();
        PSet_Owner = new DINT();
        PCmd_Oper = new BOOL();
        PCmd_Prog = new BOOL();
        PCmd_Lock = new BOOL();
        PCmd_Unlock = new BOOL();
        PCmd_Normal = new BOOL();
        PCmd_Reset = new BOOL();
        PCmd_Physical = new BOOL();
        PCmd_Virtual = new BOOL();
        XSet_CV = new REAL();
        XCmd_Acq = new BOOL();
        XCmd_BumpClose = new BOOL();
        XCmd_BumpOpen = new BOOL();
        XCmd_Rel = new BOOL();
        XCmd_Reset = new BOOL();
        XCmd_ResetAckAll = new BOOL();
        Out_CVData = new REAL();
        Out_CVOpenData = new BOOL();
        Out_CVCloseData = new BOOL();
        Out_Reset = new BOOL();
        Val_Dev = new REAL();
        Val_Pos = new REAL();
        Val_CVSet = new REAL();
        Val_CVOut = new REAL();
        Val_CVEUMin = new REAL();
        Val_CVEUMax = new REAL();
        Out_SmartDvcSts = new DINT();
        Out_OwnerSts = new DINT();
        Sts_Initialized = new BOOL();
        Sts_SmartDvcDiagAvailable = new BOOL();
        Sts_CVInfNaN = new BOOL();
        Sts_PosInfNaN = new BOOL();
        Sts_BumpOpen = new BOOL();
        Sts_BumpClose = new BOOL();
        Sts_PosStuck = new BOOL();
        Sts_Ramping = new BOOL();
        Sts_Clamped = new BOOL();
        Sts_WindupHi = new BOOL();
        Sts_WindupLo = new BOOL();
        Sts_SkipRoCLim = new BOOL();
        Sts_Active = new BOOL();
        Sts_FdbkFail = new BOOL();
        Sts_Virtual = new BOOL();
        SrcQ_IO = new SINT();
        SrcQ = new SINT();
        Sts_eFdbk = new SINT();
        Sts_eSts = new SINT();
        Sts_eFault = new SINT();
        Sts_eNotify = new SINT();
        Sts_eNotifyAll = new SINT();
        Sts_eNotifyIOFault = new SINT();
        Sts_eNotifyDeviceFault = new SINT();
        Sts_eNotifyDev = new SINT();
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
        Sts_NrdyOoS = new BOOL();
        Sts_NrdyCfgErr = new BOOL();
        Sts_NrdyDeviceFault = new BOOL();
        Sts_NrdyIntlk = new BOOL();
        Sts_NrdyIOFault = new BOOL();
        Sts_Err = new BOOL();
        Sts_ErrCVRaw = new BOOL();
        Sts_ErrCVEU = new BOOL();
        Sts_ErrCVRoCDecrLim = new BOOL();
        Sts_ErrCVRoCIncrLim = new BOOL();
        Sts_ErrLim = new BOOL();
        Sts_ErrHiDevLim = new BOOL();
        Sts_ErrLoDevLim = new BOOL();
        Sts_ErrDevDly = new BOOL();
        Sts_ErrCVIntlk = new BOOL();
        Sts_ErrCVPwrUp = new BOOL();
        Sts_ErrCycleTime = new BOOL();
        Sts_ErrOpenRate = new BOOL();
        Sts_ErrCloseRate = new BOOL();
        Sts_ErrStuckTime = new BOOL();
        Sts_ErrMaxClosedPos = new BOOL();
        Sts_ErrMaxOnTime = new BOOL();
        Sts_ErrMinOnTime = new BOOL();
        Sts_ErrBumpTime = new BOOL();
        Sts_ErrDeadTime = new BOOL();
        Sts_ErrAlm = new BOOL();
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
        Sts_DeviceFault = new BOOL();
        Sts_Dev = new BOOL();
        Sts_IntlkTrip = new BOOL();
        Sts_RdyAck = new BOOL();
        Sts_RdyReset = new BOOL();
        XRdy_Acq = new BOOL();
        XRdy_BumpClose = new BOOL();
        XRdy_BumpOpen = new BOOL();
        XRdy_Rel = new BOOL();
        XRdy_Reset = new BOOL();
        XRdy_ResetAckAll = new BOOL();
        Val_Owner = new DINT();
    }
    
    /// <summary>
    /// Creates a new <see cref="P_ANALOG_OUTPUT"/> instance initialized with the provided element.
    /// </summary>
    public P_ANALOG_OUTPUT(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 440;
    
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
        Inp_OpenedFdbkData.UpdateData((data[offset + 9] & (1 << 3)) != 0);
        Inp_ClosedFdbkData.UpdateData((data[offset + 9] & (1 << 4)) != 0);
        Inp_PosFdbk.UpdateData(data, offset + 9);
        Inp_HandFdbk.UpdateData(data, offset + 13);
        Inp_IntlkOK.UpdateData((data[offset + 17] & (1 << 5)) != 0);
        Inp_NBIntlkOK.UpdateData((data[offset + 17] & (1 << 6)) != 0);
        Inp_IntlkAvailable.UpdateData((data[offset + 17] & (1 << 7)) != 0);
        Inp_IntlkTripInh.UpdateData((data[offset + 18] & (1 << 0)) != 0);
        Inp_SmartDvcSts.UpdateData(data, offset + 18);
        Inp_SmartDvcDiagAvailable.UpdateData((data[offset + 22] & (1 << 1)) != 0);
        Inp_IOFault.UpdateData((data[offset + 22] & (1 << 2)) != 0);
        Inp_DeviceFault.UpdateData((data[offset + 22] & (1 << 3)) != 0);
        Inp_Hand.UpdateData((data[offset + 22] & (1 << 4)) != 0);
        Inp_Ovrd.UpdateData((data[offset + 22] & (1 << 5)) != 0);
        Inp_OvrdCV.UpdateData(data, offset + 22);
        Inp_ExtInh.UpdateData((data[offset + 26] & (1 << 6)) != 0);
        Inp_RdyReset.UpdateData((data[offset + 26] & (1 << 7)) != 0);
        Inp_Reset.UpdateData((data[offset + 27] & (1 << 0)) != 0);
        Cfg_AllowDisable.UpdateData((data[offset + 27] & (1 << 1)) != 0);
        Cfg_AllowShelve.UpdateData((data[offset + 27] & (1 << 2)) != 0);
        Cfg_StuckTime.UpdateData(data, offset + 27);
        Cfg_HasSmartDvc.UpdateData((data[offset + 31] & (1 << 3)) != 0);
        Cfg_SetTrack.UpdateData((data[offset + 31] & (1 << 4)) != 0);
        Cfg_ShedHold.UpdateData((data[offset + 31] & (1 << 5)) != 0);
        Cfg_SkipRoCLim.UpdateData((data[offset + 31] & (1 << 6)) != 0);
        Cfg_SetTrackOvrdHand.UpdateData((data[offset + 31] & (1 << 7)) != 0);
        Cfg_FdbkFail.UpdateData((data[offset + 32] & (1 << 0)) != 0);
        Cfg_HasOpenedFdbk.UpdateData((data[offset + 32] & (1 << 1)) != 0);
        Cfg_HasClosedFdbk.UpdateData((data[offset + 32] & (1 << 2)) != 0);
        Cfg_HasPosFdbk.UpdateData((data[offset + 32] & (1 << 3)) != 0);
        Cfg_UseOpenedFdbk.UpdateData((data[offset + 32] & (1 << 4)) != 0);
        Cfg_UseClosedFdbk.UpdateData((data[offset + 32] & (1 << 5)) != 0);
        Cfg_UsePosFdbk.UpdateData((data[offset + 32] & (1 << 6)) != 0);
        Cfg_HasCombinedFdbk.UpdateData((data[offset + 32] & (1 << 7)) != 0);
        Cfg_UseCombinedFdbk.UpdateData((data[offset + 33] & (1 << 0)) != 0);
        Cfg_HasPulseOut.UpdateData((data[offset + 33] & (1 << 1)) != 0);
        Cfg_HasOutNav.UpdateData((data[offset + 33] & (1 << 2)) != 0);
        Cfg_OvrdIntlk.UpdateData((data[offset + 37] & (1 << 3)) != 0);
        Cfg_ShedOnDeviceFault.UpdateData((data[offset + 37] & (1 << 4)) != 0);
        Cfg_ShedOnIOFault.UpdateData((data[offset + 37] & (1 << 5)) != 0);
        Cfg_CVLoLim.UpdateData(data, offset + 37);
        Cfg_CVHiLim.UpdateData(data, offset + 41);
        Cfg_CVRoCIncrLim.UpdateData(data, offset + 45);
        Cfg_CVRoCDecrLim.UpdateData(data, offset + 49);
        Cfg_CVIntlk.UpdateData(data, offset + 53);
        Cfg_CVEUMin.UpdateData(data, offset + 57);
        Cfg_CVEUMax.UpdateData(data, offset + 61);
        Cfg_CVRawMin.UpdateData(data, offset + 65);
        Cfg_CVRawMax.UpdateData(data, offset + 69);
        Cfg_MaxInactiveCV.UpdateData(data, offset + 73);
        Cfg_HiDevLim.UpdateData(data, offset + 77);
        Cfg_LoDevLim.UpdateData(data, offset + 81);
        Cfg_DevDly.UpdateData(data, offset + 85);
        Cfg_CycleTime.UpdateData(data, offset + 89);
        Cfg_OpenRate.UpdateData(data, offset + 93);
        Cfg_CloseRate.UpdateData(data, offset + 97);
        Cfg_MaxOnTime.UpdateData(data, offset + 101);
        Cfg_MinOnTime.UpdateData(data, offset + 105);
        Cfg_BumpTime.UpdateData(data, offset + 109);
        Cfg_DeadTime.UpdateData(data, offset + 113);
        Cfg_MaxClosedPos.UpdateData(data, offset + 117);
        Cfg_HasIntlkObj.UpdateData((data[offset + 121] & (1 << 6)) != 0);
        Cfg_HasOper.UpdateData((data[offset + 121] & (1 << 7)) != 0);
        Cfg_HasOperLocked.UpdateData((data[offset + 122] & (1 << 0)) != 0);
        Cfg_HasProg.UpdateData((data[offset + 122] & (1 << 1)) != 0);
        Cfg_HasProgLocked.UpdateData((data[offset + 122] & (1 << 2)) != 0);
        Cfg_HasExt.UpdateData((data[offset + 122] & (1 << 3)) != 0);
        Cfg_HasMaint.UpdateData((data[offset + 122] & (1 << 4)) != 0);
        Cfg_HasMaintOoS.UpdateData((data[offset + 122] & (1 << 5)) != 0);
        Cfg_OvrdOverLock.UpdateData((data[offset + 122] & (1 << 6)) != 0);
        Cfg_ExtOverLock.UpdateData((data[offset + 122] & (1 << 7)) != 0);
        Cfg_ProgPwrUp.UpdateData((data[offset + 123] & (1 << 0)) != 0);
        Cfg_ProgNormal.UpdateData((data[offset + 123] & (1 << 1)) != 0);
        Cfg_PCmdPriority.UpdateData((data[offset + 123] & (1 << 2)) != 0);
        Cfg_PCmdProgAsLevel.UpdateData((data[offset + 123] & (1 << 3)) != 0);
        Cfg_PCmdLockAsLevel.UpdateData((data[offset + 123] & (1 << 4)) != 0);
        Cfg_ExtAcqAsLevel.UpdateData((data[offset + 123] & (1 << 5)) != 0);
        Cfg_CVDecPlcs.UpdateData(data, offset + 123);
        Cfg_CnfrmReqd.UpdateData(data, offset + 124);
        Cfg_CVPwrUpSel.UpdateData(data, offset + 125);
        Cfg_CVPwrUp.UpdateData(data, offset + 126);
        Cfg_HasMoreObj.UpdateData((data[offset + 130] & (1 << 6)) != 0);
        Cfg_HasPosFdbkNav.UpdateData((data[offset + 130] & (1 << 7)) != 0);
        Cfg_HasHistTrend.UpdateData(data, offset + 130);
        PSet_CV.UpdateData(data, offset + 131);
        PSet_Owner.UpdateData(data, offset + 135);
        PCmd_Oper.UpdateData((data[offset + 140] & (1 << 0)) != 0);
        PCmd_Prog.UpdateData((data[offset + 140] & (1 << 1)) != 0);
        PCmd_Lock.UpdateData((data[offset + 140] & (1 << 2)) != 0);
        PCmd_Unlock.UpdateData((data[offset + 140] & (1 << 3)) != 0);
        PCmd_Normal.UpdateData((data[offset + 140] & (1 << 4)) != 0);
        PCmd_Reset.UpdateData((data[offset + 140] & (1 << 5)) != 0);
        PCmd_Physical.UpdateData((data[offset + 140] & (1 << 6)) != 0);
        PCmd_Virtual.UpdateData((data[offset + 140] & (1 << 7)) != 0);
        XSet_CV.UpdateData(data, offset + 140);
        XCmd_Acq.UpdateData((data[offset + 145] & (1 << 0)) != 0);
        XCmd_BumpClose.UpdateData((data[offset + 145] & (1 << 1)) != 0);
        XCmd_BumpOpen.UpdateData((data[offset + 145] & (1 << 2)) != 0);
        XCmd_Rel.UpdateData((data[offset + 145] & (1 << 3)) != 0);
        XCmd_Reset.UpdateData((data[offset + 145] & (1 << 4)) != 0);
        XCmd_ResetAckAll.UpdateData((data[offset + 145] & (1 << 5)) != 0);
        Out_CVData.UpdateData(data, offset + 145);
        Out_CVOpenData.UpdateData((data[offset + 149] & (1 << 6)) != 0);
        Out_CVCloseData.UpdateData((data[offset + 149] & (1 << 7)) != 0);
        Out_Reset.UpdateData((data[offset + 150] & (1 << 0)) != 0);
        Val_Dev.UpdateData(data, offset + 150);
        Val_Pos.UpdateData(data, offset + 154);
        Val_CVSet.UpdateData(data, offset + 158);
        Val_CVOut.UpdateData(data, offset + 162);
        Val_CVEUMin.UpdateData(data, offset + 166);
        Val_CVEUMax.UpdateData(data, offset + 170);
        Out_SmartDvcSts.UpdateData(data, offset + 174);
        Out_OwnerSts.UpdateData(data, offset + 178);
        Sts_Initialized.UpdateData((data[offset + 182] & (1 << 1)) != 0);
        Sts_SmartDvcDiagAvailable.UpdateData((data[offset + 182] & (1 << 2)) != 0);
        Sts_CVInfNaN.UpdateData((data[offset + 182] & (1 << 3)) != 0);
        Sts_PosInfNaN.UpdateData((data[offset + 182] & (1 << 4)) != 0);
        Sts_BumpOpen.UpdateData((data[offset + 182] & (1 << 5)) != 0);
        Sts_BumpClose.UpdateData((data[offset + 182] & (1 << 6)) != 0);
        Sts_PosStuck.UpdateData((data[offset + 182] & (1 << 7)) != 0);
        Sts_Ramping.UpdateData((data[offset + 183] & (1 << 0)) != 0);
        Sts_Clamped.UpdateData((data[offset + 183] & (1 << 1)) != 0);
        Sts_WindupHi.UpdateData((data[offset + 183] & (1 << 2)) != 0);
        Sts_WindupLo.UpdateData((data[offset + 183] & (1 << 3)) != 0);
        Sts_SkipRoCLim.UpdateData((data[offset + 183] & (1 << 4)) != 0);
        Sts_Active.UpdateData((data[offset + 183] & (1 << 5)) != 0);
        Sts_FdbkFail.UpdateData((data[offset + 183] & (1 << 6)) != 0);
        Sts_Virtual.UpdateData((data[offset + 183] & (1 << 7)) != 0);
        SrcQ_IO.UpdateData(data, offset + 183);
        SrcQ.UpdateData(data, offset + 184);
        Sts_eFdbk.UpdateData(data, offset + 185);
        Sts_eSts.UpdateData(data, offset + 186);
        Sts_eFault.UpdateData(data, offset + 187);
        Sts_eNotify.UpdateData(data, offset + 188);
        Sts_eNotifyAll.UpdateData(data, offset + 189);
        Sts_eNotifyIOFault.UpdateData(data, offset + 190);
        Sts_eNotifyDeviceFault.UpdateData(data, offset + 191);
        Sts_eNotifyDev.UpdateData(data, offset + 192);
        Sts_eNotifyIntlkTrip.UpdateData(data, offset + 193);
        Sts_UnackAlmCount.UpdateData(data, offset + 194);
        Sts_eSrc.UpdateData(data, offset + 198);
        Sts_bSrc.UpdateData(data, offset + 200);
        Sts_Available.UpdateData((data[offset + 203] & (1 << 0)) != 0);
        Sts_IntlkAvailable.UpdateData((data[offset + 207] & (1 << 1)) != 0);
        Sts_Bypass.UpdateData((data[offset + 207] & (1 << 2)) != 0);
        Sts_BypActive.UpdateData((data[offset + 207] & (1 << 3)) != 0);
        Sts_MaintByp.UpdateData((data[offset + 207] & (1 << 4)) != 0);
        Sts_NotRdy.UpdateData((data[offset + 207] & (1 << 5)) != 0);
        Sts_NrdyOoS.UpdateData((data[offset + 207] & (1 << 6)) != 0);
        Sts_NrdyCfgErr.UpdateData((data[offset + 207] & (1 << 7)) != 0);
        Sts_NrdyDeviceFault.UpdateData((data[offset + 208] & (1 << 0)) != 0);
        Sts_NrdyIntlk.UpdateData((data[offset + 208] & (1 << 1)) != 0);
        Sts_NrdyIOFault.UpdateData((data[offset + 208] & (1 << 2)) != 0);
        Sts_Err.UpdateData((data[offset + 208] & (1 << 3)) != 0);
        Sts_ErrCVRaw.UpdateData((data[offset + 208] & (1 << 4)) != 0);
        Sts_ErrCVEU.UpdateData((data[offset + 208] & (1 << 5)) != 0);
        Sts_ErrCVRoCDecrLim.UpdateData((data[offset + 208] & (1 << 6)) != 0);
        Sts_ErrCVRoCIncrLim.UpdateData((data[offset + 208] & (1 << 7)) != 0);
        Sts_ErrLim.UpdateData((data[offset + 209] & (1 << 0)) != 0);
        Sts_ErrHiDevLim.UpdateData((data[offset + 209] & (1 << 1)) != 0);
        Sts_ErrLoDevLim.UpdateData((data[offset + 209] & (1 << 2)) != 0);
        Sts_ErrDevDly.UpdateData((data[offset + 209] & (1 << 3)) != 0);
        Sts_ErrCVIntlk.UpdateData((data[offset + 209] & (1 << 4)) != 0);
        Sts_ErrCVPwrUp.UpdateData((data[offset + 209] & (1 << 5)) != 0);
        Sts_ErrCycleTime.UpdateData((data[offset + 209] & (1 << 6)) != 0);
        Sts_ErrOpenRate.UpdateData((data[offset + 209] & (1 << 7)) != 0);
        Sts_ErrCloseRate.UpdateData((data[offset + 210] & (1 << 0)) != 0);
        Sts_ErrStuckTime.UpdateData((data[offset + 210] & (1 << 1)) != 0);
        Sts_ErrMaxClosedPos.UpdateData((data[offset + 210] & (1 << 2)) != 0);
        Sts_ErrMaxOnTime.UpdateData((data[offset + 210] & (1 << 3)) != 0);
        Sts_ErrMinOnTime.UpdateData((data[offset + 210] & (1 << 4)) != 0);
        Sts_ErrBumpTime.UpdateData((data[offset + 210] & (1 << 5)) != 0);
        Sts_ErrDeadTime.UpdateData((data[offset + 210] & (1 << 6)) != 0);
        Sts_ErrAlm.UpdateData((data[offset + 210] & (1 << 7)) != 0);
        Sts_Hand.UpdateData((data[offset + 211] & (1 << 0)) != 0);
        Sts_OoS.UpdateData((data[offset + 211] & (1 << 1)) != 0);
        Sts_Maint.UpdateData((data[offset + 211] & (1 << 2)) != 0);
        Sts_Ovrd.UpdateData((data[offset + 211] & (1 << 3)) != 0);
        Sts_Ext.UpdateData((data[offset + 211] & (1 << 4)) != 0);
        Sts_Prog.UpdateData((data[offset + 211] & (1 << 5)) != 0);
        Sts_ProgLocked.UpdateData((data[offset + 211] & (1 << 6)) != 0);
        Sts_Oper.UpdateData((data[offset + 211] & (1 << 7)) != 0);
        Sts_OperLocked.UpdateData((data[offset + 212] & (1 << 0)) != 0);
        Sts_ProgOperSel.UpdateData((data[offset + 212] & (1 << 1)) != 0);
        Sts_ProgOperLock.UpdateData((data[offset + 212] & (1 << 2)) != 0);
        Sts_Normal.UpdateData((data[offset + 212] & (1 << 3)) != 0);
        Sts_ExtReqInh.UpdateData((data[offset + 212] & (1 << 4)) != 0);
        Sts_ProgReqInh.UpdateData((data[offset + 212] & (1 << 5)) != 0);
        Sts_MAcqRcvd.UpdateData((data[offset + 212] & (1 << 6)) != 0);
        Sts_Alm.UpdateData((data[offset + 212] & (1 << 7)) != 0);
        Sts_AlmInh.UpdateData((data[offset + 217] & (1 << 0)) != 0);
        Sts_IOFault.UpdateData((data[offset + 217] & (1 << 1)) != 0);
        Sts_DeviceFault.UpdateData((data[offset + 217] & (1 << 2)) != 0);
        Sts_Dev.UpdateData((data[offset + 217] & (1 << 3)) != 0);
        Sts_IntlkTrip.UpdateData((data[offset + 217] & (1 << 4)) != 0);
        Sts_RdyAck.UpdateData((data[offset + 217] & (1 << 5)) != 0);
        Sts_RdyReset.UpdateData((data[offset + 217] & (1 << 6)) != 0);
        XRdy_Acq.UpdateData((data[offset + 217] & (1 << 7)) != 0);
        XRdy_BumpClose.UpdateData((data[offset + 218] & (1 << 0)) != 0);
        XRdy_BumpOpen.UpdateData((data[offset + 218] & (1 << 1)) != 0);
        XRdy_Rel.UpdateData((data[offset + 218] & (1 << 2)) != 0);
        XRdy_Reset.UpdateData((data[offset + 218] & (1 << 3)) != 0);
        XRdy_ResetAckAll.UpdateData((data[offset + 218] & (1 << 4)) != 0);
        Val_Owner.UpdateData(data, offset + 218);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_InitializeReq</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Inp_InitializeReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_OwnerCmd</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public DINT Inp_OwnerCmd
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_OpenedFdbkData</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Inp_OpenedFdbkData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_ClosedFdbkData</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Inp_ClosedFdbkData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PosFdbk</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public REAL Inp_PosFdbk
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_HandFdbk</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public REAL Inp_HandFdbk
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_IntlkOK</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Inp_IntlkOK
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_NBIntlkOK</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Inp_NBIntlkOK
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_IntlkAvailable</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Inp_IntlkAvailable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_IntlkTripInh</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Inp_IntlkTripInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_SmartDvcSts</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public DINT Inp_SmartDvcSts
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_SmartDvcDiagAvailable</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Inp_SmartDvcDiagAvailable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_IOFault</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Inp_IOFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_DeviceFault</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Inp_DeviceFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Hand</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Inp_Hand
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Ovrd</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Inp_Ovrd
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_OvrdCV</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public REAL Inp_OvrdCV
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_ExtInh</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Inp_ExtInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_RdyReset</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Inp_RdyReset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Reset</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Inp_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_AllowDisable</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_AllowDisable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_AllowShelve</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_AllowShelve
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_StuckTime</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public REAL Cfg_StuckTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasSmartDvc</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_HasSmartDvc
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_SetTrack</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_SetTrack
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ShedHold</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_ShedHold
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_SkipRoCLim</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_SkipRoCLim
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_SetTrackOvrdHand</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_SetTrackOvrdHand
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_FdbkFail</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_FdbkFail
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasOpenedFdbk</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_HasOpenedFdbk
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasClosedFdbk</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_HasClosedFdbk
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasPosFdbk</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_HasPosFdbk
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_UseOpenedFdbk</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_UseOpenedFdbk
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_UseClosedFdbk</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_UseClosedFdbk
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_UsePosFdbk</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_UsePosFdbk
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasCombinedFdbk</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_HasCombinedFdbk
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_UseCombinedFdbk</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_UseCombinedFdbk
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasPulseOut</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_HasPulseOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasOutNav</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_HasOutNav
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OvrdIntlk</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_OvrdIntlk
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ShedOnDeviceFault</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_ShedOnDeviceFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ShedOnIOFault</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_ShedOnIOFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CVLoLim</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public REAL Cfg_CVLoLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CVHiLim</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public REAL Cfg_CVHiLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CVRoCIncrLim</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public REAL Cfg_CVRoCIncrLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CVRoCDecrLim</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public REAL Cfg_CVRoCDecrLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CVIntlk</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public REAL Cfg_CVIntlk
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CVEUMin</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public REAL Cfg_CVEUMin
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CVEUMax</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public REAL Cfg_CVEUMax
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CVRawMin</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public REAL Cfg_CVRawMin
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CVRawMax</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public REAL Cfg_CVRawMax
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_MaxInactiveCV</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public REAL Cfg_MaxInactiveCV
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HiDevLim</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public REAL Cfg_HiDevLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_LoDevLim</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public REAL Cfg_LoDevLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_DevDly</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public REAL Cfg_DevDly
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CycleTime</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public REAL Cfg_CycleTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OpenRate</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public REAL Cfg_OpenRate
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CloseRate</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public REAL Cfg_CloseRate
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_MaxOnTime</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public REAL Cfg_MaxOnTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_MinOnTime</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public REAL Cfg_MinOnTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_BumpTime</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public REAL Cfg_BumpTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_DeadTime</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public REAL Cfg_DeadTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_MaxClosedPos</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public REAL Cfg_MaxClosedPos
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasIntlkObj</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_HasIntlkObj
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasOper</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_HasOper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasOperLocked</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_HasOperLocked
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasProg</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_HasProg
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasProgLocked</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_HasProgLocked
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasExt</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_HasExt
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasMaint</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_HasMaint
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasMaintOoS</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_HasMaintOoS
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OvrdOverLock</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_OvrdOverLock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ExtOverLock</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_ExtOverLock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ProgPwrUp</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_ProgPwrUp
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ProgNormal</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_ProgNormal
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PCmdPriority</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_PCmdPriority
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PCmdProgAsLevel</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_PCmdProgAsLevel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PCmdLockAsLevel</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_PCmdLockAsLevel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ExtAcqAsLevel</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_ExtAcqAsLevel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CVDecPlcs</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public SINT Cfg_CVDecPlcs
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CnfrmReqd</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public SINT Cfg_CnfrmReqd
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CVPwrUpSel</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public SINT Cfg_CVPwrUpSel
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CVPwrUp</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public REAL Cfg_CVPwrUp
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasMoreObj</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_HasMoreObj
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasPosFdbkNav</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_HasPosFdbkNav
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasHistTrend</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public SINT Cfg_HasHistTrend
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PSet_CV</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public REAL PSet_CV
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PSet_Owner</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public DINT PSet_Owner
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Oper</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL PCmd_Oper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Prog</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL PCmd_Prog
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Lock</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL PCmd_Lock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Unlock</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL PCmd_Unlock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Normal</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL PCmd_Normal
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Reset</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL PCmd_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Physical</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL PCmd_Physical
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Virtual</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL PCmd_Virtual
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XSet_CV</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public REAL XSet_CV
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Acq</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL XCmd_Acq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_BumpClose</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL XCmd_BumpClose
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_BumpOpen</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL XCmd_BumpOpen
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Rel</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL XCmd_Rel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Reset</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL XCmd_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_ResetAckAll</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL XCmd_ResetAckAll
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_CVData</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public REAL Out_CVData
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_CVOpenData</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Out_CVOpenData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_CVCloseData</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Out_CVCloseData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_Reset</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Out_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_Dev</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public REAL Val_Dev
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_Pos</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public REAL Val_Pos
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_CVSet</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public REAL Val_CVSet
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_CVOut</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public REAL Val_CVOut
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_CVEUMin</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public REAL Val_CVEUMin
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_CVEUMax</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public REAL Val_CVEUMax
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_SmartDvcSts</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public DINT Out_SmartDvcSts
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_OwnerSts</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public DINT Out_OwnerSts
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Initialized</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_Initialized
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_SmartDvcDiagAvailable</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_SmartDvcDiagAvailable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_CVInfNaN</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_CVInfNaN
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_PosInfNaN</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_PosInfNaN
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_BumpOpen</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_BumpOpen
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_BumpClose</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_BumpClose
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_PosStuck</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_PosStuck
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Ramping</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_Ramping
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Clamped</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_Clamped
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_WindupHi</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_WindupHi
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_WindupLo</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_WindupLo
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_SkipRoCLim</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_SkipRoCLim
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Active</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_Active
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_FdbkFail</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_FdbkFail
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Virtual</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_Virtual
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SrcQ_IO</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public SINT SrcQ_IO
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SrcQ</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public SINT SrcQ
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eFdbk</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public SINT Sts_eFdbk
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eSts</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public SINT Sts_eSts
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eFault</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public SINT Sts_eFault
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotify</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public SINT Sts_eNotify
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyAll</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public SINT Sts_eNotifyAll
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyIOFault</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public SINT Sts_eNotifyIOFault
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyDeviceFault</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public SINT Sts_eNotifyDeviceFault
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyDev</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public SINT Sts_eNotifyDev
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyIntlkTrip</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public SINT Sts_eNotifyIntlkTrip
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_UnackAlmCount</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public DINT Sts_UnackAlmCount
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eSrc</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public INT Sts_eSrc
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_bSrc</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public INT Sts_bSrc
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Available</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_Available
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_IntlkAvailable</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_IntlkAvailable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Bypass</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_Bypass
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_BypActive</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_BypActive
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_MaintByp</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_MaintByp
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NotRdy</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_NotRdy
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyOoS</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_NrdyOoS
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyCfgErr</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_NrdyCfgErr
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyDeviceFault</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_NrdyDeviceFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyIntlk</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_NrdyIntlk
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyIOFault</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_NrdyIOFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Err</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_Err
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrCVRaw</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrCVRaw
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrCVEU</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrCVEU
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrCVRoCDecrLim</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrCVRoCDecrLim
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrCVRoCIncrLim</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrCVRoCIncrLim
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrLim</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrLim
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrHiDevLim</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrHiDevLim
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrLoDevLim</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrLoDevLim
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrDevDly</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrDevDly
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrCVIntlk</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrCVIntlk
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrCVPwrUp</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrCVPwrUp
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrCycleTime</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrCycleTime
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrOpenRate</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrOpenRate
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrCloseRate</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrCloseRate
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrStuckTime</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrStuckTime
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrMaxClosedPos</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrMaxClosedPos
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrMaxOnTime</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrMaxOnTime
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrMinOnTime</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrMinOnTime
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrBumpTime</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrBumpTime
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrDeadTime</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrDeadTime
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrAlm</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrAlm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Hand</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_Hand
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_OoS</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_OoS
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Maint</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_Maint
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Ovrd</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_Ovrd
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Ext</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_Ext
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Prog</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_Prog
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ProgLocked</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_ProgLocked
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Oper</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_Oper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_OperLocked</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_OperLocked
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ProgOperSel</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_ProgOperSel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ProgOperLock</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_ProgOperLock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Normal</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_Normal
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ExtReqInh</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_ExtReqInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ProgReqInh</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_ProgReqInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_MAcqRcvd</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_MAcqRcvd
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Alm</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_Alm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_AlmInh</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_AlmInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_IOFault</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_IOFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_DeviceFault</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_DeviceFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Dev</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_Dev
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_IntlkTrip</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_IntlkTrip
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_RdyAck</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_RdyAck
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_RdyReset</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_RdyReset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Acq</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL XRdy_Acq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_BumpClose</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL XRdy_BumpClose
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_BumpOpen</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL XRdy_BumpOpen
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Rel</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL XRdy_Rel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Reset</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL XRdy_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_ResetAckAll</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public BOOL XRdy_ResetAckAll
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_Owner</c> member of the <see cref="P_ANALOG_OUTPUT"/> data type.
    /// </summary>
    public DINT Val_Owner
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }
}