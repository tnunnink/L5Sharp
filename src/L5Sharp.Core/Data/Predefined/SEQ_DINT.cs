using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>SEQ_DINT</c> data type structure.
/// </summary>
[LogixData("SEQ_DINT")]
public sealed partial class SEQ_DINT : StructureData
{
    /// <summary>
    /// Creates a new <see cref="SEQ_DINT"/> instance initialized with default values.
    /// </summary>
    public SEQ_DINT() : base("SEQ_DINT")
    {
        Value = new DINT();
        InitialValue = new DINT();
        Valid = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="SEQ_DINT"/> instance initialized with the provided element.
    /// </summary>
    public SEQ_DINT(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 16;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        Value.UpdateData(data, offset + 0);
        InitialValue.UpdateData(data, offset + 8);
        Valid.UpdateData((data[offset + 15] & (1 << 0)) != 0);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>Value</c> member of the <see cref="SEQ_DINT"/> data type.
    /// </summary>
    public DINT Value
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InitialValue</c> member of the <see cref="SEQ_DINT"/> data type.
    /// </summary>
    public DINT InitialValue
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Valid</c> member of the <see cref="SEQ_DINT"/> data type.
    /// </summary>
    public BOOL Valid
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}