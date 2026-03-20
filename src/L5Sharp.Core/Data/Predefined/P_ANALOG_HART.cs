using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>P_ANALOG_HART</c> data type structure.
/// </summary>
[LogixData("P_ANALOG_HART")]
public sealed partial class P_ANALOG_HART : StructureData
{
    /// <summary>
    /// Creates a new <see cref="P_ANALOG_HART"/> instance initialized with default values.
    /// </summary>
    public P_ANALOG_HART() : base("P_ANALOG_HART")
    {
        EnableIn = new BOOL();
        EnableOut = new BOOL();
        Inp_InitializeReq = new BOOL();
        Cfg_HasHARTPV = new BOOL();
        Cfg_HasHARTSV = new BOOL();
        Cfg_HasHARTTV = new BOOL();
        Cfg_HasHARTQV = new BOOL();
        Cfg_UseHARTVarSts = new BOOL();
        Cfg_UseHARTText = new BOOL();
        Cfg_HARTPVDecPlcs = new SINT();
        Cfg_HARTSVDecPlcs = new SINT();
        Cfg_HARTTVDecPlcs = new SINT();
        Cfg_HARTQVDecPlcs = new SINT();
        Cfg_HasMoreObj = new BOOL();
        Cfg_HasNav = new BOOL();
        Set_VirtualHARTPV = new REAL();
        Set_VirtualHARTSV = new REAL();
        Set_VirtualHARTTV = new REAL();
        Set_VirtualHARTQV = new REAL();
        PCmd_Virtual = new BOOL();
        PCmd_Physical = new BOOL();
        Val_HARTPV = new REAL();
        Val_HARTSV = new REAL();
        Val_HARTTV = new REAL();
        Val_HARTQV = new REAL();
        Val_HARTLoopCurrent = new REAL();
        Val_InpRawMinFromHART = new REAL();
        Val_InpRawMaxFromHART = new REAL();
        Val_PVEUMinFromHART = new REAL();
        Val_PVEUMaxFromHART = new REAL();
        Sts_eHARTDiagCode1 = new INT();
        Sts_eHARTDiagCode2 = new INT();
        Sts_eHARTDiagCode3 = new INT();
        Sts_bHARTDiagSts = new SINT();
        Sts_bHARTDiagSts1 = new SINT();
        Sts_bHARTDiagSts2 = new SINT();
        Sts_bHARTDiagSts3 = new SINT();
        Sts_Initialized = new BOOL();
        Sts_Virtual = new BOOL();
        Sts_ConnectionFault = new BOOL();
        Sts_DvcMalfunction = new BOOL();
        Sts_CurrentSaturated = new BOOL();
        Sts_CurrentFixed = new BOOL();
        Sts_CurrentMismatch = new BOOL();
        Sts_DiagnosticActive = new BOOL();
        Val_DiagnosticSeqCount = new SINT();
        SrcQ_IO = new SINT();
        SrcQ = new SINT();
        SrcQ_HARTPV = new SINT();
        SrcQ_HARTSV = new SINT();
        SrcQ_HARTTV = new SINT();
        SrcQ_HARTQV = new SINT();
        SrcQ_HARTLoopCurrent = new SINT();
        Sts_eSts = new SINT();
        Sts_eFault = new INT();
    }
    
    /// <summary>
    /// Creates a new <see cref="P_ANALOG_HART"/> instance initialized with the provided element.
    /// </summary>
    public P_ANALOG_HART(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 404;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        EnableIn.UpdateData((data[offset + 5] & (1 << 0)) != 0);
        EnableOut.UpdateData((data[offset + 5] & (1 << 1)) != 0);
        Inp_InitializeReq.UpdateData((data[offset + 5] & (1 << 2)) != 0);
        Cfg_HasHARTPV.UpdateData((data[offset + 5] & (1 << 3)) != 0);
        Cfg_HasHARTSV.UpdateData((data[offset + 5] & (1 << 4)) != 0);
        Cfg_HasHARTTV.UpdateData((data[offset + 5] & (1 << 5)) != 0);
        Cfg_HasHARTQV.UpdateData((data[offset + 5] & (1 << 6)) != 0);
        Cfg_UseHARTVarSts.UpdateData((data[offset + 5] & (1 << 7)) != 0);
        Cfg_UseHARTText.UpdateData((data[offset + 6] & (1 << 0)) != 0);
        Cfg_HARTPVDecPlcs.UpdateData(data, offset + 6);
        Cfg_HARTSVDecPlcs.UpdateData(data, offset + 7);
        Cfg_HARTTVDecPlcs.UpdateData(data, offset + 8);
        Cfg_HARTQVDecPlcs.UpdateData(data, offset + 9);
        Cfg_HasMoreObj.UpdateData((data[offset + 10] & (1 << 1)) != 0);
        Cfg_HasNav.UpdateData((data[offset + 10] & (1 << 2)) != 0);
        Set_VirtualHARTPV.UpdateData(data, offset + 10);
        Set_VirtualHARTSV.UpdateData(data, offset + 14);
        Set_VirtualHARTTV.UpdateData(data, offset + 18);
        Set_VirtualHARTQV.UpdateData(data, offset + 22);
        PCmd_Virtual.UpdateData((data[offset + 26] & (1 << 3)) != 0);
        PCmd_Physical.UpdateData((data[offset + 26] & (1 << 4)) != 0);
        Val_HARTPV.UpdateData(data, offset + 26);
        Val_HARTSV.UpdateData(data, offset + 30);
        Val_HARTTV.UpdateData(data, offset + 34);
        Val_HARTQV.UpdateData(data, offset + 38);
        Val_HARTLoopCurrent.UpdateData(data, offset + 42);
        Val_InpRawMinFromHART.UpdateData(data, offset + 46);
        Val_InpRawMaxFromHART.UpdateData(data, offset + 50);
        Val_PVEUMinFromHART.UpdateData(data, offset + 54);
        Val_PVEUMaxFromHART.UpdateData(data, offset + 58);
        Sts_eHARTDiagCode1.UpdateData(data, offset + 62);
        Sts_eHARTDiagCode2.UpdateData(data, offset + 64);
        Sts_eHARTDiagCode3.UpdateData(data, offset + 66);
        Sts_bHARTDiagSts.UpdateData(data, offset + 68);
        Sts_bHARTDiagSts1.UpdateData(data, offset + 69);
        Sts_bHARTDiagSts2.UpdateData(data, offset + 70);
        Sts_bHARTDiagSts3.UpdateData(data, offset + 71);
        Sts_Initialized.UpdateData((data[offset + 72] & (1 << 5)) != 0);
        Sts_Virtual.UpdateData((data[offset + 72] & (1 << 6)) != 0);
        Sts_ConnectionFault.UpdateData((data[offset + 72] & (1 << 7)) != 0);
        Sts_DvcMalfunction.UpdateData((data[offset + 73] & (1 << 0)) != 0);
        Sts_CurrentSaturated.UpdateData((data[offset + 73] & (1 << 1)) != 0);
        Sts_CurrentFixed.UpdateData((data[offset + 73] & (1 << 2)) != 0);
        Sts_CurrentMismatch.UpdateData((data[offset + 73] & (1 << 3)) != 0);
        Sts_DiagnosticActive.UpdateData((data[offset + 73] & (1 << 4)) != 0);
        Val_DiagnosticSeqCount.UpdateData(data, offset + 73);
        SrcQ_IO.UpdateData(data, offset + 74);
        SrcQ.UpdateData(data, offset + 75);
        SrcQ_HARTPV.UpdateData(data, offset + 76);
        SrcQ_HARTSV.UpdateData(data, offset + 77);
        SrcQ_HARTTV.UpdateData(data, offset + 78);
        SrcQ_HARTQV.UpdateData(data, offset + 79);
        SrcQ_HARTLoopCurrent.UpdateData(data, offset + 80);
        Sts_eSts.UpdateData(data, offset + 81);
        Sts_eFault.UpdateData(data, offset + 82);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="P_ANALOG_HART"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="P_ANALOG_HART"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_InitializeReq</c> member of the <see cref="P_ANALOG_HART"/> data type.
    /// </summary>
    public BOOL Inp_InitializeReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasHARTPV</c> member of the <see cref="P_ANALOG_HART"/> data type.
    /// </summary>
    public BOOL Cfg_HasHARTPV
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasHARTSV</c> member of the <see cref="P_ANALOG_HART"/> data type.
    /// </summary>
    public BOOL Cfg_HasHARTSV
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasHARTTV</c> member of the <see cref="P_ANALOG_HART"/> data type.
    /// </summary>
    public BOOL Cfg_HasHARTTV
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasHARTQV</c> member of the <see cref="P_ANALOG_HART"/> data type.
    /// </summary>
    public BOOL Cfg_HasHARTQV
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_UseHARTVarSts</c> member of the <see cref="P_ANALOG_HART"/> data type.
    /// </summary>
    public BOOL Cfg_UseHARTVarSts
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_UseHARTText</c> member of the <see cref="P_ANALOG_HART"/> data type.
    /// </summary>
    public BOOL Cfg_UseHARTText
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HARTPVDecPlcs</c> member of the <see cref="P_ANALOG_HART"/> data type.
    /// </summary>
    public SINT Cfg_HARTPVDecPlcs
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HARTSVDecPlcs</c> member of the <see cref="P_ANALOG_HART"/> data type.
    /// </summary>
    public SINT Cfg_HARTSVDecPlcs
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HARTTVDecPlcs</c> member of the <see cref="P_ANALOG_HART"/> data type.
    /// </summary>
    public SINT Cfg_HARTTVDecPlcs
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HARTQVDecPlcs</c> member of the <see cref="P_ANALOG_HART"/> data type.
    /// </summary>
    public SINT Cfg_HARTQVDecPlcs
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasMoreObj</c> member of the <see cref="P_ANALOG_HART"/> data type.
    /// </summary>
    public BOOL Cfg_HasMoreObj
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasNav</c> member of the <see cref="P_ANALOG_HART"/> data type.
    /// </summary>
    public BOOL Cfg_HasNav
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Set_VirtualHARTPV</c> member of the <see cref="P_ANALOG_HART"/> data type.
    /// </summary>
    public REAL Set_VirtualHARTPV
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Set_VirtualHARTSV</c> member of the <see cref="P_ANALOG_HART"/> data type.
    /// </summary>
    public REAL Set_VirtualHARTSV
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Set_VirtualHARTTV</c> member of the <see cref="P_ANALOG_HART"/> data type.
    /// </summary>
    public REAL Set_VirtualHARTTV
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Set_VirtualHARTQV</c> member of the <see cref="P_ANALOG_HART"/> data type.
    /// </summary>
    public REAL Set_VirtualHARTQV
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Virtual</c> member of the <see cref="P_ANALOG_HART"/> data type.
    /// </summary>
    public BOOL PCmd_Virtual
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Physical</c> member of the <see cref="P_ANALOG_HART"/> data type.
    /// </summary>
    public BOOL PCmd_Physical
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_HARTPV</c> member of the <see cref="P_ANALOG_HART"/> data type.
    /// </summary>
    public REAL Val_HARTPV
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_HARTSV</c> member of the <see cref="P_ANALOG_HART"/> data type.
    /// </summary>
    public REAL Val_HARTSV
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_HARTTV</c> member of the <see cref="P_ANALOG_HART"/> data type.
    /// </summary>
    public REAL Val_HARTTV
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_HARTQV</c> member of the <see cref="P_ANALOG_HART"/> data type.
    /// </summary>
    public REAL Val_HARTQV
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_HARTLoopCurrent</c> member of the <see cref="P_ANALOG_HART"/> data type.
    /// </summary>
    public REAL Val_HARTLoopCurrent
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_InpRawMinFromHART</c> member of the <see cref="P_ANALOG_HART"/> data type.
    /// </summary>
    public REAL Val_InpRawMinFromHART
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_InpRawMaxFromHART</c> member of the <see cref="P_ANALOG_HART"/> data type.
    /// </summary>
    public REAL Val_InpRawMaxFromHART
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_PVEUMinFromHART</c> member of the <see cref="P_ANALOG_HART"/> data type.
    /// </summary>
    public REAL Val_PVEUMinFromHART
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_PVEUMaxFromHART</c> member of the <see cref="P_ANALOG_HART"/> data type.
    /// </summary>
    public REAL Val_PVEUMaxFromHART
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eHARTDiagCode1</c> member of the <see cref="P_ANALOG_HART"/> data type.
    /// </summary>
    public INT Sts_eHARTDiagCode1
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eHARTDiagCode2</c> member of the <see cref="P_ANALOG_HART"/> data type.
    /// </summary>
    public INT Sts_eHARTDiagCode2
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eHARTDiagCode3</c> member of the <see cref="P_ANALOG_HART"/> data type.
    /// </summary>
    public INT Sts_eHARTDiagCode3
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_bHARTDiagSts</c> member of the <see cref="P_ANALOG_HART"/> data type.
    /// </summary>
    public SINT Sts_bHARTDiagSts
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_bHARTDiagSts1</c> member of the <see cref="P_ANALOG_HART"/> data type.
    /// </summary>
    public SINT Sts_bHARTDiagSts1
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_bHARTDiagSts2</c> member of the <see cref="P_ANALOG_HART"/> data type.
    /// </summary>
    public SINT Sts_bHARTDiagSts2
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_bHARTDiagSts3</c> member of the <see cref="P_ANALOG_HART"/> data type.
    /// </summary>
    public SINT Sts_bHARTDiagSts3
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Initialized</c> member of the <see cref="P_ANALOG_HART"/> data type.
    /// </summary>
    public BOOL Sts_Initialized
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Virtual</c> member of the <see cref="P_ANALOG_HART"/> data type.
    /// </summary>
    public BOOL Sts_Virtual
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_ConnectionFault</c> member of the <see cref="P_ANALOG_HART"/> data type.
    /// </summary>
    public BOOL Sts_ConnectionFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_DvcMalfunction</c> member of the <see cref="P_ANALOG_HART"/> data type.
    /// </summary>
    public BOOL Sts_DvcMalfunction
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_CurrentSaturated</c> member of the <see cref="P_ANALOG_HART"/> data type.
    /// </summary>
    public BOOL Sts_CurrentSaturated
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_CurrentFixed</c> member of the <see cref="P_ANALOG_HART"/> data type.
    /// </summary>
    public BOOL Sts_CurrentFixed
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_CurrentMismatch</c> member of the <see cref="P_ANALOG_HART"/> data type.
    /// </summary>
    public BOOL Sts_CurrentMismatch
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_DiagnosticActive</c> member of the <see cref="P_ANALOG_HART"/> data type.
    /// </summary>
    public BOOL Sts_DiagnosticActive
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_DiagnosticSeqCount</c> member of the <see cref="P_ANALOG_HART"/> data type.
    /// </summary>
    public SINT Val_DiagnosticSeqCount
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SrcQ_IO</c> member of the <see cref="P_ANALOG_HART"/> data type.
    /// </summary>
    public SINT SrcQ_IO
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SrcQ</c> member of the <see cref="P_ANALOG_HART"/> data type.
    /// </summary>
    public SINT SrcQ
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SrcQ_HARTPV</c> member of the <see cref="P_ANALOG_HART"/> data type.
    /// </summary>
    public SINT SrcQ_HARTPV
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SrcQ_HARTSV</c> member of the <see cref="P_ANALOG_HART"/> data type.
    /// </summary>
    public SINT SrcQ_HARTSV
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SrcQ_HARTTV</c> member of the <see cref="P_ANALOG_HART"/> data type.
    /// </summary>
    public SINT SrcQ_HARTTV
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SrcQ_HARTQV</c> member of the <see cref="P_ANALOG_HART"/> data type.
    /// </summary>
    public SINT SrcQ_HARTQV
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SrcQ_HARTLoopCurrent</c> member of the <see cref="P_ANALOG_HART"/> data type.
    /// </summary>
    public SINT SrcQ_HARTLoopCurrent
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eSts</c> member of the <see cref="P_ANALOG_HART"/> data type.
    /// </summary>
    public SINT Sts_eSts
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_eFault</c> member of the <see cref="P_ANALOG_HART"/> data type.
    /// </summary>
    public INT Sts_eFault
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }
}