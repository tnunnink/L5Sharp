using System.Xml.Linq;
// Auto-generated file
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>P_VALVE_DISCRETE</c> data type structure.
/// </summary>
[LogixData("P_VALVE_DISCRETE")]
public sealed partial class P_VALVE_DISCRETE : StructureData
{
    /// <summary>
    /// Creates a new <see cref="P_VALVE_DISCRETE"/> instance initialized with default values.
    /// </summary>
    public P_VALVE_DISCRETE() : base("P_VALVE_DISCRETE")
    {
        EnableIn = new BOOL();
        EnableOut = new BOOL();
        Inp_InitializeReq = new BOOL();
        Inp_OvrdCmd = new SINT();
        Inp_Pos1FdbkData = new BOOL();
        Inp_Pos2FdbkData = new BOOL();
        Inp_ActuatorFault = new BOOL();
        Inp_IOFault = new BOOL();
        Inp_Pos1PermOK = new BOOL();
        Inp_Pos1NBPermOK = new BOOL();
        Inp_Pos2PermOK = new BOOL();
        Inp_Pos2NBPermOK = new BOOL();
        Inp_IntlkOK = new BOOL();
        Inp_NBIntlkOK = new BOOL();
        Inp_IntlkAvailable = new BOOL();
        Inp_IntlkTripInh = new BOOL();
        Inp_RdyReset = new BOOL();
        Inp_Hand = new BOOL();
        Inp_Ovrd = new BOOL();
        Inp_OwnerCmd = new DINT();
        Inp_ExtInh = new BOOL();
        Inp_HornInh = new BOOL();
        Inp_Reset = new BOOL();
        Inp_VirtualPos1HO = new BOOL();
        Inp_VirtualPos2HO = new BOOL();
        Cfg_AllowDisable = new BOOL();
        Cfg_AllowShelve = new BOOL();
        Cfg_eObjType = new SINT();
        Cfg_HasPos1Fdbk = new BOOL();
        Cfg_HasPos2Fdbk = new BOOL();
        Cfg_UsePos1Fdbk = new BOOL();
        Cfg_UsePos2Fdbk = new BOOL();
        Cfg_HasPos1PermObj = new BOOL();
        Cfg_HasPos2PermObj = new BOOL();
        Cfg_HasIntlkObj = new BOOL();
        Cfg_FailPos2 = new BOOL();
        Cfg_FdbkFail = new BOOL();
        Cfg_HasStop = new BOOL();
        Cfg_MntnOut = new BOOL();
        Cfg_MntnOutAlm = new BOOL();
        Cfg_MntnStop = new BOOL();
        Cfg_HasTrip = new BOOL();
        Cfg_TripPos2 = new BOOL();
        Cfg_HasPulse = new BOOL();
        Cfg_CompletePulse = new BOOL();
        Cfg_HasPulseToState = new BOOL();
        Cfg_HasMoreObj = new BOOL();
        Cfg_HasStatsObj = new BOOL();
        Cfg_CoastToLS = new BOOL();
        Cfg_OperPos1Prio = new BOOL();
        Cfg_OCmdResets = new BOOL();
        Cfg_XCmdResets = new BOOL();
        Cfg_OvrdPermIntlk = new BOOL();
        Cfg_PCmdPos2AsLevel = new BOOL();
        Cfg_ShedOnActuatorFault = new BOOL();
        Cfg_ShedOnIOFault = new BOOL();
        Cfg_ShedOnFailToTrip = new BOOL();
        Cfg_ShedOnFullStall = new BOOL();
        Cfg_ShedOnLossPos1 = new BOOL();
        Cfg_ShedOnLossPos2 = new BOOL();
        Cfg_ShedOnTransitStall = new BOOL();
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
        Cfg_Pos1Dly = new REAL();
        Cfg_Pos2Dly = new REAL();
        Cfg_Pos1PulseTime = new REAL();
        Cfg_Pos2PulseTime = new REAL();
        Cfg_OutPulseTime = new REAL();
        Cfg_StartHornTime = new REAL();
        Cfg_FullStallTime = new REAL();
        Cfg_TransitStallTime = new REAL();
        Cfg_TripFailTime = new REAL();
        Cfg_VirtualFdbkTime = new REAL();
        Cfg_CnfrmReqd = new SINT();
        PCmd_Pos1 = new BOOL();
        PCmd_Pos2 = new BOOL();
        PCmd_Pos1Pulse = new BOOL();
        PCmd_Pos2Pulse = new BOOL();
        PCmd_ContPulse = new BOOL();
        PCmd_Trip = new BOOL();
        PCmd_Reset = new BOOL();
        PCmd_Stop = new BOOL();
        PSet_Owner = new DINT();
        PCmd_Lock = new BOOL();
        PCmd_Normal = new BOOL();
        PCmd_Oper = new BOOL();
        PCmd_Prog = new BOOL();
        PCmd_Unlock = new BOOL();
        PCmd_Virtual = new BOOL();
        PCmd_Physical = new BOOL();
        XCmd_Pos1 = new BOOL();
        XCmd_Pos2 = new BOOL();
        XCmd_Pos1Pulse = new BOOL();
        XCmd_Pos2Pulse = new BOOL();
        XCmd_ContPulse = new BOOL();
        XCmd_Trip = new BOOL();
        XCmd_Reset = new BOOL();
        XCmd_Stop = new BOOL();
        XCmd_ResetAckAll = new BOOL();
        XCmd_Acq = new BOOL();
        XCmd_Rel = new BOOL();
        Out_Pos1Data = new BOOL();
        Out_Pos2Data = new BOOL();
        Out_StopData = new BOOL();
        Out_TripData = new BOOL();
        Out_HornData = new BOOL();
        Out_Reset = new BOOL();
        Out_OwnerSts = new DINT();
        Sts_Initialized = new BOOL();
        Sts_Available = new BOOL();
        Sts_IntlkAvailable = new BOOL();
        Sts_Bypass = new BOOL();
        Sts_BypActive = new BOOL();
        Sts_MaintByp = new BOOL();
        Sts_NotRdy = new BOOL();
        Sts_NrdyCfgErr = new BOOL();
        Sts_NrdyIntlk = new BOOL();
        Sts_NrdyOoS = new BOOL();
        Sts_NrdyPos1Perm = new BOOL();
        Sts_NrdyPos2Perm = new BOOL();
        Sts_NrdyPerm = new BOOL();
        Sts_NrdyStopPerm = new BOOL();
        Sts_Err = new BOOL();
        Sts_ErrAlm = new BOOL();
        Sts_ErrFullStallTime = new BOOL();
        Sts_ErrHas = new BOOL();
        Sts_ErrOutPulseTime = new BOOL();
        Sts_ErrPos1Dly = new BOOL();
        Sts_ErrPos1PulseTime = new BOOL();
        Sts_ErrPos2Dly = new BOOL();
        Sts_ErrPos2PulseTime = new BOOL();
        Sts_ErrTransitStallTime = new BOOL();
        Sts_ErrTripFailTime = new BOOL();
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
        Sts_ActuatorFault = new BOOL();
        Sts_CmdToPos1 = new BOOL();
        Sts_CmdToPos2 = new BOOL();
        Sts_ErrUse = new BOOL();
        Sts_ErrVirtualFdbkTime = new BOOL();
        Sts_FdbkPos1 = new BOOL();
        Sts_FdbkPos2 = new BOOL();
        Sts_FdbkFail = new BOOL();
        Sts_FullStall = new BOOL();
        Sts_Horn = new BOOL();
        Sts_LossPos1 = new BOOL();
        Sts_LossPos2 = new BOOL();
        Sts_IOFault = new BOOL();
        Sts_Moving = new BOOL();
        Sts_MovingToPos1 = new BOOL();
        Sts_MovingToPos2 = new BOOL();
        Sts_Pos1 = new BOOL();
        Sts_Pos2 = new BOOL();
        Sts_Pulsing = new BOOL();
        Sts_Stopped = new BOOL();
        Sts_TransitStall = new BOOL();
        Sts_TripFail = new BOOL();
        Sts_Tripping = new BOOL();
        Sts_UnackAlmCount = new DINT();
        Sts_NrdyTrip = new BOOL();
        Sts_NrdyIOFault = new BOOL();
        Sts_NrdyActuatorFault = new BOOL();
        Sts_IntlkTrip = new BOOL();
        Sts_NrdyFail = new BOOL();
        Sts_RdyAck = new BOOL();
        Sts_RdyReset = new BOOL();
        Sts_Virtual = new BOOL();
        Sts_bSrc = new INT();
        Sts_eSrc = new INT();
        Sts_eCmd = new SINT();
        Sts_eFdbk = new SINT();
        Sts_eFault = new SINT();
        Sts_eNotify = new SINT();
        Sts_eNotifyAll = new SINT();
        Sts_eState = new DINT();
        Sts_eSts = new SINT();
        Sts_eNotifyActuatorFault = new SINT();
        Sts_eNotifyFullStall = new SINT();
        Sts_eNotifyIntlkTrip = new SINT();
        Sts_eNotifyIOFault = new SINT();
        Sts_eNotifyLossPos1 = new SINT();
        Sts_eNotifyLossPos2 = new SINT();
        Sts_eNotifyTransitStall = new SINT();
        Sts_eNotifyTripFail = new SINT();
        SrcQ = new SINT();
        SrcQ_IO = new SINT();
        Val_Owner = new DINT();
        XRdy_Acq = new BOOL();
        XRdy_Rel = new BOOL();
        XRdy_Reset = new BOOL();
        XRdy_ResetAckAll = new BOOL();
    }

    /// <summary>
    /// Creates a new <see cref="P_VALVE_DISCRETE"/> instance initialized with the provided element.
    /// </summary>
    public P_VALVE_DISCRETE(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_InitializeReq</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Inp_InitializeReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_OvrdCmd</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public SINT Inp_OvrdCmd
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Pos1FdbkData</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Inp_Pos1FdbkData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Pos2FdbkData</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Inp_Pos2FdbkData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_ActuatorFault</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Inp_ActuatorFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_IOFault</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Inp_IOFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Pos1PermOK</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Inp_Pos1PermOK
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Pos1NBPermOK</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Inp_Pos1NBPermOK
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Pos2PermOK</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Inp_Pos2PermOK
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Pos2NBPermOK</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Inp_Pos2NBPermOK
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_IntlkOK</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Inp_IntlkOK
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_NBIntlkOK</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Inp_NBIntlkOK
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_IntlkAvailable</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Inp_IntlkAvailable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_IntlkTripInh</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Inp_IntlkTripInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_RdyReset</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Inp_RdyReset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Hand</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Inp_Hand
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Ovrd</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Inp_Ovrd
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_OwnerCmd</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public DINT Inp_OwnerCmd
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_ExtInh</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Inp_ExtInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_HornInh</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Inp_HornInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Reset</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Inp_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_VirtualPos1HO</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Inp_VirtualPos1HO
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_VirtualPos2HO</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Inp_VirtualPos2HO
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_AllowDisable</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_AllowDisable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_AllowShelve</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_AllowShelve
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_eObjType</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public SINT Cfg_eObjType
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasPos1Fdbk</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_HasPos1Fdbk
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasPos2Fdbk</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_HasPos2Fdbk
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_UsePos1Fdbk</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_UsePos1Fdbk
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_UsePos2Fdbk</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_UsePos2Fdbk
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasPos1PermObj</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_HasPos1PermObj
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasPos2PermObj</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_HasPos2PermObj
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasIntlkObj</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_HasIntlkObj
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_FailPos2</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_FailPos2
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_FdbkFail</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_FdbkFail
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasStop</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_HasStop
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_MntnOut</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_MntnOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_MntnOutAlm</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_MntnOutAlm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_MntnStop</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_MntnStop
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasTrip</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_HasTrip
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_TripPos2</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_TripPos2
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasPulse</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_HasPulse
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CompletePulse</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_CompletePulse
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasPulseToState</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_HasPulseToState
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasMoreObj</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_HasMoreObj
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasStatsObj</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_HasStatsObj
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CoastToLS</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_CoastToLS
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OperPos1Prio</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_OperPos1Prio
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OCmdResets</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_OCmdResets
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_XCmdResets</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_XCmdResets
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OvrdPermIntlk</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_OvrdPermIntlk
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PCmdPos2AsLevel</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_PCmdPos2AsLevel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ShedOnActuatorFault</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_ShedOnActuatorFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ShedOnIOFault</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_ShedOnIOFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ShedOnFailToTrip</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_ShedOnFailToTrip
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ShedOnFullStall</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_ShedOnFullStall
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ShedOnLossPos1</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_ShedOnLossPos1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ShedOnLossPos2</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_ShedOnLossPos2
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ShedOnTransitStall</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_ShedOnTransitStall
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasOper</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_HasOper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasOperLocked</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_HasOperLocked
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasProg</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_HasProg
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasProgLocked</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_HasProgLocked
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasExt</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_HasExt
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasMaint</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_HasMaint
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasMaintOoS</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_HasMaintOoS
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OvrdOverLock</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_OvrdOverLock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ExtOverLock</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_ExtOverLock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ProgPwrUp</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_ProgPwrUp
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ProgNormal</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_ProgNormal
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PCmdPriority</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_PCmdPriority
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PCmdProgAsLevel</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_PCmdProgAsLevel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PCmdLockAsLevel</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_PCmdLockAsLevel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ExtAcqAsLevel</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_ExtAcqAsLevel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_Pos1Dly</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public REAL Cfg_Pos1Dly
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_Pos2Dly</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public REAL Cfg_Pos2Dly
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_Pos1PulseTime</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public REAL Cfg_Pos1PulseTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_Pos2PulseTime</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public REAL Cfg_Pos2PulseTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OutPulseTime</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public REAL Cfg_OutPulseTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_StartHornTime</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public REAL Cfg_StartHornTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_FullStallTime</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public REAL Cfg_FullStallTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_TransitStallTime</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public REAL Cfg_TransitStallTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_TripFailTime</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public REAL Cfg_TripFailTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_VirtualFdbkTime</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public REAL Cfg_VirtualFdbkTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CnfrmReqd</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public SINT Cfg_CnfrmReqd
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Pos1</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL PCmd_Pos1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Pos2</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL PCmd_Pos2
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Pos1Pulse</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL PCmd_Pos1Pulse
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Pos2Pulse</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL PCmd_Pos2Pulse
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_ContPulse</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL PCmd_ContPulse
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Trip</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL PCmd_Trip
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Reset</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL PCmd_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Stop</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL PCmd_Stop
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PSet_Owner</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public DINT PSet_Owner
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Lock</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL PCmd_Lock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Normal</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL PCmd_Normal
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Oper</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL PCmd_Oper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Prog</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL PCmd_Prog
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Unlock</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL PCmd_Unlock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Virtual</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL PCmd_Virtual
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Physical</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL PCmd_Physical
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Pos1</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL XCmd_Pos1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Pos2</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL XCmd_Pos2
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Pos1Pulse</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL XCmd_Pos1Pulse
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Pos2Pulse</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL XCmd_Pos2Pulse
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_ContPulse</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL XCmd_ContPulse
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Trip</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL XCmd_Trip
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Reset</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL XCmd_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Stop</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL XCmd_Stop
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_ResetAckAll</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL XCmd_ResetAckAll
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Acq</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL XCmd_Acq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Rel</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL XCmd_Rel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_Pos1Data</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Out_Pos1Data
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_Pos2Data</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Out_Pos2Data
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_StopData</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Out_StopData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_TripData</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Out_TripData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_HornData</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Out_HornData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_Reset</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Out_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_OwnerSts</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public DINT Out_OwnerSts
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Initialized</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_Initialized
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Available</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_Available
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_IntlkAvailable</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_IntlkAvailable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Bypass</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_Bypass
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_BypActive</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_BypActive
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_MaintByp</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_MaintByp
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NotRdy</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_NotRdy
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyCfgErr</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_NrdyCfgErr
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyIntlk</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_NrdyIntlk
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyOoS</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_NrdyOoS
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyPos1Perm</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_NrdyPos1Perm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyPos2Perm</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_NrdyPos2Perm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyPerm</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_NrdyPerm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyStopPerm</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_NrdyStopPerm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Err</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_Err
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrAlm</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_ErrAlm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrFullStallTime</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_ErrFullStallTime
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrHas</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_ErrHas
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrOutPulseTime</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_ErrOutPulseTime
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrPos1Dly</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_ErrPos1Dly
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrPos1PulseTime</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_ErrPos1PulseTime
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrPos2Dly</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_ErrPos2Dly
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrPos2PulseTime</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_ErrPos2PulseTime
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrTransitStallTime</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_ErrTransitStallTime
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrTripFailTime</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_ErrTripFailTime
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Hand</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_Hand
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_OoS</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_OoS
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Maint</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_Maint
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Ovrd</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_Ovrd
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Ext</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_Ext
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Prog</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_Prog
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ProgLocked</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_ProgLocked
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Oper</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_Oper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_OperLocked</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_OperLocked
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ProgOperSel</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_ProgOperSel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ProgOperLock</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_ProgOperLock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Normal</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_Normal
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ExtReqInh</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_ExtReqInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ProgReqInh</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_ProgReqInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_MAcqRcvd</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_MAcqRcvd
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_CmdConflict</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_CmdConflict
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Alm</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_Alm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_AlmInh</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_AlmInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ActuatorFault</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_ActuatorFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_CmdToPos1</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_CmdToPos1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_CmdToPos2</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_CmdToPos2
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrUse</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_ErrUse
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrVirtualFdbkTime</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_ErrVirtualFdbkTime
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_FdbkPos1</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_FdbkPos1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_FdbkPos2</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_FdbkPos2
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_FdbkFail</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_FdbkFail
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_FullStall</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_FullStall
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Horn</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_Horn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_LossPos1</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_LossPos1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_LossPos2</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_LossPos2
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_IOFault</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_IOFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Moving</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_Moving
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_MovingToPos1</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_MovingToPos1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_MovingToPos2</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_MovingToPos2
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Pos1</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_Pos1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Pos2</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_Pos2
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Pulsing</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_Pulsing
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Stopped</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_Stopped
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_TransitStall</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_TransitStall
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_TripFail</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_TripFail
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Tripping</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_Tripping
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_UnackAlmCount</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public DINT Sts_UnackAlmCount
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyTrip</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_NrdyTrip
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyIOFault</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_NrdyIOFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyActuatorFault</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_NrdyActuatorFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_IntlkTrip</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_IntlkTrip
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyFail</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_NrdyFail
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_RdyAck</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_RdyAck
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_RdyReset</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_RdyReset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Virtual</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_Virtual
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_bSrc</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public INT Sts_bSrc
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eSrc</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public INT Sts_eSrc
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eCmd</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public SINT Sts_eCmd
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eFdbk</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public SINT Sts_eFdbk
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eFault</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public SINT Sts_eFault
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotify</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public SINT Sts_eNotify
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyAll</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public SINT Sts_eNotifyAll
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eState</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public DINT Sts_eState
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eSts</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public SINT Sts_eSts
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyActuatorFault</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public SINT Sts_eNotifyActuatorFault
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyFullStall</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public SINT Sts_eNotifyFullStall
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyIntlkTrip</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public SINT Sts_eNotifyIntlkTrip
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyIOFault</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public SINT Sts_eNotifyIOFault
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyLossPos1</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public SINT Sts_eNotifyLossPos1
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyLossPos2</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public SINT Sts_eNotifyLossPos2
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyTransitStall</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public SINT Sts_eNotifyTransitStall
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyTripFail</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public SINT Sts_eNotifyTripFail
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SrcQ</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public SINT SrcQ
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SrcQ_IO</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public SINT SrcQ_IO
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_Owner</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public DINT Val_Owner
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Acq</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL XRdy_Acq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Rel</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL XRdy_Rel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Reset</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL XRdy_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_ResetAckAll</c> member of the <see cref="P_VALVE_DISCRETE"/> data type.
    /// </summary>
    public BOOL XRdy_ResetAckAll
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

}
