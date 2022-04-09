using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using L5Sharp.Abstractions;
using L5Sharp.Enums;
using L5Sharp.Exceptions;

namespace L5Sharp.Core
{
    /// <summary>
    /// Represents a collection of <see cref="IModule"/> components assigned to  a specific <see cref="Port"/>.
    /// </summary>
    public sealed class Bus : IEnumerable<IModule>
    {
        private const string DefaultNetworkAddress = "192.168.1";
        private const string Ethernet = "Ethernet";
        private readonly Dictionary<PortAddress, IModule> _modules;
        private readonly IModule _parent;
        private readonly Port _port;

        /// <summary>
        /// Creates a new <see cref="Bus"/> with the provided port and parent module
        /// </summary>
        /// <param name="port">The <see cref="Port"/> that identifies where modules are are connected. This port is the
        /// downstream port of the parent module, but becomes the upstream parent port of modules created on the bus
        /// (i.e. the id value used for ParentPortId).</param>
        /// <param name="parent">The parent <see cref="IModule"/> for which modules on the bus belong. This module will
        /// be added to the collection when the address of the provided port is not null or empty.</param>
        /// <exception cref="ArgumentNullException">port or parent is null.</exception>
        public Bus(IModule parent, Port port)
        {
            _parent = parent ?? throw new ArgumentNullException(nameof(parent));
            _port = port ?? throw new ArgumentNullException(nameof(port));
            _modules = new Dictionary<PortAddress, IModule>();

            if (_port.Address.HasAddress)
                _modules.Add(_port.Address, parent);
        }

        /// <summary>
        /// Gets the type indicating which port types can be added to the <see cref="Bus"/>.
        /// </summary>
        public string Type => _port.Type;

        /// <summary>
        /// Gets the number of modules in the <see cref="Bus"/>.
        /// </summary>
        public int Count => _modules.Count;

        /// <summary>
        /// Gets the value of the current <see cref="Bus"/> size.
        /// </summary>
        /// <remarks>
        /// This value is based on the port bus size property that is provided to the bus upon construction.
        /// </remarks>
        public byte Size => _port.BusSize;

        /// <summary>
        /// Gets a value indicating whether the <see cref="Bus"/> is empty or has no modules.
        /// </summary>
        public bool IsEmpty => _modules.Count == 0;

        /// <summary>
        /// Gets a value indicating whether the <see cref="Bus"/> is full and can not add new modules.
        /// </summary>
        /// <remarks>
        /// For a bus to be full, it must have fixed size. When a bus is full, no more modules may be added to it.
        /// </remarks>
        public bool IsFull => IsFixed && _modules.Count == Size;

        /// <summary>
        /// Gets a value indicating whether the <see cref="Bus"/> is of fixed size or unbounded.
        /// </summary>
        /// <remarks>
        /// For a bus to be fixed, the parent port must have the BusSize property set to a non zero value.
        /// </remarks>
        public bool IsFixed => Size > 0;

        /// <summary>
        /// Gets a value indicating whether the <see cref="Bus"/> contains modules with slot based addresses.
        /// </summary>
        /// <remarks>
        /// The delineation is based on port type. If the parent port is a non-Ethernet type port, then the bus should
        /// represent a backplane that uses addressing based on a byte number (slot/node).
        /// </remarks>
        /// <seealso cref="IsEthernet"/>
        public bool IsBackplane => Type != Ethernet;

        /// <summary>
        /// Gets a value indicating whether the <see cref="Bus"/> contains Modules with IP based addresses.
        /// </summary>
        /// <remarks>
        /// The delineation is based on port type. If the parent port is a Ethernet type port, then the bus should
        /// represent a Ethernet network that uses addressing based on an IP address or host name.
        /// </remarks>
        /// <seealso cref="IsBackplane"/>
        public bool IsEthernet => Type == Ethernet;

        /// <summary>
        /// Gets a <see cref="IModule"/> at the specified address (slot or IP).
        /// </summary>
        /// <param name="address">The string address value that represents the slot or IP of the device to get.</param>
        public IModule? this[PortAddress address] => _modules.TryGetValue(address, out var module) ? module : null;

        /// <summary>
        /// Adds the provided <see cref="IModule"/> to the Bus collection at the address specified by the connecting port. 
        /// </summary>
        /// <param name="module">The <see cref="IModule"/> instance to add to the Bus.</param>
        /// <exception cref="ArgumentNullException">module is null.</exception>
        /// <exception cref="ComponentNameCollisionException">module name already exists on the bus.</exception>
        /// <exception cref="ArgumentException">
        /// module parent parameters do not match the Bus parent module values -or-
        /// there is no connecting port with a type that matches the bus type -or-
        /// the connecting port address is not available or valid for the current bus.
        /// </exception>
        public void Add(IModule module)
        {
            ValidateModule(module);

            var address = module.Ports.Upstream!.Address;

            _modules[address] = module;
        }

        /// <summary>
        /// Adds the provided collection of <see cref="IModule"/> objects to the <see cref="Bus"/>.
        /// </summary>
        /// <param name="modules">The collection of modules with configured ports and parent properties.</param>
        /// <exception cref="ArgumentNullException">module is null.</exception>
        /// <exception cref="ComponentNameCollisionException">module name already exists on the bus.</exception>
        /// <exception cref="ArgumentException">
        /// module parent parameters do not match the Bus parent module values -or-
        /// there is no connecting port with a type that matches the bus type -or-
        /// the connecting port address is not available or valid for the current bus.
        /// </exception>
        public void AddMany(IEnumerable<IModule> modules)
        {
            foreach (var module in modules)
                Add(module);
        }

        /// <summary>
        /// Gets the address of the <see cref="IModule"/> with the specified name.
        /// </summary>
        /// <param name="name">The name of the module for which to get the address of.</param>
        /// <returns>The string value representing the address (slot/IP) at which the module belongs to the current Bus.
        /// Null if the specified module does not exist.
        /// </returns>
        public PortAddress AddressOf(ComponentName name) =>
            _modules.SingleOrDefault(m => m.Value.Name == name).Key;

        /// <summary>
        /// Clears all <see cref="IModule"/> objects from the Bus, excluding the parent module.
        /// </summary>
        public void Clear()
        {
            var keys = _modules.Keys.Where(k => k != _port.Address).ToList();

            foreach (var key in keys)
                _modules.Remove(key);
        }

        /// <summary>
        /// Indicates whether a <see cref="IModule"/> with the specified address exists on the current <see cref="Bus"/>.
        /// </summary>
        /// <param name="address">The address of the module to search.</param>
        /// <returns>true if the bus contains a module with the specified address; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">address is null.</exception>
        public bool ContainsAddress(PortAddress address) => _modules.ContainsKey(address);

        /// <summary>
        /// Indicates whether a <see cref="IModule"/> with the specified name exists on the current <see cref="Bus"/>.
        /// </summary>
        /// <param name="name">The name of the module to search.</param>
        /// <returns>true if the bus contains a module with the specified name; otherwise, false.</returns>
        public bool ContainsName(ComponentName name) => _modules.Values.Any(m => m.Name == name);

        /// <summary>
        /// Creates a new <see cref="IModule"/> with the provided name and catalog number, and assigns it to the
        /// <see cref="Bus"/> collection at the provided slot number.
        /// If the provided slot number is not available, then will attempt to assign the module to the next available
        /// address. 
        /// </summary>
        /// <param name="name">The name of the module to create.</param>
        /// <param name="catalogNumber">The catalog number of the module to create.</param>
        /// <param name="slot">The slot number at which to assign the module.</param>
        /// <param name="description">The optional description for the module.</param>
        /// <param name="catalogService">The optional service provider that is responsible for retrieving
        /// the <see cref="ModuleDefinition"/> for the specified catalog number. By default this will use the
        /// <see cref="ModuleCatalog"/> built-in service provider.</param>
        /// <returns>The new <see cref="IModule"/> instance that was created and added to the bus.</returns>
        /// <exception cref="ModuleNotFoundException">catalogNumber could not be found by the catalog service provider.</exception>
        /// <exception cref="InvalidOperationException">The Bus instance is full and can not add new modules.</exception>
        /// <exception cref="ArgumentException">The module definition obtained for the specified catalog number
        /// does not have a valid upstream connection port for the current Bus type.</exception>
        /// <exception cref="ComponentNameCollisionException">name already exists on the current Bus instance.</exception>
        public IModule Create(ComponentName name, CatalogNumber catalogNumber, byte slot,
            string? description = null, ICatalogService? catalogService = null) =>
            NewModule(name, catalogNumber, PortAddress.FromSlot(slot), description, catalogService);

        /// <summary>
        /// Creates a new <see cref="IModule"/> with the provided name and catalog number, and assigns it to the
        /// <see cref="Bus"/> collection at the provided IP address.
        /// If the provided IP address is not available, then will attempt to assign the module to the next available
        /// address (on the default 192.168.1.1 network). 
        /// </summary>
        /// <param name="name">The name of the module to create.</param>
        /// <param name="catalogNumber">The catalog number of the module to create.</param>
        /// <param name="ip">The IP address at which to assign the module.</param>
        /// <param name="description">The optional description for the module.</param>
        /// <param name="catalogService">The optional service provider that is responsible for retrieving
        /// the <see cref="ModuleDefinition"/> for the specified catalog number. By default this will use the
        /// <see cref="ModuleCatalog"/> built-in service provider.</param>
        /// <returns>The new <see cref="IModule"/> instance that was created and added to the bus.</returns>
        /// <exception cref="ModuleNotFoundException">catalogNumber could not be found by the catalog service provider.</exception>
        /// <exception cref="InvalidOperationException">The Bus instance is full and can not add new modules.</exception>
        /// <exception cref="ArgumentException">The module definition obtained for the specified catalog number
        /// does not have a valid upstream connection port for the current Bus type.</exception>
        /// <exception cref="ComponentNameCollisionException">name already exists on the current Bus instance.</exception>
        public IModule Create(ComponentName name, CatalogNumber catalogNumber, IPAddress ip,
            string? description = null, ICatalogService? catalogService = null) =>
            NewModule(name, catalogNumber, PortAddress.FromIP(ip), description, catalogService);

        /// <summary>
        /// Creates a new <see cref="IModule"/> with the provided name, catalog number, and additional module
        /// configuration, and assigns it to the <see cref="Bus"/> collection at address specified by the
        /// module configuration (Slot/IP).
        /// </summary>
        /// <param name="name">The name of the module to create.</param>
        /// <param name="catalogNumber">The catalog number of the module to create.</param>
        /// <param name="configure">The optional configuration of the module to set additional properties.</param>
        /// <param name="catalogService">The optional service provider that is responsible for retrieving
        /// the <see cref="ModuleDefinition"/> for the specified catalog number. By default this will use the
        /// <see cref="ModuleCatalog"/> built-in service provider.</param>
        /// <returns>The new <see cref="IModule"/> instance that was created and added to the bus.</returns>
        /// <exception cref="ModuleNotFoundException">catalogNumber could not be found by the catalog service provider.</exception>
        /// <exception cref="InvalidOperationException">The Bus instance is full and can not add new modules.</exception>
        /// <exception cref="ArgumentException">The module definition obtained for the specified catalog number
        /// does not have a valid upstream connection port for the current Bus type.</exception>
        /// <exception cref="ComponentNameCollisionException">name already exists on the current Bus instance.</exception>
        /// <remarks>
        /// The configuration delegate allows a more flexible way to set exactly which properties of the module you want.
        /// The address will be determined internally using the provided slot and IP and what type of bus the current object
        /// is. Meaning, if the current bus is Ethernet type, the IP will be the selected address. If Chassis, then the slot
        /// number. If the address is not available, then the next available will be chosen. Additionally, you may provide
        /// a custom <see cref="ICatalogService"/> in order to lookup the module definition for the specified catalog number. 
        /// </remarks>
        public IModule Create(ComponentName name, CatalogNumber catalogNumber,
            Action<ModuleConfiguration>? configure = null, ICatalogService? catalogService = null)
        {
            var configuration = new ModuleConfiguration();
            configure?.Invoke(configuration);

            var catalog = catalogService ?? new ModuleCatalog();

            var definition = catalog.Lookup(catalogNumber);

            if (configuration.Revision is not null)
                definition.ConfigureRevision(configuration.Revision);

            var upstreamAddress = DetermineUpstreamAddress(configuration.Slot, configuration.IP);
            var downstreamAddress = DetermineDownstreamAddress(configuration.Slot, configuration.IP);

            definition.ConfigurePorts(_port.Type, upstreamAddress, downstreamAddress, configuration.ChassisSize);

            return NewModule(name, definition, configuration.Keying, configuration.Inhibited, configuration.MajorFault,
                configuration.SafetyEnabled, configuration.Description);
        }

        /// <summary>
        /// Creates a new <see cref="IModule"/> with the provided name and module definition,
        /// and assigns it to the <see cref="Bus"/> collection at the address configured on the connecting port definition.
        /// </summary>
        /// <param name="name">The name of the module to create.</param>
        /// <param name="definition">The definition of the module to create.</param>
        /// <param name="keying">The optional <see cref="ElectronicKeying"/> value of the module. Will default to
        /// CompatibleModule.</param>
        /// <param name="inhibited">The optional inhibited property of the module. Will default to false.</param>
        /// <param name="majorFault">The optional major fault property of the module. Will default to false.</param>
        /// <param name="safetyEnabled">The optional safety enabled property of the module. Will default to false.</param>
        /// <param name="description">The optional description for the module. Will default to empty string.</param>
        /// <returns>The new <see cref="IModule"/> instance that was created and added to the bus.</returns>
        /// <exception cref="ArgumentNullException">definition is null.</exception>
        /// <exception cref="ComponentNameCollisionException">name already exists on the Bus instance.</exception>
        /// <exception cref="ArgumentException">
        /// definition does not have a configured upstream port that matches the Bus <see cref="Type"/> -or-
        /// definition does not a connecting port address that is available on the Bus.
        /// </exception>
        public IModule Create(ComponentName name, ModuleDefinition definition, ElectronicKeying? keying = null,
            bool inhibited = default, bool majorFault = default, bool safetyEnabled = default,
            string? description = null) =>
            NewModule(name, definition, keying, inhibited, majorFault, safetyEnabled, description);

        /// <summary>
        /// Removes the <see cref="IModule"/> at the specified address of the <see cref="Bus"/> object.
        /// </summary>
        /// <param name="address">The address (slot/IP) at which to remove the bus.</param>
        /// <returns>true if the module was found and removed from the Bus.
        /// If the module was not found or the provided address is that of the parent module, then false.
        /// </returns>
        /// <exception cref="ArgumentNullException">address is null.</exception>
        public bool RemoveAt(PortAddress address) => address != _port.Address && _modules.Remove(address);

        /// <summary>
        /// Removes the <see cref="IModule"/> with the specified name from the <see cref="Bus"/> if it exists.
        /// </summary>
        /// <param name="name">The name of the module to remove.</param>
        /// <returns>true if the module was found and removed from the Bus.
        /// If the module was not found or the provided name is that of the parent module, then false.
        /// </returns>
        public bool Remove(ComponentName name)
        {
            var address = _modules.Values.SingleOrDefault(m => m.Name == name)?.Ports.Upstream?.Address;

            if (address is null || address == _port.Address)
                return false;

            return _modules.Remove(address);
        }

        /// <inheritdoc />
        public IEnumerator<IModule> GetEnumerator() => _modules.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private IModule NewModule(ComponentName name, CatalogNumber catalogNumber, PortAddress address,
            string? description = null, ICatalogService? catalogService = null)
        {
            var catalog = catalogService ?? new ModuleCatalog();

            var definition = catalog.Lookup(catalogNumber);

            address = IsAvailable(address) ? address : NextAvailable();

            definition.ConfigurePorts(_port.Type, address);

            return NewModule(name, definition, description: description);
        }

        private IModule NewModule(ComponentName name, ModuleDefinition definition,
            ElectronicKeying? keying = null,
            bool inhibited = default, bool majorFault = default, bool safetyEnabled = default,
            string? description = null)
        {
            var module = new Module(name, definition, _parent.Name, _port.Id, keying, inhibited, majorFault,
                safetyEnabled, description);

            ValidateModule(module);

            var address = module.Ports.Upstream!.Address;

            _modules.Add(address, module);

            return module;
        }

        private PortAddress DetermineUpstreamAddress(byte slot, IPAddress? ip)
        {
            var slotAddress = PortAddress.FromSlot(slot);
            var ipAddress = ip is not null ? PortAddress.FromIP(ip) : PortAddress.None;
            
            if (IsBackplane && IsAvailable(slotAddress))
                return slotAddress;

            if (IsEthernet && ipAddress.IsIPv4 && IsAvailable(ipAddress))
                return ipAddress;

            return NextAvailable();
        }

        private PortAddress DetermineDownstreamAddress(byte slot, IPAddress? ipAddress)
        {
            if (IsBackplane && ipAddress is not null)
                return PortAddress.FromIP(ipAddress);

            return IsEthernet ? PortAddress.FromSlot(slot) : PortAddress.None;
        }

        private bool IsAvailable(PortAddress? address)
        {
            if (address is null || !address.HasAddress || _modules.ContainsKey(address))
                return false;

            if (IsBackplane && address.IsSlot && (!IsFixed || address.ToSlot() < Size))
                return true;
            
            return IsEthernet && (address.IsIPv4 || address.IsHostName);
        }

        private PortAddress NextAvailable()
        {
            if (IsFull)
                throw new InvalidOperationException("The current Bus is full and can not be assigned new modules.");

            if (IsEthernet)
            {
                var taken = _modules.Keys.Where(k => k.IsIPv4 && k.ToString().StartsWith(DefaultNetworkAddress))
                    .Select(k => (int)k.ToIPAddress().GetAddressBytes()[3]);

                var next = Enumerable.Range(1, byte.MaxValue)
                    .Except(taken)
                    .First()
                    .ToString();

                return new PortAddress($"{DefaultNetworkAddress}.{next}");
            }

            var maxValue = IsFixed ? Size : byte.MaxValue;

            return new PortAddress(Enumerable.Range(byte.MinValue, maxValue)
                .Except(_modules.Keys.Select(k => (int)k.ToSlot()))
                .First()
                .ToString());
        }

        private void ValidateModule(IModule module)
        {
            if (module is null)
                throw new ArgumentNullException(nameof(module));

            if (_modules.Any(p => p.Value.Name == module.Name))
                throw new ComponentNameCollisionException(module.Name, typeof(Module));

            if (module.ParentModule != _parent.Name)
                throw new ArgumentException(
                    $"The provide module parent '{module.ParentModule}' does not match the Bus parent '{_parent.Name}' for module '{module.Name}'.");

            if (module.ParentPortId != _port.Id)
                throw new ArgumentException(
                    $"The provide module port '{module.ParentPortId}' does not match the Bus port '{_port.Id}' for module '{module.Name}'.");

            if (module.Ports.Upstream is null)
                throw new ArgumentException(
                    $"No upstream port defined for module '{module.Name}'. Module must have an upstream port to be added to bus.");

            if (module.Ports.Upstream?.Type != Type)
                throw new ArgumentException(
                    $"The provide port type '{module.Ports.Upstream?.Type}' does not match bus type '{Type}' for module '{module.Name}'.");

            if (!IsAvailable(module.Ports.Upstream?.Address))
                throw new ArgumentException(
                    $"The provided port address '{module.Ports.Upstream?.Address}' for module '{module.Name}' is not a valid available address for the Bus.");
        }
    }
}