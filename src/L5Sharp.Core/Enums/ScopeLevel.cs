using System.Linq;
using System.Xml.Linq;

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
    /// Finds the container element in the ancestral chain and returns the logix name of the element. This will be either
    /// the name of the program container or the name of the controller, depending on the scope of the element. 
    /// </summary>
    /// <param name="element">The element for which to find the container name of.</param>
    /// <returns>A <see cref="string"/> representing the name of the containing program, instruction, or controller
    /// if found; Otherwise, an empty string.</returns>
    public static string Container(XElement element) => FindContainer(element)?.LogixName() ?? string.Empty;

    /// <summary>
    /// Represents a Null <see cref="ScopeLevel"/> value.
    /// </summary>
    /// <remarks>A <c>Null</c> scope will occur on elements objects that have not been added to a container.</remarks>
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
}