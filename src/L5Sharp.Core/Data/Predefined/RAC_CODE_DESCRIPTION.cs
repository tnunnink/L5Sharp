using System.Xml.Linq;
// Auto-generated file
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>RAC_CODE_DESCRIPTION</c> data type structure.
/// </summary>
[LogixData("RAC_CODE_DESCRIPTION")]
public sealed partial class RAC_CODE_DESCRIPTION : StructureData
{
    /// <summary>
    /// Creates a new <see cref="RAC_CODE_DESCRIPTION"/> instance initialized with default values.
    /// </summary>
    public RAC_CODE_DESCRIPTION() : base("RAC_CODE_DESCRIPTION")
    {
        Code = new DINT();
        Desc = new STRING();
    }

    /// <summary>
    /// Creates a new <see cref="RAC_CODE_DESCRIPTION"/> instance initialized with the provided element.
    /// </summary>
    public RAC_CODE_DESCRIPTION(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>Code</c> member of the <see cref="RAC_CODE_DESCRIPTION"/> data type.
    /// </summary>
    public DINT Code
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Desc</c> member of the <see cref="RAC_CODE_DESCRIPTION"/> data type.
    /// </summary>
    public STRING Desc
    {
        get => GetMember<STRING>();
        set => SetMember(value);
    }

}
