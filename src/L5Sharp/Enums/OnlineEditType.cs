namespace L5Sharp.Core;

/// <summary>
/// Represents an enumeration of all <see cref="OnlineEditType"/> options for a given <see cref="Routine"/>.
/// </summary>
public class OnlineEditType : LogixEnum<OnlineEditType, string>
{
    private OnlineEditType(string name, string value) : base(name, value)
    {
    }

    /// <summary>
    /// Represents a 'Original' <see cref="OnlineEditType"/> value.
    /// </summary>
    public static readonly OnlineEditType Original = new(nameof(Original), nameof(Original));
    
    /// <summary>
    /// Represents a 'PendingEdits' <see cref="OnlineEditType"/> value.
    /// </summary>
    public static readonly OnlineEditType PendingEdits = new(nameof(PendingEdits), nameof(PendingEdits));
    
    /// <summary>
    /// Represents a 'TestEdits' <see cref="OnlineEditType"/> value.
    /// </summary>
    public static readonly OnlineEditType TestEdits = new(nameof(TestEdits), nameof(TestEdits));
}