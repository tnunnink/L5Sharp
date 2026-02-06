using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>CHANNEL_AI_CJ_DIAG_I_0</c> data type structure.
/// </summary>
[LogixData("CHANNEL_AI_CJ_DIAG:I:0")]
public sealed partial class CHANNEL_AI_CJ_DIAG_I_0 : StructureData
{
    /// <summary>
    /// Creates a new <see cref="CHANNEL_AI_CJ_DIAG_I_0"/> instance initialized with default values.
    /// </summary>
    public CHANNEL_AI_CJ_DIAG_I_0() : base("CHANNEL_AI_CJ_DIAG:I:0")
    {
        Fault = new BOOL();
        Uncertain = new BOOL();
        OpenWire = new BOOL();
        FieldPowerOff = new BOOL();
        Underrange = new BOOL();
        Overrange = new BOOL();
        Temperature = new REAL();
    }
    
    /// <summary>
    /// Creates a new <see cref="CHANNEL_AI_CJ_DIAG_I_0"/> instance initialized with the provided element.
    /// </summary>
    public CHANNEL_AI_CJ_DIAG_I_0(XElement element) : base(element)
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
        Fault.UpdateData((data[offset + 3] & (1 << 0)) != 0);
        Uncertain.UpdateData((data[offset + 3] & (1 << 1)) != 0);
        OpenWire.UpdateData((data[offset + 3] & (1 << 2)) != 0);
        FieldPowerOff.UpdateData((data[offset + 3] & (1 << 3)) != 0);
        Underrange.UpdateData((data[offset + 5] & (1 << 4)) != 0);
        Overrange.UpdateData((data[offset + 5] & (1 << 5)) != 0);
        Temperature.UpdateData(data, offset + 5);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>Fault</c> member of the <see cref="CHANNEL_AI_CJ_DIAG_I_0"/> data type.
    /// </summary>
    public BOOL Fault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Uncertain</c> member of the <see cref="CHANNEL_AI_CJ_DIAG_I_0"/> data type.
    /// </summary>
    public BOOL Uncertain
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OpenWire</c> member of the <see cref="CHANNEL_AI_CJ_DIAG_I_0"/> data type.
    /// </summary>
    public BOOL OpenWire
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FieldPowerOff</c> member of the <see cref="CHANNEL_AI_CJ_DIAG_I_0"/> data type.
    /// </summary>
    public BOOL FieldPowerOff
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Underrange</c> member of the <see cref="CHANNEL_AI_CJ_DIAG_I_0"/> data type.
    /// </summary>
    public BOOL Underrange
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Overrange</c> member of the <see cref="CHANNEL_AI_CJ_DIAG_I_0"/> data type.
    /// </summary>
    public BOOL Overrange
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Temperature</c> member of the <see cref="CHANNEL_AI_CJ_DIAG_I_0"/> data type.
    /// </summary>
    public REAL Temperature
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }
}