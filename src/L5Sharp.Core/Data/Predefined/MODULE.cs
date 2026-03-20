using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>MODULE</c> data type structure.
/// </summary>
[LogixData("MODULE")]
public sealed partial class MODULE : StructureData
{
    /// <summary>
    /// Creates a new <see cref="MODULE"/> instance initialized with default values.
    /// </summary>
    public MODULE() : base("MODULE")
    {
        
    }
    
    /// <summary>
    /// Creates a new <see cref="MODULE"/> instance initialized with the provided element.
    /// </summary>
    public MODULE(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 4;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        
        return offset + GetSize();
    }


}