using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>FUNCTION_GENERATOR</c> data type structure.
/// </summary>
[LogixData("FUNCTION_GENERATOR")]
public sealed partial class FUNCTION_GENERATOR : StructureData
{
    /// <summary>
    /// Creates a new <see cref="FUNCTION_GENERATOR"/> instance initialized with default values.
    /// </summary>
    public FUNCTION_GENERATOR() : base("FUNCTION_GENERATOR")
    {
        EnableIn = new BOOL();
        In = new REAL();
        XY1Size = new DINT();
        XY2Size = new DINT();
        Select = new BOOL();
        EnableOut = new BOOL();
        Out = new REAL();
        Status = new DINT();
        InstructFault = new BOOL();
        XY1SizeInv = new BOOL();
        XY2SizeInv = new BOOL();
        XisOutofOrder = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="FUNCTION_GENERATOR"/> instance initialized with the provided element.
    /// </summary>
    public FUNCTION_GENERATOR(XElement element) : base(element)
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
        XY1Size.UpdateData(data, offset + 9);
        XY2Size.UpdateData(data, offset + 13);
        Select.UpdateData((data[offset + 17] & (1 << 1)) != 0);
        EnableOut.UpdateData((data[offset + 21] & (1 << 2)) != 0);
        Out.UpdateData(data, offset + 21);
        Status.UpdateData(data, offset + 25);
        InstructFault.UpdateData((data[offset + 29] & (1 << 3)) != 0);
        XY1SizeInv.UpdateData((data[offset + 29] & (1 << 4)) != 0);
        XY2SizeInv.UpdateData((data[offset + 29] & (1 << 5)) != 0);
        XisOutofOrder.UpdateData((data[offset + 29] & (1 << 6)) != 0);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="FUNCTION_GENERATOR"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>In</c> member of the <see cref="FUNCTION_GENERATOR"/> data type.
    /// </summary>
    public REAL In
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XY1Size</c> member of the <see cref="FUNCTION_GENERATOR"/> data type.
    /// </summary>
    public DINT XY1Size
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XY2Size</c> member of the <see cref="FUNCTION_GENERATOR"/> data type.
    /// </summary>
    public DINT XY2Size
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Select</c> member of the <see cref="FUNCTION_GENERATOR"/> data type.
    /// </summary>
    public BOOL Select
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="FUNCTION_GENERATOR"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out</c> member of the <see cref="FUNCTION_GENERATOR"/> data type.
    /// </summary>
    public REAL Out
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Status</c> member of the <see cref="FUNCTION_GENERATOR"/> data type.
    /// </summary>
    public DINT Status
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InstructFault</c> member of the <see cref="FUNCTION_GENERATOR"/> data type.
    /// </summary>
    public BOOL InstructFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XY1SizeInv</c> member of the <see cref="FUNCTION_GENERATOR"/> data type.
    /// </summary>
    public BOOL XY1SizeInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XY2SizeInv</c> member of the <see cref="FUNCTION_GENERATOR"/> data type.
    /// </summary>
    public BOOL XY2SizeInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>XisOutofOrder</c> member of the <see cref="FUNCTION_GENERATOR"/> data type.
    /// </summary>
    public BOOL XisOutofOrder
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}