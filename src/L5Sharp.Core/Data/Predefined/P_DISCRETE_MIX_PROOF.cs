using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>P_DISCRETE_MIX_PROOF</c> data type structure.
/// </summary>
[LogixData("P_DISCRETE_MIX_PROOF")]
public sealed partial class P_DISCRETE_MIX_PROOF : StructureData
{
    /// <summary>
    /// Creates a new <see cref="P_DISCRETE_MIX_PROOF"/> instance initialized with default values.
    /// </summary>
    public P_DISCRETE_MIX_PROOF() : base("P_DISCRETE_MIX_PROOF")
    {
        EnableIn = new BOOL();
        EnableOut = new BOOL();
        Inp_InitializeReq = new BOOL();
        Inp_OwnerCmd = new DINT();
        Inp_OpenLSData = new BOOL();
        Inp_ClosedLSData = new BOOL();
        Inp_LowerSeatLSData = new BOOL();
        Inp_UpperSeatLSData = new BOOL();
        Inp_CavityInletLSData = new BOOL();
        Inp_CavityOutletLSData = new BOOL();
        Inp_IOFault = new BOOL();
        Inp_OpenIntlkOK = new BOOL();
        Inp_OpenNBIntlkOK = new BOOL();
        Inp_OpenIntlkAvailable = new BOOL();
        Inp_LowerSeatIntlkOK = new BOOL();
        Inp_LowerSeatNBIntlkOK = new BOOL();
        Inp_LowerSeatIntlkAvailable = new BOOL();
        Inp_UpperSeatIntlkOK = new BOOL();
        Inp_UpperSeatNBIntlkOK = new BOOL();
        Inp_UpperSeatIntlkAvailable = new BOOL();
        Inp_CavityIntlkOK = new BOOL();
        Inp_CavityNBIntlkOK = new BOOL();
        Inp_CavityIntlkAvailable = new BOOL();
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
        Cfg_HasLiftLower = new BOOL();
        Cfg_HasLiftUpper = new BOOL();
        Cfg_HasCleanCavity = new BOOL();
        Cfg_HasCleanLower = new BOOL();
        Cfg_HasCleanUpper = new BOOL();
        Cfg_bOutStateSt0 = new SINT();
        Cfg_bOutStateSt1 = new SINT();
        Cfg_bOutStateSt2 = new SINT();
        Cfg_bOutStateSt3 = new SINT();
        Cfg_bOutStateSt4 = new SINT();
        Cfg_bOutStateSt5 = new SINT();
        Cfg_bOutStateSt6 = new SINT();
        Cfg_bOutStateSt7 = new SINT();
        Cfg_bOutStateSt8 = new SINT();
        Cfg_bOutStateSt9 = new SINT();
        Cfg_bOutStateSt10 = new SINT();
        Cfg_bFdbkStateSt0 = new SINT();
        Cfg_bFdbkStateSt1 = new SINT();
        Cfg_bFdbkStateSt2 = new SINT();
        Cfg_bFdbkStateSt3 = new SINT();
        Cfg_bFdbkStateSt4 = new SINT();
        Cfg_bFdbkStateSt5 = new SINT();
        Cfg_bFdbkStateSt6 = new SINT();
        Cfg_bFdbkStateSt7 = new SINT();
        Cfg_bFdbkStateSt8 = new SINT();
        Cfg_bFdbkStateSt9 = new SINT();
        Cfg_bFdbkStateSt10 = new SINT();
        Cfg_bFdbkReqdSt0 = new SINT();
        Cfg_bFdbkReqdSt1 = new SINT();
        Cfg_bFdbkReqdSt2 = new SINT();
        Cfg_bFdbkReqdSt3 = new SINT();
        Cfg_bFdbkReqdSt4 = new SINT();
        Cfg_bFdbkReqdSt5 = new SINT();
        Cfg_bFdbkReqdSt6 = new SINT();
        Cfg_bFdbkReqdSt7 = new SINT();
        Cfg_bFdbkReqdSt8 = new SINT();
        Cfg_bFdbkReqdSt9 = new SINT();
        Cfg_bFdbkReqdSt10 = new SINT();
        Cfg_FdbkTimeSt0 = new REAL();
        Cfg_FdbkTimeSt1 = new REAL();
        Cfg_FdbkTimeSt2 = new REAL();
        Cfg_FdbkTimeSt3 = new REAL();
        Cfg_FdbkTimeSt4 = new REAL();
        Cfg_FdbkTimeSt5 = new REAL();
        Cfg_FdbkTimeSt6 = new REAL();
        Cfg_FdbkTimeSt7 = new REAL();
        Cfg_FdbkTimeSt8 = new REAL();
        Cfg_FdbkTimeSt9 = new REAL();
        Cfg_FdbkTimeSt10 = new REAL();
        Cfg_PulseLiftLower = new BOOL();
        Cfg_PulseLiftUpper = new BOOL();
        Cfg_PulseCleanLower = new BOOL();
        Cfg_PulseCleanUpper = new BOOL();
        Cfg_HasOpenIntlkObj = new BOOL();
        Cfg_HasLowerSeatIntlkObj = new BOOL();
        Cfg_HasUpperSeatIntlkObj = new BOOL();
        Cfg_HasCavityIntlkObj = new BOOL();
        Cfg_HasStatsObj = new BOOL();
        Cfg_HasMoreObj = new BOOL();
        Cfg_OperClosePrio = new BOOL();
        Cfg_ExtClosePrio = new BOOL();
        Cfg_OCmdResets = new BOOL();
        Cfg_XCmdResets = new BOOL();
        Cfg_OvrdPermIntlk = new BOOL();
        Cfg_ShedOnFail = new BOOL();
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
        Cfg_OpenPulseTime = new REAL();
        Cfg_ClosePulseTime = new REAL();
        Cfg_StartHornTime = new REAL();
        Cfg_FailTime = new REAL();
        Cfg_CnfrmReqd = new SINT();
        PSet_Owner = new DINT();
        PCmd_Virtual = new BOOL();
        PCmd_Physical = new BOOL();
        PCmd_Close = new BOOL();
        PCmd_Open = new BOOL();
        PCmd_LiftLower = new BOOL();
        PCmd_LiftUpper = new BOOL();
        PCmd_CleanCavity = new BOOL();
        PCmd_CleanLower = new BOOL();
        PCmd_CleanUpper = new BOOL();
        PCmd_Reset = new BOOL();
        PCmd_Prog = new BOOL();
        PCmd_Oper = new BOOL();
        PCmd_Lock = new BOOL();
        PCmd_Unlock = new BOOL();
        PCmd_Normal = new BOOL();
        XCmd_Close = new BOOL();
        XCmd_Open = new BOOL();
        XCmd_LiftLower = new BOOL();
        XCmd_LiftUpper = new BOOL();
        XCmd_CleanCavity = new BOOL();
        XCmd_CleanLower = new BOOL();
        XCmd_CleanUpper = new BOOL();
        XCmd_Acq = new BOOL();
        XCmd_Rel = new BOOL();
        XCmd_Reset = new BOOL();
        XCmd_ResetAckAll = new BOOL();
        Out_OpenData = new BOOL();
        Out_CloseData = new BOOL();
        Out_LiftLowerData = new BOOL();
        Out_LiftUpperData = new BOOL();
        Out_CavityInletData = new BOOL();
        Out_CavityOutletData = new BOOL();
        Out_LocatorData = new BOOL();
        Out_HornData = new BOOL();
        Out_Reset = new BOOL();
        Out_OwnerSts = new DINT();
        Sts_Initialized = new BOOL();
        Sts_Closed = new BOOL();
        Sts_Opened = new BOOL();
        Sts_LiftLower = new BOOL();
        Sts_LiftUpper = new BOOL();
        Sts_CleanCavity = new BOOL();
        Sts_CleanLower = new BOOL();
        Sts_CleanUpper = new BOOL();
        Sts_Moving = new BOOL();
        Sts_Pulsing = new BOOL();
        Sts_Locator = new BOOL();
        Sts_Horn = new BOOL();
        Sts_Physical = new BOOL();
        Sts_Virtual = new BOOL();
        SrcQ_IO = new SINT();
        SrcQ = new SINT();
        Sts_eFdbk = new SINT();
        Sts_eCmd = new SINT();
        Sts_eSts = new SINT();
        Sts_eState = new SINT();
        Sts_eFault = new SINT();
        Sts_eOutState = new SINT();
        Sts_eNotify = new SINT();
        Sts_eNotifyAll = new SINT();
        Sts_eNotifyIOFault = new SINT();
        Sts_eNotifyFail = new SINT();
        Sts_eNotifyIntlkTrip = new SINT();
        Sts_UnackAlmCount = new SINT();
        Sts_eSrc = new INT();
        Sts_bSrc = new INT();
        Sts_Available = new BOOL();
        Sts_OpenIntlkAvailable = new BOOL();
        Sts_LowerSeatIntlkAvailable = new BOOL();
        Sts_UpperSeatIntlkAvailable = new BOOL();
        Sts_CavityIntlkAvailable = new BOOL();
        Sts_Bypass = new BOOL();
        Sts_BypActive = new BOOL();
        Sts_MaintByp = new BOOL();
        Sts_NotRdy = new BOOL();
        Sts_NrdyCfgErr = new BOOL();
        Sts_NrdyFail = new BOOL();
        Sts_NrdyOpenIntlk = new BOOL();
        Sts_NrdyLowerSeatIntlk = new BOOL();
        Sts_NrdyUpperSeatIntlk = new BOOL();
        Sts_NrdyCavityIntlk = new BOOL();
        Sts_NrdyIOFault = new BOOL();
        Sts_NrdyOoS = new BOOL();
        Sts_NrdyPrioClose = new BOOL();
        Sts_NrdyVirtualPhysical = new BOOL();
        Sts_Err = new BOOL();
        Sts_ErrAlm = new BOOL();
        Sts_bErrFdbkTime = new INT();
        Sts_ErrOpenPulseTime = new BOOL();
        Sts_ErrClosePulseTime = new BOOL();
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
        Sts_RdyAck = new BOOL();
        Sts_RdyReset = new BOOL();
        XRdy_Acq = new BOOL();
        XRdy_Rel = new BOOL();
        XRdy_Close = new BOOL();
        XRdy_Open = new BOOL();
        XRdy_LiftLower = new BOOL();
        XRdy_LiftUpper = new BOOL();
        XRdy_CleanCavity = new BOOL();
        XRdy_CleanLower = new BOOL();
        XRdy_CleanUpper = new BOOL();
        XRdy_Reset = new BOOL();
        XRdy_ResetAckAll = new BOOL();
        Val_Owner = new DINT();
    }
    
    /// <summary>
    /// Creates a new <see cref="P_DISCRETE_MIX_PROOF"/> instance initialized with the provided element.
    /// </summary>
    public P_DISCRETE_MIX_PROOF(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_InitializeReq</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Inp_InitializeReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_OwnerCmd</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public DINT Inp_OwnerCmd
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_OpenLSData</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Inp_OpenLSData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_ClosedLSData</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Inp_ClosedLSData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_LowerSeatLSData</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Inp_LowerSeatLSData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_UpperSeatLSData</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Inp_UpperSeatLSData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_CavityInletLSData</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Inp_CavityInletLSData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_CavityOutletLSData</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Inp_CavityOutletLSData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_IOFault</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Inp_IOFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_OpenIntlkOK</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Inp_OpenIntlkOK
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_OpenNBIntlkOK</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Inp_OpenNBIntlkOK
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_OpenIntlkAvailable</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Inp_OpenIntlkAvailable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_LowerSeatIntlkOK</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Inp_LowerSeatIntlkOK
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_LowerSeatNBIntlkOK</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Inp_LowerSeatNBIntlkOK
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_LowerSeatIntlkAvailable</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Inp_LowerSeatIntlkAvailable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_UpperSeatIntlkOK</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Inp_UpperSeatIntlkOK
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_UpperSeatNBIntlkOK</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Inp_UpperSeatNBIntlkOK
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_UpperSeatIntlkAvailable</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Inp_UpperSeatIntlkAvailable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_CavityIntlkOK</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Inp_CavityIntlkOK
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_CavityNBIntlkOK</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Inp_CavityNBIntlkOK
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_CavityIntlkAvailable</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Inp_CavityIntlkAvailable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_IntlkTripInh</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Inp_IntlkTripInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_RdyReset</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Inp_RdyReset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Hand</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Inp_Hand
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Ovrd</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Inp_Ovrd
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_OvrdCmd</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public SINT Inp_OvrdCmd
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_ExtInh</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Inp_ExtInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_HornInh</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Inp_HornInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Reset</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Inp_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_AllowDisable</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Cfg_AllowDisable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_AllowShelve</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Cfg_AllowShelve
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasLiftLower</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Cfg_HasLiftLower
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasLiftUpper</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Cfg_HasLiftUpper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasCleanCavity</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Cfg_HasCleanCavity
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasCleanLower</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Cfg_HasCleanLower
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasCleanUpper</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Cfg_HasCleanUpper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_bOutStateSt0</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public SINT Cfg_bOutStateSt0
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_bOutStateSt1</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public SINT Cfg_bOutStateSt1
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_bOutStateSt2</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public SINT Cfg_bOutStateSt2
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_bOutStateSt3</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public SINT Cfg_bOutStateSt3
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_bOutStateSt4</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public SINT Cfg_bOutStateSt4
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_bOutStateSt5</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public SINT Cfg_bOutStateSt5
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_bOutStateSt6</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public SINT Cfg_bOutStateSt6
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_bOutStateSt7</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public SINT Cfg_bOutStateSt7
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_bOutStateSt8</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public SINT Cfg_bOutStateSt8
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_bOutStateSt9</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public SINT Cfg_bOutStateSt9
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_bOutStateSt10</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public SINT Cfg_bOutStateSt10
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_bFdbkStateSt0</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public SINT Cfg_bFdbkStateSt0
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_bFdbkStateSt1</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public SINT Cfg_bFdbkStateSt1
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_bFdbkStateSt2</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public SINT Cfg_bFdbkStateSt2
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_bFdbkStateSt3</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public SINT Cfg_bFdbkStateSt3
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_bFdbkStateSt4</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public SINT Cfg_bFdbkStateSt4
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_bFdbkStateSt5</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public SINT Cfg_bFdbkStateSt5
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_bFdbkStateSt6</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public SINT Cfg_bFdbkStateSt6
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_bFdbkStateSt7</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public SINT Cfg_bFdbkStateSt7
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_bFdbkStateSt8</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public SINT Cfg_bFdbkStateSt8
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_bFdbkStateSt9</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public SINT Cfg_bFdbkStateSt9
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_bFdbkStateSt10</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public SINT Cfg_bFdbkStateSt10
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_bFdbkReqdSt0</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public SINT Cfg_bFdbkReqdSt0
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_bFdbkReqdSt1</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public SINT Cfg_bFdbkReqdSt1
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_bFdbkReqdSt2</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public SINT Cfg_bFdbkReqdSt2
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_bFdbkReqdSt3</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public SINT Cfg_bFdbkReqdSt3
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_bFdbkReqdSt4</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public SINT Cfg_bFdbkReqdSt4
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_bFdbkReqdSt5</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public SINT Cfg_bFdbkReqdSt5
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_bFdbkReqdSt6</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public SINT Cfg_bFdbkReqdSt6
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_bFdbkReqdSt7</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public SINT Cfg_bFdbkReqdSt7
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_bFdbkReqdSt8</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public SINT Cfg_bFdbkReqdSt8
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_bFdbkReqdSt9</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public SINT Cfg_bFdbkReqdSt9
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_bFdbkReqdSt10</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public SINT Cfg_bFdbkReqdSt10
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_FdbkTimeSt0</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public REAL Cfg_FdbkTimeSt0
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_FdbkTimeSt1</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public REAL Cfg_FdbkTimeSt1
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_FdbkTimeSt2</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public REAL Cfg_FdbkTimeSt2
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_FdbkTimeSt3</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public REAL Cfg_FdbkTimeSt3
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_FdbkTimeSt4</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public REAL Cfg_FdbkTimeSt4
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_FdbkTimeSt5</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public REAL Cfg_FdbkTimeSt5
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_FdbkTimeSt6</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public REAL Cfg_FdbkTimeSt6
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_FdbkTimeSt7</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public REAL Cfg_FdbkTimeSt7
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_FdbkTimeSt8</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public REAL Cfg_FdbkTimeSt8
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_FdbkTimeSt9</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public REAL Cfg_FdbkTimeSt9
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_FdbkTimeSt10</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public REAL Cfg_FdbkTimeSt10
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PulseLiftLower</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Cfg_PulseLiftLower
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PulseLiftUpper</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Cfg_PulseLiftUpper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PulseCleanLower</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Cfg_PulseCleanLower
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PulseCleanUpper</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Cfg_PulseCleanUpper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasOpenIntlkObj</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Cfg_HasOpenIntlkObj
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasLowerSeatIntlkObj</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Cfg_HasLowerSeatIntlkObj
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasUpperSeatIntlkObj</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Cfg_HasUpperSeatIntlkObj
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasCavityIntlkObj</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Cfg_HasCavityIntlkObj
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasStatsObj</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Cfg_HasStatsObj
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasMoreObj</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Cfg_HasMoreObj
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OperClosePrio</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Cfg_OperClosePrio
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ExtClosePrio</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Cfg_ExtClosePrio
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OCmdResets</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Cfg_OCmdResets
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_XCmdResets</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Cfg_XCmdResets
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OvrdPermIntlk</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Cfg_OvrdPermIntlk
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ShedOnFail</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Cfg_ShedOnFail
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ShedOnIOFault</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Cfg_ShedOnIOFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasOper</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Cfg_HasOper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasOperLocked</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Cfg_HasOperLocked
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasProg</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Cfg_HasProg
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasProgLocked</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Cfg_HasProgLocked
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasExt</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Cfg_HasExt
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasMaint</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Cfg_HasMaint
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasMaintOoS</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Cfg_HasMaintOoS
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OvrdOverLock</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Cfg_OvrdOverLock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ExtOverLock</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Cfg_ExtOverLock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ProgPwrUp</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Cfg_ProgPwrUp
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ProgNormal</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Cfg_ProgNormal
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PCmdPriority</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Cfg_PCmdPriority
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PCmdProgAsLevel</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Cfg_PCmdProgAsLevel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PCmdLockAsLevel</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Cfg_PCmdLockAsLevel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ExtAcqAsLevel</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Cfg_ExtAcqAsLevel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OpenPulseTime</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public REAL Cfg_OpenPulseTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ClosePulseTime</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public REAL Cfg_ClosePulseTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_StartHornTime</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public REAL Cfg_StartHornTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_FailTime</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public REAL Cfg_FailTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CnfrmReqd</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public SINT Cfg_CnfrmReqd
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PSet_Owner</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public DINT PSet_Owner
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Virtual</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL PCmd_Virtual
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Physical</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL PCmd_Physical
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Close</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL PCmd_Close
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Open</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL PCmd_Open
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_LiftLower</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL PCmd_LiftLower
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_LiftUpper</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL PCmd_LiftUpper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_CleanCavity</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL PCmd_CleanCavity
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_CleanLower</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL PCmd_CleanLower
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_CleanUpper</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL PCmd_CleanUpper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Reset</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL PCmd_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Prog</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL PCmd_Prog
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Oper</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL PCmd_Oper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Lock</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL PCmd_Lock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Unlock</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL PCmd_Unlock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Normal</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL PCmd_Normal
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Close</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL XCmd_Close
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Open</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL XCmd_Open
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_LiftLower</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL XCmd_LiftLower
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_LiftUpper</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL XCmd_LiftUpper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_CleanCavity</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL XCmd_CleanCavity
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_CleanLower</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL XCmd_CleanLower
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_CleanUpper</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL XCmd_CleanUpper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Acq</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL XCmd_Acq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Rel</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL XCmd_Rel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Reset</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL XCmd_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_ResetAckAll</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL XCmd_ResetAckAll
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_OpenData</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Out_OpenData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_CloseData</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Out_CloseData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_LiftLowerData</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Out_LiftLowerData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_LiftUpperData</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Out_LiftUpperData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_CavityInletData</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Out_CavityInletData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_CavityOutletData</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Out_CavityOutletData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_LocatorData</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Out_LocatorData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_HornData</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Out_HornData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_Reset</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Out_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_OwnerSts</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public DINT Out_OwnerSts
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Initialized</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_Initialized
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Closed</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_Closed
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Opened</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_Opened
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_LiftLower</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_LiftLower
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_LiftUpper</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_LiftUpper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_CleanCavity</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_CleanCavity
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_CleanLower</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_CleanLower
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_CleanUpper</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_CleanUpper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Moving</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_Moving
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Pulsing</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_Pulsing
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Locator</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_Locator
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Horn</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_Horn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Physical</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_Physical
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Virtual</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_Virtual
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SrcQ_IO</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public SINT SrcQ_IO
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SrcQ</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public SINT SrcQ
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eFdbk</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public SINT Sts_eFdbk
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eCmd</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public SINT Sts_eCmd
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eSts</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public SINT Sts_eSts
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eState</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public SINT Sts_eState
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eFault</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public SINT Sts_eFault
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eOutState</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public SINT Sts_eOutState
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotify</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public SINT Sts_eNotify
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyAll</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public SINT Sts_eNotifyAll
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyIOFault</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public SINT Sts_eNotifyIOFault
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyFail</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public SINT Sts_eNotifyFail
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyIntlkTrip</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public SINT Sts_eNotifyIntlkTrip
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_UnackAlmCount</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public SINT Sts_UnackAlmCount
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eSrc</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public INT Sts_eSrc
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_bSrc</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public INT Sts_bSrc
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Available</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_Available
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_OpenIntlkAvailable</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_OpenIntlkAvailable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_LowerSeatIntlkAvailable</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_LowerSeatIntlkAvailable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_UpperSeatIntlkAvailable</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_UpperSeatIntlkAvailable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_CavityIntlkAvailable</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_CavityIntlkAvailable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Bypass</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_Bypass
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_BypActive</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_BypActive
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_MaintByp</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_MaintByp
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NotRdy</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_NotRdy
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyCfgErr</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_NrdyCfgErr
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyFail</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_NrdyFail
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyOpenIntlk</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_NrdyOpenIntlk
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyLowerSeatIntlk</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_NrdyLowerSeatIntlk
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyUpperSeatIntlk</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_NrdyUpperSeatIntlk
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyCavityIntlk</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_NrdyCavityIntlk
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyIOFault</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_NrdyIOFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyOoS</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_NrdyOoS
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyPrioClose</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_NrdyPrioClose
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyVirtualPhysical</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_NrdyVirtualPhysical
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Err</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_Err
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrAlm</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_ErrAlm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_bErrFdbkTime</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public INT Sts_bErrFdbkTime
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrOpenPulseTime</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_ErrOpenPulseTime
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrClosePulseTime</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_ErrClosePulseTime
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrFailTime</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_ErrFailTime
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Hand</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_Hand
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_OoS</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_OoS
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Maint</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_Maint
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Ovrd</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_Ovrd
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Ext</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_Ext
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Prog</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_Prog
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ProgLocked</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_ProgLocked
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Oper</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_Oper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_OperLocked</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_OperLocked
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ProgOperSel</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_ProgOperSel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ProgOperLock</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_ProgOperLock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Normal</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_Normal
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ExtReqInh</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_ExtReqInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ProgReqInh</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_ProgReqInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_MAcqRcvd</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_MAcqRcvd
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_CmdConflict</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_CmdConflict
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Alm</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_Alm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_AlmInh</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_AlmInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_IOFault</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_IOFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Fail</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_Fail
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_IntlkTrip</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_IntlkTrip
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_RdyAck</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_RdyAck
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_RdyReset</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL Sts_RdyReset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Acq</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL XRdy_Acq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Rel</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL XRdy_Rel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Close</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL XRdy_Close
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Open</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL XRdy_Open
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_LiftLower</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL XRdy_LiftLower
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_LiftUpper</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL XRdy_LiftUpper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_CleanCavity</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL XRdy_CleanCavity
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_CleanLower</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL XRdy_CleanLower
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_CleanUpper</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL XRdy_CleanUpper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Reset</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL XRdy_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_ResetAckAll</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public BOOL XRdy_ResetAckAll
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_Owner</c> member of the <see cref="P_DISCRETE_MIX_PROOF"/> data type.
    /// </summary>
    public DINT Val_Owner
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }
}