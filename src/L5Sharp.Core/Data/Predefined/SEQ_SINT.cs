using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>SEQ_SINT</c> data type structure.
/// </summary>
[LogixData("SEQ_SINT")]
public sealed partial class SEQ_SINT : StructureData
{
    /// <summary>
    /// Creates a new <see cref="SEQ_SINT"/> instance initialized with default values.
    /// </summary>
    public SEQ_SINT() : base("SEQ_SINT")
    {
        Value = new SINT();
        InitialValue = new SINT();
        Valid = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="SEQ_SINT"/> instance initialized with the provided element.
    /// </summary>
    public SEQ_SINT(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>Value</c> member of the <see cref="SEQ_SINT"/> data type.
    /// </summary>
    public SINT Value
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InitialValue</c> member of the <see cref="SEQ_SINT"/> data type.
    /// </summary>
    public SINT InitialValue
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Valid</c> member of the <see cref="SEQ_SINT"/> data type.
    /// </summary>
    public BOOL Valid
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}