using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Enums;

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
        private readonly List<Revision> _revisions;
        private readonly List<ModuleCategory> _categories;
        private List<Port> _ports;

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
        public ModuleDefinition(CatalogNumber catalogNumber, Vendor vendor, ProductType productType, ushort productCode,
            IEnumerable<Revision> revisions,
            IEnumerable<ModuleCategory> categories,
            IEnumerable<Port> ports,
            string description)
        {
            CatalogNumber = catalogNumber;
            Vendor = vendor;
            ProductType = productType;
            ProductCode = productCode;
            _revisions = revisions.ToList();
            _categories = categories.ToList();
            _ports = ports.ToList();
            Revision = _revisions.Max();
            Description = description;
        }

        /// <summary>
        /// Gets the <see cref="CatalogNumber"/> of the current <see cref="ModuleDefinition"/>.
        /// </summary>
        public CatalogNumber CatalogNumber { get; }

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
        public IEnumerable<Revision> Revisions => _revisions;

        /// <summary>
        /// Gets the configured revision of the module definition. 
        /// </summary>
        /// <remarks>
        /// This will default to the highest revision of the module definition until configured by the user using the
        /// <see cref="ConfigureRevision"/> method.
        /// </remarks>
        public Revision Revision { get; private set; }

        /// <summary>
        /// Gets a collection of <see cref="ModuleCategory"/> that apply to the module.
        /// </summary>
        public IEnumerable<ModuleCategory> Categories => _categories;

        /// <summary>
        /// Gets the set of <see cref="Ports"/> that define the connections of the module.
        /// </summary>
        public IEnumerable<Port> Ports => _ports;

        /// <summary>
        /// Configures the <see cref="Revision"/> property of the module. This property is used by various components
        /// to set the revision property of newly created modules.
        /// </summary>
        /// <param name="revision">The revision number to configure.</param>
        public void ConfigureRevision(Revision revision)
        {
            if (!_revisions.Contains(revision))
                throw new ArgumentException(
                    $"The provided revision {revision} is not a valid available revision for the definition.");

            Revision = revision;
        }

        /// <summary>
        /// Configures the <see cref="Ports"/> property of the definition in such that the first port specified by
        /// upstreamType is the upstream port with the provided upstreamAddress, and the only other port
        /// (assuming 2 ports) is the downstream port with the provided downstream address and bus size.
        /// </summary>
        /// <param name="upstreamType">The port type of the upstream port.</param>
        /// <param name="upstreamAddress">The address of the upstream port.</param>
        /// <param name="downstreamAddress">The optional address of the downstream port. If not provided, the address
        /// will default to an empty string for all downstream ports.</param>
        /// <param name="busSize">The optional size of the downstream bus. If not provided will default to zero.</param>
        public void ConfigurePorts(string upstreamType, string upstreamAddress, string? downstreamAddress = null,
            byte busSize = default)
        {
            var ports = new List<Port>();

            var upstream = _ports.FirstOrDefault(p => p.Type == upstreamType);

            if (upstream is null)
                throw new InvalidOperationException("");

            if (upstream.DownstreamOnly)
                throw new InvalidOperationException();

            ports.Add(new Port(upstream.Id, upstream.Type, upstreamAddress, true));
            ports.AddRange(_ports.Where(p => p.Type != upstreamType).Select(ds =>
                new Port(ds.Id, ds.Type, downstreamAddress, false, busSize, ds.DownstreamOnly)));

            _ports = ports;
        }
    }
}