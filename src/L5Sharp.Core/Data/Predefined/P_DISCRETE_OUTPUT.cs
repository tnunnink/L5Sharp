using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>P_DISCRETE_OUTPUT</c> data type structure.
/// </summary>
[LogixData("P_DISCRETE_OUTPUT")]
public sealed partial class P_DISCRETE_OUTPUT : StructureData
{
    /// <summary>
    /// Creates a new <see cref="P_DISCRETE_OUTPUT"/> instance initialized with default values.
    /// </summary>
    public P_DISCRETE_OUTPUT() : base("P_DISCRETE_OUTPUT")
    {
        EnableIn = new BOOL();
        EnableOut = new BOOL();
        Inp_InitializeReq = new BOOL();
        Inp_OwnerCmd = new DINT();
        Inp_OnFdbkData = new BOOL();
        Inp_OffFdbkData = new BOOL();
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
        Cfg_HornOnChange = new BOOL();
        Cfg_ExtOffPrio = new BOOL();
        Cfg_XCmdResets = new BOOL();
        Cfg_AllowDisable = new BOOL();
        Cfg_AllowShelve = new BOOL();
        Cfg_HasPulse = new BOOL();
        Cfg_CompletePulse = new BOOL();
        Cfg_FdbkFail = new BOOL();
        Cfg_HasOnFdbk = new BOOL();
        Cfg_HasOffFdbk = new BOOL();
        Cfg_UseOnFdbk = new BOOL();
        Cfg_UseOffFdbk = new BOOL();
        Cfg_OperOffPrio = new BOOL();
        Cfg_OCmdResets = new BOOL();
        Cfg_ShedOnIOFault = new BOOL();
        Cfg_ShedOnFail = new BOOL();
        Cfg_HasPermObj = new BOOL();
        Cfg_HasIntlkObj = new BOOL();
        Cfg_HasMoreObj = new BOOL();
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
        Cfg_OvrdPermIntlk = new BOOL();
        Cfg_OnDly = new REAL();
        Cfg_OffDly = new REAL();
        Cfg_OnPulseTime = new REAL();
        Cfg_OffPulseTime = new REAL();
        Cfg_OnFailTime = new REAL();
        Cfg_OffFailTime = new REAL();
        Cfg_StartHornTime = new REAL();
        Cfg_VirtualFdbkTime = new REAL();
        Cfg_CnfrmReqd = new SINT();
        PSet_Owner = new DINT();
        PCmd_Virtual = new BOOL();
        PCmd_Physical = new BOOL();
        PCmd_On = new BOOL();
        PCmd_Off = new BOOL();
        PCmd_OnPulse = new BOOL();
        PCmd_OffPulse = new BOOL();
        PCmd_ContPulse = new BOOL();
        PCmd_Oper = new BOOL();
        PCmd_Prog = new BOOL();
        PCmd_Lock = new BOOL();
        PCmd_Unlock = new BOOL();
        PCmd_Normal = new BOOL();
        PCmd_Reset = new BOOL();
        XCmd_On = new BOOL();
        XCmd_Off = new BOOL();
        XCmd_OnPulse = new BOOL();
        XCmd_OffPulse = new BOOL();
        XCmd_ContPulse = new BOOL();
        XCmd_Acq = new BOOL();
        XCmd_Rel = new BOOL();
        XCmd_Reset = new BOOL();
        XCmd_ResetAckAll = new BOOL();
        Out_CVData = new BOOL();
        Out_HornData = new BOOL();
        Out_Reset = new BOOL();
        Out_OwnerSts = new DINT();
        Sts_Initialized = new BOOL();
        Sts_Out = new BOOL();
        Sts_Pulsing = new BOOL();
        Sts_FdbkOff = new BOOL();
        Sts_FdbkOn = new BOOL();
        Sts_FdbkFail = new BOOL();
        Sts_Horn = new BOOL();
        Sts_Virtual = new BOOL();
        SrcQ_IO = new SINT();
        SrcQ = new SINT();
        Sts_eCmd = new SINT();
        Sts_eFdbk = new SINT();
        Sts_eSts = new SINT();
        Sts_eFault = new SINT();
        Sts_eState = new SINT();
        Sts_eNotify = new SINT();
        Sts_eNotifyAll = new SINT();
        Sts_eNotifyIOFault = new SINT();
        Sts_eNotifyOnFail = new SINT();
        Sts_eNotifyOffFail = new SINT();
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
        Sts_NrdyIntlk = new BOOL();
        Sts_NrdyPerm = new BOOL();
        Sts_NrdyPrioOff = new BOOL();
        Sts_NrdyFail = new BOOL();
        Sts_NrdyIOFault = new BOOL();
        Sts_Err = new BOOL();
        Sts_ErrOnDly = new BOOL();
        Sts_ErrOffDly = new BOOL();
        Sts_ErrOnPulseTime = new BOOL();
        Sts_ErrOffPulseTime = new BOOL();
        Sts_ErrOnFailTime = new BOOL();
        Sts_ErrOffFailTime = new BOOL();
        Sts_ErrStartHornTime = new BOOL();
        Sts_ErrVirtualFdbkTime = new BOOL();
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
        Sts_OnFail = new BOOL();
        Sts_OffFail = new BOOL();
        Sts_IntlkTrip = new BOOL();
        Sts_RdyAck = new BOOL();
        Sts_RdyReset = new BOOL();
        XRdy_Acq = new BOOL();
        XRdy_Rel = new BOOL();
        XRdy_On = new BOOL();
        XRdy_Off = new BOOL();
        XRdy_OnPulse = new BOOL();
        XRdy_OffPulse = new BOOL();
        XRdy_ContPulse = new BOOL();
        XRdy_Reset = new BOOL();
        XRdy_ResetAckAll = new BOOL();
        Val_Owner = new DINT();
    }
    
    /// <summary>
    /// Creates a new <see cref="P_DISCRETE_OUTPUT"/> instance initialized with the provided element.
    /// </summary>
    public P_DISCRETE_OUTPUT(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 304;
    
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
        Inp_OnFdbkData.UpdateData((data[offset + 9] & (1 << 3)) != 0);
        Inp_OffFdbkData.UpdateData((data[offset + 9] & (1 << 4)) != 0);
        Inp_IOFault.UpdateData((data[offset + 9] & (1 << 5)) != 0);
        Inp_PermOK.UpdateData((data[offset + 9] & (1 << 6)) != 0);
        Inp_NBPermOK.UpdateData((data[offset + 9] & (1 << 7)) != 0);
        Inp_IntlkOK.UpdateData((data[offset + 10] & (1 << 0)) != 0);
        Inp_NBIntlkOK.UpdateData((data[offset + 10] & (1 << 1)) != 0);
        Inp_IntlkAvailable.UpdateData((data[offset + 10] & (1 << 2)) != 0);
        Inp_IntlkTripInh.UpdateData((data[offset + 10] & (1 << 3)) != 0);
        Inp_RdyReset.UpdateData((data[offset + 10] & (1 << 4)) != 0);
        Inp_Hand.UpdateData((data[offset + 10] & (1 << 5)) != 0);
        Inp_Ovrd.UpdateData((data[offset + 10] & (1 << 6)) != 0);
        Inp_OvrdCmd.UpdateData(data, offset + 10);
        Inp_ExtInh.UpdateData((data[offset + 11] & (1 << 7)) != 0);
        Inp_HornInh.UpdateData((data[offset + 12] & (1 << 0)) != 0);
        Inp_Reset.UpdateData((data[offset + 12] & (1 << 1)) != 0);
        Cfg_HornOnChange.UpdateData((data[offset + 12] & (1 << 2)) != 0);
        Cfg_ExtOffPrio.UpdateData((data[offset + 12] & (1 << 3)) != 0);
        Cfg_XCmdResets.UpdateData((data[offset + 12] & (1 << 4)) != 0);
        Cfg_AllowDisable.UpdateData((data[offset + 12] & (1 << 5)) != 0);
        Cfg_AllowShelve.UpdateData((data[offset + 12] & (1 << 6)) != 0);
        Cfg_HasPulse.UpdateData((data[offset + 12] & (1 << 7)) != 0);
        Cfg_CompletePulse.UpdateData((data[offset + 13] & (1 << 0)) != 0);
        Cfg_FdbkFail.UpdateData((data[offset + 13] & (1 << 1)) != 0);
        Cfg_HasOnFdbk.UpdateData((data[offset + 13] & (1 << 2)) != 0);
        Cfg_HasOffFdbk.UpdateData((data[offset + 13] & (1 << 3)) != 0);
        Cfg_UseOnFdbk.UpdateData((data[offset + 13] & (1 << 4)) != 0);
        Cfg_UseOffFdbk.UpdateData((data[offset + 13] & (1 << 5)) != 0);
        Cfg_OperOffPrio.UpdateData((data[offset + 13] & (1 << 6)) != 0);
        Cfg_OCmdResets.UpdateData((data[offset + 13] & (1 << 7)) != 0);
        Cfg_ShedOnIOFault.UpdateData((data[offset + 14] & (1 << 0)) != 0);
        Cfg_ShedOnFail.UpdateData((data[offset + 14] & (1 << 1)) != 0);
        Cfg_HasPermObj.UpdateData((data[offset + 14] & (1 << 2)) != 0);
        Cfg_HasIntlkObj.UpdateData((data[offset + 18] & (1 << 3)) != 0);
        Cfg_HasMoreObj.UpdateData((data[offset + 18] & (1 << 4)) != 0);
        Cfg_HasOper.UpdateData((data[offset + 18] & (1 << 5)) != 0);
        Cfg_HasOperLocked.UpdateData((data[offset + 18] & (1 << 6)) != 0);
        Cfg_HasProg.UpdateData((data[offset + 18] & (1 << 7)) != 0);
        Cfg_HasProgLocked.UpdateData((data[offset + 19] & (1 << 0)) != 0);
        Cfg_HasExt.UpdateData((data[offset + 19] & (1 << 1)) != 0);
        Cfg_HasMaint.UpdateData((data[offset + 19] & (1 << 2)) != 0);
        Cfg_HasMaintOoS.UpdateData((data[offset + 19] & (1 << 3)) != 0);
        Cfg_OvrdOverLock.UpdateData((data[offset + 19] & (1 << 4)) != 0);
        Cfg_ExtOverLock.UpdateData((data[offset + 19] & (1 << 5)) != 0);
        Cfg_ProgPwrUp.UpdateData((data[offset + 19] & (1 << 6)) != 0);
        Cfg_ProgNormal.UpdateData((data[offset + 19] & (1 << 7)) != 0);
        Cfg_PCmdPriority.UpdateData((data[offset + 20] & (1 << 0)) != 0);
        Cfg_PCmdProgAsLevel.UpdateData((data[offset + 20] & (1 << 1)) != 0);
        Cfg_PCmdLockAsLevel.UpdateData((data[offset + 20] & (1 << 2)) != 0);
        Cfg_ExtAcqAsLevel.UpdateData((data[offset + 20] & (1 << 3)) != 0);
        Cfg_OvrdPermIntlk.UpdateData((data[offset + 20] & (1 << 4)) != 0);
        Cfg_OnDly.UpdateData(data, offset + 20);
        Cfg_OffDly.UpdateData(data, offset + 24);
        Cfg_OnPulseTime.UpdateData(data, offset + 28);
        Cfg_OffPulseTime.UpdateData(data, offset + 32);
        Cfg_OnFailTime.UpdateData(data, offset + 36);
        Cfg_OffFailTime.UpdateData(data, offset + 40);
        Cfg_StartHornTime.UpdateData(data, offset + 44);
        Cfg_VirtualFdbkTime.UpdateData(data, offset + 48);
        Cfg_CnfrmReqd.UpdateData(data, offset + 52);
        PSet_Owner.UpdateData(data, offset + 53);
        PCmd_Virtual.UpdateData((data[offset + 57] & (1 << 5)) != 0);
        PCmd_Physical.UpdateData((data[offset + 57] & (1 << 6)) != 0);
        PCmd_On.UpdateData((data[offset + 57] & (1 << 7)) != 0);
        PCmd_Off.UpdateData((data[offset + 58] & (1 << 0)) != 0);
        PCmd_OnPulse.UpdateData((data[offset + 58] & (1 << 1)) != 0);
        PCmd_OffPulse.UpdateData((data[offset + 58] & (1 << 2)) != 0);
        PCmd_ContPulse.UpdateData((data[offset + 58] & (1 << 3)) != 0);
        PCmd_Oper.UpdateData((data[offset + 58] & (1 << 4)) != 0);
        PCmd_Prog.UpdateData((data[offset + 58] & (1 << 5)) != 0);
        PCmd_Lock.UpdateData((data[offset + 58] & (1 << 6)) != 0);
        PCmd_Unlock.UpdateData((data[offset + 58] & (1 << 7)) != 0);
        PCmd_Normal.UpdateData((data[offset + 59] & (1 << 0)) != 0);
        PCmd_Reset.UpdateData((data[offset + 59] & (1 << 1)) != 0);
        XCmd_On.UpdateData((data[offset + 59] & (1 << 2)) != 0);
        XCmd_Off.UpdateData((data[offset + 59] & (1 << 3)) != 0);
        XCmd_OnPulse.UpdateData((data[offset + 59] & (1 << 4)) != 0);
        XCmd_OffPulse.UpdateData((data[offset + 59] & (1 << 5)) != 0);
        XCmd_ContPulse.UpdateData((data[offset + 59] & (1 << 6)) != 0);
        XCmd_Acq.UpdateData((data[offset + 59] & (1 << 7)) != 0);
        XCmd_Rel.UpdateData((data[offset + 60] & (1 << 0)) != 0);
        XCmd_Reset.UpdateData((data[offset + 60] & (1 << 1)) != 0);
        XCmd_ResetAckAll.UpdateData((data[offset + 60] & (1 << 2)) != 0);
        Out_CVData.UpdateData((data[offset + 60] & (1 << 3)) != 0);
        Out_HornData.UpdateData((data[offset + 60] & (1 << 4)) != 0);
        Out_Reset.UpdateData((data[offset + 60] & (1 << 5)) != 0);
        Out_OwnerSts.UpdateData(data, offset + 60);
        Sts_Initialized.UpdateData((data[offset + 64] & (1 << 6)) != 0);
        Sts_Out.UpdateData((data[offset + 64] & (1 << 7)) != 0);
        Sts_Pulsing.UpdateData((data[offset + 65] & (1 << 0)) != 0);
        Sts_FdbkOff.UpdateData((data[offset + 65] & (1 << 1)) != 0);
        Sts_FdbkOn.UpdateData((data[offset + 65] & (1 << 2)) != 0);
        Sts_FdbkFail.UpdateData((data[offset + 65] & (1 << 3)) != 0);
        Sts_Horn.UpdateData((data[offset + 65] & (1 << 4)) != 0);
        Sts_Virtual.UpdateData((data[offset + 65] & (1 << 5)) != 0);
        SrcQ_IO.UpdateData(data, offset + 65);
        SrcQ.UpdateData(data, offset + 66);
        Sts_eCmd.UpdateData(data, offset + 67);
        Sts_eFdbk.UpdateData(data, offset + 68);
        Sts_eSts.UpdateData(data, offset + 69);
        Sts_eFault.UpdateData(data, offset + 70);
        Sts_eState.UpdateData(data, offset + 71);
        Sts_eNotify.UpdateData(data, offset + 72);
        Sts_eNotifyAll.UpdateData(data, offset + 73);
        Sts_eNotifyIOFault.UpdateData(data, offset + 74);
        Sts_eNotifyOnFail.UpdateData(data, offset + 75);
        Sts_eNotifyOffFail.UpdateData(data, offset + 76);
        Sts_eNotifyIntlkTrip.UpdateData(data, offset + 77);
        Sts_UnackAlmCount.UpdateData(data, offset + 78);
        Sts_eSrc.UpdateData(data, offset + 82);
        Sts_bSrc.UpdateData(data, offset + 84);
        Sts_Available.UpdateData((data[offset + 86] & (1 << 6)) != 0);
        Sts_IntlkAvailable.UpdateData((data[offset + 86] & (1 << 7)) != 0);
        Sts_Bypass.UpdateData((data[offset + 87] & (1 << 0)) != 0);
        Sts_BypActive.UpdateData((data[offset + 91] & (1 << 1)) != 0);
        Sts_MaintByp.UpdateData((data[offset + 91] & (1 << 2)) != 0);
        Sts_NotRdy.UpdateData((data[offset + 91] & (1 << 3)) != 0);
        Sts_NrdyOoS.UpdateData((data[offset + 91] & (1 << 4)) != 0);
        Sts_NrdyCfgErr.UpdateData((data[offset + 91] & (1 << 5)) != 0);
        Sts_NrdyIntlk.UpdateData((data[offset + 91] & (1 << 6)) != 0);
        Sts_NrdyPerm.UpdateData((data[offset + 91] & (1 << 7)) != 0);
        Sts_NrdyPrioOff.UpdateData((data[offset + 92] & (1 << 0)) != 0);
        Sts_NrdyFail.UpdateData((data[offset + 92] & (1 << 1)) != 0);
        Sts_NrdyIOFault.UpdateData((data[offset + 92] & (1 << 2)) != 0);
        Sts_Err.UpdateData((data[offset + 92] & (1 << 3)) != 0);
        Sts_ErrOnDly.UpdateData((data[offset + 92] & (1 << 4)) != 0);
        Sts_ErrOffDly.UpdateData((data[offset + 92] & (1 << 5)) != 0);
        Sts_ErrOnPulseTime.UpdateData((data[offset + 92] & (1 << 6)) != 0);
        Sts_ErrOffPulseTime.UpdateData((data[offset + 92] & (1 << 7)) != 0);
        Sts_ErrOnFailTime.UpdateData((data[offset + 93] & (1 << 0)) != 0);
        Sts_ErrOffFailTime.UpdateData((data[offset + 93] & (1 << 1)) != 0);
        Sts_ErrStartHornTime.UpdateData((data[offset + 93] & (1 << 2)) != 0);
        Sts_ErrVirtualFdbkTime.UpdateData((data[offset + 93] & (1 << 3)) != 0);
        Sts_ErrAlm.UpdateData((data[offset + 93] & (1 << 4)) != 0);
        Sts_Hand.UpdateData((data[offset + 93] & (1 << 5)) != 0);
        Sts_OoS.UpdateData((data[offset + 93] & (1 << 6)) != 0);
        Sts_Maint.UpdateData((data[offset + 93] & (1 << 7)) != 0);
        Sts_Ovrd.UpdateData((data[offset + 94] & (1 << 0)) != 0);
        Sts_Ext.UpdateData((data[offset + 94] & (1 << 1)) != 0);
        Sts_Prog.UpdateData((data[offset + 94] & (1 << 2)) != 0);
        Sts_ProgLocked.UpdateData((data[offset + 94] & (1 << 3)) != 0);
        Sts_Oper.UpdateData((data[offset + 94] & (1 << 4)) != 0);
        Sts_OperLocked.UpdateData((data[offset + 94] & (1 << 5)) != 0);
        Sts_ProgOperSel.UpdateData((data[offset + 94] & (1 << 6)) != 0);
        Sts_ProgOperLock.UpdateData((data[offset + 94] & (1 << 7)) != 0);
        Sts_Normal.UpdateData((data[offset + 95] & (1 << 0)) != 0);
        Sts_ExtReqInh.UpdateData((data[offset + 95] & (1 << 1)) != 0);
        Sts_ProgReqInh.UpdateData((data[offset + 95] & (1 << 2)) != 0);
        Sts_MAcqRcvd.UpdateData((data[offset + 95] & (1 << 3)) != 0);
        Sts_Alm.UpdateData((data[offset + 95] & (1 << 4)) != 0);
        Sts_AlmInh.UpdateData((data[offset + 95] & (1 << 5)) != 0);
        Sts_IOFault.UpdateData((data[offset + 95] & (1 << 6)) != 0);
        Sts_OnFail.UpdateData((data[offset + 95] & (1 << 7)) != 0);
        Sts_OffFail.UpdateData((data[offset + 96] & (1 << 0)) != 0);
        Sts_IntlkTrip.UpdateData((data[offset + 96] & (1 << 1)) != 0);
        Sts_RdyAck.UpdateData((data[offset + 96] & (1 << 2)) != 0);
        Sts_RdyReset.UpdateData((data[offset + 96] & (1 << 3)) != 0);
        XRdy_Acq.UpdateData((data[offset + 96] & (1 << 4)) != 0);
        XRdy_Rel.UpdateData((data[offset + 96] & (1 << 5)) != 0);
        XRdy_On.UpdateData((data[offset + 96] & (1 << 6)) != 0);
        XRdy_Off.UpdateData((data[offset + 96] & (1 << 7)) != 0);
        XRdy_OnPulse.UpdateData((data[offset + 97] & (1 << 0)) != 0);
        XRdy_OffPulse.UpdateData((data[offset + 97] & (1 << 1)) != 0);
        XRdy_ContPulse.UpdateData((data[offset + 101] & (1 << 2)) != 0);
        XRdy_Reset.UpdateData((data[offset + 101] & (1 << 3)) != 0);
        XRdy_ResetAckAll.UpdateData((data[offset + 101] & (1 << 4)) != 0);
        Val_Owner.UpdateData(data, offset + 101);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_InitializeReq</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Inp_InitializeReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_OwnerCmd</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public DINT Inp_OwnerCmd
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_OnFdbkData</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Inp_OnFdbkData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_OffFdbkData</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Inp_OffFdbkData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_IOFault</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Inp_IOFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PermOK</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Inp_PermOK
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_NBPermOK</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Inp_NBPermOK
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_IntlkOK</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Inp_IntlkOK
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_NBIntlkOK</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Inp_NBIntlkOK
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_IntlkAvailable</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Inp_IntlkAvailable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_IntlkTripInh</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Inp_IntlkTripInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_RdyReset</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Inp_RdyReset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Hand</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Inp_Hand
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Ovrd</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Inp_Ovrd
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_OvrdCmd</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public SINT Inp_OvrdCmd
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_ExtInh</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Inp_ExtInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_HornInh</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Inp_HornInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Reset</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Inp_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HornOnChange</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_HornOnChange
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ExtOffPrio</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_ExtOffPrio
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_XCmdResets</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_XCmdResets
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_AllowDisable</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_AllowDisable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_AllowShelve</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_AllowShelve
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasPulse</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_HasPulse
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CompletePulse</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_CompletePulse
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_FdbkFail</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_FdbkFail
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasOnFdbk</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_HasOnFdbk
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasOffFdbk</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_HasOffFdbk
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_UseOnFdbk</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_UseOnFdbk
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_UseOffFdbk</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_UseOffFdbk
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OperOffPrio</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_OperOffPrio
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OCmdResets</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_OCmdResets
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ShedOnIOFault</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_ShedOnIOFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ShedOnFail</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_ShedOnFail
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasPermObj</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_HasPermObj
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasIntlkObj</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_HasIntlkObj
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasMoreObj</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_HasMoreObj
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasOper</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_HasOper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasOperLocked</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_HasOperLocked
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasProg</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_HasProg
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasProgLocked</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_HasProgLocked
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasExt</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_HasExt
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasMaint</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_HasMaint
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasMaintOoS</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_HasMaintOoS
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OvrdOverLock</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_OvrdOverLock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ExtOverLock</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_ExtOverLock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ProgPwrUp</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_ProgPwrUp
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ProgNormal</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_ProgNormal
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PCmdPriority</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_PCmdPriority
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PCmdProgAsLevel</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_PCmdProgAsLevel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PCmdLockAsLevel</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_PCmdLockAsLevel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ExtAcqAsLevel</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_ExtAcqAsLevel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OvrdPermIntlk</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Cfg_OvrdPermIntlk
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OnDly</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public REAL Cfg_OnDly
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OffDly</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public REAL Cfg_OffDly
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OnPulseTime</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public REAL Cfg_OnPulseTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OffPulseTime</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public REAL Cfg_OffPulseTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OnFailTime</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public REAL Cfg_OnFailTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OffFailTime</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public REAL Cfg_OffFailTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_StartHornTime</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public REAL Cfg_StartHornTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_VirtualFdbkTime</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public REAL Cfg_VirtualFdbkTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CnfrmReqd</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public SINT Cfg_CnfrmReqd
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PSet_Owner</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public DINT PSet_Owner
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Virtual</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL PCmd_Virtual
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Physical</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL PCmd_Physical
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_On</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL PCmd_On
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Off</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL PCmd_Off
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_OnPulse</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL PCmd_OnPulse
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_OffPulse</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL PCmd_OffPulse
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_ContPulse</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL PCmd_ContPulse
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Oper</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL PCmd_Oper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Prog</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL PCmd_Prog
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Lock</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL PCmd_Lock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Unlock</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL PCmd_Unlock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Normal</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL PCmd_Normal
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Reset</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL PCmd_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_On</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL XCmd_On
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Off</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL XCmd_Off
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_OnPulse</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL XCmd_OnPulse
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_OffPulse</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL XCmd_OffPulse
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_ContPulse</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL XCmd_ContPulse
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Acq</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL XCmd_Acq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Rel</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL XCmd_Rel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Reset</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL XCmd_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_ResetAckAll</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL XCmd_ResetAckAll
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_CVData</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Out_CVData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_HornData</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Out_HornData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_Reset</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Out_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_OwnerSts</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public DINT Out_OwnerSts
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Initialized</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_Initialized
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Out</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_Out
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Pulsing</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_Pulsing
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_FdbkOff</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_FdbkOff
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_FdbkOn</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_FdbkOn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_FdbkFail</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_FdbkFail
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Horn</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_Horn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Virtual</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_Virtual
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SrcQ_IO</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public SINT SrcQ_IO
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SrcQ</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public SINT SrcQ
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eCmd</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public SINT Sts_eCmd
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eFdbk</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public SINT Sts_eFdbk
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eSts</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public SINT Sts_eSts
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eFault</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public SINT Sts_eFault
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eState</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public SINT Sts_eState
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotify</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public SINT Sts_eNotify
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyAll</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public SINT Sts_eNotifyAll
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyIOFault</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public SINT Sts_eNotifyIOFault
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyOnFail</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public SINT Sts_eNotifyOnFail
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyOffFail</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public SINT Sts_eNotifyOffFail
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyIntlkTrip</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public SINT Sts_eNotifyIntlkTrip
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_UnackAlmCount</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public DINT Sts_UnackAlmCount
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eSrc</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public INT Sts_eSrc
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_bSrc</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public INT Sts_bSrc
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Available</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_Available
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_IntlkAvailable</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_IntlkAvailable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Bypass</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_Bypass
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_BypActive</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_BypActive
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_MaintByp</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_MaintByp
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NotRdy</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_NotRdy
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyOoS</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_NrdyOoS
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyCfgErr</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_NrdyCfgErr
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyIntlk</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_NrdyIntlk
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyPerm</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_NrdyPerm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyPrioOff</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_NrdyPrioOff
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyFail</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_NrdyFail
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NrdyIOFault</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_NrdyIOFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Err</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_Err
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrOnDly</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrOnDly
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrOffDly</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrOffDly
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrOnPulseTime</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrOnPulseTime
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrOffPulseTime</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrOffPulseTime
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrOnFailTime</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrOnFailTime
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrOffFailTime</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrOffFailTime
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrStartHornTime</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrStartHornTime
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrVirtualFdbkTime</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrVirtualFdbkTime
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrAlm</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrAlm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Hand</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_Hand
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_OoS</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_OoS
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Maint</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_Maint
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Ovrd</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_Ovrd
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Ext</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_Ext
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Prog</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_Prog
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ProgLocked</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_ProgLocked
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Oper</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_Oper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_OperLocked</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_OperLocked
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ProgOperSel</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_ProgOperSel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ProgOperLock</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_ProgOperLock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Normal</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_Normal
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ExtReqInh</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_ExtReqInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ProgReqInh</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_ProgReqInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_MAcqRcvd</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_MAcqRcvd
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Alm</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_Alm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_AlmInh</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_AlmInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_IOFault</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_IOFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_OnFail</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_OnFail
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_OffFail</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_OffFail
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_IntlkTrip</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_IntlkTrip
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_RdyAck</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_RdyAck
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_RdyReset</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL Sts_RdyReset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Acq</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL XRdy_Acq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Rel</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL XRdy_Rel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_On</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL XRdy_On
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Off</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL XRdy_Off
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_OnPulse</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL XRdy_OnPulse
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_OffPulse</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL XRdy_OffPulse
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_ContPulse</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL XRdy_ContPulse
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Reset</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL XRdy_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_ResetAckAll</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public BOOL XRdy_ResetAckAll
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_Owner</c> member of the <see cref="P_DISCRETE_OUTPUT"/> data type.
    /// </summary>
    public DINT Val_Owner
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }
}