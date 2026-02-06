using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

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
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 704;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        EnableIn.UpdateData((data[offset + 5] & (1 << 0)) != 0);
        EnableOut.UpdateData((data[offset + 5] & (1 << 1)) != 0);
        Inp_InitializeReq.UpdateData((data[offset + 5] & (1 << 2)) != 0);
        Inp_PVData.UpdateData(data, offset + 5);
        Inp_SmartDvcSts.UpdateData(data, offset + 9);
        Inp_SmartDvcDiagAvailable.UpdateData((data[offset + 13] & (1 << 3)) != 0);
        Inp_ModFault.UpdateData((data[offset + 13] & (1 << 4)) != 0);
        Inp_ChanFault.UpdateData((data[offset + 13] & (1 << 5)) != 0);
        Inp_OutOfSpec.UpdateData((data[offset + 13] & (1 << 6)) != 0);
        Inp_FuncCheck.UpdateData((data[offset + 13] & (1 << 7)) != 0);
        Inp_MaintReqd.UpdateData((data[offset + 14] & (1 << 0)) != 0);
        Inp_PVUncertain.UpdateData((data[offset + 14] & (1 << 1)) != 0);
        Inp_PVNotify.UpdateData(data, offset + 14);
        Inp_HiHiGate.UpdateData((data[offset + 15] & (1 << 2)) != 0);
        Inp_HiGate.UpdateData((data[offset + 15] & (1 << 3)) != 0);
        Inp_LoGate.UpdateData((data[offset + 15] & (1 << 4)) != 0);
        Inp_LoLoGate.UpdateData((data[offset + 15] & (1 << 5)) != 0);
        Inp_HiRoCGate.UpdateData((data[offset + 15] & (1 << 6)) != 0);
        Inp_HiDevGate.UpdateData((data[offset + 15] & (1 << 7)) != 0);
        Inp_LoDevGate.UpdateData((data[offset + 16] & (1 << 0)) != 0);
        Inp_OoRGate.UpdateData((data[offset + 16] & (1 << 1)) != 0);
        Inp_Reset.UpdateData((data[offset + 16] & (1 << 2)) != 0);
        Cfg_AllowDisable.UpdateData((data[offset + 16] & (1 << 3)) != 0);
        Cfg_AllowShelve.UpdateData((data[offset + 16] & (1 << 4)) != 0);
        Cfg_ClampSB.UpdateData(data, offset + 16);
        Cfg_InpRawMin.UpdateData(data, offset + 20);
        Cfg_InpRawMax.UpdateData(data, offset + 24);
        Cfg_PVEUMin.UpdateData(data, offset + 28);
        Cfg_PVEUMax.UpdateData(data, offset + 32);
        Cfg_Ref.UpdateData(data, offset + 36);
        Cfg_FiltWLag.UpdateData(data, offset + 40);
        Cfg_FiltOrder.UpdateData(data, offset + 44);
        Cfg_RateTime.UpdateData(data, offset + 48);
        Cfg_PVHiLim.UpdateData(data, offset + 52);
        Cfg_PVLoLim.UpdateData(data, offset + 56);
        Cfg_PVReplaceVal.UpdateData(data, offset + 60);
        Cfg_HiHiLim.UpdateData(data, offset + 64);
        Cfg_HiHiDB.UpdateData(data, offset + 68);
        Cfg_HiHiGateDly.UpdateData(data, offset + 72);
        Cfg_HiLim.UpdateData(data, offset + 76);
        Cfg_HiDB.UpdateData(data, offset + 80);
        Cfg_HiGateDly.UpdateData(data, offset + 84);
        Cfg_LoLim.UpdateData(data, offset + 88);
        Cfg_LoDB.UpdateData(data, offset + 92);
        Cfg_LoGateDly.UpdateData(data, offset + 96);
        Cfg_LoLoLim.UpdateData(data, offset + 100);
        Cfg_LoLoDB.UpdateData(data, offset + 104);
        Cfg_LoLoGateDly.UpdateData(data, offset + 108);
        Cfg_HiRoCLim.UpdateData(data, offset + 112);
        Cfg_HiRoCDB.UpdateData(data, offset + 116);
        Cfg_HiRoCGateDly.UpdateData(data, offset + 120);
        Cfg_HiDevLim.UpdateData(data, offset + 124);
        Cfg_HiDevDB.UpdateData(data, offset + 128);
        Cfg_HiDevGateDly.UpdateData(data, offset + 132);
        Cfg_LoDevLim.UpdateData(data, offset + 136);
        Cfg_LoDevDB.UpdateData(data, offset + 140);
        Cfg_LoDevGateDly.UpdateData(data, offset + 144);
        Cfg_OoRHiLim.UpdateData(data, offset + 148);
        Cfg_OoRLoLim.UpdateData(data, offset + 152);
        Cfg_OoRDB.UpdateData(data, offset + 156);
        Cfg_OoRGateDly.UpdateData(data, offset + 160);
        Cfg_OoROnDly.UpdateData(data, offset + 164);
        Cfg_OoROffDly.UpdateData(data, offset + 168);
        Cfg_StuckTime.UpdateData(data, offset + 172);
        Cfg_InpOoRAction.UpdateData(data, offset + 176);
        Cfg_InpOoRQual.UpdateData(data, offset + 177);
        Cfg_InpStuckAction.UpdateData(data, offset + 178);
        Cfg_InpStuckQual.UpdateData(data, offset + 179);
        Cfg_InpNaNAction.UpdateData(data, offset + 180);
        Cfg_InpNaNQual.UpdateData(data, offset + 181);
        Cfg_ModFaultAction.UpdateData(data, offset + 182);
        Cfg_ModFaultQual.UpdateData(data, offset + 183);
        Cfg_ChanFaultAction.UpdateData(data, offset + 184);
        Cfg_ChanFaultQual.UpdateData(data, offset + 185);
        Cfg_OutOfSpecAction.UpdateData(data, offset + 186);
        Cfg_OutOfSpecQual.UpdateData(data, offset + 187);
        Cfg_FuncCheckAction.UpdateData(data, offset + 188);
        Cfg_FuncCheckQual.UpdateData(data, offset + 189);
        Cfg_MaintReqdAction.UpdateData(data, offset + 190);
        Cfg_MaintReqdQual.UpdateData(data, offset + 191);
        Cfg_CfgErrAction.UpdateData(data, offset + 192);
        Cfg_CfgErrQual.UpdateData(data, offset + 193);
        Cfg_CtrlHiHiLim.UpdateData(data, offset + 194);
        Cfg_CtrlHiHiDB.UpdateData(data, offset + 198);
        Cfg_CtrlHiLim.UpdateData(data, offset + 202);
        Cfg_CtrlHiDB.UpdateData(data, offset + 206);
        Cfg_CtrlLoLim.UpdateData(data, offset + 210);
        Cfg_CtrlLoDB.UpdateData(data, offset + 214);
        Cfg_CtrlLoLoLim.UpdateData(data, offset + 218);
        Cfg_CtrlLoLoDB.UpdateData(data, offset + 222);
        Cfg_HasSmartDvc.UpdateData((data[offset + 226] & (1 << 5)) != 0);
        Cfg_HasRoC.UpdateData((data[offset + 226] & (1 << 6)) != 0);
        Cfg_HasDev.UpdateData((data[offset + 226] & (1 << 7)) != 0);
        Cfg_HasMoreObj.UpdateData((data[offset + 227] & (1 << 0)) != 0);
        Cfg_HasOutNav.UpdateData((data[offset + 227] & (1 << 1)) != 0);
        Cfg_HasPVNav.UpdateData((data[offset + 227] & (1 << 2)) != 0);
        Cfg_HasHistTrend.UpdateData(data, offset + 227);
        Cfg_FailOnUncertain.UpdateData((data[offset + 228] & (1 << 3)) != 0);
        Cfg_NoSubstPV.UpdateData((data[offset + 228] & (1 << 4)) != 0);
        Cfg_SetTrack.UpdateData((data[offset + 228] & (1 << 5)) != 0);
        Cfg_SclngTyp.UpdateData(data, offset + 228);
        Cfg_PVDecPlcs.UpdateData(data, offset + 229);
        Cfg_CnfrmReqd.UpdateData(data, offset + 230);
        PSet_Owner.UpdateData(data, offset + 231);
        Set_VirtualPV.UpdateData(data, offset + 235);
        PCmd_ClearCapt.UpdateData((data[offset + 239] & (1 << 6)) != 0);
        PCmd_Reset.UpdateData((data[offset + 239] & (1 << 7)) != 0);
        PCmd_Physical.UpdateData((data[offset + 244] & (1 << 0)) != 0);
        PCmd_Virtual.UpdateData((data[offset + 244] & (1 << 1)) != 0);
        XCmd_ClearCapt.UpdateData((data[offset + 244] & (1 << 2)) != 0);
        XCmd_Reset.UpdateData((data[offset + 244] & (1 << 3)) != 0);
        XCmd_ResetAckAll.UpdateData((data[offset + 244] & (1 << 4)) != 0);
        Val.UpdateData(data, offset + 244);
        Val_InpPV.UpdateData(data, offset + 248);
        Val_RoC.UpdateData(data, offset + 252);
        Val_Dev.UpdateData(data, offset + 256);
        Val_PVMinCapt.UpdateData(data, offset + 260);
        Val_PVMaxCapt.UpdateData(data, offset + 264);
        Val_PVEUMin.UpdateData(data, offset + 268);
        Val_PVEUMax.UpdateData(data, offset + 272);
        Out_Reset.UpdateData((data[offset + 276] & (1 << 5)) != 0);
        Out_SmartDvcSts.UpdateData(data, offset + 276);
        Sts_Initialized.UpdateData((data[offset + 280] & (1 << 6)) != 0);
        Sts_SmartDvcDiagAvailable.UpdateData((data[offset + 280] & (1 << 7)) != 0);
        Sts_PVGood.UpdateData((data[offset + 281] & (1 << 0)) != 0);
        Sts_PVUncertain.UpdateData((data[offset + 281] & (1 << 1)) != 0);
        Sts_PVBad.UpdateData((data[offset + 281] & (1 << 2)) != 0);
        Sts_InpStuck.UpdateData((data[offset + 281] & (1 << 3)) != 0);
        Sts_InpNaN.UpdateData((data[offset + 281] & (1 << 4)) != 0);
        Sts_OutOfSpec.UpdateData((data[offset + 281] & (1 << 5)) != 0);
        Sts_FuncCheck.UpdateData((data[offset + 281] & (1 << 6)) != 0);
        Sts_MaintReqd.UpdateData((data[offset + 281] & (1 << 7)) != 0);
        Sts_UseInp.UpdateData((data[offset + 282] & (1 << 0)) != 0);
        Sts_HoldLast.UpdateData((data[offset + 282] & (1 << 1)) != 0);
        Sts_Clamped.UpdateData((data[offset + 282] & (1 << 2)) != 0);
        Sts_Replaced.UpdateData((data[offset + 282] & (1 << 3)) != 0);
        Sts_SubstPV.UpdateData((data[offset + 282] & (1 << 4)) != 0);
        Sts_InpPV.UpdateData((data[offset + 282] & (1 << 5)) != 0);
        Sts_Virtual.UpdateData((data[offset + 282] & (1 << 6)) != 0);
        SrcQ_IO.UpdateData(data, offset + 282);
        SrcQ.UpdateData(data, offset + 283);
        Sts_eSts.UpdateData(data, offset + 284);
        Sts_eFault.UpdateData(data, offset + 285);
        Sts_eNotify.UpdateData(data, offset + 287);
        Sts_eNotifyAll.UpdateData(data, offset + 288);
        Sts_eNotifyHiHi.UpdateData(data, offset + 289);
        Sts_eNotifyHi.UpdateData(data, offset + 290);
        Sts_eNotifyLo.UpdateData(data, offset + 291);
        Sts_eNotifyLoLo.UpdateData(data, offset + 292);
        Sts_eNotifyHiRoC.UpdateData(data, offset + 293);
        Sts_eNotifyHiDev.UpdateData(data, offset + 294);
        Sts_eNotifyLoDev.UpdateData(data, offset + 295);
        Sts_eNotifyFail.UpdateData(data, offset + 296);
        Sts_UnackAlmCount.UpdateData(data, offset + 297);
        Sts_MaintByp.UpdateData((data[offset + 301] & (1 << 7)) != 0);
        Sts_Err.UpdateData((data[offset + 302] & (1 << 0)) != 0);
        Sts_ErrRaw.UpdateData((data[offset + 302] & (1 << 1)) != 0);
        Sts_ErrEU.UpdateData((data[offset + 302] & (1 << 2)) != 0);
        Sts_ErrFiltWLag.UpdateData((data[offset + 302] & (1 << 3)) != 0);
        Sts_ErrFiltOrder.UpdateData((data[offset + 302] & (1 << 4)) != 0);
        Sts_ErrRateTime.UpdateData((data[offset + 302] & (1 << 5)) != 0);
        Sts_ErrHiHiDB.UpdateData((data[offset + 302] & (1 << 6)) != 0);
        Sts_ErrHiHiGateDly.UpdateData((data[offset + 302] & (1 << 7)) != 0);
        Sts_ErrHiDB.UpdateData((data[offset + 307] & (1 << 0)) != 0);
        Sts_ErrHiGateDly.UpdateData((data[offset + 307] & (1 << 1)) != 0);
        Sts_ErrLoDB.UpdateData((data[offset + 307] & (1 << 2)) != 0);
        Sts_ErrLoGateDly.UpdateData((data[offset + 307] & (1 << 3)) != 0);
        Sts_ErrLoLoDB.UpdateData((data[offset + 307] & (1 << 4)) != 0);
        Sts_ErrLoLoGateDly.UpdateData((data[offset + 307] & (1 << 5)) != 0);
        Sts_ErrHiRoCDB.UpdateData((data[offset + 307] & (1 << 6)) != 0);
        Sts_ErrHiRoCGateDly.UpdateData((data[offset + 307] & (1 << 7)) != 0);
        Sts_ErrHiDevDB.UpdateData((data[offset + 308] & (1 << 0)) != 0);
        Sts_ErrHiDevGateDly.UpdateData((data[offset + 308] & (1 << 1)) != 0);
        Sts_ErrLoDevDB.UpdateData((data[offset + 308] & (1 << 2)) != 0);
        Sts_ErrLoDevGateDly.UpdateData((data[offset + 308] & (1 << 3)) != 0);
        Sts_ErrOoRDB.UpdateData((data[offset + 308] & (1 << 4)) != 0);
        Sts_ErrOoRGateDly.UpdateData((data[offset + 308] & (1 << 5)) != 0);
        Sts_ErrOoROnDly.UpdateData((data[offset + 308] & (1 << 6)) != 0);
        Sts_ErrOoROffDly.UpdateData((data[offset + 308] & (1 << 7)) != 0);
        Sts_ErrStuckTime.UpdateData((data[offset + 309] & (1 << 0)) != 0);
        Sts_ErrAlm.UpdateData((data[offset + 309] & (1 << 1)) != 0);
        Sts_ErrDB.UpdateData((data[offset + 309] & (1 << 2)) != 0);
        Sts_ErrCtrlDB.UpdateData((data[offset + 309] & (1 << 3)) != 0);
        Sts_ErrCtrlHiHiDB.UpdateData((data[offset + 309] & (1 << 4)) != 0);
        Sts_ErrCtrlHiDB.UpdateData((data[offset + 309] & (1 << 5)) != 0);
        Sts_ErrCtrlLoDB.UpdateData((data[offset + 309] & (1 << 6)) != 0);
        Sts_ErrCtrlLoLoDB.UpdateData((data[offset + 309] & (1 << 7)) != 0);
        Sts_Alm.UpdateData((data[offset + 310] & (1 << 0)) != 0);
        Sts_AlmInh.UpdateData((data[offset + 310] & (1 << 1)) != 0);
        Sts_IOFault.UpdateData((data[offset + 310] & (1 << 2)) != 0);
        Sts_HiHiCmp.UpdateData((data[offset + 310] & (1 << 3)) != 0);
        Sts_HiHiGate.UpdateData((data[offset + 310] & (1 << 4)) != 0);
        Sts_HiHi.UpdateData((data[offset + 310] & (1 << 5)) != 0);
        Sts_HiCmp.UpdateData((data[offset + 310] & (1 << 6)) != 0);
        Sts_HiGate.UpdateData((data[offset + 310] & (1 << 7)) != 0);
        Sts_Hi.UpdateData((data[offset + 315] & (1 << 0)) != 0);
        Sts_LoCmp.UpdateData((data[offset + 315] & (1 << 1)) != 0);
        Sts_LoGate.UpdateData((data[offset + 315] & (1 << 2)) != 0);
        Sts_Lo.UpdateData((data[offset + 315] & (1 << 3)) != 0);
        Sts_LoLoCmp.UpdateData((data[offset + 315] & (1 << 4)) != 0);
        Sts_LoLoGate.UpdateData((data[offset + 315] & (1 << 5)) != 0);
        Sts_LoLo.UpdateData((data[offset + 315] & (1 << 6)) != 0);
        Sts_CtrlHiHi.UpdateData((data[offset + 315] & (1 << 7)) != 0);
        Sts_CtrlHi.UpdateData((data[offset + 316] & (1 << 0)) != 0);
        Sts_CtrlLo.UpdateData((data[offset + 316] & (1 << 1)) != 0);
        Sts_CtrlLoLo.UpdateData((data[offset + 316] & (1 << 2)) != 0);
        Sts_HiRoCCmp.UpdateData((data[offset + 316] & (1 << 3)) != 0);
        Sts_HiRoCGate.UpdateData((data[offset + 316] & (1 << 4)) != 0);
        Sts_HiRoC.UpdateData((data[offset + 316] & (1 << 5)) != 0);
        Sts_HiDevCmp.UpdateData((data[offset + 316] & (1 << 6)) != 0);
        Sts_HiDevGate.UpdateData((data[offset + 316] & (1 << 7)) != 0);
        Sts_HiDev.UpdateData((data[offset + 317] & (1 << 0)) != 0);
        Sts_LoDevCmp.UpdateData((data[offset + 317] & (1 << 1)) != 0);
        Sts_LoDevGate.UpdateData((data[offset + 317] & (1 << 2)) != 0);
        Sts_LoDev.UpdateData((data[offset + 317] & (1 << 3)) != 0);
        Sts_OoRHiCmp.UpdateData((data[offset + 317] & (1 << 4)) != 0);
        Sts_OoRLoCmp.UpdateData((data[offset + 317] & (1 << 5)) != 0);
        Sts_OoRCmp.UpdateData((data[offset + 317] & (1 << 6)) != 0);
        Sts_OoRGate.UpdateData((data[offset + 317] & (1 << 7)) != 0);
        Sts_OoR.UpdateData((data[offset + 318] & (1 << 0)) != 0);
        Sts_Fail.UpdateData((data[offset + 318] & (1 << 1)) != 0);
        Sts_RdyAck.UpdateData((data[offset + 318] & (1 << 2)) != 0);
        Sts_RdyReset.UpdateData((data[offset + 318] & (1 << 3)) != 0);
        XRdy_ClearCapt.UpdateData((data[offset + 318] & (1 << 4)) != 0);
        XRdy_Reset.UpdateData((data[offset + 318] & (1 << 5)) != 0);
        XRdy_ResetAckAll.UpdateData((data[offset + 318] & (1 << 6)) != 0);
        Val_Owner.UpdateData(data, offset + 318);
        
        return offset + GetSize();
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