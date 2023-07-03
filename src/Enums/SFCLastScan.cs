using L5Sharp.Components;

namespace L5Sharp.Enums;

/// <summary>
/// Represents an enumeration of all <see cref="SFCLastScan"/> options for a given <see cref="Controller"/>.
/// </summary>
public sealed class SFCLastScan : LogixEnum<SFCLastScan, string>
{
    private SFCLastScan(string name, string value) : base(name, value)
    {
    }

    /// <summary>
    /// Represents a 'AutomaticReset' <see cref="SFCLastScan"/> value.
    /// </summary>
    public static readonly SFCLastScan AutomaticReset = new(nameof(AutomaticReset), nameof(AutomaticReset));
    
    /// <summary>
    /// Represents a 'ProgrammaticReset' <see cref="SFCLastScan"/> value.
    /// </summary>
    public static readonly SFCLastScan ProgrammaticReset = new(nameof(ProgrammaticReset), nameof(ProgrammaticReset));
    
    /// <summary>
    /// Represents a 'DontScan' <see cref="SFCLastScan"/> value.
    /// </summary>
    public static readonly SFCLastScan DontScan = new(nameof(DontScan), nameof(DontScan));
}