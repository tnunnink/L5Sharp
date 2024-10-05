namespace L5Sharp.Core;

/// <summary>
/// 
/// </summary>
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
    /// Specifies the scope as a path to a <c>DataType</c> with the provided name. 
    /// </summary>
    /// <returns>The configured <see cref="Scope"/> instance.</returns>
    Scope DataType(string name);

    /// <summary>
    /// Specifies the scope as a path to a <c>Module</c> with the provided name.
    /// </summary>
    /// <returns>The configured <see cref="Scope"/> instance.</returns>
    Scope Module(string name);

    /// <summary>
    /// Specifies the scope as a path to a <c>AddOnInstruction</c> with the provided name.
    /// </summary>
    /// <returns>The configured <see cref="Scope"/> instance.</returns>
    Scope Instruction(string name);

    /// <summary>
    /// Specifies the scope as a path to a <c>Tag</c> with the provided name.
    /// </summary>
    /// <returns>The configured <see cref="Scope"/> instance.</returns>
    Scope Tag(string name);

    /// <summary>
    /// Specifies the scope as a path to a <c>Program</c> with the provided name.
    /// </summary>
    /// <returns>The configured <see cref="Scope"/> instance.</returns>
    Scope Program(string name);

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