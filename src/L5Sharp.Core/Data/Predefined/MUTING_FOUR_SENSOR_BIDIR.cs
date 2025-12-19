using System.Xml.Linq;
// Auto-generated file
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>MUTING_FOUR_SENSOR_BIDIR</c> data type structure.
/// </summary>
[LogixData("MUTING_FOUR_SENSOR_BIDIR")]
public sealed partial class MUTING_FOUR_SENSOR_BIDIR : StructureData
{
    /// <summary>
    /// Creates a new <see cref="MUTING_FOUR_SENSOR_BIDIR"/> instance initialized with default values.
    /// </summary>
    public MUTING_FOUR_SENSOR_BIDIR() : base("MUTING_FOUR_SENSOR_BIDIR")
    {
        EnableIn = new BOOL();
        RestartType = new BOOL();
        LightCurtain = new BOOL();
        Sensor1 = new BOOL();
        Sensor2 = new BOOL();
        Sensor3 = new BOOL();
        Sensor4 = new BOOL();
        EnableMute = new BOOL();
        Override = new BOOL();
        InputStatus = new BOOL();
        MutingLampStatus = new BOOL();
        Reset = new BOOL();
        Direction = new BOOL();
        S1S2Time = new DINT();
        S2LCTime = new DINT();
        LCS3Time = new DINT();
        S3S4Time = new DINT();
        MaximumMuteTime = new DINT();
        MaximumOverrideTime = new DINT();
        EnableOut = new BOOL();
        O1 = new BOOL();
        ML = new BOOL();
        CA = new BOOL();
        FP = new BOOL();
        FaultCode = new DINT();
        DiagnosticCode = new DINT();
    }

    /// <summary>
    /// Creates a new <see cref="MUTING_FOUR_SENSOR_BIDIR"/> instance initialized with the provided element.
    /// </summary>
    public MUTING_FOUR_SENSOR_BIDIR(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="MUTING_FOUR_SENSOR_BIDIR"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RestartType</c> member of the <see cref="MUTING_FOUR_SENSOR_BIDIR"/> data type.
    /// </summary>
    public BOOL RestartType
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>LightCurtain</c> member of the <see cref="MUTING_FOUR_SENSOR_BIDIR"/> data type.
    /// </summary>
    public BOOL LightCurtain
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sensor1</c> member of the <see cref="MUTING_FOUR_SENSOR_BIDIR"/> data type.
    /// </summary>
    public BOOL Sensor1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sensor2</c> member of the <see cref="MUTING_FOUR_SENSOR_BIDIR"/> data type.
    /// </summary>
    public BOOL Sensor2
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sensor3</c> member of the <see cref="MUTING_FOUR_SENSOR_BIDIR"/> data type.
    /// </summary>
    public BOOL Sensor3
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sensor4</c> member of the <see cref="MUTING_FOUR_SENSOR_BIDIR"/> data type.
    /// </summary>
    public BOOL Sensor4
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableMute</c> member of the <see cref="MUTING_FOUR_SENSOR_BIDIR"/> data type.
    /// </summary>
    public BOOL EnableMute
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Override</c> member of the <see cref="MUTING_FOUR_SENSOR_BIDIR"/> data type.
    /// </summary>
    public BOOL Override
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InputStatus</c> member of the <see cref="MUTING_FOUR_SENSOR_BIDIR"/> data type.
    /// </summary>
    public BOOL InputStatus
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>MutingLampStatus</c> member of the <see cref="MUTING_FOUR_SENSOR_BIDIR"/> data type.
    /// </summary>
    public BOOL MutingLampStatus
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Reset</c> member of the <see cref="MUTING_FOUR_SENSOR_BIDIR"/> data type.
    /// </summary>
    public BOOL Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Direction</c> member of the <see cref="MUTING_FOUR_SENSOR_BIDIR"/> data type.
    /// </summary>
    public BOOL Direction
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>S1S2Time</c> member of the <see cref="MUTING_FOUR_SENSOR_BIDIR"/> data type.
    /// </summary>
    public DINT S1S2Time
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>S2LCTime</c> member of the <see cref="MUTING_FOUR_SENSOR_BIDIR"/> data type.
    /// </summary>
    public DINT S2LCTime
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>LCS3Time</c> member of the <see cref="MUTING_FOUR_SENSOR_BIDIR"/> data type.
    /// </summary>
    public DINT LCS3Time
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>S3S4Time</c> member of the <see cref="MUTING_FOUR_SENSOR_BIDIR"/> data type.
    /// </summary>
    public DINT S3S4Time
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>MaximumMuteTime</c> member of the <see cref="MUTING_FOUR_SENSOR_BIDIR"/> data type.
    /// </summary>
    public DINT MaximumMuteTime
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>MaximumOverrideTime</c> member of the <see cref="MUTING_FOUR_SENSOR_BIDIR"/> data type.
    /// </summary>
    public DINT MaximumOverrideTime
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="MUTING_FOUR_SENSOR_BIDIR"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>O1</c> member of the <see cref="MUTING_FOUR_SENSOR_BIDIR"/> data type.
    /// </summary>
    public BOOL O1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ML</c> member of the <see cref="MUTING_FOUR_SENSOR_BIDIR"/> data type.
    /// </summary>
    public BOOL ML
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CA</c> member of the <see cref="MUTING_FOUR_SENSOR_BIDIR"/> data type.
    /// </summary>
    public BOOL CA
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FP</c> member of the <see cref="MUTING_FOUR_SENSOR_BIDIR"/> data type.
    /// </summary>
    public BOOL FP
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FaultCode</c> member of the <see cref="MUTING_FOUR_SENSOR_BIDIR"/> data type.
    /// </summary>
    public DINT FaultCode
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DiagnosticCode</c> member of the <see cref="MUTING_FOUR_SENSOR_BIDIR"/> data type.
    /// </summary>
    public DINT DiagnosticCode
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

}
