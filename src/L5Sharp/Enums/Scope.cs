using System.Linq;
using System.Xml.Linq;
using L5Sharp.Utilities;

namespace L5Sharp.Enums;

/// <summary>
/// Represents an enumeration of all Logix <see cref="Scope"/> options.
/// </summary>
public class Scope : LogixEnum<Scope, string>
{
    private Scope(string name, string value) : base(name, value)
    {
    }

    /// <summary>
    /// Determines the <see cref="Scope"/> of the provided <see cref="XElement"/> by looking up the ancestral chain.
    /// </summary>
    /// <param name="element">The element for which to determine the scope of.</param>
    /// <returns><see cref="Program"/> if the element has a <c>Program</c> element ancestor,
    /// <see cref="Controller"/> if the element has a <c>Controller</c> element ancestor,
    /// <see cref="Instruction"/> if the element has a <c>AddOnInstructionDefinition</c> element ancestor,
    /// <see cref="Null"/> otherwise.
    /// </returns>
    public static Scope Type(XElement element)
    {
        var ancestor = FindContainer(element)?.Name.LocalName;

        return ancestor switch
        {
            L5XName.Controller => Controller,
            L5XName.Program => Program,
            L5XName.AddOnInstructionDefinition => Instruction,
            _ => Null
        };
    }

    /// <summary>
    /// Finds the container element in the ancestral chain and returns the logix name of the element. This will be either
    /// the name of the program container or the name of the controller, depending on the scope of the element. 
    /// </summary>
    /// <param name="element">The element for which to find the container name of.</param>
    /// <returns>A <see cref="string"/> representing the name of the containing program, instruction, or controller
    /// if found; Otherwise, an empty string.</returns>
    public static string Container(XElement element) => FindContainer(element)?.LogixName() ?? string.Empty;

    /// <summary>
    /// Finds the container element in the ancestral chain and returns the logix name of the element. This will be either
    /// the name of the program container or the name of the controller, depending on the scope of the element. 
    /// </summary>
    /// <param name="element">The element for which to find the container name of.</param>
    /// <returns>A <see cref="string"/> representing the name of the containing program, instruction, or controller
    /// if found; Otherwise, an empty string.</returns>
    public static string Task(XElement element) => FindTask(element)?.LogixName() ?? string.Empty;

    /// <summary>
    /// Represents a Null <see cref="Scope"/> value.
    /// </summary>
    /// <remarks>A <c>Null</c> scope will occur on elements objects that have not been added to a container.</remarks>
    public static readonly Scope Null = new(nameof(Null), "NullScope");

    /// <summary>
    /// Represents a Controller <see cref="Scope"/> value.
    /// </summary>
    public static readonly Scope Controller = new(nameof(Controller), "ControllerScope");

    /// <summary>
    /// Represents a Program <see cref="Scope"/> value.
    /// </summary>
    public static readonly Scope Program = new(nameof(Program), "ProgramScope");

    /// <summary>
    /// Represents a Program <see cref="Scope"/> value.
    /// </summary>
    public static readonly Scope Instruction = new(nameof(Instruction), "InstructionScope");

    /// <summary>
    /// Finds the first ancestor element that is either a <c>Program</c>, <c>Controller</c>, or
    /// <c>AddOnInstructionDefinition</c> element.
    /// </summary>
    /// <param name="node">The <see cref="XNode"/> to examine.</param>
    /// <returns>The first matching ancestor <see cref="XElement"/> if found or <c>null</c>.</returns>
    private static XElement? FindContainer(XNode node)
    {
        return node.Ancestors().FirstOrDefault(e => e.Name.LocalName
            is L5XName.Program or L5XName.Controller or L5XName.AddOnInstructionDefinition);
    }

    /// <summary>
    /// Finds the first <c>Task</c> element in the L5X document with the provided node's container name.
    /// </summary>
    /// <param name="node">The <see cref="XNode"/> to examine.</param>
    /// <returns>The first matching ancestor <see cref="XElement"/> if found or <c>null</c>.</returns>
    private static XElement? FindTask(XNode node)
    {
        var container = FindContainer(node)?.LogixName() ?? string.Empty;

        return node.Ancestors(L5XName.RSLogix5000Content).Descendants(L5XName.Task)
            .FirstOrDefault(e => e.Descendants(L5XName.ScheduledProgram).Any(p => p.LogixName() == container));
    }
}