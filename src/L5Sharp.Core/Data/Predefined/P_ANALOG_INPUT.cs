using System.Xml.Linq;
// Auto-generated file
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>P_ANALOG_INPUT</c> data type structure.
/// </summary>
[LogixData("P_ANALOG_INPUT")]
public sealed partial class P_ANALOG_INPUT : StructureData
{
    /// <summary>
    /// Creates a new <see cref="P_ANALOG_INPUT"/> instance initialized with default values.
    /// </summary>
    public P_ANALOG_INPUT() : base("P_ANALOG_INPUT")
    {
        EnableIn = new BOOL();
        EnableOut = new BOOL();
        Inp_InitializeReq = new BOOL();
        Inp_PVData = new REAL();
        Inp_SmartDvcSts = new DINT();
        Inp_SmartDvcDiagAvailable = new BOOL();
        Inp_ModFault = new BOOL();
        Inp_ChanFault = new BOOL();
        Inp_OutOfSpec = new BOOL();
        Inp_FuncCheck = new BOOL();
        Inp_MaintReqd = new BOOL();
        Inp_PVUncertain = new BOOL();
        Inp_PVNotify = new SINT();
        Inp_HiHiGate = new BOOL();
        Inp_HiGate = new BOOL();
        Inp_LoGate = new BOOL();
        Inp_LoLoGate = new BOOL();
        Inp_HiRoCGate = new BOOL();
        Inp_HiDevGate = new BOOL();
        Inp_LoDevGate = new BOOL();
        Inp_OoRGate = new BOOL();
        Inp_Reset = new BOOL();
        Cfg_AllowDisable = new BOOL();
        Cfg_AllowShelve = new BOOL();
        Cfg_ClampSB = new REAL();
        Cfg_InpRawMin = new REAL();
        Cfg_InpRawMax = new REAL();
        Cfg_PVEUMin = new REAL();
        Cfg_PVEUMax = new REAL();
        Cfg_Ref = new REAL();
        Cfg_FiltWLag = new REAL();
        Cfg_FiltOrder = new DINT();
        Cfg_RateTime = new REAL();
        Cfg_PVHiLim = new REAL();
        Cfg_PVLoLim = new REAL();
        Cfg_PVReplaceVal = new REAL();
        Cfg_HiHiLim = new REAL();
        Cfg_HiHiDB = new REAL();
        Cfg_HiHiGateDly = new REAL();
        Cfg_HiLim = new REAL();
        Cfg_HiDB = new REAL();
        Cfg_HiGateDly = new REAL();
        Cfg_LoLim = new REAL();
        Cfg_LoDB = new REAL();
        Cfg_LoGateDly = new REAL();
        Cfg_LoLoLim = new REAL();
        Cfg_LoLoDB = new REAL();
        Cfg_LoLoGateDly = new REAL();
        Cfg_HiRoCLim = new REAL();
        Cfg_HiRoCDB = new REAL();
        Cfg_HiRoCGateDly = new REAL();
        Cfg_HiDevLim = new REAL();
        Cfg_HiDevDB = new REAL();
        Cfg_HiDevGateDly = new REAL();
        Cfg_LoDevLim = new REAL();
        Cfg_LoDevDB = new REAL();
        Cfg_LoDevGateDly = new REAL();
        Cfg_OoRHiLim = new REAL();
        Cfg_OoRLoLim = new REAL();
        Cfg_OoRDB = new REAL();
        Cfg_OoRGateDly = new REAL();
        Cfg_OoROnDly = new REAL();
        Cfg_OoROffDly = new REAL();
        Cfg_StuckTime = new REAL();
        Cfg_InpOoRAction = new SINT();
        Cfg_InpOoRQual = new SINT();
        Cfg_InpStuckAction = new SINT();
        Cfg_InpStuckQual = new SINT();
        Cfg_InpNaNAction = new SINT();
        Cfg_InpNaNQual = new SINT();
        Cfg_ModFaultAction = new SINT();
        Cfg_ModFaultQual = new SINT();
        Cfg_ChanFaultAction = new SINT();
        Cfg_ChanFaultQual = new SINT();
        Cfg_OutOfSpecAction = new SINT();
        Cfg_OutOfSpecQual = new SINT();
        Cfg_FuncCheckAction = new SINT();
        Cfg_FuncCheckQual = new SINT();
        Cfg_MaintReqdAction = new SINT();
        Cfg_MaintReqdQual = new SINT();
        Cfg_CfgErrAction = new SINT();
        Cfg_CfgErrQual = new SINT();
        Cfg_CtrlHiHiLim = new REAL();
        Cfg_CtrlHiHiDB = new REAL();
        Cfg_CtrlHiLim = new REAL();
        Cfg_CtrlHiDB = new REAL();
        Cfg_CtrlLoLim = new REAL();
        Cfg_CtrlLoDB = new REAL();
        Cfg_CtrlLoLoLim = new REAL();
        Cfg_CtrlLoLoDB = new REAL();
        Cfg_HasSmartDvc = new BOOL();
        Cfg_HasRoC = new BOOL();
        Cfg_HasDev = new BOOL();
        Cfg_HasMoreObj = new BOOL();
        Cfg_HasOutNav = new BOOL();
        Cfg_HasPVNav = new BOOL();
        Cfg_HasHistTrend = new SINT();
        Cfg_FailOnUncertain = new BOOL();
        Cfg_NoSubstPV = new BOOL();
        Cfg_SetTrack = new BOOL();
        Cfg_SclngTyp = new SINT();
        Cfg_PVDecPlcs = new SINT();
        Cfg_CnfrmReqd = new SINT();
        PSet_Owner = new DINT();
        Set_VirtualPV = new REAL();
        PCmd_ClearCapt = new BOOL();
        PCmd_Reset = new BOOL();
        PCmd_Physical = new BOOL();
        PCmd_Virtual = new BOOL();
        XCmd_ClearCapt = new BOOL();
        XCmd_Reset = new BOOL();
        XCmd_ResetAckAll = new BOOL();
        Val = new REAL();
        Val_InpPV = new REAL();
        Val_RoC = new REAL();
        Val_Dev = new REAL();
        Val_PVMinCapt = new REAL();
        Val_PVMaxCapt = new REAL();
        Val_PVEUMin = new REAL();
        Val_PVEUMax = new REAL();
        Out_Reset = new BOOL();
        Out_SmartDvcSts = new DINT();
        Sts_Initialized = new BOOL();
        Sts_SmartDvcDiagAvailable = new BOOL();
        Sts_PVGood = new BOOL();
        Sts_PVUncertain = new BOOL();
        Sts_PVBad = new BOOL();
        Sts_InpStuck = new BOOL();
        Sts_InpNaN = new BOOL();
        Sts_OutOfSpec = new BOOL();
        Sts_FuncCheck = new BOOL();
        Sts_MaintReqd = new BOOL();
        Sts_UseInp = new BOOL();
        Sts_HoldLast = new BOOL();
        Sts_Clamped = new BOOL();
        Sts_Replaced = new BOOL();
        Sts_SubstPV = new BOOL();
        Sts_InpPV = new BOOL();
        Sts_Virtual = new BOOL();
        SrcQ_IO = new SINT();
        SrcQ = new SINT();
        Sts_eSts = new SINT();
        Sts_eFault = new INT();
        Sts_eNotify = new SINT();
        Sts_eNotifyAll = new SINT();
        Sts_eNotifyHiHi = new SINT();
        Sts_eNotifyHi = new SINT();
        Sts_eNotifyLo = new SINT();
        Sts_eNotifyLoLo = new SINT();
        Sts_eNotifyHiRoC = new SINT();
        Sts_eNotifyHiDev = new SINT();
        Sts_eNotifyLoDev = new SINT();
        Sts_eNotifyFail = new SINT();
        Sts_UnackAlmCount = new DINT();
        Sts_MaintByp = new BOOL();
        Sts_Err = new BOOL();
        Sts_ErrRaw = new BOOL();
        Sts_ErrEU = new BOOL();
        Sts_ErrFiltWLag = new BOOL();
        Sts_ErrFiltOrder = new BOOL();
        Sts_ErrRateTime = new BOOL();
        Sts_ErrHiHiDB = new BOOL();
        Sts_ErrHiHiGateDly = new BOOL();
        Sts_ErrHiDB = new BOOL();
        Sts_ErrHiGateDly = new BOOL();
        Sts_ErrLoDB = new BOOL();
        Sts_ErrLoGateDly = new BOOL();
        Sts_ErrLoLoDB = new BOOL();
        Sts_ErrLoLoGateDly = new BOOL();
        Sts_ErrHiRoCDB = new BOOL();
        Sts_ErrHiRoCGateDly = new BOOL();
        Sts_ErrHiDevDB = new BOOL();
        Sts_ErrHiDevGateDly = new BOOL();
        Sts_ErrLoDevDB = new BOOL();
        Sts_ErrLoDevGateDly = new BOOL();
        Sts_ErrOoRDB = new BOOL();
        Sts_ErrOoRGateDly = new BOOL();
        Sts_ErrOoROnDly = new BOOL();
        Sts_ErrOoROffDly = new BOOL();
        Sts_ErrStuckTime = new BOOL();
        Sts_ErrAlm = new BOOL();
        Sts_ErrDB = new BOOL();
        Sts_ErrCtrlDB = new BOOL();
        Sts_ErrCtrlHiHiDB = new BOOL();
        Sts_ErrCtrlHiDB = new BOOL();
        Sts_ErrCtrlLoDB = new BOOL();
        Sts_ErrCtrlLoLoDB = new BOOL();
        Sts_Alm = new BOOL();
        Sts_AlmInh = new BOOL();
        Sts_IOFault = new BOOL();
        Sts_HiHiCmp = new BOOL();
        Sts_HiHiGate = new BOOL();
        Sts_HiHi = new BOOL();
        Sts_HiCmp = new BOOL();
        Sts_HiGate = new BOOL();
        Sts_Hi = new BOOL();
        Sts_LoCmp = new BOOL();
        Sts_LoGate = new BOOL();
        Sts_Lo = new BOOL();
        Sts_LoLoCmp = new BOOL();
        Sts_LoLoGate = new BOOL();
        Sts_LoLo = new BOOL();
        Sts_CtrlHiHi = new BOOL();
        Sts_CtrlHi = new BOOL();
        Sts_CtrlLo = new BOOL();
        Sts_CtrlLoLo = new BOOL();
        Sts_HiRoCCmp = new BOOL();
        Sts_HiRoCGate = new BOOL();
        Sts_HiRoC = new BOOL();
        Sts_HiDevCmp = new BOOL();
        Sts_HiDevGate = new BOOL();
        Sts_HiDev = new BOOL();
        Sts_LoDevCmp = new BOOL();
        Sts_LoDevGate = new BOOL();
        Sts_LoDev = new BOOL();
        Sts_OoRHiCmp = new BOOL();
        Sts_OoRLoCmp = new BOOL();
        Sts_OoRCmp = new BOOL();
        Sts_OoRGate = new BOOL();
        Sts_OoR = new BOOL();
        Sts_Fail = new BOOL();
        Sts_RdyAck = new BOOL();
        Sts_RdyReset = new BOOL();
        XRdy_ClearCapt = new BOOL();
        XRdy_Reset = new BOOL();
        XRdy_ResetAckAll = new BOOL();
        Val_Owner = new DINT();
    }

    /// <summary>
    /// Creates a new <see cref="P_ANALOG_INPUT"/> instance initialized with the provided element.
    /// </summary>
    public P_ANALOG_INPUT(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_InitializeReq</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Inp_InitializeReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVData</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public REAL Inp_PVData
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_SmartDvcSts</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public DINT Inp_SmartDvcSts
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_SmartDvcDiagAvailable</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Inp_SmartDvcDiagAvailable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_ModFault</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Inp_ModFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_ChanFault</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Inp_ChanFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_OutOfSpec</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Inp_OutOfSpec
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_FuncCheck</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Inp_FuncCheck
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_MaintReqd</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Inp_MaintReqd
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVUncertain</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Inp_PVUncertain
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVNotify</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public SINT Inp_PVNotify
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_HiHiGate</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Inp_HiHiGate
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_HiGate</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Inp_HiGate
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_LoGate</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Inp_LoGate
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_LoLoGate</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Inp_LoLoGate
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_HiRoCGate</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Inp_HiRoCGate
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_HiDevGate</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Inp_HiDevGate
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_LoDevGate</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Inp_LoDevGate
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_OoRGate</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Inp_OoRGate
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Reset</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Inp_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_AllowDisable</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Cfg_AllowDisable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_AllowShelve</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Cfg_AllowShelve
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ClampSB</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public REAL Cfg_ClampSB
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_InpRawMin</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public REAL Cfg_InpRawMin
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_InpRawMax</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public REAL Cfg_InpRawMax
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PVEUMin</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public REAL Cfg_PVEUMin
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PVEUMax</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public REAL Cfg_PVEUMax
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_Ref</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public REAL Cfg_Ref
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_FiltWLag</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public REAL Cfg_FiltWLag
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_FiltOrder</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public DINT Cfg_FiltOrder
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_RateTime</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public REAL Cfg_RateTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PVHiLim</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public REAL Cfg_PVHiLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PVLoLim</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public REAL Cfg_PVLoLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PVReplaceVal</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public REAL Cfg_PVReplaceVal
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HiHiLim</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public REAL Cfg_HiHiLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HiHiDB</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public REAL Cfg_HiHiDB
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HiHiGateDly</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public REAL Cfg_HiHiGateDly
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HiLim</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public REAL Cfg_HiLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HiDB</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public REAL Cfg_HiDB
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HiGateDly</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public REAL Cfg_HiGateDly
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_LoLim</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public REAL Cfg_LoLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_LoDB</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public REAL Cfg_LoDB
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_LoGateDly</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public REAL Cfg_LoGateDly
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_LoLoLim</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public REAL Cfg_LoLoLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_LoLoDB</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public REAL Cfg_LoLoDB
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_LoLoGateDly</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public REAL Cfg_LoLoGateDly
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HiRoCLim</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public REAL Cfg_HiRoCLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HiRoCDB</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public REAL Cfg_HiRoCDB
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HiRoCGateDly</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public REAL Cfg_HiRoCGateDly
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HiDevLim</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public REAL Cfg_HiDevLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HiDevDB</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public REAL Cfg_HiDevDB
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HiDevGateDly</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public REAL Cfg_HiDevGateDly
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_LoDevLim</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public REAL Cfg_LoDevLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_LoDevDB</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public REAL Cfg_LoDevDB
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_LoDevGateDly</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public REAL Cfg_LoDevGateDly
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OoRHiLim</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public REAL Cfg_OoRHiLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OoRLoLim</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public REAL Cfg_OoRLoLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OoRDB</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public REAL Cfg_OoRDB
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OoRGateDly</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public REAL Cfg_OoRGateDly
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OoROnDly</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public REAL Cfg_OoROnDly
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OoROffDly</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public REAL Cfg_OoROffDly
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_StuckTime</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public REAL Cfg_StuckTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_InpOoRAction</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public SINT Cfg_InpOoRAction
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_InpOoRQual</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public SINT Cfg_InpOoRQual
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_InpStuckAction</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public SINT Cfg_InpStuckAction
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_InpStuckQual</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public SINT Cfg_InpStuckQual
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_InpNaNAction</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public SINT Cfg_InpNaNAction
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_InpNaNQual</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public SINT Cfg_InpNaNQual
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ModFaultAction</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public SINT Cfg_ModFaultAction
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ModFaultQual</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public SINT Cfg_ModFaultQual
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ChanFaultAction</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public SINT Cfg_ChanFaultAction
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ChanFaultQual</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public SINT Cfg_ChanFaultQual
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OutOfSpecAction</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public SINT Cfg_OutOfSpecAction
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OutOfSpecQual</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public SINT Cfg_OutOfSpecQual
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_FuncCheckAction</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public SINT Cfg_FuncCheckAction
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_FuncCheckQual</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public SINT Cfg_FuncCheckQual
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_MaintReqdAction</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public SINT Cfg_MaintReqdAction
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_MaintReqdQual</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public SINT Cfg_MaintReqdQual
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CfgErrAction</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public SINT Cfg_CfgErrAction
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CfgErrQual</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public SINT Cfg_CfgErrQual
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CtrlHiHiLim</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public REAL Cfg_CtrlHiHiLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CtrlHiHiDB</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public REAL Cfg_CtrlHiHiDB
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CtrlHiLim</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public REAL Cfg_CtrlHiLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CtrlHiDB</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public REAL Cfg_CtrlHiDB
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CtrlLoLim</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public REAL Cfg_CtrlLoLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CtrlLoDB</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public REAL Cfg_CtrlLoDB
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CtrlLoLoLim</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public REAL Cfg_CtrlLoLoLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CtrlLoLoDB</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public REAL Cfg_CtrlLoLoDB
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasSmartDvc</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Cfg_HasSmartDvc
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasRoC</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Cfg_HasRoC
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasDev</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Cfg_HasDev
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasMoreObj</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Cfg_HasMoreObj
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasOutNav</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Cfg_HasOutNav
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasPVNav</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Cfg_HasPVNav
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasHistTrend</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public SINT Cfg_HasHistTrend
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_FailOnUncertain</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Cfg_FailOnUncertain
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_NoSubstPV</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Cfg_NoSubstPV
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_SetTrack</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Cfg_SetTrack
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_SclngTyp</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public SINT Cfg_SclngTyp
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PVDecPlcs</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public SINT Cfg_PVDecPlcs
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CnfrmReqd</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public SINT Cfg_CnfrmReqd
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PSet_Owner</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public DINT PSet_Owner
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Set_VirtualPV</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public REAL Set_VirtualPV
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_ClearCapt</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL PCmd_ClearCapt
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Reset</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL PCmd_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Physical</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL PCmd_Physical
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Virtual</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL PCmd_Virtual
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_ClearCapt</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL XCmd_ClearCapt
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Reset</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL XCmd_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_ResetAckAll</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL XCmd_ResetAckAll
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public REAL Val
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_InpPV</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public REAL Val_InpPV
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_RoC</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public REAL Val_RoC
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_Dev</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public REAL Val_Dev
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_PVMinCapt</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public REAL Val_PVMinCapt
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_PVMaxCapt</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public REAL Val_PVMaxCapt
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_PVEUMin</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public REAL Val_PVEUMin
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_PVEUMax</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public REAL Val_PVEUMax
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_Reset</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Out_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_SmartDvcSts</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public DINT Out_SmartDvcSts
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Initialized</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_Initialized
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_SmartDvcDiagAvailable</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_SmartDvcDiagAvailable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_PVGood</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_PVGood
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_PVUncertain</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_PVUncertain
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_PVBad</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_PVBad
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_InpStuck</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_InpStuck
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_InpNaN</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_InpNaN
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_OutOfSpec</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_OutOfSpec
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_FuncCheck</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_FuncCheck
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_MaintReqd</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_MaintReqd
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_UseInp</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_UseInp
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_HoldLast</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_HoldLast
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Clamped</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_Clamped
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Replaced</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_Replaced
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_SubstPV</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_SubstPV
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_InpPV</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_InpPV
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Virtual</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_Virtual
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SrcQ_IO</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public SINT SrcQ_IO
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SrcQ</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public SINT SrcQ
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eSts</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public SINT Sts_eSts
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eFault</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public INT Sts_eFault
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotify</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public SINT Sts_eNotify
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyAll</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public SINT Sts_eNotifyAll
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyHiHi</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public SINT Sts_eNotifyHiHi
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyHi</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public SINT Sts_eNotifyHi
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyLo</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public SINT Sts_eNotifyLo
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyLoLo</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public SINT Sts_eNotifyLoLo
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyHiRoC</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public SINT Sts_eNotifyHiRoC
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyHiDev</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public SINT Sts_eNotifyHiDev
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyLoDev</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public SINT Sts_eNotifyLoDev
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyFail</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public SINT Sts_eNotifyFail
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_UnackAlmCount</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public DINT Sts_UnackAlmCount
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_MaintByp</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_MaintByp
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Err</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_Err
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrRaw</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrRaw
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrEU</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrEU
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrFiltWLag</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrFiltWLag
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrFiltOrder</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrFiltOrder
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrRateTime</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrRateTime
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrHiHiDB</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrHiHiDB
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrHiHiGateDly</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrHiHiGateDly
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrHiDB</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrHiDB
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrHiGateDly</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrHiGateDly
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrLoDB</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrLoDB
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrLoGateDly</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrLoGateDly
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrLoLoDB</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrLoLoDB
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrLoLoGateDly</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrLoLoGateDly
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrHiRoCDB</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrHiRoCDB
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrHiRoCGateDly</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrHiRoCGateDly
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrHiDevDB</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrHiDevDB
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrHiDevGateDly</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrHiDevGateDly
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrLoDevDB</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrLoDevDB
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrLoDevGateDly</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrLoDevGateDly
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrOoRDB</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrOoRDB
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrOoRGateDly</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrOoRGateDly
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrOoROnDly</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrOoROnDly
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrOoROffDly</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrOoROffDly
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrStuckTime</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrStuckTime
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrAlm</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrAlm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrDB</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrDB
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrCtrlDB</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrCtrlDB
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrCtrlHiHiDB</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrCtrlHiHiDB
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrCtrlHiDB</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrCtrlHiDB
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrCtrlLoDB</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrCtrlLoDB
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrCtrlLoLoDB</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrCtrlLoLoDB
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Alm</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_Alm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_AlmInh</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_AlmInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_IOFault</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_IOFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_HiHiCmp</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_HiHiCmp
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_HiHiGate</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_HiHiGate
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_HiHi</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_HiHi
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_HiCmp</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_HiCmp
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_HiGate</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_HiGate
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Hi</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_Hi
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_LoCmp</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_LoCmp
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_LoGate</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_LoGate
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Lo</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_Lo
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_LoLoCmp</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_LoLoCmp
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_LoLoGate</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_LoLoGate
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_LoLo</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_LoLo
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_CtrlHiHi</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_CtrlHiHi
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_CtrlHi</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_CtrlHi
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_CtrlLo</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_CtrlLo
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_CtrlLoLo</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_CtrlLoLo
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_HiRoCCmp</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_HiRoCCmp
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_HiRoCGate</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_HiRoCGate
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_HiRoC</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_HiRoC
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_HiDevCmp</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_HiDevCmp
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_HiDevGate</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_HiDevGate
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_HiDev</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_HiDev
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_LoDevCmp</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_LoDevCmp
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_LoDevGate</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_LoDevGate
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_LoDev</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_LoDev
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_OoRHiCmp</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_OoRHiCmp
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_OoRLoCmp</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_OoRLoCmp
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_OoRCmp</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_OoRCmp
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_OoRGate</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_OoRGate
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_OoR</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_OoR
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Fail</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_Fail
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_RdyAck</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_RdyAck
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_RdyReset</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_RdyReset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_ClearCapt</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL XRdy_ClearCapt
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Reset</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL XRdy_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_ResetAckAll</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public BOOL XRdy_ResetAckAll
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_Owner</c> member of the <see cref="P_ANALOG_INPUT"/> data type.
    /// </summary>
    public DINT Val_Owner
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

}
