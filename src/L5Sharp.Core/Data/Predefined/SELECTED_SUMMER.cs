using System.Xml.Linq;
// Auto-generated file
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart

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
