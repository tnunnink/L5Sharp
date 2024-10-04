namespace L5Sharp.Core;

public interface IScopeTypeBuilder
{
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
    Scope Routine(string name);

    /// <summary>
    /// Specifies ... 
    /// </summary>
    /// <returns>The configured <see cref="Scope"/> instance.</returns>
    Scope Task(string name);

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