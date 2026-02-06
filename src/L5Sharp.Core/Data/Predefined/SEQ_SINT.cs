using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>SEQ_SINT</c> data type structure.
/// </summary>
[LogixData("SEQ_SINT")]
public sealed partial class SEQ_SINT : StructureData
{
    /// <summary>
    /// Creates a new <see cref="SEQ_SINT"/> instance initialized with default values.
    /// </summary>
    public SEQ_SINT() : base("SEQ_SINT")
    {
        Value = new SINT();
        InitialValue = new SINT();
        Valid = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="SEQ_SINT"/> instance initialized with the provided element.
    /// </summary>
    public SEQ_SINT(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 8;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        Value.UpdateData(data, offset + 0);
        InitialValue.UpdateData(data, offset + 2);
        Valid.UpdateData((data[offset + 6] & (1 << 0)) != 0);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>Value</c> member of the <see cref="SEQ_SINT"/> data type.
    /// </summary>
    public SINT Value
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InitialValue</c> member of the <see cref="SEQ_SINT"/> data type.
    /// </summary>
    public SINT InitialValue
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Valid</c> member of the <see cref="SEQ_SINT"/> data type.
    /// </summary>
    public BOOL Valid
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}