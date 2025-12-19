using System.Xml.Linq;
// Auto-generated file
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart

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
