using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>ALARM_SET_CONTROL</c> data type structure.
/// </summary>
[LogixData("ALARM_SET_CONTROL")]
public sealed partial class ALARM_SET_CONTROL : StructureData
{
    /// <summary>
    /// Creates a new <see cref="ALARM_SET_CONTROL"/> instance initialized with default values.
    /// </summary>
    public ALARM_SET_CONTROL() : base("ALARM_SET_CONTROL")
    {
        EnableIn = new BOOL();
        EnableOut = new BOOL();
        LastState = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="ALARM_SET_CONTROL"/> instance initialized with the provided element.
    /// </summary>
    public ALARM_SET_CONTROL(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="ALARM_SET_CONTROL"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="ALARM_SET_CONTROL"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>LastState</c> member of the <see cref="ALARM_SET_CONTROL"/> data type.
    /// </summary>
    public BOOL LastState
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}