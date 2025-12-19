using System.Xml.Linq;
// Auto-generated file
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>DOMINANT_RESET</c> data type structure.
/// </summary>
[LogixData("DOMINANT_RESET")]
public sealed partial class DOMINANT_RESET : StructureData
{
    /// <summary>
    /// Creates a new <see cref="DOMINANT_RESET"/> instance initialized with default values.
    /// </summary>
    public DOMINANT_RESET() : base("DOMINANT_RESET")
    {
        EnableIn = new BOOL();
        Set = new BOOL();
        Reset = new BOOL();
        EnableOut = new BOOL();
        Out = new BOOL();
        OutNot = new BOOL();
    }

    /// <summary>
    /// Creates a new <see cref="DOMINANT_RESET"/> instance initialized with the provided element.
    /// </summary>
    public DOMINANT_RESET(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="DOMINANT_RESET"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Set</c> member of the <see cref="DOMINANT_RESET"/> data type.
    /// </summary>
    public BOOL Set
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Reset</c> member of the <see cref="DOMINANT_RESET"/> data type.
    /// </summary>
    public BOOL Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="DOMINANT_RESET"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out</c> member of the <see cref="DOMINANT_RESET"/> data type.
    /// </summary>
    public BOOL Out
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OutNot</c> member of the <see cref="DOMINANT_RESET"/> data type.
    /// </summary>
    public BOOL OutNot
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

}
