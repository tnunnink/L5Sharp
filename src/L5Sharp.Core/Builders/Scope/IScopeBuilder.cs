namespace L5Sharp.Core;

/// <summary>
/// A fluent API for building up a scope path that identifies a specific location or element in the L5X tree.
/// </summary>
public interface IScopeBuilder
{
    /// <summary>
    /// Specifies the name of the program the object is scoped to. 
    /// </summary>
    /// <param name="program">The name of the program.</param>
    /// <returns>The next step in the build fluent interface.</returns>
    IScopeProgramBuilder In(string program);

    /// <summary>
    /// Specifies the type name of the element for the scope instance. This can be any component or code name, such
    /// as Tag, Module, Rung, etc. 
    /// </summary>
    /// <param name="type">The element type name.</param>
    /// <returns>The next step in the build fluent interface.</returns>
    IScopeNameBuilder Type(string type);

    /// <summary>
    /// Specifies the scope as a path to a data type with the provided name. 
    /// </summary>
    /// <returns>The configured <see cref="Scope"/> instance.</returns>
    Scope DataType(string name);

    /// <summary>
    /// Specifies ... 
    /// </summary>
    /// <returns>The configured <see cref="Scope"/> instance.</returns>
    Scope Module(string name);

    /// <summary>
    /// Specifies ... 
    /// </summary>
    /// <returns>The configured <see cref="Scope"/> instance.</returns>
    Scope Aoi(string name);

    /// <summary>
    /// Specifies ... 
    /// </summary>
    /// <returns>The configured <see cref="Scope"/> instance.</returns>
    Scope Tag(string name);

    /// <summary>
    /// Specifies ... 
    /// </summary>
    /// <returns>The configured <see cref="Scope"/> instance.</returns>
    Scope Program(string name);

    /// <summary>
    /// Specifies ... 
    /// </summary>
    /// <returns>The configured <see cref="Scope"/> instance.</returns>
    Scope Task(string name);
}