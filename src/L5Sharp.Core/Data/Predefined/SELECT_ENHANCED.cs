using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

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
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 64;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        EnableIn.UpdateData((data[offset + 5] & (1 << 0)) != 0);
        In1.UpdateData(data, offset + 5);
        In2.UpdateData(data, offset + 9);
        In3.UpdateData(data, offset + 13);
        In4.UpdateData(data, offset + 17);
        In5.UpdateData(data, offset + 21);
        In6.UpdateData(data, offset + 25);
        In1Fault.UpdateData((data[offset + 29] & (1 << 1)) != 0);
        In2Fault.UpdateData((data[offset + 29] & (1 << 2)) != 0);
        In3Fault.UpdateData((data[offset + 29] & (1 << 3)) != 0);
        In4Fault.UpdateData((data[offset + 29] & (1 << 4)) != 0);
        In5Fault.UpdateData((data[offset + 29] & (1 << 5)) != 0);
        In6Fault.UpdateData((data[offset + 29] & (1 << 6)) != 0);
        InsUsed.UpdateData(data, offset + 29);
        SelectorMode.UpdateData(data, offset + 33);
        ProgSelector.UpdateData(data, offset + 37);
        OperSelector.UpdateData(data, offset + 41);
        ProgProgReq.UpdateData((data[offset + 45] & (1 << 7)) != 0);
        ProgOperReq.UpdateData((data[offset + 46] & (1 << 0)) != 0);
        ProgOverrideReq.UpdateData((data[offset + 46] & (1 << 1)) != 0);
        OperProgReq.UpdateData((data[offset + 46] & (1 << 2)) != 0);
        OperOperReq.UpdateData((data[offset + 46] & (1 << 3)) != 0);
        ProgValueReset.UpdateData((data[offset + 46] & (1 << 4)) != 0);
        EnableOut.UpdateData((data[offset + 50] & (1 << 5)) != 0);
        Out.UpdateData(data, offset + 50);
        SelectedIn.UpdateData(data, offset + 54);
        ProgOper.UpdateData((data[offset + 58] & (1 << 6)) != 0);
        Override.UpdateData((data[offset + 58] & (1 << 7)) != 0);
        Status.UpdateData(data, offset + 58);
        InstructFault.UpdateData((data[offset + 63] & (1 << 0)) != 0);
        InsFaulted.UpdateData((data[offset + 63] & (1 << 1)) != 0);
        InsUsedInv.UpdateData((data[offset + 63] & (1 << 2)) != 0);
        SelectorModeInv.UpdateData((data[offset + 63] & (1 << 3)) != 0);
        ProgSelectorInv.UpdateData((data[offset + 63] & (1 << 4)) != 0);
        OperSelectorInv.UpdateData((data[offset + 63] & (1 << 5)) != 0);
        
        return offset + GetSize();
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