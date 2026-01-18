using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>SEQ_BOOL</c> data type structure.
/// </summary>
[LogixData("SEQ_BOOL")]
public sealed partial class SEQ_BOOL : StructureData
{
    /// <summary>
    /// Creates a new <see cref="SEQ_BOOL"/> instance initialized with default values.
    /// </summary>
    public SEQ_BOOL() : base("SEQ_BOOL")
    {
        Value = new BOOL();
        InitialValue = new BOOL();
        Valid = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="SEQ_BOOL"/> instance initialized with the provided element.
    /// </summary>
    public SEQ_BOOL(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>Value</c> member of the <see cref="SEQ_BOOL"/> data type.
    /// </summary>
    public BOOL Value
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InitialValue</c> member of the <see cref="SEQ_BOOL"/> data type.
    /// </summary>
    public BOOL InitialValue
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Valid</c> member of the <see cref="SEQ_BOOL"/> data type.
    /// </summary>
    public BOOL Valid
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}