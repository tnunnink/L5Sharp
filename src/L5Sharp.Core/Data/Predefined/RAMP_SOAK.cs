using System.Xml.Linq;
// Auto-generated file
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>RAMP_SOAK</c> data type structure.
/// </summary>
[LogixData("RAMP_SOAK")]
public sealed partial class RAMP_SOAK : StructureData
{
    /// <summary>
    /// Creates a new <see cref="RAMP_SOAK"/> instance initialized with default values.
    /// </summary>
    public RAMP_SOAK() : base("RAMP_SOAK")
    {
        EnableIn = new BOOL();
        PV = new REAL();
        PVFault = new BOOL();
        NumberOfSegs = new DINT();
        ManHoldAftInit = new BOOL();
        CyclicSingle = new BOOL();
        TimeRate = new BOOL();
        GuarRamp = new BOOL();
        RampDeadband = new REAL();
        GuarSoak = new BOOL();
        SoakDeadband = new REAL();
        CurrentSegProg = new DINT();
        OutProg = new REAL();
        SoakTimeProg = new REAL();
        CurrentSegOper = new DINT();
        OutOper = new REAL();
        SoakTimeOper = new REAL();
        ProgProgReq = new BOOL();
        ProgOperReq = new BOOL();
        ProgAutoReq = new BOOL();
        ProgManualReq = new BOOL();
        ProgHoldReq = new BOOL();
        OperProgReq = new BOOL();
        OperOperReq = new BOOL();
        OperAutoReq = new BOOL();
        OperManualReq = new BOOL();
        Initialize = new BOOL();
        ProgValueReset = new BOOL();
        EnableOut = new BOOL();
        Out = new REAL();
        CurrentSeg = new DINT();
        SoakTimeLeft = new REAL();
        GuarRampOn = new BOOL();
        GuarSoakOn = new BOOL();
        ProgOper = new BOOL();
        Auto = new BOOL();
        Manual = new BOOL();
        Hold = new BOOL();
        Status = new DINT();
        InstructFault = new BOOL();
        PVFaulted = new BOOL();
        NumberOfSegsInv = new BOOL();
        RampDeadbandInv = new BOOL();
        SoakDeadbandInv = new BOOL();
        CurrSegProgInv = new BOOL();
        SoakTimeProgInv = new BOOL();
        CurrSegOperInv = new BOOL();
        SoakTimeOperInv = new BOOL();
        RampValueInv = new BOOL();
        SoakTimeInv = new BOOL();
    }

    /// <summary>
    /// Creates a new <see cref="RAMP_SOAK"/> instance initialized with the provided element.
    /// </summary>
    public RAMP_SOAK(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="RAMP_SOAK"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PV</c> member of the <see cref="RAMP_SOAK"/> data type.
    /// </summary>
    public REAL PV
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PVFault</c> member of the <see cref="RAMP_SOAK"/> data type.
    /// </summary>
    public BOOL PVFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>NumberOfSegs</c> member of the <see cref="RAMP_SOAK"/> data type.
    /// </summary>
    public DINT NumberOfSegs
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ManHoldAftInit</c> member of the <see cref="RAMP_SOAK"/> data type.
    /// </summary>
    public BOOL ManHoldAftInit
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CyclicSingle</c> member of the <see cref="RAMP_SOAK"/> data type.
    /// </summary>
    public BOOL CyclicSingle
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TimeRate</c> member of the <see cref="RAMP_SOAK"/> data type.
    /// </summary>
    public BOOL TimeRate
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>GuarRamp</c> member of the <see cref="RAMP_SOAK"/> data type.
    /// </summary>
    public BOOL GuarRamp
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RampDeadband</c> member of the <see cref="RAMP_SOAK"/> data type.
    /// </summary>
    public REAL RampDeadband
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>GuarSoak</c> member of the <see cref="RAMP_SOAK"/> data type.
    /// </summary>
    public BOOL GuarSoak
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SoakDeadband</c> member of the <see cref="RAMP_SOAK"/> data type.
    /// </summary>
    public REAL SoakDeadband
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CurrentSegProg</c> member of the <see cref="RAMP_SOAK"/> data type.
    /// </summary>
    public DINT CurrentSegProg
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OutProg</c> member of the <see cref="RAMP_SOAK"/> data type.
    /// </summary>
    public REAL OutProg
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SoakTimeProg</c> member of the <see cref="RAMP_SOAK"/> data type.
    /// </summary>
    public REAL SoakTimeProg
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CurrentSegOper</c> member of the <see cref="RAMP_SOAK"/> data type.
    /// </summary>
    public DINT CurrentSegOper
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OutOper</c> member of the <see cref="RAMP_SOAK"/> data type.
    /// </summary>
    public REAL OutOper
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SoakTimeOper</c> member of the <see cref="RAMP_SOAK"/> data type.
    /// </summary>
    public REAL SoakTimeOper
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgProgReq</c> member of the <see cref="RAMP_SOAK"/> data type.
    /// </summary>
    public BOOL ProgProgReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgOperReq</c> member of the <see cref="RAMP_SOAK"/> data type.
    /// </summary>
    public BOOL ProgOperReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgAutoReq</c> member of the <see cref="RAMP_SOAK"/> data type.
    /// </summary>
    public BOOL ProgAutoReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgManualReq</c> member of the <see cref="RAMP_SOAK"/> data type.
    /// </summary>
    public BOOL ProgManualReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgHoldReq</c> member of the <see cref="RAMP_SOAK"/> data type.
    /// </summary>
    public BOOL ProgHoldReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OperProgReq</c> member of the <see cref="RAMP_SOAK"/> data type.
    /// </summary>
    public BOOL OperProgReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OperOperReq</c> member of the <see cref="RAMP_SOAK"/> data type.
    /// </summary>
    public BOOL OperOperReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OperAutoReq</c> member of the <see cref="RAMP_SOAK"/> data type.
    /// </summary>
    public BOOL OperAutoReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OperManualReq</c> member of the <see cref="RAMP_SOAK"/> data type.
    /// </summary>
    public BOOL OperManualReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Initialize</c> member of the <see cref="RAMP_SOAK"/> data type.
    /// </summary>
    public BOOL Initialize
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgValueReset</c> member of the <see cref="RAMP_SOAK"/> data type.
    /// </summary>
    public BOOL ProgValueReset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="RAMP_SOAK"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Out</c> member of the <see cref="RAMP_SOAK"/> data type.
    /// </summary>
    public REAL Out
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CurrentSeg</c> member of the <see cref="RAMP_SOAK"/> data type.
    /// </summary>
    public DINT CurrentSeg
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SoakTimeLeft</c> member of the <see cref="RAMP_SOAK"/> data type.
    /// </summary>
    public REAL SoakTimeLeft
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>GuarRampOn</c> member of the <see cref="RAMP_SOAK"/> data type.
    /// </summary>
    public BOOL GuarRampOn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>GuarSoakOn</c> member of the <see cref="RAMP_SOAK"/> data type.
    /// </summary>
    public BOOL GuarSoakOn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ProgOper</c> member of the <see cref="RAMP_SOAK"/> data type.
    /// </summary>
    public BOOL ProgOper
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Auto</c> member of the <see cref="RAMP_SOAK"/> data type.
    /// </summary>
    public BOOL Auto
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Manual</c> member of the <see cref="RAMP_SOAK"/> data type.
    /// </summary>
    public BOOL Manual
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Hold</c> member of the <see cref="RAMP_SOAK"/> data type.
    /// </summary>
    public BOOL Hold
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Status</c> member of the <see cref="RAMP_SOAK"/> data type.
    /// </summary>
    public DINT Status
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InstructFault</c> member of the <see cref="RAMP_SOAK"/> data type.
    /// </summary>
    public BOOL InstructFault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PVFaulted</c> member of the <see cref="RAMP_SOAK"/> data type.
    /// </summary>
    public BOOL PVFaulted
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>NumberOfSegsInv</c> member of the <see cref="RAMP_SOAK"/> data type.
    /// </summary>
    public BOOL NumberOfSegsInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RampDeadbandInv</c> member of the <see cref="RAMP_SOAK"/> data type.
    /// </summary>
    public BOOL RampDeadbandInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SoakDeadbandInv</c> member of the <see cref="RAMP_SOAK"/> data type.
    /// </summary>
    public BOOL SoakDeadbandInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CurrSegProgInv</c> member of the <see cref="RAMP_SOAK"/> data type.
    /// </summary>
    public BOOL CurrSegProgInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SoakTimeProgInv</c> member of the <see cref="RAMP_SOAK"/> data type.
    /// </summary>
    public BOOL SoakTimeProgInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CurrSegOperInv</c> member of the <see cref="RAMP_SOAK"/> data type.
    /// </summary>
    public BOOL CurrSegOperInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SoakTimeOperInv</c> member of the <see cref="RAMP_SOAK"/> data type.
    /// </summary>
    public BOOL SoakTimeOperInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RampValueInv</c> member of the <see cref="RAMP_SOAK"/> data type.
    /// </summary>
    public BOOL RampValueInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SoakTimeInv</c> member of the <see cref="RAMP_SOAK"/> data type.
    /// </summary>
    public BOOL SoakTimeInv
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

}
