namespace L5Sharp.Core;

/// <summary>
/// A fluent API for building up a scope path that identifies a specific location or element in the L5X tree.
/// This interface is scoped to a single program, hence limiting the API to Routine/Tag, or allowing further scoping
/// into a specific Routine.
/// </summary>
public interface IScopeProgramBuilder
{
    /// <summary>
    /// Specifies the name of the program the object is scoped to. 
    /// </summary>
    /// <param name="routine">The name of the program.</param>
    /// <returns>The next step in the build fluent interface.</returns>
    IScopeRoutineBuilder In(string routine);

    /// <summary>
    /// Specifies the type name of the element for the scope instance. This can be any component or code name, such
    /// as Tag, Module, Rung, etc. 
    /// </summary>
    /// <param name="type">The element type name.</param>
    /// <returns>The next step in the build fluent interface.</returns>
    IScopeNameBuilder Type(string type);

    /// <summary>
    /// Specifies the scope as a path to a <c>Tag</c> with the provided name.
    /// </summary>
    /// <returns>The configured <see cref="Scope"/> instance.</returns>
    Scope Tag(string name);

    /// <summary>
    /// Specifies the scope as a path to a <c>Routine</c> with the provided name.
    /// </summary>
    /// <returns>The configured <see cref="Scope"/> instance.</returns>
    Scope Routine(string name);
}