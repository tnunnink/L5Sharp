using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>FBD_MASK_EQUAL</c> data type structure.
/// </summary>
[LogixData("FBD_MASK_EQUAL")]
public sealed partial class FBD_MASK_EQUAL : StructureData
{
    /// <summary>
    /// Creates a new <see cref="FBD_MASK_EQUAL"/> instance initialized with default values.
    /// </summary>
    public FBD_MASK_EQUAL() : base("FBD_MASK_EQUAL")
    {
        EnableIn = new BOOL();
        Source = new DINT();
        Mask = new DINT();
        Compare = new DINT();
        EnableOut = new BOOL();
        Dest = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="FBD_MASK_EQUAL"/> instance initialized with the provided element.
    /// </summary>
    public FBD_MASK_EQUAL(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="FBD_MASK_EQUAL"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Source</c> member of the <see cref="FBD_MASK_EQUAL"/> data type.
    /// </summary>
    public DINT Source
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Mask</c> member of the <see cref="FBD_MASK_EQUAL"/> data type.
    /// </summary>
    public DINT Mask
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Compare</c> member of the <see cref="FBD_MASK_EQUAL"/> data type.
    /// </summary>
    public DINT Compare
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="FBD_MASK_EQUAL"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Dest</c> member of the <see cref="FBD_MASK_EQUAL"/> data type.
    /// </summary>
    public BOOL Dest
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}