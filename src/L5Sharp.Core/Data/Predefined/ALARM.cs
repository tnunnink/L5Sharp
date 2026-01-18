using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>ALARM</c> data type structure.
/// </summary>
[LogixData("ALARM")]
public sealed partial class ALARM : StructureData
{
    /// <summary>
    /// Creates a new <see cref="ALARM"/> instance initialized with default values.
    /// </summary>
    public ALARM() : base("ALARM")
    {
        EnableIn = new BOOL();
        In = new REAL();
        HHLimit = new REAL();
        HLimit = new REAL();
        LLimit = new REAL();
        LLLimit = new REAL();
        Deadband = new REAL();
        ROCPosLimit = new REAL();
        ROCNegLimit = new REAL();
        ROCPeriod = new REAL();
        EnableOut = new BOOL();
        HHAlarm = new BOOL();
        HAlarm = new BOOL();
        LAlarm = new BOOL();
        LLAlarm = new BOOL();
        ROCPosAlarm = new BOOL();
        ROCNegAlarm = new BOOL();
        ROC = new REAL();
        Status = new DINT();
        InstructFault = new BOOL();
        DeadbandInv = new BOOL();
        ROCPosLimitInv = new BOOL();
        ROCNegLimitInv = new BOOL();
        ROCPeriodInv = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="ALARM"/> instance initialized with the provided element.
    /// </summary>
    public ALARM(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="ALARM"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>In</c> member of the <see cref="ALARM"/> data type.
    /// </summary>
    public REAL In
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HHLimit</c> member of the <see cref="ALARM"/> data type.
    /// </summary>
    public REAL HHLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HLimit</c> member of the <see cref="ALARM"/> data type.
    /// </summary>
    public REAL HLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>LLimit</c> member of the <see cref="ALARM"/> data type.
    /// </summary>
    public REAL LLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>LLLimit</c> member of the <see cref="ALARM"/> data type.
    /// </summary>
    public REAL LLLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Deadband</c> member of the <see cref="ALARM"/> data type.
    /// </summary>
    public REAL Deadband
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ROCPosLimit</c> member of the <see cref="ALARM"/> data type.
    /// </summary>
    public REAL ROCPosLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ROCNegLimit</c> member of the <see cref="ALARM"/> data type.
    /// </summary>
    public REAL ROCNegLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ROCPeriod</c> member of the <see cref="ALARM"/> data type.
    /// </summary>
    public REAL ROCPeriod
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="ALARM"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HHAlarm</c> member of the <see cref="ALARM"/> data type.
    /// </summary>
    public BOOL HHAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HAlarm</c> member of the <see cref="ALARM"/> data type.
    /// </summary>
    public BOOL HAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>LAlarm</c> member of the <see cref="ALARM"/> data type.
    /// </summary>
    public BOOL LAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>LLAlarm</c> member of the <see cref="ALARM"/> data type.
    /// </summary>
    public BOOL LLAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ROCPosAlarm</c> member of the <see cref="ALARM"/> data type.
    /// </summary>
    public BOOL ROCPosAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ROCNegAlarm</c> member of the <see cref="ALARM"/> data type.
    /// </summary>
    public BOOL ROCNegAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ROC</c> member of the <see cref="ALARM"/> data type.
    /// </summary>
    public REAL ROC
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Status</c> member of the <see cref="ALARM"/> data type.
    /// </summary>
    public DINT Status
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InstructFault</c> member of the <see cref="ALARM"/> data type.
    /// </summary>
    public BOOL InstructFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DeadbandInv</c> member of the <see cref="ALARM"/> data type.
    /// </summary>
    public BOOL DeadbandInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ROCPosLimitInv</c> member of the <see cref="ALARM"/> data type.
    /// </summary>
    public BOOL ROCPosLimitInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ROCNegLimitInv</c> member of the <see cref="ALARM"/> data type.
    /// </summary>
    public BOOL ROCNegLimitInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ROCPeriodInv</c> member of the <see cref="ALARM"/> data type.
    /// </summary>
    public BOOL ROCPeriodInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}