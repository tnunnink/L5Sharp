using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>CHANNEL_AI_NAMUR_I_0</c> data type structure.
/// </summary>
[LogixData("CHANNEL_AI_NAMUR:I:0")]
public sealed partial class CHANNEL_AI_NAMUR_I_0 : StructureData
{
    /// <summary>
    /// Creates a new <see cref="CHANNEL_AI_NAMUR_I_0"/> instance initialized with default values.
    /// </summary>
    public CHANNEL_AI_NAMUR_I_0() : base("CHANNEL_AI_NAMUR:I:0")
    {
        Fault = new BOOL();
        Uncertain = new BOOL();
        Underrange = new BOOL();
        Overrange = new BOOL();
        LowSaturation = new BOOL();
        HighSaturation = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="CHANNEL_AI_NAMUR_I_0"/> instance initialized with the provided element.
    /// </summary>
    public CHANNEL_AI_NAMUR_I_0(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>Fault</c> member of the <see cref="CHANNEL_AI_NAMUR_I_0"/> data type.
    /// </summary>
    public BOOL Fault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Uncertain</c> member of the <see cref="CHANNEL_AI_NAMUR_I_0"/> data type.
    /// </summary>
    public BOOL Uncertain
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Underrange</c> member of the <see cref="CHANNEL_AI_NAMUR_I_0"/> data type.
    /// </summary>
    public BOOL Underrange
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Overrange</c> member of the <see cref="CHANNEL_AI_NAMUR_I_0"/> data type.
    /// </summary>
    public BOOL Overrange
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>LowSaturation</c> member of the <see cref="CHANNEL_AI_NAMUR_I_0"/> data type.
    /// </summary>
    public BOOL LowSaturation
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HighSaturation</c> member of the <see cref="CHANNEL_AI_NAMUR_I_0"/> data type.
    /// </summary>
    public BOOL HighSaturation
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}