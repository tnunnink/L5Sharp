using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>OUTPUT_COMPENSATION</c> data type structure.
/// </summary>
[LogixData("OUTPUT_COMPENSATION")]
public sealed partial class OUTPUT_COMPENSATION : StructureData
{
    /// <summary>
    /// Creates a new <see cref="OUTPUT_COMPENSATION"/> instance initialized with default values.
    /// </summary>
    public OUTPUT_COMPENSATION() : base("OUTPUT_COMPENSATION")
    {
        Offset = new REAL();
        LatchDelay = new REAL();
        UnlatchDelay = new REAL();
        Mode = new DINT();
        CycleTime = new REAL();
        DutyCycle = new REAL();
    }
    
    /// <summary>
    /// Creates a new <see cref="OUTPUT_COMPENSATION"/> instance initialized with the provided element.
    /// </summary>
    public OUTPUT_COMPENSATION(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>Offset</c> member of the <see cref="OUTPUT_COMPENSATION"/> data type.
    /// </summary>
    public REAL Offset
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>LatchDelay</c> member of the <see cref="OUTPUT_COMPENSATION"/> data type.
    /// </summary>
    public REAL LatchDelay
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>UnlatchDelay</c> member of the <see cref="OUTPUT_COMPENSATION"/> data type.
    /// </summary>
    public REAL UnlatchDelay
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Mode</c> member of the <see cref="OUTPUT_COMPENSATION"/> data type.
    /// </summary>
    public DINT Mode
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CycleTime</c> member of the <see cref="OUTPUT_COMPENSATION"/> data type.
    /// </summary>
    public REAL CycleTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DutyCycle</c> member of the <see cref="OUTPUT_COMPENSATION"/> data type.
    /// </summary>
    public REAL DutyCycle
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }
}