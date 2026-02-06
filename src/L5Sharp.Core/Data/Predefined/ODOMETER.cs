using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>ODOMETER</c> data type structure.
/// </summary>
[LogixData("ODOMETER")]
public sealed partial class ODOMETER : StructureData
{
    /// <summary>
    /// Creates a new <see cref="ODOMETER"/> instance initialized with default values.
    /// </summary>
    public ODOMETER() : base("ODOMETER")
    {
        Data = new ArrayData<INT>(5);
    }
    
    /// <summary>
    /// Creates a new <see cref="ODOMETER"/> instance initialized with the provided element.
    /// </summary>
    public ODOMETER(XElement element) : base(element)
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
        Data.UpdateData(data, offset + 0);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>Data</c> member of the <see cref="ODOMETER"/> data type.
    /// </summary>
    public ArrayData<INT> Data
    {
        get => GetArray<INT>();
        set => SetArray(value);
    }
}