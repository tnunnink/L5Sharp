using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>FBD_TRUNCATE</c> data type structure.
/// </summary>
[LogixData("FBD_TRUNCATE")]
public sealed partial class FBD_TRUNCATE : StructureData
{
    /// <summary>
    /// Creates a new <see cref="FBD_TRUNCATE"/> instance initialized with default values.
    /// </summary>
    public FBD_TRUNCATE() : base("FBD_TRUNCATE")
    {
        EnableIn = new BOOL();
        Source = new REAL();
        EnableOut = new BOOL();
        Dest = new DINT();
    }
    
    /// <summary>
    /// Creates a new <see cref="FBD_TRUNCATE"/> instance initialized with the provided element.
    /// </summary>
    public FBD_TRUNCATE(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="FBD_TRUNCATE"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Source</c> member of the <see cref="FBD_TRUNCATE"/> data type.
    /// </summary>
    public REAL Source
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="FBD_TRUNCATE"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Dest</c> member of the <see cref="FBD_TRUNCATE"/> data type.
    /// </summary>
    public DINT Dest
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }
}