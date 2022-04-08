using L5Sharp.Core;

namespace L5Sharp.Builders
{
    /// <summary>
    /// The intermediate step of the <see cref="IModuleBuilder"/> that required the catalog number value.
    /// </summary>
    public interface IModuleCatalogNumberStep
    {
        /// <summary>
        /// Configures the catalog number of the module being created.
        /// </summary>
        /// <param name="catalogNumber">The value of the catalog number that represents the module to create.</param>
        /// <returns>A <see cref="IModuleBuilder"/> instance with the configured catalog number.</returns>
        IModuleBuilder WithCatalog(CatalogNumber catalogNumber);
    }
}