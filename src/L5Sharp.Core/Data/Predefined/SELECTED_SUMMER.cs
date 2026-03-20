using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>SELECTED_SUMMER</c> data type structure.
/// </summary>
[LogixData("SELECTED_SUMMER")]
public sealed partial class SELECTED_SUMMER : StructureData
{
    /// <summary>
    /// Creates a new <see cref="SELECTED_SUMMER"/> instance initialized with default values.
    /// </summary>
    public SELECTED_SUMMER() : base("SELECTED_SUMMER")
    {
        EnableIn = new BOOL();
        In1 = new REAL();
        Gain1 = new REAL();
        Select1 = new BOOL();
        In2 = new REAL();
        Gain2 = new REAL();
        Select2 = new BOOL();
        In3 = new REAL();
        Gain3 = new REAL();
        Select3 = new BOOL();
        In4 = new REAL();
        Gain4 = new REAL();
        Select4 = new BOOL();
        In5 = new REAL();
        Gain5 = new REAL();
        Select5 = new BOOL();
        In6 = new REAL();
        Gain6 = new REAL();
        Select6 = new BOOL();
        In7 = new REAL();
        Gain7 = new REAL();
        Select7 = new BOOL();
        In8 = new REAL();
        Gain8 = new REAL();
        Select8 = new BOOL();
        Bias = new REAL();
        EnableOut = new BOOL();
        Out = new REAL();
    }
    
    /// <summary>
    /// Creates a new <see cref="SELECTED_SUMMER"/> instance initialized with the provided element.
    /// </summary>
    public SELECTED_SUMMER(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 84;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        EnableIn.UpdateData((data[offset + 5] & (1 << 0)) != 0);
        In1.UpdateData(data, offset + 5);
        Gain1.UpdateData(data, offset + 9);
        Select1.UpdateData((data[offset + 13] & (1 << 1)) != 0);
        In2.UpdateData(data, offset + 13);
        Gain2.UpdateData(data, offset + 17);
        Select2.UpdateData((data[offset + 21] & (1 << 2)) != 0);
        In3.UpdateData(data, offset + 21);
        Gain3.UpdateData(data, offset + 25);
        Select3.UpdateData((data[offset + 29] & (1 << 3)) != 0);
        In4.UpdateData(data, offset + 29);
        Gain4.UpdateData(data, offset + 33);
        Select4.UpdateData((data[offset + 37] & (1 << 4)) != 0);
        In5.UpdateData(data, offset + 37);
        Gain5.UpdateData(data, offset + 41);
        Select5.UpdateData((data[offset + 45] & (1 << 5)) != 0);
        In6.UpdateData(data, offset + 45);
        Gain6.UpdateData(data, offset + 49);
        Select6.UpdateData((data[offset + 53] & (1 << 6)) != 0);
        In7.UpdateData(data, offset + 53);
        Gain7.UpdateData(data, offset + 57);
        Select7.UpdateData((data[offset + 61] & (1 << 7)) != 0);
        In8.UpdateData(data, offset + 61);
        Gain8.UpdateData(data, offset + 65);
        Select8.UpdateData((data[offset + 70] & (1 << 0)) != 0);
        Bias.UpdateData(data, offset + 70);
        EnableOut.UpdateData((data[offset + 78] & (1 << 1)) != 0);
        Out.UpdateData(data, offset + 78);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="SELECTED_SUMMER"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>In1</c> member of the <see cref="SELECTED_SUMMER"/> data type.
    /// </summary>
    public REAL In1
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Gain1</c> member of the <see cref="SELECTED_SUMMER"/> data type.
    /// </summary>
    public REAL Gain1
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Select1</c> member of the <see cref="SELECTED_SUMMER"/> data type.
    /// </summary>
    public BOOL Select1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>In2</c> member of the <see cref="SELECTED_SUMMER"/> data type.
    /// </summary>
    public REAL In2
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Gain2</c> member of the <see cref="SELECTED_SUMMER"/> data type.
    /// </summary>
    public REAL Gain2
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Select2</c> member of the <see cref="SELECTED_SUMMER"/> data type.
    /// </summary>
    public BOOL Select2
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>In3</c> member of the <see cref="SELECTED_SUMMER"/> data type.
    /// </summary>
    public REAL In3
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Gain3</c> member of the <see cref="SELECTED_SUMMER"/> data type.
    /// </summary>
    public REAL Gain3
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Select3</c> member of the <see cref="SELECTED_SUMMER"/> data type.
    /// </summary>
    public BOOL Select3
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>In4</c> member of the <see cref="SELECTED_SUMMER"/> data type.
    /// </summary>
    public REAL In4
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Gain4</c> member of the <see cref="SELECTED_SUMMER"/> data type.
    /// </summary>
    public REAL Gain4
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Select4</c> member of the <see cref="SELECTED_SUMMER"/> data type.
    /// </summary>
    public BOOL Select4
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>In5</c> member of the <see cref="SELECTED_SUMMER"/> data type.
    /// </summary>
    public REAL In5
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Gain5</c> member of the <see cref="SELECTED_SUMMER"/> data type.
    /// </summary>
    public REAL Gain5
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Select5</c> member of the <see cref="SELECTED_SUMMER"/> data type.
    /// </summary>
    public BOOL Select5
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>In6</c> member of the <see cref="SELECTED_SUMMER"/> data type.
    /// </summary>
    public REAL In6
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Gain6</c> member of the <see cref="SELECTED_SUMMER"/> data type.
    /// </summary>
    public REAL Gain6
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Select6</c> member of the <see cref="SELECTED_SUMMER"/> data type.
    /// </summary>
    public BOOL Select6
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>In7</c> member of the <see cref="SELECTED_SUMMER"/> data type.
    /// </summary>
    public REAL In7
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Gain7</c> member of the <see cref="SELECTED_SUMMER"/> data type.
    /// </summary>
    public REAL Gain7
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Select7</c> member of the <see cref="SELECTED_SUMMER"/> data type.
    /// </summary>
    public BOOL Select7
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>In8</c> member of the <see cref="SELECTED_SUMMER"/> data type.
    /// </summary>
    public REAL In8
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Gain8</c> member of the <see cref="SELECTED_SUMMER"/> data type.
    /// </summary>
    public REAL Gain8
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Select8</c> member of the <see cref="SELECTED_SUMMER"/> data type.
    /// </summary>
    public BOOL Select8
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Bias</c> member of the <see cref="SELECTED_SUMMER"/> data type.
    /// </summary>
    public REAL Bias
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="SELECTED_SUMMER"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out</c> member of the <see cref="SELECTED_SUMMER"/> data type.
    /// </summary>
    public REAL Out
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }
}