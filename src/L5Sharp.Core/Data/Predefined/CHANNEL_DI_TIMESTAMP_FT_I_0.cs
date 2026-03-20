using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>CHANNEL_DI_TIMESTAMP_FT_I_0</c> data type structure.
/// </summary>
[LogixData("CHANNEL_DI_TIMESTAMP_FT:I:0")]
public sealed partial class CHANNEL_DI_TIMESTAMP_FT_I_0 : StructureData
{
    /// <summary>
    /// Creates a new <see cref="CHANNEL_DI_TIMESTAMP_FT_I_0"/> instance initialized with default values.
    /// </summary>
    public CHANNEL_DI_TIMESTAMP_FT_I_0() : base("CHANNEL_DI_TIMESTAMP_FT:I:0")
    {
        Data = new BOOL();
        Fault = new BOOL();
        Uncertain = new BOOL();
        OpenWire = new BOOL();
        ShortCircuit = new BOOL();
        Chatter = new BOOL();
        FieldPowerOff = new BOOL();
        Indeterminate = new BOOL();
        TimestampOverflowOffOn = new BOOL();
        TimestampOverflowOnOff = new BOOL();
        CIPSyncValid = new BOOL();
        CIPSyncTimeout = new BOOL();
        TimestampOffOnNumber = new INT();
        TimestampOnOffNumber = new INT();
        TimestampOffOn = new LINT();
        TimestampOnOff = new LINT();
    }
    
    /// <summary>
    /// Creates a new <see cref="CHANNEL_DI_TIMESTAMP_FT_I_0"/> instance initialized with the provided element.
    /// </summary>
    public CHANNEL_DI_TIMESTAMP_FT_I_0(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 24;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        Data.UpdateData((data[offset + 3] & (1 << 0)) != 0);
        Fault.UpdateData((data[offset + 3] & (1 << 1)) != 0);
        Uncertain.UpdateData((data[offset + 3] & (1 << 2)) != 0);
        OpenWire.UpdateData((data[offset + 3] & (1 << 3)) != 0);
        ShortCircuit.UpdateData((data[offset + 3] & (1 << 4)) != 0);
        Chatter.UpdateData((data[offset + 3] & (1 << 5)) != 0);
        FieldPowerOff.UpdateData((data[offset + 3] & (1 << 6)) != 0);
        Indeterminate.UpdateData((data[offset + 3] & (1 << 7)) != 0);
        TimestampOverflowOffOn.UpdateData((data[offset + 4] & (1 << 0)) != 0);
        TimestampOverflowOnOff.UpdateData((data[offset + 4] & (1 << 1)) != 0);
        CIPSyncValid.UpdateData((data[offset + 4] & (1 << 2)) != 0);
        CIPSyncTimeout.UpdateData((data[offset + 4] & (1 << 3)) != 0);
        TimestampOffOnNumber.UpdateData(data, offset + 6);
        TimestampOnOffNumber.UpdateData(data, offset + 8);
        TimestampOffOn.UpdateData(data, offset + 10);
        TimestampOnOff.UpdateData(data, offset + 18);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>Data</c> member of the <see cref="CHANNEL_DI_TIMESTAMP_FT_I_0"/> data type.
    /// </summary>
    public BOOL Data
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Fault</c> member of the <see cref="CHANNEL_DI_TIMESTAMP_FT_I_0"/> data type.
    /// </summary>
    public BOOL Fault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Uncertain</c> member of the <see cref="CHANNEL_DI_TIMESTAMP_FT_I_0"/> data type.
    /// </summary>
    public BOOL Uncertain
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OpenWire</c> member of the <see cref="CHANNEL_DI_TIMESTAMP_FT_I_0"/> data type.
    /// </summary>
    public BOOL OpenWire
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ShortCircuit</c> member of the <see cref="CHANNEL_DI_TIMESTAMP_FT_I_0"/> data type.
    /// </summary>
    public BOOL ShortCircuit
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Chatter</c> member of the <see cref="CHANNEL_DI_TIMESTAMP_FT_I_0"/> data type.
    /// </summary>
    public BOOL Chatter
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FieldPowerOff</c> member of the <see cref="CHANNEL_DI_TIMESTAMP_FT_I_0"/> data type.
    /// </summary>
    public BOOL FieldPowerOff
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Indeterminate</c> member of the <see cref="CHANNEL_DI_TIMESTAMP_FT_I_0"/> data type.
    /// </summary>
    public BOOL Indeterminate
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TimestampOverflowOffOn</c> member of the <see cref="CHANNEL_DI_TIMESTAMP_FT_I_0"/> data type.
    /// </summary>
    public BOOL TimestampOverflowOffOn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TimestampOverflowOnOff</c> member of the <see cref="CHANNEL_DI_TIMESTAMP_FT_I_0"/> data type.
    /// </summary>
    public BOOL TimestampOverflowOnOff
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CIPSyncValid</c> member of the <see cref="CHANNEL_DI_TIMESTAMP_FT_I_0"/> data type.
    /// </summary>
    public BOOL CIPSyncValid
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CIPSyncTimeout</c> member of the <see cref="CHANNEL_DI_TIMESTAMP_FT_I_0"/> data type.
    /// </summary>
    public BOOL CIPSyncTimeout
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TimestampOffOnNumber</c> member of the <see cref="CHANNEL_DI_TIMESTAMP_FT_I_0"/> data type.
    /// </summary>
    public INT TimestampOffOnNumber
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TimestampOnOffNumber</c> member of the <see cref="CHANNEL_DI_TIMESTAMP_FT_I_0"/> data type.
    /// </summary>
    public INT TimestampOnOffNumber
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TimestampOffOn</c> member of the <see cref="CHANNEL_DI_TIMESTAMP_FT_I_0"/> data type.
    /// </summary>
    public LINT TimestampOffOn
    {
        get => GetMember<LINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TimestampOnOff</c> member of the <see cref="CHANNEL_DI_TIMESTAMP_FT_I_0"/> data type.
    /// </summary>
    public LINT TimestampOnOff
    {
        get => GetMember<LINT>();
        set => SetMember(value);
    }
}