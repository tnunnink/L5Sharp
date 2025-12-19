using System.Xml.Linq;
// Auto-generated file
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>P_VARIABLE_SPEED_DRIVE</c> data type structure.
/// </summary>
[LogixData("P_VARIABLE_SPEED_DRIVE")]
public sealed partial class P_VARIABLE_SPEED_DRIVE : StructureData
{
    /// <summary>
    /// Creates a new <see cref="P_VARIABLE_SPEED_DRIVE"/> instance initialized with default values.
    /// </summary>
    public P_VARIABLE_SPEED_DRIVE() : base("P_VARIABLE_SPEED_DRIVE")
    {
        EnableIn = new BOOL();
        EnableOut = new BOOL();
        Inp_InitializeReq = new BOOL();
        Inp_OwnerCmd = new DINT();
        Inp_SpeedFdbkData = new REAL();
        Inp_DatalinkData = new REAL();
        Inp_LastFaultCodeData = new DINT();
        Inp_ReadyData = new BOOL();
        Inp_RunningData = new BOOL();
        Inp_CommandDirData = new BOOL();
        Inp_ActualDirData = new BOOL();
        Inp_AcceleratingData = new BOOL();
        Inp_DeceleratingData = new BOOL();
        Inp_AtSpeedData = new BOOL();
        Inp_AlarmData = new BOOL();
        Inp_FaultedData = new BOOL();
        Inp_DvcNotify = new SINT();
        Inp_IOFault = new BOOL();
        Inp_FwdPermOK = new BOOL();
        Inp_FwdNBPermOK = new BOOL();
        Inp_RevPermOK = new BOOL();
        Inp_RevNBPermOK = new BOOL();
        Inp_IntlkOK = new BOOL();
        Inp_NBIntlkOK = new BOOL();
        Inp_IntlkAvailable = new BOOL();
        Inp_IntlkTripInh = new BOOL();
        Inp_RdyReset = new BOOL();
        Inp_Hand = new BOOL();
        Inp_Ovrd = new BOOL();
        Inp_OvrdCmd = new SINT();
        Inp_OvrdSpeed = new REAL();
        Inp_OvrdOutDatalink = new REAL();
        Inp_ExtInh = new BOOL();
        Inp_HornInh = new BOOL();
        Inp_Reset = new BOOL();
        Cfg_AllowDisable = new BOOL();
        Cfg_AllowShelve = new BOOL();
        Cfg_HasReverse = new BOOL();
        Cfg_HasJog = new BOOL();
        Cfg_AllowLocal = new BOOL();
        Cfg_HasRunFdbk = new BOOL();
        Cfg_UseRunFdbk = new BOOL();
        Cfg_HasSpeedFdbk = new BOOL();
        Cfg_UseSpeedFdbk = new BOOL();
        Cfg_HasInpDatalink = new BOOL();
        Cfg_HasOutDatalink = new BOOL();
        Cfg_HasDvcObj = new BOOL();
        Cfg_HasFwdPermObj = new BOOL();
        Cfg_HasRevPermObj = new BOOL();
        Cfg_HasIntlkObj = new BOOL();
        Cfg_HasResInhObj = new BOOL();
        Cfg_HasRunTimeObj = new BOOL();
        Cfg_HasMoreObj = new BOOL();
        Cfg_SetTrack = new BOOL();
        Cfg_SetTrackOvrdHand = new BOOL();
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
        Cfg_DecPlcs = new SINT();
        Cfg_InpDatalinkDecPlcs = new SINT();
        Cfg_OutDatalinkDecPlcs = new SINT();
        Cfg_MinSpeedRef = new REAL();
        Cfg_MaxSpeedRef = new REAL();
        Cfg_JogSpeedRef = new REAL();
        Cfg_SpeedRefRawMin = new REAL();
        Cfg_SpeedRefRawMax = new REAL();
        Cfg_SpeedRefEUMin = new REAL();
        Cfg_SpeedRefEUMax = new REAL();
        Cfg_SpeedFdbkRawMin = new REAL();
        Cfg_SpeedFdbkRawMax = new REAL();
        Cfg_SpeedFdbkEUMin = new REAL();
        Cfg_SpeedFdbkEUMax = new REAL();
        Cfg_InpDatalinkRawMin = new REAL();
        Cfg_InpDatalinkRawMax = new REAL();
        Cfg_InpDatalinkEUMin = new REAL();
        Cfg_InpDatalinkEUMax = new REAL();
        Cfg_OutDatalinkMin = new REAL();
        Cfg_OutDatalinkMax = new REAL();
        Cfg_OutDatalinkRawMin = new REAL();
        Cfg_OutDatalinkRawMax = new REAL();
        Cfg_OutDatalinkEUMin = new REAL();
        Cfg_OutDatalinkEUMax = new REAL();
        Cfg_StartHornTime = new REAL();
        Cfg_VirtualRampTime = new REAL();
        Cfg_FailToStartTime = new REAL();
        Cfg_FailToStopTime = new REAL();
        Cfg_ResetPulseTime = new REAL();
        Cfg_MaxJogTime = new REAL();
        Cfg_eKeepRef = new SINT();
        Cfg_eKeepStart = new SINT();
        Cfg_eKeepJog = new SINT();
        Cfg_eKeepOutDatalink = new SINT();
        Cfg_CnfrmReqd = new SINT();
        Cfg_HasHistTrend = new SINT();
        PSet_SpeedRef = new REAL();
        PSet_OutDatalink = new REAL();
        PSet_Owner = new DINT();
        XSet_SpeedRef = new REAL();
        XSet_OutDatalink = new REAL();
        PCmd_Virtual = new BOOL();
        PCmd_Physical = new BOOL();
        PCmd_StartFwd = new BOOL();
        PCmd_StartRev = new BOOL();
        PCmd_Stop = new BOOL();
        PCmd_Prog = new BOOL();
        PCmd_Oper = new BOOL();
        PCmd_Lock = new BOOL();
        PCmd_Unlock = new BOOL();
        PCmd_Normal = new BOOL();
        PCmd_Reset = new BOOL();
        XCmd_StartFwd = new BOOL();
        XCmd_StartRev = new BOOL();
        XCmd_Stop = new BOOL();
        XCmd_JogFwd = new BOOL();
        XCmd_JogRev = new BOOL();
        XCmd_Acq = new BOOL();
        XCmd_Rel = new BOOL();
        XCmd_Reset = new BOOL();
        XCmd_ResetAckAll = new BOOL();
        Out_SpeedRefData = new REAL();
        Out_DatalinkData = new REAL();
        Out_RunData = new BOOL();
        Out_StopData = new BOOL();
        Out_StartData = new BOOL();
        Out_ClearFaultData = new BOOL();
        Out_FwdData = new BOOL();
        Out_RevData = new BOOL();
        Out_HornData = new BOOL();
        Out_Reset = new BOOL();
        Out_OwnerSts = new DINT();
        Val_SpeedRef = new REAL();
        Val_SpeedFdbk = new REAL();
        Val_InpDatalink = new REAL();
        Val_OutDatalink = new REAL();
        Val_SpeedRefEUMin = new REAL();
        Val_SpeedRefEUMax = new REAL();
        Val_SpeedFdbkEUMin = new REAL();
        Val_SpeedFdbkEUMax = new REAL();
        Sts_Initialized = new BOOL();
        Sts_Stopped = new BOOL();
        Sts_StartingFwd = new BOOL();
        Sts_StartingRev = new BOOL();
        Sts_RunningFwd = new BOOL();
        Sts_RunningRev = new BOOL();
        Sts_StoppingFwd = new BOOL();
        Sts_StoppingRev = new BOOL();
        Sts_JoggingFwd = new BOOL();
        Sts_JoggingRev = new BOOL();
        Sts_Horn = new BOOL();
        Sts_CommandDir = new BOOL();
        Sts_ActualDir = new BOOL();
        Sts_Accel = new BOOL();
        Sts_Decel = new BOOL();
        Sts_NotReady = new BOOL();
        Sts_Alarm = new BOOL();
        Sts_AtSpeed = new BOOL();
        Sts_SpeedLimited = new BOOL();
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
        Sts_eNotifyDriveFault = new SINT();
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
        Sts_NrdyDriveNotReady = new BOOL();
        Sts_NrdyFail = new BOOL();
        Sts_NrdyIntlk = new BOOL();
        Sts_NrdyIOFault = new BOOL();
        Sts_NrdyOoS = new BOOL();
        Sts_NrdyFwdPerm = new BOOL();
        Sts_NrdyRevPerm = new BOOL();
        Sts_NrdyPerm = new BOOL();
        Sts_NrdyPrioStop = new BOOL();
        Sts_NrdyTrip = new BOOL();
        Sts_Err = new BOOL();
        Sts_ErrAlm = new BOOL();
        Sts_ErrSpeedFdbkRaw = new BOOL();
        Sts_ErrSpeedFdbkEU = new BOOL();
        Sts_ErrSpeedRefLim = new BOOL();
        Sts_ErrSpeedRefEU = new BOOL();
        Sts_ErrSpeedRefRaw = new BOOL();
        Sts_ErrInpDatalinkRaw = new BOOL();
        Sts_ErrInpDatalinkEU = new BOOL();
        Sts_ErrOutDatalinkLim = new BOOL();
        Sts_ErrOutDatalinkEU = new BOOL();
        Sts_ErrOutDatalinkRaw = new BOOL();
        Sts_ErrVirtualRampTime = new BOOL();
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
        Sts_DriveFault = new BOOL();
        Sts_RdyAck = new BOOL();
        Sts_RdyReset = new BOOL();
        XRdy_Acq = new BOOL();
        XRdy_Rel = new BOOL();
        XRdy_StartFwd = new BOOL();
        XRdy_StartRev = new BOOL();
        XRdy_JogFwd = new BOOL();
        XRdy_JogRev = new BOOL();
        XRdy_Stop = new BOOL();
        XRdy_Reset = new BOOL();
        XRdy_ResetAckAll = new BOOL();
        Val_Owner = new DINT();
    }

    /// <summary>
    /// Creates a new <see cref="P_VARIABLE_SPEED_DRIVE"/> instance initialized with the provided element.
    /// </summary>
    public P_VARIABLE_SPEED_DRIVE(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_InitializeReq</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Inp_InitializeReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_OwnerCmd</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public DINT Inp_OwnerCmd
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_SpeedFdbkData</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public REAL Inp_SpeedFdbkData
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_DatalinkData</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public REAL Inp_DatalinkData
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_LastFaultCodeData</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public DINT Inp_LastFaultCodeData
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_ReadyData</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Inp_ReadyData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_RunningData</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Inp_RunningData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_CommandDirData</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Inp_CommandDirData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_ActualDirData</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Inp_ActualDirData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_AcceleratingData</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Inp_AcceleratingData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_DeceleratingData</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Inp_DeceleratingData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_AtSpeedData</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Inp_AtSpeedData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_AlarmData</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Inp_AlarmData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_FaultedData</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Inp_FaultedData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_DvcNotify</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public SINT Inp_DvcNotify
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_IOFault</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Inp_IOFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_FwdPermOK</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Inp_FwdPermOK
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_FwdNBPermOK</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Inp_FwdNBPermOK
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_RevPermOK</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Inp_RevPermOK
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_RevNBPermOK</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Inp_RevNBPermOK
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_IntlkOK</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Inp_IntlkOK
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_NBIntlkOK</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Inp_NBIntlkOK
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_IntlkAvailable</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Inp_IntlkAvailable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_IntlkTripInh</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Inp_IntlkTripInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_RdyReset</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Inp_RdyReset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Hand</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Inp_Hand
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Ovrd</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Inp_Ovrd
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_OvrdCmd</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public SINT Inp_OvrdCmd
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_OvrdSpeed</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public REAL Inp_OvrdSpeed
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_OvrdOutDatalink</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public REAL Inp_OvrdOutDatalink
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_ExtInh</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Inp_ExtInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_HornInh</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Inp_HornInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Reset</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Inp_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_AllowDisable</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Cfg_AllowDisable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_AllowShelve</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Cfg_AllowShelve
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasReverse</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Cfg_HasReverse
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasJog</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Cfg_HasJog
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_AllowLocal</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Cfg_AllowLocal
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasRunFdbk</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Cfg_HasRunFdbk
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_UseRunFdbk</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Cfg_UseRunFdbk
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasSpeedFdbk</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Cfg_HasSpeedFdbk
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_UseSpeedFdbk</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Cfg_UseSpeedFdbk
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasInpDatalink</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Cfg_HasInpDatalink
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasOutDatalink</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Cfg_HasOutDatalink
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasDvcObj</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Cfg_HasDvcObj
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasFwdPermObj</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Cfg_HasFwdPermObj
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasRevPermObj</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Cfg_HasRevPermObj
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasIntlkObj</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Cfg_HasIntlkObj
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasResInhObj</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Cfg_HasResInhObj
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasRunTimeObj</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Cfg_HasRunTimeObj
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasMoreObj</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Cfg_HasMoreObj
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_SetTrack</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Cfg_SetTrack
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_SetTrackOvrdHand</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Cfg_SetTrackOvrdHand
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OperStopPrio</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Cfg_OperStopPrio
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ExtStopPrio</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Cfg_ExtStopPrio
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OCmdResets</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Cfg_OCmdResets
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_XCmdResets</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Cfg_XCmdResets
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OvrdPermIntlk</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Cfg_OvrdPermIntlk
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ShedOnFailToStart</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Cfg_ShedOnFailToStart
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ShedOnIOFault</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Cfg_ShedOnIOFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasOper</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Cfg_HasOper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasOperLocked</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Cfg_HasOperLocked
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasProg</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Cfg_HasProg
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasProgLocked</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Cfg_HasProgLocked
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasExt</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Cfg_HasExt
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasMaint</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Cfg_HasMaint
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasMaintOoS</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Cfg_HasMaintOoS
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OvrdOverLock</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Cfg_OvrdOverLock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ExtOverLock</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Cfg_ExtOverLock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ProgPwrUp</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Cfg_ProgPwrUp
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ProgNormal</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Cfg_ProgNormal
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PCmdPriority</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Cfg_PCmdPriority
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PCmdProgAsLevel</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Cfg_PCmdProgAsLevel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PCmdLockAsLevel</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Cfg_PCmdLockAsLevel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ExtAcqAsLevel</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Cfg_ExtAcqAsLevel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_DecPlcs</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public SINT Cfg_DecPlcs
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_InpDatalinkDecPlcs</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public SINT Cfg_InpDatalinkDecPlcs
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OutDatalinkDecPlcs</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public SINT Cfg_OutDatalinkDecPlcs
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_MinSpeedRef</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public REAL Cfg_MinSpeedRef
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_MaxSpeedRef</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public REAL Cfg_MaxSpeedRef
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_JogSpeedRef</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public REAL Cfg_JogSpeedRef
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_SpeedRefRawMin</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public REAL Cfg_SpeedRefRawMin
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_SpeedRefRawMax</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public REAL Cfg_SpeedRefRawMax
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_SpeedRefEUMin</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public REAL Cfg_SpeedRefEUMin
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_SpeedRefEUMax</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public REAL Cfg_SpeedRefEUMax
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_SpeedFdbkRawMin</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public REAL Cfg_SpeedFdbkRawMin
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_SpeedFdbkRawMax</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public REAL Cfg_SpeedFdbkRawMax
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_SpeedFdbkEUMin</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public REAL Cfg_SpeedFdbkEUMin
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_SpeedFdbkEUMax</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public REAL Cfg_SpeedFdbkEUMax
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_InpDatalinkRawMin</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public REAL Cfg_InpDatalinkRawMin
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_InpDatalinkRawMax</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public REAL Cfg_InpDatalinkRawMax
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_InpDatalinkEUMin</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public REAL Cfg_InpDatalinkEUMin
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_InpDatalinkEUMax</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public REAL Cfg_InpDatalinkEUMax
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OutDatalinkMin</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public REAL Cfg_OutDatalinkMin
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OutDatalinkMax</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public REAL Cfg_OutDatalinkMax
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OutDatalinkRawMin</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public REAL Cfg_OutDatalinkRawMin
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OutDatalinkRawMax</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public REAL Cfg_OutDatalinkRawMax
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OutDatalinkEUMin</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public REAL Cfg_OutDatalinkEUMin
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OutDatalinkEUMax</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public REAL Cfg_OutDatalinkEUMax
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_StartHornTime</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public REAL Cfg_StartHornTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_VirtualRampTime</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public REAL Cfg_VirtualRampTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_FailToStartTime</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public REAL Cfg_FailToStartTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_FailToStopTime</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public REAL Cfg_FailToStopTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ResetPulseTime</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public REAL Cfg_ResetPulseTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_MaxJogTime</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public REAL Cfg_MaxJogTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_eKeepRef</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public SINT Cfg_eKeepRef
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_eKeepStart</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public SINT Cfg_eKeepStart
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_eKeepJog</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public SINT Cfg_eKeepJog
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_eKeepOutDatalink</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public SINT Cfg_eKeepOutDatalink
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CnfrmReqd</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public SINT Cfg_CnfrmReqd
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasHistTrend</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public SINT Cfg_HasHistTrend
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PSet_SpeedRef</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public REAL PSet_SpeedRef
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PSet_OutDatalink</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public REAL PSet_OutDatalink
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PSet_Owner</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public DINT PSet_Owner
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XSet_SpeedRef</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public REAL XSet_SpeedRef
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XSet_OutDatalink</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public REAL XSet_OutDatalink
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Virtual</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL PCmd_Virtual
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Physical</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL PCmd_Physical
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_StartFwd</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL PCmd_StartFwd
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_StartRev</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL PCmd_StartRev
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Stop</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL PCmd_Stop
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Prog</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL PCmd_Prog
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Oper</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL PCmd_Oper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Lock</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL PCmd_Lock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Unlock</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL PCmd_Unlock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Normal</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL PCmd_Normal
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Reset</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL PCmd_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_StartFwd</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL XCmd_StartFwd
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_StartRev</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL XCmd_StartRev
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Stop</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL XCmd_Stop
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_JogFwd</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL XCmd_JogFwd
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_JogRev</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL XCmd_JogRev
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Acq</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL XCmd_Acq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Rel</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL XCmd_Rel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Reset</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL XCmd_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_ResetAckAll</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL XCmd_ResetAckAll
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_SpeedRefData</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public REAL Out_SpeedRefData
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_DatalinkData</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public REAL Out_DatalinkData
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_RunData</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Out_RunData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_StopData</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Out_StopData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_StartData</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Out_StartData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_ClearFaultData</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Out_ClearFaultData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_FwdData</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Out_FwdData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_RevData</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Out_RevData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_HornData</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Out_HornData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_Reset</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Out_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_OwnerSts</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public DINT Out_OwnerSts
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_SpeedRef</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public REAL Val_SpeedRef
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_SpeedFdbk</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public REAL Val_SpeedFdbk
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_InpDatalink</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public REAL Val_InpDatalink
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_OutDatalink</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public REAL Val_OutDatalink
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_SpeedRefEUMin</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public REAL Val_SpeedRefEUMin
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_SpeedRefEUMax</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public REAL Val_SpeedRefEUMax
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_SpeedFdbkEUMin</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public REAL Val_SpeedFdbkEUMin
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_SpeedFdbkEUMax</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public REAL Val_SpeedFdbkEUMax
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Initialized</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_Initialized
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Stopped</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_Stopped
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_StartingFwd</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_StartingFwd
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_StartingRev</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_StartingRev
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_RunningFwd</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_RunningFwd
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_RunningRev</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_RunningRev
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_StoppingFwd</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_StoppingFwd
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_StoppingRev</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_StoppingRev
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_JoggingFwd</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_JoggingFwd
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_JoggingRev</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_JoggingRev
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Horn</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_Horn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_CommandDir</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_CommandDir
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ActualDir</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_ActualDir
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Accel</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_Accel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Decel</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_Decel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NotReady</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_NotReady
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Alarm</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_Alarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_AtSpeed</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_AtSpeed
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_SpeedLimited</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_SpeedLimited
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Virtual</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_Virtual
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SrcQ_IO</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public SINT SrcQ_IO
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SrcQ</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public SINT SrcQ
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eCmd</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public SINT Sts_eCmd
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eFdbk</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public SINT Sts_eFdbk
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eSts</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public SINT Sts_eSts
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eFault</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public SINT Sts_eFault
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotify</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public SINT Sts_eNotify
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyAll</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public SINT Sts_eNotifyAll
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyIOFault</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public SINT Sts_eNotifyIOFault
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyFailToStart</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public SINT Sts_eNotifyFailToStart
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyFailToStop</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public SINT Sts_eNotifyFailToStop
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyIntlkTrip</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public SINT Sts_eNotifyIntlkTrip
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyDriveFault</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public SINT Sts_eNotifyDriveFault
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_UnackAlmCount</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public DINT Sts_UnackAlmCount
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eFaultCode</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public DINT Sts_eFaultCode
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eSrc</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public INT Sts_eSrc
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_bSrc</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public INT Sts_bSrc
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Available</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_Available
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_IntlkAvailable</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_IntlkAvailable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Bypass</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_Bypass
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_BypActive</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_BypActive
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_MaintByp</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_MaintByp
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NotRdy</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_NotRdy
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyCfgErr</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_NrdyCfgErr
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyDriveNotReady</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_NrdyDriveNotReady
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyFail</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_NrdyFail
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyIntlk</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_NrdyIntlk
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyIOFault</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_NrdyIOFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyOoS</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_NrdyOoS
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyFwdPerm</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_NrdyFwdPerm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyRevPerm</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_NrdyRevPerm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyPerm</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_NrdyPerm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyPrioStop</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_NrdyPrioStop
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyTrip</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_NrdyTrip
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Err</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_Err
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrAlm</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_ErrAlm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrSpeedFdbkRaw</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_ErrSpeedFdbkRaw
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrSpeedFdbkEU</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_ErrSpeedFdbkEU
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrSpeedRefLim</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_ErrSpeedRefLim
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrSpeedRefEU</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_ErrSpeedRefEU
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrSpeedRefRaw</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_ErrSpeedRefRaw
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrInpDatalinkRaw</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_ErrInpDatalinkRaw
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrInpDatalinkEU</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_ErrInpDatalinkEU
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrOutDatalinkLim</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_ErrOutDatalinkLim
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrOutDatalinkEU</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_ErrOutDatalinkEU
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrOutDatalinkRaw</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_ErrOutDatalinkRaw
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrVirtualRampTime</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_ErrVirtualRampTime
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrFailToStartTime</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_ErrFailToStartTime
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrFailToStopTime</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_ErrFailToStopTime
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrResetPulseTime</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_ErrResetPulseTime
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrMaxJogTime</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_ErrMaxJogTime
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Hand</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_Hand
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_OoS</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_OoS
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Maint</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_Maint
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Ovrd</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_Ovrd
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Ext</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_Ext
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Prog</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_Prog
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ProgLocked</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_ProgLocked
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Oper</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_Oper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_OperLocked</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_OperLocked
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ProgOperSel</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_ProgOperSel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ProgOperLock</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_ProgOperLock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Normal</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_Normal
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ExtReqInh</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_ExtReqInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ProgReqInh</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_ProgReqInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_MAcqRcvd</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_MAcqRcvd
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_CmdConflict</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_CmdConflict
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Alm</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_Alm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_AlmInh</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_AlmInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_IOFault</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_IOFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_FailToStart</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_FailToStart
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_FailToStop</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_FailToStop
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_IntlkTrip</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_IntlkTrip
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_DriveFault</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_DriveFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_RdyAck</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_RdyAck
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_RdyReset</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL Sts_RdyReset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Acq</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL XRdy_Acq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Rel</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL XRdy_Rel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_StartFwd</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL XRdy_StartFwd
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_StartRev</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL XRdy_StartRev
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_JogFwd</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL XRdy_JogFwd
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_JogRev</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL XRdy_JogRev
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Stop</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL XRdy_Stop
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Reset</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL XRdy_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_ResetAckAll</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public BOOL XRdy_ResetAckAll
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_Owner</c> member of the <see cref="P_VARIABLE_SPEED_DRIVE"/> data type.
    /// </summary>
    public DINT Val_Owner
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

}
