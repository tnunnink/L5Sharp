using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>P_STRAPPING_TABLE_ROW</c> data type structure.
/// </summary>
[LogixData("P_STRAPPING_TABLE_ROW")]
public sealed partial class P_STRAPPING_TABLE_ROW : StructureData
{
    /// <summary>
    /// Creates a new <see cref="P_STRAPPING_TABLE_ROW"/> instance initialized with default values.
    /// </summary>
    public P_STRAPPING_TABLE_ROW() : base("P_STRAPPING_TABLE_ROW")
    {
        Major = new REAL();
        Minor = new REAL();
        Volume = new REAL();
    }
    
    /// <summary>
    /// Creates a new <see cref="P_STRAPPING_TABLE_ROW"/> instance initialized with the provided element.
    /// </summary>
    public P_STRAPPING_TABLE_ROW(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>Major</c> member of the <see cref="P_STRAPPING_TABLE_ROW"/> data type.
    /// </summary>
    public REAL Major
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Minor</c> member of the <see cref="P_STRAPPING_TABLE_ROW"/> data type.
    /// </summary>
    public REAL Minor
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Volume</c> member of the <see cref="P_STRAPPING_TABLE_ROW"/> data type.
    /// </summary>
    public REAL Volume
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }
}