using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>DCI_STOP_TEST_LOCK</c> data type structure.
/// </summary>
[LogixData("DCI_STOP_TEST_LOCK")]
public sealed partial class DCI_STOP_TEST_LOCK : StructureData
{
    /// <summary>
    /// Creates a new <see cref="DCI_STOP_TEST_LOCK"/> instance initialized with default values.
    /// </summary>
    public DCI_STOP_TEST_LOCK() : base("DCI_STOP_TEST_LOCK")
    {
        EnableIn = new BOOL();
        ChannelA = new BOOL();
        ChannelB = new BOOL();
        InputStatus = new BOOL();
        Reset = new BOOL();
        RestartType = new BOOL();
        ColdStartType = new BOOL();
        TestRequest = new BOOL();
        UnlockRequest = new BOOL();
        LockFeedback = new BOOL();
        HazardStopped = new BOOL();
        SafetyFunction = new DINT();
        InputType = new DINT();
        DiscrepancyTime = new DINT();
        EnableOut = new BOOL();
        O1 = new BOOL();
        FP = new BOOL();
        TC = new BOOL();
        ULC = new BOOL();
        FaultCode = new DINT();
        DiagnosticCode = new DINT();
    }
    
    /// <summary>
    /// Creates a new <see cref="DCI_STOP_TEST_LOCK"/> instance initialized with the provided element.
    /// </summary>
    public DCI_STOP_TEST_LOCK(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="DCI_STOP_TEST_LOCK"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ChannelA</c> member of the <see cref="DCI_STOP_TEST_LOCK"/> data type.
    /// </summary>
    public BOOL ChannelA
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ChannelB</c> member of the <see cref="DCI_STOP_TEST_LOCK"/> data type.
    /// </summary>
    public BOOL ChannelB
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InputStatus</c> member of the <see cref="DCI_STOP_TEST_LOCK"/> data type.
    /// </summary>
    public BOOL InputStatus
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Reset</c> member of the <see cref="DCI_STOP_TEST_LOCK"/> data type.
    /// </summary>
    public BOOL Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RestartType</c> member of the <see cref="DCI_STOP_TEST_LOCK"/> data type.
    /// </summary>
    public BOOL RestartType
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ColdStartType</c> member of the <see cref="DCI_STOP_TEST_LOCK"/> data type.
    /// </summary>
    public BOOL ColdStartType
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TestRequest</c> member of the <see cref="DCI_STOP_TEST_LOCK"/> data type.
    /// </summary>
    public BOOL TestRequest
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>UnlockRequest</c> member of the <see cref="DCI_STOP_TEST_LOCK"/> data type.
    /// </summary>
    public BOOL UnlockRequest
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>LockFeedback</c> member of the <see cref="DCI_STOP_TEST_LOCK"/> data type.
    /// </summary>
    public BOOL LockFeedback
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HazardStopped</c> member of the <see cref="DCI_STOP_TEST_LOCK"/> data type.
    /// </summary>
    public BOOL HazardStopped
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SafetyFunction</c> member of the <see cref="DCI_STOP_TEST_LOCK"/> data type.
    /// </summary>
    public DINT SafetyFunction
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InputType</c> member of the <see cref="DCI_STOP_TEST_LOCK"/> data type.
    /// </summary>
    public DINT InputType
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DiscrepancyTime</c> member of the <see cref="DCI_STOP_TEST_LOCK"/> data type.
    /// </summary>
    public DINT DiscrepancyTime
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="DCI_STOP_TEST_LOCK"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>O1</c> member of the <see cref="DCI_STOP_TEST_LOCK"/> data type.
    /// </summary>
    public BOOL O1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FP</c> member of the <see cref="DCI_STOP_TEST_LOCK"/> data type.
    /// </summary>
    public BOOL FP
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TC</c> member of the <see cref="DCI_STOP_TEST_LOCK"/> data type.
    /// </summary>
    public BOOL TC
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ULC</c> member of the <see cref="DCI_STOP_TEST_LOCK"/> data type.
    /// </summary>
    public BOOL ULC
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FaultCode</c> member of the <see cref="DCI_STOP_TEST_LOCK"/> data type.
    /// </summary>
    public DINT FaultCode
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DiagnosticCode</c> member of the <see cref="DCI_STOP_TEST_LOCK"/> data type.
    /// </summary>
    public DINT DiagnosticCode
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }
}