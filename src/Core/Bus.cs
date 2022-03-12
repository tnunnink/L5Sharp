using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using L5Sharp.Exceptions;
using L5Sharp.Extensions;

namespace L5Sharp.Core
{
    /// <summary>
    /// Represents a collection of <see cref="IModule"/> components assigned to  a specific <see cref="Port"/>.
    /// </summary>
    public sealed class Bus : IEnumerable<IModule>
    {
        private const string DefaultNetworkAddress = "192.168.1";
        private readonly Port _port;
        private readonly Dictionary<string, IModule> _modules;

        internal Bus(Port port, byte size)
        {
            _port = port ?? throw new ArgumentNullException(nameof(port));
            _modules = new Dictionary<string, IModule> { { _port.Address, _port.Module } };
            Size = size;
        }

        /// <summary>
        /// Gets the type indicating which port types can be added to the <see cref="Bus"/>.
        /// </summary>
        public string Type => _port.Type;

        /// <summary>
        /// Gets the number of Modules in the <see cref="Bus"/>.
        /// </summary>
        public int Count => _modules.Count;

        /// <summary>
        /// Gets the value of the current <see cref="Bus"/> size.
        /// </summary>
        public byte Size { get; }

        /// <summary>
        /// Gets a value indicating whether the <see cref="Bus"/> is empty or has not Modules.
        /// </summary>
        public bool IsEmpty => _modules.Count == 0;

        /// <summary>
        /// Gets a value indicating whether the <see cref="Bus"/> is full and can not add new Modules.
        /// </summary>
        public bool IsFull => IsFixed && _modules.Count == Size;

        /// <summary>
        /// Gets a value indicating whether the <see cref="Bus"/> is of fixed size or unbounded.
        /// </summary>
        public bool IsFixed => Size > 0;

        /// <summary>
        /// Gets a value indicating whether the <see cref="Bus"/> contains Modules with slot based addresses.
        /// </summary>
        public bool IsChassis => Type != "Ethernet" && _modules.Keys.All(k => k.IsByte());

        /// <summary>
        /// Gets a value indicating whether the <see cref="Bus"/> contains Modules with IP based addresses.
        /// </summary>
        public bool IsEthernet => Type == "Ethernet" && _modules.Keys.All(k => k.IsIPv4());

        /// <summary>
        /// Gets a <see cref="IModule"/> at the specified address (slot or IP).
        /// </summary>
        /// <param name="address">The string address value that represents the slot or IP of the device to get.</param>
        public IModule? this[string address] => _modules.TryGetValue(address, out var module) ? module : null;

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

            var address = module.Ports.Connecting()?.Address!;

            _modules[address] = module;
        }

        /// <summary>
        /// Gets the address of the <see cref="IModule"/> with the specified name.
        /// </summary>
        /// <param name="name">The name of the module for which to get the address of.</param>
        /// <returns>The string value representing the address (slot/IP) at which the module belongs to the current Bus.
        /// Null if the specified module does not exist.
        /// </returns>
        public string AddressOf(ComponentName name) => 
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
        public bool ContainsAddress(string address) => _modules.ContainsKey(address);

        /// <summary>
        /// Indicates whether a <see cref="IModule"/> with the specified name exists on the current <see cref="Bus"/>.
        /// </summary>
        /// <param name="name">The name of the module to search.</param>
        /// <returns>true if the bus contains a module with the specified name; otherwise, false.</returns>
        public bool ContainsName(ComponentName name) => _modules.Values.Any(m => m.Name == name);

        /// <summary>
        /// Creates a new <see cref="IModule"/> with the provided name and catalog number, and assigns it to the address
        /// determined using the provided IP address and/or slot number.
        /// </summary>
        /// <param name="name">The name of the module to create.</param>
        /// <param name="catalogNumber">The catalog number of the module to create.</param>
        /// <param name="slot">The slot number of the module. If not provided will default to 0.</param>
        /// <param name="ipAddress">The IP Address of the module.</param>
        /// <param name="description">The optional description for the module.</param>
        /// <param name="catalogService">The optional service provider that is responsible for retrieving
        /// the <see cref="ModuleDefinition"/> for the specified catalog number. By default this will use the
        /// <see cref="ModuleCatalog"/> built in service provider.</param>
        /// <returns>The <see cref="IModule"/> instance that was created using the provided parameters.</returns>
        /// <exception cref="ModuleNotFoundException">catalogNumber could not be found by the catalog service provider.</exception>
        /// <exception cref="InvalidOperationException">The Bus instance is full and can not add new modules.</exception>
        /// <exception cref="ArgumentException">The module definition obtained for the specified catalog number
        /// does not have a valid upstream connection port for the current Bus type.</exception>
        /// <exception cref="ComponentNameCollisionException">name already exists on the current Bus instance.</exception>
        /// <seealso cref="New(L5Sharp.Core.ComponentName,L5Sharp.Core.ModuleDefinition,string?)"/>
        public IModule New(ComponentName name, CatalogNumber catalogNumber,
            byte slot = default, IPAddress? ipAddress = null,
            string? description = null, ICatalogService? catalogService = null) =>
            NewModule(name, catalogNumber, slot, ipAddress, description, catalogService);

        /// <summary>
        /// Creates a new <see cref="IModule"/> with the provided name and module definition,
        /// and assigns it to the <see cref="Bus"/> collection at the address configured on the connecting port definition.
        /// </summary>
        /// <param name="name">The name of the module to create.</param>
        /// <param name="definition">The definition of the module to create.</param>
        /// <param name="description">The optional description for the module.</param>
        /// <returns>The <see cref="IModule"/> instance that was created using the provided parameters.</returns>
        /// <exception cref="ArgumentNullException">definition is null.</exception>
        /// <exception cref="ComponentNameCollisionException">name already exists on the Bus instance.</exception>
        /// <exception cref="ArgumentException">
        /// definition does not have a configured upstream port that matches the Bus <see cref="Type"/> -or-
        /// definition does not a connecting port address that is available on the Bus.
        /// </exception>
        public IModule New(ComponentName name, ModuleDefinition definition, string? description = null)
        {
            if (definition is null)
                throw new ArgumentNullException(nameof(definition));

            return NewModule(name, definition, description);
        }

        /// <summary>
        /// Removes the <see cref="IModule"/> at the specified address of the <see cref="Bus"/> object.
        /// </summary>
        /// <param name="address">The address (slot/IP) at which to remove the bus.</param>
        /// <returns>true if the module was found and removed from the Bus.
        /// If the module was not found or the provided address is that of the parent module, then false.
        /// </returns>
        /// <exception cref="ArgumentNullException">address is null.</exception>
        public bool RemoveAt(string address) => address != _port.Address && _modules.Remove(address);

        /// <summary>
        /// Removes the <see cref="IModule"/> with the specified name from the <see cref="Bus"/> if it exists.
        /// </summary>
        /// <param name="name">The name of the module to remove.</param>
        /// <returns>true if the module was found and removed from the Bus.
        /// If the module was not found or the provided name is that of the parent module, then false.
        /// </returns>
        public bool Remove(ComponentName name)
        {
            if (name == _port.Module.Name)
                return false;
            
            var module = _modules.Values.SingleOrDefault(m => m.Name == name);

            var address = module?.Ports.Connecting()?.Address;
            
            return address is not null && _modules.Remove(address);
        }

        /// <inheritdoc />
        public IEnumerator<IModule> GetEnumerator() => _modules.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private void ConfigureDownstreamPort(ModuleDefinition definition, string address)
        {
            var local = definition.Ports.FirstOrDefault(p => p.Type != _port.Type);

            if (local is null) return;

            local.Upstream = false;
            local.Address = address;
        }

        private void ConfigureUpstreamPort(ModuleDefinition definition, string address)
        {
            var connecting = definition.Ports.FirstOrDefault(p => p.Type == _port.Type && !p.DownstreamOnly);

            if (connecting is null)
                throw new ArgumentException(
                    $"The module definition for '{definition.CatalogNumber}' does not contain a valid connecting " +
                    $"port of type '{_port.Type}' for the current Bus.");

            connecting.Upstream = true;
            connecting.Address = address;
        }

        private string DetermineDownstreamAddress(byte slot, IPAddress? ipAddress)
        {
            if (IsChassis && ipAddress is not null)
                return ipAddress.ToString();

            return IsEthernet ? slot.ToString() : string.Empty;
        }

        private string DetermineUpstreamAddress(byte slot, IPAddress? ipAddress)
        {
            if (IsChassis && IsAvailable(slot.ToString()))
                return slot.ToString();

            if (IsEthernet && ipAddress is not null && IsAvailable(ipAddress.ToString()))
                return ipAddress.ToString();

            return NextAvailable();
        }

        private bool IsAvailable(string? address)
        {
            if (string.IsNullOrEmpty(address) || _modules.ContainsKey(address))
                return false;

            if (IsChassis && byte.TryParse(address, out var slot) && (!IsFixed || slot < Size))
                return true;

            return IsEthernet && address.IsIPv4();
        }

        private Module NewModule(ComponentName name, CatalogNumber catalogNumber,
            byte slot = default, IPAddress? ipAddress = null,
            string? description = null, ICatalogService? catalogService = null)
        {
            var catalog = catalogService ?? new ModuleCatalog();
            var definition = catalog.Lookup(catalogNumber);

            var upstreamAddress = DetermineUpstreamAddress(slot, ipAddress);
            ConfigureUpstreamPort(definition, upstreamAddress);

            var downstreamAddress = DetermineDownstreamAddress(slot, ipAddress);
            ConfigureDownstreamPort(definition, downstreamAddress);

            return NewModule(name, definition, description);
        }

        private Module NewModule(ComponentName name, ModuleDefinition definition, string? description = null)
        {
            var module = new Module(name, definition, _port.Module.Name, _port.Id, description);

            ValidateModule(module);

            var address = module.Ports.Connecting()?.Address!;

            _modules.Add(address, module);

            return module;
        }

        private string NextAvailable()
        {
            if (IsFull)
                throw new InvalidOperationException("The current Bus is full and can not assign additional modules.");
            
            if (IsEthernet)
            {
                var taken = _modules.Keys.Where(k => k.StartsWith(DefaultNetworkAddress))
                    .Select(k => (int)IPAddress.Parse(k).GetAddressBytes()[3]);
                
                var next = Enumerable.Range(byte.MinValue, byte.MaxValue)
                    .Except(taken)
                    .First()
                    .ToString();
                
                return IPAddress.Parse($"{DefaultNetworkAddress}.{next}").ToString();
            }
            
            var range = IsFixed ? Size : byte.MaxValue;

            return Enumerable.Range(byte.MinValue, range)
                .Except(_modules.Keys.Select(int.Parse))
                .First()
                .ToString();
        }

        private void ValidateModule(IModule module)
        {
            if (module is null)
                throw new ArgumentNullException(nameof(module));

            if (_modules.Any(p => p.Value.Name == module.Name))
                throw new ComponentNameCollisionException(module.Name, typeof(Module));

            if (module.ParentModule != _port.Module.Name)
                throw new ArgumentException(
                    $"The provide module parent '{module.ParentModule}' does not match the Bus parent '{_port.Module.Name}'.");

            if (module.ParentPortId != _port.Id)
                throw new ArgumentException(
                    $"The provide module port '{module.ParentPortId}' does not match the Bus port '{_port.Id}'.");

            if (module.Ports.Connecting() is null)
                throw new ArgumentException(
                    "No upstream port defined for the provided module. Module must have an upstream port to be added to bus.");

            if (module.Ports.Connecting()?.Type != Type)
                throw new ArgumentException(
                    $"The provide module's upstream port of type '{module.Ports.Connecting()?.Type}'does not match bus type '{Type}'.");

            if (!IsAvailable(module.Ports.Connecting()?.Address))
                throw new ArgumentException(
                    $"The provided module upstream port address '{module.Ports.Connecting()?.Address}' is not a valid available address for the Bus.");
        }
    }
}