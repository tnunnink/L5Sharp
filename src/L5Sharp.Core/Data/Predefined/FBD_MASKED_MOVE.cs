using System.Xml.Linq;
// Auto-generated file
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>FBD_MASKED_MOVE</c> data type structure.
/// </summary>
[LogixData("FBD_MASKED_MOVE")]
public sealed partial class FBD_MASKED_MOVE : StructureData
{
    /// <summary>
    /// Creates a new <see cref="FBD_MASKED_MOVE"/> instance initialized with default values.
    /// </summary>
    public FBD_MASKED_MOVE() : base("FBD_MASKED_MOVE")
    {
        EnableIn = new BOOL();
        Source = new DINT();
        Mask = new DINT();
        Target = new DINT();
        EnableOut = new BOOL();
        Dest = new DINT();
    }

    /// <summary>
    /// Creates a new <see cref="FBD_MASKED_MOVE"/> instance initialized with the provided element.
    /// </summary>
    public FBD_MASKED_MOVE(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="FBD_MASKED_MOVE"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Source</c> member of the <see cref="FBD_MASKED_MOVE"/> data type.
    /// </summary>
    public DINT Source
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Mask</c> member of the <see cref="FBD_MASKED_MOVE"/> data type.
    /// </summary>
    public DINT Mask
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Target</c> member of the <see cref="FBD_MASKED_MOVE"/> data type.
    /// </summary>
    public DINT Target
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="FBD_MASKED_MOVE"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Dest</c> member of the <see cref="FBD_MASKED_MOVE"/> data type.
    /// </summary>
    public DINT Dest
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

}
