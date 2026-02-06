using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>CAM_PROFILE</c> data type structure.
/// </summary>
[LogixData("CAM_PROFILE")]
public sealed partial class CAM_PROFILE : StructureData
{
    /// <summary>
    /// Creates a new <see cref="CAM_PROFILE"/> instance initialized with default values.
    /// </summary>
    public CAM_PROFILE() : base("CAM_PROFILE")
    {
        Status = new DINT();
    }
    
    /// <summary>
    /// Creates a new <see cref="CAM_PROFILE"/> instance initialized with the provided element.
    /// </summary>
    public CAM_PROFILE(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 56;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        Status.UpdateData(data, offset + 0);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>Status</c> member of the <see cref="CAM_PROFILE"/> data type.
    /// </summary>
    public DINT Status
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }
}