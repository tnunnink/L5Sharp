namespace L5Sharp.Core;

/// <summary>
/// Represents an enumeration of all <see cref="TriggerType"/> options for a given <see cref="Trend"/>.
/// </summary>
public class TriggerType : LogixEnum<TriggerType, string>
{
    private TriggerType(string name, string value) : base(name, value)
    {
    }

    /// <summary>
    /// Represents a NoTrigger <see cref="TriggerType"/> value.
    /// </summary>
    public static readonly TriggerType NoTrigger = new(nameof(NoTrigger), nameof(NoTrigger));

    /// <summary>
    /// Represents a EventTrigger <see cref="TriggerType"/> value.
    /// </summary>
    public static readonly TriggerType EventTrigger = new(nameof(EventTrigger), nameof(EventTrigger));
}