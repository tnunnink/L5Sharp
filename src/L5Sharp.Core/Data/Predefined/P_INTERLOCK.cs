using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

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
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 200;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        EnableIn.UpdateData((data[offset + 5] & (1 << 0)) != 0);
        EnableOut.UpdateData((data[offset + 5] & (1 << 1)) != 0);
        Inp_InitializeReq.UpdateData((data[offset + 5] & (1 << 2)) != 0);
        Inp_Intlk00.UpdateData((data[offset + 5] & (1 << 3)) != 0);
        Inp_Intlk01.UpdateData((data[offset + 5] & (1 << 4)) != 0);
        Inp_Intlk02.UpdateData((data[offset + 5] & (1 << 5)) != 0);
        Inp_Intlk03.UpdateData((data[offset + 5] & (1 << 6)) != 0);
        Inp_Intlk04.UpdateData((data[offset + 5] & (1 << 7)) != 0);
        Inp_Intlk05.UpdateData((data[offset + 6] & (1 << 0)) != 0);
        Inp_Intlk06.UpdateData((data[offset + 6] & (1 << 1)) != 0);
        Inp_Intlk07.UpdateData((data[offset + 6] & (1 << 2)) != 0);
        Inp_Intlk08.UpdateData((data[offset + 6] & (1 << 3)) != 0);
        Inp_Intlk09.UpdateData((data[offset + 6] & (1 << 4)) != 0);
        Inp_Intlk10.UpdateData((data[offset + 6] & (1 << 5)) != 0);
        Inp_Intlk11.UpdateData((data[offset + 6] & (1 << 6)) != 0);
        Inp_Intlk12.UpdateData((data[offset + 6] & (1 << 7)) != 0);
        Inp_Intlk13.UpdateData((data[offset + 7] & (1 << 0)) != 0);
        Inp_Intlk14.UpdateData((data[offset + 7] & (1 << 1)) != 0);
        Inp_Intlk15.UpdateData((data[offset + 7] & (1 << 2)) != 0);
        Inp_Intlk16.UpdateData((data[offset + 7] & (1 << 3)) != 0);
        Inp_Intlk17.UpdateData((data[offset + 7] & (1 << 4)) != 0);
        Inp_Intlk18.UpdateData((data[offset + 7] & (1 << 5)) != 0);
        Inp_Intlk19.UpdateData((data[offset + 7] & (1 << 6)) != 0);
        Inp_Intlk20.UpdateData((data[offset + 7] & (1 << 7)) != 0);
        Inp_Intlk21.UpdateData((data[offset + 8] & (1 << 0)) != 0);
        Inp_Intlk22.UpdateData((data[offset + 8] & (1 << 1)) != 0);
        Inp_Intlk23.UpdateData((data[offset + 8] & (1 << 2)) != 0);
        Inp_Intlk24.UpdateData((data[offset + 8] & (1 << 3)) != 0);
        Inp_Intlk25.UpdateData((data[offset + 8] & (1 << 4)) != 0);
        Inp_Intlk26.UpdateData((data[offset + 8] & (1 << 5)) != 0);
        Inp_Intlk27.UpdateData((data[offset + 8] & (1 << 6)) != 0);
        Inp_Intlk28.UpdateData((data[offset + 8] & (1 << 7)) != 0);
        Inp_Intlk29.UpdateData((data[offset + 9] & (1 << 0)) != 0);
        Inp_Intlk30.UpdateData((data[offset + 9] & (1 << 1)) != 0);
        Inp_Intlk31.UpdateData((data[offset + 9] & (1 << 2)) != 0);
        Inp_IOFault.UpdateData(data, offset + 9);
        Inp_Available.UpdateData((data[offset + 13] & (1 << 3)) != 0);
        Inp_BypActive.UpdateData((data[offset + 13] & (1 << 4)) != 0);
        Inp_LatchDefeat.UpdateData((data[offset + 13] & (1 << 5)) != 0);
        Inp_Reset.UpdateData((data[offset + 13] & (1 << 6)) != 0);
        Cfg_OKState.UpdateData(data, offset + 13);
        Cfg_Latched.UpdateData(data, offset + 17);
        Cfg_StopOnly.UpdateData(data, offset + 21);
        Cfg_Bypassable.UpdateData(data, offset + 25);
        Cfg_HasNav.UpdateData(data, offset + 29);
        Cfg_eType00.UpdateData(data, offset + 33);
        Cfg_eType01.UpdateData(data, offset + 34);
        Cfg_eType02.UpdateData(data, offset + 35);
        Cfg_eType03.UpdateData(data, offset + 36);
        Cfg_eType04.UpdateData(data, offset + 37);
        Cfg_eType05.UpdateData(data, offset + 38);
        Cfg_eType06.UpdateData(data, offset + 39);
        Cfg_eType07.UpdateData(data, offset + 40);
        Cfg_eType08.UpdateData(data, offset + 41);
        Cfg_eType09.UpdateData(data, offset + 42);
        Cfg_eType10.UpdateData(data, offset + 43);
        Cfg_eType11.UpdateData(data, offset + 44);
        Cfg_eType12.UpdateData(data, offset + 45);
        Cfg_eType13.UpdateData(data, offset + 46);
        Cfg_eType14.UpdateData(data, offset + 47);
        Cfg_eType15.UpdateData(data, offset + 48);
        Cfg_eType16.UpdateData(data, offset + 49);
        Cfg_eType17.UpdateData(data, offset + 50);
        Cfg_eType18.UpdateData(data, offset + 51);
        Cfg_eType19.UpdateData(data, offset + 52);
        Cfg_eType20.UpdateData(data, offset + 53);
        Cfg_eType21.UpdateData(data, offset + 54);
        Cfg_eType22.UpdateData(data, offset + 55);
        Cfg_eType23.UpdateData(data, offset + 56);
        Cfg_eType24.UpdateData(data, offset + 57);
        Cfg_eType25.UpdateData(data, offset + 58);
        Cfg_eType26.UpdateData(data, offset + 59);
        Cfg_eType27.UpdateData(data, offset + 60);
        Cfg_eType28.UpdateData(data, offset + 61);
        Cfg_eType29.UpdateData(data, offset + 62);
        Cfg_eType30.UpdateData(data, offset + 63);
        Cfg_eType31.UpdateData(data, offset + 64);
        Cfg_HasType.UpdateData(data, offset + 65);
        Cfg_TypeDesc00.UpdateData((data[offset + 66] & (1 << 7)) != 0);
        Cfg_TypeDesc01.UpdateData((data[offset + 67] & (1 << 0)) != 0);
        Cfg_TypeDesc02.UpdateData((data[offset + 67] & (1 << 1)) != 0);
        Cfg_TypeDesc03.UpdateData((data[offset + 67] & (1 << 2)) != 0);
        Cfg_TypeDesc04.UpdateData((data[offset + 67] & (1 << 3)) != 0);
        Cfg_TypeDesc05.UpdateData((data[offset + 67] & (1 << 4)) != 0);
        Cfg_TypeDesc06.UpdateData((data[offset + 67] & (1 << 5)) != 0);
        Cfg_TypeDesc07.UpdateData((data[offset + 67] & (1 << 6)) != 0);
        Cfg_BankID.UpdateData(data, offset + 67);
        Cfg_HasMoreObj.UpdateData((data[offset + 69] & (1 << 7)) != 0);
        Cfg_CnfrmReqd.UpdateData(data, offset + 69);
        PCmd_Reset.UpdateData((data[offset + 71] & (1 << 0)) != 0);
        Out_Reset.UpdateData((data[offset + 71] & (1 << 1)) != 0);
        Val_FirstUpBankID.UpdateData(data, offset + 71);
        Val_FirstUpIndex.UpdateData(data, offset + 73);
        Sts_Initialized.UpdateData((data[offset + 75] & (1 << 2)) != 0);
        Sts_IntlkOK.UpdateData((data[offset + 75] & (1 << 3)) != 0);
        Sts_NBIntlkOK.UpdateData((data[offset + 75] & (1 << 4)) != 0);
        Sts_Available.UpdateData((data[offset + 75] & (1 << 5)) != 0);
        Sts_IntlkTripInh.UpdateData((data[offset + 75] & (1 << 6)) != 0);
        Sts_BypActive.UpdateData((data[offset + 75] & (1 << 7)) != 0);
        Sts_FirstUpDetect.UpdateData((data[offset + 76] & (1 << 0)) != 0);
        Sts_BankIDError.UpdateData((data[offset + 76] & (1 << 1)) != 0);
        Sts_LatchDefeat.UpdateData((data[offset + 76] & (1 << 2)) != 0);
        Sts_RdyReset.UpdateData((data[offset + 76] & (1 << 3)) != 0);
        Sts_LatchMask.UpdateData(data, offset + 76);
        Sts_BypassMask.UpdateData(data, offset + 80);
        Sts_Intlk.UpdateData(data, offset + 84);
        Sts_FirstOut.UpdateData(data, offset + 88);
        
        return offset + GetSize();
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