using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>SELECTABLE_NEGATE</c> data type structure.
/// </summary>
[LogixData("SELECTABLE_NEGATE")]
public sealed partial class SELECTABLE_NEGATE : StructureData
{
    /// <summary>
    /// Creates a new <see cref="SELECTABLE_NEGATE"/> instance initialized with default values.
    /// </summary>
    public SELECTABLE_NEGATE() : base("SELECTABLE_NEGATE")
    {
        EnableIn = new BOOL();
        In = new REAL();
        NegateEnable = new BOOL();
        EnableOut = new BOOL();
        Out = new REAL();
    }
    
    /// <summary>
    /// Creates a new <see cref="SELECTABLE_NEGATE"/> instance initialized with the provided element.
    /// </summary>
    public SELECTABLE_NEGATE(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="SELECTABLE_NEGATE"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>In</c> member of the <see cref="SELECTABLE_NEGATE"/> data type.
    /// </summary>
    public REAL In
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>NegateEnable</c> member of the <see cref="SELECTABLE_NEGATE"/> data type.
    /// </summary>
    public BOOL NegateEnable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="SELECTABLE_NEGATE"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out</c> member of the <see cref="SELECTABLE_NEGATE"/> data type.
    /// </summary>
    public REAL Out
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }
}