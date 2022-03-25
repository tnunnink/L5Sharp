using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
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
        private const string Ethernet = "Ethernet";
        
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
            Revisions = new ReadOnlyCollection<Revision>(revisions.ToList());
            Categories = new ReadOnlyCollection<ModuleCategory>(categories.ToList());
            Ports = new ReadOnlyCollection<Port>(ports.ToList());
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
        public IReadOnlyCollection<Revision> Revisions { get; }

        /// <summary>
        /// Gets a collection of <see cref="ModuleCategory"/> that the Module.
        /// </summary>
        public IReadOnlyCollection<ModuleCategory> Categories { get; }

        /// <summary>
        /// Gets the set of <see cref="Ports"/> that should be defined on the Module.
        /// </summary>
        public IReadOnlyCollection<Port> Ports { get; }

        /// <summary>
        /// Configures the <see cref="Ports"/> of the definition in such that the first <b>Non-Ethernet</b> type port is the
        /// upstream port with the provided slot number, and the first <b>Ethernet</b> type port (if exists) is
        /// the downstream port with the provided ipAddress (if not null).
        /// </summary>
        /// <param name="upstreamType">The port type of the upstream parent that determines which port is the primary
        /// upstream port.</param>
        /// <param name="upstreamAddress">The address of the upstream port.</param>
        /// <param name="downstreamAddress">The address of the downstream port.</param>
        /// <remarks>
        /// Provides a simple way to configure the port connections by supplying a slot number and optional IP.
        /// This method assumes the port with the slot number will be the upstream connecting port.
        /// </remarks>
        public ModuleDefinition ConfigurePorts(string upstreamType, string upstreamAddress, string downstreamAddress)
        {
            var ports = new List<Port>();

            var upstream = Ports.FirstOrDefault(p => p.Type == upstreamType)?.Configure(upstreamAddress, true);
            if (upstream is not null)
                ports.Add(upstream);

            var downstream = Ports.FirstOrDefault(p => p.Type != upstreamType)?.Configure(downstreamAddress);
            if (downstream is not null)
                ports.Add(downstream);

            return new ModuleDefinition(CatalogNumber, Vendor, ProductType, ProductCode, Revisions, Categories,
                ports, Description);
        }
        
        /// <summary>
        /// Configures the <see cref="Ports"/> of the definition such that the first found non 'Ethernet' type port is
        /// the upstream port with the specified slot address, and any 'Ethernet' type ports are downstream ports with
        /// the provided IP address, therefore making a backplane ready port configuration.
        /// </summary>
        /// <param name="slot">The slot number address of the upstream port to configure.</param>
        /// <param name="ipAddress">The optional <see cref="IPAddress"/> of the downstream port to configure.
        /// If not provided, will default to the <see cref="IPAddress.Any"/> value (i.e. 0.0.0.0).</param>
        /// <returns>A new <see cref="ModuleDefinition"/> with the same properties but configured ports using the
        /// provided slot and IP address.</returns>
        public ModuleDefinition ConfigureBackplane(byte slot, IPAddress? ipAddress = null)
        {
            var ports = new List<Port>();
            
            var upstream = Ports.FirstOrDefault(p => p.Type != Ethernet)?.Configure(slot.ToString(), true);
            
            //should we fail?
            if (upstream is not null)
                ports.Add(upstream);

            var downstream = Ports.Where(p => p.Type == Ethernet)
                .Select(p => p.Configure(ipAddress?.ToString() ?? IPAddress.Any.ToString()));
            ports.AddRange(downstream);
            
            return new ModuleDefinition(CatalogNumber, Vendor, ProductType, ProductCode, Revisions, Categories, ports,
                Description);
        }

        /// <summary>
        /// Configures the <see cref="Ports"/> of the definition such that the first found 'Ethernet' type port is
        /// the upstream port with the specified IP address, and the first non 'Ethernet' type port is the downstream
        /// ports with the provided slot number, therefore making a backplane ready port configuration.
        /// </summary>
        /// <param name="ipAddress">The <see cref="IPAddress"/> of the upstream port to configure.</param>
        /// <param name="slot">The optional slot number of the downstream port to configure. If not provided,
        /// will default to 0.</param>
        /// <returns>A new <see cref="ModuleDefinition"/> with the same properties but configured ports using the
        /// provided slot and IP address.</returns>
        public ModuleDefinition ConfigureEthernet(IPAddress ipAddress, byte slot = default)
        {
            var ports = new List<Port>();
            
            //should we fail?
            var upstream = Ports.FirstOrDefault(p => p.Type == Ethernet)?.Configure(ipAddress.ToString(), true);
            if (upstream is not null)
                ports.Add(upstream);

            var downstream = Ports.Where(p => p.Type != Ethernet).Select(p => p.Configure(slot.ToString()));
            ports.AddRange(downstream);
            
            return new ModuleDefinition(CatalogNumber, Vendor, ProductType, ProductCode, Revisions, Categories, ports,
                Description);
        }
    }
}