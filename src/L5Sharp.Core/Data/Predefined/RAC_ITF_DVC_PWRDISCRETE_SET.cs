using System.Xml.Linq;
// Auto-generated file
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>RAC_ITF_DVC_PWRDISCRETE_SET</c> data type structure.
/// </summary>
[LogixData("RAC_ITF_DVC_PWRDISCRETE_SET")]
public sealed partial class RAC_ITF_DVC_PWRDISCRETE_SET : StructureData
{
    /// <summary>
    /// Creates a new <see cref="RAC_ITF_DVC_PWRDISCRETE_SET"/> instance initialized with default values.
    /// </summary>
    public RAC_ITF_DVC_PWRDISCRETE_SET() : base("RAC_ITF_DVC_PWRDISCRETE_SET")
    {
        InhibitCmd = new BOOL();
        InhibitSet = new BOOL();
    }

    /// <summary>
    /// Creates a new <see cref="RAC_ITF_DVC_PWRDISCRETE_SET"/> instance initialized with the provided element.
    /// </summary>
    public RAC_ITF_DVC_PWRDISCRETE_SET(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>InhibitCmd</c> member of the <see cref="RAC_ITF_DVC_PWRDISCRETE_SET"/> data type.
    /// </summary>
    public BOOL InhibitCmd
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InhibitSet</c> member of the <see cref="RAC_ITF_DVC_PWRDISCRETE_SET"/> data type.
    /// </summary>
    public BOOL InhibitSet
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

}
