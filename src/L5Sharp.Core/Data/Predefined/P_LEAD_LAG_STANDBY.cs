using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>P_LEAD_LAG_STANDBY</c> data type structure.
/// </summary>
[LogixData("P_LEAD_LAG_STANDBY")]
public sealed partial class P_LEAD_LAG_STANDBY : StructureData
{
    /// <summary>
    /// Creates a new <see cref="P_LEAD_LAG_STANDBY"/> instance initialized with default values.
    /// </summary>
    public P_LEAD_LAG_STANDBY() : base("P_LEAD_LAG_STANDBY")
    {
        EnableIn = new BOOL();
        EnableOut = new BOOL();
        Inp_InitializeReq = new BOOL();
        Inp_OwnerCmd = new DINT();
        Inp_PermOK = new BOOL();
        Inp_NBPermOK = new BOOL();
        Inp_IntlkOK = new BOOL();
        Inp_NBIntlkOK = new BOOL();
        Inp_IntlkAvailable = new BOOL();
        Inp_IntlkTripInh = new BOOL();
        Inp_RdyReset = new BOOL();
        Inp_Hand = new BOOL();
        Inp_Ovrd = new BOOL();
        Inp_OvrdDemand = new DINT();
        Inp_OvrdCmd = new DINT();
        Inp_ExtInh = new BOOL();
        Inp_Reset = new BOOL();
        Cfg_AllowDisable = new BOOL();
        Cfg_AllowShelve = new BOOL();
        Cfg_NumMotors = new DINT();
        Cfg_MaxDemand = new DINT();
        Cfg_MinDemand = new DINT();
        Cfg_StartDly = new REAL();
        Cfg_StopDly = new REAL();
        Cfg_FirstOnFirstOff = new BOOL();
        Cfg_AllowRotate = new BOOL();
        Cfg_RotateOnStop = new BOOL();
        Cfg_HasPermObj = new BOOL();
        Cfg_HasIntlkObj = new BOOL();
        Cfg_HasMoreObj = new BOOL();
        Cfg_HasNav0 = new BOOL();
        Cfg_HasNav1 = new BOOL();
        Cfg_HasNav2 = new BOOL();
        Cfg_HasNav3 = new BOOL();
        Cfg_HasNav4 = new BOOL();
        Cfg_HasNav5 = new BOOL();
        Cfg_HasNav6 = new BOOL();
        Cfg_HasNav7 = new BOOL();
        Cfg_HasNav8 = new BOOL();
        Cfg_HasNav9 = new BOOL();
        Cfg_HasNav10 = new BOOL();
        Cfg_HasNav11 = new BOOL();
        Cfg_HasNav12 = new BOOL();
        Cfg_HasNav13 = new BOOL();
        Cfg_HasNav14 = new BOOL();
        Cfg_HasNav15 = new BOOL();
        Cfg_HasNav16 = new BOOL();
        Cfg_HasNav17 = new BOOL();
        Cfg_HasNav18 = new BOOL();
        Cfg_HasNav19 = new BOOL();
        Cfg_HasNav20 = new BOOL();
        Cfg_HasNav21 = new BOOL();
        Cfg_HasNav22 = new BOOL();
        Cfg_HasNav23 = new BOOL();
        Cfg_HasNav24 = new BOOL();
        Cfg_HasNav25 = new BOOL();
        Cfg_HasNav26 = new BOOL();
        Cfg_HasNav27 = new BOOL();
        Cfg_HasNav28 = new BOOL();
        Cfg_HasNav29 = new BOOL();
        Cfg_SetTrack = new BOOL();
        Cfg_SetTrackOvrdHand = new BOOL();
        Cfg_OperStopPrio = new BOOL();
        Cfg_ExtStopPrio = new BOOL();
        Cfg_OCmdResets = new BOOL();
        Cfg_XCmdResets = new BOOL();
        Cfg_OvrdPermIntlk = new BOOL();
        Cfg_CnfrmReqd = new SINT();
        PSet_Demand = new DINT();
        PSet_Owner = new DINT();
        XSet_Demand = new DINT();
        PCmd_Start = new BOOL();
        PCmd_Stop = new BOOL();
        PCmd_Rotate = new BOOL();
        PCmd_Prog = new BOOL();
        PCmd_Oper = new BOOL();
        PCmd_Lock = new BOOL();
        PCmd_Unlock = new BOOL();
        PCmd_Normal = new BOOL();
        PCmd_Reset = new BOOL();
        XCmd_Start = new BOOL();
        XCmd_Stop = new BOOL();
        XCmd_Rotate = new BOOL();
        XCmd_Reset = new BOOL();
        XCmd_ResetAckAll = new BOOL();
        Val_Demand = new DINT();
        Val_RotateRank = new DINT();
        Val_RotateID = new DINT();
        Sts_eCmd = new SINT();
        Sts_Fdbk = new SINT();
        Sts_eSts = new INT();
        Sts_eFault = new INT();
        Sts_eNotifyAll = new SINT();
        Sts_Initialized = new BOOL();
        Sts_Stopped = new BOOL();
        Sts_Running = new BOOL();
        Sts_Stopping = new BOOL();
        Sts_Incr = new BOOL();
        Sts_Decr = new BOOL();
        Sts_Available = new BOOL();
        Sts_IntlkAvailable = new BOOL();
        Sts_Bypass = new BOOL();
        Sts_BypActive = new BOOL();
        Sts_NotRdy = new BOOL();
        Sts_NrdyCfgErr = new BOOL();
        Sts_NrdyIntlk = new BOOL();
        Sts_NrdyOoS = new BOOL();
        Sts_NrdyPrioStop = new BOOL();
        Sts_NrdyPerm = new BOOL();
        Sts_MaintByp = new BOOL();
        Sts_Alm = new BOOL();
        Sts_AlmInh = new BOOL();
        Sts_Err = new BOOL();
        Sts_ErrStartDly = new BOOL();
        Sts_ErrStopDly = new BOOL();
        Sts_ErrAlm = new BOOL();
        Val_Owner = new DINT();
        Sts_MotorAvailable = new DINT();
        Sts_MotorStopped = new DINT();
        Sts_MotorStarting = new DINT();
        Sts_MotorRunning = new DINT();
        Sts_MotorStopping = new DINT();
        Sts_Hand = new BOOL();
        Sts_OoS = new BOOL();
        Sts_Maint = new BOOL();
        Sts_Ovrd = new BOOL();
        Sts_Ext = new BOOL();
        Sts_Prog = new BOOL();
        Sts_ProgLocked = new BOOL();
        Sts_Oper = new BOOL();
        Sts_OperLocked = new BOOL();
        Sts_Normal = new BOOL();
        Sts_ExtReqInh = new BOOL();
        Sts_ProgReqInh = new BOOL();
        Sts_MAcqRcvd = new BOOL();
        Sts_CantStart = new BOOL();
        Sts_CantStop = new BOOL();
        Sts_IntlkTrip = new BOOL();
        Sts_RdyReset = new BOOL();
        Sts_RdyAck = new BOOL();
        Sts_UnackAlmCount = new DINT();
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
        XCmd_Acq = new BOOL();
        XCmd_Rel = new BOOL();
        Out_Reset = new BOOL();
        Out_OwnerSts = new DINT();
        Sts_eSrc = new INT();
        Sts_bSrc = new INT();
        Sts_ProgOperSel = new BOOL();
        Sts_ProgOperLock = new BOOL();
        XRdy_Acq = new BOOL();
        XRdy_Rel = new BOOL();
        XRdy_Reset = new BOOL();
        XRdy_ResetAckAll = new BOOL();
        XRdy_Stop = new BOOL();
        XRdy_Start = new BOOL();
        XRdy_Rotate = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="P_LEAD_LAG_STANDBY"/> instance initialized with the provided element.
    /// </summary>
    public P_LEAD_LAG_STANDBY(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 1288;
    
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
        Inp_PermOK.UpdateData((data[offset + 9] & (1 << 3)) != 0);
        Inp_NBPermOK.UpdateData((data[offset + 9] & (1 << 4)) != 0);
        Inp_IntlkOK.UpdateData((data[offset + 9] & (1 << 5)) != 0);
        Inp_NBIntlkOK.UpdateData((data[offset + 9] & (1 << 6)) != 0);
        Inp_IntlkAvailable.UpdateData((data[offset + 9] & (1 << 7)) != 0);
        Inp_IntlkTripInh.UpdateData((data[offset + 10] & (1 << 0)) != 0);
        Inp_RdyReset.UpdateData((data[offset + 10] & (1 << 1)) != 0);
        Inp_Hand.UpdateData((data[offset + 10] & (1 << 2)) != 0);
        Inp_Ovrd.UpdateData((data[offset + 10] & (1 << 3)) != 0);
        Inp_OvrdDemand.UpdateData(data, offset + 10);
        Inp_OvrdCmd.UpdateData(data, offset + 14);
        Inp_ExtInh.UpdateData((data[offset + 18] & (1 << 4)) != 0);
        Inp_Reset.UpdateData((data[offset + 18] & (1 << 5)) != 0);
        Cfg_AllowDisable.UpdateData((data[offset + 18] & (1 << 6)) != 0);
        Cfg_AllowShelve.UpdateData((data[offset + 18] & (1 << 7)) != 0);
        Cfg_NumMotors.UpdateData(data, offset + 18);
        Cfg_MaxDemand.UpdateData(data, offset + 22);
        Cfg_MinDemand.UpdateData(data, offset + 26);
        Cfg_StartDly.UpdateData(data, offset + 30);
        Cfg_StopDly.UpdateData(data, offset + 34);
        Cfg_FirstOnFirstOff.UpdateData((data[offset + 39] & (1 << 0)) != 0);
        Cfg_AllowRotate.UpdateData((data[offset + 39] & (1 << 1)) != 0);
        Cfg_RotateOnStop.UpdateData((data[offset + 39] & (1 << 2)) != 0);
        Cfg_HasPermObj.UpdateData((data[offset + 39] & (1 << 3)) != 0);
        Cfg_HasIntlkObj.UpdateData((data[offset + 39] & (1 << 4)) != 0);
        Cfg_HasMoreObj.UpdateData((data[offset + 39] & (1 << 5)) != 0);
        Cfg_HasNav0.UpdateData((data[offset + 39] & (1 << 6)) != 0);
        Cfg_HasNav1.UpdateData((data[offset + 39] & (1 << 7)) != 0);
        Cfg_HasNav2.UpdateData((data[offset + 40] & (1 << 0)) != 0);
        Cfg_HasNav3.UpdateData((data[offset + 40] & (1 << 1)) != 0);
        Cfg_HasNav4.UpdateData((data[offset + 40] & (1 << 2)) != 0);
        Cfg_HasNav5.UpdateData((data[offset + 40] & (1 << 3)) != 0);
        Cfg_HasNav6.UpdateData((data[offset + 40] & (1 << 4)) != 0);
        Cfg_HasNav7.UpdateData((data[offset + 40] & (1 << 5)) != 0);
        Cfg_HasNav8.UpdateData((data[offset + 40] & (1 << 6)) != 0);
        Cfg_HasNav9.UpdateData((data[offset + 40] & (1 << 7)) != 0);
        Cfg_HasNav10.UpdateData((data[offset + 41] & (1 << 0)) != 0);
        Cfg_HasNav11.UpdateData((data[offset + 41] & (1 << 1)) != 0);
        Cfg_HasNav12.UpdateData((data[offset + 41] & (1 << 2)) != 0);
        Cfg_HasNav13.UpdateData((data[offset + 45] & (1 << 3)) != 0);
        Cfg_HasNav14.UpdateData((data[offset + 45] & (1 << 4)) != 0);
        Cfg_HasNav15.UpdateData((data[offset + 45] & (1 << 5)) != 0);
        Cfg_HasNav16.UpdateData((data[offset + 45] & (1 << 6)) != 0);
        Cfg_HasNav17.UpdateData((data[offset + 45] & (1 << 7)) != 0);
        Cfg_HasNav18.UpdateData((data[offset + 46] & (1 << 0)) != 0);
        Cfg_HasNav19.UpdateData((data[offset + 46] & (1 << 1)) != 0);
        Cfg_HasNav20.UpdateData((data[offset + 46] & (1 << 2)) != 0);
        Cfg_HasNav21.UpdateData((data[offset + 46] & (1 << 3)) != 0);
        Cfg_HasNav22.UpdateData((data[offset + 46] & (1 << 4)) != 0);
        Cfg_HasNav23.UpdateData((data[offset + 46] & (1 << 5)) != 0);
        Cfg_HasNav24.UpdateData((data[offset + 46] & (1 << 6)) != 0);
        Cfg_HasNav25.UpdateData((data[offset + 46] & (1 << 7)) != 0);
        Cfg_HasNav26.UpdateData((data[offset + 47] & (1 << 0)) != 0);
        Cfg_HasNav27.UpdateData((data[offset + 47] & (1 << 1)) != 0);
        Cfg_HasNav28.UpdateData((data[offset + 47] & (1 << 2)) != 0);
        Cfg_HasNav29.UpdateData((data[offset + 47] & (1 << 3)) != 0);
        Cfg_SetTrack.UpdateData((data[offset + 47] & (1 << 4)) != 0);
        Cfg_SetTrackOvrdHand.UpdateData((data[offset + 47] & (1 << 5)) != 0);
        Cfg_OperStopPrio.UpdateData((data[offset + 47] & (1 << 6)) != 0);
        Cfg_ExtStopPrio.UpdateData((data[offset + 47] & (1 << 7)) != 0);
        Cfg_OCmdResets.UpdateData((data[offset + 48] & (1 << 0)) != 0);
        Cfg_XCmdResets.UpdateData((data[offset + 48] & (1 << 1)) != 0);
        Cfg_OvrdPermIntlk.UpdateData((data[offset + 48] & (1 << 2)) != 0);
        Cfg_CnfrmReqd.UpdateData(data, offset + 48);
        PSet_Demand.UpdateData(data, offset + 49);
        PSet_Owner.UpdateData(data, offset + 53);
        XSet_Demand.UpdateData(data, offset + 57);
        PCmd_Start.UpdateData((data[offset + 61] & (1 << 3)) != 0);
        PCmd_Stop.UpdateData((data[offset + 61] & (1 << 4)) != 0);
        PCmd_Rotate.UpdateData((data[offset + 61] & (1 << 5)) != 0);
        PCmd_Prog.UpdateData((data[offset + 61] & (1 << 6)) != 0);
        PCmd_Oper.UpdateData((data[offset + 61] & (1 << 7)) != 0);
        PCmd_Lock.UpdateData((data[offset + 62] & (1 << 0)) != 0);
        PCmd_Unlock.UpdateData((data[offset + 62] & (1 << 1)) != 0);
        PCmd_Normal.UpdateData((data[offset + 62] & (1 << 2)) != 0);
        PCmd_Reset.UpdateData((data[offset + 62] & (1 << 3)) != 0);
        XCmd_Start.UpdateData((data[offset + 62] & (1 << 4)) != 0);
        XCmd_Stop.UpdateData((data[offset + 62] & (1 << 5)) != 0);
        XCmd_Rotate.UpdateData((data[offset + 62] & (1 << 6)) != 0);
        XCmd_Reset.UpdateData((data[offset + 62] & (1 << 7)) != 0);
        XCmd_ResetAckAll.UpdateData((data[offset + 67] & (1 << 0)) != 0);
        Val_Demand.UpdateData(data, offset + 67);
        Val_RotateRank.UpdateData(data, offset + 71);
        Val_RotateID.UpdateData(data, offset + 75);
        Sts_eCmd.UpdateData(data, offset + 79);
        Sts_Fdbk.UpdateData(data, offset + 80);
        Sts_eSts.UpdateData(data, offset + 81);
        Sts_eFault.UpdateData(data, offset + 83);
        Sts_eNotifyAll.UpdateData(data, offset + 85);
        Sts_Initialized.UpdateData((data[offset + 86] & (1 << 1)) != 0);
        Sts_Stopped.UpdateData((data[offset + 86] & (1 << 2)) != 0);
        Sts_Running.UpdateData((data[offset + 86] & (1 << 3)) != 0);
        Sts_Stopping.UpdateData((data[offset + 86] & (1 << 4)) != 0);
        Sts_Incr.UpdateData((data[offset + 86] & (1 << 5)) != 0);
        Sts_Decr.UpdateData((data[offset + 86] & (1 << 6)) != 0);
        Sts_Available.UpdateData((data[offset + 86] & (1 << 7)) != 0);
        Sts_IntlkAvailable.UpdateData((data[offset + 87] & (1 << 0)) != 0);
        Sts_Bypass.UpdateData((data[offset + 87] & (1 << 1)) != 0);
        Sts_BypActive.UpdateData((data[offset + 87] & (1 << 2)) != 0);
        Sts_NotRdy.UpdateData((data[offset + 87] & (1 << 3)) != 0);
        Sts_NrdyCfgErr.UpdateData((data[offset + 87] & (1 << 4)) != 0);
        Sts_NrdyIntlk.UpdateData((data[offset + 87] & (1 << 5)) != 0);
        Sts_NrdyOoS.UpdateData((data[offset + 87] & (1 << 6)) != 0);
        Sts_NrdyPrioStop.UpdateData((data[offset + 87] & (1 << 7)) != 0);
        Sts_NrdyPerm.UpdateData((data[offset + 88] & (1 << 0)) != 0);
        Sts_MaintByp.UpdateData((data[offset + 88] & (1 << 1)) != 0);
        Sts_Alm.UpdateData((data[offset + 88] & (1 << 2)) != 0);
        Sts_AlmInh.UpdateData((data[offset + 88] & (1 << 3)) != 0);
        Sts_Err.UpdateData((data[offset + 88] & (1 << 4)) != 0);
        Sts_ErrStartDly.UpdateData((data[offset + 88] & (1 << 5)) != 0);
        Sts_ErrStopDly.UpdateData((data[offset + 88] & (1 << 6)) != 0);
        Sts_ErrAlm.UpdateData((data[offset + 88] & (1 << 7)) != 0);
        Val_Owner.UpdateData(data, offset + 88);
        Sts_MotorAvailable.UpdateData(data, offset + 92);
        Sts_MotorStopped.UpdateData(data, offset + 96);
        Sts_MotorStarting.UpdateData(data, offset + 100);
        Sts_MotorRunning.UpdateData(data, offset + 104);
        Sts_MotorStopping.UpdateData(data, offset + 108);
        Sts_Hand.UpdateData((data[offset + 113] & (1 << 0)) != 0);
        Sts_OoS.UpdateData((data[offset + 113] & (1 << 1)) != 0);
        Sts_Maint.UpdateData((data[offset + 113] & (1 << 2)) != 0);
        Sts_Ovrd.UpdateData((data[offset + 113] & (1 << 3)) != 0);
        Sts_Ext.UpdateData((data[offset + 113] & (1 << 4)) != 0);
        Sts_Prog.UpdateData((data[offset + 113] & (1 << 5)) != 0);
        Sts_ProgLocked.UpdateData((data[offset + 113] & (1 << 6)) != 0);
        Sts_Oper.UpdateData((data[offset + 113] & (1 << 7)) != 0);
        Sts_OperLocked.UpdateData((data[offset + 114] & (1 << 0)) != 0);
        Sts_Normal.UpdateData((data[offset + 114] & (1 << 1)) != 0);
        Sts_ExtReqInh.UpdateData((data[offset + 114] & (1 << 2)) != 0);
        Sts_ProgReqInh.UpdateData((data[offset + 114] & (1 << 3)) != 0);
        Sts_MAcqRcvd.UpdateData((data[offset + 114] & (1 << 4)) != 0);
        Sts_CantStart.UpdateData((data[offset + 114] & (1 << 5)) != 0);
        Sts_CantStop.UpdateData((data[offset + 114] & (1 << 6)) != 0);
        Sts_IntlkTrip.UpdateData((data[offset + 114] & (1 << 7)) != 0);
        Sts_RdyReset.UpdateData((data[offset + 115] & (1 << 0)) != 0);
        Sts_RdyAck.UpdateData((data[offset + 115] & (1 << 1)) != 0);
        Sts_UnackAlmCount.UpdateData(data, offset + 115);
        Cfg_HasOper.UpdateData((data[offset + 119] & (1 << 2)) != 0);
        Cfg_HasOperLocked.UpdateData((data[offset + 119] & (1 << 3)) != 0);
        Cfg_HasProg.UpdateData((data[offset + 119] & (1 << 4)) != 0);
        Cfg_HasProgLocked.UpdateData((data[offset + 119] & (1 << 5)) != 0);
        Cfg_HasExt.UpdateData((data[offset + 119] & (1 << 6)) != 0);
        Cfg_HasMaint.UpdateData((data[offset + 119] & (1 << 7)) != 0);
        Cfg_HasMaintOoS.UpdateData((data[offset + 120] & (1 << 0)) != 0);
        Cfg_OvrdOverLock.UpdateData((data[offset + 120] & (1 << 1)) != 0);
        Cfg_ExtOverLock.UpdateData((data[offset + 120] & (1 << 2)) != 0);
        Cfg_ProgPwrUp.UpdateData((data[offset + 120] & (1 << 3)) != 0);
        Cfg_ProgNormal.UpdateData((data[offset + 120] & (1 << 4)) != 0);
        Cfg_PCmdPriority.UpdateData((data[offset + 120] & (1 << 5)) != 0);
        Cfg_PCmdProgAsLevel.UpdateData((data[offset + 120] & (1 << 6)) != 0);
        Cfg_PCmdLockAsLevel.UpdateData((data[offset + 120] & (1 << 7)) != 0);
        Cfg_ExtAcqAsLevel.UpdateData((data[offset + 121] & (1 << 0)) != 0);
        XCmd_Acq.UpdateData((data[offset + 121] & (1 << 1)) != 0);
        XCmd_Rel.UpdateData((data[offset + 121] & (1 << 2)) != 0);
        Out_Reset.UpdateData((data[offset + 121] & (1 << 3)) != 0);
        Out_OwnerSts.UpdateData(data, offset + 121);
        Sts_eSrc.UpdateData(data, offset + 125);
        Sts_bSrc.UpdateData(data, offset + 127);
        Sts_ProgOperSel.UpdateData((data[offset + 129] & (1 << 4)) != 0);
        Sts_ProgOperLock.UpdateData((data[offset + 129] & (1 << 5)) != 0);
        XRdy_Acq.UpdateData((data[offset + 129] & (1 << 6)) != 0);
        XRdy_Rel.UpdateData((data[offset + 129] & (1 << 7)) != 0);
        XRdy_Reset.UpdateData((data[offset + 130] & (1 << 0)) != 0);
        XRdy_ResetAckAll.UpdateData((data[offset + 130] & (1 << 1)) != 0);
        XRdy_Stop.UpdateData((data[offset + 134] & (1 << 2)) != 0);
        XRdy_Start.UpdateData((data[offset + 134] & (1 << 3)) != 0);
        XRdy_Rotate.UpdateData((data[offset + 134] & (1 << 4)) != 0);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_InitializeReq</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Inp_InitializeReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_OwnerCmd</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public DINT Inp_OwnerCmd
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PermOK</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Inp_PermOK
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_NBPermOK</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Inp_NBPermOK
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_IntlkOK</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Inp_IntlkOK
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_NBIntlkOK</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Inp_NBIntlkOK
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_IntlkAvailable</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Inp_IntlkAvailable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_IntlkTripInh</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Inp_IntlkTripInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_RdyReset</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Inp_RdyReset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Hand</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Inp_Hand
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Ovrd</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Inp_Ovrd
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_OvrdDemand</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public DINT Inp_OvrdDemand
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_OvrdCmd</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public DINT Inp_OvrdCmd
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_ExtInh</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Inp_ExtInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Reset</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Inp_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_AllowDisable</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Cfg_AllowDisable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_AllowShelve</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Cfg_AllowShelve
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_NumMotors</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public DINT Cfg_NumMotors
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_MaxDemand</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public DINT Cfg_MaxDemand
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_MinDemand</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public DINT Cfg_MinDemand
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_StartDly</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public REAL Cfg_StartDly
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_StopDly</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public REAL Cfg_StopDly
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_FirstOnFirstOff</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Cfg_FirstOnFirstOff
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_AllowRotate</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Cfg_AllowRotate
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_RotateOnStop</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Cfg_RotateOnStop
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasPermObj</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Cfg_HasPermObj
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasIntlkObj</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Cfg_HasIntlkObj
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasMoreObj</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Cfg_HasMoreObj
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasNav0</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Cfg_HasNav0
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasNav1</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Cfg_HasNav1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasNav2</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Cfg_HasNav2
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasNav3</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Cfg_HasNav3
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasNav4</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Cfg_HasNav4
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasNav5</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Cfg_HasNav5
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasNav6</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Cfg_HasNav6
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasNav7</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Cfg_HasNav7
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasNav8</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Cfg_HasNav8
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasNav9</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Cfg_HasNav9
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasNav10</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Cfg_HasNav10
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasNav11</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Cfg_HasNav11
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasNav12</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Cfg_HasNav12
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasNav13</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Cfg_HasNav13
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasNav14</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Cfg_HasNav14
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasNav15</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Cfg_HasNav15
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasNav16</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Cfg_HasNav16
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasNav17</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Cfg_HasNav17
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasNav18</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Cfg_HasNav18
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasNav19</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Cfg_HasNav19
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasNav20</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Cfg_HasNav20
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasNav21</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Cfg_HasNav21
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasNav22</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Cfg_HasNav22
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasNav23</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Cfg_HasNav23
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasNav24</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Cfg_HasNav24
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasNav25</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Cfg_HasNav25
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasNav26</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Cfg_HasNav26
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasNav27</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Cfg_HasNav27
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasNav28</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Cfg_HasNav28
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasNav29</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Cfg_HasNav29
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_SetTrack</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Cfg_SetTrack
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_SetTrackOvrdHand</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Cfg_SetTrackOvrdHand
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OperStopPrio</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Cfg_OperStopPrio
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ExtStopPrio</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Cfg_ExtStopPrio
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OCmdResets</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Cfg_OCmdResets
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_XCmdResets</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Cfg_XCmdResets
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OvrdPermIntlk</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Cfg_OvrdPermIntlk
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CnfrmReqd</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public SINT Cfg_CnfrmReqd
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PSet_Demand</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public DINT PSet_Demand
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PSet_Owner</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public DINT PSet_Owner
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XSet_Demand</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public DINT XSet_Demand
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Start</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL PCmd_Start
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Stop</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL PCmd_Stop
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Rotate</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL PCmd_Rotate
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Prog</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL PCmd_Prog
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Oper</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL PCmd_Oper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Lock</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL PCmd_Lock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Unlock</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL PCmd_Unlock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Normal</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL PCmd_Normal
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Reset</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL PCmd_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Start</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL XCmd_Start
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Stop</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL XCmd_Stop
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Rotate</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL XCmd_Rotate
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Reset</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL XCmd_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_ResetAckAll</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL XCmd_ResetAckAll
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_Demand</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public DINT Val_Demand
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_RotateRank</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public DINT Val_RotateRank
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_RotateID</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public DINT Val_RotateID
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eCmd</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public SINT Sts_eCmd
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Fdbk</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public SINT Sts_Fdbk
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eSts</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public INT Sts_eSts
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eFault</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public INT Sts_eFault
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyAll</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public SINT Sts_eNotifyAll
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Initialized</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Sts_Initialized
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Stopped</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Sts_Stopped
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Running</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Sts_Running
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Stopping</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Sts_Stopping
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Incr</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Sts_Incr
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Decr</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Sts_Decr
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Available</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Sts_Available
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_IntlkAvailable</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Sts_IntlkAvailable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Bypass</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Sts_Bypass
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_BypActive</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Sts_BypActive
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NotRdy</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Sts_NotRdy
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyCfgErr</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Sts_NrdyCfgErr
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyIntlk</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Sts_NrdyIntlk
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyOoS</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Sts_NrdyOoS
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyPrioStop</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Sts_NrdyPrioStop
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyPerm</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Sts_NrdyPerm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_MaintByp</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Sts_MaintByp
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Alm</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Sts_Alm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_AlmInh</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Sts_AlmInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Err</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Sts_Err
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrStartDly</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Sts_ErrStartDly
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrStopDly</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Sts_ErrStopDly
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrAlm</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Sts_ErrAlm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_Owner</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public DINT Val_Owner
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_MotorAvailable</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public DINT Sts_MotorAvailable
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_MotorStopped</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public DINT Sts_MotorStopped
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_MotorStarting</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public DINT Sts_MotorStarting
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_MotorRunning</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public DINT Sts_MotorRunning
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_MotorStopping</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public DINT Sts_MotorStopping
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Hand</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Sts_Hand
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_OoS</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Sts_OoS
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Maint</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Sts_Maint
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Ovrd</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Sts_Ovrd
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Ext</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Sts_Ext
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Prog</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Sts_Prog
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ProgLocked</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Sts_ProgLocked
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Oper</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Sts_Oper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_OperLocked</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Sts_OperLocked
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Normal</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Sts_Normal
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ExtReqInh</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Sts_ExtReqInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ProgReqInh</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Sts_ProgReqInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_MAcqRcvd</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Sts_MAcqRcvd
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_CantStart</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Sts_CantStart
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_CantStop</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Sts_CantStop
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_IntlkTrip</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Sts_IntlkTrip
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_RdyReset</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Sts_RdyReset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_RdyAck</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Sts_RdyAck
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_UnackAlmCount</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public DINT Sts_UnackAlmCount
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasOper</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Cfg_HasOper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasOperLocked</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Cfg_HasOperLocked
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasProg</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Cfg_HasProg
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasProgLocked</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Cfg_HasProgLocked
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasExt</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Cfg_HasExt
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasMaint</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Cfg_HasMaint
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasMaintOoS</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Cfg_HasMaintOoS
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OvrdOverLock</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Cfg_OvrdOverLock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ExtOverLock</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Cfg_ExtOverLock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ProgPwrUp</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Cfg_ProgPwrUp
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ProgNormal</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Cfg_ProgNormal
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PCmdPriority</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Cfg_PCmdPriority
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PCmdProgAsLevel</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Cfg_PCmdProgAsLevel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PCmdLockAsLevel</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Cfg_PCmdLockAsLevel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ExtAcqAsLevel</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Cfg_ExtAcqAsLevel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Acq</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL XCmd_Acq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Rel</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL XCmd_Rel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_Reset</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Out_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_OwnerSts</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public DINT Out_OwnerSts
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eSrc</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public INT Sts_eSrc
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_bSrc</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public INT Sts_bSrc
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ProgOperSel</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Sts_ProgOperSel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ProgOperLock</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL Sts_ProgOperLock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Acq</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL XRdy_Acq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Rel</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL XRdy_Rel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Reset</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL XRdy_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_ResetAckAll</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL XRdy_ResetAckAll
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Stop</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL XRdy_Stop
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Start</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL XRdy_Start
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Rotate</c> member of the <see cref="P_LEAD_LAG_STANDBY"/> data type.
    /// </summary>
    public BOOL XRdy_Rotate
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}