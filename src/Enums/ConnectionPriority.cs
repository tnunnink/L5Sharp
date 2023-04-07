using L5Sharp.Components;

namespace L5Sharp.Enums;

/// <summary>
/// An enumeration of all <see cref="ConnectionPriority"/> for a Logix <see cref="ModuleConnection"/>. 
/// </summary>
public sealed class ConnectionPriority : LogixEnum<ConnectionPriority, string>
{
    private ConnectionPriority(string name, string value) : base(name, value)
    {
    }

    /// <summary>
    /// Represents a <b>Scheduled</b> <see cref="ConnectionPriority"/>.
    /// </summary>
    public static readonly ConnectionPriority Scheduled = new(nameof(Scheduled), nameof(Scheduled));

    /// <summary>
    /// Represents a <b>Low</b> <see cref="ConnectionPriority"/>.
    /// </summary>
    public static readonly ConnectionPriority Low = new(nameof(Low), nameof(Low));

    /// <summary>
    /// Represents a <b>High</b> <see cref="ConnectionPriority"/>.
    /// </summary>
    public static readonly ConnectionPriority High = new(nameof(High), nameof(High));

    /// <summary>
    /// Represents a <b>Urgent</b> <see cref="ConnectionPriority"/>.
    /// </summary>
    public static readonly ConnectionPriority Urgent = new(nameof(Urgent), nameof(Urgent));
}