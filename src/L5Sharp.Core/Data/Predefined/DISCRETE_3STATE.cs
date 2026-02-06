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
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 48;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        EnableIn.UpdateData((data[offset + 9] & (1 << 0)) != 0);
        Prog0Command.UpdateData((data[offset + 9] & (1 << 1)) != 0);
        Prog1Command.UpdateData((data[offset + 9] & (1 << 2)) != 0);
        Prog2Command.UpdateData((data[offset + 9] & (1 << 3)) != 0);
        Oper0Req.UpdateData((data[offset + 9] & (1 << 4)) != 0);
        Oper1Req.UpdateData((data[offset + 9] & (1 << 5)) != 0);
        Oper2Req.UpdateData((data[offset + 9] & (1 << 6)) != 0);
        State0Perm.UpdateData((data[offset + 9] & (1 << 7)) != 0);
        State1Perm.UpdateData((data[offset + 10] & (1 << 0)) != 0);
        State2Perm.UpdateData((data[offset + 10] & (1 << 1)) != 0);
        FB0.UpdateData((data[offset + 10] & (1 << 2)) != 0);
        FB1.UpdateData((data[offset + 10] & (1 << 3)) != 0);
        FB2.UpdateData((data[offset + 10] & (1 << 4)) != 0);
        FB3.UpdateData((data[offset + 10] & (1 << 5)) != 0);
        HandFB0.UpdateData((data[offset + 10] & (1 << 6)) != 0);
        HandFB1.UpdateData((data[offset + 10] & (1 << 7)) != 0);
        HandFB2.UpdateData((data[offset + 11] & (1 << 0)) != 0);
        FaultTime.UpdateData(data, offset + 11);
        FaultAlarmLatch.UpdateData((data[offset + 15] & (1 << 1)) != 0);
        FaultAlmUnlatch.UpdateData((data[offset + 15] & (1 << 2)) != 0);
        OverrideOnInit.UpdateData((data[offset + 15] & (1 << 3)) != 0);
        OverrideOnFault.UpdateData((data[offset + 15] & (1 << 4)) != 0);
        Out0State0.UpdateData((data[offset + 15] & (1 << 5)) != 0);
        Out0State1.UpdateData((data[offset + 15] & (1 << 6)) != 0);
        Out0State2.UpdateData((data[offset + 15] & (1 << 7)) != 0);
        Out1State0.UpdateData((data[offset + 16] & (1 << 0)) != 0);
        Out1State1.UpdateData((data[offset + 16] & (1 << 1)) != 0);
        Out1State2.UpdateData((data[offset + 16] & (1 << 2)) != 0);
        Out2State0.UpdateData((data[offset + 16] & (1 << 3)) != 0);
        Out2State1.UpdateData((data[offset + 16] & (1 << 4)) != 0);
        Out2State2.UpdateData((data[offset + 16] & (1 << 5)) != 0);
        OverrideState.UpdateData(data, offset + 16);
        FB0State0.UpdateData((data[offset + 20] & (1 << 6)) != 0);
        FB0State1.UpdateData((data[offset + 20] & (1 << 7)) != 0);
        FB0State2.UpdateData((data[offset + 21] & (1 << 0)) != 0);
        FB1State0.UpdateData((data[offset + 21] & (1 << 1)) != 0);
        FB1State1.UpdateData((data[offset + 21] & (1 << 2)) != 0);
        FB1State2.UpdateData((data[offset + 21] & (1 << 3)) != 0);
        FB2State0.UpdateData((data[offset + 21] & (1 << 4)) != 0);
        FB2State1.UpdateData((data[offset + 21] & (1 << 5)) != 0);
        FB2State2.UpdateData((data[offset + 21] & (1 << 6)) != 0);
        FB3State0.UpdateData((data[offset + 21] & (1 << 7)) != 0);
        FB3State1.UpdateData((data[offset + 22] & (1 << 0)) != 0);
        FB3State2.UpdateData((data[offset + 22] & (1 << 1)) != 0);
        ProgProgReq.UpdateData((data[offset + 22] & (1 << 2)) != 0);
        ProgOperReq.UpdateData((data[offset + 22] & (1 << 3)) != 0);
        ProgOverrideReq.UpdateData((data[offset + 22] & (1 << 4)) != 0);
        ProgHandReq.UpdateData((data[offset + 22] & (1 << 5)) != 0);
        OperProgReq.UpdateData((data[offset + 22] & (1 << 6)) != 0);
        OperOperReq.UpdateData((data[offset + 22] & (1 << 7)) != 0);
        ProgValueReset.UpdateData((data[offset + 23] & (1 << 0)) != 0);
        EnableOut.UpdateData((data[offset + 27] & (1 << 1)) != 0);
        Out0.UpdateData((data[offset + 27] & (1 << 2)) != 0);
        Out1.UpdateData((data[offset + 27] & (1 << 3)) != 0);
        Out2.UpdateData((data[offset + 27] & (1 << 4)) != 0);
        Device0State.UpdateData((data[offset + 27] & (1 << 5)) != 0);
        Device1State.UpdateData((data[offset + 27] & (1 << 6)) != 0);
        Device2State.UpdateData((data[offset + 27] & (1 << 7)) != 0);
        Command0Status.UpdateData((data[offset + 28] & (1 << 0)) != 0);
        Command1Status.UpdateData((data[offset + 28] & (1 << 1)) != 0);
        Command2Status.UpdateData((data[offset + 28] & (1 << 2)) != 0);
        FaultAlarm.UpdateData((data[offset + 28] & (1 << 3)) != 0);
        ModeAlarm.UpdateData((data[offset + 28] & (1 << 4)) != 0);
        ProgOper.UpdateData((data[offset + 28] & (1 << 5)) != 0);
        Override.UpdateData((data[offset + 28] & (1 << 6)) != 0);
        Hand.UpdateData((data[offset + 28] & (1 << 7)) != 0);
        Status.UpdateData(data, offset + 28);
        InstructFault.UpdateData((data[offset + 33] & (1 << 0)) != 0);
        FaultTimeInv.UpdateData((data[offset + 33] & (1 << 1)) != 0);
        OverrideStateInv.UpdateData((data[offset + 33] & (1 << 2)) != 0);
        ProgCommandInv.UpdateData((data[offset + 33] & (1 << 3)) != 0);
        OperReqInv.UpdateData((data[offset + 33] & (1 << 4)) != 0);
        HandCommandInv.UpdateData((data[offset + 33] & (1 << 5)) != 0);
        
        return offset + GetSize();
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