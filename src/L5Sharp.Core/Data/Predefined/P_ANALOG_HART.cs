using System.Xml.Linq;
// Auto-generated file
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart

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
