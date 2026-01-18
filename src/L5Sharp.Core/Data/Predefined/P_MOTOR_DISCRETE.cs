using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>P_MOTOR_DISCRETE</c> data type structure.
/// </summary>
[LogixData("P_MOTOR_DISCRETE")]
public sealed partial class P_MOTOR_DISCRETE : StructureData
{
    /// <summary>
    /// Creates a new <see cref="P_MOTOR_DISCRETE"/> instance initialized with default values.
    /// </summary>
    public P_MOTOR_DISCRETE() : base("P_MOTOR_DISCRETE")
    {
        EnableIn = new BOOL();
        EnableOut = new BOOL();
        Inp_InitializeReq = new BOOL();
        Inp_OwnerCmd = new DINT();
        Inp_LastFaultCodeData = new DINT();
        Inp_ReadyData = new BOOL();
        Inp_1RunFdbkData = new BOOL();
        Inp_2RunFdbkData = new BOOL();
        Inp_AlarmData = new BOOL();
        Inp_FaultedData = new BOOL();
        Inp_DvcNotify = new SINT();
        Inp_IOFault = new BOOL();
        Inp_1PermOK = new BOOL();
        Inp_1NBPermOK = new BOOL();
        Inp_2PermOK = new BOOL();
        Inp_2NBPermOK = new BOOL();
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
        Cfg_eObjType = new SINT();
        Cfg_HasStart1 = new BOOL();
        Cfg_HasStart2 = new BOOL();
        Cfg_HasJog1 = new BOOL();
        Cfg_HasJog2 = new BOOL();
        Cfg_HasStop = new BOOL();
        Cfg_AllowLocal = new BOOL();
        Cfg_HasRunFdbk = new BOOL();
        Cfg_UseRunFdbk = new BOOL();
        Cfg_HasDvcObj = new BOOL();
        Cfg_Has1PermObj = new BOOL();
        Cfg_Has2PermObj = new BOOL();
        Cfg_HasIntlkObj = new BOOL();
        Cfg_HasResInhObj = new BOOL();
        Cfg_HasRunTimeObj = new BOOL();
        Cfg_HasMoreObj = new BOOL();
        Cfg_OperStopPrio = new BOOL();
        Cfg_ExtStopPrio = new BOOL();
        Cfg_OCmdResets = new BOOL();
        Cfg_XCmdResets = new BOOL();
        Cfg_OvrdPermIntlk = new BOOL();
        Cfg_ShedOnFailToStart = new BOOL();
        Cfg_ShedOnIOFault = new BOOL();
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
        Cfg_PauseTime = new REAL();
        Cfg_StartHornTime = new REAL();
        Cfg_VirtualFdbkTime = new REAL();
        Cfg_FailToStartTime = new REAL();
        Cfg_FailToStopTime = new REAL();
        Cfg_ResetPulseTime = new REAL();
        Cfg_MaxJogTime = new REAL();
        Cfg_eKeepStart = new SINT();
        Cfg_eKeepJog = new SINT();
        Cfg_CnfrmReqd = new SINT();
        Cfg_HasHistTrend = new SINT();
        PSet_Owner = new DINT();
        PCmd_Virtual = new BOOL();
        PCmd_Physical = new BOOL();
        PCmd_Start1 = new BOOL();
        PCmd_Start2 = new BOOL();
        PCmd_Stop = new BOOL();
        PCmd_Prog = new BOOL();
        PCmd_Oper = new BOOL();
        PCmd_Lock = new BOOL();
        PCmd_Unlock = new BOOL();
        PCmd_Normal = new BOOL();
        PCmd_Reset = new BOOL();
        XCmd_Start1 = new BOOL();
        XCmd_Start2 = new BOOL();
        XCmd_Stop = new BOOL();
        XCmd_Jog1 = new BOOL();
        XCmd_Jog2 = new BOOL();
        XCmd_Acq = new BOOL();
        XCmd_Rel = new BOOL();
        XCmd_Reset = new BOOL();
        XCmd_ResetAckAll = new BOOL();
        Out_Run1Data = new BOOL();
        Out_Run2Data = new BOOL();
        Out_Start1Data = new BOOL();
        Out_Start2Data = new BOOL();
        Out_StopData = new BOOL();
        Out_ClearFaultData = new BOOL();
        Out_HornData = new BOOL();
        Out_Reset = new BOOL();
        Out_OwnerSts = new DINT();
        Sts_Initialized = new BOOL();
        Sts_Stopped = new BOOL();
        Sts_Starting1 = new BOOL();
        Sts_Starting2 = new BOOL();
        Sts_Running1 = new BOOL();
        Sts_Running2 = new BOOL();
        Sts_Stopping = new BOOL();
        Sts_Jogging1 = new BOOL();
        Sts_Jogging2 = new BOOL();
        Sts_Horn = new BOOL();
        Sts_NotReady = new BOOL();
        Sts_Alarm = new BOOL();
        Sts_Virtual = new BOOL();
        SrcQ_IO = new SINT();
        SrcQ = new SINT();
        Sts_eCmd = new SINT();
        Sts_eFdbk = new SINT();
        Sts_eSts = new SINT();
        Sts_eFault = new SINT();
        Sts_eNotify = new SINT();
        Sts_eNotifyAll = new SINT();
        Sts_eNotifyIOFault = new SINT();
        Sts_eNotifyFailToStart = new SINT();
        Sts_eNotifyFailToStop = new SINT();
        Sts_eNotifyIntlkTrip = new SINT();
        Sts_eNotifyMotorFault = new SINT();
        Sts_UnackAlmCount = new DINT();
        Sts_eFaultCode = new DINT();
        Sts_eSrc = new INT();
        Sts_bSrc = new INT();
        Sts_Available = new BOOL();
        Sts_IntlkAvailable = new BOOL();
        Sts_Bypass = new BOOL();
        Sts_BypActive = new BOOL();
        Sts_MaintByp = new BOOL();
        Sts_NotRdy = new BOOL();
        Sts_NrdyCfgErr = new BOOL();
        Sts_NrdyDvcNotReady = new BOOL();
        Sts_NrdyFail = new BOOL();
        Sts_NrdyIntlk = new BOOL();
        Sts_NrdyIOFault = new BOOL();
        Sts_NrdyOoS = new BOOL();
        Sts_Nrdy1Perm = new BOOL();
        Sts_Nrdy2Perm = new BOOL();
        Sts_NrdyPerm = new BOOL();
        Sts_NrdyPrioStop = new BOOL();
        Sts_NrdyTrip = new BOOL();
        Sts_Err = new BOOL();
        Sts_ErrAlm = new BOOL();
        Sts_ErrPauseTime = new BOOL();
        Sts_ErrVirtualFdbkTime = new BOOL();
        Sts_ErrFailToStartTime = new BOOL();
        Sts_ErrFailToStopTime = new BOOL();
        Sts_ErrResetPulseTime = new BOOL();
        Sts_ErrMaxJogTime = new BOOL();
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
        Sts_FailToStart = new BOOL();
        Sts_FailToStop = new BOOL();
        Sts_IntlkTrip = new BOOL();
        Sts_MotorFault = new BOOL();
        Sts_RdyAck = new BOOL();
        Sts_RdyReset = new BOOL();
        XRdy_Acq = new BOOL();
        XRdy_Rel = new BOOL();
        XRdy_Start1 = new BOOL();
        XRdy_Start2 = new BOOL();
        XRdy_Jog1 = new BOOL();
        XRdy_Jog2 = new BOOL();
        XRdy_Stop = new BOOL();
        XRdy_Reset = new BOOL();
        XRdy_ResetAckAll = new BOOL();
        Val_Owner = new DINT();
    }
    
    /// <summary>
    /// Creates a new <see cref="P_MOTOR_DISCRETE"/> instance initialized with the provided element.
    /// </summary>
    public P_MOTOR_DISCRETE(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_InitializeReq</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Inp_InitializeReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_OwnerCmd</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public DINT Inp_OwnerCmd
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_LastFaultCodeData</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public DINT Inp_LastFaultCodeData
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_ReadyData</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Inp_ReadyData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_1RunFdbkData</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Inp_1RunFdbkData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_2RunFdbkData</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Inp_2RunFdbkData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_AlarmData</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Inp_AlarmData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_FaultedData</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Inp_FaultedData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_DvcNotify</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public SINT Inp_DvcNotify
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_IOFault</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Inp_IOFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_1PermOK</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Inp_1PermOK
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_1NBPermOK</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Inp_1NBPermOK
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_2PermOK</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Inp_2PermOK
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_2NBPermOK</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Inp_2NBPermOK
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_IntlkOK</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Inp_IntlkOK
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_NBIntlkOK</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Inp_NBIntlkOK
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_IntlkAvailable</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Inp_IntlkAvailable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_IntlkTripInh</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Inp_IntlkTripInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_RdyReset</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Inp_RdyReset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Hand</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Inp_Hand
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Ovrd</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Inp_Ovrd
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_OvrdCmd</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public SINT Inp_OvrdCmd
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_ExtInh</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Inp_ExtInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_HornInh</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Inp_HornInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Reset</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Inp_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_AllowDisable</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_AllowDisable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_AllowShelve</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_AllowShelve
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_eObjType</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public SINT Cfg_eObjType
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasStart1</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_HasStart1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasStart2</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_HasStart2
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasJog1</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_HasJog1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasJog2</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_HasJog2
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasStop</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_HasStop
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_AllowLocal</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_AllowLocal
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasRunFdbk</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_HasRunFdbk
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_UseRunFdbk</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_UseRunFdbk
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasDvcObj</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_HasDvcObj
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_Has1PermObj</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_Has1PermObj
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_Has2PermObj</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_Has2PermObj
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasIntlkObj</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_HasIntlkObj
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasResInhObj</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_HasResInhObj
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasRunTimeObj</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_HasRunTimeObj
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasMoreObj</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_HasMoreObj
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OperStopPrio</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_OperStopPrio
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ExtStopPrio</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_ExtStopPrio
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OCmdResets</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_OCmdResets
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_XCmdResets</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_XCmdResets
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OvrdPermIntlk</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_OvrdPermIntlk
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ShedOnFailToStart</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_ShedOnFailToStart
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ShedOnIOFault</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_ShedOnIOFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasOper</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_HasOper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasOperLocked</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_HasOperLocked
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasProg</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_HasProg
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasProgLocked</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_HasProgLocked
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasExt</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_HasExt
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasMaint</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_HasMaint
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasMaintOoS</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_HasMaintOoS
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OvrdOverLock</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_OvrdOverLock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ExtOverLock</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_ExtOverLock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ProgPwrUp</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_ProgPwrUp
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ProgNormal</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_ProgNormal
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PCmdPriority</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_PCmdPriority
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PCmdProgAsLevel</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_PCmdProgAsLevel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PCmdLockAsLevel</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_PCmdLockAsLevel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ExtAcqAsLevel</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Cfg_ExtAcqAsLevel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PauseTime</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public REAL Cfg_PauseTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_StartHornTime</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public REAL Cfg_StartHornTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_VirtualFdbkTime</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public REAL Cfg_VirtualFdbkTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_FailToStartTime</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public REAL Cfg_FailToStartTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_FailToStopTime</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public REAL Cfg_FailToStopTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ResetPulseTime</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public REAL Cfg_ResetPulseTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_MaxJogTime</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public REAL Cfg_MaxJogTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_eKeepStart</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public SINT Cfg_eKeepStart
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_eKeepJog</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public SINT Cfg_eKeepJog
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CnfrmReqd</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public SINT Cfg_CnfrmReqd
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasHistTrend</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public SINT Cfg_HasHistTrend
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PSet_Owner</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public DINT PSet_Owner
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Virtual</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL PCmd_Virtual
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Physical</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL PCmd_Physical
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Start1</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL PCmd_Start1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Start2</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL PCmd_Start2
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Stop</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL PCmd_Stop
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Prog</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL PCmd_Prog
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Oper</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL PCmd_Oper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Lock</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL PCmd_Lock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Unlock</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL PCmd_Unlock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Normal</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL PCmd_Normal
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Reset</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL PCmd_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Start1</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL XCmd_Start1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Start2</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL XCmd_Start2
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Stop</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL XCmd_Stop
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Jog1</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL XCmd_Jog1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Jog2</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL XCmd_Jog2
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Acq</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL XCmd_Acq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Rel</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL XCmd_Rel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Reset</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL XCmd_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_ResetAckAll</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL XCmd_ResetAckAll
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_Run1Data</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Out_Run1Data
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_Run2Data</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Out_Run2Data
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_Start1Data</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Out_Start1Data
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_Start2Data</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Out_Start2Data
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_StopData</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Out_StopData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_ClearFaultData</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Out_ClearFaultData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_HornData</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Out_HornData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_Reset</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Out_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_OwnerSts</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public DINT Out_OwnerSts
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Initialized</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_Initialized
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Stopped</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_Stopped
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Starting1</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_Starting1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Starting2</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_Starting2
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Running1</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_Running1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Running2</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_Running2
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Stopping</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_Stopping
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Jogging1</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_Jogging1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Jogging2</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_Jogging2
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Horn</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_Horn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NotReady</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_NotReady
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Alarm</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_Alarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Virtual</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_Virtual
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SrcQ_IO</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public SINT SrcQ_IO
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SrcQ</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public SINT SrcQ
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eCmd</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public SINT Sts_eCmd
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eFdbk</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public SINT Sts_eFdbk
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eSts</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public SINT Sts_eSts
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eFault</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public SINT Sts_eFault
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotify</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public SINT Sts_eNotify
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyAll</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public SINT Sts_eNotifyAll
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyIOFault</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public SINT Sts_eNotifyIOFault
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyFailToStart</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public SINT Sts_eNotifyFailToStart
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyFailToStop</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public SINT Sts_eNotifyFailToStop
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyIntlkTrip</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public SINT Sts_eNotifyIntlkTrip
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyMotorFault</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public SINT Sts_eNotifyMotorFault
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_UnackAlmCount</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public DINT Sts_UnackAlmCount
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eFaultCode</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public DINT Sts_eFaultCode
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eSrc</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public INT Sts_eSrc
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_bSrc</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public INT Sts_bSrc
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Available</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_Available
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_IntlkAvailable</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_IntlkAvailable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Bypass</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_Bypass
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_BypActive</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_BypActive
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_MaintByp</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_MaintByp
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NotRdy</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_NotRdy
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyCfgErr</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_NrdyCfgErr
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyDvcNotReady</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_NrdyDvcNotReady
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyFail</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_NrdyFail
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyIntlk</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_NrdyIntlk
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyIOFault</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_NrdyIOFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyOoS</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_NrdyOoS
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Nrdy1Perm</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_Nrdy1Perm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Nrdy2Perm</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_Nrdy2Perm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyPerm</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_NrdyPerm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyPrioStop</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_NrdyPrioStop
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyTrip</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_NrdyTrip
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Err</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_Err
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrAlm</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_ErrAlm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrPauseTime</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_ErrPauseTime
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrVirtualFdbkTime</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_ErrVirtualFdbkTime
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrFailToStartTime</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_ErrFailToStartTime
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrFailToStopTime</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_ErrFailToStopTime
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrResetPulseTime</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_ErrResetPulseTime
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrMaxJogTime</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_ErrMaxJogTime
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Hand</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_Hand
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_OoS</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_OoS
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Maint</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_Maint
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Ovrd</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_Ovrd
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Ext</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_Ext
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Prog</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_Prog
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ProgLocked</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_ProgLocked
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Oper</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_Oper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_OperLocked</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_OperLocked
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ProgOperSel</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_ProgOperSel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ProgOperLock</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_ProgOperLock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Normal</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_Normal
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ExtReqInh</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_ExtReqInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ProgReqInh</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_ProgReqInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_MAcqRcvd</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_MAcqRcvd
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_CmdConflict</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_CmdConflict
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Alm</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_Alm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_AlmInh</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_AlmInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_IOFault</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_IOFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_FailToStart</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_FailToStart
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_FailToStop</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_FailToStop
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_IntlkTrip</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_IntlkTrip
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_MotorFault</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_MotorFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_RdyAck</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_RdyAck
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_RdyReset</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL Sts_RdyReset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Acq</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL XRdy_Acq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Rel</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL XRdy_Rel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Start1</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL XRdy_Start1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Start2</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL XRdy_Start2
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Jog1</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL XRdy_Jog1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Jog2</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL XRdy_Jog2
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Stop</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL XRdy_Stop
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Reset</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL XRdy_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_ResetAckAll</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public BOOL XRdy_ResetAckAll
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_Owner</c> member of the <see cref="P_MOTOR_DISCRETE"/> data type.
    /// </summary>
    public DINT Val_Owner
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }
}