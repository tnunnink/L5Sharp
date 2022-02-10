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
    /// This object must be retrieved from the <see cref="LogixCatalog"/> service object. It serves mostly as a container
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
        /// <param name="ports">The set of <see cref="PortDefinition"/> objects that define the port connections for the module.</param>
        /// <param name="description">The description of the module.</param>
        public ModuleDefinition(CatalogNumber catalogNumber, Vendor vendor, ProductType productType,
            ushort productCode,
            IEnumerable<Revision> revisions,
            IEnumerable<ModuleCategory> categories,
            IEnumerable<PortDefinition> ports,
            string description)
        {
            CatalogNumber = catalogNumber;
            Vendor = vendor;
            ProductType = productType;
            ProductCode = productCode;
            Revisions = new ReadOnlyCollection<Revision>(revisions.ToList());
            Categories = new ReadOnlyCollection<ModuleCategory>(categories.ToList());
            Ports = new ReadOnlyCollection<PortDefinition>(ports.ToList());
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
        public IReadOnlyCollection<PortDefinition> Ports { get; }

        /// <summary>
        /// Configures the <see cref="Ports"/> of the definition in such that the first <b>Non-Ethernet</b> type port is the
        /// upstream port with the provided slot number, and the first <b>Ethernet</b> type port (if exists) is
        /// the downstream port with the provided ipAddress (if not null).
        /// </summary>
        /// <param name="slot">The slot number of connecting port to configure.</param>
        /// <param name="ipAddress">The IP address of the local port to configure.
        /// If not provided will default to empty string.</param>
        /// <remarks>
        /// Provides a simple way to configure the port connections by supplying a slot number and optional IP.
        /// This method assumes the port with the slot number will be the upstream connecting port.
        /// </remarks>
        /// <seealso cref="ConfigurePorts(System.Net.IPAddress,byte)"/>
        public void ConfigurePorts(byte slot, IPAddress? ipAddress = null)
        {
            var primary = Ports.FirstOrDefault(p => p.Type != "Ethernet");

            if (primary is not null)
            {
                primary.Upstream = true;
                primary.Address = slot.ToString();
            }

            var secondary = Ports.FirstOrDefault(p => p.Type == "Ethernet");

            if (secondary is not null)
                secondary.Address = ipAddress?.ToString() ?? IPAddress.Any.ToString();
        }
        
        /// <summary>
        /// Configures the <see cref="Ports"/> of the definition in such that the first <b>Ethernet</b> type port is the
        /// upstream connecting port with the provided IP address, and the first <b>Non-Ethernet</b> type port (if exists) is
        /// the downstream local port with the provided slot number.
        /// </summary>
        /// <param name="ipAddress">The IP address of the connecting port to configure.</param>
        /// <param name="slot">The slot number of local port to configure. If not provided will default to 0.</param>
        /// <remarks>
        /// Provides a simple way to configure the port connections by supplying a IP and optional slot number.
        /// This method assumes the port with the IP address will be the upstream connecting port.
        /// </remarks>
        /// <seealso cref="ConfigurePorts(byte,System.Net.IPAddress?)"/>
        public void ConfigurePorts(IPAddress ipAddress, byte slot)
        {
            var primary = Ports.FirstOrDefault(p => p.Type == "Ethernet");

            if (primary is not null)
            {
                primary.Upstream = true;
                primary.Address = ipAddress.ToString();
            }

            var secondary = Ports.FirstOrDefault(p => p.Type != "Ethernet");

            if (secondary is not null)
                secondary.Address = slot.ToString();
        }
    }
}