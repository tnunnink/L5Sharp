using System.Linq;
using System.Xml.Linq;

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
    /// <see cref="Controller"/> if the element has a <c>Controller</c> element ancestor, <see cref="Null"/> otherwise.
    /// </returns>
    public static Scope FromElement(XElement element)
    {
        var ancestor = element.Ancestors().FirstOrDefault(IsScopeElement)?.Name.ToString();
        return ancestor is not null ? FromName(ancestor) : Null;
    }

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

    private static bool IsScopeElement(XElement element) =>
        element.Name == Program.Name || element.Name == Controller.Name;
}