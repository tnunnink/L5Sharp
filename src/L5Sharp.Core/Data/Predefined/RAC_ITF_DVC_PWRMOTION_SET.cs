using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>RAC_ITF_DVC_PWRMOTION_SET</c> data type structure.
/// </summary>
[LogixData("RAC_ITF_DVC_PWRMOTION_SET")]
public sealed partial class RAC_ITF_DVC_PWRMOTION_SET : StructureData
{
    /// <summary>
    /// Creates a new <see cref="RAC_ITF_DVC_PWRMOTION_SET"/> instance initialized with default values.
    /// </summary>
    public RAC_ITF_DVC_PWRMOTION_SET() : base("RAC_ITF_DVC_PWRMOTION_SET")
    {
        InhibitCmd = new BOOL();
        InhibitSet = new BOOL();
        Lock = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="RAC_ITF_DVC_PWRMOTION_SET"/> instance initialized with the provided element.
    /// </summary>
    public RAC_ITF_DVC_PWRMOTION_SET(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 4;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        InhibitCmd.UpdateData((data[offset + 3] & (1 << 0)) != 0);
        InhibitSet.UpdateData((data[offset + 3] & (1 << 1)) != 0);
        Lock.UpdateData((data[offset + 3] & (1 << 2)) != 0);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>InhibitCmd</c> member of the <see cref="RAC_ITF_DVC_PWRMOTION_SET"/> data type.
    /// </summary>
    public BOOL InhibitCmd
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InhibitSet</c> member of the <see cref="RAC_ITF_DVC_PWRMOTION_SET"/> data type.
    /// </summary>
    public BOOL InhibitSet
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Lock</c> member of the <see cref="RAC_ITF_DVC_PWRMOTION_SET"/> data type.
    /// </summary>
    public BOOL Lock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}