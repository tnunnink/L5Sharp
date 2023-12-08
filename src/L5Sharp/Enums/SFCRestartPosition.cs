namespace L5Sharp.Core;

/// <summary>
/// Represents an enumeration of all <see cref="SFCRestartPosition"/> options for a given <see cref="Controller"/>.
/// </summary>
public sealed class SFCRestartPosition : LogixEnum<SFCRestartPosition, string>
{
    private SFCRestartPosition(string name, string value) : base(name, value)
    {
    }

    /// <summary>
    /// Represents a 'MostRecent' <see cref="SFCRestartPosition"/> value.
    /// </summary>
    public static readonly SFCRestartPosition MostRecent = new(nameof(MostRecent), nameof(MostRecent));
    
    /// <summary>
    /// Represents a 'InitialStep' <see cref="SFCRestartPosition"/> value.
    /// </summary>
    public static readonly SFCRestartPosition InitialStep = new(nameof(InitialStep), nameof(InitialStep));
}