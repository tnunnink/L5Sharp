using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>DISCRETE_2STATE</c> data type structure.
/// </summary>
[LogixData("DISCRETE_2STATE")]
public sealed partial class DISCRETE_2STATE : StructureData
{
    /// <summary>
    /// Creates a new <see cref="DISCRETE_2STATE"/> instance initialized with default values.
    /// </summary>
    public DISCRETE_2STATE() : base("DISCRETE_2STATE")
    {
        EnableIn = new BOOL();
        ProgCommand = new BOOL();
        Oper0Req = new BOOL();
        Oper1Req = new BOOL();
        State0Perm = new BOOL();
        State1Perm = new BOOL();
        FB0 = new BOOL();
        FB1 = new BOOL();
        HandFB = new BOOL();
        FaultTime = new REAL();
        FaultAlarmLatch = new BOOL();
        FaultAlmUnlatch = new BOOL();
        OverrideOnInit = new BOOL();
        OverrideOnFault = new BOOL();
        OutReverse = new BOOL();
        OverrideState = new BOOL();
        FB0State0 = new BOOL();
        FB0State1 = new BOOL();
        FB1State0 = new BOOL();
        FB1State1 = new BOOL();
        ProgProgReq = new BOOL();
        ProgOperReq = new BOOL();
        ProgOverrideReq = new BOOL();
        ProgHandReq = new BOOL();
        OperProgReq = new BOOL();
        OperOperReq = new BOOL();
        ProgValueReset = new BOOL();
        EnableOut = new BOOL();
        Out = new BOOL();
        Device0State = new BOOL();
        Device1State = new BOOL();
        CommandStatus = new BOOL();
        FaultAlarm = new BOOL();
        ModeAlarm = new BOOL();
        ProgOper = new BOOL();
        Override = new BOOL();
        Hand = new BOOL();
        Status = new DINT();
        InstructFault = new BOOL();
        FaultTimeInv = new BOOL();
        OperReqInv = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="DISCRETE_2STATE"/> instance initialized with the provided element.
    /// </summary>
    public DISCRETE_2STATE(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="DISCRETE_2STATE"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgCommand</c> member of the <see cref="DISCRETE_2STATE"/> data type.
    /// </summary>
    public BOOL ProgCommand
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Oper0Req</c> member of the <see cref="DISCRETE_2STATE"/> data type.
    /// </summary>
    public BOOL Oper0Req
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Oper1Req</c> member of the <see cref="DISCRETE_2STATE"/> data type.
    /// </summary>
    public BOOL Oper1Req
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>State0Perm</c> member of the <see cref="DISCRETE_2STATE"/> data type.
    /// </summary>
    public BOOL State0Perm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>State1Perm</c> member of the <see cref="DISCRETE_2STATE"/> data type.
    /// </summary>
    public BOOL State1Perm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FB0</c> member of the <see cref="DISCRETE_2STATE"/> data type.
    /// </summary>
    public BOOL FB0
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FB1</c> member of the <see cref="DISCRETE_2STATE"/> data type.
    /// </summary>
    public BOOL FB1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HandFB</c> member of the <see cref="DISCRETE_2STATE"/> data type.
    /// </summary>
    public BOOL HandFB
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FaultTime</c> member of the <see cref="DISCRETE_2STATE"/> data type.
    /// </summary>
    public REAL FaultTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FaultAlarmLatch</c> member of the <see cref="DISCRETE_2STATE"/> data type.
    /// </summary>
    public BOOL FaultAlarmLatch
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FaultAlmUnlatch</c> member of the <see cref="DISCRETE_2STATE"/> data type.
    /// </summary>
    public BOOL FaultAlmUnlatch
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OverrideOnInit</c> member of the <see cref="DISCRETE_2STATE"/> data type.
    /// </summary>
    public BOOL OverrideOnInit
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OverrideOnFault</c> member of the <see cref="DISCRETE_2STATE"/> data type.
    /// </summary>
    public BOOL OverrideOnFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OutReverse</c> member of the <see cref="DISCRETE_2STATE"/> data type.
    /// </summary>
    public BOOL OutReverse
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OverrideState</c> member of the <see cref="DISCRETE_2STATE"/> data type.
    /// </summary>
    public BOOL OverrideState
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FB0State0</c> member of the <see cref="DISCRETE_2STATE"/> data type.
    /// </summary>
    public BOOL FB0State0
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FB0State1</c> member of the <see cref="DISCRETE_2STATE"/> data type.
    /// </summary>
    public BOOL FB0State1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FB1State0</c> member of the <see cref="DISCRETE_2STATE"/> data type.
    /// </summary>
    public BOOL FB1State0
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FB1State1</c> member of the <see cref="DISCRETE_2STATE"/> data type.
    /// </summary>
    public BOOL FB1State1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgProgReq</c> member of the <see cref="DISCRETE_2STATE"/> data type.
    /// </summary>
    public BOOL ProgProgReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgOperReq</c> member of the <see cref="DISCRETE_2STATE"/> data type.
    /// </summary>
    public BOOL ProgOperReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgOverrideReq</c> member of the <see cref="DISCRETE_2STATE"/> data type.
    /// </summary>
    public BOOL ProgOverrideReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgHandReq</c> member of the <see cref="DISCRETE_2STATE"/> data type.
    /// </summary>
    public BOOL ProgHandReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OperProgReq</c> member of the <see cref="DISCRETE_2STATE"/> data type.
    /// </summary>
    public BOOL OperProgReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OperOperReq</c> member of the <see cref="DISCRETE_2STATE"/> data type.
    /// </summary>
    public BOOL OperOperReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgValueReset</c> member of the <see cref="DISCRETE_2STATE"/> data type.
    /// </summary>
    public BOOL ProgValueReset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="DISCRETE_2STATE"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out</c> member of the <see cref="DISCRETE_2STATE"/> data type.
    /// </summary>
    public BOOL Out
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Device0State</c> member of the <see cref="DISCRETE_2STATE"/> data type.
    /// </summary>
    public BOOL Device0State
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Device1State</c> member of the <see cref="DISCRETE_2STATE"/> data type.
    /// </summary>
    public BOOL Device1State
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CommandStatus</c> member of the <see cref="DISCRETE_2STATE"/> data type.
    /// </summary>
    public BOOL CommandStatus
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FaultAlarm</c> member of the <see cref="DISCRETE_2STATE"/> data type.
    /// </summary>
    public BOOL FaultAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ModeAlarm</c> member of the <see cref="DISCRETE_2STATE"/> data type.
    /// </summary>
    public BOOL ModeAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgOper</c> member of the <see cref="DISCRETE_2STATE"/> data type.
    /// </summary>
    public BOOL ProgOper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Override</c> member of the <see cref="DISCRETE_2STATE"/> data type.
    /// </summary>
    public BOOL Override
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Hand</c> member of the <see cref="DISCRETE_2STATE"/> data type.
    /// </summary>
    public BOOL Hand
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Status</c> member of the <see cref="DISCRETE_2STATE"/> data type.
    /// </summary>
    public DINT Status
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InstructFault</c> member of the <see cref="DISCRETE_2STATE"/> data type.
    /// </summary>
    public BOOL InstructFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FaultTimeInv</c> member of the <see cref="DISCRETE_2STATE"/> data type.
    /// </summary>
    public BOOL FaultTimeInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OperReqInv</c> member of the <see cref="DISCRETE_2STATE"/> data type.
    /// </summary>
    public BOOL OperReqInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}