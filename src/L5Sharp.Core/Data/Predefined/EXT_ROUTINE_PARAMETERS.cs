using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>EXT_ROUTINE_PARAMETERS</c> data type structure.
/// </summary>
[LogixData("EXT_ROUTINE_PARAMETERS")]
public sealed partial class EXT_ROUTINE_PARAMETERS : StructureData
{
    /// <summary>
    /// Creates a new <see cref="EXT_ROUTINE_PARAMETERS"/> instance initialized with default values.
    /// </summary>
    public EXT_ROUTINE_PARAMETERS() : base("EXT_ROUTINE_PARAMETERS")
    {
        ElementSize = new DINT();
        ElementCount = new DINT();
        ParamType = new DINT();
    }
    
    /// <summary>
    /// Creates a new <see cref="EXT_ROUTINE_PARAMETERS"/> instance initialized with the provided element.
    /// </summary>
    public EXT_ROUTINE_PARAMETERS(XElement element) : base(element)
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
        ElementSize.UpdateData(data, offset + 0);
        ElementCount.UpdateData(data, offset + 4);
        ParamType.UpdateData(data, offset + 8);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>ElementSize</c> member of the <see cref="EXT_ROUTINE_PARAMETERS"/> data type.
    /// </summary>
    public DINT ElementSize
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ElementCount</c> member of the <see cref="EXT_ROUTINE_PARAMETERS"/> data type.
    /// </summary>
    public DINT ElementCount
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ParamType</c> member of the <see cref="EXT_ROUTINE_PARAMETERS"/> data type.
    /// </summary>
    public DINT ParamType
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }
}