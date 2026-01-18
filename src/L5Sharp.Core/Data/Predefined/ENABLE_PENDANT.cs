using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>ENABLE_PENDANT</c> data type structure.
/// </summary>
[LogixData("ENABLE_PENDANT")]
public sealed partial class ENABLE_PENDANT : StructureData
{
    /// <summary>
    /// Creates a new <see cref="ENABLE_PENDANT"/> instance initialized with default values.
    /// </summary>
    public ENABLE_PENDANT() : base("ENABLE_PENDANT")
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
    /// Creates a new <see cref="ENABLE_PENDANT"/> instance initialized with the provided element.
    /// </summary>
    public ENABLE_PENDANT(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="ENABLE_PENDANT"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ResetType</c> member of the <see cref="ENABLE_PENDANT"/> data type.
    /// </summary>
    public BOOL ResetType
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ChannelA</c> member of the <see cref="ENABLE_PENDANT"/> data type.
    /// </summary>
    public BOOL ChannelA
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ChannelB</c> member of the <see cref="ENABLE_PENDANT"/> data type.
    /// </summary>
    public BOOL ChannelB
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CircuitReset</c> member of the <see cref="ENABLE_PENDANT"/> data type.
    /// </summary>
    public BOOL CircuitReset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FaultReset</c> member of the <see cref="ENABLE_PENDANT"/> data type.
    /// </summary>
    public BOOL FaultReset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="ENABLE_PENDANT"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>O1</c> member of the <see cref="ENABLE_PENDANT"/> data type.
    /// </summary>
    public BOOL O1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CI</c> member of the <see cref="ENABLE_PENDANT"/> data type.
    /// </summary>
    public BOOL CI
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CRHO</c> member of the <see cref="ENABLE_PENDANT"/> data type.
    /// </summary>
    public BOOL CRHO
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>II</c> member of the <see cref="ENABLE_PENDANT"/> data type.
    /// </summary>
    public BOOL II
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FP</c> member of the <see cref="ENABLE_PENDANT"/> data type.
    /// </summary>
    public BOOL FP
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}