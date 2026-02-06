using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>P_ANALOG_INPUT_MULTI</c> data type structure.
/// </summary>
[LogixData("P_ANALOG_INPUT_MULTI")]
public sealed partial class P_ANALOG_INPUT_MULTI : StructureData
{
    /// <summary>
    /// Creates a new <see cref="P_ANALOG_INPUT_MULTI"/> instance initialized with default values.
    /// </summary>
    public P_ANALOG_INPUT_MULTI() : base("P_ANALOG_INPUT_MULTI")
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
        Inp_PVCData = new REAL();
        Inp_PVCSrcQ = new SINT();
        Inp_PVCNotify = new SINT();
        Inp_SmartDvcCSts = new DINT();
        Inp_PVDData = new REAL();
        Inp_PVDSrcQ = new SINT();
        Inp_PVDNotify = new SINT();
        Inp_SmartDvcDSts = new DINT();
        Inp_PVEData = new REAL();
        Inp_PVESrcQ = new SINT();
        Inp_PVENotify = new SINT();
        Inp_SmartDvcESts = new DINT();
        Inp_PVFData = new REAL();
        Inp_PVFSrcQ = new SINT();
        Inp_PVFNotify = new SINT();
        Inp_SmartDvcFSts = new DINT();
        Inp_PVGData = new REAL();
        Inp_PVGSrcQ = new SINT();
        Inp_PVGNotify = new SINT();
        Inp_SmartDvcGSts = new DINT();
        Inp_PVHData = new REAL();
        Inp_PVHSrcQ = new SINT();
        Inp_PVHNotify = new SINT();
        Inp_SmartDvcHSts = new DINT();
        Inp_PVABad = new BOOL();
        Inp_PVAUncertain = new BOOL();
        Inp_SmartDvcADiagAvailable = new BOOL();
        Inp_PVBBad = new BOOL();
        Inp_PVBUncertain = new BOOL();
        Inp_SmartDvcBDiagAvailable = new BOOL();
        Inp_PVCBad = new BOOL();
        Inp_PVCUncertain = new BOOL();
        Inp_SmartDvcCDiagAvailable = new BOOL();
        Inp_PVDBad = new BOOL();
        Inp_PVDUncertain = new BOOL();
        Inp_SmartDvcDDiagAvailable = new BOOL();
        Inp_PVEBad = new BOOL();
        Inp_PVEUncertain = new BOOL();
        Inp_SmartDvcEDiagAvailable = new BOOL();
        Inp_PVFBad = new BOOL();
        Inp_PVFUncertain = new BOOL();
        Inp_SmartDvcFDiagAvailable = new BOOL();
        Inp_PVGBad = new BOOL();
        Inp_PVGUncertain = new BOOL();
        Inp_SmartDvcGDiagAvailable = new BOOL();
        Inp_PVHBad = new BOOL();
        Inp_PVHUncertain = new BOOL();
        Inp_SmartDvcHDiagAvailable = new BOOL();
        Cfg_AllowDisable = new BOOL();
        Cfg_AllowShelve = new BOOL();
        Cfg_HasPVA = new BOOL();
        Cfg_HasPVB = new BOOL();
        Cfg_HasPVC = new BOOL();
        Cfg_HasPVD = new BOOL();
        Cfg_HasPVE = new BOOL();
        Cfg_HasPVF = new BOOL();
        Cfg_HasPVG = new BOOL();
        Cfg_HasPVH = new BOOL();
        Cfg_UsePVA = new BOOL();
        Cfg_UsePVB = new BOOL();
        Cfg_UsePVC = new BOOL();
        Cfg_UsePVD = new BOOL();
        Cfg_UsePVE = new BOOL();
        Cfg_UsePVF = new BOOL();
        Cfg_UsePVG = new BOOL();
        Cfg_UsePVH = new BOOL();
        Cfg_RejectUncertain = new BOOL();
        Cfg_UseStdDev = new BOOL();
        Cfg_CalcAvg = new BOOL();
        Cfg_UseInpSrcQPVA = new BOOL();
        Cfg_UseInpSrcQPVB = new BOOL();
        Cfg_UseInpSrcQPVC = new BOOL();
        Cfg_UseInpSrcQPVD = new BOOL();
        Cfg_UseInpSrcQPVE = new BOOL();
        Cfg_UseInpSrcQPVF = new BOOL();
        Cfg_UseInpSrcQPVG = new BOOL();
        Cfg_UseInpSrcQPVH = new BOOL();
        Cfg_HasPVNav = new BOOL();
        Cfg_HasMoreObj = new BOOL();
        Cfg_HasNav = new SINT();
        Cfg_MinGood = new DINT();
        Cfg_CalcWhen2 = new DINT();
        Cfg_PVEUMin = new REAL();
        Cfg_PVEUMax = new REAL();
        Cfg_AbsDevLim = new REAL();
        Cfg_OoRHiLim = new REAL();
        Cfg_OoRLoLim = new REAL();
        Cfg_OoRDB = new REAL();
        Cfg_PVDecPlcs = new SINT();
        Cfg_CnfrmReqd = new SINT();
        PSet_Owner = new DINT();
        PCmd_Reset = new BOOL();
        XCmd_Reset = new BOOL();
        XCmd_ResetAckAll = new BOOL();
        Val = new REAL();
        Val_PVA = new REAL();
        Val_PVB = new REAL();
        Val_PVC = new REAL();
        Val_PVD = new REAL();
        Val_PVE = new REAL();
        Val_PVF = new REAL();
        Val_PVG = new REAL();
        Val_PVH = new REAL();
        Val_InpPV = new REAL();
        Val_PVEUMin = new REAL();
        Val_PVEUMax = new REAL();
        Out_SmartDvcSts = new DINT();
        Val_NumPVs = new DINT();
        Sts_Initialized = new BOOL();
        Sts_SmartDvcDiagAvailable = new BOOL();
        Sts_PVBad = new BOOL();
        Sts_PVUncertain = new BOOL();
        Sts_PVAReject = new BOOL();
        Sts_PVBReject = new BOOL();
        Sts_PVCReject = new BOOL();
        Sts_PVDReject = new BOOL();
        Sts_PVEReject = new BOOL();
        Sts_PVFReject = new BOOL();
        Sts_PVGReject = new BOOL();
        Sts_PVHReject = new BOOL();
        SrcQ_IOA = new SINT();
        SrcQ_IOB = new SINT();
        SrcQ_IOC = new SINT();
        SrcQ_IOD = new SINT();
        SrcQ_IOE = new SINT();
        SrcQ_IOF = new SINT();
        SrcQ_IOG = new SINT();
        SrcQ_IOH = new SINT();
        SrcQ_IO = new SINT();
        SrcQ = new SINT();
        Sts_eSts = new SINT();
        Sts_eFault = new INT();
        Sts_eNotify = new SINT();
        Sts_eNotifyAll = new SINT();
        Sts_eNotifyAnyReject = new SINT();
        Sts_eNotifyMinGood = new SINT();
        Sts_eNotifyFail = new SINT();
        Sts_UnackAlmCount = new DINT();
        Sts_MaintByp = new BOOL();
        Sts_Err = new BOOL();
        Sts_ErrEU = new BOOL();
        Sts_ErrHas = new BOOL();
        Sts_ErrUse = new BOOL();
        Sts_ErrMinGood = new BOOL();
        Sts_ErrOoRDB = new BOOL();
        Sts_ErrAlm = new BOOL();
        Sts_Alm = new BOOL();
        Sts_AlmInh = new BOOL();
        Sts_AnyReject = new BOOL();
        Sts_MinGood = new BOOL();
        Sts_Fail = new BOOL();
        Sts_RdyAck = new BOOL();
        Sts_RdyReset = new BOOL();
        XRdy_Reset = new BOOL();
        XRdy_ResetAckAll = new BOOL();
        Val_Owner = new DINT();
    }
    
    /// <summary>
    /// Creates a new <see cref="P_ANALOG_INPUT_MULTI"/> instance initialized with the provided element.
    /// </summary>
    public P_ANALOG_INPUT_MULTI(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 472;
    
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
        Inp_PVCData.UpdateData(data, offset + 25);
        Inp_PVCSrcQ.UpdateData(data, offset + 29);
        Inp_PVCNotify.UpdateData(data, offset + 30);
        Inp_SmartDvcCSts.UpdateData(data, offset + 31);
        Inp_PVDData.UpdateData(data, offset + 35);
        Inp_PVDSrcQ.UpdateData(data, offset + 39);
        Inp_PVDNotify.UpdateData(data, offset + 40);
        Inp_SmartDvcDSts.UpdateData(data, offset + 41);
        Inp_PVEData.UpdateData(data, offset + 45);
        Inp_PVESrcQ.UpdateData(data, offset + 49);
        Inp_PVENotify.UpdateData(data, offset + 50);
        Inp_SmartDvcESts.UpdateData(data, offset + 51);
        Inp_PVFData.UpdateData(data, offset + 55);
        Inp_PVFSrcQ.UpdateData(data, offset + 59);
        Inp_PVFNotify.UpdateData(data, offset + 60);
        Inp_SmartDvcFSts.UpdateData(data, offset + 61);
        Inp_PVGData.UpdateData(data, offset + 65);
        Inp_PVGSrcQ.UpdateData(data, offset + 69);
        Inp_PVGNotify.UpdateData(data, offset + 70);
        Inp_SmartDvcGSts.UpdateData(data, offset + 71);
        Inp_PVHData.UpdateData(data, offset + 75);
        Inp_PVHSrcQ.UpdateData(data, offset + 79);
        Inp_PVHNotify.UpdateData(data, offset + 80);
        Inp_SmartDvcHSts.UpdateData(data, offset + 81);
        Inp_PVABad.UpdateData((data[offset + 85] & (1 << 3)) != 0);
        Inp_PVAUncertain.UpdateData((data[offset + 85] & (1 << 4)) != 0);
        Inp_SmartDvcADiagAvailable.UpdateData((data[offset + 85] & (1 << 5)) != 0);
        Inp_PVBBad.UpdateData((data[offset + 85] & (1 << 6)) != 0);
        Inp_PVBUncertain.UpdateData((data[offset + 85] & (1 << 7)) != 0);
        Inp_SmartDvcBDiagAvailable.UpdateData((data[offset + 86] & (1 << 0)) != 0);
        Inp_PVCBad.UpdateData((data[offset + 86] & (1 << 1)) != 0);
        Inp_PVCUncertain.UpdateData((data[offset + 86] & (1 << 2)) != 0);
        Inp_SmartDvcCDiagAvailable.UpdateData((data[offset + 86] & (1 << 3)) != 0);
        Inp_PVDBad.UpdateData((data[offset + 86] & (1 << 4)) != 0);
        Inp_PVDUncertain.UpdateData((data[offset + 86] & (1 << 5)) != 0);
        Inp_SmartDvcDDiagAvailable.UpdateData((data[offset + 86] & (1 << 6)) != 0);
        Inp_PVEBad.UpdateData((data[offset + 86] & (1 << 7)) != 0);
        Inp_PVEUncertain.UpdateData((data[offset + 87] & (1 << 0)) != 0);
        Inp_SmartDvcEDiagAvailable.UpdateData((data[offset + 87] & (1 << 1)) != 0);
        Inp_PVFBad.UpdateData((data[offset + 87] & (1 << 2)) != 0);
        Inp_PVFUncertain.UpdateData((data[offset + 87] & (1 << 3)) != 0);
        Inp_SmartDvcFDiagAvailable.UpdateData((data[offset + 87] & (1 << 4)) != 0);
        Inp_PVGBad.UpdateData((data[offset + 87] & (1 << 5)) != 0);
        Inp_PVGUncertain.UpdateData((data[offset + 87] & (1 << 6)) != 0);
        Inp_SmartDvcGDiagAvailable.UpdateData((data[offset + 87] & (1 << 7)) != 0);
        Inp_PVHBad.UpdateData((data[offset + 88] & (1 << 0)) != 0);
        Inp_PVHUncertain.UpdateData((data[offset + 88] & (1 << 1)) != 0);
        Inp_SmartDvcHDiagAvailable.UpdateData((data[offset + 88] & (1 << 2)) != 0);
        Cfg_AllowDisable.UpdateData((data[offset + 88] & (1 << 3)) != 0);
        Cfg_AllowShelve.UpdateData((data[offset + 88] & (1 << 4)) != 0);
        Cfg_HasPVA.UpdateData((data[offset + 88] & (1 << 5)) != 0);
        Cfg_HasPVB.UpdateData((data[offset + 88] & (1 << 6)) != 0);
        Cfg_HasPVC.UpdateData((data[offset + 88] & (1 << 7)) != 0);
        Cfg_HasPVD.UpdateData((data[offset + 93] & (1 << 0)) != 0);
        Cfg_HasPVE.UpdateData((data[offset + 93] & (1 << 1)) != 0);
        Cfg_HasPVF.UpdateData((data[offset + 93] & (1 << 2)) != 0);
        Cfg_HasPVG.UpdateData((data[offset + 93] & (1 << 3)) != 0);
        Cfg_HasPVH.UpdateData((data[offset + 93] & (1 << 4)) != 0);
        Cfg_UsePVA.UpdateData((data[offset + 93] & (1 << 5)) != 0);
        Cfg_UsePVB.UpdateData((data[offset + 93] & (1 << 6)) != 0);
        Cfg_UsePVC.UpdateData((data[offset + 93] & (1 << 7)) != 0);
        Cfg_UsePVD.UpdateData((data[offset + 94] & (1 << 0)) != 0);
        Cfg_UsePVE.UpdateData((data[offset + 94] & (1 << 1)) != 0);
        Cfg_UsePVF.UpdateData((data[offset + 94] & (1 << 2)) != 0);
        Cfg_UsePVG.UpdateData((data[offset + 94] & (1 << 3)) != 0);
        Cfg_UsePVH.UpdateData((data[offset + 94] & (1 << 4)) != 0);
        Cfg_RejectUncertain.UpdateData((data[offset + 94] & (1 << 5)) != 0);
        Cfg_UseStdDev.UpdateData((data[offset + 94] & (1 << 6)) != 0);
        Cfg_CalcAvg.UpdateData((data[offset + 94] & (1 << 7)) != 0);
        Cfg_UseInpSrcQPVA.UpdateData((data[offset + 95] & (1 << 0)) != 0);
        Cfg_UseInpSrcQPVB.UpdateData((data[offset + 95] & (1 << 1)) != 0);
        Cfg_UseInpSrcQPVC.UpdateData((data[offset + 95] & (1 << 2)) != 0);
        Cfg_UseInpSrcQPVD.UpdateData((data[offset + 95] & (1 << 3)) != 0);
        Cfg_UseInpSrcQPVE.UpdateData((data[offset + 95] & (1 << 4)) != 0);
        Cfg_UseInpSrcQPVF.UpdateData((data[offset + 95] & (1 << 5)) != 0);
        Cfg_UseInpSrcQPVG.UpdateData((data[offset + 95] & (1 << 6)) != 0);
        Cfg_UseInpSrcQPVH.UpdateData((data[offset + 95] & (1 << 7)) != 0);
        Cfg_HasPVNav.UpdateData((data[offset + 96] & (1 << 0)) != 0);
        Cfg_HasMoreObj.UpdateData((data[offset + 96] & (1 << 1)) != 0);
        Cfg_HasNav.UpdateData(data, offset + 96);
        Cfg_MinGood.UpdateData(data, offset + 97);
        Cfg_CalcWhen2.UpdateData(data, offset + 101);
        Cfg_PVEUMin.UpdateData(data, offset + 105);
        Cfg_PVEUMax.UpdateData(data, offset + 109);
        Cfg_AbsDevLim.UpdateData(data, offset + 113);
        Cfg_OoRHiLim.UpdateData(data, offset + 117);
        Cfg_OoRLoLim.UpdateData(data, offset + 121);
        Cfg_OoRDB.UpdateData(data, offset + 125);
        Cfg_PVDecPlcs.UpdateData(data, offset + 129);
        Cfg_CnfrmReqd.UpdateData(data, offset + 130);
        PSet_Owner.UpdateData(data, offset + 131);
        PCmd_Reset.UpdateData((data[offset + 135] & (1 << 2)) != 0);
        XCmd_Reset.UpdateData((data[offset + 135] & (1 << 3)) != 0);
        XCmd_ResetAckAll.UpdateData((data[offset + 135] & (1 << 4)) != 0);
        Val.UpdateData(data, offset + 135);
        Val_PVA.UpdateData(data, offset + 139);
        Val_PVB.UpdateData(data, offset + 143);
        Val_PVC.UpdateData(data, offset + 147);
        Val_PVD.UpdateData(data, offset + 151);
        Val_PVE.UpdateData(data, offset + 155);
        Val_PVF.UpdateData(data, offset + 159);
        Val_PVG.UpdateData(data, offset + 163);
        Val_PVH.UpdateData(data, offset + 167);
        Val_InpPV.UpdateData(data, offset + 171);
        Val_PVEUMin.UpdateData(data, offset + 175);
        Val_PVEUMax.UpdateData(data, offset + 179);
        Out_SmartDvcSts.UpdateData(data, offset + 183);
        Val_NumPVs.UpdateData(data, offset + 187);
        Sts_Initialized.UpdateData((data[offset + 191] & (1 << 5)) != 0);
        Sts_SmartDvcDiagAvailable.UpdateData((data[offset + 191] & (1 << 6)) != 0);
        Sts_PVBad.UpdateData((data[offset + 191] & (1 << 7)) != 0);
        Sts_PVUncertain.UpdateData((data[offset + 196] & (1 << 0)) != 0);
        Sts_PVAReject.UpdateData((data[offset + 196] & (1 << 1)) != 0);
        Sts_PVBReject.UpdateData((data[offset + 196] & (1 << 2)) != 0);
        Sts_PVCReject.UpdateData((data[offset + 196] & (1 << 3)) != 0);
        Sts_PVDReject.UpdateData((data[offset + 196] & (1 << 4)) != 0);
        Sts_PVEReject.UpdateData((data[offset + 196] & (1 << 5)) != 0);
        Sts_PVFReject.UpdateData((data[offset + 196] & (1 << 6)) != 0);
        Sts_PVGReject.UpdateData((data[offset + 196] & (1 << 7)) != 0);
        Sts_PVHReject.UpdateData((data[offset + 197] & (1 << 0)) != 0);
        SrcQ_IOA.UpdateData(data, offset + 197);
        SrcQ_IOB.UpdateData(data, offset + 198);
        SrcQ_IOC.UpdateData(data, offset + 199);
        SrcQ_IOD.UpdateData(data, offset + 200);
        SrcQ_IOE.UpdateData(data, offset + 201);
        SrcQ_IOF.UpdateData(data, offset + 202);
        SrcQ_IOG.UpdateData(data, offset + 203);
        SrcQ_IOH.UpdateData(data, offset + 204);
        SrcQ_IO.UpdateData(data, offset + 205);
        SrcQ.UpdateData(data, offset + 206);
        Sts_eSts.UpdateData(data, offset + 207);
        Sts_eFault.UpdateData(data, offset + 208);
        Sts_eNotify.UpdateData(data, offset + 210);
        Sts_eNotifyAll.UpdateData(data, offset + 211);
        Sts_eNotifyAnyReject.UpdateData(data, offset + 212);
        Sts_eNotifyMinGood.UpdateData(data, offset + 213);
        Sts_eNotifyFail.UpdateData(data, offset + 214);
        Sts_UnackAlmCount.UpdateData(data, offset + 215);
        Sts_MaintByp.UpdateData((data[offset + 219] & (1 << 1)) != 0);
        Sts_Err.UpdateData((data[offset + 219] & (1 << 2)) != 0);
        Sts_ErrEU.UpdateData((data[offset + 219] & (1 << 3)) != 0);
        Sts_ErrHas.UpdateData((data[offset + 219] & (1 << 4)) != 0);
        Sts_ErrUse.UpdateData((data[offset + 219] & (1 << 5)) != 0);
        Sts_ErrMinGood.UpdateData((data[offset + 219] & (1 << 6)) != 0);
        Sts_ErrOoRDB.UpdateData((data[offset + 219] & (1 << 7)) != 0);
        Sts_ErrAlm.UpdateData((data[offset + 220] & (1 << 0)) != 0);
        Sts_Alm.UpdateData((data[offset + 220] & (1 << 1)) != 0);
        Sts_AlmInh.UpdateData((data[offset + 220] & (1 << 2)) != 0);
        Sts_AnyReject.UpdateData((data[offset + 220] & (1 << 3)) != 0);
        Sts_MinGood.UpdateData((data[offset + 220] & (1 << 4)) != 0);
        Sts_Fail.UpdateData((data[offset + 220] & (1 << 5)) != 0);
        Sts_RdyAck.UpdateData((data[offset + 220] & (1 << 6)) != 0);
        Sts_RdyReset.UpdateData((data[offset + 220] & (1 << 7)) != 0);
        XRdy_Reset.UpdateData((data[offset + 221] & (1 << 0)) != 0);
        XRdy_ResetAckAll.UpdateData((data[offset + 221] & (1 << 1)) != 0);
        Val_Owner.UpdateData(data, offset + 221);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_InitializeReq</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Inp_InitializeReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVAData</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public REAL Inp_PVAData
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVASrcQ</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public SINT Inp_PVASrcQ
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVANotify</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public SINT Inp_PVANotify
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_SmartDvcASts</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public DINT Inp_SmartDvcASts
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVBData</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public REAL Inp_PVBData
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVBSrcQ</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public SINT Inp_PVBSrcQ
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVBNotify</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public SINT Inp_PVBNotify
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_SmartDvcBSts</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public DINT Inp_SmartDvcBSts
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVCData</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public REAL Inp_PVCData
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVCSrcQ</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public SINT Inp_PVCSrcQ
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVCNotify</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public SINT Inp_PVCNotify
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_SmartDvcCSts</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public DINT Inp_SmartDvcCSts
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVDData</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public REAL Inp_PVDData
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVDSrcQ</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public SINT Inp_PVDSrcQ
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVDNotify</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public SINT Inp_PVDNotify
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_SmartDvcDSts</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public DINT Inp_SmartDvcDSts
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVEData</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public REAL Inp_PVEData
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVESrcQ</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public SINT Inp_PVESrcQ
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVENotify</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public SINT Inp_PVENotify
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_SmartDvcESts</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public DINT Inp_SmartDvcESts
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVFData</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public REAL Inp_PVFData
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVFSrcQ</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public SINT Inp_PVFSrcQ
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVFNotify</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public SINT Inp_PVFNotify
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_SmartDvcFSts</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public DINT Inp_SmartDvcFSts
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVGData</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public REAL Inp_PVGData
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVGSrcQ</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public SINT Inp_PVGSrcQ
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVGNotify</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public SINT Inp_PVGNotify
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_SmartDvcGSts</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public DINT Inp_SmartDvcGSts
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVHData</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public REAL Inp_PVHData
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVHSrcQ</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public SINT Inp_PVHSrcQ
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVHNotify</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public SINT Inp_PVHNotify
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_SmartDvcHSts</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public DINT Inp_SmartDvcHSts
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVABad</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Inp_PVABad
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVAUncertain</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Inp_PVAUncertain
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_SmartDvcADiagAvailable</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Inp_SmartDvcADiagAvailable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVBBad</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Inp_PVBBad
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVBUncertain</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Inp_PVBUncertain
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_SmartDvcBDiagAvailable</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Inp_SmartDvcBDiagAvailable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVCBad</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Inp_PVCBad
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVCUncertain</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Inp_PVCUncertain
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_SmartDvcCDiagAvailable</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Inp_SmartDvcCDiagAvailable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVDBad</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Inp_PVDBad
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVDUncertain</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Inp_PVDUncertain
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_SmartDvcDDiagAvailable</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Inp_SmartDvcDDiagAvailable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVEBad</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Inp_PVEBad
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVEUncertain</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Inp_PVEUncertain
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_SmartDvcEDiagAvailable</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Inp_SmartDvcEDiagAvailable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVFBad</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Inp_PVFBad
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVFUncertain</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Inp_PVFUncertain
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_SmartDvcFDiagAvailable</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Inp_SmartDvcFDiagAvailable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVGBad</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Inp_PVGBad
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVGUncertain</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Inp_PVGUncertain
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_SmartDvcGDiagAvailable</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Inp_SmartDvcGDiagAvailable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVHBad</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Inp_PVHBad
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVHUncertain</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Inp_PVHUncertain
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_SmartDvcHDiagAvailable</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Inp_SmartDvcHDiagAvailable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_AllowDisable</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Cfg_AllowDisable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_AllowShelve</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Cfg_AllowShelve
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasPVA</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Cfg_HasPVA
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasPVB</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Cfg_HasPVB
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasPVC</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Cfg_HasPVC
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasPVD</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Cfg_HasPVD
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasPVE</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Cfg_HasPVE
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasPVF</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Cfg_HasPVF
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasPVG</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Cfg_HasPVG
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasPVH</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Cfg_HasPVH
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_UsePVA</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Cfg_UsePVA
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_UsePVB</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Cfg_UsePVB
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_UsePVC</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Cfg_UsePVC
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_UsePVD</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Cfg_UsePVD
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_UsePVE</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Cfg_UsePVE
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_UsePVF</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Cfg_UsePVF
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_UsePVG</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Cfg_UsePVG
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_UsePVH</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Cfg_UsePVH
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_RejectUncertain</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Cfg_RejectUncertain
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_UseStdDev</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Cfg_UseStdDev
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CalcAvg</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Cfg_CalcAvg
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_UseInpSrcQPVA</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Cfg_UseInpSrcQPVA
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_UseInpSrcQPVB</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Cfg_UseInpSrcQPVB
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_UseInpSrcQPVC</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Cfg_UseInpSrcQPVC
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_UseInpSrcQPVD</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Cfg_UseInpSrcQPVD
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_UseInpSrcQPVE</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Cfg_UseInpSrcQPVE
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_UseInpSrcQPVF</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Cfg_UseInpSrcQPVF
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_UseInpSrcQPVG</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Cfg_UseInpSrcQPVG
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_UseInpSrcQPVH</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Cfg_UseInpSrcQPVH
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasPVNav</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Cfg_HasPVNav
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasMoreObj</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Cfg_HasMoreObj
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasNav</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public SINT Cfg_HasNav
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_MinGood</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public DINT Cfg_MinGood
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CalcWhen2</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public DINT Cfg_CalcWhen2
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PVEUMin</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public REAL Cfg_PVEUMin
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PVEUMax</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public REAL Cfg_PVEUMax
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_AbsDevLim</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public REAL Cfg_AbsDevLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OoRHiLim</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public REAL Cfg_OoRHiLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OoRLoLim</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public REAL Cfg_OoRLoLim
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OoRDB</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public REAL Cfg_OoRDB
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_PVDecPlcs</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public SINT Cfg_PVDecPlcs
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CnfrmReqd</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public SINT Cfg_CnfrmReqd
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PSet_Owner</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public DINT PSet_Owner
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Reset</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL PCmd_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Reset</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL XCmd_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_ResetAckAll</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL XCmd_ResetAckAll
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public REAL Val
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_PVA</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public REAL Val_PVA
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_PVB</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public REAL Val_PVB
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_PVC</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public REAL Val_PVC
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_PVD</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public REAL Val_PVD
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_PVE</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public REAL Val_PVE
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_PVF</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public REAL Val_PVF
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_PVG</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public REAL Val_PVG
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_PVH</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public REAL Val_PVH
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_InpPV</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public REAL Val_InpPV
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_PVEUMin</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public REAL Val_PVEUMin
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_PVEUMax</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public REAL Val_PVEUMax
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_SmartDvcSts</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public DINT Out_SmartDvcSts
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_NumPVs</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public DINT Val_NumPVs
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Initialized</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Sts_Initialized
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_SmartDvcDiagAvailable</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Sts_SmartDvcDiagAvailable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_PVBad</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Sts_PVBad
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_PVUncertain</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Sts_PVUncertain
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_PVAReject</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Sts_PVAReject
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_PVBReject</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Sts_PVBReject
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_PVCReject</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Sts_PVCReject
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_PVDReject</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Sts_PVDReject
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_PVEReject</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Sts_PVEReject
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_PVFReject</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Sts_PVFReject
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_PVGReject</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Sts_PVGReject
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_PVHReject</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Sts_PVHReject
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SrcQ_IOA</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public SINT SrcQ_IOA
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SrcQ_IOB</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public SINT SrcQ_IOB
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SrcQ_IOC</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public SINT SrcQ_IOC
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SrcQ_IOD</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public SINT SrcQ_IOD
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SrcQ_IOE</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public SINT SrcQ_IOE
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SrcQ_IOF</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public SINT SrcQ_IOF
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SrcQ_IOG</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public SINT SrcQ_IOG
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SrcQ_IOH</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public SINT SrcQ_IOH
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SrcQ_IO</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public SINT SrcQ_IO
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SrcQ</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public SINT SrcQ
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eSts</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public SINT Sts_eSts
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eFault</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public INT Sts_eFault
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotify</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public SINT Sts_eNotify
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyAll</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public SINT Sts_eNotifyAll
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyAnyReject</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public SINT Sts_eNotifyAnyReject
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyMinGood</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public SINT Sts_eNotifyMinGood
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyFail</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public SINT Sts_eNotifyFail
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_UnackAlmCount</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public DINT Sts_UnackAlmCount
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_MaintByp</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Sts_MaintByp
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Err</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Sts_Err
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrEU</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Sts_ErrEU
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrHas</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Sts_ErrHas
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrUse</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Sts_ErrUse
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrMinGood</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Sts_ErrMinGood
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrOoRDB</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Sts_ErrOoRDB
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrAlm</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Sts_ErrAlm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Alm</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Sts_Alm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_AlmInh</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Sts_AlmInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_AnyReject</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Sts_AnyReject
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_MinGood</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Sts_MinGood
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Fail</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Sts_Fail
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_RdyAck</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Sts_RdyAck
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_RdyReset</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL Sts_RdyReset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Reset</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL XRdy_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_ResetAckAll</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public BOOL XRdy_ResetAckAll
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_Owner</c> member of the <see cref="P_ANALOG_INPUT_MULTI"/> data type.
    /// </summary>
    public DINT Val_Owner
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }
}