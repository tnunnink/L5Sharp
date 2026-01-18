using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>EIGHT_POS_MODE_SELECTOR</c> data type structure.
/// </summary>
[LogixData("EIGHT_POS_MODE_SELECTOR")]
public sealed partial class EIGHT_POS_MODE_SELECTOR : StructureData
{
    /// <summary>
    /// Creates a new <see cref="EIGHT_POS_MODE_SELECTOR"/> instance initialized with default values.
    /// </summary>
    public EIGHT_POS_MODE_SELECTOR() : base("EIGHT_POS_MODE_SELECTOR")
    {
        EnableIn = new BOOL();
        Input1 = new BOOL();
        Input2 = new BOOL();
        Input3 = new BOOL();
        Input4 = new BOOL();
        Input5 = new BOOL();
        Input6 = new BOOL();
        Input7 = new BOOL();
        Input8 = new BOOL();
        InputStatus = new BOOL();
        Lock = new BOOL();
        Reset = new BOOL();
        EnableOut = new BOOL();
        O1 = new BOOL();
        O2 = new BOOL();
        O3 = new BOOL();
        O4 = new BOOL();
        O5 = new BOOL();
        O6 = new BOOL();
        O7 = new BOOL();
        O8 = new BOOL();
        FP = new BOOL();
        FaultCode = new DINT();
        DiagnosticCode = new DINT();
    }
    
    /// <summary>
    /// Creates a new <see cref="EIGHT_POS_MODE_SELECTOR"/> instance initialized with the provided element.
    /// </summary>
    public EIGHT_POS_MODE_SELECTOR(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="EIGHT_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Input1</c> member of the <see cref="EIGHT_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public BOOL Input1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Input2</c> member of the <see cref="EIGHT_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public BOOL Input2
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Input3</c> member of the <see cref="EIGHT_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public BOOL Input3
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Input4</c> member of the <see cref="EIGHT_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public BOOL Input4
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Input5</c> member of the <see cref="EIGHT_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public BOOL Input5
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Input6</c> member of the <see cref="EIGHT_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public BOOL Input6
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Input7</c> member of the <see cref="EIGHT_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public BOOL Input7
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Input8</c> member of the <see cref="EIGHT_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public BOOL Input8
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InputStatus</c> member of the <see cref="EIGHT_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public BOOL InputStatus
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Lock</c> member of the <see cref="EIGHT_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public BOOL Lock
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Reset</c> member of the <see cref="EIGHT_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public BOOL Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="EIGHT_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>O1</c> member of the <see cref="EIGHT_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public BOOL O1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>O2</c> member of the <see cref="EIGHT_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public BOOL O2
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>O3</c> member of the <see cref="EIGHT_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public BOOL O3
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>O4</c> member of the <see cref="EIGHT_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public BOOL O4
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>O5</c> member of the <see cref="EIGHT_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public BOOL O5
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>O6</c> member of the <see cref="EIGHT_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public BOOL O6
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>O7</c> member of the <see cref="EIGHT_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public BOOL O7
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>O8</c> member of the <see cref="EIGHT_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public BOOL O8
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FP</c> member of the <see cref="EIGHT_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public BOOL FP
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FaultCode</c> member of the <see cref="EIGHT_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public DINT FaultCode
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DiagnosticCode</c> member of the <see cref="EIGHT_POS_MODE_SELECTOR"/> data type.
    /// </summary>
    public DINT DiagnosticCode
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }
}