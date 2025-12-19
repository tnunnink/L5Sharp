using System.Xml.Linq;
// Auto-generated file
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>CAM_PROFILE_EXTENDED</c> data type structure.
/// </summary>
[LogixData("CAM_PROFILE_EXTENDED")]
public sealed partial class CAM_PROFILE_EXTENDED : StructureData
{
    /// <summary>
    /// Creates a new <see cref="CAM_PROFILE_EXTENDED"/> instance initialized with default values.
    /// </summary>
    public CAM_PROFILE_EXTENDED() : base("CAM_PROFILE_EXTENDED")
    {
        Status = new DINT();
        SegmentType = new DINT();
        Master = new LREAL();
        Slave = new LREAL();
        C0 = new LREAL();
        C1 = new LREAL();
        C2 = new LREAL();
        C3 = new LREAL();
    }

    /// <summary>
    /// Creates a new <see cref="CAM_PROFILE_EXTENDED"/> instance initialized with the provided element.
    /// </summary>
    public CAM_PROFILE_EXTENDED(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>Status</c> member of the <see cref="CAM_PROFILE_EXTENDED"/> data type.
    /// </summary>
    public DINT Status
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SegmentType</c> member of the <see cref="CAM_PROFILE_EXTENDED"/> data type.
    /// </summary>
    public DINT SegmentType
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Master</c> member of the <see cref="CAM_PROFILE_EXTENDED"/> data type.
    /// </summary>
    public LREAL Master
    {
        get => GetMember<LREAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Slave</c> member of the <see cref="CAM_PROFILE_EXTENDED"/> data type.
    /// </summary>
    public LREAL Slave
    {
        get => GetMember<LREAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>C0</c> member of the <see cref="CAM_PROFILE_EXTENDED"/> data type.
    /// </summary>
    public LREAL C0
    {
        get => GetMember<LREAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>C1</c> member of the <see cref="CAM_PROFILE_EXTENDED"/> data type.
    /// </summary>
    public LREAL C1
    {
        get => GetMember<LREAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>C2</c> member of the <see cref="CAM_PROFILE_EXTENDED"/> data type.
    /// </summary>
    public LREAL C2
    {
        get => GetMember<LREAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>C3</c> member of the <see cref="CAM_PROFILE_EXTENDED"/> data type.
    /// </summary>
    public LREAL C3
    {
        get => GetMember<LREAL>();
        set => SetMember(value);
    }

}
