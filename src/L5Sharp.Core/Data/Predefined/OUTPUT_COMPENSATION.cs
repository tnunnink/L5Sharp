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
        Offset.UpdateData(data, offset + 0);
        LatchDelay.UpdateData(data, offset + 4);
        UnlatchDelay.UpdateData(data, offset + 8);
        Mode.UpdateData(data, offset + 12);
        CycleTime.UpdateData(data, offset + 16);
        DutyCycle.UpdateData(data, offset + 20);
        
        return offset + GetSize();
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