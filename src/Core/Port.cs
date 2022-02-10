using System;
using System.Linq;
using System.Net;
using L5Sharp.Extensions;

namespace L5Sharp.Core
{
    /// <summary>
    /// A component of a <see cref="Module"/> that represents the means for connecting devices on a network or in a chassis.
    /// </summary>
    /// <remarks>
    /// A Port is a component that helps define the structure of the IO tree.
    /// The Port may be the slot on the chassis or backplane where the <see cref="Module"/> resides.
    /// This may also be the network address (IP) of the device. Each Port may also have a <see cref="Bus"/> that
    /// contains child Modules. Each Port is identifiable by the <see cref="Id"/> property. 
    /// </remarks>
    public sealed class Port
    {
        internal Port(Module module, PortDefinition definition)
        {
            Module = module ?? throw new ArgumentNullException(nameof(module));
            Id = definition.Id;
            Type = definition.Type ?? throw new ArgumentNullException(nameof(definition.Type));
            Upstream = definition.Upstream;
            Address = definition.Address;

            if (!Upstream && (Address.IsByte() || Address.IsIPv4()))
                Bus = new Bus(this, definition.BusSize);
        }

        /// <summary>
        /// Gets the <see cref="Module"/> instance that owns the current <see cref="Port"/>.
        /// </summary>
        public Module Module { get; }

        /// <summary>
        /// Gets the Id of the <see cref="Port"/>.
        /// </summary>
        /// <remarks>
        /// All Modules have at least one port. Each is identified by the Id property. Typically Modules will have one
        /// or two ports with Ids '1' and '2', respectively.
        /// </remarks>
        public int Id { get; }

        /// <summary>
        /// Gets the address of the <see cref="Port"/>.
        /// </summary>
        /// <remarks>
        /// The Address of a port represents the slot or IP address for the port. This value is used in the
        /// <see cref="Module"/> to determine the slot and IP properties. All ports must have an Address.
        /// </remarks>
        public string Address { get; }

        /// <summary>
        /// Gets the value that represents the <see cref="Port"/> type.
        /// </summary>
        /// <remarks>
        /// This value appears to be specific to the product. Ports with IP will have 'Network' for their type. This
        /// property is required for importing a <see cref="Module"/> correctly.
        /// </remarks>
        public string Type { get; }

        /// <summary>
        /// Gets the value indicating whether the there are devices upstream of the current <see cref="Port"/>.
        /// </summary>
        /// <remarks>
        /// From examining the L5X examples, the upstream flag seems to indicate that the port is one connected to a
        /// parent module living 'upstream' of the current device. Non-upstream ports seem to indicate they contain
        /// child modules, or connect to devices living 'downstream' of the current Module. This property must be set
        /// correctly to be able to import L5X.
        /// </remarks>
        public bool Upstream { get; }

        /// <summary>
        /// Gets the <see cref="Bus"/> for the <see cref="Port"/> object.
        /// </summary>
        /// <remarks>
        /// A port's bus represents the chassis or network on which Modules are accessible to the port.
        /// Only downstream modules will have a valid Bus. 
        /// </remarks>
        public Bus? Bus { get; }

        /// <summary>
        /// Creates a child <see cref="Module"/> and adds to the current <see cref="Bus"/> of the Port.
        /// </summary>
        /// <param name="name">The name of the module.</param>
        /// <param name="catalogNumber">The catalog number of the module.</param>
        /// <param name="slot">The slot number that identifies where on the bus to add the new module.
        /// If the provided slot number is not available, then it will be placed at the next available slot.</param>
        /// <param name="description">The option description of the module.</param>
        /// <returns>The instance of the <see cref="Module"/> that was created and added to the bus.</returns>
        /// <exception cref="InvalidOperationException">The current port is not a downstream local port with a non-null bus.</exception>
        public Module NewModule(ComponentName name, CatalogNumber catalogNumber, byte slot = default,
            string? description = null)
        {
            if (Bus is null)
                throw new InvalidOperationException(
                    "The current port does not have a Bus defined. Can only add modules to downstream ports.");

            var catalog = new LogixCatalog();
            var definition = catalog.Lookup(catalogNumber);

            ConfigurePorts(definition, slot);

            var module = new Module(name, definition, Module.Name, Id, description);

            Bus.Add(module);

            return module;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="catalogNumber"></param>
        /// <param name="slot">The slot number of the current</param>
        /// <param name="ipAddress">The optional IP address of the module if the specified module has a network type port.
        /// 
        /// </param>
        /// <param name="description">The description of the module.</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public Module NewModule(ComponentName name, CatalogNumber catalogNumber, byte slot, IPAddress ipAddress,
            string? description = null)
        {
            if (Bus is null)
                throw new InvalidOperationException(
                    "The current port does not have a Bus defined. Can only add modules to downstream ports.");

            var catalog = new LogixCatalog();
            var definition = catalog.Lookup(catalogNumber);

            ConfigurePorts(definition, slot, ipAddress);

            var module = new Module(name, definition, Module.Name, Id, description);

            Bus.Add(module);

            return module;
        }

        private void ConfigurePorts(ModuleDefinition definition, byte slot, IPAddress ipAddress)
        {
            var address = Type == "Ethernet" ? ipAddress.ToString() : slot.ToString();

            foreach (var port in definition.Ports)
            {
                port.Upstream = port.Type == Type && !port.DownstreamOnly;
                port.Address = port.Upstream ? Bus!.GetAddress(address) :
                    port.Type == "Ethernet" ? ipAddress.ToString() : slot.ToString();
            }

            if (!definition.Ports.Any(p => p.Upstream))
                throw new ArgumentException();
        }

        private void ConfigurePorts(ModuleDefinition definition, byte slot)
        {
            var address = slot.ToString();

            foreach (var port in definition.Ports)
            {
                port.Upstream = port.Type == Type && !port.DownstreamOnly;

                if (port.Upstream)
                    port.Address = Bus!.GetAddress(address);
            }
        }
    }
}