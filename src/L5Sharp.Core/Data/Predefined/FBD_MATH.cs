using System.Xml.Linq;
// Auto-generated file
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>FBD_MATH</c> data type structure.
/// </summary>
[LogixData("FBD_MATH")]
public sealed partial class FBD_MATH : StructureData
{
    /// <summary>
    /// Creates a new <see cref="FBD_MATH"/> instance initialized with default values.
    /// </summary>
    public FBD_MATH() : base("FBD_MATH")
    {
        EnableIn = new BOOL();
        SourceA = new REAL();
        SourceB = new REAL();
        EnableOut = new BOOL();
        Dest = new REAL();
    }

    /// <summary>
    /// Creates a new <see cref="FBD_MATH"/> instance initialized with the provided element.
    /// </summary>
    public FBD_MATH(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="FBD_MATH"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SourceA</c> member of the <see cref="FBD_MATH"/> data type.
    /// </summary>
    public REAL SourceA
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SourceB</c> member of the <see cref="FBD_MATH"/> data type.
    /// </summary>
    public REAL SourceB
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="FBD_MATH"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Dest</c> member of the <see cref="FBD_MATH"/> data type.
    /// </summary>
    public REAL Dest
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

}
