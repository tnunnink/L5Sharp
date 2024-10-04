namespace L5Sharp.Core;

/// <summary>
/// A fluent API for building up a scope path that identifies a specific location or element in the L5X tree.
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
    /// Specifies ... 
    /// </summary>
    /// <returns>The next step in the build fluent interface.</returns>
    Scope Rung(int number);

    /// <summary>
    /// Specifies ... 
    /// </summary>
    /// <returns>The next step in the build fluent interface.</returns>
    Scope Line(int number);

    /// <summary>
    /// Specifies ... 
    /// </summary>
    /// <returns>The next step in the build fluent interface.</returns>
    Scope Sheet(int number);
}