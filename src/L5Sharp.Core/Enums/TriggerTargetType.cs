using L5Sharp.Components;

namespace L5Sharp.Enums;

/// <summary>
/// Represents an enumeration of all <see cref="TriggerTargetType"/> options for a given <see cref="Trend"/>.
/// </summary>
public class TriggerTargetType : LogixEnum<TriggerTargetType, string>
{
    private TriggerTargetType(string name, string value) : base(name, value)
    {
    }

    /// <summary>
    /// Represents a TargetValue <see cref="TriggerTargetType"/> value.
    /// </summary>
    public static readonly TriggerTargetType TargetValue = new(nameof(TargetValue), nameof(TargetValue));

    /// <summary>
    /// Represents a TargetTag <see cref="TriggerTargetType"/> value.
    /// </summary>
    public static readonly TriggerTargetType TargetTag = new(nameof(TargetTag), nameof(TargetTag));
}