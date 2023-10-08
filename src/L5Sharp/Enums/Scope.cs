using System.Linq;
using System.Xml.Linq;
using L5Sharp.Common;
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
    /// <see cref="Controller"/> if the element has a <c>Controller</c> element ancestor, <see cref="Null"/> otherwise.
    /// </returns>
    public static Scope FromElement(XElement element)
    {
        var ancestor = element.Ancestors().FirstOrDefault(IsScopeElement)?.Name.ToString();
        return ancestor is not null ? FromName(ancestor) : Null;
    }

    /// <summary>
    /// Finds the container element in the ancestral chain and returns the logix name of the element. This will be either
    /// the name of the program container or the name of the controller, depending on the scope of the element. 
    /// </summary>
    /// <param name="element">The element for which to find the container name of.</param>
    /// <returns>A <see cref="string"/> representing the name of the containing program or controller.</returns>
    public static string Container(XElement element) =>
        element.Ancestors(FromElement(element).Name).FirstOrDefault()?.LogixName() ?? string.Empty;

    /// <summary>
    /// Generates a <see cref="ComponentKey"/> value for the provided <see cref="XElement"/> using its element type (name)
    /// logic name attribute, and place in the L5X hierarchy.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> for which to determine the component key.</param>
    /// <returns>A <see cref="ComponentKey"/> which uniquely identifies the component across the L5X file.</returns>
    public static ComponentKey DetermineKey(XElement element)
    {
        var type = element.Name.LocalName;
        var container = Container(element);
        var name = element.LogixName();
        return new ComponentKey(type, container, name);
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