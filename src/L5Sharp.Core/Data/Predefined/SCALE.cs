using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>SCALE</c> data type structure.
/// </summary>
[LogixData("SCALE")]
public sealed partial class SCALE : StructureData
{
    /// <summary>
    /// Creates a new <see cref="SCALE"/> instance initialized with default values.
    /// </summary>
    public SCALE() : base("SCALE")
    {
        EnableIn = new BOOL();
        In = new REAL();
        InRawMax = new REAL();
        InRawMin = new REAL();
        InEUMax = new REAL();
        InEUMin = new REAL();
        Limiting = new BOOL();
        EnableOut = new BOOL();
        Out = new REAL();
        MaxAlarm = new BOOL();
        MinAlarm = new BOOL();
        Status = new DINT();
        InstructFault = new BOOL();
        InRawRangeInv = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="SCALE"/> instance initialized with the provided element.
    /// </summary>
    public SCALE(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 52;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        EnableIn.UpdateData((data[offset + 5] & (1 << 0)) != 0);
        In.UpdateData(data, offset + 5);
        InRawMax.UpdateData(data, offset + 9);
        InRawMin.UpdateData(data, offset + 13);
        InEUMax.UpdateData(data, offset + 17);
        InEUMin.UpdateData(data, offset + 21);
        Limiting.UpdateData((data[offset + 25] & (1 << 1)) != 0);
        EnableOut.UpdateData((data[offset + 29] & (1 << 2)) != 0);
        Out.UpdateData(data, offset + 29);
        MaxAlarm.UpdateData((data[offset + 33] & (1 << 3)) != 0);
        MinAlarm.UpdateData((data[offset + 33] & (1 << 4)) != 0);
        Status.UpdateData(data, offset + 33);
        InstructFault.UpdateData((data[offset + 37] & (1 << 5)) != 0);
        InRawRangeInv.UpdateData((data[offset + 37] & (1 << 6)) != 0);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="SCALE"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>In</c> member of the <see cref="SCALE"/> data type.
    /// </summary>
    public REAL In
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InRawMax</c> member of the <see cref="SCALE"/> data type.
    /// </summary>
    public REAL InRawMax
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InRawMin</c> member of the <see cref="SCALE"/> data type.
    /// </summary>
    public REAL InRawMin
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InEUMax</c> member of the <see cref="SCALE"/> data type.
    /// </summary>
    public REAL InEUMax
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InEUMin</c> member of the <see cref="SCALE"/> data type.
    /// </summary>
    public REAL InEUMin
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Limiting</c> member of the <see cref="SCALE"/> data type.
    /// </summary>
    public BOOL Limiting
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="SCALE"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out</c> member of the <see cref="SCALE"/> data type.
    /// </summary>
    public REAL Out
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>MaxAlarm</c> member of the <see cref="SCALE"/> data type.
    /// </summary>
    public BOOL MaxAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>MinAlarm</c> member of the <see cref="SCALE"/> data type.
    /// </summary>
    public BOOL MinAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Status</c> member of the <see cref="SCALE"/> data type.
    /// </summary>
    public DINT Status
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InstructFault</c> member of the <see cref="SCALE"/> data type.
    /// </summary>
    public BOOL InstructFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InRawRangeInv</c> member of the <see cref="SCALE"/> data type.
    /// </summary>
    public BOOL InRawRangeInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}