using System.Xml.Linq;
// Auto-generated file
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>SIGNED_ODOMETER</c> data type structure.
/// </summary>
[LogixData("SIGNED_ODOMETER")]
public sealed partial class SIGNED_ODOMETER : StructureData
{
    /// <summary>
    /// Creates a new <see cref="SIGNED_ODOMETER"/> instance initialized with default values.
    /// </summary>
    public SIGNED_ODOMETER() : base("SIGNED_ODOMETER")
    {
        Data = new INT[5];
    }

    /// <summary>
    /// Creates a new <see cref="SIGNED_ODOMETER"/> instance initialized with the provided element.
    /// </summary>
    public SIGNED_ODOMETER(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>Data</c> member of the <see cref="SIGNED_ODOMETER"/> data type.
    /// </summary>
    public INT[] Data
    {
        get => GetArray<INT>();
        set => SetArray(value);
    }

}
