using System.Collections.Generic;
using L5Sharp.Components;
using L5Sharp.Enums;
using L5Sharp.Rockwell;

namespace L5Sharp.Core
{
    /// <summary>
    /// A set of properties that defines a <see cref="Module"/> component.
    /// </summary>
    /// <remarks>
    /// This object must be retrieved from the <see cref="ModuleCatalog"/> service object. It serves mostly as a container
    /// of data that can be retrieved in order to created a valid <see cref="Module"/> instance using a known
    /// <see cref="CatalogNumber"/>. 
    /// </remarks>
    public class ModuleDefinition
    {
        /// <summary>
        /// Creates a new <see cref="ModuleDefinition"/> object with the provided data.
        /// </summary>
        /// <param name="catalogNumber">The catalog number of the module.</param>
        /// <param name="vendor">The vendor id or entity object of the module.</param>
        /// <param name="productType">The product type id or entity of the module.</param>
        /// <param name="productCode">The unique product code for the module.</param>
        /// <param name="revisions">A set of <see cref="Revision"/> objects that are valid for the module.</param>
        /// <param name="categories">A set of <see cref="ModuleCategory"/> options that are applicable to the module.</param>
        /// <param name="ports">The set of <see cref="Port"/> objects that define the port connections for the module.</param>
        /// <param name="description">The description of the module.</param>
        public ModuleDefinition(string catalogNumber, Vendor vendor, ProductType productType, ushort productCode,
            IEnumerable<Revision> revisions,
            IEnumerable<ModuleCategory> categories,
            IEnumerable<Port> ports,
            string description)
        {
            CatalogNumber = catalogNumber;
            Vendor = vendor;
            ProductType = productType;
            ProductCode = productCode;
            Revisions = revisions;
            Categories = categories;
            Ports = ports;
            Description = description;
        }

        /// <summary>
        /// Gets the <see cref="CatalogNumber"/> of the current <see cref="ModuleDefinition"/>.
        /// </summary>
        public string CatalogNumber { get; }

        /// <summary>
        /// Gets the description of the <see cref="ModuleDefinition"/>.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Gets the <see cref="Vendor"/> of the current <see cref="ModuleDefinition"/>.
        /// </summary>
        public Vendor Vendor { get; }

        /// <summary>
        /// Gets the <see cref="ProductType"/> of the current <see cref="ModuleDefinition"/>.
        /// </summary>
        public ProductType ProductType { get; }

        /// <summary>
        /// Gets the <see cref="ushort"/> code number that identifies the <see cref="ModuleDefinition"/>. 
        /// </summary>
        public ushort ProductCode { get; }

        /// <summary>
        /// Gets a collection of valid <see cref="Revisions"/> for the <see cref="ModuleDefinition"/>.
        /// </summary>
        public IEnumerable<Revision> Revisions { get; }

        /// <summary>
        /// Gets a collection of <see cref="ModuleCategory"/> that apply to the module.
        /// </summary>
        public IEnumerable<ModuleCategory> Categories { get; }

        /// <summary>
        /// Gets the set of <see cref="Ports"/> that define the connections of the module.
        /// </summary>
        public IEnumerable<Port> Ports { get; }
    }
}