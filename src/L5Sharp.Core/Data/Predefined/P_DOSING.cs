using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>P_DOSING</c> data type structure.
/// </summary>
[LogixData("P_DOSING")]
public sealed partial class P_DOSING : StructureData
{
    /// <summary>
    /// Creates a new <see cref="P_DOSING"/> instance initialized with default values.
    /// </summary>
    public P_DOSING() : base("P_DOSING")
    {
        EnableIn = new BOOL();
        EnableOut = new BOOL();
        Inp_InitializeReq = new BOOL();
        Inp_OwnerCmd = new DINT();
        Inp_RatePVBad = new BOOL();
        Inp_RatePVUncertain = new BOOL();
        Inp_QtyPVBad = new BOOL();
        Inp_QtyPVUncertain = new BOOL();
        Inp_RunFdbk = new BOOL();
        Inp_DribbleFdbk = new BOOL();
        Inp_StopFdbk = new BOOL();
        Inp_CtrldEqpFault = new BOOL();
        Inp_Reset = new BOOL();
        Inp_eRatePVSrcQ = new SINT();
        Inp_eRatePVNotify = new SINT();
        Inp_eQtyPVSrcQ = new SINT();
        Inp_eQtyPVNotify = new SINT();
        Inp_ExtInh = new BOOL();
        Cfg_AllowDisable = new BOOL();
        Cfg_AllowShelve = new BOOL();
        Cfg_HasEqpFdbk = new BOOL();
        Cfg_UseEqpFdbk = new BOOL();
        Cfg_HasDribble = new BOOL();
        Cfg_HasRatePVNav = new BOOL();
        Cfg_HasMonitoring = new BOOL();
        Cfg_HasMoreObj = new BOOL();
        Cfg_HasQtyPVNav = new BOOL();
        Cfg_AutoAdjPreact = new BOOL();
        Cfg_LossInQty = new BOOL();
        Cfg_SetTrack = new BOOL();
        Cfg_ShedOnEqpFault = new BOOL();
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
        Cfg_AutoAdjPercent = new REAL();
        Cfg_CountsPerEU = new REAL();
        Cfg_EUQtyMult = new REAL();
        Cfg_BumpTime = new REAL();
        Cfg_ClearPulseTime = new REAL();
        Cfg_FaultTime = new REAL();
        Cfg_HiFlowRateLim = new REAL();
        Cfg_LoFlowRateLim = new REAL();
        Cfg_LoRateCutoff = new REAL();
        Cfg_MaxQty = new REAL();
        Cfg_RateFiltTimeConst = new REAL();
        Cfg_RateTime = new REAL();
        Cfg_Rollover = new REAL();
        Cfg_SettleTime = new REAL();
        Cfg_VirtualRate = new REAL();
        Cfg_VirtualDribbleRate = new REAL();
        Cfg_eKeepSP = new SINT();
        Cfg_eKeepStart = new SINT();
        Cfg_eKeepTol = new SINT();
        Cfg_eKeepDribblePreact = new SINT();
        Cfg_HasHistTrend = new SINT();
        Cfg_QtyDecPlcs = new SINT();
        Cfg_RateDecPlcs = new SINT();
        Cfg_CnfrmReqd = new SINT();
        PCmd_Bump = new BOOL();
        PCmd_ClearTot = new BOOL();
        PCmd_StartTot = new BOOL();
        PCmd_StopTot = new BOOL();
        PCmd_StartFlow = new BOOL();
        PCmd_StopFlow = new BOOL();
        PCmd_CheckTol = new BOOL();
        PCmd_Lock = new BOOL();
        PCmd_Normal = new BOOL();
        PCmd_Oper = new BOOL();
        PCmd_Prog = new BOOL();
        PCmd_Unlock = new BOOL();
        PCmd_Reset = new BOOL();
        PCmd_Virtual = new BOOL();
        PCmd_Physical = new BOOL();
        PSet_Owner = new DINT();
        PSet_DribbleQty = new REAL();
        PSet_Preact = new REAL();
        PSet_SP = new REAL();
        PSet_TolHi = new REAL();
        PSet_TolLo = new REAL();
        XCmd_Bump = new BOOL();
        XCmd_ClearTot = new BOOL();
        XCmd_StartTot = new BOOL();
        XCmd_StopTot = new BOOL();
        XCmd_StartFlow = new BOOL();
        XCmd_StopFlow = new BOOL();
        XCmd_CheckTol = new BOOL();
        XCmd_Reset = new BOOL();
        XCmd_Acq = new BOOL();
        XCmd_Rel = new BOOL();
        XCmd_ResetAckAll = new BOOL();
        XSet_DribbleQty = new REAL();
        XSet_Preact = new REAL();
        XSet_SP = new REAL();
        XSet_TolHi = new REAL();
        XSet_TolLo = new REAL();
        Out_ClearTot = new BOOL();
        Out_RunTot = new BOOL();
        Out_RunFlow = new BOOL();
        Out_StopFlow = new BOOL();
        Out_DribbleFlow = new BOOL();
        Out_Reset = new BOOL();
        Out_OwnerSts = new DINT();
        Sts_Initialized = new BOOL();
        Sts_CalcQty = new BOOL();
        Sts_CalcRate = new BOOL();
        Sts_Cleared = new BOOL();
        Sts_TotRunning = new BOOL();
        Sts_FlowStarting = new BOOL();
        Sts_FlowRunning = new BOOL();
        Sts_FlowStopping = new BOOL();
        Sts_FlowStopped = new BOOL();
        Sts_DribbleStarting = new BOOL();
        Sts_FlowDribble = new BOOL();
        Sts_Bumping = new BOOL();
        Sts_InTol = new BOOL();
        Sts_Complete = new BOOL();
        Sts_Virtual = new BOOL();
        Sts_bSrc = new INT();
        Sts_eCmd = new SINT();
        Sts_eFdbk = new SINT();
        Sts_eFault = new SINT();
        Sts_eNotifyAll = new SINT();
        Sts_eNotify = new SINT();
        Sts_eNotifyEqpFault = new SINT();
        Sts_eNotifyHiFlowRate = new SINT();
        Sts_eNotifyLoFlowRate = new SINT();
        Sts_eNotifyOverTol = new SINT();
        Sts_eNotifyUnderTol = new SINT();
        Sts_eNotifyZeroFault = new SINT();
        Sts_eSrc = new INT();
        Sts_eSts = new SINT();
        Sts_UnackAlmCount = new SINT();
        Sts_Available = new BOOL();
        Sts_MaintByp = new BOOL();
        Sts_NotRdy = new BOOL();
        Sts_NrdyCfgErr = new BOOL();
        Sts_NrdyEqpFault = new BOOL();
        Sts_NrdyOoS = new BOOL();
        Sts_NrdyPVBad = new BOOL();
        Sts_Err = new BOOL();
        Sts_ErrAlm = new BOOL();
        Sts_ErrBumpTime = new BOOL();
        Sts_ErrClearPulseTime = new BOOL();
        Sts_ErrCountsPerEU = new BOOL();
        Sts_ErrCutoff = new BOOL();
        Sts_ErrEUQtyMult = new BOOL();
        Sts_ErrFaultTime = new BOOL();
        Sts_ErrInpSrc = new BOOL();
        Sts_ErrRateTime = new BOOL();
        Sts_ErrLim = new BOOL();
        Sts_ErrRollover = new BOOL();
        Sts_ErrRateFiltTimeConst = new BOOL();
        Sts_ErrSettleTime = new BOOL();
        Sts_ErrVirtual = new BOOL();
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
        Sts_EqpFault = new BOOL();
        Sts_HiFlowRate = new BOOL();
        Sts_LoFlowRate = new BOOL();
        Sts_LoRateCutoff = new BOOL();
        Sts_OverTol = new BOOL();
        Sts_UnderTol = new BOOL();
        Sts_ZeroFault = new BOOL();
        Sts_QtyBad = new BOOL();
        Sts_QtyUncertain = new BOOL();
        Sts_RateBad = new BOOL();
        Sts_RateUncertain = new BOOL();
        Sts_RdyAck = new BOOL();
        Sts_RdyReset = new BOOL();
        Val_SP = new REAL();
        Val_Remain = new REAL();
        Val_PercentComplete = new REAL();
        Val_QtyPV = new REAL();
        Val_Qty = new REAL();
        Val_RatePV = new REAL();
        Val_Rate = new REAL();
        Val_DribbleQty = new REAL();
        Val_Preact = new REAL();
        Val_TolHi = new REAL();
        Val_TolLo = new REAL();
        Val_Owner = new DINT();
        SrcQ = new SINT();
        SrcQ_IO = new SINT();
        XRdy_Acq = new BOOL();
        XRdy_Rel = new BOOL();
        XRdy_Reset = new BOOL();
        XRdy_ResetAckAll = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="P_DOSING"/> instance initialized with the provided element.
    /// </summary>
    public P_DOSING(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 492;
    
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
        Inp_RatePVBad.UpdateData((data[offset + 9] & (1 << 3)) != 0);
        Inp_RatePVUncertain.UpdateData((data[offset + 9] & (1 << 4)) != 0);
        Inp_QtyPVBad.UpdateData((data[offset + 9] & (1 << 5)) != 0);
        Inp_QtyPVUncertain.UpdateData((data[offset + 9] & (1 << 6)) != 0);
        Inp_RunFdbk.UpdateData((data[offset + 9] & (1 << 7)) != 0);
        Inp_DribbleFdbk.UpdateData((data[offset + 10] & (1 << 0)) != 0);
        Inp_StopFdbk.UpdateData((data[offset + 10] & (1 << 1)) != 0);
        Inp_CtrldEqpFault.UpdateData((data[offset + 10] & (1 << 2)) != 0);
        Inp_Reset.UpdateData((data[offset + 10] & (1 << 3)) != 0);
        Inp_eRatePVSrcQ.UpdateData(data, offset + 10);
        Inp_eRatePVNotify.UpdateData(data, offset + 11);
        Inp_eQtyPVSrcQ.UpdateData(data, offset + 12);
        Inp_eQtyPVNotify.UpdateData(data, offset + 13);
        Inp_ExtInh.UpdateData((data[offset + 14] & (1 << 4)) != 0);
        Cfg_AllowDisable.UpdateData((data[offset + 14] & (1 << 5)) != 0);
        Cfg_AllowShelve.UpdateData((data[offset + 14] & (1 << 6)) != 0);
        Cfg_HasEqpFdbk.UpdateData((data[offset + 14] & (1 << 7)) != 0);
        Cfg_UseEqpFdbk.UpdateData((data[offset + 15] & (1 << 0)) != 0);
        Cfg_HasDribble.UpdateData((data[offset + 15] & (1 << 1)) != 0);
        Cfg_HasRatePVNav.UpdateData((data[offset + 15] & (1 << 2)) != 0);
        Cfg_HasMonitoring.UpdateData((data[offset + 15] & (1 << 3)) != 0);
        Cfg_HasMoreObj.UpdateData((data[offset + 15] & (1 << 4)) != 0);
        Cfg_HasQtyPVNav.UpdateData((data[offset + 15] & (1 << 5)) != 0);
        Cfg_AutoAdjPreact.UpdateData((data[offset + 15] & (1 << 6)) != 0);
        Cfg_LossInQty.UpdateData((data[offset + 15] & (1 << 7)) != 0);
        Cfg_SetTrack.UpdateData((data[offset + 16] & (1 << 0)) != 0);
        Cfg_ShedOnEqpFault.UpdateData((data[offset + 16] & (1 << 1)) != 0);
        Cfg_HasOper.UpdateData((data[offset + 16] & (1 << 2)) != 0);
        Cfg_HasOperLocked.UpdateData((data[offset + 16] & (1 << 3)) != 0);
        Cfg_HasProg.UpdateData((data[offset + 16] & (1 << 4)) != 0);
        Cfg_HasProgLocked.UpdateData((data[offset + 16] & (1 << 5)) != 0);
        Cfg_HasExt.UpdateData((data[offset + 16] & (1 << 6)) != 0);
        Cfg_HasMaint.UpdateData((data[offset + 16] & (1 << 7)) != 0);
        Cfg_HasMaintOoS.UpdateData((data[offset + 17] & (1 << 0)) != 0);
        Cfg_OvrdOverLock.UpdateData((data[offset + 17] & (1 << 1)) != 0);
        Cfg_ExtOverLock.UpdateData((data[offset + 17] & (1 << 2)) != 0);
        Cfg_ProgPwrUp.UpdateData((data[offset + 17] & (1 << 3)) != 0);
        Cfg_ProgNormal.UpdateData((data[offset + 17] & (1 << 4)) != 0);
        Cfg_PCmdPriority.UpdateData((data[offset + 17] & (1 << 5)) != 0);
        Cfg_PCmdProgAsLevel.UpdateData((data[offset + 17] & (1 << 6)) != 0);
        Cfg_PCmdLockAsLevel.UpdateData((data[offset + 17] & (1 << 7)) != 0);
        Cfg_ExtAcqAsLevel.UpdateData((data[offset + 18] & (1 << 0)) != 0);
        Cfg_AutoAdjPercent.UpdateData(data, offset + 18);
        Cfg_CountsPerEU.UpdateData(data, offset + 22);
        Cfg_EUQtyMult.UpdateData(data, offset + 26);
        Cfg_BumpTime.UpdateData(data, offset + 30);
        Cfg_ClearPulseTime.UpdateData(data, offset + 34);
        Cfg_FaultTime.UpdateData(data, offset + 38);
        Cfg_HiFlowRateLim.UpdateData(data, offset + 42);
        Cfg_LoFlowRateLim.UpdateData(data, offset + 46);
        Cfg_LoRateCutoff.UpdateData(data, offset + 50);
        Cfg_MaxQty.UpdateData(data, offset + 54);
        Cfg_RateFiltTimeConst.UpdateData(data, offset + 58);
        Cfg_RateTime.UpdateData(data, offset + 62);
        Cfg_Rollover.UpdateData(data, offset + 66);
        Cfg_SettleTime.UpdateData(data, offset + 70);
        Cfg_VirtualRate.UpdateData(data, offset + 74);
        Cfg_VirtualDribbleRate.UpdateData(data, offset + 78);
        Cfg_eKeepSP.UpdateData(data, offset + 82);
        Cfg_eKeepStart.UpdateData(data, offset + 83);
        Cfg_eKeepTol.UpdateData(data, offset + 84);
        Cfg_eKeepDribblePreact.UpdateData(data, offset + 85);
        Cfg_HasHistTrend.UpdateData(data, offset + 86);
        Cfg_QtyDecPlcs.UpdateData(data, offset + 87);
        Cfg_RateDecPlcs.UpdateData(data, offset + 88);
        Cfg_CnfrmReqd.UpdateData(data, offset + 89);
        PCmd_Bump.UpdateData((data[offset + 90] & (1 << 1)) != 0);
        PCmd_ClearTot.UpdateData((data[offset + 90] & (1 << 2)) != 0);
        PCmd_StartTot.UpdateData((data[offset + 90] & (1 << 3)) != 0);
        PCmd_StopTot.UpdateData((data[offset + 90] & (1 << 4)) != 0);
        PCmd_StartFlow.UpdateData((data[offset + 90] & (1 << 5)) != 0);
        PCmd_StopFlow.UpdateData((data[offset + 90] & (1 << 6)) != 0);
        PCmd_CheckTol.UpdateData((data[offset + 90] & (1 << 7)) != 0);
        PCmd_Lock.UpdateData((data[offset + 91] & (1 << 0)) != 0);
        PCmd_Normal.UpdateData((data[offset + 91] & (1 << 1)) != 0);
        PCmd_Oper.UpdateData((data[offset + 91] & (1 << 2)) != 0);
        PCmd_Prog.UpdateData((data[offset + 91] & (1 << 3)) != 0);
        PCmd_Unlock.UpdateData((data[offset + 91] & (1 << 4)) != 0);
        PCmd_Reset.UpdateData((data[offset + 95] & (1 << 5)) != 0);
        PCmd_Virtual.UpdateData((data[offset + 95] & (1 << 6)) != 0);
        PCmd_Physical.UpdateData((data[offset + 95] & (1 << 7)) != 0);
        PSet_Owner.UpdateData(data, offset + 95);
        PSet_DribbleQty.UpdateData(data, offset + 99);
        PSet_Preact.UpdateData(data, offset + 103);
        PSet_SP.UpdateData(data, offset + 107);
        PSet_TolHi.UpdateData(data, offset + 111);
        PSet_TolLo.UpdateData(data, offset + 115);
        XCmd_Bump.UpdateData((data[offset + 120] & (1 << 0)) != 0);
        XCmd_ClearTot.UpdateData((data[offset + 120] & (1 << 1)) != 0);
        XCmd_StartTot.UpdateData((data[offset + 120] & (1 << 2)) != 0);
        XCmd_StopTot.UpdateData((data[offset + 120] & (1 << 3)) != 0);
        XCmd_StartFlow.UpdateData((data[offset + 120] & (1 << 4)) != 0);
        XCmd_StopFlow.UpdateData((data[offset + 120] & (1 << 5)) != 0);
        XCmd_CheckTol.UpdateData((data[offset + 120] & (1 << 6)) != 0);
        XCmd_Reset.UpdateData((data[offset + 120] & (1 << 7)) != 0);
        XCmd_Acq.UpdateData((data[offset + 121] & (1 << 0)) != 0);
        XCmd_Rel.UpdateData((data[offset + 121] & (1 << 1)) != 0);
        XCmd_ResetAckAll.UpdateData((data[offset + 121] & (1 << 2)) != 0);
        XSet_DribbleQty.UpdateData(data, offset + 121);
        XSet_Preact.UpdateData(data, offset + 125);
        XSet_SP.UpdateData(data, offset + 129);
        XSet_TolHi.UpdateData(data, offset + 133);
        XSet_TolLo.UpdateData(data, offset + 137);
        Out_ClearTot.UpdateData((data[offset + 141] & (1 << 3)) != 0);
        Out_RunTot.UpdateData((data[offset + 141] & (1 << 4)) != 0);
        Out_RunFlow.UpdateData((data[offset + 141] & (1 << 5)) != 0);
        Out_StopFlow.UpdateData((data[offset + 141] & (1 << 6)) != 0);
        Out_DribbleFlow.UpdateData((data[offset + 141] & (1 << 7)) != 0);
        Out_Reset.UpdateData((data[offset + 142] & (1 << 0)) != 0);
        Out_OwnerSts.UpdateData(data, offset + 142);
        Sts_Initialized.UpdateData((data[offset + 146] & (1 << 1)) != 0);
        Sts_CalcQty.UpdateData((data[offset + 146] & (1 << 2)) != 0);
        Sts_CalcRate.UpdateData((data[offset + 146] & (1 << 3)) != 0);
        Sts_Cleared.UpdateData((data[offset + 146] & (1 << 4)) != 0);
        Sts_TotRunning.UpdateData((data[offset + 146] & (1 << 5)) != 0);
        Sts_FlowStarting.UpdateData((data[offset + 146] & (1 << 6)) != 0);
        Sts_FlowRunning.UpdateData((data[offset + 146] & (1 << 7)) != 0);
        Sts_FlowStopping.UpdateData((data[offset + 147] & (1 << 0)) != 0);
        Sts_FlowStopped.UpdateData((data[offset + 147] & (1 << 1)) != 0);
        Sts_DribbleStarting.UpdateData((data[offset + 147] & (1 << 2)) != 0);
        Sts_FlowDribble.UpdateData((data[offset + 147] & (1 << 3)) != 0);
        Sts_Bumping.UpdateData((data[offset + 147] & (1 << 4)) != 0);
        Sts_InTol.UpdateData((data[offset + 147] & (1 << 5)) != 0);
        Sts_Complete.UpdateData((data[offset + 147] & (1 << 6)) != 0);
        Sts_Virtual.UpdateData((data[offset + 151] & (1 << 7)) != 0);
        Sts_bSrc.UpdateData(data, offset + 151);
        Sts_eCmd.UpdateData(data, offset + 153);
        Sts_eFdbk.UpdateData(data, offset + 154);
        Sts_eFault.UpdateData(data, offset + 155);
        Sts_eNotifyAll.UpdateData(data, offset + 156);
        Sts_eNotify.UpdateData(data, offset + 157);
        Sts_eNotifyEqpFault.UpdateData(data, offset + 158);
        Sts_eNotifyHiFlowRate.UpdateData(data, offset + 159);
        Sts_eNotifyLoFlowRate.UpdateData(data, offset + 160);
        Sts_eNotifyOverTol.UpdateData(data, offset + 161);
        Sts_eNotifyUnderTol.UpdateData(data, offset + 162);
        Sts_eNotifyZeroFault.UpdateData(data, offset + 163);
        Sts_eSrc.UpdateData(data, offset + 164);
        Sts_eSts.UpdateData(data, offset + 166);
        Sts_UnackAlmCount.UpdateData(data, offset + 167);
        Sts_Available.UpdateData((data[offset + 169] & (1 << 0)) != 0);
        Sts_MaintByp.UpdateData((data[offset + 169] & (1 << 1)) != 0);
        Sts_NotRdy.UpdateData((data[offset + 169] & (1 << 2)) != 0);
        Sts_NrdyCfgErr.UpdateData((data[offset + 169] & (1 << 3)) != 0);
        Sts_NrdyEqpFault.UpdateData((data[offset + 169] & (1 << 4)) != 0);
        Sts_NrdyOoS.UpdateData((data[offset + 169] & (1 << 5)) != 0);
        Sts_NrdyPVBad.UpdateData((data[offset + 169] & (1 << 6)) != 0);
        Sts_Err.UpdateData((data[offset + 169] & (1 << 7)) != 0);
        Sts_ErrAlm.UpdateData((data[offset + 170] & (1 << 0)) != 0);
        Sts_ErrBumpTime.UpdateData((data[offset + 170] & (1 << 1)) != 0);
        Sts_ErrClearPulseTime.UpdateData((data[offset + 170] & (1 << 2)) != 0);
        Sts_ErrCountsPerEU.UpdateData((data[offset + 170] & (1 << 3)) != 0);
        Sts_ErrCutoff.UpdateData((data[offset + 170] & (1 << 4)) != 0);
        Sts_ErrEUQtyMult.UpdateData((data[offset + 170] & (1 << 5)) != 0);
        Sts_ErrFaultTime.UpdateData((data[offset + 170] & (1 << 6)) != 0);
        Sts_ErrInpSrc.UpdateData((data[offset + 170] & (1 << 7)) != 0);
        Sts_ErrRateTime.UpdateData((data[offset + 171] & (1 << 0)) != 0);
        Sts_ErrLim.UpdateData((data[offset + 171] & (1 << 1)) != 0);
        Sts_ErrRollover.UpdateData((data[offset + 171] & (1 << 2)) != 0);
        Sts_ErrRateFiltTimeConst.UpdateData((data[offset + 171] & (1 << 3)) != 0);
        Sts_ErrSettleTime.UpdateData((data[offset + 171] & (1 << 4)) != 0);
        Sts_ErrVirtual.UpdateData((data[offset + 171] & (1 << 5)) != 0);
        Sts_Hand.UpdateData((data[offset + 171] & (1 << 6)) != 0);
        Sts_OoS.UpdateData((data[offset + 171] & (1 << 7)) != 0);
        Sts_Maint.UpdateData((data[offset + 172] & (1 << 0)) != 0);
        Sts_Ovrd.UpdateData((data[offset + 172] & (1 << 1)) != 0);
        Sts_Ext.UpdateData((data[offset + 172] & (1 << 2)) != 0);
        Sts_Prog.UpdateData((data[offset + 172] & (1 << 3)) != 0);
        Sts_ProgLocked.UpdateData((data[offset + 172] & (1 << 4)) != 0);
        Sts_Oper.UpdateData((data[offset + 172] & (1 << 5)) != 0);
        Sts_OperLocked.UpdateData((data[offset + 172] & (1 << 6)) != 0);
        Sts_ProgOperSel.UpdateData((data[offset + 172] & (1 << 7)) != 0);
        Sts_ProgOperLock.UpdateData((data[offset + 173] & (1 << 0)) != 0);
        Sts_Normal.UpdateData((data[offset + 173] & (1 << 1)) != 0);
        Sts_ExtReqInh.UpdateData((data[offset + 173] & (1 << 2)) != 0);
        Sts_ProgReqInh.UpdateData((data[offset + 173] & (1 << 3)) != 0);
        Sts_MAcqRcvd.UpdateData((data[offset + 173] & (1 << 4)) != 0);
        Sts_CmdConflict.UpdateData((data[offset + 173] & (1 << 5)) != 0);
        Sts_Alm.UpdateData((data[offset + 173] & (1 << 6)) != 0);
        Sts_AlmInh.UpdateData((data[offset + 173] & (1 << 7)) != 0);
        Sts_EqpFault.UpdateData((data[offset + 174] & (1 << 0)) != 0);
        Sts_HiFlowRate.UpdateData((data[offset + 174] & (1 << 1)) != 0);
        Sts_LoFlowRate.UpdateData((data[offset + 174] & (1 << 2)) != 0);
        Sts_LoRateCutoff.UpdateData((data[offset + 174] & (1 << 3)) != 0);
        Sts_OverTol.UpdateData((data[offset + 174] & (1 << 4)) != 0);
        Sts_UnderTol.UpdateData((data[offset + 174] & (1 << 5)) != 0);
        Sts_ZeroFault.UpdateData((data[offset + 178] & (1 << 6)) != 0);
        Sts_QtyBad.UpdateData((data[offset + 178] & (1 << 7)) != 0);
        Sts_QtyUncertain.UpdateData((data[offset + 179] & (1 << 0)) != 0);
        Sts_RateBad.UpdateData((data[offset + 179] & (1 << 1)) != 0);
        Sts_RateUncertain.UpdateData((data[offset + 179] & (1 << 2)) != 0);
        Sts_RdyAck.UpdateData((data[offset + 179] & (1 << 3)) != 0);
        Sts_RdyReset.UpdateData((data[offset + 179] & (1 << 4)) != 0);
        Val_SP.UpdateData(data, offset + 179);
        Val_Remain.UpdateData(data, offset + 183);
        Val_PercentComplete.UpdateData(data, offset + 187);
        Val_QtyPV.UpdateData(data, offset + 191);
        Val_Qty.UpdateData(data, offset + 195);
        Val_RatePV.UpdateData(data, offset + 199);
        Val_Rate.UpdateData(data, offset + 203);
        Val_DribbleQty.UpdateData(data, offset + 207);
        Val_Preact.UpdateData(data, offset + 211);
        Val_TolHi.UpdateData(data, offset + 215);
        Val_TolLo.UpdateData(data, offset + 219);
        Val_Owner.UpdateData(data, offset + 223);
        SrcQ.UpdateData(data, offset + 227);
        SrcQ_IO.UpdateData(data, offset + 228);
        XRdy_Acq.UpdateData((data[offset + 229] & (1 << 5)) != 0);
        XRdy_Rel.UpdateData((data[offset + 229] & (1 << 6)) != 0);
        XRdy_Reset.UpdateData((data[offset + 229] & (1 << 7)) != 0);
        XRdy_ResetAckAll.UpdateData((data[offset + 230] & (1 << 0)) != 0);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_InitializeReq</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Inp_InitializeReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_OwnerCmd</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public DINT Inp_OwnerCmd
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_RatePVBad</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Inp_RatePVBad
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_RatePVUncertain</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Inp_RatePVUncertain
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_QtyPVBad</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Inp_QtyPVBad
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_QtyPVUncertain</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Inp_QtyPVUncertain
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_RunFdbk</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Inp_RunFdbk
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_DribbleFdbk</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Inp_DribbleFdbk
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_StopFdbk</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Inp_StopFdbk
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_CtrldEqpFault</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Inp_CtrldEqpFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Reset</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Inp_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_eRatePVSrcQ</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public SINT Inp_eRatePVSrcQ
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_eRatePVNotify</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public SINT Inp_eRatePVNotify
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_eQtyPVSrcQ</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public SINT Inp_eQtyPVSrcQ
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_eQtyPVNotify</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public SINT Inp_eQtyPVNotify
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_ExtInh</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Inp_ExtInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_AllowDisable</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Cfg_AllowDisable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_AllowShelve</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Cfg_AllowShelve
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasEqpFdbk</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Cfg_HasEqpFdbk
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_UseEqpFdbk</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Cfg_UseEqpFdbk
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasDribble</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Cfg_HasDribble
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasRatePVNav</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Cfg_HasRatePVNav
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasMonitoring</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Cfg_HasMonitoring
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasMoreObj</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Cfg_HasMoreObj
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasQtyPVNav</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Cfg_HasQtyPVNav
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_AutoAdjPreact</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Cfg_AutoAdjPreact
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_LossInQty</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Cfg_LossInQty
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_SetTrack</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Cfg_SetTrack
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ShedOnEqpFault</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Cfg_ShedOnEqpFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasOper</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Cfg_HasOper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasOperLocked</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Cfg_HasOperLocked
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasProg</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Cfg_HasProg
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasProgLocked</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Cfg_HasProgLocked
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasExt</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Cfg_HasExt
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasMaint</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Cfg_HasMaint
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasMaintOoS</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Cfg_HasMaintOoS
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OvrdOverLock</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Cfg_OvrdOverLock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ExtOverLock</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Cfg_ExtOverLock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ProgPwrUp</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Cfg_ProgPwrUp
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ProgNormal</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Cfg_ProgNormal
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PCmdPriority</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Cfg_PCmdPriority
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PCmdProgAsLevel</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Cfg_PCmdProgAsLevel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PCmdLockAsLevel</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Cfg_PCmdLockAsLevel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ExtAcqAsLevel</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Cfg_ExtAcqAsLevel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_AutoAdjPercent</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public REAL Cfg_AutoAdjPercent
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CountsPerEU</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public REAL Cfg_CountsPerEU
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_EUQtyMult</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public REAL Cfg_EUQtyMult
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_BumpTime</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public REAL Cfg_BumpTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ClearPulseTime</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public REAL Cfg_ClearPulseTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_FaultTime</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public REAL Cfg_FaultTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HiFlowRateLim</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public REAL Cfg_HiFlowRateLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_LoFlowRateLim</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public REAL Cfg_LoFlowRateLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_LoRateCutoff</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public REAL Cfg_LoRateCutoff
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_MaxQty</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public REAL Cfg_MaxQty
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_RateFiltTimeConst</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public REAL Cfg_RateFiltTimeConst
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_RateTime</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public REAL Cfg_RateTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_Rollover</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public REAL Cfg_Rollover
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_SettleTime</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public REAL Cfg_SettleTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_VirtualRate</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public REAL Cfg_VirtualRate
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_VirtualDribbleRate</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public REAL Cfg_VirtualDribbleRate
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_eKeepSP</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public SINT Cfg_eKeepSP
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_eKeepStart</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public SINT Cfg_eKeepStart
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_eKeepTol</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public SINT Cfg_eKeepTol
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_eKeepDribblePreact</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public SINT Cfg_eKeepDribblePreact
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasHistTrend</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public SINT Cfg_HasHistTrend
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_QtyDecPlcs</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public SINT Cfg_QtyDecPlcs
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_RateDecPlcs</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public SINT Cfg_RateDecPlcs
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CnfrmReqd</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public SINT Cfg_CnfrmReqd
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Bump</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL PCmd_Bump
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_ClearTot</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL PCmd_ClearTot
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_StartTot</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL PCmd_StartTot
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_StopTot</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL PCmd_StopTot
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_StartFlow</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL PCmd_StartFlow
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_StopFlow</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL PCmd_StopFlow
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_CheckTol</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL PCmd_CheckTol
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Lock</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL PCmd_Lock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Normal</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL PCmd_Normal
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Oper</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL PCmd_Oper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Prog</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL PCmd_Prog
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Unlock</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL PCmd_Unlock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Reset</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL PCmd_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Virtual</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL PCmd_Virtual
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Physical</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL PCmd_Physical
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PSet_Owner</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public DINT PSet_Owner
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PSet_DribbleQty</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public REAL PSet_DribbleQty
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PSet_Preact</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public REAL PSet_Preact
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PSet_SP</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public REAL PSet_SP
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PSet_TolHi</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public REAL PSet_TolHi
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PSet_TolLo</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public REAL PSet_TolLo
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Bump</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL XCmd_Bump
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_ClearTot</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL XCmd_ClearTot
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_StartTot</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL XCmd_StartTot
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_StopTot</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL XCmd_StopTot
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_StartFlow</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL XCmd_StartFlow
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_StopFlow</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL XCmd_StopFlow
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_CheckTol</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL XCmd_CheckTol
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Reset</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL XCmd_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Acq</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL XCmd_Acq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Rel</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL XCmd_Rel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_ResetAckAll</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL XCmd_ResetAckAll
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XSet_DribbleQty</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public REAL XSet_DribbleQty
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XSet_Preact</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public REAL XSet_Preact
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XSet_SP</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public REAL XSet_SP
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XSet_TolHi</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public REAL XSet_TolHi
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XSet_TolLo</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public REAL XSet_TolLo
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_ClearTot</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Out_ClearTot
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_RunTot</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Out_RunTot
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_RunFlow</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Out_RunFlow
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_StopFlow</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Out_StopFlow
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_DribbleFlow</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Out_DribbleFlow
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_Reset</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Out_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_OwnerSts</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public DINT Out_OwnerSts
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Initialized</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_Initialized
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_CalcQty</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_CalcQty
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_CalcRate</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_CalcRate
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Cleared</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_Cleared
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_TotRunning</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_TotRunning
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_FlowStarting</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_FlowStarting
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_FlowRunning</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_FlowRunning
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_FlowStopping</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_FlowStopping
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_FlowStopped</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_FlowStopped
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_DribbleStarting</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_DribbleStarting
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_FlowDribble</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_FlowDribble
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Bumping</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_Bumping
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_InTol</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_InTol
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Complete</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_Complete
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Virtual</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_Virtual
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_bSrc</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public INT Sts_bSrc
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eCmd</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public SINT Sts_eCmd
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eFdbk</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public SINT Sts_eFdbk
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eFault</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public SINT Sts_eFault
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyAll</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public SINT Sts_eNotifyAll
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotify</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public SINT Sts_eNotify
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyEqpFault</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public SINT Sts_eNotifyEqpFault
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyHiFlowRate</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public SINT Sts_eNotifyHiFlowRate
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyLoFlowRate</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public SINT Sts_eNotifyLoFlowRate
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyOverTol</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public SINT Sts_eNotifyOverTol
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyUnderTol</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public SINT Sts_eNotifyUnderTol
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyZeroFault</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public SINT Sts_eNotifyZeroFault
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eSrc</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public INT Sts_eSrc
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eSts</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public SINT Sts_eSts
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_UnackAlmCount</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public SINT Sts_UnackAlmCount
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Available</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_Available
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_MaintByp</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_MaintByp
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NotRdy</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_NotRdy
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyCfgErr</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_NrdyCfgErr
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyEqpFault</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_NrdyEqpFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyOoS</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_NrdyOoS
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyPVBad</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_NrdyPVBad
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Err</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_Err
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrAlm</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_ErrAlm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrBumpTime</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_ErrBumpTime
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrClearPulseTime</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_ErrClearPulseTime
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrCountsPerEU</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_ErrCountsPerEU
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrCutoff</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_ErrCutoff
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrEUQtyMult</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_ErrEUQtyMult
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrFaultTime</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_ErrFaultTime
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrInpSrc</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_ErrInpSrc
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrRateTime</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_ErrRateTime
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrLim</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_ErrLim
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrRollover</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_ErrRollover
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrRateFiltTimeConst</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_ErrRateFiltTimeConst
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrSettleTime</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_ErrSettleTime
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrVirtual</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_ErrVirtual
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Hand</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_Hand
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_OoS</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_OoS
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Maint</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_Maint
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Ovrd</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_Ovrd
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Ext</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_Ext
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Prog</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_Prog
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ProgLocked</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_ProgLocked
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Oper</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_Oper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_OperLocked</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_OperLocked
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ProgOperSel</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_ProgOperSel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ProgOperLock</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_ProgOperLock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Normal</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_Normal
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ExtReqInh</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_ExtReqInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ProgReqInh</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_ProgReqInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_MAcqRcvd</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_MAcqRcvd
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_CmdConflict</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_CmdConflict
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Alm</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_Alm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_AlmInh</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_AlmInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_EqpFault</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_EqpFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_HiFlowRate</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_HiFlowRate
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_LoFlowRate</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_LoFlowRate
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_LoRateCutoff</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_LoRateCutoff
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_OverTol</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_OverTol
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_UnderTol</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_UnderTol
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ZeroFault</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_ZeroFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_QtyBad</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_QtyBad
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_QtyUncertain</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_QtyUncertain
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_RateBad</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_RateBad
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_RateUncertain</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_RateUncertain
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_RdyAck</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_RdyAck
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_RdyReset</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL Sts_RdyReset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_SP</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public REAL Val_SP
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_Remain</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public REAL Val_Remain
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_PercentComplete</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public REAL Val_PercentComplete
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_QtyPV</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public REAL Val_QtyPV
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_Qty</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public REAL Val_Qty
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_RatePV</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public REAL Val_RatePV
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_Rate</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public REAL Val_Rate
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_DribbleQty</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public REAL Val_DribbleQty
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_Preact</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public REAL Val_Preact
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_TolHi</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public REAL Val_TolHi
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_TolLo</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public REAL Val_TolLo
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_Owner</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public DINT Val_Owner
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SrcQ</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public SINT SrcQ
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SrcQ_IO</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public SINT SrcQ_IO
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Acq</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL XRdy_Acq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Rel</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL XRdy_Rel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Reset</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL XRdy_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_ResetAckAll</c> member of the <see cref="P_DOSING"/> data type.
    /// </summary>
    public BOOL XRdy_ResetAckAll
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}