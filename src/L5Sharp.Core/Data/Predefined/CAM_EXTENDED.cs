using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>CAM_EXTENDED</c> data type structure.
/// </summary>
[LogixData("CAM_EXTENDED")]
public sealed partial class CAM_EXTENDED : StructureData
{
    /// <summary>
    /// Creates a new <see cref="CAM_EXTENDED"/> instance initialized with default values.
    /// </summary>
    public CAM_EXTENDED() : base("CAM_EXTENDED")
    {
        Master = new LREAL();
        Slave = new LREAL();
        SegmentType = new DINT();
    }
    
    /// <summary>
    /// Creates a new <see cref="CAM_EXTENDED"/> instance initialized with the provided element.
    /// </summary>
    public CAM_EXTENDED(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>Master</c> member of the <see cref="CAM_EXTENDED"/> data type.
    /// </summary>
    public LREAL Master
    {
        get => GetMember<LREAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Slave</c> member of the <see cref="CAM_EXTENDED"/> data type.
    /// </summary>
    public LREAL Slave
    {
        get => GetMember<LREAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SegmentType</c> member of the <see cref="CAM_EXTENDED"/> data type.
    /// </summary>
    public DINT SegmentType
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }
}