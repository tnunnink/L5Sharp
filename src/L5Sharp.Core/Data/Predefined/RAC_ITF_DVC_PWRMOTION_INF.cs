using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>RAC_ITF_DVC_PWRMOTION_INF</c> data type structure.
/// </summary>
[LogixData("RAC_ITF_DVC_PWRMOTION_INF")]
public sealed partial class RAC_ITF_DVC_PWRMOTION_INF : StructureData
{
    /// <summary>
    /// Creates a new <see cref="RAC_ITF_DVC_PWRMOTION_INF"/> instance initialized with default values.
    /// </summary>
    public RAC_ITF_DVC_PWRMOTION_INF() : base("RAC_ITF_DVC_PWRMOTION_INF")
    {
        AxisID = new DINT();
        Lock = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="RAC_ITF_DVC_PWRMOTION_INF"/> instance initialized with the provided element.
    /// </summary>
    public RAC_ITF_DVC_PWRMOTION_INF(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>AxisID</c> member of the <see cref="RAC_ITF_DVC_PWRMOTION_INF"/> data type.
    /// </summary>
    public DINT AxisID
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Lock</c> member of the <see cref="RAC_ITF_DVC_PWRMOTION_INF"/> data type.
    /// </summary>
    public BOOL Lock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}