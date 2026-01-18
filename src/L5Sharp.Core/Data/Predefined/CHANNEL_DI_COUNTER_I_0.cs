using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>CHANNEL_DI_COUNTER_I_0</c> data type structure.
/// </summary>
[LogixData("CHANNEL_DI_COUNTER:I:0")]
public sealed partial class CHANNEL_DI_COUNTER_I_0 : StructureData
{
    /// <summary>
    /// Creates a new <see cref="CHANNEL_DI_COUNTER_I_0"/> instance initialized with default values.
    /// </summary>
    public CHANNEL_DI_COUNTER_I_0() : base("CHANNEL_DI_COUNTER:I:0")
    {
        Data = new BOOL();
        Fault = new BOOL();
        Uncertain = new BOOL();
        Done = new BOOL();
        Rollover = new BOOL();
        Count = new DINT();
    }
    
    /// <summary>
    /// Creates a new <see cref="CHANNEL_DI_COUNTER_I_0"/> instance initialized with the provided element.
    /// </summary>
    public CHANNEL_DI_COUNTER_I_0(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>Data</c> member of the <see cref="CHANNEL_DI_COUNTER_I_0"/> data type.
    /// </summary>
    public BOOL Data
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Fault</c> member of the <see cref="CHANNEL_DI_COUNTER_I_0"/> data type.
    /// </summary>
    public BOOL Fault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Uncertain</c> member of the <see cref="CHANNEL_DI_COUNTER_I_0"/> data type.
    /// </summary>
    public BOOL Uncertain
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Done</c> member of the <see cref="CHANNEL_DI_COUNTER_I_0"/> data type.
    /// </summary>
    public BOOL Done
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Rollover</c> member of the <see cref="CHANNEL_DI_COUNTER_I_0"/> data type.
    /// </summary>
    public BOOL Rollover
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Count</c> member of the <see cref="CHANNEL_DI_COUNTER_I_0"/> data type.
    /// </summary>
    public new DINT Count
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }
}