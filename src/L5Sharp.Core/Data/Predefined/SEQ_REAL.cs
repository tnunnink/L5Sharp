using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>SEQ_REAL</c> data type structure.
/// </summary>
[LogixData("SEQ_REAL")]
public sealed partial class SEQ_REAL : StructureData
{
    /// <summary>
    /// Creates a new <see cref="SEQ_REAL"/> instance initialized with default values.
    /// </summary>
    public SEQ_REAL() : base("SEQ_REAL")
    {
        Value = new REAL();
        InitialValue = new REAL();
        Valid = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="SEQ_REAL"/> instance initialized with the provided element.
    /// </summary>
    public SEQ_REAL(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>Value</c> member of the <see cref="SEQ_REAL"/> data type.
    /// </summary>
    public REAL Value
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InitialValue</c> member of the <see cref="SEQ_REAL"/> data type.
    /// </summary>
    public REAL InitialValue
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Valid</c> member of the <see cref="SEQ_REAL"/> data type.
    /// </summary>
    public BOOL Valid
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}