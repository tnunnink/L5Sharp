namespace L5Sharp.Core;

/// <summary>
/// Represents an enumeration of all <see cref="SFCExecutionControl"/> options for a given <see cref="Controller"/>.
/// </summary>
public sealed class SFCExecutionControl : LogixEnum<SFCExecutionControl, string>
{
    private SFCExecutionControl(string name, string value) : base(name, value)
    {
    }

    /// <summary>
    /// Represents a 'CurrentActive' <see cref="SFCExecutionControl"/> value.
    /// </summary>
    public static readonly SFCExecutionControl CurrentActive = new(nameof(CurrentActive), nameof(CurrentActive));
    
    /// <summary>
    /// Represents a 'UntilFalse' <see cref="SFCExecutionControl"/> value.
    /// </summary>
    public static readonly SFCExecutionControl UntilFalse = new(nameof(UntilFalse), nameof(UntilFalse));
}