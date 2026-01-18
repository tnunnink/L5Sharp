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

    /// <summary>
    /// The <c>Data</c> member of the <see cref="ODOMETER"/> data type.
    /// </summary>
    public ArrayData<INT> Data
    {
        get => GetArray<INT>();
        set => SetArray(value);
    }
}