namespace L5Sharp.Core;

/// <summary>
/// Defines a builder interface for constructing and configuring import modules
/// within the system. This interface extends the functionality of
/// <see cref="IImportConfigBuilder{TBuilder}"/> by providing methods specific
/// to the configuration of module-level imports.
/// </summary>
public interface IImportModuleBuilder : IImportConfigBuilder<IImportModuleBuilder>
{
    /// <summary>
    /// Configures the module builder to connect to a specified parent using its name.
    /// </summary>
    /// <param name="parentName">The name of the parent to connect the module to.</param>
    /// <returns>The current instance of <see cref="IImportModuleBuilder"/> with the updated configuration.</returns>
    IImportModuleBuilder ConnectTo(string parentName);
}