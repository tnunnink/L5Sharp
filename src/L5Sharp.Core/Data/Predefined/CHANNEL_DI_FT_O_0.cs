using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>CHANNEL_DI_FT_O_0</c> data type structure.
/// </summary>
[LogixData("CHANNEL_DI_FT:O:0")]
public sealed partial class CHANNEL_DI_FT_O_0 : StructureData
{
    /// <summary>
    /// Creates a new <see cref="CHANNEL_DI_FT_O_0"/> instance initialized with default values.
    /// </summary>
    public CHANNEL_DI_FT_O_0() : base("CHANNEL_DI_FT:O:0")
    {
        ResetFault = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="CHANNEL_DI_FT_O_0"/> instance initialized with the provided element.
    /// </summary>
    public CHANNEL_DI_FT_O_0(XElement element) : base(element)
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
        ResetFault.UpdateData((data[offset + 2] & (1 << 0)) != 0);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>ResetFault</c> member of the <see cref="CHANNEL_DI_FT_O_0"/> data type.
    /// </summary>
    public BOOL ResetFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}