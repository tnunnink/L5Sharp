namespace L5Sharp.Core;

/// <summary>
/// Defines a contract for building and configuring reference location components within a reference path.
/// Provides methods to associate a reference with a specific target identifier, either by name or numerical value.
/// </summary>
public interface IReferenceLocationBuilder : IReferenceBuilder
{
    /// <summary>
    /// Configures the reference by associating it with a specific target identifier by name.
    /// </summary>
    /// <param name="name">The name identifier to associate with the reference.</param>
    /// <returns>An instance of <see cref="IReferenceScopeBuilder"/> that allows further configuration of the reference.</returns>
    IReferenceScopeBuilder Named(string name);

    /// <summary>
    /// Configures the reference by associating it with a specific target identifier by numerical value.
    /// </summary>
    /// <param name="number">The numerical identifier to associate with the reference.</param>
    /// <returns>An instance of <see cref="IReferenceScopeBuilder"/> that allows further configuration of the reference.</returns>
    IReferenceScopeBuilder Number(int number);
}