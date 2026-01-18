using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>CHANNEL_DO_O_0</c> data type structure.
/// </summary>
[LogixData("CHANNEL_DO:O:0")]
public sealed partial class CHANNEL_DO_O_0 : StructureData
{
    /// <summary>
    /// Creates a new <see cref="CHANNEL_DO_O_0"/> instance initialized with default values.
    /// </summary>
    public CHANNEL_DO_O_0() : base("CHANNEL_DO:O:0")
    {
        Data = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="CHANNEL_DO_O_0"/> instance initialized with the provided element.
    /// </summary>
    public CHANNEL_DO_O_0(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>Data</c> member of the <see cref="CHANNEL_DO_O_0"/> data type.
    /// </summary>
    public BOOL Data
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}