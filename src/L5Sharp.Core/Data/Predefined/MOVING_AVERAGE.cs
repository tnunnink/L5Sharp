using System.Xml.Linq;
// Auto-generated file
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>MOVING_AVERAGE</c> data type structure.
/// </summary>
[LogixData("MOVING_AVERAGE")]
public sealed partial class MOVING_AVERAGE : StructureData
{
    /// <summary>
    /// Creates a new <see cref="MOVING_AVERAGE"/> instance initialized with default values.
    /// </summary>
    public MOVING_AVERAGE() : base("MOVING_AVERAGE")
    {
        EnableIn = new BOOL();
        In = new REAL();
        InFault = new BOOL();
        Initialize = new BOOL();
        SampleEnable = new BOOL();
        NumberOfSamples = new DINT();
        UseWeights = new BOOL();
        EnableOut = new BOOL();
        Out = new REAL();
        Status = new DINT();
        InstructFault = new BOOL();
        InFaulted = new BOOL();
        NumberOfSampInv = new BOOL();
    }

    /// <summary>
    /// Creates a new <see cref="MOVING_AVERAGE"/> instance initialized with the provided element.
    /// </summary>
    public MOVING_AVERAGE(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="MOVING_AVERAGE"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>In</c> member of the <see cref="MOVING_AVERAGE"/> data type.
    /// </summary>
    public REAL In
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InFault</c> member of the <see cref="MOVING_AVERAGE"/> data type.
    /// </summary>
    public BOOL InFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Initialize</c> member of the <see cref="MOVING_AVERAGE"/> data type.
    /// </summary>
    public BOOL Initialize
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SampleEnable</c> member of the <see cref="MOVING_AVERAGE"/> data type.
    /// </summary>
    public BOOL SampleEnable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>NumberOfSamples</c> member of the <see cref="MOVING_AVERAGE"/> data type.
    /// </summary>
    public DINT NumberOfSamples
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>UseWeights</c> member of the <see cref="MOVING_AVERAGE"/> data type.
    /// </summary>
    public BOOL UseWeights
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="MOVING_AVERAGE"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out</c> member of the <see cref="MOVING_AVERAGE"/> data type.
    /// </summary>
    public REAL Out
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Status</c> member of the <see cref="MOVING_AVERAGE"/> data type.
    /// </summary>
    public DINT Status
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InstructFault</c> member of the <see cref="MOVING_AVERAGE"/> data type.
    /// </summary>
    public BOOL InstructFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InFaulted</c> member of the <see cref="MOVING_AVERAGE"/> data type.
    /// </summary>
    public BOOL InFaulted
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>NumberOfSampInv</c> member of the <see cref="MOVING_AVERAGE"/> data type.
    /// </summary>
    public BOOL NumberOfSampInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

}
