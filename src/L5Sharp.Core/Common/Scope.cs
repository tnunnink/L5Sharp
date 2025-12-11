using System;
using System.Linq;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// Represents the definition of a Scope in the Logix system.
/// </summary>
/// <remarks>
/// A Scope defines the hierarchical or functional container for elements in a Logix project,
/// such as programs, routines, and controller-level objects. The class provides functionality
/// to access and determine the level and container of the current scope object based
/// on XML configuration.
/// </remarks>
public sealed class Scope
{
    private readonly XElement _element;

    /// <summary>
    /// Creates a new scope instance based on the provided <see cref="XElement"/> which contains the hierarchical
    /// references which are used to determine the scope of the element.
    /// </summary>
    /// <param name="element">The element to determine the scope of.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="element"/> is null.</exception>
    private Scope(XElement element)
    {
        _element = element ?? throw new ArgumentNullException(nameof(element));
    }

    /// <summary>
    /// Gets the <see cref="ScopeLevel"/> of the current <see cref="Scope"/> object.
    /// </summary>
    /// <returns>
    /// A <see cref="ScopeLevel"/> representing the hierarchical level of the scope,
    /// such as Controller, Program, Routine, or None.
    /// </returns>
    /// <remarks>
    /// The level is determined based on the XML structure of the underlying element and
    /// its ancestors. The only elements that are considered are <c>Controller</c>, <c>Program</c>, and
    /// <c>AddOnInstructionDefinition</c> (Routine) elements.
    /// </remarks>
    public ScopeLevel Level => DetermineLevel();

    /// <summary>
    /// Gets the name of the container in which the current <see cref="Scope"/> resides.
    /// </summary>
    /// <remarks>
    /// The container is determined based on the XML structure and the ancestry of the associated
    /// element, considering predefined L5X container types such as Controller, Program, or Add-On
    /// Instruction Definition. If no valid container is found, an empty string is returned.
    /// </remarks>
    /// <returns>
    /// A string representing the name of the container, or an empty string if no container is found.
    /// </returns>
    public string Container => DetermineContainer();

    /// <summary>
    /// Gets a value indicating whether the current <see cref="Scope"/> is at the global level.
    /// </summary>
    /// <remarks>
    /// A global scope is defined as one that resides at the controller level, meaning
    /// it is not confined to a specific program or routine. This property enables determining
    /// whether the scope applies to the entire controller namespace.
    /// </remarks>
    /// <returns>
    /// <c>true</c> if the <see cref="Scope"/> is at the controller level; otherwise, <c>false</c>.
    /// </returns>
    public bool IsController => Level == ScopeLevel.Controller;

    /// <summary>
    /// Gets a value indicating whether the current <see cref="Scope"/> object represents a program scope.
    /// </summary>
    /// <remarks>
    /// A scope is considered a program if its <see cref="ScopeLevel"/> is equivalent to <see cref="ScopeLevel.Program"/>.
    /// </remarks>
    /// <returns>
    /// A <c>true</c> value if the scope is at the program level; otherwise, <c>false</c>.
    /// </returns>
    public bool IsProgram => Level == ScopeLevel.Program;

    /// <summary>
    /// Gets a value indicating whether the current <see cref="Scope"/> object represents a routine-level scope.
    /// </summary>
    /// <remarks>
    /// A routine-level scope corresponds to AOI components within the L5X file, where the scope This property
    /// checks if the <see cref="Scope.Level"/> is equal to <see cref="ScopeLevel.Routine"/>.
    /// </remarks>
    /// <returns>
    /// <c>true</c> if the current scope represents a routine level; otherwise, <c>false</c>.
    /// </returns>
    public bool IsRoutine => Level == ScopeLevel.Routine;

    /// <summary>
    /// Gets a value indicating whether the current <see cref="Scope"/> is local.
    /// </summary>
    /// <remarks>
    /// A scope is considered local if its level is either <see cref="ScopeLevel.Program"/> or
    /// <see cref="ScopeLevel.Routine"/>. Local scopes represent functional or hierarchical containers
    /// within a specific program or routine, as opposed to global scopes which span the entire controller.
    /// </remarks>
    /// <returns>
    /// <c>true</c> if the scope level is <see cref="ScopeLevel.Program"/> or <see cref="ScopeLevel.Routine"/>;
    /// otherwise, <c>false</c>.
    /// </returns>
    public bool IsLocal => Level == ScopeLevel.Program || Level == ScopeLevel.Routine;

    /// <summary>
    /// Determines if the current <see cref="Scope"/> is local to the specified container.
    /// </summary>
    /// <param name="container">The name of the container to compare against.</param>
    /// <returns>
    /// True if the current scope is the controller and the container is empty,
    /// or if the container matches the current scope's container name. Returns false otherwise.
    /// </returns>
    public bool IsIn(string container)
    {
        if (IsController && container.IsEmpty()) return true;
        return Container.IsEquivalent(container);
    }

    /// <summary>
    /// Creates a new <see cref="Scope"/> instance based on the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The XML element representing the scope.</param>
    /// <returns>A new instance of the <see cref="Scope"/> class initialized with the provided element.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="element"/> is null.</exception>
    public static Scope Of(XElement element) => new(element);

    #region Internal

    /// <summary>
    /// Determines the scope level of the current element based on its XML ancestry.
    /// </summary>
    /// <returns>
    /// A <see cref="ScopeLevel"/> value that represents the determined scope level.
    /// </returns>
    private ScopeLevel DetermineLevel()
    {
        var ancestors = _element.Ancestors().Where(IsContainer).Select(x => x.Name.LocalName).ToArray();

        return ancestors.Length switch
        {
            2 when ancestors[0] == L5XName.Program => ScopeLevel.Program,
            2 when ancestors[0] == L5XName.AddOnInstructionDefinition => ScopeLevel.Routine,
            1 when ancestors[0] == L5XName.Controller => ScopeLevel.Controller,
            _ => ScopeLevel.None
        };
    }

    /// <summary>
    /// Determines the container of the current XML element based on its ancestry and predefined L5X element types.
    /// </summary>
    /// <returns>
    /// A string representing the name of the determined container, or an empty string if no container is found.
    /// </returns>
    private string DetermineContainer()
    {
        var ancestors = _element.Ancestors().Where(IsContainer).ToArray();

        return ancestors.Length switch
        {
            2 when ancestors[0].Name.LocalName == L5XName.Program => ancestors[0].LogixName(),
            2 when ancestors[0].Name.LocalName == L5XName.AddOnInstructionDefinition => ancestors[0].LogixName(),
            1 when ancestors[0].Name.LocalName == L5XName.Controller => ancestors[0].LogixName(),
            _ => string.Empty
        };
    }

    /// <summary>
    /// Determines if the specified XML element represents a container element.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to evaluate.</param>
    /// <returns>
    /// A boolean value indicating whether the specified element is a container element.
    /// </returns>
    private static bool IsContainer(XElement element) =>
        element.Name.LocalName
            is L5XName.Controller
            or L5XName.Program
            or L5XName.AddOnInstructionDefinition;

    #endregion
}