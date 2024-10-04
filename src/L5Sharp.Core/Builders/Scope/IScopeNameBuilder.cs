namespace L5Sharp.Core;

/// <summary>
/// A nested builder interface for specifying the target name of the a <see cref="Scope"/> instance.
/// </summary>
public interface IScopeNameBuilder
{
    /// <summary>
    /// Specifies the element type the object is scoped to. 
    /// </summary>
    /// <param name="name">The name of the target.</param>
    /// <returns>The next step in the build fluent interface.</returns>
    Scope Named(string name);
}