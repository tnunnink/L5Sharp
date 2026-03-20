using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>OUTPUT_CAM</c> data type structure.
/// </summary>
[LogixData("OUTPUT_CAM")]
public sealed partial class OUTPUT_CAM : StructureData
{
    /// <summary>
    /// Creates a new <see cref="OUTPUT_CAM"/> instance initialized with default values.
    /// </summary>
    public OUTPUT_CAM() : base("OUTPUT_CAM")
    {
        OutputBit = new DINT();
        LatchType = new DINT();
        UnlatchType = new DINT();
        Left = new REAL();
        Right = new REAL();
        Duration = new REAL();
        EnableType = new DINT();
        EnableBit = new DINT();
    }
    
    /// <summary>
    /// Creates a new <see cref="OUTPUT_CAM"/> instance initialized with the provided element.
    /// </summary>
    public OUTPUT_CAM(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 32;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        OutputBit.UpdateData(data, offset + 0);
        LatchType.UpdateData(data, offset + 4);
        UnlatchType.UpdateData(data, offset + 8);
        Left.UpdateData(data, offset + 12);
        Right.UpdateData(data, offset + 16);
        Duration.UpdateData(data, offset + 20);
        EnableType.UpdateData(data, offset + 24);
        EnableBit.UpdateData(data, offset + 28);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>OutputBit</c> member of the <see cref="OUTPUT_CAM"/> data type.
    /// </summary>
    public DINT OutputBit
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>LatchType</c> member of the <see cref="OUTPUT_CAM"/> data type.
    /// </summary>
    public DINT LatchType
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>UnlatchType</c> member of the <see cref="OUTPUT_CAM"/> data type.
    /// </summary>
    public DINT UnlatchType
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Left</c> member of the <see cref="OUTPUT_CAM"/> data type.
    /// </summary>
    public REAL Left
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Right</c> member of the <see cref="OUTPUT_CAM"/> data type.
    /// </summary>
    public REAL Right
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Duration</c> member of the <see cref="OUTPUT_CAM"/> data type.
    /// </summary>
    public REAL Duration
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableType</c> member of the <see cref="OUTPUT_CAM"/> data type.
    /// </summary>
    public DINT EnableType
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableBit</c> member of the <see cref="OUTPUT_CAM"/> data type.
    /// </summary>
    public DINT EnableBit
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }
}