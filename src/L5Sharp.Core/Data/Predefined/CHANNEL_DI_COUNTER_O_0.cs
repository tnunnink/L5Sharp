using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>CHANNEL_DI_COUNTER_O_0</c> data type structure.
/// </summary>
[LogixData("CHANNEL_DI_COUNTER:O:0")]
public sealed partial class CHANNEL_DI_COUNTER_O_0 : StructureData
{
    /// <summary>
    /// Creates a new <see cref="CHANNEL_DI_COUNTER_O_0"/> instance initialized with default values.
    /// </summary>
    public CHANNEL_DI_COUNTER_O_0() : base("CHANNEL_DI_COUNTER:O:0")
    {
        Reset = new BOOL();
        RolloverAck = new BOOL();
        Preset = new DINT();
    }
    
    /// <summary>
    /// Creates a new <see cref="CHANNEL_DI_COUNTER_O_0"/> instance initialized with the provided element.
    /// </summary>
    public CHANNEL_DI_COUNTER_O_0(XElement element) : base(element)
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
        Reset.UpdateData((data[offset + 3] & (1 << 0)) != 0);
        RolloverAck.UpdateData((data[offset + 3] & (1 << 1)) != 0);
        Preset.UpdateData(data, offset + 5);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>Reset</c> member of the <see cref="CHANNEL_DI_COUNTER_O_0"/> data type.
    /// </summary>
    public BOOL Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RolloverAck</c> member of the <see cref="CHANNEL_DI_COUNTER_O_0"/> data type.
    /// </summary>
    public BOOL RolloverAck
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Preset</c> member of the <see cref="CHANNEL_DI_COUNTER_O_0"/> data type.
    /// </summary>
    public DINT Preset
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }
}