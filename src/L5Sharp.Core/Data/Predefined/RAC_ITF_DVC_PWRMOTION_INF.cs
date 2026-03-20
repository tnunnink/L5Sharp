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
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 8;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        AxisID.UpdateData(data, offset + 0);
        Lock.UpdateData((data[offset + 6] & (1 << 0)) != 0);
        
        return offset + GetSize();
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