using System.Xml.Linq;
// Auto-generated file
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>FBD_LIMIT</c> data type structure.
/// </summary>
[LogixData("FBD_LIMIT")]
public sealed partial class FBD_LIMIT : StructureData
{
    /// <summary>
    /// Creates a new <see cref="FBD_LIMIT"/> instance initialized with default values.
    /// </summary>
    public FBD_LIMIT() : base("FBD_LIMIT")
    {
        EnableIn = new BOOL();
        LowLimit = new REAL();
        Test = new REAL();
        HighLimit = new REAL();
        EnableOut = new BOOL();
        Dest = new BOOL();
    }

    /// <summary>
    /// Creates a new <see cref="FBD_LIMIT"/> instance initialized with the provided element.
    /// </summary>
    public FBD_LIMIT(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="FBD_LIMIT"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>LowLimit</c> member of the <see cref="FBD_LIMIT"/> data type.
    /// </summary>
    public REAL LowLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Test</c> member of the <see cref="FBD_LIMIT"/> data type.
    /// </summary>
    public REAL Test
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HighLimit</c> member of the <see cref="FBD_LIMIT"/> data type.
    /// </summary>
    public REAL HighLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="FBD_LIMIT"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Dest</c> member of the <see cref="FBD_LIMIT"/> data type.
    /// </summary>
    public BOOL Dest
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

}
