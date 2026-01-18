using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>FBD_MATH_ADVANCED</c> data type structure.
/// </summary>
[LogixData("FBD_MATH_ADVANCED")]
public sealed partial class FBD_MATH_ADVANCED : StructureData
{
    /// <summary>
    /// Creates a new <see cref="FBD_MATH_ADVANCED"/> instance initialized with default values.
    /// </summary>
    public FBD_MATH_ADVANCED() : base("FBD_MATH_ADVANCED")
    {
        EnableIn = new BOOL();
        Source = new REAL();
        EnableOut = new BOOL();
        Dest = new REAL();
    }
    
    /// <summary>
    /// Creates a new <see cref="FBD_MATH_ADVANCED"/> instance initialized with the provided element.
    /// </summary>
    public FBD_MATH_ADVANCED(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="FBD_MATH_ADVANCED"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Source</c> member of the <see cref="FBD_MATH_ADVANCED"/> data type.
    /// </summary>
    public REAL Source
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="FBD_MATH_ADVANCED"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Dest</c> member of the <see cref="FBD_MATH_ADVANCED"/> data type.
    /// </summary>
    public REAL Dest
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }
}