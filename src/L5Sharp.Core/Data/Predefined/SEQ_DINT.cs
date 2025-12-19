using System.Xml.Linq;
// Auto-generated file
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>SEQ_DINT</c> data type structure.
/// </summary>
[LogixData("SEQ_DINT")]
public sealed partial class SEQ_DINT : StructureData
{
    /// <summary>
    /// Creates a new <see cref="SEQ_DINT"/> instance initialized with default values.
    /// </summary>
    public SEQ_DINT() : base("SEQ_DINT")
    {
        Value = new DINT();
        InitialValue = new DINT();
        Valid = new BOOL();
    }

    /// <summary>
    /// Creates a new <see cref="SEQ_DINT"/> instance initialized with the provided element.
    /// </summary>
    public SEQ_DINT(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>Value</c> member of the <see cref="SEQ_DINT"/> data type.
    /// </summary>
    public DINT Value
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InitialValue</c> member of the <see cref="SEQ_DINT"/> data type.
    /// </summary>
    public DINT InitialValue
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Valid</c> member of the <see cref="SEQ_DINT"/> data type.
    /// </summary>
    public BOOL Valid
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

}
