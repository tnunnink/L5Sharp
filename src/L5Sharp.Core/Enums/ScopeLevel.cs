namespace L5Sharp.Core;

/// <summary>
/// Represents an enumeration of all Logix <see cref="ScopeLevel"/> options.
/// </summary>
public class ScopeLevel : LogixEnum<ScopeLevel, string>
{
    private ScopeLevel(string name, string value) : base(name, value)
    {
    }

    /// <summary>
    /// Represents a Null <see cref="ScopeLevel"/> value.
    /// </summary>
    /// <remarks>A <c>Null</c> scope will occur on element objects that have not been added to a container.</remarks>
    public static readonly ScopeLevel Null = new(nameof(Null), "NullScope");

    /// <summary>
    /// Represents a Controller <see cref="ScopeLevel"/> value.
    /// </summary>
    public static readonly ScopeLevel Controller = new(nameof(Controller), "ControllerScope");

    /// <summary>
    /// Represents a Program <see cref="ScopeLevel"/> value.
    /// </summary>
    public static readonly ScopeLevel Program = new(nameof(Program), "ProgramScope");

    /// <summary>
    /// Represents a Program <see cref="ScopeLevel"/> value.
    /// </summary>
    public static readonly ScopeLevel Routine = new(nameof(Routine), "RoutineScope");
}