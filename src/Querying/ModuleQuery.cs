using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.L5X;

namespace L5Sharp.Querying
{
    /// <summary>
    /// A fluent <see cref="ILogixQuery{TResult}"/> that adds advanced querying for <see cref="IModule"/>
    /// elements within the L5X context.  
    /// </summary>  
    public class ModuleQuery : LogixQuery<IModule>
    {
        /// <summary>
        /// Creates a new <see cref="ModuleQuery"/> with the provided source element to query.
        /// </summary>
        /// <param name="source">An <see cref="IEnumerable{T}"/> containing the source <see cref="XElement"/> objects
        /// to query.</param>
        public ModuleQuery(IEnumerable<XElement> source) : base(source)
        {
        }

        /// <summary>
        /// Filters the collection to include only <see cref="IModule"/> objects with the specified parent name.
        /// </summary>
        /// <param name="parentName">The name of the module that is the immediate parent of the modules to filter.</param>
        /// <returns>A new <see cref="ModuleQuery"/> containing ony modules with the specified parent name.</returns>
        public ModuleQuery WithParent(ComponentName parentName)
        {
            if (parentName is null)
                throw new ArgumentNullException(nameof(parentName));

            var results = this.Where(e =>
                e.Attribute(L5XAttribute.ParentModule.ToString())?.Value == parentName.ToString());

            return new ModuleQuery(results);
        }

        /// <summary>
        /// Filters the collection of modules to include only those that satisfy the provided catalog number predicate.
        /// </summary>
        /// <param name="predicate">The condition of the catalog number to filter on.</param>
        /// <returns>A new <see cref="ModuleQuery"/> containing module that satisfy the provided catalog number
        /// condition.</returns>
        public ModuleQuery WithCatalog(Predicate<CatalogNumber> predicate)
        {
            if (predicate is null)
                throw new ArgumentNullException(nameof(predicate));

            var results = this.Where(e =>
            {
                var catalogNumber = e.Attribute(L5XAttribute.CatalogNumber.ToString());
                return catalogNumber is not null && predicate.Invoke(catalogNumber.Value.Parse<CatalogNumber>());
            });

            return new ModuleQuery(results);
        }
    }
}