using System.Xml.Linq;
// Auto-generated file
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>SELECT_ENHANCED</c> data type structure.
/// </summary>
[LogixData("SELECT_ENHANCED")]
public sealed partial class SELECT_ENHANCED : StructureData
{
    /// <summary>
    /// Creates a new <see cref="SELECT_ENHANCED"/> instance initialized with default values.
    /// </summary>
    public SELECT_ENHANCED() : base("SELECT_ENHANCED")
    {
        EnableIn = new BOOL();
        In1 = new REAL();
        In2 = new REAL();
        In3 = new REAL();
        In4 = new REAL();
        In5 = new REAL();
        In6 = new REAL();
        In1Fault = new BOOL();
        In2Fault = new BOOL();
        In3Fault = new BOOL();
        In4Fault = new BOOL();
        In5Fault = new BOOL();
        In6Fault = new BOOL();
        InsUsed = new DINT();
        SelectorMode = new DINT();
        ProgSelector = new DINT();
        OperSelector = new DINT();
        ProgProgReq = new BOOL();
        ProgOperReq = new BOOL();
        ProgOverrideReq = new BOOL();
        OperProgReq = new BOOL();
        OperOperReq = new BOOL();
        ProgValueReset = new BOOL();
        EnableOut = new BOOL();
        Out = new REAL();
        SelectedIn = new DINT();
        ProgOper = new BOOL();
        Override = new BOOL();
        Status = new DINT();
        InstructFault = new BOOL();
        InsFaulted = new BOOL();
        InsUsedInv = new BOOL();
        SelectorModeInv = new BOOL();
        ProgSelectorInv = new BOOL();
        OperSelectorInv = new BOOL();
    }

    /// <summary>
    /// Creates a new <see cref="SELECT_ENHANCED"/> instance initialized with the provided element.
    /// </summary>
    public SELECT_ENHANCED(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="SELECT_ENHANCED"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>In1</c> member of the <see cref="SELECT_ENHANCED"/> data type.
    /// </summary>
    public REAL In1
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>In2</c> member of the <see cref="SELECT_ENHANCED"/> data type.
    /// </summary>
    public REAL In2
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>In3</c> member of the <see cref="SELECT_ENHANCED"/> data type.
    /// </summary>
    public REAL In3
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>In4</c> member of the <see cref="SELECT_ENHANCED"/> data type.
    /// </summary>
    public REAL In4
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>In5</c> member of the <see cref="SELECT_ENHANCED"/> data type.
    /// </summary>
    public REAL In5
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>In6</c> member of the <see cref="SELECT_ENHANCED"/> data type.
    /// </summary>
    public REAL In6
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>In1Fault</c> member of the <see cref="SELECT_ENHANCED"/> data type.
    /// </summary>
    public BOOL In1Fault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>In2Fault</c> member of the <see cref="SELECT_ENHANCED"/> data type.
    /// </summary>
    public BOOL In2Fault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>In3Fault</c> member of the <see cref="SELECT_ENHANCED"/> data type.
    /// </summary>
    public BOOL In3Fault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>In4Fault</c> member of the <see cref="SELECT_ENHANCED"/> data type.
    /// </summary>
    public BOOL In4Fault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>In5Fault</c> member of the <see cref="SELECT_ENHANCED"/> data type.
    /// </summary>
    public BOOL In5Fault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>In6Fault</c> member of the <see cref="SELECT_ENHANCED"/> data type.
    /// </summary>
    public BOOL In6Fault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InsUsed</c> member of the <see cref="SELECT_ENHANCED"/> data type.
    /// </summary>
    public DINT InsUsed
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SelectorMode</c> member of the <see cref="SELECT_ENHANCED"/> data type.
    /// </summary>
    public DINT SelectorMode
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgSelector</c> member of the <see cref="SELECT_ENHANCED"/> data type.
    /// </summary>
    public DINT ProgSelector
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OperSelector</c> member of the <see cref="SELECT_ENHANCED"/> data type.
    /// </summary>
    public DINT OperSelector
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgProgReq</c> member of the <see cref="SELECT_ENHANCED"/> data type.
    /// </summary>
    public BOOL ProgProgReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgOperReq</c> member of the <see cref="SELECT_ENHANCED"/> data type.
    /// </summary>
    public BOOL ProgOperReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgOverrideReq</c> member of the <see cref="SELECT_ENHANCED"/> data type.
    /// </summary>
    public BOOL ProgOverrideReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OperProgReq</c> member of the <see cref="SELECT_ENHANCED"/> data type.
    /// </summary>
    public BOOL OperProgReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OperOperReq</c> member of the <see cref="SELECT_ENHANCED"/> data type.
    /// </summary>
    public BOOL OperOperReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgValueReset</c> member of the <see cref="SELECT_ENHANCED"/> data type.
    /// </summary>
    public BOOL ProgValueReset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="SELECT_ENHANCED"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out</c> member of the <see cref="SELECT_ENHANCED"/> data type.
    /// </summary>
    public REAL Out
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SelectedIn</c> member of the <see cref="SELECT_ENHANCED"/> data type.
    /// </summary>
    public DINT SelectedIn
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgOper</c> member of the <see cref="SELECT_ENHANCED"/> data type.
    /// </summary>
    public BOOL ProgOper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Override</c> member of the <see cref="SELECT_ENHANCED"/> data type.
    /// </summary>
    public BOOL Override
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Status</c> member of the <see cref="SELECT_ENHANCED"/> data type.
    /// </summary>
    public DINT Status
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InstructFault</c> member of the <see cref="SELECT_ENHANCED"/> data type.
    /// </summary>
    public BOOL InstructFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InsFaulted</c> member of the <see cref="SELECT_ENHANCED"/> data type.
    /// </summary>
    public BOOL InsFaulted
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InsUsedInv</c> member of the <see cref="SELECT_ENHANCED"/> data type.
    /// </summary>
    public BOOL InsUsedInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SelectorModeInv</c> member of the <see cref="SELECT_ENHANCED"/> data type.
    /// </summary>
    public BOOL SelectorModeInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgSelectorInv</c> member of the <see cref="SELECT_ENHANCED"/> data type.
    /// </summary>
    public BOOL ProgSelectorInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OperSelectorInv</c> member of the <see cref="SELECT_ENHANCED"/> data type.
    /// </summary>
    public BOOL OperSelectorInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

}
