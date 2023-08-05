using System.Xml.Linq;
using L5Sharp.Enums;
using L5Sharp.Types.Atomics;

// ReSharper disable InconsistentNaming I want to keep the naming consistent with Logix (for now).

namespace L5Sharp.Types.Predefined;

/// <summary>
/// A predefined or built in data type in Logix that is a part of the alarm instruction set.
/// </summary>
public sealed class ALARM : StructureType
{
    /// <summary>
    /// Creates a new <see cref="ALARM"/> data type instance.
    /// </summary>
    public ALARM() : base(nameof(ALARM))
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
    
    /// <inheritdoc />
    public ALARM(XElement element) : base(element)
    {
    }

    /// <inheritdoc />
    public override DataTypeClass Class => DataTypeClass.Predefined;

    /// <summary>
    /// Gets the <see cref="EnableIn"/> member of the <see cref="ALARM"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="In"/> member of the <see cref="ALARM"/> data type.
    /// </summary>
    public REAL In
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="HHLimit"/> member of the <see cref="ALARM"/> data type.
    /// </summary>
    public REAL HHLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="HLimit"/> member of the <see cref="ALARM"/> data type.
    /// </summary>
    public REAL HLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="LLimit"/> member of the <see cref="ALARM"/> data type.
    /// </summary>
    public REAL LLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="LLLimit"/> member of the <see cref="ALARM"/> data type.
    /// </summary>
    public REAL LLLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="Deadband"/> member of the <see cref="ALARM"/> data type.
    /// </summary>
    public REAL Deadband
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCPosLimit"/> member of the <see cref="ALARM"/> data type.
    /// </summary>
    public REAL ROCPosLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCNegLimit"/> member of the <see cref="ALARM"/> data type.
    /// </summary>
    public REAL ROCNegLimit
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCPeriod"/> member of the <see cref="ALARM"/> data type.
    /// </summary>
    public REAL ROCPeriod
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="EnableOut"/> member of the <see cref="ALARM"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="HHAlarm"/> member of the <see cref="ALARM"/> data type.
    /// </summary>
    public BOOL HHAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="HAlarm"/> member of the <see cref="ALARM"/> data type.
    /// </summary>
    public BOOL HAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="LAlarm"/> member of the <see cref="ALARM"/> data type.
    /// </summary>
    public BOOL LAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="LLAlarm"/> member of the <see cref="ALARM"/> data type.
    /// </summary>
    public BOOL LLAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCPosAlarm"/> member of the <see cref="ALARM"/> data type.
    /// </summary>
    public BOOL ROCPosAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCNegAlarm"/> member of the <see cref="ALARM"/> data type.
    /// </summary>
    public BOOL ROCNegAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ROC"/> member of the <see cref="ALARM"/> data type.
    /// </summary>
    public REAL ROC
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="Status"/> member of the <see cref="ALARM"/> data type.
    /// </summary>
    public DINT Status
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="InstructFault"/> member of the <see cref="ALARM"/> data type.
    /// </summary>
    public BOOL InstructFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="DeadbandInv"/> member of the <see cref="ALARM"/> data type.
    /// </summary>
    public BOOL DeadbandInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCPosLimitInv"/> member of the <see cref="ALARM"/> data type.
    /// </summary>
    public BOOL ROCPosLimitInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCNegLimitInv"/> member of the <see cref="ALARM"/> data type.
    /// </summary>
    public BOOL ROCNegLimitInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ROCPeriodInv"/> member of the <see cref="ALARM"/> data type.
    /// </summary>
    public BOOL ROCPeriodInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}