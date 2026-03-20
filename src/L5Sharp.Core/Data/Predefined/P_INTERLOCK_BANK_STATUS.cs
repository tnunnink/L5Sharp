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
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 16;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        Val_FirstUpIndex.UpdateData(data, offset + 0);
        Val_FirstUpBankID.UpdateData(data, offset + 2);
        Val_BankMap.UpdateData(data, offset + 4);
        Val_BankSts.UpdateData(data, offset + 8);
        Inp_Reset.UpdateData((data[offset + 15] & (1 << 0)) != 0);
        Inp_BypassActive.UpdateData((data[offset + 15] & (1 << 1)) != 0);
        Inp_LatchDefeat.UpdateData((data[offset + 15] & (1 << 2)) != 0);
        Inp_Available.UpdateData((data[offset + 15] & (1 << 3)) != 0);
        Sts_BankIDError.UpdateData((data[offset + 15] & (1 << 4)) != 0);
        Sts_IntlkOK.UpdateData((data[offset + 15] & (1 << 5)) != 0);
        Sts_NBIntlkOK.UpdateData((data[offset + 15] & (1 << 6)) != 0);
        Sts_IntlkTripInh.UpdateData((data[offset + 15] & (1 << 7)) != 0);
        Sts_Available.UpdateData((data[offset + 16] & (1 << 0)) != 0);
        Sts_FirstUpDetect.UpdateData((data[offset + 16] & (1 << 1)) != 0);
        Sts_RdyReset.UpdateData((data[offset + 16] & (1 << 2)) != 0);
        Sts_PrevIntlkOK.UpdateData((data[offset + 16] & (1 << 3)) != 0);
        Sts_PrevNBIntlkOK.UpdateData((data[offset + 16] & (1 << 4)) != 0);
        
        return offset + GetSize();
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