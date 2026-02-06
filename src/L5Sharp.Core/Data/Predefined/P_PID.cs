using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>P_PID</c> data type structure.
/// </summary>
[LogixData("P_PID")]
public sealed partial class P_PID : StructureData
{
    /// <summary>
    /// Creates a new <see cref="P_PID"/> instance initialized with default values.
    /// </summary>
    public P_PID() : base("P_PID")
    {
        EnableIn = new BOOL();
        EnableOut = new BOOL();
        Inp_InitializeReq = new BOOL();
        Inp_OwnerCmd = new DINT();
        Inp_PV = new REAL();
        Inp_CascSP = new REAL();
        Inp_FF = new REAL();
        Inp_FFPrev = new REAL();
        Inp_CVTrack = new REAL();
        Inp_CVInitialVal = new REAL();
        Inp_CVPrev = new REAL();
        Inp_UseFFPrev = new BOOL();
        Inp_UseCVInitialVal = new BOOL();
        Inp_UseCVPrev = new BOOL();
        Inp_WindupHi = new BOOL();
        Inp_WindupLo = new BOOL();
        Inp_InnerAvailable = new BOOL();
        Inp_IntlkOK = new BOOL();
        Inp_NBIntlkOK = new BOOL();
        Inp_IntlkAvailable = new BOOL();
        Inp_IntlkTripInh = new BOOL();
        Inp_RdyReset = new BOOL();
        Inp_CVIOFault = new BOOL();
        Inp_PVBad = new BOOL();
        Inp_PVUncertain = new BOOL();
        Inp_PVSrcQ = new SINT();
        Inp_PVNotify = new SINT();
        Inp_CVNotify = new SINT();
        Inp_CascSPNotify = new SINT();
        Inp_HiHiDevGate = new BOOL();
        Inp_HiDevGate = new BOOL();
        Inp_LoDevGate = new BOOL();
        Inp_LoLoDevGate = new BOOL();
        Inp_Hand = new BOOL();
        Inp_HandFdbk = new REAL();
        Inp_HandFdbkBad = new BOOL();
        Inp_Ovrd = new BOOL();
        Inp_OvrdCmd = new SINT();
        Inp_OvrdSP = new REAL();
        Inp_OvrdSPTarget = new REAL();
        Inp_OvrdSPRampTime = new REAL();
        Inp_OvrdRatio = new REAL();
        Inp_OvrdCV = new REAL();
        Inp_ExtInh = new BOOL();
        Inp_Reset = new BOOL();
        Cfg_AllowDisable = new BOOL();
        Cfg_AllowShelve = new BOOL();
        Cfg_HasRatio = new BOOL();
        Cfg_HasCasc = new BOOL();
        Cfg_HasAuto = new BOOL();
        Cfg_HasMan = new BOOL();
        Cfg_HasSPRamp = new BOOL();
        Cfg_ExecTime = new REAL();
        Cfg_PGain = new REAL();
        Cfg_IGain = new REAL();
        Cfg_DGain = new REAL();
        Cfg_CVTrackGain = new REAL();
        Cfg_UseCVTrack = new BOOL();
        Cfg_PSPWeight = new REAL();
        Cfg_DSPWeight = new REAL();
        Cfg_PVTrack = new BOOL();
        Cfg_GainBumpless = new BOOL();
        Cfg_PositionBump = new BOOL();
        Cfg_UseESquared = new BOOL();
        Cfg_CtrlAction = new BOOL();
        Cfg_Dependent = new BOOL();
        Cfg_UseDSmoothing = new BOOL();
        Cfg_DevDB = new REAL();
        Cfg_DevDBEnter = new REAL();
        Cfg_UseIntegDevDB = new BOOL();
        Cfg_SkipCVManLim = new BOOL();
        Cfg_SkipCVManRoC = new BOOL();
        Cfg_InitializeToMan = new BOOL();
        Cfg_SetTrack = new BOOL();
        Cfg_SetTrackOvrdHand = new BOOL();
        Cfg_HasIntlkObj = new BOOL();
        Cfg_HasMoreObj = new BOOL();
        Cfg_HasHistTrend = new SINT();
        Cfg_HasCascSPNav = new BOOL();
        Cfg_HasPVNav = new BOOL();
        Cfg_HasCVNav = new BOOL();
        Cfg_HasOper = new BOOL();
        Cfg_HasOperLocked = new BOOL();
        Cfg_HasProg = new BOOL();
        Cfg_HasProgLocked = new BOOL();
        Cfg_HasExt = new BOOL();
        Cfg_HasMaint = new BOOL();
        Cfg_HasMaintOoS = new BOOL();
        Cfg_OvrdOverLock = new BOOL();
        Cfg_ExtOverLock = new BOOL();
        Cfg_eKeepLM = new SINT();
        Cfg_eKeepCV = new SINT();
        Cfg_eKeepSP = new SINT();
        Cfg_eKeepRatio = new SINT();
        Cfg_ProgPwrUp = new BOOL();
        Cfg_ProgNormal = new BOOL();
        Cfg_PCmdPriority = new BOOL();
        Cfg_PCmdProgAsLevel = new BOOL();
        Cfg_PCmdLockAsLevel = new BOOL();
        Cfg_ExtAcqAsLevel = new BOOL();
        Cfg_OvrdIntlk = new BOOL();
        Cfg_SPFailLatch = new BOOL();
        Cfg_PVFailLatch = new BOOL();
        Cfg_CVFailLatch = new BOOL();
        Cfg_LockLM = new BOOL();
        Cfg_NormLM = new SINT();
        Cfg_PwrUpLM = new SINT();
        Cfg_PVFailTrigger = new SINT();
        Cfg_IntlkTripSPAction = new SINT();
        Cfg_SPFailSPAction = new SINT();
        Cfg_PVFailSPAction = new SINT();
        Cfg_CVFailSPAction = new SINT();
        Cfg_IntlkTripCVAction = new SINT();
        Cfg_SPFailCVAction = new SINT();
        Cfg_PVFailCVAction = new SINT();
        Cfg_CVFailCVAction = new SINT();
        Cfg_IntlkTripLMAction = new SINT();
        Cfg_SPFailLMAction = new SINT();
        Cfg_PVFailLMAction = new SINT();
        Cfg_CVFailLMAction = new SINT();
        Cfg_PVDecPlcs = new SINT();
        Cfg_CVDecPlcs = new SINT();
        Cfg_RatioDecPlcs = new SINT();
        Cfg_RatioLoLim = new REAL();
        Cfg_RatioHiLim = new REAL();
        Cfg_SPLoLim = new REAL();
        Cfg_SPHiLim = new REAL();
        Cfg_SPRoCIncrLim = new REAL();
        Cfg_SPRoCDecrLim = new REAL();
        Cfg_SkipSPRoCLim = new BOOL();
        Cfg_SPRampMaxDev = new REAL();
        Cfg_PVEUMin = new REAL();
        Cfg_PVEUMax = new REAL();
        Cfg_CVEUMin = new REAL();
        Cfg_CVEUMax = new REAL();
        Cfg_CVLoLim = new REAL();
        Cfg_CVHiLim = new REAL();
        Cfg_CVRoCIncrLim = new REAL();
        Cfg_CVRoCDecrLim = new REAL();
        Cfg_MaxInactiveCV = new REAL();
        Cfg_SPIntlk = new REAL();
        Cfg_CVIntlk = new REAL();
        Cfg_SPPwrUp = new REAL();
        Cfg_CVPwrUp = new REAL();
        Cfg_CVPwrUpSel = new SINT();
        Cfg_HiHiDevLim = new REAL();
        Cfg_HiHiDevDB = new REAL();
        Cfg_HiHiDevGateDly = new REAL();
        Cfg_HiDevLim = new REAL();
        Cfg_HiDevDB = new REAL();
        Cfg_HiDevGateDly = new REAL();
        Cfg_LoDevLim = new REAL();
        Cfg_LoDevDB = new REAL();
        Cfg_LoDevGateDly = new REAL();
        Cfg_LoLoDevLim = new REAL();
        Cfg_LoLoDevDB = new REAL();
        Cfg_LoLoDevGateDly = new REAL();
        Cfg_CnfrmReqd = new SINT();
        PSet_Ratio = new REAL();
        PSet_SP = new REAL();
        PSet_SPTarget = new REAL();
        PSet_SPRampTime = new REAL();
        PSet_CV = new REAL();
        PSet_Owner = new DINT();
        XSet_Ratio = new REAL();
        XSet_SP = new REAL();
        XSet_SPTarget = new REAL();
        XSet_SPRampTime = new REAL();
        XSet_CV = new REAL();
        PCmd_Casc = new BOOL();
        PCmd_Auto = new BOOL();
        PCmd_Man = new BOOL();
        PCmd_NormLM = new BOOL();
        PCmd_SPRampStart = new BOOL();
        PCmd_SPRampStop = new BOOL();
        PCmd_Oper = new BOOL();
        PCmd_Prog = new BOOL();
        PCmd_Lock = new BOOL();
        PCmd_Unlock = new BOOL();
        PCmd_Normal = new BOOL();
        PCmd_Reset = new BOOL();
        XCmd_Casc = new BOOL();
        XCmd_Auto = new BOOL();
        XCmd_Man = new BOOL();
        XCmd_NormLM = new BOOL();
        XCmd_SPRampStart = new BOOL();
        XCmd_SPRampStop = new BOOL();
        XCmd_Acq = new BOOL();
        XCmd_Rel = new BOOL();
        XCmd_Reset = new BOOL();
        XCmd_ResetAckAll = new BOOL();
        Val_PV = new REAL();
        Val_Ratio = new REAL();
        Val_SPSet = new REAL();
        Val_SP = new REAL();
        Val_SPTarget = new REAL();
        Val_SPRampTime = new REAL();
        Val_SPRampRoC = new REAL();
        Val_SPRoCIncr = new REAL();
        Val_SPRoCDecr = new REAL();
        Val_E = new REAL();
        Val_CVSet = new REAL();
        Val_CVOut = new REAL();
        Val_PVPercent = new REAL();
        Val_SPPercent = new REAL();
        Val_EPercent = new REAL();
        Val_CVOutPercent = new REAL();
        Val_ExecTime = new REAL();
        Out_Reset = new BOOL();
        Out_OwnerSts = new DINT();
        Sts_Initialized = new BOOL();
        SrcQ_IO = new SINT();
        SrcQ = new SINT();
        Sts_eSts = new SINT();
        Sts_eFault = new SINT();
        Sts_eState = new SINT();
        Sts_eNotify = new SINT();
        Sts_eNotifyAll = new SINT();
        Sts_eNotifyHiHiDev = new SINT();
        Sts_eNotifyHiDev = new SINT();
        Sts_eNotifyLoDev = new SINT();
        Sts_eNotifyLoLoDev = new SINT();
        Sts_eNotifyFail = new SINT();
        Sts_eNotifyIntlkTrip = new SINT();
        Sts_UnackAlmCount = new DINT();
        Sts_Casc = new BOOL();
        Sts_Auto = new BOOL();
        Sts_Man = new BOOL();
        Sts_NormLM = new BOOL();
        Sts_Initializing = new BOOL();
        Sts_WindupHi = new BOOL();
        Sts_WindupLo = new BOOL();
        Sts_RatioClamped = new BOOL();
        Sts_IntlkSP = new BOOL();
        Sts_SPHeld = new BOOL();
        Sts_SPShedPV = new BOOL();
        Sts_SPShed = new BOOL();
        Sts_SPTrackPV = new BOOL();
        Sts_SPHiClamped = new BOOL();
        Sts_SPLoClamped = new BOOL();
        Sts_SPClamped = new BOOL();
        Sts_SPRampingUp = new BOOL();
        Sts_SPRampingDown = new BOOL();
        Sts_SPRamping = new BOOL();
        Sts_SPRampWizardInProgress = new BOOL();
        Sts_SkipSPRoCLim = new BOOL();
        Sts_DevDBAct = new BOOL();
        Sts_PVUncertain = new BOOL();
        Sts_PVBad = new BOOL();
        Sts_SPBad = new BOOL();
        Sts_FFBad = new BOOL();
        Sts_FFPrevBad = new BOOL();
        Sts_CVInfNaN = new BOOL();
        Sts_CVBad = new BOOL();
        Sts_CVPrevBad = new BOOL();
        Sts_HandFdbkBad = new BOOL();
        Sts_IntlkCV = new BOOL();
        Sts_CVHeld = new BOOL();
        Sts_CVShed = new BOOL();
        Sts_CVHiClamped = new BOOL();
        Sts_CVLoClamped = new BOOL();
        Sts_CVClamped = new BOOL();
        Sts_CVRampingUp = new BOOL();
        Sts_CVRampingDown = new BOOL();
        Sts_CVRamping = new BOOL();
        Sts_Active = new BOOL();
        Sts_Available = new BOOL();
        Sts_CascAvailable = new BOOL();
        Sts_ExtAvailable = new BOOL();
        Sts_IntlkAvailable = new BOOL();
        Sts_Bypass = new BOOL();
        Sts_BypActive = new BOOL();
        Sts_NotRdy = new BOOL();
        Sts_NrdyCfgErr = new BOOL();
        Sts_NrdyInit = new BOOL();
        Sts_NrdyIntlk = new BOOL();
        Sts_NrdyFail = new BOOL();
        Sts_NrdyCVFail = new BOOL();
        Sts_NrdyPVFail = new BOOL();
        Sts_NrdySPFail = new BOOL();
        Sts_NrdyOoS = new BOOL();
        Sts_MaintByp = new BOOL();
        Sts_NrdyInner = new BOOL();
        Sts_Err = new BOOL();
        Sts_ErrRatioLim = new BOOL();
        Sts_ErrSPLim = new BOOL();
        Sts_ErrCVLim = new BOOL();
        Sts_ErrPVEU = new BOOL();
        Sts_ErrCVEU = new BOOL();
        Sts_ErrDevDB = new BOOL();
        Sts_ErrExecTime = new BOOL();
        Sts_ErrPGain = new BOOL();
        Sts_ErrIGain = new BOOL();
        Sts_ErrDGain = new BOOL();
        Sts_ErrCVTrackGain = new BOOL();
        Sts_ErrPSPWeight = new BOOL();
        Sts_ErrDSPWeight = new BOOL();
        Sts_ErrSPRoCIncrLim = new BOOL();
        Sts_ErrSPRoCDecrLim = new BOOL();
        Sts_ErrCVRoCIncrLim = new BOOL();
        Sts_ErrCVRoCDecrLim = new BOOL();
        Sts_ErrSPIntlk = new BOOL();
        Sts_ErrCVIntlk = new BOOL();
        Sts_ErrSPPwrUp = new BOOL();
        Sts_ErrCVPwrUp = new BOOL();
        Sts_ErrAlm = new BOOL();
        Sts_ErrHiHiDevLim = new BOOL();
        Sts_ErrHiHiDevGateDly = new BOOL();
        Sts_ErrHiHiDevDB = new BOOL();
        Sts_ErrHiDevLim = new BOOL();
        Sts_ErrHiDevGateDly = new BOOL();
        Sts_ErrHiDevDB = new BOOL();
        Sts_ErrLoDevLim = new BOOL();
        Sts_ErrLoDevGateDly = new BOOL();
        Sts_ErrLoDevDB = new BOOL();
        Sts_ErrLoLoDevLim = new BOOL();
        Sts_ErrLoLoDevGateDly = new BOOL();
        Sts_ErrLoLoDevDB = new BOOL();
        Sts_eSrc = new INT();
        Sts_bSrc = new INT();
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
        Sts_HiHiDevCmp = new BOOL();
        Sts_HiHiDevGate = new BOOL();
        Sts_HiHiDev = new BOOL();
        Sts_HiDevCmp = new BOOL();
        Sts_HiDevGate = new BOOL();
        Sts_HiDev = new BOOL();
        Sts_LoDevCmp = new BOOL();
        Sts_LoDevGate = new BOOL();
        Sts_LoDev = new BOOL();
        Sts_LoLoDevCmp = new BOOL();
        Sts_LoLoDevGate = new BOOL();
        Sts_LoLoDev = new BOOL();
        Sts_Fail = new BOOL();
        Sts_IntlkTrip = new BOOL();
        Sts_RdyReset = new BOOL();
        Sts_RdyAck = new BOOL();
        XRdy_Acq = new BOOL();
        XRdy_Rel = new BOOL();
        XRdy_Casc = new BOOL();
        XRdy_Auto = new BOOL();
        XRdy_Man = new BOOL();
        XRdy_NormLM = new BOOL();
        XRdy_SPRampStart = new BOOL();
        XRdy_SPRampStop = new BOOL();
        XRdy_Reset = new BOOL();
        XRdy_ResetAckAll = new BOOL();
        Val_Owner = new DINT();
    }
    
    /// <summary>
    /// Creates a new <see cref="P_PID"/> instance initialized with the provided element.
    /// </summary>
    public P_PID(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 816;
    
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
        Inp_PV.UpdateData(data, offset + 9);
        Inp_CascSP.UpdateData(data, offset + 13);
        Inp_FF.UpdateData(data, offset + 17);
        Inp_FFPrev.UpdateData(data, offset + 21);
        Inp_CVTrack.UpdateData(data, offset + 25);
        Inp_CVInitialVal.UpdateData(data, offset + 29);
        Inp_CVPrev.UpdateData(data, offset + 33);
        Inp_UseFFPrev.UpdateData((data[offset + 37] & (1 << 3)) != 0);
        Inp_UseCVInitialVal.UpdateData((data[offset + 37] & (1 << 4)) != 0);
        Inp_UseCVPrev.UpdateData((data[offset + 37] & (1 << 5)) != 0);
        Inp_WindupHi.UpdateData((data[offset + 37] & (1 << 6)) != 0);
        Inp_WindupLo.UpdateData((data[offset + 37] & (1 << 7)) != 0);
        Inp_InnerAvailable.UpdateData((data[offset + 38] & (1 << 0)) != 0);
        Inp_IntlkOK.UpdateData((data[offset + 38] & (1 << 1)) != 0);
        Inp_NBIntlkOK.UpdateData((data[offset + 38] & (1 << 2)) != 0);
        Inp_IntlkAvailable.UpdateData((data[offset + 38] & (1 << 3)) != 0);
        Inp_IntlkTripInh.UpdateData((data[offset + 38] & (1 << 4)) != 0);
        Inp_RdyReset.UpdateData((data[offset + 38] & (1 << 5)) != 0);
        Inp_CVIOFault.UpdateData((data[offset + 38] & (1 << 6)) != 0);
        Inp_PVBad.UpdateData((data[offset + 38] & (1 << 7)) != 0);
        Inp_PVUncertain.UpdateData((data[offset + 39] & (1 << 0)) != 0);
        Inp_PVSrcQ.UpdateData(data, offset + 39);
        Inp_PVNotify.UpdateData(data, offset + 40);
        Inp_CVNotify.UpdateData(data, offset + 41);
        Inp_CascSPNotify.UpdateData(data, offset + 42);
        Inp_HiHiDevGate.UpdateData((data[offset + 43] & (1 << 1)) != 0);
        Inp_HiDevGate.UpdateData((data[offset + 43] & (1 << 2)) != 0);
        Inp_LoDevGate.UpdateData((data[offset + 43] & (1 << 3)) != 0);
        Inp_LoLoDevGate.UpdateData((data[offset + 43] & (1 << 4)) != 0);
        Inp_Hand.UpdateData((data[offset + 43] & (1 << 5)) != 0);
        Inp_HandFdbk.UpdateData(data, offset + 43);
        Inp_HandFdbkBad.UpdateData((data[offset + 47] & (1 << 6)) != 0);
        Inp_Ovrd.UpdateData((data[offset + 47] & (1 << 7)) != 0);
        Inp_OvrdCmd.UpdateData(data, offset + 47);
        Inp_OvrdSP.UpdateData(data, offset + 48);
        Inp_OvrdSPTarget.UpdateData(data, offset + 52);
        Inp_OvrdSPRampTime.UpdateData(data, offset + 56);
        Inp_OvrdRatio.UpdateData(data, offset + 60);
        Inp_OvrdCV.UpdateData(data, offset + 64);
        Inp_ExtInh.UpdateData((data[offset + 69] & (1 << 0)) != 0);
        Inp_Reset.UpdateData((data[offset + 69] & (1 << 1)) != 0);
        Cfg_AllowDisable.UpdateData((data[offset + 69] & (1 << 2)) != 0);
        Cfg_AllowShelve.UpdateData((data[offset + 69] & (1 << 3)) != 0);
        Cfg_HasRatio.UpdateData((data[offset + 69] & (1 << 4)) != 0);
        Cfg_HasCasc.UpdateData((data[offset + 69] & (1 << 5)) != 0);
        Cfg_HasAuto.UpdateData((data[offset + 69] & (1 << 6)) != 0);
        Cfg_HasMan.UpdateData((data[offset + 69] & (1 << 7)) != 0);
        Cfg_HasSPRamp.UpdateData((data[offset + 70] & (1 << 0)) != 0);
        Cfg_ExecTime.UpdateData(data, offset + 70);
        Cfg_PGain.UpdateData(data, offset + 74);
        Cfg_IGain.UpdateData(data, offset + 78);
        Cfg_DGain.UpdateData(data, offset + 82);
        Cfg_CVTrackGain.UpdateData(data, offset + 86);
        Cfg_UseCVTrack.UpdateData((data[offset + 90] & (1 << 1)) != 0);
        Cfg_PSPWeight.UpdateData(data, offset + 90);
        Cfg_DSPWeight.UpdateData(data, offset + 94);
        Cfg_PVTrack.UpdateData((data[offset + 98] & (1 << 2)) != 0);
        Cfg_GainBumpless.UpdateData((data[offset + 102] & (1 << 3)) != 0);
        Cfg_PositionBump.UpdateData((data[offset + 102] & (1 << 4)) != 0);
        Cfg_UseESquared.UpdateData((data[offset + 102] & (1 << 5)) != 0);
        Cfg_CtrlAction.UpdateData((data[offset + 102] & (1 << 6)) != 0);
        Cfg_Dependent.UpdateData((data[offset + 102] & (1 << 7)) != 0);
        Cfg_UseDSmoothing.UpdateData((data[offset + 103] & (1 << 0)) != 0);
        Cfg_DevDB.UpdateData(data, offset + 103);
        Cfg_DevDBEnter.UpdateData(data, offset + 107);
        Cfg_UseIntegDevDB.UpdateData((data[offset + 111] & (1 << 1)) != 0);
        Cfg_SkipCVManLim.UpdateData((data[offset + 111] & (1 << 2)) != 0);
        Cfg_SkipCVManRoC.UpdateData((data[offset + 111] & (1 << 3)) != 0);
        Cfg_InitializeToMan.UpdateData((data[offset + 111] & (1 << 4)) != 0);
        Cfg_SetTrack.UpdateData((data[offset + 111] & (1 << 5)) != 0);
        Cfg_SetTrackOvrdHand.UpdateData((data[offset + 111] & (1 << 6)) != 0);
        Cfg_HasIntlkObj.UpdateData((data[offset + 111] & (1 << 7)) != 0);
        Cfg_HasMoreObj.UpdateData((data[offset + 112] & (1 << 0)) != 0);
        Cfg_HasHistTrend.UpdateData(data, offset + 112);
        Cfg_HasCascSPNav.UpdateData((data[offset + 113] & (1 << 1)) != 0);
        Cfg_HasPVNav.UpdateData((data[offset + 113] & (1 << 2)) != 0);
        Cfg_HasCVNav.UpdateData((data[offset + 113] & (1 << 3)) != 0);
        Cfg_HasOper.UpdateData((data[offset + 113] & (1 << 4)) != 0);
        Cfg_HasOperLocked.UpdateData((data[offset + 113] & (1 << 5)) != 0);
        Cfg_HasProg.UpdateData((data[offset + 113] & (1 << 6)) != 0);
        Cfg_HasProgLocked.UpdateData((data[offset + 113] & (1 << 7)) != 0);
        Cfg_HasExt.UpdateData((data[offset + 114] & (1 << 0)) != 0);
        Cfg_HasMaint.UpdateData((data[offset + 114] & (1 << 1)) != 0);
        Cfg_HasMaintOoS.UpdateData((data[offset + 114] & (1 << 2)) != 0);
        Cfg_OvrdOverLock.UpdateData((data[offset + 114] & (1 << 3)) != 0);
        Cfg_ExtOverLock.UpdateData((data[offset + 114] & (1 << 4)) != 0);
        Cfg_eKeepLM.UpdateData(data, offset + 114);
        Cfg_eKeepCV.UpdateData(data, offset + 115);
        Cfg_eKeepSP.UpdateData(data, offset + 116);
        Cfg_eKeepRatio.UpdateData(data, offset + 117);
        Cfg_ProgPwrUp.UpdateData((data[offset + 118] & (1 << 5)) != 0);
        Cfg_ProgNormal.UpdateData((data[offset + 118] & (1 << 6)) != 0);
        Cfg_PCmdPriority.UpdateData((data[offset + 118] & (1 << 7)) != 0);
        Cfg_PCmdProgAsLevel.UpdateData((data[offset + 119] & (1 << 0)) != 0);
        Cfg_PCmdLockAsLevel.UpdateData((data[offset + 119] & (1 << 1)) != 0);
        Cfg_ExtAcqAsLevel.UpdateData((data[offset + 119] & (1 << 2)) != 0);
        Cfg_OvrdIntlk.UpdateData((data[offset + 119] & (1 << 3)) != 0);
        Cfg_SPFailLatch.UpdateData((data[offset + 119] & (1 << 4)) != 0);
        Cfg_PVFailLatch.UpdateData((data[offset + 119] & (1 << 5)) != 0);
        Cfg_CVFailLatch.UpdateData((data[offset + 119] & (1 << 6)) != 0);
        Cfg_LockLM.UpdateData((data[offset + 119] & (1 << 7)) != 0);
        Cfg_NormLM.UpdateData(data, offset + 119);
        Cfg_PwrUpLM.UpdateData(data, offset + 120);
        Cfg_PVFailTrigger.UpdateData(data, offset + 121);
        Cfg_IntlkTripSPAction.UpdateData(data, offset + 122);
        Cfg_SPFailSPAction.UpdateData(data, offset + 123);
        Cfg_PVFailSPAction.UpdateData(data, offset + 124);
        Cfg_CVFailSPAction.UpdateData(data, offset + 125);
        Cfg_IntlkTripCVAction.UpdateData(data, offset + 126);
        Cfg_SPFailCVAction.UpdateData(data, offset + 127);
        Cfg_PVFailCVAction.UpdateData(data, offset + 128);
        Cfg_CVFailCVAction.UpdateData(data, offset + 129);
        Cfg_IntlkTripLMAction.UpdateData(data, offset + 130);
        Cfg_SPFailLMAction.UpdateData(data, offset + 131);
        Cfg_PVFailLMAction.UpdateData(data, offset + 132);
        Cfg_CVFailLMAction.UpdateData(data, offset + 133);
        Cfg_PVDecPlcs.UpdateData(data, offset + 134);
        Cfg_CVDecPlcs.UpdateData(data, offset + 135);
        Cfg_RatioDecPlcs.UpdateData(data, offset + 136);
        Cfg_RatioLoLim.UpdateData(data, offset + 137);
        Cfg_RatioHiLim.UpdateData(data, offset + 141);
        Cfg_SPLoLim.UpdateData(data, offset + 145);
        Cfg_SPHiLim.UpdateData(data, offset + 149);
        Cfg_SPRoCIncrLim.UpdateData(data, offset + 153);
        Cfg_SPRoCDecrLim.UpdateData(data, offset + 157);
        Cfg_SkipSPRoCLim.UpdateData((data[offset + 162] & (1 << 0)) != 0);
        Cfg_SPRampMaxDev.UpdateData(data, offset + 162);
        Cfg_PVEUMin.UpdateData(data, offset + 166);
        Cfg_PVEUMax.UpdateData(data, offset + 170);
        Cfg_CVEUMin.UpdateData(data, offset + 174);
        Cfg_CVEUMax.UpdateData(data, offset + 178);
        Cfg_CVLoLim.UpdateData(data, offset + 182);
        Cfg_CVHiLim.UpdateData(data, offset + 186);
        Cfg_CVRoCIncrLim.UpdateData(data, offset + 190);
        Cfg_CVRoCDecrLim.UpdateData(data, offset + 194);
        Cfg_MaxInactiveCV.UpdateData(data, offset + 198);
        Cfg_SPIntlk.UpdateData(data, offset + 202);
        Cfg_CVIntlk.UpdateData(data, offset + 206);
        Cfg_SPPwrUp.UpdateData(data, offset + 210);
        Cfg_CVPwrUp.UpdateData(data, offset + 214);
        Cfg_CVPwrUpSel.UpdateData(data, offset + 218);
        Cfg_HiHiDevLim.UpdateData(data, offset + 219);
        Cfg_HiHiDevDB.UpdateData(data, offset + 223);
        Cfg_HiHiDevGateDly.UpdateData(data, offset + 227);
        Cfg_HiDevLim.UpdateData(data, offset + 231);
        Cfg_HiDevDB.UpdateData(data, offset + 235);
        Cfg_HiDevGateDly.UpdateData(data, offset + 239);
        Cfg_LoDevLim.UpdateData(data, offset + 243);
        Cfg_LoDevDB.UpdateData(data, offset + 247);
        Cfg_LoDevGateDly.UpdateData(data, offset + 251);
        Cfg_LoLoDevLim.UpdateData(data, offset + 255);
        Cfg_LoLoDevDB.UpdateData(data, offset + 259);
        Cfg_LoLoDevGateDly.UpdateData(data, offset + 263);
        Cfg_CnfrmReqd.UpdateData(data, offset + 267);
        PSet_Ratio.UpdateData(data, offset + 268);
        PSet_SP.UpdateData(data, offset + 272);
        PSet_SPTarget.UpdateData(data, offset + 276);
        PSet_SPRampTime.UpdateData(data, offset + 280);
        PSet_CV.UpdateData(data, offset + 284);
        PSet_Owner.UpdateData(data, offset + 288);
        XSet_Ratio.UpdateData(data, offset + 292);
        XSet_SP.UpdateData(data, offset + 296);
        XSet_SPTarget.UpdateData(data, offset + 300);
        XSet_SPRampTime.UpdateData(data, offset + 304);
        XSet_CV.UpdateData(data, offset + 308);
        PCmd_Casc.UpdateData((data[offset + 312] & (1 << 1)) != 0);
        PCmd_Auto.UpdateData((data[offset + 312] & (1 << 2)) != 0);
        PCmd_Man.UpdateData((data[offset + 312] & (1 << 3)) != 0);
        PCmd_NormLM.UpdateData((data[offset + 312] & (1 << 4)) != 0);
        PCmd_SPRampStart.UpdateData((data[offset + 312] & (1 << 5)) != 0);
        PCmd_SPRampStop.UpdateData((data[offset + 312] & (1 << 6)) != 0);
        PCmd_Oper.UpdateData((data[offset + 312] & (1 << 7)) != 0);
        PCmd_Prog.UpdateData((data[offset + 313] & (1 << 0)) != 0);
        PCmd_Lock.UpdateData((data[offset + 313] & (1 << 1)) != 0);
        PCmd_Unlock.UpdateData((data[offset + 313] & (1 << 2)) != 0);
        PCmd_Normal.UpdateData((data[offset + 313] & (1 << 3)) != 0);
        PCmd_Reset.UpdateData((data[offset + 313] & (1 << 4)) != 0);
        XCmd_Casc.UpdateData((data[offset + 313] & (1 << 5)) != 0);
        XCmd_Auto.UpdateData((data[offset + 313] & (1 << 6)) != 0);
        XCmd_Man.UpdateData((data[offset + 317] & (1 << 7)) != 0);
        XCmd_NormLM.UpdateData((data[offset + 318] & (1 << 0)) != 0);
        XCmd_SPRampStart.UpdateData((data[offset + 318] & (1 << 1)) != 0);
        XCmd_SPRampStop.UpdateData((data[offset + 318] & (1 << 2)) != 0);
        XCmd_Acq.UpdateData((data[offset + 318] & (1 << 3)) != 0);
        XCmd_Rel.UpdateData((data[offset + 318] & (1 << 4)) != 0);
        XCmd_Reset.UpdateData((data[offset + 318] & (1 << 5)) != 0);
        XCmd_ResetAckAll.UpdateData((data[offset + 318] & (1 << 6)) != 0);
        Val_PV.UpdateData(data, offset + 318);
        Val_Ratio.UpdateData(data, offset + 322);
        Val_SPSet.UpdateData(data, offset + 326);
        Val_SP.UpdateData(data, offset + 330);
        Val_SPTarget.UpdateData(data, offset + 334);
        Val_SPRampTime.UpdateData(data, offset + 338);
        Val_SPRampRoC.UpdateData(data, offset + 342);
        Val_SPRoCIncr.UpdateData(data, offset + 346);
        Val_SPRoCDecr.UpdateData(data, offset + 350);
        Val_E.UpdateData(data, offset + 354);
        Val_CVSet.UpdateData(data, offset + 358);
        Val_CVOut.UpdateData(data, offset + 362);
        Val_PVPercent.UpdateData(data, offset + 366);
        Val_SPPercent.UpdateData(data, offset + 370);
        Val_EPercent.UpdateData(data, offset + 374);
        Val_CVOutPercent.UpdateData(data, offset + 378);
        Val_ExecTime.UpdateData(data, offset + 382);
        Out_Reset.UpdateData((data[offset + 386] & (1 << 7)) != 0);
        Out_OwnerSts.UpdateData(data, offset + 386);
        Sts_Initialized.UpdateData((data[offset + 391] & (1 << 0)) != 0);
        SrcQ_IO.UpdateData(data, offset + 391);
        SrcQ.UpdateData(data, offset + 392);
        Sts_eSts.UpdateData(data, offset + 393);
        Sts_eFault.UpdateData(data, offset + 394);
        Sts_eState.UpdateData(data, offset + 395);
        Sts_eNotify.UpdateData(data, offset + 396);
        Sts_eNotifyAll.UpdateData(data, offset + 397);
        Sts_eNotifyHiHiDev.UpdateData(data, offset + 398);
        Sts_eNotifyHiDev.UpdateData(data, offset + 399);
        Sts_eNotifyLoDev.UpdateData(data, offset + 400);
        Sts_eNotifyLoLoDev.UpdateData(data, offset + 401);
        Sts_eNotifyFail.UpdateData(data, offset + 402);
        Sts_eNotifyIntlkTrip.UpdateData(data, offset + 403);
        Sts_UnackAlmCount.UpdateData(data, offset + 404);
        Sts_Casc.UpdateData((data[offset + 408] & (1 << 1)) != 0);
        Sts_Auto.UpdateData((data[offset + 408] & (1 << 2)) != 0);
        Sts_Man.UpdateData((data[offset + 408] & (1 << 3)) != 0);
        Sts_NormLM.UpdateData((data[offset + 408] & (1 << 4)) != 0);
        Sts_Initializing.UpdateData((data[offset + 408] & (1 << 5)) != 0);
        Sts_WindupHi.UpdateData((data[offset + 408] & (1 << 6)) != 0);
        Sts_WindupLo.UpdateData((data[offset + 408] & (1 << 7)) != 0);
        Sts_RatioClamped.UpdateData((data[offset + 409] & (1 << 0)) != 0);
        Sts_IntlkSP.UpdateData((data[offset + 409] & (1 << 1)) != 0);
        Sts_SPHeld.UpdateData((data[offset + 409] & (1 << 2)) != 0);
        Sts_SPShedPV.UpdateData((data[offset + 409] & (1 << 3)) != 0);
        Sts_SPShed.UpdateData((data[offset + 409] & (1 << 4)) != 0);
        Sts_SPTrackPV.UpdateData((data[offset + 409] & (1 << 5)) != 0);
        Sts_SPHiClamped.UpdateData((data[offset + 409] & (1 << 6)) != 0);
        Sts_SPLoClamped.UpdateData((data[offset + 409] & (1 << 7)) != 0);
        Sts_SPClamped.UpdateData((data[offset + 410] & (1 << 0)) != 0);
        Sts_SPRampingUp.UpdateData((data[offset + 410] & (1 << 1)) != 0);
        Sts_SPRampingDown.UpdateData((data[offset + 410] & (1 << 2)) != 0);
        Sts_SPRamping.UpdateData((data[offset + 410] & (1 << 3)) != 0);
        Sts_SPRampWizardInProgress.UpdateData((data[offset + 410] & (1 << 4)) != 0);
        Sts_SkipSPRoCLim.UpdateData((data[offset + 410] & (1 << 5)) != 0);
        Sts_DevDBAct.UpdateData((data[offset + 410] & (1 << 6)) != 0);
        Sts_PVUncertain.UpdateData((data[offset + 410] & (1 << 7)) != 0);
        Sts_PVBad.UpdateData((data[offset + 411] & (1 << 0)) != 0);
        Sts_SPBad.UpdateData((data[offset + 415] & (1 << 1)) != 0);
        Sts_FFBad.UpdateData((data[offset + 415] & (1 << 2)) != 0);
        Sts_FFPrevBad.UpdateData((data[offset + 415] & (1 << 3)) != 0);
        Sts_CVInfNaN.UpdateData((data[offset + 415] & (1 << 4)) != 0);
        Sts_CVBad.UpdateData((data[offset + 415] & (1 << 5)) != 0);
        Sts_CVPrevBad.UpdateData((data[offset + 415] & (1 << 6)) != 0);
        Sts_HandFdbkBad.UpdateData((data[offset + 415] & (1 << 7)) != 0);
        Sts_IntlkCV.UpdateData((data[offset + 416] & (1 << 0)) != 0);
        Sts_CVHeld.UpdateData((data[offset + 416] & (1 << 1)) != 0);
        Sts_CVShed.UpdateData((data[offset + 416] & (1 << 2)) != 0);
        Sts_CVHiClamped.UpdateData((data[offset + 416] & (1 << 3)) != 0);
        Sts_CVLoClamped.UpdateData((data[offset + 416] & (1 << 4)) != 0);
        Sts_CVClamped.UpdateData((data[offset + 416] & (1 << 5)) != 0);
        Sts_CVRampingUp.UpdateData((data[offset + 416] & (1 << 6)) != 0);
        Sts_CVRampingDown.UpdateData((data[offset + 416] & (1 << 7)) != 0);
        Sts_CVRamping.UpdateData((data[offset + 417] & (1 << 0)) != 0);
        Sts_Active.UpdateData((data[offset + 417] & (1 << 1)) != 0);
        Sts_Available.UpdateData((data[offset + 417] & (1 << 2)) != 0);
        Sts_CascAvailable.UpdateData((data[offset + 417] & (1 << 3)) != 0);
        Sts_ExtAvailable.UpdateData((data[offset + 417] & (1 << 4)) != 0);
        Sts_IntlkAvailable.UpdateData((data[offset + 417] & (1 << 5)) != 0);
        Sts_Bypass.UpdateData((data[offset + 417] & (1 << 6)) != 0);
        Sts_BypActive.UpdateData((data[offset + 417] & (1 << 7)) != 0);
        Sts_NotRdy.UpdateData((data[offset + 418] & (1 << 0)) != 0);
        Sts_NrdyCfgErr.UpdateData((data[offset + 418] & (1 << 1)) != 0);
        Sts_NrdyInit.UpdateData((data[offset + 418] & (1 << 2)) != 0);
        Sts_NrdyIntlk.UpdateData((data[offset + 418] & (1 << 3)) != 0);
        Sts_NrdyFail.UpdateData((data[offset + 418] & (1 << 4)) != 0);
        Sts_NrdyCVFail.UpdateData((data[offset + 418] & (1 << 5)) != 0);
        Sts_NrdyPVFail.UpdateData((data[offset + 418] & (1 << 6)) != 0);
        Sts_NrdySPFail.UpdateData((data[offset + 418] & (1 << 7)) != 0);
        Sts_NrdyOoS.UpdateData((data[offset + 419] & (1 << 0)) != 0);
        Sts_MaintByp.UpdateData((data[offset + 423] & (1 << 1)) != 0);
        Sts_NrdyInner.UpdateData((data[offset + 423] & (1 << 2)) != 0);
        Sts_Err.UpdateData((data[offset + 423] & (1 << 3)) != 0);
        Sts_ErrRatioLim.UpdateData((data[offset + 423] & (1 << 4)) != 0);
        Sts_ErrSPLim.UpdateData((data[offset + 423] & (1 << 5)) != 0);
        Sts_ErrCVLim.UpdateData((data[offset + 423] & (1 << 6)) != 0);
        Sts_ErrPVEU.UpdateData((data[offset + 423] & (1 << 7)) != 0);
        Sts_ErrCVEU.UpdateData((data[offset + 424] & (1 << 0)) != 0);
        Sts_ErrDevDB.UpdateData((data[offset + 424] & (1 << 1)) != 0);
        Sts_ErrExecTime.UpdateData((data[offset + 424] & (1 << 2)) != 0);
        Sts_ErrPGain.UpdateData((data[offset + 424] & (1 << 3)) != 0);
        Sts_ErrIGain.UpdateData((data[offset + 424] & (1 << 4)) != 0);
        Sts_ErrDGain.UpdateData((data[offset + 424] & (1 << 5)) != 0);
        Sts_ErrCVTrackGain.UpdateData((data[offset + 424] & (1 << 6)) != 0);
        Sts_ErrPSPWeight.UpdateData((data[offset + 424] & (1 << 7)) != 0);
        Sts_ErrDSPWeight.UpdateData((data[offset + 425] & (1 << 0)) != 0);
        Sts_ErrSPRoCIncrLim.UpdateData((data[offset + 425] & (1 << 1)) != 0);
        Sts_ErrSPRoCDecrLim.UpdateData((data[offset + 425] & (1 << 2)) != 0);
        Sts_ErrCVRoCIncrLim.UpdateData((data[offset + 425] & (1 << 3)) != 0);
        Sts_ErrCVRoCDecrLim.UpdateData((data[offset + 425] & (1 << 4)) != 0);
        Sts_ErrSPIntlk.UpdateData((data[offset + 425] & (1 << 5)) != 0);
        Sts_ErrCVIntlk.UpdateData((data[offset + 425] & (1 << 6)) != 0);
        Sts_ErrSPPwrUp.UpdateData((data[offset + 425] & (1 << 7)) != 0);
        Sts_ErrCVPwrUp.UpdateData((data[offset + 426] & (1 << 0)) != 0);
        Sts_ErrAlm.UpdateData((data[offset + 426] & (1 << 1)) != 0);
        Sts_ErrHiHiDevLim.UpdateData((data[offset + 426] & (1 << 2)) != 0);
        Sts_ErrHiHiDevGateDly.UpdateData((data[offset + 426] & (1 << 3)) != 0);
        Sts_ErrHiHiDevDB.UpdateData((data[offset + 426] & (1 << 4)) != 0);
        Sts_ErrHiDevLim.UpdateData((data[offset + 426] & (1 << 5)) != 0);
        Sts_ErrHiDevGateDly.UpdateData((data[offset + 426] & (1 << 6)) != 0);
        Sts_ErrHiDevDB.UpdateData((data[offset + 426] & (1 << 7)) != 0);
        Sts_ErrLoDevLim.UpdateData((data[offset + 427] & (1 << 0)) != 0);
        Sts_ErrLoDevGateDly.UpdateData((data[offset + 431] & (1 << 1)) != 0);
        Sts_ErrLoDevDB.UpdateData((data[offset + 431] & (1 << 2)) != 0);
        Sts_ErrLoLoDevLim.UpdateData((data[offset + 431] & (1 << 3)) != 0);
        Sts_ErrLoLoDevGateDly.UpdateData((data[offset + 431] & (1 << 4)) != 0);
        Sts_ErrLoLoDevDB.UpdateData((data[offset + 431] & (1 << 5)) != 0);
        Sts_eSrc.UpdateData(data, offset + 431);
        Sts_bSrc.UpdateData(data, offset + 433);
        Sts_Hand.UpdateData((data[offset + 435] & (1 << 6)) != 0);
        Sts_OoS.UpdateData((data[offset + 435] & (1 << 7)) != 0);
        Sts_Maint.UpdateData((data[offset + 436] & (1 << 0)) != 0);
        Sts_Ovrd.UpdateData((data[offset + 436] & (1 << 1)) != 0);
        Sts_Ext.UpdateData((data[offset + 436] & (1 << 2)) != 0);
        Sts_Prog.UpdateData((data[offset + 436] & (1 << 3)) != 0);
        Sts_ProgLocked.UpdateData((data[offset + 436] & (1 << 4)) != 0);
        Sts_Oper.UpdateData((data[offset + 436] & (1 << 5)) != 0);
        Sts_OperLocked.UpdateData((data[offset + 436] & (1 << 6)) != 0);
        Sts_ProgOperSel.UpdateData((data[offset + 436] & (1 << 7)) != 0);
        Sts_ProgOperLock.UpdateData((data[offset + 437] & (1 << 0)) != 0);
        Sts_Normal.UpdateData((data[offset + 437] & (1 << 1)) != 0);
        Sts_ExtReqInh.UpdateData((data[offset + 437] & (1 << 2)) != 0);
        Sts_ProgReqInh.UpdateData((data[offset + 437] & (1 << 3)) != 0);
        Sts_MAcqRcvd.UpdateData((data[offset + 437] & (1 << 4)) != 0);
        Sts_Alm.UpdateData((data[offset + 437] & (1 << 5)) != 0);
        Sts_AlmInh.UpdateData((data[offset + 437] & (1 << 6)) != 0);
        Sts_HiHiDevCmp.UpdateData((data[offset + 437] & (1 << 7)) != 0);
        Sts_HiHiDevGate.UpdateData((data[offset + 438] & (1 << 0)) != 0);
        Sts_HiHiDev.UpdateData((data[offset + 438] & (1 << 1)) != 0);
        Sts_HiDevCmp.UpdateData((data[offset + 438] & (1 << 2)) != 0);
        Sts_HiDevGate.UpdateData((data[offset + 438] & (1 << 3)) != 0);
        Sts_HiDev.UpdateData((data[offset + 438] & (1 << 4)) != 0);
        Sts_LoDevCmp.UpdateData((data[offset + 438] & (1 << 5)) != 0);
        Sts_LoDevGate.UpdateData((data[offset + 438] & (1 << 6)) != 0);
        Sts_LoDev.UpdateData((data[offset + 438] & (1 << 7)) != 0);
        Sts_LoLoDevCmp.UpdateData((data[offset + 439] & (1 << 0)) != 0);
        Sts_LoLoDevGate.UpdateData((data[offset + 439] & (1 << 1)) != 0);
        Sts_LoLoDev.UpdateData((data[offset + 439] & (1 << 2)) != 0);
        Sts_Fail.UpdateData((data[offset + 439] & (1 << 3)) != 0);
        Sts_IntlkTrip.UpdateData((data[offset + 439] & (1 << 4)) != 0);
        Sts_RdyReset.UpdateData((data[offset + 439] & (1 << 5)) != 0);
        Sts_RdyAck.UpdateData((data[offset + 439] & (1 << 6)) != 0);
        XRdy_Acq.UpdateData((data[offset + 439] & (1 << 7)) != 0);
        XRdy_Rel.UpdateData((data[offset + 440] & (1 << 0)) != 0);
        XRdy_Casc.UpdateData((data[offset + 440] & (1 << 1)) != 0);
        XRdy_Auto.UpdateData((data[offset + 440] & (1 << 2)) != 0);
        XRdy_Man.UpdateData((data[offset + 440] & (1 << 3)) != 0);
        XRdy_NormLM.UpdateData((data[offset + 440] & (1 << 4)) != 0);
        XRdy_SPRampStart.UpdateData((data[offset + 440] & (1 << 5)) != 0);
        XRdy_SPRampStop.UpdateData((data[offset + 440] & (1 << 6)) != 0);
        XRdy_Reset.UpdateData((data[offset + 440] & (1 << 7)) != 0);
        XRdy_ResetAckAll.UpdateData((data[offset + 441] & (1 << 0)) != 0);
        Val_Owner.UpdateData(data, offset + 441);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_InitializeReq</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Inp_InitializeReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_OwnerCmd</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public DINT Inp_OwnerCmd
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PV</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Inp_PV
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_CascSP</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Inp_CascSP
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_FF</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Inp_FF
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_FFPrev</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Inp_FFPrev
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_CVTrack</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Inp_CVTrack
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_CVInitialVal</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Inp_CVInitialVal
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_CVPrev</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Inp_CVPrev
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_UseFFPrev</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Inp_UseFFPrev
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_UseCVInitialVal</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Inp_UseCVInitialVal
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_UseCVPrev</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Inp_UseCVPrev
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_WindupHi</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Inp_WindupHi
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_WindupLo</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Inp_WindupLo
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_InnerAvailable</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Inp_InnerAvailable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_IntlkOK</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Inp_IntlkOK
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_NBIntlkOK</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Inp_NBIntlkOK
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_IntlkAvailable</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Inp_IntlkAvailable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_IntlkTripInh</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Inp_IntlkTripInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_RdyReset</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Inp_RdyReset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_CVIOFault</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Inp_CVIOFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVBad</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Inp_PVBad
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVUncertain</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Inp_PVUncertain
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVSrcQ</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public SINT Inp_PVSrcQ
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVNotify</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public SINT Inp_PVNotify
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_CVNotify</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public SINT Inp_CVNotify
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_CascSPNotify</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public SINT Inp_CascSPNotify
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_HiHiDevGate</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Inp_HiHiDevGate
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_HiDevGate</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Inp_HiDevGate
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_LoDevGate</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Inp_LoDevGate
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_LoLoDevGate</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Inp_LoLoDevGate
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Hand</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Inp_Hand
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_HandFdbk</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Inp_HandFdbk
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_HandFdbkBad</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Inp_HandFdbkBad
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Ovrd</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Inp_Ovrd
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_OvrdCmd</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public SINT Inp_OvrdCmd
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_OvrdSP</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Inp_OvrdSP
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_OvrdSPTarget</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Inp_OvrdSPTarget
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_OvrdSPRampTime</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Inp_OvrdSPRampTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_OvrdRatio</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Inp_OvrdRatio
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_OvrdCV</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Inp_OvrdCV
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_ExtInh</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Inp_ExtInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Reset</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Inp_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_AllowDisable</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Cfg_AllowDisable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_AllowShelve</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Cfg_AllowShelve
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasRatio</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Cfg_HasRatio
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasCasc</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Cfg_HasCasc
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasAuto</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Cfg_HasAuto
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasMan</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Cfg_HasMan
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasSPRamp</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Cfg_HasSPRamp
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ExecTime</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Cfg_ExecTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PGain</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Cfg_PGain
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_IGain</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Cfg_IGain
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_DGain</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Cfg_DGain
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CVTrackGain</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Cfg_CVTrackGain
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_UseCVTrack</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Cfg_UseCVTrack
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PSPWeight</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Cfg_PSPWeight
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_DSPWeight</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Cfg_DSPWeight
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PVTrack</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Cfg_PVTrack
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_GainBumpless</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Cfg_GainBumpless
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PositionBump</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Cfg_PositionBump
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_UseESquared</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Cfg_UseESquared
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CtrlAction</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Cfg_CtrlAction
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_Dependent</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Cfg_Dependent
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_UseDSmoothing</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Cfg_UseDSmoothing
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_DevDB</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Cfg_DevDB
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_DevDBEnter</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Cfg_DevDBEnter
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_UseIntegDevDB</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Cfg_UseIntegDevDB
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_SkipCVManLim</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Cfg_SkipCVManLim
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_SkipCVManRoC</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Cfg_SkipCVManRoC
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_InitializeToMan</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Cfg_InitializeToMan
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_SetTrack</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Cfg_SetTrack
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_SetTrackOvrdHand</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Cfg_SetTrackOvrdHand
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasIntlkObj</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Cfg_HasIntlkObj
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasMoreObj</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Cfg_HasMoreObj
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasHistTrend</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public SINT Cfg_HasHistTrend
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasCascSPNav</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Cfg_HasCascSPNav
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasPVNav</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Cfg_HasPVNav
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasCVNav</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Cfg_HasCVNav
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasOper</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Cfg_HasOper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasOperLocked</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Cfg_HasOperLocked
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasProg</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Cfg_HasProg
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasProgLocked</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Cfg_HasProgLocked
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasExt</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Cfg_HasExt
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasMaint</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Cfg_HasMaint
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasMaintOoS</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Cfg_HasMaintOoS
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OvrdOverLock</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Cfg_OvrdOverLock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ExtOverLock</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Cfg_ExtOverLock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_eKeepLM</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public SINT Cfg_eKeepLM
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_eKeepCV</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public SINT Cfg_eKeepCV
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_eKeepSP</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public SINT Cfg_eKeepSP
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_eKeepRatio</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public SINT Cfg_eKeepRatio
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ProgPwrUp</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Cfg_ProgPwrUp
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ProgNormal</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Cfg_ProgNormal
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PCmdPriority</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Cfg_PCmdPriority
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PCmdProgAsLevel</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Cfg_PCmdProgAsLevel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PCmdLockAsLevel</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Cfg_PCmdLockAsLevel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ExtAcqAsLevel</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Cfg_ExtAcqAsLevel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OvrdIntlk</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Cfg_OvrdIntlk
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_SPFailLatch</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Cfg_SPFailLatch
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PVFailLatch</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Cfg_PVFailLatch
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CVFailLatch</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Cfg_CVFailLatch
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_LockLM</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Cfg_LockLM
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_NormLM</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public SINT Cfg_NormLM
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PwrUpLM</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public SINT Cfg_PwrUpLM
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PVFailTrigger</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public SINT Cfg_PVFailTrigger
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_IntlkTripSPAction</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public SINT Cfg_IntlkTripSPAction
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_SPFailSPAction</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public SINT Cfg_SPFailSPAction
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PVFailSPAction</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public SINT Cfg_PVFailSPAction
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CVFailSPAction</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public SINT Cfg_CVFailSPAction
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_IntlkTripCVAction</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public SINT Cfg_IntlkTripCVAction
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_SPFailCVAction</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public SINT Cfg_SPFailCVAction
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PVFailCVAction</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public SINT Cfg_PVFailCVAction
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CVFailCVAction</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public SINT Cfg_CVFailCVAction
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_IntlkTripLMAction</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public SINT Cfg_IntlkTripLMAction
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_SPFailLMAction</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public SINT Cfg_SPFailLMAction
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PVFailLMAction</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public SINT Cfg_PVFailLMAction
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CVFailLMAction</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public SINT Cfg_CVFailLMAction
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PVDecPlcs</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public SINT Cfg_PVDecPlcs
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CVDecPlcs</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public SINT Cfg_CVDecPlcs
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_RatioDecPlcs</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public SINT Cfg_RatioDecPlcs
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_RatioLoLim</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Cfg_RatioLoLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_RatioHiLim</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Cfg_RatioHiLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_SPLoLim</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Cfg_SPLoLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_SPHiLim</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Cfg_SPHiLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_SPRoCIncrLim</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Cfg_SPRoCIncrLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_SPRoCDecrLim</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Cfg_SPRoCDecrLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_SkipSPRoCLim</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Cfg_SkipSPRoCLim
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_SPRampMaxDev</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Cfg_SPRampMaxDev
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PVEUMin</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Cfg_PVEUMin
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PVEUMax</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Cfg_PVEUMax
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CVEUMin</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Cfg_CVEUMin
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CVEUMax</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Cfg_CVEUMax
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CVLoLim</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Cfg_CVLoLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CVHiLim</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Cfg_CVHiLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CVRoCIncrLim</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Cfg_CVRoCIncrLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CVRoCDecrLim</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Cfg_CVRoCDecrLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_MaxInactiveCV</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Cfg_MaxInactiveCV
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_SPIntlk</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Cfg_SPIntlk
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CVIntlk</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Cfg_CVIntlk
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_SPPwrUp</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Cfg_SPPwrUp
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CVPwrUp</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Cfg_CVPwrUp
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CVPwrUpSel</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public SINT Cfg_CVPwrUpSel
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HiHiDevLim</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Cfg_HiHiDevLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HiHiDevDB</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Cfg_HiHiDevDB
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HiHiDevGateDly</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Cfg_HiHiDevGateDly
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HiDevLim</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Cfg_HiDevLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HiDevDB</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Cfg_HiDevDB
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HiDevGateDly</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Cfg_HiDevGateDly
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_LoDevLim</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Cfg_LoDevLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_LoDevDB</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Cfg_LoDevDB
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_LoDevGateDly</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Cfg_LoDevGateDly
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_LoLoDevLim</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Cfg_LoLoDevLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_LoLoDevDB</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Cfg_LoLoDevDB
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_LoLoDevGateDly</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Cfg_LoLoDevGateDly
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CnfrmReqd</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public SINT Cfg_CnfrmReqd
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PSet_Ratio</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL PSet_Ratio
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PSet_SP</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL PSet_SP
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PSet_SPTarget</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL PSet_SPTarget
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PSet_SPRampTime</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL PSet_SPRampTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PSet_CV</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL PSet_CV
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PSet_Owner</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public DINT PSet_Owner
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XSet_Ratio</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL XSet_Ratio
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XSet_SP</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL XSet_SP
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XSet_SPTarget</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL XSet_SPTarget
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XSet_SPRampTime</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL XSet_SPRampTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XSet_CV</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL XSet_CV
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Casc</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL PCmd_Casc
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Auto</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL PCmd_Auto
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Man</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL PCmd_Man
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_NormLM</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL PCmd_NormLM
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_SPRampStart</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL PCmd_SPRampStart
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_SPRampStop</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL PCmd_SPRampStop
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Oper</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL PCmd_Oper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Prog</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL PCmd_Prog
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Lock</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL PCmd_Lock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Unlock</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL PCmd_Unlock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Normal</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL PCmd_Normal
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Reset</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL PCmd_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Casc</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL XCmd_Casc
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Auto</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL XCmd_Auto
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Man</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL XCmd_Man
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_NormLM</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL XCmd_NormLM
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_SPRampStart</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL XCmd_SPRampStart
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_SPRampStop</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL XCmd_SPRampStop
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Acq</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL XCmd_Acq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Rel</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL XCmd_Rel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Reset</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL XCmd_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_ResetAckAll</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL XCmd_ResetAckAll
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_PV</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Val_PV
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_Ratio</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Val_Ratio
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_SPSet</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Val_SPSet
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_SP</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Val_SP
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_SPTarget</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Val_SPTarget
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_SPRampTime</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Val_SPRampTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_SPRampRoC</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Val_SPRampRoC
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_SPRoCIncr</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Val_SPRoCIncr
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_SPRoCDecr</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Val_SPRoCDecr
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_E</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Val_E
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_CVSet</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Val_CVSet
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_CVOut</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Val_CVOut
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_PVPercent</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Val_PVPercent
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_SPPercent</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Val_SPPercent
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_EPercent</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Val_EPercent
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_CVOutPercent</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Val_CVOutPercent
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_ExecTime</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public REAL Val_ExecTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_Reset</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Out_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_OwnerSts</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public DINT Out_OwnerSts
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Initialized</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_Initialized
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SrcQ_IO</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public SINT SrcQ_IO
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SrcQ</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public SINT SrcQ
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eSts</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public SINT Sts_eSts
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eFault</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public SINT Sts_eFault
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eState</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public SINT Sts_eState
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotify</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public SINT Sts_eNotify
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyAll</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public SINT Sts_eNotifyAll
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyHiHiDev</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public SINT Sts_eNotifyHiHiDev
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyHiDev</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public SINT Sts_eNotifyHiDev
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyLoDev</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public SINT Sts_eNotifyLoDev
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyLoLoDev</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public SINT Sts_eNotifyLoLoDev
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyFail</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public SINT Sts_eNotifyFail
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyIntlkTrip</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public SINT Sts_eNotifyIntlkTrip
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_UnackAlmCount</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public DINT Sts_UnackAlmCount
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Casc</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_Casc
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Auto</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_Auto
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Man</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_Man
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NormLM</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_NormLM
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Initializing</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_Initializing
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_WindupHi</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_WindupHi
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_WindupLo</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_WindupLo
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_RatioClamped</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_RatioClamped
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_IntlkSP</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_IntlkSP
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_SPHeld</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_SPHeld
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_SPShedPV</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_SPShedPV
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_SPShed</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_SPShed
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_SPTrackPV</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_SPTrackPV
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_SPHiClamped</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_SPHiClamped
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_SPLoClamped</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_SPLoClamped
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_SPClamped</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_SPClamped
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_SPRampingUp</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_SPRampingUp
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_SPRampingDown</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_SPRampingDown
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_SPRamping</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_SPRamping
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_SPRampWizardInProgress</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_SPRampWizardInProgress
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_SkipSPRoCLim</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_SkipSPRoCLim
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_DevDBAct</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_DevDBAct
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_PVUncertain</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_PVUncertain
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_PVBad</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_PVBad
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_SPBad</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_SPBad
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_FFBad</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_FFBad
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_FFPrevBad</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_FFPrevBad
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_CVInfNaN</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_CVInfNaN
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_CVBad</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_CVBad
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_CVPrevBad</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_CVPrevBad
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_HandFdbkBad</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_HandFdbkBad
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_IntlkCV</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_IntlkCV
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_CVHeld</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_CVHeld
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_CVShed</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_CVShed
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_CVHiClamped</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_CVHiClamped
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_CVLoClamped</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_CVLoClamped
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_CVClamped</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_CVClamped
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_CVRampingUp</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_CVRampingUp
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_CVRampingDown</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_CVRampingDown
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_CVRamping</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_CVRamping
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Active</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_Active
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Available</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_Available
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_CascAvailable</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_CascAvailable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ExtAvailable</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_ExtAvailable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_IntlkAvailable</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_IntlkAvailable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Bypass</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_Bypass
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_BypActive</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_BypActive
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NotRdy</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_NotRdy
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyCfgErr</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_NrdyCfgErr
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyInit</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_NrdyInit
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyIntlk</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_NrdyIntlk
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyFail</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_NrdyFail
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyCVFail</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_NrdyCVFail
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyPVFail</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_NrdyPVFail
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdySPFail</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_NrdySPFail
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyOoS</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_NrdyOoS
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_MaintByp</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_MaintByp
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyInner</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_NrdyInner
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Err</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_Err
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrRatioLim</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_ErrRatioLim
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrSPLim</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_ErrSPLim
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrCVLim</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_ErrCVLim
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrPVEU</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_ErrPVEU
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrCVEU</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_ErrCVEU
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrDevDB</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_ErrDevDB
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrExecTime</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_ErrExecTime
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrPGain</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_ErrPGain
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrIGain</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_ErrIGain
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrDGain</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_ErrDGain
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrCVTrackGain</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_ErrCVTrackGain
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrPSPWeight</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_ErrPSPWeight
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrDSPWeight</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_ErrDSPWeight
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrSPRoCIncrLim</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_ErrSPRoCIncrLim
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrSPRoCDecrLim</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_ErrSPRoCDecrLim
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrCVRoCIncrLim</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_ErrCVRoCIncrLim
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrCVRoCDecrLim</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_ErrCVRoCDecrLim
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrSPIntlk</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_ErrSPIntlk
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrCVIntlk</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_ErrCVIntlk
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrSPPwrUp</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_ErrSPPwrUp
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrCVPwrUp</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_ErrCVPwrUp
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrAlm</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_ErrAlm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrHiHiDevLim</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_ErrHiHiDevLim
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrHiHiDevGateDly</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_ErrHiHiDevGateDly
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrHiHiDevDB</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_ErrHiHiDevDB
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrHiDevLim</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_ErrHiDevLim
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrHiDevGateDly</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_ErrHiDevGateDly
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrHiDevDB</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_ErrHiDevDB
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrLoDevLim</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_ErrLoDevLim
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrLoDevGateDly</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_ErrLoDevGateDly
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrLoDevDB</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_ErrLoDevDB
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrLoLoDevLim</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_ErrLoLoDevLim
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrLoLoDevGateDly</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_ErrLoLoDevGateDly
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrLoLoDevDB</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_ErrLoLoDevDB
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eSrc</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public INT Sts_eSrc
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_bSrc</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public INT Sts_bSrc
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Hand</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_Hand
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_OoS</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_OoS
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Maint</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_Maint
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Ovrd</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_Ovrd
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Ext</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_Ext
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Prog</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_Prog
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ProgLocked</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_ProgLocked
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Oper</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_Oper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_OperLocked</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_OperLocked
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ProgOperSel</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_ProgOperSel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ProgOperLock</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_ProgOperLock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Normal</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_Normal
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ExtReqInh</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_ExtReqInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ProgReqInh</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_ProgReqInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_MAcqRcvd</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_MAcqRcvd
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Alm</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_Alm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_AlmInh</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_AlmInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_HiHiDevCmp</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_HiHiDevCmp
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_HiHiDevGate</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_HiHiDevGate
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_HiHiDev</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_HiHiDev
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_HiDevCmp</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_HiDevCmp
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_HiDevGate</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_HiDevGate
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_HiDev</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_HiDev
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_LoDevCmp</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_LoDevCmp
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_LoDevGate</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_LoDevGate
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_LoDev</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_LoDev
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_LoLoDevCmp</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_LoLoDevCmp
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_LoLoDevGate</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_LoLoDevGate
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_LoLoDev</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_LoLoDev
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Fail</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_Fail
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_IntlkTrip</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_IntlkTrip
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_RdyReset</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_RdyReset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_RdyAck</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL Sts_RdyAck
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Acq</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL XRdy_Acq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Rel</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL XRdy_Rel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Casc</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL XRdy_Casc
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Auto</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL XRdy_Auto
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Man</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL XRdy_Man
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_NormLM</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL XRdy_NormLM
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_SPRampStart</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL XRdy_SPRampStart
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_SPRampStop</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL XRdy_SPRampStop
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Reset</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL XRdy_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_ResetAckAll</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public BOOL XRdy_ResetAckAll
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_Owner</c> member of the <see cref="P_PID"/> data type.
    /// </summary>
    public DINT Val_Owner
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }
}