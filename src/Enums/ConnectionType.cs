using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Elements;

namespace L5Sharp.Enums;

/// <summary>
/// An enumeration of all <see cref="ConnectionType"/> values for a given Logix <see cref="Connection"/>
/// </summary>
public sealed class ConnectionType : LogixEnum<ConnectionType, string>
{
    private ConnectionType(string name, string value) : base(name, value)
    {
    }

    /// <summary>
    /// Represents an Unknown <see cref="ConnectionType"/> value.
    /// </summary>
    public static readonly ConnectionType Unknown = new(nameof(Unknown), nameof(Unknown));

    /// <summary>
    /// Represents an Input <see cref="ConnectionType"/> value.
    /// </summary>
    public static readonly ConnectionType Input = new(nameof(Input), nameof(Input));

    /// <summary>
    /// Represents an Output <see cref="ConnectionType"/> value.
    /// </summary>
    public static readonly ConnectionType Output = new(nameof(Output), nameof(Output));
        
    /// <summary>
    /// Represents an Output <see cref="ConnectionType"/> value.
    /// </summary>
    public static readonly ConnectionType DiagnosticInput = new(nameof(DiagnosticInput), nameof(DiagnosticInput));

    /// <summary>
    /// Represents an MotionSync <see cref="ConnectionType"/> value.
    /// </summary>
    public static readonly ConnectionType MotionSync = new(nameof(MotionSync), nameof(MotionSync));

    /// <summary>
    /// Represents an MotionAsync <see cref="ConnectionType"/> value.
    /// </summary>
    public static readonly ConnectionType MotionAsync = new(nameof(MotionAsync), nameof(MotionAsync));

    /// <summary>
    /// Represents an MotionEvent <see cref="ConnectionType"/> value.
    /// </summary>
    public static readonly ConnectionType MotionEvent = new(nameof(MotionEvent), nameof(MotionEvent));

    /// <summary>
    /// Represents an SafetyInput <see cref="ConnectionType"/> value.
    /// </summary>
    public static readonly ConnectionType SafetyInput = new(nameof(SafetyInput), nameof(SafetyInput));

    /// <summary>
    /// Represents an SafetyOutput <see cref="ConnectionType"/> value.
    /// </summary>
    public static readonly ConnectionType SafetyOutput = new(nameof(SafetyOutput), nameof(SafetyOutput));

    /// <summary>
    /// Represents an SafetyOutput <see cref="ConnectionType"/> value.
    /// </summary>
    public static readonly ConnectionType StandardDataDriven =
        new(nameof(StandardDataDriven), nameof(StandardDataDriven));

    /// <summary>
    /// Represents an SafetyOutput <see cref="ConnectionType"/> value.
    /// </summary>
    public static readonly ConnectionType SafetyInputDataDriven =
        new(nameof(SafetyInputDataDriven), nameof(SafetyInputDataDriven));

    /// <summary>
    /// Represents an SafetyOutput <see cref="ConnectionType"/> value.
    /// </summary>
    public static readonly ConnectionType SafetyOutputDataDriven =
        new(nameof(SafetyOutputDataDriven), nameof(SafetyOutputDataDriven));
}