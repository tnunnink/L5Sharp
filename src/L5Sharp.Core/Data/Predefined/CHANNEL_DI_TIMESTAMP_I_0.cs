using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>CHANNEL_DI_TIMESTAMP_I_0</c> data type structure.
/// </summary>
[LogixData("CHANNEL_DI_TIMESTAMP:I:0")]
public sealed partial class CHANNEL_DI_TIMESTAMP_I_0 : StructureData
{
    /// <summary>
    /// Creates a new <see cref="CHANNEL_DI_TIMESTAMP_I_0"/> instance initialized with default values.
    /// </summary>
    public CHANNEL_DI_TIMESTAMP_I_0() : base("CHANNEL_DI_TIMESTAMP:I:0")
    {
        Data = new BOOL();
        Fault = new BOOL();
        Uncertain = new BOOL();
        Chatter = new BOOL();
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
    /// Creates a new <see cref="CHANNEL_DI_TIMESTAMP_I_0"/> instance initialized with the provided element.
    /// </summary>
    public CHANNEL_DI_TIMESTAMP_I_0(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>Data</c> member of the <see cref="CHANNEL_DI_TIMESTAMP_I_0"/> data type.
    /// </summary>
    public BOOL Data
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Fault</c> member of the <see cref="CHANNEL_DI_TIMESTAMP_I_0"/> data type.
    /// </summary>
    public BOOL Fault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Uncertain</c> member of the <see cref="CHANNEL_DI_TIMESTAMP_I_0"/> data type.
    /// </summary>
    public BOOL Uncertain
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Chatter</c> member of the <see cref="CHANNEL_DI_TIMESTAMP_I_0"/> data type.
    /// </summary>
    public BOOL Chatter
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TimestampOverflowOffOn</c> member of the <see cref="CHANNEL_DI_TIMESTAMP_I_0"/> data type.
    /// </summary>
    public BOOL TimestampOverflowOffOn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TimestampOverflowOnOff</c> member of the <see cref="CHANNEL_DI_TIMESTAMP_I_0"/> data type.
    /// </summary>
    public BOOL TimestampOverflowOnOff
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CIPSyncValid</c> member of the <see cref="CHANNEL_DI_TIMESTAMP_I_0"/> data type.
    /// </summary>
    public BOOL CIPSyncValid
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CIPSyncTimeout</c> member of the <see cref="CHANNEL_DI_TIMESTAMP_I_0"/> data type.
    /// </summary>
    public BOOL CIPSyncTimeout
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TimestampOffOnNumber</c> member of the <see cref="CHANNEL_DI_TIMESTAMP_I_0"/> data type.
    /// </summary>
    public INT TimestampOffOnNumber
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TimestampOnOffNumber</c> member of the <see cref="CHANNEL_DI_TIMESTAMP_I_0"/> data type.
    /// </summary>
    public INT TimestampOnOffNumber
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TimestampOffOn</c> member of the <see cref="CHANNEL_DI_TIMESTAMP_I_0"/> data type.
    /// </summary>
    public LINT TimestampOffOn
    {
        get => GetMember<LINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TimestampOnOff</c> member of the <see cref="CHANNEL_DI_TIMESTAMP_I_0"/> data type.
    /// </summary>
    public LINT TimestampOnOff
    {
        get => GetMember<LINT>();
        set => SetMember(value);
    }
}