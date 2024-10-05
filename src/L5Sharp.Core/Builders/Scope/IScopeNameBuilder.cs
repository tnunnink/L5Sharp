namespace L5Sharp.Core;

/// <summary>
/// A nested builder interface for specifying the target name of the a <see cref="Scope"/> instance.
/// </summary>
public interface IScopeNameBuilder
{
    /// <summary>
    /// Specifies the name (or number) of the target element in the scope instance. 
    /// </summary>
    /// <param name="name">The name or number of the target element.</param>
    /// <returns>The next step in the build fluent interface.</returns>
    Scope Named(string name);
}