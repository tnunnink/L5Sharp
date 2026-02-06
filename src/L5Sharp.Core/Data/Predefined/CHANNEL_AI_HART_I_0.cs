using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>CHANNEL_AI_HART_I_0</c> data type structure.
/// </summary>
[LogixData("CHANNEL_AI_HART:I:0")]
public sealed partial class CHANNEL_AI_HART_I_0 : StructureData
{
    /// <summary>
    /// Creates a new <see cref="CHANNEL_AI_HART_I_0"/> instance initialized with default values.
    /// </summary>
    public CHANNEL_AI_HART_I_0() : base("CHANNEL_AI_HART:I:0")
    {
        Ch = new CHANNEL_AI_I_0();
        Class = new USINT();
        Unit = new USINT();
        Manual = new BOOL();
        Constant = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="CHANNEL_AI_HART_I_0"/> instance initialized with the provided element.
    /// </summary>
    public CHANNEL_AI_HART_I_0(XElement element) : base(element)
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
        Ch.UpdateData(data, offset + 0);
        Class.UpdateData(data, offset + 0);
        Unit.UpdateData(data, offset + 1);
        Manual.UpdateData((data[offset + 4] & (1 << 0)) != 0);
        Constant.UpdateData((data[offset + 4] & (1 << 1)) != 0);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>Ch</c> member of the <see cref="CHANNEL_AI_HART_I_0"/> data type.
    /// </summary>
    public CHANNEL_AI_I_0 Ch
    {
        get => GetMember<CHANNEL_AI_I_0>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Class</c> member of the <see cref="CHANNEL_AI_HART_I_0"/> data type.
    /// </summary>
    public USINT Class
    {
        get => GetMember<USINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Unit</c> member of the <see cref="CHANNEL_AI_HART_I_0"/> data type.
    /// </summary>
    public USINT Unit
    {
        get => GetMember<USINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Manual</c> member of the <see cref="CHANNEL_AI_HART_I_0"/> data type.
    /// </summary>
    public BOOL Manual
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Constant</c> member of the <see cref="CHANNEL_AI_HART_I_0"/> data type.
    /// </summary>
    public BOOL Constant
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}