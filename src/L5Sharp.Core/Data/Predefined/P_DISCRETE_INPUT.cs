using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>P_DISCRETE_INPUT</c> data type structure.
/// </summary>
[LogixData("P_DISCRETE_INPUT")]
public sealed partial class P_DISCRETE_INPUT : StructureData
{
    /// <summary>
    /// Creates a new <see cref="P_DISCRETE_INPUT"/> instance initialized with default values.
    /// </summary>
    public P_DISCRETE_INPUT() : base("P_DISCRETE_INPUT")
    {
        EnableIn = new BOOL();
        EnableOut = new BOOL();
        Inp_InitializeReq = new BOOL();
        Inp_PVData = new BOOL();
        Inp_ModFault = new BOOL();
        Inp_ChanFault = new BOOL();
        Inp_PVUncertain = new BOOL();
        Inp_PVNotify = new SINT();
        Inp_Target = new BOOL();
        Inp_Gate = new BOOL();
        Inp_Reset = new BOOL();
        Cfg_AllowDisable = new BOOL();
        Cfg_AllowShelve = new BOOL();
        Cfg_NoSubstPV = new BOOL();
        Cfg_SubstTracksTarget = new BOOL();
        Cfg_NormTextVis = new BOOL();
        Cfg_HasMoreObj = new BOOL();
        Cfg_Debounce = new REAL();
        Cfg_GateDly = new REAL();
        Cfg_TgtDisagreeOffDly = new REAL();
        Cfg_TgtDisagreeOnDly = new REAL();
        Cfg_CnfrmReqd = new SINT();
        Set_VirtualPV = new BOOL();
        PCmd_Virtual = new BOOL();
        PCmd_Physical = new BOOL();
        PCmd_Reset = new BOOL();
        XCmd_Reset = new BOOL();
        XCmd_ResetAckAll = new BOOL();
        Out = new BOOL();
        Out_InpPV = new BOOL();
        Out_Reset = new BOOL();
        Sts_Initialized = new BOOL();
        Sts_PVUncertain = new BOOL();
        Sts_SubstPV = new BOOL();
        Sts_InpPV = new BOOL();
        Sts_Virtual = new BOOL();
        SrcQ_IO = new SINT();
        SrcQ = new SINT();
        Sts_eSts = new SINT();
        Sts_eFault = new SINT();
        Sts_eNotify = new SINT();
        Sts_eNotifyAll = new SINT();
        Sts_eNotifyIOFault = new SINT();
        Sts_eNotifyTgtDisagree = new SINT();
        Sts_UnackAlmCount = new DINT();
        Sts_MaintByp = new BOOL();
        Sts_Err = new BOOL();
        Sts_ErrGateDly = new BOOL();
        Sts_ErrTgtDisagreeOffDly = new BOOL();
        Sts_ErrTgtDisagreeOnDly = new BOOL();
        Sts_ErrDebounce = new BOOL();
        Sts_ErrAlm = new BOOL();
        Sts_Alm = new BOOL();
        Sts_AlmInh = new BOOL();
        Sts_IOFault = new BOOL();
        Sts_TgtDisagreeCmp = new BOOL();
        Sts_TgtDisagreeGate = new BOOL();
        Sts_TgtDisagree = new BOOL();
        Sts_RdyAck = new BOOL();
        Sts_RdyReset = new BOOL();
        XRdy_Reset = new BOOL();
        XRdy_ResetAckAll = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="P_DISCRETE_INPUT"/> instance initialized with the provided element.
    /// </summary>
    public P_DISCRETE_INPUT(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 168;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        EnableIn.UpdateData((data[offset + 5] & (1 << 0)) != 0);
        EnableOut.UpdateData((data[offset + 5] & (1 << 1)) != 0);
        Inp_InitializeReq.UpdateData((data[offset + 5] & (1 << 2)) != 0);
        Inp_PVData.UpdateData((data[offset + 5] & (1 << 3)) != 0);
        Inp_ModFault.UpdateData((data[offset + 5] & (1 << 4)) != 0);
        Inp_ChanFault.UpdateData((data[offset + 5] & (1 << 5)) != 0);
        Inp_PVUncertain.UpdateData((data[offset + 5] & (1 << 6)) != 0);
        Inp_PVNotify.UpdateData(data, offset + 5);
        Inp_Target.UpdateData((data[offset + 6] & (1 << 7)) != 0);
        Inp_Gate.UpdateData((data[offset + 7] & (1 << 0)) != 0);
        Inp_Reset.UpdateData((data[offset + 7] & (1 << 1)) != 0);
        Cfg_AllowDisable.UpdateData((data[offset + 7] & (1 << 2)) != 0);
        Cfg_AllowShelve.UpdateData((data[offset + 7] & (1 << 3)) != 0);
        Cfg_NoSubstPV.UpdateData((data[offset + 7] & (1 << 4)) != 0);
        Cfg_SubstTracksTarget.UpdateData((data[offset + 7] & (1 << 5)) != 0);
        Cfg_NormTextVis.UpdateData((data[offset + 7] & (1 << 6)) != 0);
        Cfg_HasMoreObj.UpdateData((data[offset + 7] & (1 << 7)) != 0);
        Cfg_Debounce.UpdateData(data, offset + 7);
        Cfg_GateDly.UpdateData(data, offset + 11);
        Cfg_TgtDisagreeOffDly.UpdateData(data, offset + 15);
        Cfg_TgtDisagreeOnDly.UpdateData(data, offset + 19);
        Cfg_CnfrmReqd.UpdateData(data, offset + 23);
        Set_VirtualPV.UpdateData((data[offset + 25] & (1 << 0)) != 0);
        PCmd_Virtual.UpdateData((data[offset + 25] & (1 << 1)) != 0);
        PCmd_Physical.UpdateData((data[offset + 25] & (1 << 2)) != 0);
        PCmd_Reset.UpdateData((data[offset + 25] & (1 << 3)) != 0);
        XCmd_Reset.UpdateData((data[offset + 25] & (1 << 4)) != 0);
        XCmd_ResetAckAll.UpdateData((data[offset + 25] & (1 << 5)) != 0);
        Out.UpdateData((data[offset + 25] & (1 << 6)) != 0);
        Out_InpPV.UpdateData((data[offset + 25] & (1 << 7)) != 0);
        Out_Reset.UpdateData((data[offset + 26] & (1 << 0)) != 0);
        Sts_Initialized.UpdateData((data[offset + 26] & (1 << 1)) != 0);
        Sts_PVUncertain.UpdateData((data[offset + 26] & (1 << 2)) != 0);
        Sts_SubstPV.UpdateData((data[offset + 26] & (1 << 3)) != 0);
        Sts_InpPV.UpdateData((data[offset + 26] & (1 << 4)) != 0);
        Sts_Virtual.UpdateData((data[offset + 26] & (1 << 5)) != 0);
        SrcQ_IO.UpdateData(data, offset + 26);
        SrcQ.UpdateData(data, offset + 27);
        Sts_eSts.UpdateData(data, offset + 28);
        Sts_eFault.UpdateData(data, offset + 29);
        Sts_eNotify.UpdateData(data, offset + 30);
        Sts_eNotifyAll.UpdateData(data, offset + 31);
        Sts_eNotifyIOFault.UpdateData(data, offset + 32);
        Sts_eNotifyTgtDisagree.UpdateData(data, offset + 33);
        Sts_UnackAlmCount.UpdateData(data, offset + 34);
        Sts_MaintByp.UpdateData((data[offset + 38] & (1 << 6)) != 0);
        Sts_Err.UpdateData((data[offset + 38] & (1 << 7)) != 0);
        Sts_ErrGateDly.UpdateData((data[offset + 43] & (1 << 0)) != 0);
        Sts_ErrTgtDisagreeOffDly.UpdateData((data[offset + 43] & (1 << 1)) != 0);
        Sts_ErrTgtDisagreeOnDly.UpdateData((data[offset + 43] & (1 << 2)) != 0);
        Sts_ErrDebounce.UpdateData((data[offset + 43] & (1 << 3)) != 0);
        Sts_ErrAlm.UpdateData((data[offset + 43] & (1 << 4)) != 0);
        Sts_Alm.UpdateData((data[offset + 43] & (1 << 5)) != 0);
        Sts_AlmInh.UpdateData((data[offset + 43] & (1 << 6)) != 0);
        Sts_IOFault.UpdateData((data[offset + 43] & (1 << 7)) != 0);
        Sts_TgtDisagreeCmp.UpdateData((data[offset + 44] & (1 << 0)) != 0);
        Sts_TgtDisagreeGate.UpdateData((data[offset + 44] & (1 << 1)) != 0);
        Sts_TgtDisagree.UpdateData((data[offset + 44] & (1 << 2)) != 0);
        Sts_RdyAck.UpdateData((data[offset + 44] & (1 << 3)) != 0);
        Sts_RdyReset.UpdateData((data[offset + 44] & (1 << 4)) != 0);
        XRdy_Reset.UpdateData((data[offset + 44] & (1 << 5)) != 0);
        XRdy_ResetAckAll.UpdateData((data[offset + 44] & (1 << 6)) != 0);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_InitializeReq</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public BOOL Inp_InitializeReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVData</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public BOOL Inp_PVData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_ModFault</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public BOOL Inp_ModFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_ChanFault</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public BOOL Inp_ChanFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVUncertain</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public BOOL Inp_PVUncertain
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_PVNotify</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public SINT Inp_PVNotify
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Target</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public BOOL Inp_Target
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Gate</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public BOOL Inp_Gate
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Reset</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public BOOL Inp_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_AllowDisable</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public BOOL Cfg_AllowDisable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_AllowShelve</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public BOOL Cfg_AllowShelve
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_NoSubstPV</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public BOOL Cfg_NoSubstPV
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_SubstTracksTarget</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public BOOL Cfg_SubstTracksTarget
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_NormTextVis</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public BOOL Cfg_NormTextVis
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasMoreObj</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public BOOL Cfg_HasMoreObj
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_Debounce</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public REAL Cfg_Debounce
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_GateDly</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public REAL Cfg_GateDly
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_TgtDisagreeOffDly</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public REAL Cfg_TgtDisagreeOffDly
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_TgtDisagreeOnDly</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public REAL Cfg_TgtDisagreeOnDly
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CnfrmReqd</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public SINT Cfg_CnfrmReqd
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Set_VirtualPV</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public BOOL Set_VirtualPV
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Virtual</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public BOOL PCmd_Virtual
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Physical</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public BOOL PCmd_Physical
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Reset</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public BOOL PCmd_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_Reset</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public BOOL XCmd_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XCmd_ResetAckAll</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public BOOL XCmd_ResetAckAll
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public BOOL Out
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_InpPV</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public BOOL Out_InpPV
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_Reset</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public BOOL Out_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Initialized</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_Initialized
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_PVUncertain</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_PVUncertain
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_SubstPV</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_SubstPV
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_InpPV</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_InpPV
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Virtual</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_Virtual
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SrcQ_IO</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public SINT SrcQ_IO
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SrcQ</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public SINT SrcQ
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eSts</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public SINT Sts_eSts
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eFault</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public SINT Sts_eFault
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotify</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public SINT Sts_eNotify
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyAll</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public SINT Sts_eNotifyAll
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyIOFault</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public SINT Sts_eNotifyIOFault
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eNotifyTgtDisagree</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public SINT Sts_eNotifyTgtDisagree
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_UnackAlmCount</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public DINT Sts_UnackAlmCount
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_MaintByp</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_MaintByp
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Err</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_Err
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrGateDly</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrGateDly
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrTgtDisagreeOffDly</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrTgtDisagreeOffDly
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrTgtDisagreeOnDly</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrTgtDisagreeOnDly
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrDebounce</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrDebounce
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ErrAlm</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_ErrAlm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Alm</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_Alm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_AlmInh</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_AlmInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_IOFault</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_IOFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_TgtDisagreeCmp</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_TgtDisagreeCmp
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_TgtDisagreeGate</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_TgtDisagreeGate
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_TgtDisagree</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_TgtDisagree
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_RdyAck</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_RdyAck
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_RdyReset</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public BOOL Sts_RdyReset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_Reset</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public BOOL XRdy_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XRdy_ResetAckAll</c> member of the <see cref="P_DISCRETE_INPUT"/> data type.
    /// </summary>
    public BOOL XRdy_ResetAckAll
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}