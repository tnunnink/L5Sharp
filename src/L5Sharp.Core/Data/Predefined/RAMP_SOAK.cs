using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

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
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 108;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        EnableIn.UpdateData((data[offset + 5] & (1 << 0)) != 0);
        PV.UpdateData(data, offset + 5);
        PVFault.UpdateData((data[offset + 9] & (1 << 1)) != 0);
        NumberOfSegs.UpdateData(data, offset + 9);
        ManHoldAftInit.UpdateData((data[offset + 13] & (1 << 2)) != 0);
        CyclicSingle.UpdateData((data[offset + 13] & (1 << 3)) != 0);
        TimeRate.UpdateData((data[offset + 13] & (1 << 4)) != 0);
        GuarRamp.UpdateData((data[offset + 13] & (1 << 5)) != 0);
        RampDeadband.UpdateData(data, offset + 13);
        GuarSoak.UpdateData((data[offset + 17] & (1 << 6)) != 0);
        SoakDeadband.UpdateData(data, offset + 17);
        CurrentSegProg.UpdateData(data, offset + 21);
        OutProg.UpdateData(data, offset + 25);
        SoakTimeProg.UpdateData(data, offset + 29);
        CurrentSegOper.UpdateData(data, offset + 33);
        OutOper.UpdateData(data, offset + 37);
        SoakTimeOper.UpdateData(data, offset + 41);
        ProgProgReq.UpdateData((data[offset + 45] & (1 << 7)) != 0);
        ProgOperReq.UpdateData((data[offset + 46] & (1 << 0)) != 0);
        ProgAutoReq.UpdateData((data[offset + 46] & (1 << 1)) != 0);
        ProgManualReq.UpdateData((data[offset + 46] & (1 << 2)) != 0);
        ProgHoldReq.UpdateData((data[offset + 46] & (1 << 3)) != 0);
        OperProgReq.UpdateData((data[offset + 46] & (1 << 4)) != 0);
        OperOperReq.UpdateData((data[offset + 46] & (1 << 5)) != 0);
        OperAutoReq.UpdateData((data[offset + 46] & (1 << 6)) != 0);
        OperManualReq.UpdateData((data[offset + 46] & (1 << 7)) != 0);
        Initialize.UpdateData((data[offset + 47] & (1 << 0)) != 0);
        ProgValueReset.UpdateData((data[offset + 47] & (1 << 1)) != 0);
        EnableOut.UpdateData((data[offset + 51] & (1 << 2)) != 0);
        Out.UpdateData(data, offset + 51);
        CurrentSeg.UpdateData(data, offset + 55);
        SoakTimeLeft.UpdateData(data, offset + 59);
        GuarRampOn.UpdateData((data[offset + 63] & (1 << 3)) != 0);
        GuarSoakOn.UpdateData((data[offset + 63] & (1 << 4)) != 0);
        ProgOper.UpdateData((data[offset + 63] & (1 << 5)) != 0);
        Auto.UpdateData((data[offset + 63] & (1 << 6)) != 0);
        Manual.UpdateData((data[offset + 63] & (1 << 7)) != 0);
        Hold.UpdateData((data[offset + 64] & (1 << 0)) != 0);
        Status.UpdateData(data, offset + 64);
        InstructFault.UpdateData((data[offset + 68] & (1 << 1)) != 0);
        PVFaulted.UpdateData((data[offset + 68] & (1 << 2)) != 0);
        NumberOfSegsInv.UpdateData((data[offset + 68] & (1 << 3)) != 0);
        RampDeadbandInv.UpdateData((data[offset + 68] & (1 << 4)) != 0);
        SoakDeadbandInv.UpdateData((data[offset + 68] & (1 << 5)) != 0);
        CurrSegProgInv.UpdateData((data[offset + 68] & (1 << 6)) != 0);
        SoakTimeProgInv.UpdateData((data[offset + 68] & (1 << 7)) != 0);
        CurrSegOperInv.UpdateData((data[offset + 69] & (1 << 0)) != 0);
        SoakTimeOperInv.UpdateData((data[offset + 69] & (1 << 1)) != 0);
        RampValueInv.UpdateData((data[offset + 69] & (1 << 2)) != 0);
        SoakTimeInv.UpdateData((data[offset + 69] & (1 << 3)) != 0);
        
        return offset + GetSize();
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