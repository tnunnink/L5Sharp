using System.Linq;
using System.Xml.Linq;

namespace L5Sharp.Enums;

/// <summary>
/// Represents an enumeration of all Logix <see cref="Scope"/> options.
/// </summary>
public abstract class Scope : LogixEnum<Scope, string>
{
    private Scope(string name, string value) : base(name, value)
    {
    }

    /// <summary>
    /// Gets the corresponding L5XName for the current <see cref="Scope"/>.
    /// </summary>
    public abstract XName XName { get; }

    /// <summary>
    /// Gets the <see cref="Scope"/> option for the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">A <see cref="XElement"/> for which to determine the scope of.</param>
    /// <returns>A <see cref="Scope"/> representing the first found container of the provided element.</returns>
    public static Scope FromElement(XElement element)
    {
        var containers = All().Select(s => s.XName.ToString());
        
        var ancestor = element.Ancestors().FirstOrDefault(a => containers.Any(c => c == a.Name))?.Name.ToString();

        return ancestor switch
        {
            L5XName.Routine => Routine,
            L5XName.Program => Program,
            L5XName.AddOnInstructionDefinition => Instruction,
            L5XName.Controller => Controller,
            _ => Null
        };
    }

    /// <summary>
    /// Represents a Null <see cref="Scope"/> value.
    /// </summary>
    public static readonly Scope Null = new NullScope();

    /// <summary>
    /// Represents a Controller <see cref="Scope"/> value.
    /// </summary>
    public static readonly Scope Controller = new ControllerScope();

    /// <summary>
    /// Represents a Program <see cref="Scope"/> value.
    /// </summary>
    public static readonly Scope Program = new ProgramScope();

    /// <summary>
    /// Represents a Routine <see cref="Scope"/> value.
    /// </summary>
    public static readonly Scope Routine = new RoutineScope();

    /// <summary>
    /// Represents a Routine <see cref="Scope"/> value.
    /// </summary>
    /// <remarks>This type is not found in the L5X but one created internally to represent AOIs.</remarks>
    public static readonly Scope Instruction = new InstructionScope();

    private class NullScope : Scope
    {
        public NullScope() : base(nameof(Null), "NullScope")
        {
        }

        public override XName XName => string.Empty;
    }
    
    private class ControllerScope : Scope
    {
        public ControllerScope() : base(nameof(Controller), "ControllerScope")
        {
        }

        public override XName XName => L5XName.Controller;
    }
    
    private class ProgramScope : Scope
    {
        public ProgramScope() : base(nameof(Program), "ProgramScope")
        {
        }

        public override XName XName => L5XName.Program;
    }
    
    private class RoutineScope : Scope
    {
        public RoutineScope() : base(nameof(Routine), "RoutineScope")
        {
        }

        public override XName XName => L5XName.Routine;
    }
    
    private class InstructionScope : Scope
    {
        public InstructionScope() : base(nameof(Instruction), "InstructionScope")
        {
        }

        public override XName XName => L5XName.AddOnInstructionDefinition;
    }
}