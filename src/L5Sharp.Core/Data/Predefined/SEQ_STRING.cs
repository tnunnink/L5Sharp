using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>SEQ_STRING</c> data type structure.
/// </summary>
[LogixData("SEQ_STRING")]
public sealed partial class SEQ_STRING : StructureData
{
    /// <summary>
    /// Creates a new <see cref="SEQ_STRING"/> instance initialized with default values.
    /// </summary>
    public SEQ_STRING() : base("SEQ_STRING")
    {
        Value = new STRING();
        InitialValue = new STRING();
        Valid = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="SEQ_STRING"/> instance initialized with the provided element.
    /// </summary>
    public SEQ_STRING(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>Value</c> member of the <see cref="SEQ_STRING"/> data type.
    /// </summary>
    public STRING Value
    {
        get => GetMember<STRING>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InitialValue</c> member of the <see cref="SEQ_STRING"/> data type.
    /// </summary>
    public STRING InitialValue
    {
        get => GetMember<STRING>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Valid</c> member of the <see cref="SEQ_STRING"/> data type.
    /// </summary>
    public BOOL Valid
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}