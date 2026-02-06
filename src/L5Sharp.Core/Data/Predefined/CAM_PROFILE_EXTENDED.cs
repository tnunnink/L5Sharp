using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

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
        SegmentType.UpdateData(data, offset + 4);
        Master.UpdateData(data, offset + 8);
        Slave.UpdateData(data, offset + 16);
        C0.UpdateData(data, offset + 24);
        C1.UpdateData(data, offset + 32);
        C2.UpdateData(data, offset + 40);
        C3.UpdateData(data, offset + 48);
        
        return offset + GetSize();
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