using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>FBD_COMPARE</c> data type structure.
/// </summary>
[LogixData("FBD_COMPARE")]
public sealed partial class FBD_COMPARE : StructureData
{
    /// <summary>
    /// Creates a new <see cref="FBD_COMPARE"/> instance initialized with default values.
    /// </summary>
    public FBD_COMPARE() : base("FBD_COMPARE")
    {
        EnableIn = new BOOL();
        SourceA = new REAL();
        SourceB = new REAL();
        EnableOut = new BOOL();
        Dest = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="FBD_COMPARE"/> instance initialized with the provided element.
    /// </summary>
    public FBD_COMPARE(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="FBD_COMPARE"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SourceA</c> member of the <see cref="FBD_COMPARE"/> data type.
    /// </summary>
    public REAL SourceA
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SourceB</c> member of the <see cref="FBD_COMPARE"/> data type.
    /// </summary>
    public REAL SourceB
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="FBD_COMPARE"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Dest</c> member of the <see cref="FBD_COMPARE"/> data type.
    /// </summary>
    public BOOL Dest
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}