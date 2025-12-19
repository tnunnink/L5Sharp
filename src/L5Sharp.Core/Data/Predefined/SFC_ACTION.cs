using System.Xml.Linq;
// Auto-generated file
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>SFC_ACTION</c> data type structure.
/// </summary>
[LogixData("SFC_ACTION")]
public sealed partial class SFC_ACTION : StructureData
{
    /// <summary>
    /// Creates a new <see cref="SFC_ACTION"/> instance initialized with default values.
    /// </summary>
    public SFC_ACTION() : base("SFC_ACTION")
    {
        Status = new DINT();
        A = new BOOL();
        Q = new BOOL();
        PauseTimer = new BOOL();
        PRE = new DINT();
        T = new DINT();
        Count = new DINT();
    }

    /// <summary>
    /// Creates a new <see cref="SFC_ACTION"/> instance initialized with the provided element.
    /// </summary>
    public SFC_ACTION(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>Status</c> member of the <see cref="SFC_ACTION"/> data type.
    /// </summary>
    public DINT Status
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>A</c> member of the <see cref="SFC_ACTION"/> data type.
    /// </summary>
    public BOOL A
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Q</c> member of the <see cref="SFC_ACTION"/> data type.
    /// </summary>
    public BOOL Q
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PauseTimer</c> member of the <see cref="SFC_ACTION"/> data type.
    /// </summary>
    public BOOL PauseTimer
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PRE</c> member of the <see cref="SFC_ACTION"/> data type.
    /// </summary>
    public DINT PRE
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>T</c> member of the <see cref="SFC_ACTION"/> data type.
    /// </summary>
    public DINT T
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Count</c> member of the <see cref="SFC_ACTION"/> data type.
    /// </summary>
    public DINT Count
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

}
