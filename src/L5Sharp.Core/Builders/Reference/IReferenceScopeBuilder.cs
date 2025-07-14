namespace L5Sharp.Core;

/// <summary>
/// Defines a contract for configuring the scope of a reference during the building process.
/// This interface allows specification of container boundaries to restrict the reference's applicability.
/// </summary>
public interface IReferenceScopeBuilder : IReferenceBuilder
{
    /// <summary>
    /// Configures the reference scope by specifying one or more container names to restrict the scope of the reference.
    /// </summary>
    /// <param name="containers">An array of container names that define the scope of the reference.</param>
    /// <returns>An instance of <see cref="IReferenceScopeBuilder"/> configured with the specified container names.</returns>
    IReferenceScopeBuilder In(params string[] containers);
}