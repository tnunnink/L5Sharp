using System.Xml.Linq;
// Auto-generated file
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>FBD_BOOLEAN_NOT</c> data type structure.
/// </summary>
[LogixData("FBD_BOOLEAN_NOT")]
public sealed partial class FBD_BOOLEAN_NOT : StructureData
{
    /// <summary>
    /// Creates a new <see cref="FBD_BOOLEAN_NOT"/> instance initialized with default values.
    /// </summary>
    public FBD_BOOLEAN_NOT() : base("FBD_BOOLEAN_NOT")
    {
        EnableIn = new BOOL();
        In = new BOOL();
        EnableOut = new BOOL();
        Out = new BOOL();
    }

    /// <summary>
    /// Creates a new <see cref="FBD_BOOLEAN_NOT"/> instance initialized with the provided element.
    /// </summary>
    public FBD_BOOLEAN_NOT(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="FBD_BOOLEAN_NOT"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>In</c> member of the <see cref="FBD_BOOLEAN_NOT"/> data type.
    /// </summary>
    public BOOL In
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="FBD_BOOLEAN_NOT"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out</c> member of the <see cref="FBD_BOOLEAN_NOT"/> data type.
    /// </summary>
    public BOOL Out
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

}
