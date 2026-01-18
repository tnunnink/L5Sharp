using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>SAFELY_LIMITED_SPEED</c> data type structure.
/// </summary>
[LogixData("SAFELY_LIMITED_SPEED")]
public sealed partial class SAFELY_LIMITED_SPEED : StructureData
{
    /// <summary>
    /// Creates a new <see cref="SAFELY_LIMITED_SPEED"/> instance initialized with default values.
    /// </summary>
    public SAFELY_LIMITED_SPEED() : base("SAFELY_LIMITED_SPEED")
    {
        EnableIn = new BOOL();
        EnableOut = new BOOL();
        RestartType = new BOOL();
        ColdStartType = new BOOL();
        Request = new BOOL();
        Reset = new BOOL();
        O1 = new BOOL();
        RR = new BOOL();
        FP = new BOOL();
        CheckDelayActive = new BOOL();
        CheckDelay = new INT();
        ActiveLimit = new REAL();
        FaultType = new SINT();
        DiagnosticCode = new SINT();
    }
    
    /// <summary>
    /// Creates a new <see cref="SAFELY_LIMITED_SPEED"/> instance initialized with the provided element.
    /// </summary>
    public SAFELY_LIMITED_SPEED(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="SAFELY_LIMITED_SPEED"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="SAFELY_LIMITED_SPEED"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RestartType</c> member of the <see cref="SAFELY_LIMITED_SPEED"/> data type.
    /// </summary>
    public BOOL RestartType
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ColdStartType</c> member of the <see cref="SAFELY_LIMITED_SPEED"/> data type.
    /// </summary>
    public BOOL ColdStartType
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Request</c> member of the <see cref="SAFELY_LIMITED_SPEED"/> data type.
    /// </summary>
    public BOOL Request
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Reset</c> member of the <see cref="SAFELY_LIMITED_SPEED"/> data type.
    /// </summary>
    public BOOL Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>O1</c> member of the <see cref="SAFELY_LIMITED_SPEED"/> data type.
    /// </summary>
    public BOOL O1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RR</c> member of the <see cref="SAFELY_LIMITED_SPEED"/> data type.
    /// </summary>
    public BOOL RR
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FP</c> member of the <see cref="SAFELY_LIMITED_SPEED"/> data type.
    /// </summary>
    public BOOL FP
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CheckDelayActive</c> member of the <see cref="SAFELY_LIMITED_SPEED"/> data type.
    /// </summary>
    public BOOL CheckDelayActive
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CheckDelay</c> member of the <see cref="SAFELY_LIMITED_SPEED"/> data type.
    /// </summary>
    public INT CheckDelay
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ActiveLimit</c> member of the <see cref="SAFELY_LIMITED_SPEED"/> data type.
    /// </summary>
    public REAL ActiveLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FaultType</c> member of the <see cref="SAFELY_LIMITED_SPEED"/> data type.
    /// </summary>
    public SINT FaultType
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DiagnosticCode</c> member of the <see cref="SAFELY_LIMITED_SPEED"/> data type.
    /// </summary>
    public SINT DiagnosticCode
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }
}