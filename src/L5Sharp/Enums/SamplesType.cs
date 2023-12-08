namespace L5Sharp.Core;

/// <summary>
/// Represents an enumeration of all <see cref="SamplesType"/> options for a given <see cref="Trend"/>.
/// </summary>
public class SamplesType : LogixEnum<SamplesType, string>
{
    private SamplesType(string name, string value) : base(name, value)
    {
    }

    /// <summary>
    /// Represents a Samples <see cref="SamplesType"/> value.
    /// </summary>
    public static readonly SamplesType Samples = new(nameof(Samples), "Samples");

    /// <summary>
    /// Represents a TimePeriod <see cref="SamplesType"/> value.
    /// </summary>
    public static readonly SamplesType TimePeriod = new(nameof(TimePeriod), "Time Period");
}