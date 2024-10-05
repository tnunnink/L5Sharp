namespace L5Sharp.Core;

/// <summary>
/// Represents the scope types in the L5Sharp.Core namespace.
/// </summary>
public class ScopeType : LogixEnum<ScopeType, string>
{
    private ScopeType(string name, string value) : base(name, value)
    {
    }

    /// <summary>
    /// Represents a Null <see cref="ScopeType"/> value.
    /// </summary>
    /// <remarks>A <c>Null</c> scope will occur on elements objects that have not been added to a container.</remarks>
    public static readonly ScopeType Empty = new(nameof(Empty), "");

    /// <summary>
    /// Represents a DataType <see cref="ScopeType"/> value.
    /// </summary>
    public static readonly ScopeType DataType = new(nameof(DataType), "DataType");

    /// <summary>
    /// Represents a Module <see cref="ScopeType"/> value.
    /// </summary>
    public static readonly ScopeType Instruction = new(nameof(Instruction), "AddOnInstructionDefinition");

    /// <summary>
    /// Represents a Module <see cref="ScopeType"/> value.
    /// </summary>
    public static readonly ScopeType Module = new(nameof(Module), "Module");

    /// <summary>
    /// Represents a Program <see cref="ScopeType"/> value.
    /// </summary>
    public static readonly ScopeType Tag = new(nameof(Tag), "Tag");

    /// <summary>
    /// Represents a Program <see cref="ScopeType"/> value.
    /// </summary>
    public static readonly ScopeType Program = new(nameof(Program), "Program");

    /// <summary>
    /// Represents a Routine <see cref="ScopeType"/> value.
    /// </summary>
    public static readonly ScopeType Routine = new(nameof(Routine), "Routine");

    /// <summary>
    /// Represents a Task <see cref="ScopeType"/> value.
    /// </summary>
    public static readonly ScopeType Task = new(nameof(Task), "Task");

    /// <summary>
    /// Represents a Rung <see cref="ScopeType"/> value.
    /// </summary>
    public static readonly ScopeType Rung = new(nameof(Rung), "Rung");

    /// <summary>
    /// Represents a Line <see cref="ScopeType"/> value.
    /// </summary>
    public static readonly ScopeType Line = new(nameof(Line), "Line");

    /// <summary>
    /// Represents a Sheet <see cref="ScopeType"/> value.
    /// </summary>
    public static readonly ScopeType Sheet = new(nameof(Sheet), "Sheet");

    /// <summary>
    /// Indicates whether this is a type that is contained in a program scope (i.e., Tag, Routine).
    /// </summary>
    /// <returns>
    /// <c>true</c> if the current type is a program scope type; otherwise, <c>false</c>.
    /// </returns>
    public bool InController => !InProgram && !InRoutine;

    /// <summary>
    /// Indicates whether this is a type that is contained in a program scope (i.e., Tag, Routine).
    /// </summary>
    /// <returns>
    /// <c>true</c> if the current type is a program scope type; otherwise, <c>false</c>.
    /// </returns>
    public bool InProgram => this == Tag || this == Routine;

    /// <summary>
    /// Indicates whether this is a type that is contained in a routine scope (i.e., Rung, Line, Sheet).
    /// </summary>
    /// <returns>
    /// <c>true</c> if the current type is a routine scope type; otherwise, <c>false</c>.
    /// </returns>
    public bool InRoutine => this == Rung || this == Line || this == Sheet;
}