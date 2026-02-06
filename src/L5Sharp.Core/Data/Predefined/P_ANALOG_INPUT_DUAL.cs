using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>P_ANALOG_INPUT_DUAL</c> data type structure.
/// </summary>
[LogixData("P_ANALOG_INPUT_DUAL")]
public sealed partial class P_ANALOG_INPUT_DUAL : StructureData
{
    /// <summary>
    /// Creates a new <see cref="P_ANALOG_INPUT_DUAL"/> instance initialized with default values.
    /// </summary>
    public P_ANALOG_INPUT_DUAL() : base("P_ANALOG_INPUT_DUAL")
    {
        EnableIn = new BOOL();
        EnableOut = new BOOL();
        Inp_InitializeReq = new BOOL();
        Inp_PVAData = new REAL();
        Inp_PVASrcQ = new SINT();
        Inp_PVANotify = new SINT();
        Inp_SmartDvcASts = new DINT();
        Inp_PVBData = new REAL();
        Inp_PVBSrcQ = new SINT();
        Inp_PVBNotify = new SINT();
        Inp_SmartDvcBSts = new DINT();
        Inp_PVABad = new BOOL();
        Inp_PVAUncertain = new BOOL();
        Inp_SmartDvcADiagAvailable = new BOOL();
        Inp_PVBBad = new BOOL();
        Inp_PVBUncertain = new BOOL();
        Inp_SmartDvcBDiagAvailable = new BOOL();
        Inp_DiffGate = new BOOL();
        Cfg_AllowDisable = new BOOL();
        Cfg_AllowShelve = new BOOL();
        Cfg_UseInpSrcQPVA = new BOOL();
        Cfg_UseInpSrcQPVB = new BOOL();
        Cfg_HasPVNav = new BOOL();
        Cfg_HasMoreObj = new BOOL();
        Cfg_HasNav = new SINT();
        Cfg_PVEUMin = new REAL();
        Cfg_PVEUMax = new REAL();
        Cfg_DiffLim = new REAL();
        Cfg_DiffDB = new REAL();
        Cfg_DiffGateDly = new REAL();
        Cfg_OoRHiLim = new REAL();
        Cfg_OoRLoLim = new REAL();
        Cfg_OoRDB = new REAL();
        Cfg_AllowOper = new BOOL();
        Cfg_AllowProg = new BOOL();
        Cfg_AllowExt = new BOOL();
        Cfg_PVDecPlcs = new SINT();
        Cfg_CnfrmReqd = new SINT();
        PSet_Owner = new DINT();
        PCmd_SelA = new BOOL();
        PCmd_SelB = new BOOL();
        PCmd_SelAvg = new BOOL();
        PCmd_SelMin = new BOOL();
        PCmd_SelMax = new BOOL();
        PCmd_Reset = new BOOL();
        XCmd_SelA = new BOOL();
        XCmd_SelB = new BOOL();
        XCmd_SelAvg = new BOOL();
        XCmd_SelMin = new BOOL();
        XCmd_SelMax = new BOOL();
        XCmd_Reset = new BOOL();
        XCmd_ResetAckAll = new BOOL();
        Val = new REAL();
        Val_PVA = new REAL();
        Val_PVB = new REAL();
        Val_AvgPV = new REAL();
        Val_MinPV = new REAL();
        Val_MaxPV = new REAL();
        Val_InpPV = new REAL();
        Val_Diff = new REAL();
        Val_PVEUMin = new REAL();
        Val_PVEUMax = new REAL();
        Out_SmartDvcSts = new DINT();
        Sts_Initialized = new BOOL();
        Sts_SmartDvcDiagAvailable = new BOOL();
        Sts_PVASel = new BOOL();
        Sts_PVBSel = new BOOL();
        Sts_AvgSel = new BOOL();
        Sts_MinSel = new BOOL();
        Sts_MaxSel = new BOOL();
        Sts_PVBad = new BOOL();
        Sts_PVUncertain = new BOOL();
        SrcQ_IOA = new SINT();
        SrcQ_IOB = new SINT();
        SrcQ_IO = new SINT();
        SrcQ = new SINT();
        Sts_eSts = new SINT();
        Sts_eFault = new INT();
        Sts_eNotify = new SINT();
        Sts_eNotifyAll = new SINT();
        Sts_eNotifyOneGood = new SINT();
        Sts_eNotifyNoneGood = new SINT();
        Sts_eNotifyDiff = new SINT();
        Sts_eNotifyFail = new SINT();
        Sts_UnackAlmCount = new DINT();
        Sts_Err = new BOOL();
        Sts_ErrEU = new BOOL();
        Sts_ErrDiffDB = new BOOL();
        Sts_ErrDiffGateDly = new BOOL();
        Sts_ErrOoRDB = new BOOL();
        Sts_ErrAlm = new BOOL();
        Sts_Alm = new BOOL();
        Sts_AlmInh = new BOOL();
        Sts_OneGood = new BOOL();
        Sts_NoneGood = new BOOL();
        Sts_DiffCmp = new BOOL();
        Sts_DiffGate = new BOOL();
        Sts_Diff = new BOOL();
        Sts_Fail = new BOOL();
        Sts_RdyAck = new BOOL();
        Sts_RdyReset = new BOOL();
        XRdy_SelA = new BOOL();
        XRdy_SelB = new BOOL();
        XRdy_SelAvg = new BOOL();
        XRdy_SelMin = new BOOL();
        XRdy_SelMax = new BOOL();
        XRdy_Reset = new BOOL();
        XRdy_ResetAckAll = new BOOL();
        Val_Owner = new DINT();
    }
    
    /// <summary>
    /// Creates a new <see cref="P_ANALOG_INPUT_DUAL"/> instance initialized with the provided element.
    /// </summary>
    public P_ANALOG_INPUT_DUAL(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 260;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        EnableIn.UpdateData((data[offset + 5] & (1 << 0)) != 0);
        EnableOut.UpdateData((data[offset + 5] & (1 << 1)) != 0);
        Inp_InitializeReq.UpdateData((data[offset + 5] & (1 << 2)) != 0);
        Inp_PVAData.UpdateData(data, offset + 5);
        Inp_PVASrcQ.UpdateData(data, offset + 9);
        Inp_PVANotify.UpdateData(data, offset + 10);
        Inp_SmartDvcASts.UpdateData(data, offset + 11);
        Inp_PVBData.UpdateData(data, offset + 15);
        Inp_PVBSrcQ.UpdateData(data, offset + 19);
        Inp_PVBNotify.UpdateData(data, offset + 20);
        Inp_SmartDvcBSts.UpdateData(data, offset + 21);
        Inp_PVABad.UpdateData((data[offset + 25] & (1 << 3)) != 0);
        Inp_PVAUncertain.UpdateData((data[offset + 25] & (1 << 4)) != 0);
        Inp_SmartDvcADiagAvailable.UpdateData((data[offset + 25] & (1 << 5)) != 0);
        Inp_PVBBad.UpdateData((data[offset + 25] & (1 << 6)) != 0);
        Inp_PVBUncertain.UpdateData((data[offset + 25] & (1 << 7)) != 0);
        Inp_SmartDvcBDiagAvailable.UpdateData((data[offset + 26] & (1 << 0)) != 0);
        Inp_DiffGate.UpdateData((data[offset + 26] & (1 << 1)) != 0);
        Cfg_AllowDisable.UpdateData((data[offset + 26] & (1 << 2)) != 0);
        Cfg_AllowShelve.UpdateData((data[offset + 26] & (1 << 3)) != 0);
        Cfg_UseInpSrcQPVA.UpdateData((data[offset + 26] & (1 << 4)) != 0);
        Cfg_UseInpSrcQPVB.UpdateData((data[offset + 26] & (1 << 5)) != 0);
        Cfg_HasPVNav.UpdateData((data[offset + 26] & (1 << 6)) != 0);
        Cfg_HasMoreObj.UpdateData((data[offset + 26] & (1 << 7)) != 0);
        Cfg_HasNav.UpdateData(data, offset + 26);
        Cfg_PVEUMin.UpdateData(data, offset + 27);
        Cfg_PVEUMax.UpdateData(data, offset + 31);
        Cfg_DiffLim.UpdateData(data, offset + 35);
        Cfg_DiffDB.UpdateData(data, offset + 39);
        Cfg_DiffGateDly.UpdateData(data, offset + 43);
        Cfg_OoRHiLim.UpdateData(data, offset + 47);
        Cfg_OoRLoLim.UpdateData(data, offset + 51);
        Cfg_OoRDB.UpdateData(data, offset + 55);
        Cfg_AllowOper.UpdateData((data[offset + 60] & (1 << 0)) != 0);
        Cfg_AllowProg.UpdateData((data[offset + 60] & (1 << 1)) != 0);
        Cfg_AllowExt.UpdateData((data[offset + 60] & (1 << 2)) != 0);
        Cfg_PVDecPlcs.UpdateData(data, offset + 60);
        Cfg_CnfrmReqd.UpdateData(data, offset + 61);
        PSet_Owner.UpdateData(data, offset + 62);
        PCmd_SelA.UpdateData((data[offset + 66] & (1 << 3)) != 0);
        PCmd_SelB.UpdateData((data[offset + 66] & (1 << 4)) != 0);
        PCmd_SelAvg.UpdateData((data[offset + 66] & (1 << 5)) != 0);
        PCmd_SelMin.UpdateData((data[offset + 66] & (1 << 6)) != 0);
        PCmd_SelMax.UpdateData((data[offset + 66] & (1 << 7)) != 0);
        PCmd_Reset.UpdateData((data[offset + 67] & (1 << 0)) != 0);
        XCmd_SelA.UpdateData((data[offset + 67] & (1 << 1)) != 0);
        XCmd_SelB.UpdateData((data[offset + 67] & (1 << 2)) != 0);
        XCmd_SelAvg.UpdateData((data[offset + 67] & (1 << 3)) != 0);
        XCmd_SelMin.UpdateData((data[offset + 67] & (1 << 4)) != 0);
        XCmd_SelMax.UpdateData((data[offset + 67] & (1 << 5)) != 0);
        XCmd_Reset.UpdateData((data[offset + 67] & (1 << 6)) != 0);
        XCmd_ResetAckAll.UpdateData((data[offset + 67] & (1 << 7)) != 0);
        Val.UpdateData(data, offset + 67);
        Val_PVA.UpdateData(data, offset + 71);
        Val_PVB.UpdateData(data, offset + 75);
        Val_AvgPV.UpdateData(data, offset + 79);
        Val_MinPV.UpdateData(data, offset + 83);
        Val_MaxPV.UpdateData(data, offset + 87);
        Val_InpPV.UpdateData(data, offset + 91);
        Val_Diff.UpdateData(data, offset + 95);
        Val_PVEUMin.UpdateData(data, offset + 99);
        Val_PVEUMax.UpdateData(data, offset + 103);
        Out_SmartDvcSts.UpdateData(data, offset + 107);
        Sts_Initialized.UpdateData((data[offset + 116] & (1 << 0)) != 0);
        Sts_SmartDvcDiagAvailable.UpdateData((data[offset + 116] & (1 << 1)) != 0);
        Sts_PVASel.UpdateData((data[offset + 116] & (1 << 2)) != 0);
        Sts_PVBSel.UpdateData((data[offset + 116] & (1 << 3)) != 0);
        Sts_AvgSel.UpdateData((data[offset + 116] & (1 << 4)) != 0);
        Sts_MinSel.UpdateData((data[offset + 116] & (1 << 5)) != 0);
        Sts_MaxSel.UpdateData((data[offset + 116] & (1 << 6)) != 0);
        Sts_PVBad.UpdateData((data[offset + 116] & (1 << 7)) != 0);
        Sts_PVUncertain.UpdateData((data[offset + 117] & (1 << 0)) != 0);
        SrcQ_IOA.UpdateData(data, offset + 117);
        SrcQ_IOB.UpdateData(data, offset + 118);
        SrcQ_IO.UpdateData(data, offset + 119);
        SrcQ.UpdateData(data, offset + 120);
        Sts_eSts.UpdateData(data, offset + 121);
        Sts_eFault.UpdateData(data, offset + 122);
        Sts_eNotify.UpdateData(data, offset + 124);
        Sts_eNotifyAll.UpdateData(data, offset + 125);
        Sts_eNotifyOneGood.UpdateData(data, offset + 126);
        Sts_eNotifyNoneGood.UpdateData(data, offset + 127);
        Sts_eNotifyDiff.UpdateData(data, offset + 128);
        Sts_eNotifyFail.UpdateData(data, offset + 129);
        Sts_UnackAlmCount.UpdateData(data, offset + 130);
        Sts_Err.UpdateData((data[offset + 134] & (1 << 1)) != 0);
        Sts_ErrEU.UpdateData((data[offset + 134] & (1 << 2)) != 0);
        Sts_ErrDiffDB.UpdateData((data[offset + 134] & (1 << 3)) != 0);
        Sts_ErrDiffGateDly.UpdateData((data[offset + 134] & (1 << 4)) != 0);
        Sts_ErrOoRDB.UpdateData((data[offset + 134] & (1 << 5)) != 0);
        Sts_ErrAlm.UpdateData((data[offset + 134] & (1 << 6)) != 0);
        Sts_Alm.UpdateData((data[offset + 134] & (1 << 7)) != 0);
        Sts_AlmInh.UpdateData((data[offset + 135] & (1 << 0)) != 0);
        Sts_OneGood.UpdateData((data[offset + 135] & (1 << 1)) != 0);
        Sts_NoneGood.UpdateData((data[offset + 135] & (1 << 2)) != 0);
        Sts_DiffCmp.UpdateData((data[offset + 135] & (1 << 3)) != 0);
        Sts_DiffGate.UpdateData((data[offset + 135] & (1 << 4)) != 0);
        Sts_Diff.UpdateData((data[offset + 135] & (1 << 5)) != 0);
        Sts_Fail.UpdateData((data[offset + 135] & (1 << 6)) != 0);
        Sts_RdyAck.UpdateData((data[offset + 135] & (1 << 7)) != 0);
        Sts_RdyReset.UpdateData((data[offset + 136] & (1 << 0)) != 0);
        XRdy_SelA.UpdateData((data[offset + 136] & (1 << 1)) != 0);
        XRdy_SelB.UpdateData((data[offset + 136] & (1 << 2)) != 0);
        XRdy_SelAvg.UpdateData((data[offset + 136] & (1 << 3)) != 0);
        XRdy_SelMin.UpdateData((data[offset + 136] & (1 << 4)) != 0);
        XRdy_SelMax.UpdateData((data[offset + 136] & (1 << 5)) != 0);
        XRdy_Reset.UpdateData((data[offset + 136] & (1 << 6)) != 0);
        XRdy_ResetAckAll.UpdateData((data[offset + 136] & (1 << 7)) != 0);
        Val_Owner.UpdateData(data, offset + 136);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_InitializeReq</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL Inp_InitializeReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVAData</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public REAL Inp_PVAData
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVASrcQ</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public SINT Inp_PVASrcQ
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVANotify</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public SINT Inp_PVANotify
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_SmartDvcASts</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public DINT Inp_SmartDvcASts
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVBData</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public REAL Inp_PVBData
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVBSrcQ</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public SINT Inp_PVBSrcQ
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVBNotify</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public SINT Inp_PVBNotify
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_SmartDvcBSts</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public DINT Inp_SmartDvcBSts
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVABad</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL Inp_PVABad
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVAUncertain</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL Inp_PVAUncertain
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_SmartDvcADiagAvailable</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL Inp_SmartDvcADiagAvailable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVBBad</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL Inp_PVBBad
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVBUncertain</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL Inp_PVBUncertain
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_SmartDvcBDiagAvailable</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL Inp_SmartDvcBDiagAvailable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_DiffGate</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL Inp_DiffGate
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_AllowDisable</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL Cfg_AllowDisable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_AllowShelve</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL Cfg_AllowShelve
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_UseInpSrcQPVA</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL Cfg_UseInpSrcQPVA
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_UseInpSrcQPVB</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL Cfg_UseInpSrcQPVB
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasPVNav</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL Cfg_HasPVNav
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasMoreObj</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL Cfg_HasMoreObj
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasNav</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public SINT Cfg_HasNav
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PVEUMin</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public REAL Cfg_PVEUMin
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PVEUMax</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public REAL Cfg_PVEUMax
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_DiffLim</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public REAL Cfg_DiffLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_DiffDB</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public REAL Cfg_DiffDB
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_DiffGateDly</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public REAL Cfg_DiffGateDly
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OoRHiLim</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public REAL Cfg_OoRHiLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OoRLoLim</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public REAL Cfg_OoRLoLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OoRDB</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public REAL Cfg_OoRDB
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_AllowOper</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL Cfg_AllowOper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_AllowProg</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL Cfg_AllowProg
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_AllowExt</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL Cfg_AllowExt
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PVDecPlcs</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public SINT Cfg_PVDecPlcs
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CnfrmReqd</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public SINT Cfg_CnfrmReqd
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PSet_Owner</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public DINT PSet_Owner
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_SelA</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL PCmd_SelA
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_SelB</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL PCmd_SelB
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_SelAvg</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL PCmd_SelAvg
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_SelMin</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL PCmd_SelMin
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_SelMax</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL PCmd_SelMax
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Reset</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL PCmd_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_SelA</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL XCmd_SelA
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_SelB</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL XCmd_SelB
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_SelAvg</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL XCmd_SelAvg
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_SelMin</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL XCmd_SelMin
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_SelMax</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL XCmd_SelMax
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Reset</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL XCmd_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_ResetAckAll</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL XCmd_ResetAckAll
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public REAL Val
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_PVA</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public REAL Val_PVA
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_PVB</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public REAL Val_PVB
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_AvgPV</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public REAL Val_AvgPV
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_MinPV</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public REAL Val_MinPV
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_MaxPV</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public REAL Val_MaxPV
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_InpPV</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public REAL Val_InpPV
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_Diff</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public REAL Val_Diff
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_PVEUMin</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public REAL Val_PVEUMin
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_PVEUMax</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public REAL Val_PVEUMax
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_SmartDvcSts</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public DINT Out_SmartDvcSts
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Initialized</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL Sts_Initialized
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_SmartDvcDiagAvailable</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL Sts_SmartDvcDiagAvailable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_PVASel</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL Sts_PVASel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_PVBSel</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL Sts_PVBSel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_AvgSel</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL Sts_AvgSel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_MinSel</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL Sts_MinSel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_MaxSel</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL Sts_MaxSel
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_PVBad</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL Sts_PVBad
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_PVUncertain</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL Sts_PVUncertain
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SrcQ_IOA</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public SINT SrcQ_IOA
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SrcQ_IOB</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public SINT SrcQ_IOB
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SrcQ_IO</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public SINT SrcQ_IO
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SrcQ</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public SINT SrcQ
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eSts</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public SINT Sts_eSts
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eFault</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public INT Sts_eFault
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotify</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public SINT Sts_eNotify
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyAll</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public SINT Sts_eNotifyAll
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyOneGood</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public SINT Sts_eNotifyOneGood
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyNoneGood</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public SINT Sts_eNotifyNoneGood
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyDiff</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public SINT Sts_eNotifyDiff
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyFail</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public SINT Sts_eNotifyFail
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_UnackAlmCount</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public DINT Sts_UnackAlmCount
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Err</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL Sts_Err
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrEU</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL Sts_ErrEU
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrDiffDB</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL Sts_ErrDiffDB
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrDiffGateDly</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL Sts_ErrDiffGateDly
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrOoRDB</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL Sts_ErrOoRDB
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrAlm</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL Sts_ErrAlm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Alm</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL Sts_Alm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_AlmInh</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL Sts_AlmInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_OneGood</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL Sts_OneGood
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NoneGood</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL Sts_NoneGood
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_DiffCmp</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL Sts_DiffCmp
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_DiffGate</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL Sts_DiffGate
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Diff</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL Sts_Diff
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Fail</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL Sts_Fail
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_RdyAck</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL Sts_RdyAck
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_RdyReset</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL Sts_RdyReset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_SelA</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL XRdy_SelA
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_SelB</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL XRdy_SelB
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_SelAvg</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL XRdy_SelAvg
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_SelMin</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL XRdy_SelMin
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_SelMax</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL XRdy_SelMax
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Reset</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL XRdy_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_ResetAckAll</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public BOOL XRdy_ResetAckAll
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_Owner</c> member of the <see cref="P_ANALOG_INPUT_DUAL"/> data type.
    /// </summary>
    public DINT Val_Owner
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }
}