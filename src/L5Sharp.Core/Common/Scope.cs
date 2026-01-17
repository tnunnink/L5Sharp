using System;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// Represents the definition of a Scope in the Logix system.
/// </summary>
/// <remarks>
/// A Scope defines the hierarchical container for elements in a Logix project,
/// such as programs, routines, and controller-level objects. The class scope using a <see cref="ScopeLevel"/> and
/// container name to identify the scope of a given element.
/// </remarks>
public sealed class Scope
{
    /// <summary>
    /// Creates a new scope with the provided <see cref="ScopeLevel"/> and container name.
    /// </summary>
    private Scope(ScopeLevel level, string container, string? routine = null)
    {
        Level = level;
        Container = container;
        Routine = routine;
    }

    /// <summary>
    /// Creates a new scope instance based on the provided <see cref="XElement"/>.
    /// </summary>
    private Scope(XElement element)
    {
        if (element is null)
            throw new ArgumentNullException(nameof(element));

        Level = DetermineLevel(element);
        Container = DetermineContainer(element);
        Routine = DetermineRoutine(element);
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
    public ScopeLevel Level { get; }

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
    public string Container { get; }

    /// <summary>
    /// Gets the name of the routine associated with the current <see cref="Scope"/> object, if available.
    /// </summary>
    /// <returns>
    /// A <see cref="string"/> representing the name of the routine, or <c>null</c> if the scope does not
    /// pertain to a routine.
    /// </returns>
    /// <remarks>
    /// The routine name is determined based on the XML structure of the underlying element and
    /// its relationship to other scope elements. It is only applicable if the scope represents
    /// a <c>RoutineScope</c>.
    /// </remarks>
    public string? Routine { get; }

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
    /// checks if the <see cref="Scope.Level"/> is equal to <see cref="ScopeLevel.Aoi"/>.
    /// </remarks>
    /// <returns>
    /// <c>true</c> if the current scope represents a routine level; otherwise, <c>false</c>.
    /// </returns>
    public bool IsRoutine => Level == ScopeLevel.Aoi;

    /// <summary>
    /// Gets a value indicating whether the current <see cref="Scope"/> is local.
    /// </summary>
    /// <remarks>
    /// A scope is considered local if its level is either <see cref="ScopeLevel.Program"/> or
    /// <see cref="ScopeLevel.Aoi"/>. Local scopes represent functional or hierarchical containers
    /// within a specific program or routine, as opposed to global scopes which span the entire controller.
    /// </remarks>
    /// <returns>
    /// <c>true</c> if the scope level is <see cref="ScopeLevel.Program"/> or <see cref="ScopeLevel.Aoi"/>;
    /// otherwise, <c>false</c>.
    /// </returns>
    public bool IsLocal => Level == ScopeLevel.Program || Level == ScopeLevel.Aoi;

    /// <summary>
    /// Gets a value indicating whether the current <see cref="Scope"/> object represents a logical context.
    /// </summary>
    /// <returns>
    /// <c>true</c> if the <see cref="Scope"/> is local (either Program or Routine level) and has a defined routine; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// A logical context is determined by the scope being on the Program or Routine level and having an associated routine.
    /// This property is typically used to identify cases where specific logic elements are defined within a contained scope.
    /// </remarks>
    public bool IsLogic => IsLocal && Routine is not null;

    /// <summary>
    /// Determines if the current <see cref="Scope"/> is local to the specified container.
    /// </summary>
    /// <param name="container">The name of the container to compare against.</param>
    /// <returns>
    /// <c>true</c> if the current scope is the controller and the container is empty,
    /// or if the container matches the current scope's container name. Returns <c>false</c> otherwise.
    /// </returns>
    public bool IsIn(string? container)
    {
        if (IsController && string.IsNullOrEmpty(container)) return true;
        return Container.IsEquivalent(container);
    }

    /// <summary>
    /// Determines whether this scope is visible to the specified scope, based on the scope's level and container.
    /// </summary>
    /// <param name="scope">The scope to check visibility against.</param>
    /// <returns>
    /// <c>true</c> if either scope instances are controller scoped, or if they are both
    /// locally (program or routine) scoped and have the same container name; otherwise, <c>false</c>.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown when the provided scope is null.</exception>
    /// <remarks>
    /// Note that here is that if either is globally scoped, then it is visible to the other. Otherwise,
    /// they need to be in the same local scope.
    /// </remarks>
    public bool IsVisibleTo(Scope scope)
    {
        if (scope is null)
            throw new ArgumentNullException(nameof(scope));

        if (scope.IsController || IsController) return true;
        return scope.IsLocal && IsLocal && scope.Container.IsEquivalent(Container);
    }

    /// <summary>
    /// Determines if the current <see cref="Scope"/> is local to the specified <paramref name="scope"/>, meaning
    /// it is program or routine scoped and has the same container name.
    /// </summary>
    /// <param name="scope">The scope to compare against the current scope.</param>
    /// <returns>
    /// <c>true</c> if both scopes are local, and their containers are equivalent; otherwise, <c>false</c>.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="scope"/> parameter is null.</exception>
    public bool IsLocalTo(Scope scope)
    {
        if (scope is null)
            throw new ArgumentNullException(nameof(scope));

        return scope.IsLocal && IsLocal && scope.Container.IsEquivalent(Container);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (obj is not Scope other)
            return false;

        return Level == other.Level &&
               StringComparer.OrdinalIgnoreCase.Equals(Container, other.Container) &&
               StringComparer.OrdinalIgnoreCase.Equals(Routine, other.Routine);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        var levelHash = Level.GetHashCode();
        var containerHash = StringComparer.OrdinalIgnoreCase.GetHashCode(Container);
        var routineHash = Routine is not null ? StringComparer.OrdinalIgnoreCase.GetHashCode(Routine) : 0;
        return levelHash ^ containerHash ^ routineHash;
    }

    /// <summary>
    /// Gets the default instance of <see cref="Scope"/> representing no specific scope in the Logix system.
    /// </summary>
    /// <returns>
    /// A <see cref="Scope"/> object with a <see cref="ScopeLevel"/> of <c>None</c> and an empty container name.
    /// </returns>
    /// <remarks>
    /// This property represents a null or undefined scope, typically used when an element does not belong
    /// to any specific container or hierarchical scope within the system.
    /// </remarks>
    public static Scope None => new(ScopeLevel.None, string.Empty);

    /// <summary>
    /// Gets a <see cref="Scope"/> instance representing the global Controller scope.
    /// </summary>
    /// <returns>
    /// A <see cref="Scope"/> with the <see cref="ScopeLevel"/> set to <c>Controller</c> and an empty container name.
    /// </returns>
    /// <remarks>
    /// The Controller scope represents the highest hierarchical level in a Logix project,
    /// containing global objects accessible by all programs within the project.
    /// </remarks>
    public static Scope Controller => new(ScopeLevel.Controller, string.Empty);

    /// <summary>
    /// Creates a new <see cref="Scope"/> instance representing a program-level scope.
    /// </summary>
    /// <param name="name">The name of the program container.</param>
    /// <param name="routine">Optional. The name of the routine within the program scope.</param>
    /// <returns>
    /// A <see cref="Scope"/> object with the <see cref="ScopeLevel"/> set to <see cref="ScopeLevel.Program"/>
    /// and the specified container and routine names.
    /// </returns>
    /// <remarks>
    /// Program scopes represent a hierarchical level within a Logix project where program-specific
    /// elements such as tags and routines are defined. If a routine name is provided, the scope
    /// will also represent a logical context within that program.
    /// </remarks>
    public static Scope Program(string name, string? routine = null) => new(ScopeLevel.Program, name, routine);

    /// <summary>
    /// Creates a new <see cref="Scope"/> instance representing an Add-On Instruction (AOI) scope.
    /// </summary>
    /// <param name="name">The name of the Add-On Instruction container.</param>
    /// <param name="routine">Optional. The name of the routine within the AOI scope.</param>
    /// <returns>
    /// A <see cref="Scope"/> object with the <see cref="ScopeLevel"/> set to <see cref="ScopeLevel.Aoi"/>
    /// and the specified container and routine names.
    /// </returns>
    /// <remarks>
    /// AOI scopes represent Add-On Instruction definitions in a Logix project. These are reusable
    /// instruction blocks that encapsulate logic and can contain local tags and routines. If a routine
    /// name is provided, the scope will represent a logical context within that AOI.
    /// </remarks>
    public static Scope Aoi(string name, string? routine = null) => new(ScopeLevel.Aoi, name, routine);

    /// <summary>
    /// Creates a new <see cref="Scope"/> instance based on the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The XML element representing the scope.</param>
    /// <returns>A new instance of the <see cref="Scope"/> class initialized with the provided element.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="element"/> is null.</exception>
    public static Scope Of(XElement element) => new(element);

    /// <summary>
    /// Determines if the provided objects are equal.
    /// </summary>
    /// <param name="left">An object to compare.</param>
    /// <param name="right">An object to compare.</param>
    /// <returns>true if the provided objects are equal; otherwise, false.</returns>
    public static bool operator ==(Scope? left, Scope? right) => Equals(left, right);

    /// <summary>
    /// Determines if the provided objects are not equal.
    /// </summary>
    /// <param name="left">An object to compare.</param>
    /// <param name="right">An object to compare.</param>
    /// <returns>true if the provided objects are not equal; otherwise, false.</returns>
    public static bool operator !=(Scope? left, Scope? right) => !Equals(left, right);

    /// <summary>
    /// Determines the scope level of the provided element based on its XML ancestry.
    /// </summary>
    /// <returns>
    /// A <see cref="ScopeLevel"/> value that represents the determined scope level.
    /// </returns>
    private static ScopeLevel DetermineLevel(XElement element)
    {
        if (!element.TryGetAncestor(IsContainer, out var container))
            return ScopeLevel.None;

        return container.Name.LocalName switch
        {
            L5XName.Program => ScopeLevel.Program,
            L5XName.AddOnInstructionDefinition => ScopeLevel.Aoi,
            L5XName.Controller => ScopeLevel.Controller,
            _ => ScopeLevel.None
        };
    }

    /// <summary>
    /// Determines the container of the provided XML element based on its ancestry and predefined L5X element types.
    /// </summary>
    /// <returns>
    /// A string representing the name of the determined container, or an empty string if no container is found.
    /// </returns>
    private static string DetermineContainer(XElement element)
    {
        if (!element.TryGetAncestor(IsContainer, out var container))
            return string.Empty;

        return container.Name.LocalName switch
        {
            L5XName.Program => container.LogixName(),
            L5XName.AddOnInstructionDefinition => container.LogixName(),
            _ => string.Empty
        };
    }

    /// <summary>
    /// Determines the name of the routine that contains the given XML element, if any.
    /// </summary>
    /// <param name="element">The XML element to evaluate.</param>
    /// <returns>
    /// The name of the routine if the XML element is contained within a routine,
    /// or null if the element is not associated with any routine.
    /// </returns>
    private static string? DetermineRoutine(XElement element)
    {
        if (!element.TryGetAncestor(e => e.Name.LocalName is L5XName.Routine, out var routine))
            return null;

        return routine.LogixName();
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
}