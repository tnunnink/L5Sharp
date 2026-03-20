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
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 24;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        Master.UpdateData(data, offset + 0);
        Slave.UpdateData(data, offset + 8);
        SegmentType.UpdateData(data, offset + 16);
        
        return offset + GetSize();
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