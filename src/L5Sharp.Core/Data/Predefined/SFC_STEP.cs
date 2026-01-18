using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>SFC_STEP</c> data type structure.
/// </summary>
[LogixData("SFC_STEP")]
public sealed partial class SFC_STEP : StructureData
{
    /// <summary>
    /// Creates a new <see cref="SFC_STEP"/> instance initialized with default values.
    /// </summary>
    public SFC_STEP() : base("SFC_STEP")
    {
        Status = new DINT();
        X = new BOOL();
        FS = new BOOL();
        SA = new BOOL();
        LS = new BOOL();
        DN = new BOOL();
        OV = new BOOL();
        AlarmEn = new BOOL();
        AlarmLow = new BOOL();
        AlarmHigh = new BOOL();
        Reset = new BOOL();
        PauseTimer = new BOOL();
        PRE = new DINT();
        T = new DINT();
        TMax = new DINT();
        Count = new DINT();
        LimitLow = new DINT();
        LimitHigh = new DINT();
    }
    
    /// <summary>
    /// Creates a new <see cref="SFC_STEP"/> instance initialized with the provided element.
    /// </summary>
    public SFC_STEP(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>Status</c> member of the <see cref="SFC_STEP"/> data type.
    /// </summary>
    public DINT Status
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>X</c> member of the <see cref="SFC_STEP"/> data type.
    /// </summary>
    public BOOL X
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FS</c> member of the <see cref="SFC_STEP"/> data type.
    /// </summary>
    public BOOL FS
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SA</c> member of the <see cref="SFC_STEP"/> data type.
    /// </summary>
    public BOOL SA
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>LS</c> member of the <see cref="SFC_STEP"/> data type.
    /// </summary>
    public BOOL LS
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DN</c> member of the <see cref="SFC_STEP"/> data type.
    /// </summary>
    public BOOL DN
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OV</c> member of the <see cref="SFC_STEP"/> data type.
    /// </summary>
    public BOOL OV
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AlarmEn</c> member of the <see cref="SFC_STEP"/> data type.
    /// </summary>
    public BOOL AlarmEn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AlarmLow</c> member of the <see cref="SFC_STEP"/> data type.
    /// </summary>
    public BOOL AlarmLow
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AlarmHigh</c> member of the <see cref="SFC_STEP"/> data type.
    /// </summary>
    public BOOL AlarmHigh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Reset</c> member of the <see cref="SFC_STEP"/> data type.
    /// </summary>
    public BOOL Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PauseTimer</c> member of the <see cref="SFC_STEP"/> data type.
    /// </summary>
    public BOOL PauseTimer
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PRE</c> member of the <see cref="SFC_STEP"/> data type.
    /// </summary>
    public DINT PRE
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>T</c> member of the <see cref="SFC_STEP"/> data type.
    /// </summary>
    public DINT T
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TMax</c> member of the <see cref="SFC_STEP"/> data type.
    /// </summary>
    public DINT TMax
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Count</c> member of the <see cref="SFC_STEP"/> data type.
    /// </summary>
    public new DINT Count
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>LimitLow</c> member of the <see cref="SFC_STEP"/> data type.
    /// </summary>
    public DINT LimitLow
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>LimitHigh</c> member of the <see cref="SFC_STEP"/> data type.
    /// </summary>
    public DINT LimitHigh
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }
}