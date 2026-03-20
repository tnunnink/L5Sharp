using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>HL_LIMIT</c> data type structure.
/// </summary>
[LogixData("HL_LIMIT")]
public sealed partial class HL_LIMIT : StructureData
{
    /// <summary>
    /// Creates a new <see cref="HL_LIMIT"/> instance initialized with default values.
    /// </summary>
    public HL_LIMIT() : base("HL_LIMIT")
    {
        EnableIn = new BOOL();
        In = new REAL();
        HighLimit = new REAL();
        LowLimit = new REAL();
        SelectLimit = new DINT();
        EnableOut = new BOOL();
        Out = new REAL();
        HighAlarm = new BOOL();
        LowAlarm = new BOOL();
        Status = new DINT();
        InstructFault = new BOOL();
        LimitsInv = new BOOL();
        SelectLimitInv = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="HL_LIMIT"/> instance initialized with the provided element.
    /// </summary>
    public HL_LIMIT(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 36;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        EnableIn.UpdateData((data[offset + 5] & (1 << 0)) != 0);
        In.UpdateData(data, offset + 5);
        HighLimit.UpdateData(data, offset + 9);
        LowLimit.UpdateData(data, offset + 13);
        SelectLimit.UpdateData(data, offset + 17);
        EnableOut.UpdateData((data[offset + 25] & (1 << 1)) != 0);
        Out.UpdateData(data, offset + 25);
        HighAlarm.UpdateData((data[offset + 29] & (1 << 2)) != 0);
        LowAlarm.UpdateData((data[offset + 29] & (1 << 3)) != 0);
        Status.UpdateData(data, offset + 29);
        InstructFault.UpdateData((data[offset + 33] & (1 << 4)) != 0);
        LimitsInv.UpdateData((data[offset + 33] & (1 << 5)) != 0);
        SelectLimitInv.UpdateData((data[offset + 33] & (1 << 6)) != 0);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="HL_LIMIT"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>In</c> member of the <see cref="HL_LIMIT"/> data type.
    /// </summary>
    public REAL In
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HighLimit</c> member of the <see cref="HL_LIMIT"/> data type.
    /// </summary>
    public REAL HighLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>LowLimit</c> member of the <see cref="HL_LIMIT"/> data type.
    /// </summary>
    public REAL LowLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SelectLimit</c> member of the <see cref="HL_LIMIT"/> data type.
    /// </summary>
    public DINT SelectLimit
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="HL_LIMIT"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out</c> member of the <see cref="HL_LIMIT"/> data type.
    /// </summary>
    public REAL Out
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HighAlarm</c> member of the <see cref="HL_LIMIT"/> data type.
    /// </summary>
    public BOOL HighAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>LowAlarm</c> member of the <see cref="HL_LIMIT"/> data type.
    /// </summary>
    public BOOL LowAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Status</c> member of the <see cref="HL_LIMIT"/> data type.
    /// </summary>
    public DINT Status
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InstructFault</c> member of the <see cref="HL_LIMIT"/> data type.
    /// </summary>
    public BOOL InstructFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>LimitsInv</c> member of the <see cref="HL_LIMIT"/> data type.
    /// </summary>
    public BOOL LimitsInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SelectLimitInv</c> member of the <see cref="HL_LIMIT"/> data type.
    /// </summary>
    public BOOL SelectLimitInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}