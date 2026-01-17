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
    /// Represents the <see cref="ScopeLevel"/> option for a null or undefined scope in a Logix application.
    /// </summary>
    /// <remarks>
    /// The <c>None</c> scope typically signifies the absence of a defined scope level,
    /// serving as a default or placeholder value in scenarios where no specific scope is applicable.
    /// </remarks>
    public static readonly ScopeLevel None = new(nameof(None), nameof(None));

    /// <summary>
    /// Represents the <see cref="ScopeLevel"/> for a Controller-level scope in a Logix application.
    /// </summary>
    /// <remarks>
    /// The <c>Controller</c> scope typically signifies tags or configurations accessible globally
    /// across the project, without being constrained to a specific program or routine.
    /// </remarks>
    public static readonly ScopeLevel Controller = new(nameof(Controller), nameof(Controller));

    /// <summary>
    /// Represents the <see cref="ScopeLevel"/> option corresponding to a program-level scope in a Logix environment.
    /// </summary>
    /// <remarks>
    /// This scope level is used to identify elements or tags that are associated specifically with a program.
    /// The program-level scope can define properties and behavior distinct from other levels such as controller or AOI.
    /// </remarks>
    public static readonly ScopeLevel Program = new(nameof(Program), nameof(Program));

    /// <summary>
    /// Represents the AOI (Add-On Instruction) scope level within the Logix system.
    /// </summary>
    /// <remarks>
    /// The AOI scope corresponds to a scope level associated with Add-On Instructions in a Logix controller project.
    /// It is used to define and manage elements that belong specifically to an AOI context.
    /// </remarks>
    public static readonly ScopeLevel Aoi = new(nameof(Aoi), nameof(Aoi));
}