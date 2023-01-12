using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        /// Determines if the <see cref="ModuleDefinition"/> has a port capable of connecting to a module with
        /// the specified type.
        /// </summary>
        /// <param name="portType">The port type of the module to test the connection of a port in the current
        /// definition.</param>
        /// <returns>true if the definition object contains a port with the specified type that is not set to
        /// downstream only; otherwise, false.</returns>
        public bool CanConnectTo(string portType) => _ports.Any(p => p.Type == portType && !p.DownstreamOnly);

        /// <summary>
        /// Configures the <see cref="Revision"/> property of the <see cref="ModuleDefinition"/>.
        /// This property is used by various components to set the revision property of newly created modules.
        /// </summary>
        /// <param name="revision">The revision number to configure.</param>
        /// <exception cref="InvalidOperationException">The current definition object does not contain a revision that
        /// matches the one specified by <c>revision</c>.</exception>
        public void ConfigureRevision(Revision revision)
        {
            if (!_revisions.Contains(revision))
                throw new InvalidOperationException(
                    $"The provided revision {revision} is not a valid available revision for the definition.");

            Revision = revision;
        }

        /// <summary>
        /// Configures the <see cref="Ports"/> collection of the <see cref="ModuleDefinition"/> in such that the
        /// first port specified by upstreamType is the upstream port with the provided upstreamAddress,
        /// and all other ports are set as downstream ports with the provided downstream address and bus size, if provided.
        /// </summary>
        /// <param name="upstreamType">The port type of the upstream port.</param>
        /// <param name="upstreamAddress">The address of the upstream port.</param>
        /// <param name="downstreamAddress">The optional address of the downstream port. If not provided, the address
        /// will default to an empty string for all downstream ports.</param>
        /// <param name="busSize">The optional size of the downstream bus. If not provided will default to zero.</param>
        /// <exception cref="InvalidOperationException">No port exists with type upstreamType -or- the port with type
        /// upstreamType is configured as a downstream only port.</exception>
        public void ConfigurePorts(string upstreamType, Address upstreamAddress,
            Address? downstreamAddress = null, byte busSize = default)
        {
            var ports = new List<Port>();

            var upstream = _ports.FirstOrDefault(p => p.Type == upstreamType);

            if (upstream is null)
                throw new InvalidOperationException(
                    $"No port of type {upstreamType} available on the current definition.");

            if (upstream.DownstreamOnly)
                throw new InvalidOperationException(
                    $"The port type {upstream.Type} is a downstream only port and can not be configured for upstream connections.");

            ports.Add(new Port(upstream.Id, upstream.Type, upstreamAddress, true));

            ports.AddRange(_ports.Where(p => p.Type != upstreamType).Select(ds =>
                new Port(ds.Id, ds.Type, downstreamAddress, false, busSize, ds.DownstreamOnly)));

            _ports = ports;
        }

        /// <summary>
        /// Configures the slot number of the first found non-Ethernet type port with the provided value.
        /// </summary>
        /// <param name="slot">The the slot number value to configure.</param>
        /// <exception cref="InvalidOperationException">No non-Ethernet type port exists for the current definition.</exception>
        public void ConfigureSlot(byte slot)
        {
            var target = _ports.FirstOrDefault(p => p.Type != "Ethernet");

            if (target is null)
                throw new InvalidOperationException(
                    $"No non-Ethernet port found on the current definition for {CatalogNumber}.");

            var port = new Port(target.Id, target.Type, Address.FromSlot(slot), target.Upstream, target.BusSize,
                target.DownstreamOnly);

            _ports.Remove(target);
            _ports.Add(port);
        }

        /// <summary>
        /// Configures the IP address of the first found Ethernet type port with the provided value.
        /// </summary>
        /// <param name="ipAddress">The IP address value to configure.</param>
        /// <exception cref="InvalidOperationException">No port of type Ethernet exists for the current definition.</exception>
        public void ConfigureIP(IPAddress ipAddress)
        {
            var target = _ports.FirstOrDefault(p => p.Type == "Ethernet");

            if (target is null)
                throw new InvalidOperationException(
                    $"No Ethernet port found on the current definition for {CatalogNumber}.");

            var port = new Port(target.Id, target.Type, Address.FromIP(ipAddress), target.Upstream, target.BusSize,
                target.DownstreamOnly);

            _ports.Remove(target);
            _ports.Add(port);
        }

        /// <summary>
        /// Configures the upstream property of the port with the specified portType value.
        /// </summary>
        /// <param name="portType">The string value of the port to configure upstream property for.</param>
        /// <param name="upstream">The upstream property value to configure. Defaults to true.</param>
        /// <exception cref="InvalidOperationException">portType value does not exits for the current definition.</exception>
        public void ConfigureUpstream(string portType, bool upstream = true)
        {
            var target = _ports.FirstOrDefault(p => p.Type == portType);

            if (target is null)
                throw new InvalidOperationException(
                    $"No port of type '{portType}' exists on the current definition for {CatalogNumber}.");

            var port = new Port(target.Id, target.Type, target.Address, upstream, target.BusSize,
                target.DownstreamOnly);

            _ports.Remove(target);
            _ports.Add(port);
        }
    }
}