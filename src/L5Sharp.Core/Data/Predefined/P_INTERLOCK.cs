using System.Xml.Linq;
// Auto-generated file
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>P_INTERLOCK</c> data type structure.
/// </summary>
[LogixData("P_INTERLOCK")]
public sealed partial class P_INTERLOCK : StructureData
{
    /// <summary>
    /// Creates a new <see cref="P_INTERLOCK"/> instance initialized with default values.
    /// </summary>
    public P_INTERLOCK() : base("P_INTERLOCK")
    {
        EnableIn = new BOOL();
        EnableOut = new BOOL();
        Inp_InitializeReq = new BOOL();
        Inp_Intlk00 = new BOOL();
        Inp_Intlk01 = new BOOL();
        Inp_Intlk02 = new BOOL();
        Inp_Intlk03 = new BOOL();
        Inp_Intlk04 = new BOOL();
        Inp_Intlk05 = new BOOL();
        Inp_Intlk06 = new BOOL();
        Inp_Intlk07 = new BOOL();
        Inp_Intlk08 = new BOOL();
        Inp_Intlk09 = new BOOL();
        Inp_Intlk10 = new BOOL();
        Inp_Intlk11 = new BOOL();
        Inp_Intlk12 = new BOOL();
        Inp_Intlk13 = new BOOL();
        Inp_Intlk14 = new BOOL();
        Inp_Intlk15 = new BOOL();
        Inp_Intlk16 = new BOOL();
        Inp_Intlk17 = new BOOL();
        Inp_Intlk18 = new BOOL();
        Inp_Intlk19 = new BOOL();
        Inp_Intlk20 = new BOOL();
        Inp_Intlk21 = new BOOL();
        Inp_Intlk22 = new BOOL();
        Inp_Intlk23 = new BOOL();
        Inp_Intlk24 = new BOOL();
        Inp_Intlk25 = new BOOL();
        Inp_Intlk26 = new BOOL();
        Inp_Intlk27 = new BOOL();
        Inp_Intlk28 = new BOOL();
        Inp_Intlk29 = new BOOL();
        Inp_Intlk30 = new BOOL();
        Inp_Intlk31 = new BOOL();
        Inp_IOFault = new DINT();
        Inp_Available = new BOOL();
        Inp_BypActive = new BOOL();
        Inp_LatchDefeat = new BOOL();
        Inp_Reset = new BOOL();
        Cfg_OKState = new DINT();
        Cfg_Latched = new DINT();
        Cfg_StopOnly = new DINT();
        Cfg_Bypassable = new DINT();
        Cfg_HasNav = new DINT();
        Cfg_eType00 = new SINT();
        Cfg_eType01 = new SINT();
        Cfg_eType02 = new SINT();
        Cfg_eType03 = new SINT();
        Cfg_eType04 = new SINT();
        Cfg_eType05 = new SINT();
        Cfg_eType06 = new SINT();
        Cfg_eType07 = new SINT();
        Cfg_eType08 = new SINT();
        Cfg_eType09 = new SINT();
        Cfg_eType10 = new SINT();
        Cfg_eType11 = new SINT();
        Cfg_eType12 = new SINT();
        Cfg_eType13 = new SINT();
        Cfg_eType14 = new SINT();
        Cfg_eType15 = new SINT();
        Cfg_eType16 = new SINT();
        Cfg_eType17 = new SINT();
        Cfg_eType18 = new SINT();
        Cfg_eType19 = new SINT();
        Cfg_eType20 = new SINT();
        Cfg_eType21 = new SINT();
        Cfg_eType22 = new SINT();
        Cfg_eType23 = new SINT();
        Cfg_eType24 = new SINT();
        Cfg_eType25 = new SINT();
        Cfg_eType26 = new SINT();
        Cfg_eType27 = new SINT();
        Cfg_eType28 = new SINT();
        Cfg_eType29 = new SINT();
        Cfg_eType30 = new SINT();
        Cfg_eType31 = new SINT();
        Cfg_HasType = new SINT();
        Cfg_TypeDesc00 = new BOOL();
        Cfg_TypeDesc01 = new BOOL();
        Cfg_TypeDesc02 = new BOOL();
        Cfg_TypeDesc03 = new BOOL();
        Cfg_TypeDesc04 = new BOOL();
        Cfg_TypeDesc05 = new BOOL();
        Cfg_TypeDesc06 = new BOOL();
        Cfg_TypeDesc07 = new BOOL();
        Cfg_BankID = new INT();
        Cfg_HasMoreObj = new BOOL();
        Cfg_CnfrmReqd = new SINT();
        PCmd_Reset = new BOOL();
        Out_Reset = new BOOL();
        Val_FirstUpBankID = new INT();
        Val_FirstUpIndex = new INT();
        Sts_Initialized = new BOOL();
        Sts_IntlkOK = new BOOL();
        Sts_NBIntlkOK = new BOOL();
        Sts_Available = new BOOL();
        Sts_IntlkTripInh = new BOOL();
        Sts_BypActive = new BOOL();
        Sts_FirstUpDetect = new BOOL();
        Sts_BankIDError = new BOOL();
        Sts_LatchDefeat = new BOOL();
        Sts_RdyReset = new BOOL();
        Sts_LatchMask = new DINT();
        Sts_BypassMask = new DINT();
        Sts_Intlk = new DINT();
        Sts_FirstOut = new DINT();
    }

    /// <summary>
    /// Creates a new <see cref="P_INTERLOCK"/> instance initialized with the provided element.
    /// </summary>
    public P_INTERLOCK(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_InitializeReq</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public BOOL Inp_InitializeReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Intlk00</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public BOOL Inp_Intlk00
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Intlk01</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public BOOL Inp_Intlk01
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Intlk02</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public BOOL Inp_Intlk02
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Intlk03</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public BOOL Inp_Intlk03
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Intlk04</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public BOOL Inp_Intlk04
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Intlk05</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public BOOL Inp_Intlk05
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Intlk06</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public BOOL Inp_Intlk06
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Intlk07</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public BOOL Inp_Intlk07
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Intlk08</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public BOOL Inp_Intlk08
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Intlk09</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public BOOL Inp_Intlk09
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Intlk10</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public BOOL Inp_Intlk10
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Intlk11</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public BOOL Inp_Intlk11
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Intlk12</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public BOOL Inp_Intlk12
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Intlk13</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public BOOL Inp_Intlk13
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Intlk14</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public BOOL Inp_Intlk14
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Intlk15</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public BOOL Inp_Intlk15
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Intlk16</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public BOOL Inp_Intlk16
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Intlk17</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public BOOL Inp_Intlk17
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Intlk18</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public BOOL Inp_Intlk18
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Intlk19</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public BOOL Inp_Intlk19
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Intlk20</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public BOOL Inp_Intlk20
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Intlk21</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public BOOL Inp_Intlk21
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Intlk22</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public BOOL Inp_Intlk22
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Intlk23</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public BOOL Inp_Intlk23
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Intlk24</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public BOOL Inp_Intlk24
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Intlk25</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public BOOL Inp_Intlk25
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Intlk26</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public BOOL Inp_Intlk26
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Intlk27</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public BOOL Inp_Intlk27
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Intlk28</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public BOOL Inp_Intlk28
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Intlk29</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public BOOL Inp_Intlk29
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Intlk30</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public BOOL Inp_Intlk30
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Intlk31</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public BOOL Inp_Intlk31
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_IOFault</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public DINT Inp_IOFault
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Available</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public BOOL Inp_Available
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_BypActive</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public BOOL Inp_BypActive
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_LatchDefeat</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public BOOL Inp_LatchDefeat
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Reset</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public BOOL Inp_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_OKState</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public DINT Cfg_OKState
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_Latched</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public DINT Cfg_Latched
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_StopOnly</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public DINT Cfg_StopOnly
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_Bypassable</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public DINT Cfg_Bypassable
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasNav</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public DINT Cfg_HasNav
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_eType00</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public SINT Cfg_eType00
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_eType01</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public SINT Cfg_eType01
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_eType02</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public SINT Cfg_eType02
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_eType03</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public SINT Cfg_eType03
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_eType04</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public SINT Cfg_eType04
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_eType05</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public SINT Cfg_eType05
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_eType06</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public SINT Cfg_eType06
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_eType07</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public SINT Cfg_eType07
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_eType08</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public SINT Cfg_eType08
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_eType09</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public SINT Cfg_eType09
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_eType10</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public SINT Cfg_eType10
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_eType11</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public SINT Cfg_eType11
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_eType12</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public SINT Cfg_eType12
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_eType13</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public SINT Cfg_eType13
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_eType14</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public SINT Cfg_eType14
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_eType15</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public SINT Cfg_eType15
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_eType16</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public SINT Cfg_eType16
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_eType17</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public SINT Cfg_eType17
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_eType18</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public SINT Cfg_eType18
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_eType19</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public SINT Cfg_eType19
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_eType20</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public SINT Cfg_eType20
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_eType21</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public SINT Cfg_eType21
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_eType22</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public SINT Cfg_eType22
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_eType23</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public SINT Cfg_eType23
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_eType24</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public SINT Cfg_eType24
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_eType25</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public SINT Cfg_eType25
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_eType26</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public SINT Cfg_eType26
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_eType27</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public SINT Cfg_eType27
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_eType28</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public SINT Cfg_eType28
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_eType29</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public SINT Cfg_eType29
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_eType30</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public SINT Cfg_eType30
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_eType31</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public SINT Cfg_eType31
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasType</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public SINT Cfg_HasType
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_TypeDesc00</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public BOOL Cfg_TypeDesc00
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_TypeDesc01</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public BOOL Cfg_TypeDesc01
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_TypeDesc02</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public BOOL Cfg_TypeDesc02
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_TypeDesc03</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public BOOL Cfg_TypeDesc03
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_TypeDesc04</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public BOOL Cfg_TypeDesc04
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_TypeDesc05</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public BOOL Cfg_TypeDesc05
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_TypeDesc06</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public BOOL Cfg_TypeDesc06
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_TypeDesc07</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public BOOL Cfg_TypeDesc07
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_BankID</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public INT Cfg_BankID
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasMoreObj</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public BOOL Cfg_HasMoreObj
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CnfrmReqd</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public SINT Cfg_CnfrmReqd
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PCmd_Reset</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public BOOL PCmd_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out_Reset</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public BOOL Out_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_FirstUpBankID</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public INT Val_FirstUpBankID
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_FirstUpIndex</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public INT Val_FirstUpIndex
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Initialized</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public BOOL Sts_Initialized
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_IntlkOK</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public BOOL Sts_IntlkOK
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NBIntlkOK</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public BOOL Sts_NBIntlkOK
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Available</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public BOOL Sts_Available
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_IntlkTripInh</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public BOOL Sts_IntlkTripInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_BypActive</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public BOOL Sts_BypActive
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_FirstUpDetect</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public BOOL Sts_FirstUpDetect
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_BankIDError</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public BOOL Sts_BankIDError
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_LatchDefeat</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public BOOL Sts_LatchDefeat
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_RdyReset</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public BOOL Sts_RdyReset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_LatchMask</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public DINT Sts_LatchMask
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_BypassMask</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public DINT Sts_BypassMask
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Intlk</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public DINT Sts_Intlk
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_FirstOut</c> member of the <see cref="P_INTERLOCK"/> data type.
    /// </summary>
    public DINT Sts_FirstOut
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

}
