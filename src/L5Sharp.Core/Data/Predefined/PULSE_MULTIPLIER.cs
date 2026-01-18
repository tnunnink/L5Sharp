using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>PULSE_MULTIPLIER</c> data type structure.
/// </summary>
[LogixData("PULSE_MULTIPLIER")]
public sealed partial class PULSE_MULTIPLIER : StructureData
{
    /// <summary>
    /// Creates a new <see cref="PULSE_MULTIPLIER"/> instance initialized with default values.
    /// </summary>
    public PULSE_MULTIPLIER() : base("PULSE_MULTIPLIER")
    {
        EnableIn = new BOOL();
        In = new DINT();
        Initialize = new BOOL();
        InitialValue = new DINT();
        Mode = new BOOL();
        WordSize = new DINT();
        Multiplier = new DINT();
        EnableOut = new BOOL();
        Out = new REAL();
        Status = new DINT();
        InstructFault = new BOOL();
        WordSizeInv = new BOOL();
        OutOverflow = new BOOL();
        LostPrecision = new BOOL();
        MultiplierInv = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="PULSE_MULTIPLIER"/> instance initialized with the provided element.
    /// </summary>
    public PULSE_MULTIPLIER(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="PULSE_MULTIPLIER"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>In</c> member of the <see cref="PULSE_MULTIPLIER"/> data type.
    /// </summary>
    public DINT In
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Initialize</c> member of the <see cref="PULSE_MULTIPLIER"/> data type.
    /// </summary>
    public BOOL Initialize
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InitialValue</c> member of the <see cref="PULSE_MULTIPLIER"/> data type.
    /// </summary>
    public DINT InitialValue
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Mode</c> member of the <see cref="PULSE_MULTIPLIER"/> data type.
    /// </summary>
    public BOOL Mode
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>WordSize</c> member of the <see cref="PULSE_MULTIPLIER"/> data type.
    /// </summary>
    public DINT WordSize
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Multiplier</c> member of the <see cref="PULSE_MULTIPLIER"/> data type.
    /// </summary>
    public DINT Multiplier
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="PULSE_MULTIPLIER"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out</c> member of the <see cref="PULSE_MULTIPLIER"/> data type.
    /// </summary>
    public REAL Out
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Status</c> member of the <see cref="PULSE_MULTIPLIER"/> data type.
    /// </summary>
    public DINT Status
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InstructFault</c> member of the <see cref="PULSE_MULTIPLIER"/> data type.
    /// </summary>
    public BOOL InstructFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>WordSizeInv</c> member of the <see cref="PULSE_MULTIPLIER"/> data type.
    /// </summary>
    public BOOL WordSizeInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OutOverflow</c> member of the <see cref="PULSE_MULTIPLIER"/> data type.
    /// </summary>
    public BOOL OutOverflow
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>LostPrecision</c> member of the <see cref="PULSE_MULTIPLIER"/> data type.
    /// </summary>
    public BOOL LostPrecision
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>MultiplierInv</c> member of the <see cref="PULSE_MULTIPLIER"/> data type.
    /// </summary>
    public BOOL MultiplierInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}