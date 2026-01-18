using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>DISCRETE_3STATE</c> data type structure.
/// </summary>
[LogixData("DISCRETE_3STATE")]
public sealed partial class DISCRETE_3STATE : StructureData
{
    /// <summary>
    /// Creates a new <see cref="DISCRETE_3STATE"/> instance initialized with default values.
    /// </summary>
    public DISCRETE_3STATE() : base("DISCRETE_3STATE")
    {
        EnableIn = new BOOL();
        Prog0Command = new BOOL();
        Prog1Command = new BOOL();
        Prog2Command = new BOOL();
        Oper0Req = new BOOL();
        Oper1Req = new BOOL();
        Oper2Req = new BOOL();
        State0Perm = new BOOL();
        State1Perm = new BOOL();
        State2Perm = new BOOL();
        FB0 = new BOOL();
        FB1 = new BOOL();
        FB2 = new BOOL();
        FB3 = new BOOL();
        HandFB0 = new BOOL();
        HandFB1 = new BOOL();
        HandFB2 = new BOOL();
        FaultTime = new REAL();
        FaultAlarmLatch = new BOOL();
        FaultAlmUnlatch = new BOOL();
        OverrideOnInit = new BOOL();
        OverrideOnFault = new BOOL();
        Out0State0 = new BOOL();
        Out0State1 = new BOOL();
        Out0State2 = new BOOL();
        Out1State0 = new BOOL();
        Out1State1 = new BOOL();
        Out1State2 = new BOOL();
        Out2State0 = new BOOL();
        Out2State1 = new BOOL();
        Out2State2 = new BOOL();
        OverrideState = new DINT();
        FB0State0 = new BOOL();
        FB0State1 = new BOOL();
        FB0State2 = new BOOL();
        FB1State0 = new BOOL();
        FB1State1 = new BOOL();
        FB1State2 = new BOOL();
        FB2State0 = new BOOL();
        FB2State1 = new BOOL();
        FB2State2 = new BOOL();
        FB3State0 = new BOOL();
        FB3State1 = new BOOL();
        FB3State2 = new BOOL();
        ProgProgReq = new BOOL();
        ProgOperReq = new BOOL();
        ProgOverrideReq = new BOOL();
        ProgHandReq = new BOOL();
        OperProgReq = new BOOL();
        OperOperReq = new BOOL();
        ProgValueReset = new BOOL();
        EnableOut = new BOOL();
        Out0 = new BOOL();
        Out1 = new BOOL();
        Out2 = new BOOL();
        Device0State = new BOOL();
        Device1State = new BOOL();
        Device2State = new BOOL();
        Command0Status = new BOOL();
        Command1Status = new BOOL();
        Command2Status = new BOOL();
        FaultAlarm = new BOOL();
        ModeAlarm = new BOOL();
        ProgOper = new BOOL();
        Override = new BOOL();
        Hand = new BOOL();
        Status = new DINT();
        InstructFault = new BOOL();
        FaultTimeInv = new BOOL();
        OverrideStateInv = new BOOL();
        ProgCommandInv = new BOOL();
        OperReqInv = new BOOL();
        HandCommandInv = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="DISCRETE_3STATE"/> instance initialized with the provided element.
    /// </summary>
    public DISCRETE_3STATE(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Prog0Command</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL Prog0Command
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Prog1Command</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL Prog1Command
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Prog2Command</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL Prog2Command
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Oper0Req</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL Oper0Req
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Oper1Req</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL Oper1Req
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Oper2Req</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL Oper2Req
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>State0Perm</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL State0Perm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>State1Perm</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL State1Perm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>State2Perm</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL State2Perm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FB0</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL FB0
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FB1</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL FB1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FB2</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL FB2
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FB3</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL FB3
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HandFB0</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL HandFB0
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HandFB1</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL HandFB1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HandFB2</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL HandFB2
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FaultTime</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public REAL FaultTime
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FaultAlarmLatch</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL FaultAlarmLatch
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FaultAlmUnlatch</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL FaultAlmUnlatch
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OverrideOnInit</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL OverrideOnInit
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OverrideOnFault</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL OverrideOnFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out0State0</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL Out0State0
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out0State1</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL Out0State1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out0State2</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL Out0State2
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out1State0</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL Out1State0
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out1State1</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL Out1State1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out1State2</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL Out1State2
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out2State0</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL Out2State0
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out2State1</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL Out2State1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out2State2</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL Out2State2
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OverrideState</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public DINT OverrideState
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FB0State0</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL FB0State0
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FB0State1</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL FB0State1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FB0State2</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL FB0State2
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FB1State0</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL FB1State0
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FB1State1</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL FB1State1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FB1State2</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL FB1State2
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FB2State0</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL FB2State0
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FB2State1</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL FB2State1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FB2State2</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL FB2State2
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FB3State0</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL FB3State0
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FB3State1</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL FB3State1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FB3State2</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL FB3State2
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgProgReq</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL ProgProgReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgOperReq</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL ProgOperReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgOverrideReq</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL ProgOverrideReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgHandReq</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL ProgHandReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OperProgReq</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL OperProgReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OperOperReq</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL OperOperReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgValueReset</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL ProgValueReset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out0</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL Out0
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out1</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL Out1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out2</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL Out2
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Device0State</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL Device0State
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Device1State</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL Device1State
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Device2State</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL Device2State
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Command0Status</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL Command0Status
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Command1Status</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL Command1Status
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Command2Status</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL Command2Status
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FaultAlarm</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL FaultAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ModeAlarm</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL ModeAlarm
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgOper</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL ProgOper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Override</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL Override
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Hand</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL Hand
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Status</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public DINT Status
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InstructFault</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL InstructFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FaultTimeInv</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL FaultTimeInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OverrideStateInv</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL OverrideStateInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgCommandInv</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL ProgCommandInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OperReqInv</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL OperReqInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HandCommandInv</c> member of the <see cref="DISCRETE_3STATE"/> data type.
    /// </summary>
    public BOOL HandCommandInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}