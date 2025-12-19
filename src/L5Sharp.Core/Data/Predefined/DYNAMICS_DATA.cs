using System.Xml.Linq;
// Auto-generated file
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart

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
