using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>CAM</c> data type structure.
/// </summary>
[LogixData("CAM")]
public sealed partial class CAM : StructureData
{
    /// <summary>
    /// Creates a new <see cref="CAM"/> instance initialized with default values.
    /// </summary>
    public CAM() : base("CAM")
    {
        Master = new REAL();
        Slave = new REAL();
        SegmentType = new DINT();
    }
    
    /// <summary>
    /// Creates a new <see cref="CAM"/> instance initialized with the provided element.
    /// </summary>
    public CAM(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 12;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        Master.UpdateData(data, offset + 0);
        Slave.UpdateData(data, offset + 4);
        SegmentType.UpdateData(data, offset + 8);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>Master</c> member of the <see cref="CAM"/> data type.
    /// </summary>
    public REAL Master
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Slave</c> member of the <see cref="CAM"/> data type.
    /// </summary>
    public REAL Slave
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SegmentType</c> member of the <see cref="CAM"/> data type.
    /// </summary>
    public DINT SegmentType
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }
}