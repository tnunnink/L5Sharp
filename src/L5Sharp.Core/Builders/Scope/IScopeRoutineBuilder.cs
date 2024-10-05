namespace L5Sharp.Core;

/// <summary>
/// A fluent API for building up a scope path that identifies a specific location or element in the L5X tree.
/// This interface is scoped to a single routine, hence limiting the API to Rung/Line/Sheet types.
/// </summary>
public interface IScopeRoutineBuilder
{
    /// <summary>
    /// Specifies the type name of the element for the scope instance. This can be any component or code name, such
    /// as Tag, Module, Rung, etc. 
    /// </summary>
    /// <param name="type">The element type name.</param>
    /// <returns>The next step in the build fluent interface.</returns>
    IScopeNameBuilder Type(string type);

    /// <summary>
    /// Specifies the scope as a path to a <c>Rung</c> with the provided name.
    /// </summary>
    /// <returns>The configured <see cref="Scope"/> instance.</returns>
    Scope Rung(int number);

    /// <summary>
    /// Specifies the scope as a path to a <c>Line</c> with the provided name.
    /// </summary>
    /// <returns>The configured <see cref="Scope"/> instance.</returns>
    Scope Line(int number);

    /// <summary>
    /// Specifies the scope as a path to a <c>Sheet</c> with the provided name.
    /// </summary>
    /// <returns>The configured <see cref="Scope"/> instance.</returns>
    Scope Sheet(int number);
}