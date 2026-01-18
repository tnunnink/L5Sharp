using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>P_INTERLOCK_BANK_STATUS</c> data type structure.
/// </summary>
[LogixData("P_INTERLOCK_BANK_STATUS")]
public sealed partial class P_INTERLOCK_BANK_STATUS : StructureData
{
    /// <summary>
    /// Creates a new <see cref="P_INTERLOCK_BANK_STATUS"/> instance initialized with default values.
    /// </summary>
    public P_INTERLOCK_BANK_STATUS() : base("P_INTERLOCK_BANK_STATUS")
    {
        Val_FirstUpIndex = new INT();
        Val_FirstUpBankID = new INT();
        Val_BankMap = new DINT();
        Val_BankSts = new DINT();
        Inp_Reset = new BOOL();
        Inp_BypassActive = new BOOL();
        Inp_LatchDefeat = new BOOL();
        Inp_Available = new BOOL();
        Sts_BankIDError = new BOOL();
        Sts_IntlkOK = new BOOL();
        Sts_NBIntlkOK = new BOOL();
        Sts_IntlkTripInh = new BOOL();
        Sts_Available = new BOOL();
        Sts_FirstUpDetect = new BOOL();
        Sts_RdyReset = new BOOL();
        Sts_PrevIntlkOK = new BOOL();
        Sts_PrevNBIntlkOK = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="P_INTERLOCK_BANK_STATUS"/> instance initialized with the provided element.
    /// </summary>
    public P_INTERLOCK_BANK_STATUS(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>Val_FirstUpIndex</c> member of the <see cref="P_INTERLOCK_BANK_STATUS"/> data type.
    /// </summary>
    public INT Val_FirstUpIndex
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_FirstUpBankID</c> member of the <see cref="P_INTERLOCK_BANK_STATUS"/> data type.
    /// </summary>
    public INT Val_FirstUpBankID
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_BankMap</c> member of the <see cref="P_INTERLOCK_BANK_STATUS"/> data type.
    /// </summary>
    public DINT Val_BankMap
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_BankSts</c> member of the <see cref="P_INTERLOCK_BANK_STATUS"/> data type.
    /// </summary>
    public DINT Val_BankSts
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Reset</c> member of the <see cref="P_INTERLOCK_BANK_STATUS"/> data type.
    /// </summary>
    public BOOL Inp_Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_BypassActive</c> member of the <see cref="P_INTERLOCK_BANK_STATUS"/> data type.
    /// </summary>
    public BOOL Inp_BypassActive
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_LatchDefeat</c> member of the <see cref="P_INTERLOCK_BANK_STATUS"/> data type.
    /// </summary>
    public BOOL Inp_LatchDefeat
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Available</c> member of the <see cref="P_INTERLOCK_BANK_STATUS"/> data type.
    /// </summary>
    public BOOL Inp_Available
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_BankIDError</c> member of the <see cref="P_INTERLOCK_BANK_STATUS"/> data type.
    /// </summary>
    public BOOL Sts_BankIDError
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_IntlkOK</c> member of the <see cref="P_INTERLOCK_BANK_STATUS"/> data type.
    /// </summary>
    public BOOL Sts_IntlkOK
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_NBIntlkOK</c> member of the <see cref="P_INTERLOCK_BANK_STATUS"/> data type.
    /// </summary>
    public BOOL Sts_NBIntlkOK
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_IntlkTripInh</c> member of the <see cref="P_INTERLOCK_BANK_STATUS"/> data type.
    /// </summary>
    public BOOL Sts_IntlkTripInh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Available</c> member of the <see cref="P_INTERLOCK_BANK_STATUS"/> data type.
    /// </summary>
    public BOOL Sts_Available
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_FirstUpDetect</c> member of the <see cref="P_INTERLOCK_BANK_STATUS"/> data type.
    /// </summary>
    public BOOL Sts_FirstUpDetect
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_RdyReset</c> member of the <see cref="P_INTERLOCK_BANK_STATUS"/> data type.
    /// </summary>
    public BOOL Sts_RdyReset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_PrevIntlkOK</c> member of the <see cref="P_INTERLOCK_BANK_STATUS"/> data type.
    /// </summary>
    public BOOL Sts_PrevIntlkOK
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_PrevNBIntlkOK</c> member of the <see cref="P_INTERLOCK_BANK_STATUS"/> data type.
    /// </summary>
    public BOOL Sts_PrevNBIntlkOK
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}