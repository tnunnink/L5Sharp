using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

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
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 104;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        EnableIn.UpdateData((data[offset + 5] & (1 << 0)) != 0);
        RestartType.UpdateData((data[offset + 5] & (1 << 1)) != 0);
        LightCurtain.UpdateData((data[offset + 5] & (1 << 2)) != 0);
        Sensor1.UpdateData((data[offset + 5] & (1 << 3)) != 0);
        Sensor2.UpdateData((data[offset + 5] & (1 << 4)) != 0);
        Sensor3.UpdateData((data[offset + 5] & (1 << 5)) != 0);
        Sensor4.UpdateData((data[offset + 5] & (1 << 6)) != 0);
        EnableMute.UpdateData((data[offset + 5] & (1 << 7)) != 0);
        Override.UpdateData((data[offset + 6] & (1 << 0)) != 0);
        InputStatus.UpdateData((data[offset + 6] & (1 << 1)) != 0);
        MutingLampStatus.UpdateData((data[offset + 6] & (1 << 2)) != 0);
        Reset.UpdateData((data[offset + 6] & (1 << 3)) != 0);
        Direction.UpdateData((data[offset + 6] & (1 << 4)) != 0);
        S1S2Time.UpdateData(data, offset + 6);
        S2LCTime.UpdateData(data, offset + 10);
        LCS3Time.UpdateData(data, offset + 14);
        S3S4Time.UpdateData(data, offset + 18);
        MaximumMuteTime.UpdateData(data, offset + 22);
        MaximumOverrideTime.UpdateData(data, offset + 26);
        EnableOut.UpdateData((data[offset + 34] & (1 << 5)) != 0);
        O1.UpdateData((data[offset + 34] & (1 << 6)) != 0);
        ML.UpdateData((data[offset + 34] & (1 << 7)) != 0);
        CA.UpdateData((data[offset + 35] & (1 << 0)) != 0);
        FP.UpdateData((data[offset + 35] & (1 << 1)) != 0);
        FaultCode.UpdateData(data, offset + 35);
        DiagnosticCode.UpdateData(data, offset + 39);
        
        return offset + GetSize();
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