using System.Xml.Linq;
// Auto-generated file
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>FBD_CONVERT</c> data type structure.
/// </summary>
[LogixData("FBD_CONVERT")]
public sealed partial class FBD_CONVERT : StructureData
{
    /// <summary>
    /// Creates a new <see cref="FBD_CONVERT"/> instance initialized with default values.
    /// </summary>
    public FBD_CONVERT() : base("FBD_CONVERT")
    {
        EnableIn = new BOOL();
        Source = new DINT();
        EnableOut = new BOOL();
        Dest = new DINT();
    }

    /// <summary>
    /// Creates a new <see cref="FBD_CONVERT"/> instance initialized with the provided element.
    /// </summary>
    public FBD_CONVERT(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="FBD_CONVERT"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Source</c> member of the <see cref="FBD_CONVERT"/> data type.
    /// </summary>
    public DINT Source
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="FBD_CONVERT"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Dest</c> member of the <see cref="FBD_CONVERT"/> data type.
    /// </summary>
    public DINT Dest
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

}
