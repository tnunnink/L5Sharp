using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>EIGHT_POS_MODE_SELECTOR</c> data type structure.
/// </summary>
[LogixData("EIGHT_POS_MODE_SELECTOR")]
public sealed partial class EIGHT_POS_MODE_SELECTOR : StructureData
{
    /// <summary>
    /// Creates a new <see cref="EIGHT_POS_MODE_SELECTOR"/> instance initialized with default values.
    /// </summary>
    public EIGHT_POS_MODE_SELECTOR() : base("EIGHT_POS_MODE_SELECTOR")
    {
        EnableIn = new BOOL();
        Input1 = new BOOL();
        Input2 = new BOOL();
        Input3 = new BOOL();
        Input4 = new BOOL();
        Input5 = new BOOL();
        Input6 = new BOOL();
        Input7 = new BOOL();
        Input8 = new BOOL();
        InputStatus = new BOOL();
        Lock = new BOOL();
        Reset = new BOOL();
        EnableOut = new BOOL();
        O1 = new BOOL();
        O2 = new BOOL();
        O3 = new BOOL();
        O4 = new BOOL();
        O5 = new BOOL();
        O6 = new BOOL();
        O7 = new BOOL();
        O8 = new BOOL();
        FP = new BOOL();
        FaultCode = new DINT();
        DiagnosticCode = new DINT();
    }
    
    /// <summary>
    /// Creates a new <see cref="EIGHT_POS_MODE_SELECTOR"/> instance initialized with the provided element.
    /// </summary>
    public EIGHT_POS_MODE_SELECTOR(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 44;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        EnableIn.UpdateData((data[offset + 5] & (1 << 0)) != 0);
        Input1.UpdateData((data[offset + 5] & (1 << 1)) != 0);
        Input2.UpdateData((data[offset + 5] & (1 << 2)) != 0);
        Input3.UpdateData((data[offset + 5] & (1 << 3)) != 0);
        Input4.UpdateData((data[offset + 5] & (1 << 4)) != 0);
        Input5.UpdateData((data[offset + 5] & (1 << 5)) != 0);
        Input6.UpdateData((data[offset + 5] & (1 << 6)) != 0);
        Input7.UpdateData((data[offset + 5] & (1 << 7)) != 0);
        Input8.UpdateData((data[offset + 6] & (1 << 0)) != 0);
        InputStatus.UpdateData((data[offset + 6] & (1 << 1)) != 0);
        Lock.UpdateData((data[offset + 6] & (1 << 2)) != 0);
        Reset.UpdateData((data[offset + 6] & (1 << 3)) != 0);
        EnableOut.UpdateData((data[offset + 10] & (1 << 4)) != 0);
        O1.UpdateData((data[offset + 10] & (1 << 5)) != 0);
        O2.UpdateData((data[offset + 10] & (1 << 6)) != 0);
        O3.UpdateData((data[offset + 10] & (1 << 7)) != 0);
        O4.UpdateData((data[offset + 11] & (1 << 0)) != 0);
        O5.UpdateData((data[offset + 11] & (1 << 1)) != 0);
        O6.UpdateData((data[offset + 11] & (1 << 2)) != 0);
        O7.UpdateData((data[offset + 11] & (1 << 3)) != 0);
        O8.UpdateData((data[offset + 11] & (1 << 4)) != 0);
        FP.UpdateData((data[offset + 11] & (1 << 5)) != 0);
        FaultCode.UpdateData(data, offset + 11);
        DiagnosticCode.UpdateData(data, offset + 15);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="EIGHT_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Input1</c> member of the <see cref="EIGHT_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public BOOL Input1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Input2</c> member of the <see cref="EIGHT_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public BOOL Input2
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Input3</c> member of the <see cref="EIGHT_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public BOOL Input3
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Input4</c> member of the <see cref="EIGHT_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public BOOL Input4
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Input5</c> member of the <see cref="EIGHT_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public BOOL Input5
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Input6</c> member of the <see cref="EIGHT_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public BOOL Input6
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Input7</c> member of the <see cref="EIGHT_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public BOOL Input7
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Input8</c> member of the <see cref="EIGHT_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public BOOL Input8
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InputStatus</c> member of the <see cref="EIGHT_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public BOOL InputStatus
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Lock</c> member of the <see cref="EIGHT_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public BOOL Lock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Reset</c> member of the <see cref="EIGHT_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public BOOL Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="EIGHT_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>O1</c> member of the <see cref="EIGHT_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public BOOL O1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>O2</c> member of the <see cref="EIGHT_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public BOOL O2
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>O3</c> member of the <see cref="EIGHT_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public BOOL O3
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>O4</c> member of the <see cref="EIGHT_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public BOOL O4
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>O5</c> member of the <see cref="EIGHT_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public BOOL O5
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>O6</c> member of the <see cref="EIGHT_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public BOOL O6
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>O7</c> member of the <see cref="EIGHT_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public BOOL O7
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>O8</c> member of the <see cref="EIGHT_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public BOOL O8
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FP</c> member of the <see cref="EIGHT_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public BOOL FP
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FaultCode</c> member of the <see cref="EIGHT_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public DINT FaultCode
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DiagnosticCode</c> member of the <see cref="EIGHT_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public DINT DiagnosticCode
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }
}