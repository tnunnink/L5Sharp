using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>SEQ_INT</c> data type structure.
/// </summary>
[LogixData("SEQ_INT")]
public sealed partial class SEQ_INT : StructureData
{
    /// <summary>
    /// Creates a new <see cref="SEQ_INT"/> instance initialized with default values.
    /// </summary>
    public SEQ_INT() : base("SEQ_INT")
    {
        Value = new INT();
        InitialValue = new INT();
        Valid = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="SEQ_INT"/> instance initialized with the provided element.
    /// </summary>
    public SEQ_INT(XElement element) : base(element)
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
        InitialValue.UpdateData(data, offset + 4);
        Valid.UpdateData((data[offset + 9] & (1 << 0)) != 0);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>Value</c> member of the <see cref="SEQ_INT"/> data type.
    /// </summary>
    public INT Value
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InitialValue</c> member of the <see cref="SEQ_INT"/> data type.
    /// </summary>
    public INT InitialValue
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Valid</c> member of the <see cref="SEQ_INT"/> data type.
    /// </summary>
    public BOOL Valid
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}