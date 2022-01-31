using System.Collections.Generic;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    /// <summary>
    /// A set of properties that defines the Logix <see cref="IModule"/>.  
    /// </summary>
    public class ModuleDefinition
    {
        private ModuleDefinition(CatalogNumber catalogNumber,
            Vendor? vendor = null,
            ProductType? productType = null,
            Revision? revision = null)
        {
            CatalogNumber = catalogNumber;
        }

        /// <summary>
        /// Gets the <see cref="CatalogNumber"/> of the current <see cref="ModuleDefinition"/>.
        /// </summary>
        public CatalogNumber CatalogNumber { get; }

        /// <summary>
        /// Gets the <see cref="Vendor"/> of the current <see cref="ModuleDefinition"/>.
        /// </summary>
        public Vendor Vendor { get; }

        /// <summary>
        /// Gets the <see cref="ProductType"/> of the current <see cref="ModuleDefinition"/>.
        /// </summary>
        public ProductType ProductType { get; }

        public IEnumerable<Revision> Revisions { get; }
        
        public string Description { get; }
        
        public IEnumerable<ModuleCategory> Categories { get; }

        public IEnumerable<Port> Ports { get; }
    }
}