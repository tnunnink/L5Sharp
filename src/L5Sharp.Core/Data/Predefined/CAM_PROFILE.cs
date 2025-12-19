using System.Xml.Linq;
// Auto-generated file
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart

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

    /// <summary>
    /// The <c>Status</c> member of the <see cref="CAM_PROFILE"/> data type.
    /// </summary>
    public DINT Status
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

}
