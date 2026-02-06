using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>DCI_STOP_TEST_MUTE</c> data type structure.
/// </summary>
[LogixData("DCI_STOP_TEST_MUTE")]
public sealed partial class DCI_STOP_TEST_MUTE : StructureData
{
    /// <summary>
    /// Creates a new <see cref="DCI_STOP_TEST_MUTE"/> instance initialized with default values.
    /// </summary>
    public DCI_STOP_TEST_MUTE() : base("DCI_STOP_TEST_MUTE")
    {
        EnableIn = new BOOL();
        ChannelA = new BOOL();
        ChannelB = new BOOL();
        InputStatus = new BOOL();
        Reset = new BOOL();
        RestartType = new BOOL();
        ColdStartType = new BOOL();
        TestRequest = new BOOL();
        Mute = new BOOL();
        MutingLampStatus = new BOOL();
        SafetyFunction = new DINT();
        InputType = new DINT();
        DiscrepancyTime = new DINT();
        TestType = new DINT();
        TestTime = new DINT();
        EnableOut = new BOOL();
        O1 = new BOOL();
        FP = new BOOL();
        TC = new BOOL();
        ML = new BOOL();
        SS = new BOOL();
        FaultCode = new DINT();
        DiagnosticCode = new DINT();
    }
    
    /// <summary>
    /// Creates a new <see cref="DCI_STOP_TEST_MUTE"/> instance initialized with the provided element.
    /// </summary>
    public DCI_STOP_TEST_MUTE(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 100;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        EnableIn.UpdateData((data[offset + 5] & (1 << 0)) != 0);
        ChannelA.UpdateData((data[offset + 5] & (1 << 1)) != 0);
        ChannelB.UpdateData((data[offset + 5] & (1 << 2)) != 0);
        InputStatus.UpdateData((data[offset + 5] & (1 << 3)) != 0);
        Reset.UpdateData((data[offset + 5] & (1 << 4)) != 0);
        RestartType.UpdateData((data[offset + 5] & (1 << 5)) != 0);
        ColdStartType.UpdateData((data[offset + 5] & (1 << 6)) != 0);
        TestRequest.UpdateData((data[offset + 5] & (1 << 7)) != 0);
        Mute.UpdateData((data[offset + 6] & (1 << 0)) != 0);
        MutingLampStatus.UpdateData((data[offset + 6] & (1 << 1)) != 0);
        SafetyFunction.UpdateData(data, offset + 6);
        InputType.UpdateData(data, offset + 10);
        DiscrepancyTime.UpdateData(data, offset + 14);
        TestType.UpdateData(data, offset + 18);
        TestTime.UpdateData(data, offset + 22);
        EnableOut.UpdateData((data[offset + 30] & (1 << 2)) != 0);
        O1.UpdateData((data[offset + 30] & (1 << 3)) != 0);
        FP.UpdateData((data[offset + 30] & (1 << 4)) != 0);
        TC.UpdateData((data[offset + 30] & (1 << 5)) != 0);
        ML.UpdateData((data[offset + 30] & (1 << 6)) != 0);
        SS.UpdateData((data[offset + 30] & (1 << 7)) != 0);
        FaultCode.UpdateData(data, offset + 30);
        DiagnosticCode.UpdateData(data, offset + 34);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="DCI_STOP_TEST_MUTE"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ChannelA</c> member of the <see cref="DCI_STOP_TEST_MUTE"/> data type.
    /// </summary>
    public BOOL ChannelA
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ChannelB</c> member of the <see cref="DCI_STOP_TEST_MUTE"/> data type.
    /// </summary>
    public BOOL ChannelB
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InputStatus</c> member of the <see cref="DCI_STOP_TEST_MUTE"/> data type.
    /// </summary>
    public BOOL InputStatus
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Reset</c> member of the <see cref="DCI_STOP_TEST_MUTE"/> data type.
    /// </summary>
    public BOOL Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RestartType</c> member of the <see cref="DCI_STOP_TEST_MUTE"/> data type.
    /// </summary>
    public BOOL RestartType
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ColdStartType</c> member of the <see cref="DCI_STOP_TEST_MUTE"/> data type.
    /// </summary>
    public BOOL ColdStartType
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TestRequest</c> member of the <see cref="DCI_STOP_TEST_MUTE"/> data type.
    /// </summary>
    public BOOL TestRequest
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Mute</c> member of the <see cref="DCI_STOP_TEST_MUTE"/> data type.
    /// </summary>
    public BOOL Mute
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>MutingLampStatus</c> member of the <see cref="DCI_STOP_TEST_MUTE"/> data type.
    /// </summary>
    public BOOL MutingLampStatus
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SafetyFunction</c> member of the <see cref="DCI_STOP_TEST_MUTE"/> data type.
    /// </summary>
    public DINT SafetyFunction
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InputType</c> member of the <see cref="DCI_STOP_TEST_MUTE"/> data type.
    /// </summary>
    public DINT InputType
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DiscrepancyTime</c> member of the <see cref="DCI_STOP_TEST_MUTE"/> data type.
    /// </summary>
    public DINT DiscrepancyTime
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TestType</c> member of the <see cref="DCI_STOP_TEST_MUTE"/> data type.
    /// </summary>
    public DINT TestType
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TestTime</c> member of the <see cref="DCI_STOP_TEST_MUTE"/> data type.
    /// </summary>
    public DINT TestTime
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="DCI_STOP_TEST_MUTE"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>O1</c> member of the <see cref="DCI_STOP_TEST_MUTE"/> data type.
    /// </summary>
    public BOOL O1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FP</c> member of the <see cref="DCI_STOP_TEST_MUTE"/> data type.
    /// </summary>
    public BOOL FP
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TC</c> member of the <see cref="DCI_STOP_TEST_MUTE"/> data type.
    /// </summary>
    public BOOL TC
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ML</c> member of the <see cref="DCI_STOP_TEST_MUTE"/> data type.
    /// </summary>
    public BOOL ML
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SS</c> member of the <see cref="DCI_STOP_TEST_MUTE"/> data type.
    /// </summary>
    public BOOL SS
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FaultCode</c> member of the <see cref="DCI_STOP_TEST_MUTE"/> data type.
    /// </summary>
    public DINT FaultCode
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DiagnosticCode</c> member of the <see cref="DCI_STOP_TEST_MUTE"/> data type.
    /// </summary>
    public DINT DiagnosticCode
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }
}