using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>MULTIPLEXER</c> data type structure.
/// </summary>
[LogixData("MULTIPLEXER")]
public sealed partial class MULTIPLEXER : StructureData
{
    /// <summary>
    /// Creates a new <see cref="MULTIPLEXER"/> instance initialized with default values.
    /// </summary>
    public MULTIPLEXER() : base("MULTIPLEXER")
    {
        EnableIn = new BOOL();
        In1 = new REAL();
        In2 = new REAL();
        In3 = new REAL();
        In4 = new REAL();
        In5 = new REAL();
        In6 = new REAL();
        In7 = new REAL();
        In8 = new REAL();
        Selector = new DINT();
        EnableOut = new BOOL();
        Out = new REAL();
        Status = new DINT();
        InstructFault = new BOOL();
        SelectorInv = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="MULTIPLEXER"/> instance initialized with the provided element.
    /// </summary>
    public MULTIPLEXER(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 60;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        EnableIn.UpdateData((data[offset + 5] & (1 << 0)) != 0);
        In1.UpdateData(data, offset + 5);
        In2.UpdateData(data, offset + 9);
        In3.UpdateData(data, offset + 13);
        In4.UpdateData(data, offset + 17);
        In5.UpdateData(data, offset + 21);
        In6.UpdateData(data, offset + 25);
        In7.UpdateData(data, offset + 29);
        In8.UpdateData(data, offset + 33);
        Selector.UpdateData(data, offset + 37);
        EnableOut.UpdateData((data[offset + 45] & (1 << 1)) != 0);
        Out.UpdateData(data, offset + 45);
        Status.UpdateData(data, offset + 49);
        InstructFault.UpdateData((data[offset + 53] & (1 << 2)) != 0);
        SelectorInv.UpdateData((data[offset + 53] & (1 << 3)) != 0);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="MULTIPLEXER"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>In1</c> member of the <see cref="MULTIPLEXER"/> data type.
    /// </summary>
    public REAL In1
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>In2</c> member of the <see cref="MULTIPLEXER"/> data type.
    /// </summary>
    public REAL In2
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>In3</c> member of the <see cref="MULTIPLEXER"/> data type.
    /// </summary>
    public REAL In3
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>In4</c> member of the <see cref="MULTIPLEXER"/> data type.
    /// </summary>
    public REAL In4
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>In5</c> member of the <see cref="MULTIPLEXER"/> data type.
    /// </summary>
    public REAL In5
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>In6</c> member of the <see cref="MULTIPLEXER"/> data type.
    /// </summary>
    public REAL In6
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>In7</c> member of the <see cref="MULTIPLEXER"/> data type.
    /// </summary>
    public REAL In7
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>In8</c> member of the <see cref="MULTIPLEXER"/> data type.
    /// </summary>
    public REAL In8
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Selector</c> member of the <see cref="MULTIPLEXER"/> data type.
    /// </summary>
    public DINT Selector
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="MULTIPLEXER"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out</c> member of the <see cref="MULTIPLEXER"/> data type.
    /// </summary>
    public REAL Out
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Status</c> member of the <see cref="MULTIPLEXER"/> data type.
    /// </summary>
    public DINT Status
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InstructFault</c> member of the <see cref="MULTIPLEXER"/> data type.
    /// </summary>
    public BOOL InstructFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SelectorInv</c> member of the <see cref="MULTIPLEXER"/> data type.
    /// </summary>
    public BOOL SelectorInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}