using System.Xml.Linq;
// Auto-generated file
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>DIVERSE_INPUT</c> data type structure.
/// </summary>
[LogixData("DIVERSE_INPUT")]
public sealed partial class DIVERSE_INPUT : StructureData
{
    /// <summary>
    /// Creates a new <see cref="DIVERSE_INPUT"/> instance initialized with default values.
    /// </summary>
    public DIVERSE_INPUT() : base("DIVERSE_INPUT")
    {
        EnableIn = new BOOL();
        ResetType = new BOOL();
        ChannelA = new BOOL();
        ChannelB = new BOOL();
        CircuitReset = new BOOL();
        FaultReset = new BOOL();
        EnableOut = new BOOL();
        O1 = new BOOL();
        CI = new BOOL();
        CRHO = new BOOL();
        II = new BOOL();
        FP = new BOOL();
    }

    /// <summary>
    /// Creates a new <see cref="DIVERSE_INPUT"/> instance initialized with the provided element.
    /// </summary>
    public DIVERSE_INPUT(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="DIVERSE_INPUT"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ResetType</c> member of the <see cref="DIVERSE_INPUT"/> data type.
    /// </summary>
    public BOOL ResetType
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ChannelA</c> member of the <see cref="DIVERSE_INPUT"/> data type.
    /// </summary>
    public BOOL ChannelA
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ChannelB</c> member of the <see cref="DIVERSE_INPUT"/> data type.
    /// </summary>
    public BOOL ChannelB
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CircuitReset</c> member of the <see cref="DIVERSE_INPUT"/> data type.
    /// </summary>
    public BOOL CircuitReset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FaultReset</c> member of the <see cref="DIVERSE_INPUT"/> data type.
    /// </summary>
    public BOOL FaultReset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="DIVERSE_INPUT"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>O1</c> member of the <see cref="DIVERSE_INPUT"/> data type.
    /// </summary>
    public BOOL O1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CI</c> member of the <see cref="DIVERSE_INPUT"/> data type.
    /// </summary>
    public BOOL CI
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CRHO</c> member of the <see cref="DIVERSE_INPUT"/> data type.
    /// </summary>
    public BOOL CRHO
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>II</c> member of the <see cref="DIVERSE_INPUT"/> data type.
    /// </summary>
    public BOOL II
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FP</c> member of the <see cref="DIVERSE_INPUT"/> data type.
    /// </summary>
    public BOOL FP
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

}
