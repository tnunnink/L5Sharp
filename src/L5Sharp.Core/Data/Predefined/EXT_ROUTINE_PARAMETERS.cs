using System.Xml.Linq;
// Auto-generated file
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>EXT_ROUTINE_PARAMETERS</c> data type structure.
/// </summary>
[LogixData("EXT_ROUTINE_PARAMETERS")]
public sealed partial class EXT_ROUTINE_PARAMETERS : StructureData
{
    /// <summary>
    /// Creates a new <see cref="EXT_ROUTINE_PARAMETERS"/> instance initialized with default values.
    /// </summary>
    public EXT_ROUTINE_PARAMETERS() : base("EXT_ROUTINE_PARAMETERS")
    {
        ElementSize = new DINT();
        ElementCount = new DINT();
        ParamType = new DINT();
    }

    /// <summary>
    /// Creates a new <see cref="EXT_ROUTINE_PARAMETERS"/> instance initialized with the provided element.
    /// </summary>
    public EXT_ROUTINE_PARAMETERS(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>ElementSize</c> member of the <see cref="EXT_ROUTINE_PARAMETERS"/> data type.
    /// </summary>
    public DINT ElementSize
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ElementCount</c> member of the <see cref="EXT_ROUTINE_PARAMETERS"/> data type.
    /// </summary>
    public DINT ElementCount
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ParamType</c> member of the <see cref="EXT_ROUTINE_PARAMETERS"/> data type.
    /// </summary>
    public DINT ParamType
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

}
