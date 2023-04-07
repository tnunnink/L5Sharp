using L5Sharp.Components;

namespace L5Sharp.Enums;

/// <summary>
/// Represents an enumeration of <see cref="ModuleCategory"/> for a given <see cref="Module"/>.
/// </summary>
public sealed class ModuleCategory : LogixEnum<ModuleCategory, string>
{
    private ModuleCategory(string name, string value) : base(name, value)
    {
    }


    /// <summary>
    /// Represents an Analog <see cref="ModuleCategory"/>.
    /// </summary>
    public static readonly ModuleCategory Analog = new(nameof(Analog), "Analog");
        
    /// <summary>
    /// Represents a Digital <see cref="ModuleCategory"/>.
    /// </summary>
    public static readonly ModuleCategory Digital = new(nameof(Digital), "Digital");
        
    /// <summary>
    /// Represents an Analog <see cref="ModuleCategory"/>.
    /// </summary>
    public static readonly ModuleCategory Input = new(nameof(Input), "Input");
        
    /// <summary>
    /// Represents a Communication <see cref="ModuleCategory"/>.
    /// </summary>
    public static readonly ModuleCategory Communication = new(nameof(Communication), "Communication");
        
    /// <summary>
    /// Represents a Controller <see cref="ModuleCategory"/>.
    /// </summary>
    public static readonly ModuleCategory Controller = new(nameof(Controller), "Controller");
        
    /// <summary>
    /// Represents a Motion <see cref="ModuleCategory"/>.
    /// </summary>
    public static readonly ModuleCategory Motion = new(nameof(Motion), "Motion");
        
    /// <summary>
    /// Represents a Specialty <see cref="ModuleCategory"/>.
    /// </summary>
    public static readonly ModuleCategory Specialty = new(nameof(Specialty), "Specialty");
}