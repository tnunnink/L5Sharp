using L5Sharp.Components;
using L5Sharp.Core;

namespace L5Sharp
{
    /// <summary>
    /// A service that allows lookups of <see cref="ModuleDefinition"/> for a given <see cref="Module"/> based on the
    /// value of a provided <see cref="CatalogNumber"/>. 
    /// </summary>
    public interface ICatalogService
    {
        /// <summary>
        /// Gets the <see cref="ModuleDefinition"/> for the specified <see cref="CatalogNumber"/> value.
        /// </summary>
        /// <param name="catalogNumber">The catalog number of the module to lookup.</param>
        /// <returns>A new <see cref="ModuleDefinition"/> object containing the data can be used to instantiate or
        /// create new <see cref="Module"/> components.</returns>
        ModuleDefinition Lookup(string catalogNumber);
    }
}