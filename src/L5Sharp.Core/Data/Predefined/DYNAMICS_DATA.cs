using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>DYNAMICS_DATA</c> data type structure.
/// </summary>
[LogixData("DYNAMICS_DATA")]
public sealed partial class DYNAMICS_DATA : StructureData
{
    /// <summary>
    /// Creates a new <see cref="DYNAMICS_DATA"/> instance initialized with default values.
    /// </summary>
    public DYNAMICS_DATA() : base("DYNAMICS_DATA")
    {
        UnitsMode = new DINT();
        TimeUnits = new DINT();
        Profile = new DINT();
        Speed = new REAL();
        Acceleration = new REAL();
        Deceleration = new REAL();
        AccelerationJerk = new REAL();
        DecelerationJerk = new REAL();
        OrientationSpeed = new REAL();
        OrientationAcceleration = new REAL();
        OrientationDeceleration = new REAL();
    }
    
    /// <summary>
    /// Creates a new <see cref="DYNAMICS_DATA"/> instance initialized with the provided element.
    /// </summary>
    public DYNAMICS_DATA(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 44;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        UnitsMode.UpdateData(data, offset + 0);
        TimeUnits.UpdateData(data, offset + 4);
        Profile.UpdateData(data, offset + 8);
        Speed.UpdateData(data, offset + 12);
        Acceleration.UpdateData(data, offset + 16);
        Deceleration.UpdateData(data, offset + 20);
        AccelerationJerk.UpdateData(data, offset + 24);
        DecelerationJerk.UpdateData(data, offset + 28);
        OrientationSpeed.UpdateData(data, offset + 32);
        OrientationAcceleration.UpdateData(data, offset + 36);
        OrientationDeceleration.UpdateData(data, offset + 40);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>UnitsMode</c> member of the <see cref="DYNAMICS_DATA"/> data type.
    /// </summary>
    public DINT UnitsMode
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TimeUnits</c> member of the <see cref="DYNAMICS_DATA"/> data type.
    /// </summary>
    public DINT TimeUnits
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Profile</c> member of the <see cref="DYNAMICS_DATA"/> data type.
    /// </summary>
    public DINT Profile
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Speed</c> member of the <see cref="DYNAMICS_DATA"/> data type.
    /// </summary>
    public REAL Speed
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Acceleration</c> member of the <see cref="DYNAMICS_DATA"/> data type.
    /// </summary>
    public REAL Acceleration
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Deceleration</c> member of the <see cref="DYNAMICS_DATA"/> data type.
    /// </summary>
    public REAL Deceleration
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AccelerationJerk</c> member of the <see cref="DYNAMICS_DATA"/> data type.
    /// </summary>
    public REAL AccelerationJerk
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DecelerationJerk</c> member of the <see cref="DYNAMICS_DATA"/> data type.
    /// </summary>
    public REAL DecelerationJerk
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OrientationSpeed</c> member of the <see cref="DYNAMICS_DATA"/> data type.
    /// </summary>
    public REAL OrientationSpeed
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OrientationAcceleration</c> member of the <see cref="DYNAMICS_DATA"/> data type.
    /// </summary>
    public REAL OrientationAcceleration
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OrientationDeceleration</c> member of the <see cref="DYNAMICS_DATA"/> data type.
    /// </summary>
    public REAL OrientationDeceleration
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }
}