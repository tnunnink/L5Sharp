using System.Xml.Linq;
// Auto-generated file
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>FBD_BIT_FIELD_DISTRIBUTE</c> data type structure.
/// </summary>
[LogixData("FBD_BIT_FIELD_DISTRIBUTE")]
public sealed partial class FBD_BIT_FIELD_DISTRIBUTE : StructureData
{
    /// <summary>
    /// Creates a new <see cref="FBD_BIT_FIELD_DISTRIBUTE"/> instance initialized with default values.
    /// </summary>
    public FBD_BIT_FIELD_DISTRIBUTE() : base("FBD_BIT_FIELD_DISTRIBUTE")
    {
        EnableIn = new BOOL();
        Source = new DINT();
        SourceBit = new DINT();
        Length = new DINT();
        DestBit = new DINT();
        Target = new DINT();
        EnableOut = new BOOL();
        Dest = new DINT();
    }

    /// <summary>
    /// Creates a new <see cref="FBD_BIT_FIELD_DISTRIBUTE"/> instance initialized with the provided element.
    /// </summary>
    public FBD_BIT_FIELD_DISTRIBUTE(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="FBD_BIT_FIELD_DISTRIBUTE"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Source</c> member of the <see cref="FBD_BIT_FIELD_DISTRIBUTE"/> data type.
    /// </summary>
    public DINT Source
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SourceBit</c> member of the <see cref="FBD_BIT_FIELD_DISTRIBUTE"/> data type.
    /// </summary>
    public DINT SourceBit
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Length</c> member of the <see cref="FBD_BIT_FIELD_DISTRIBUTE"/> data type.
    /// </summary>
    public DINT Length
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DestBit</c> member of the <see cref="FBD_BIT_FIELD_DISTRIBUTE"/> data type.
    /// </summary>
    public DINT DestBit
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Target</c> member of the <see cref="FBD_BIT_FIELD_DISTRIBUTE"/> data type.
    /// </summary>
    public DINT Target
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="FBD_BIT_FIELD_DISTRIBUTE"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Dest</c> member of the <see cref="FBD_BIT_FIELD_DISTRIBUTE"/> data type.
    /// </summary>
    public DINT Dest
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

}
