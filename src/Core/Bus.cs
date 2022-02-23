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
    /// Represents a collection of <see cref="Module"/> components assigned to  a specific <see cref="Port"/>.
    /// </summary>
    public sealed class Bus : IEnumerable<Module>
    {
        private readonly Port _port;
        private readonly Dictionary<string, Module> _modules;

        internal Bus(Port port, byte size)
        {
            _port = port ?? throw new ArgumentNullException(nameof(port));
            _modules = new Dictionary<string, Module> { { _port.Address, _port.Module } };
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
        /// Gets a <see cref="Module"/> at the specified address (slot or IP).
        /// </summary>
        /// <param name="address">The string address value that represents the slot or IP of the device to get.</param>
        public Module this[string address] => _modules[address];

        /// <summary>
        /// Adds the provided <see cref="Module"/> to the Bus collection at the address specified by the connecting port. 
        /// </summary>
        /// <param name="module">The <see cref="Module"/> instance to add to the Bus.</param>
        /// <exception cref="ArgumentNullException"><c>module</c> is null.</exception>
        /// <exception cref="ComponentNameCollisionException"><c>module</c> name already exists on the bus.</exception>
        /// <exception cref="ArgumentException">
        /// <c>module</c> parent parameters do not match the bus' parent port -or-
        /// there is no non-null connecting port with a type that matches the bus type -or-
        /// the connecting port address is not available or valid for the current bus.
        /// .</exception>
        public void Add(Module module)
        {
            ValidateModule(module);

            var address = module.Ports.Connecting()?.Address!;

            _modules[address] = module;
        }

        /// <summary>
        /// Creates a new <see cref="Module"/> with the provided name and catalog number, and assigns it to the address
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
        /// <returns>The <see cref="Module"/> instance that was created using the provided parameters.</returns>
        /// <exception cref="ModuleNotFoundException"><c>catalogNumber</c> could not be found by the catalog service provider.</exception>
        /// <exception cref="InvalidOperationException">The Bus instance is full and can not add new modules.</exception>
        /// <exception cref="ArgumentException">The module definition obtained for the specified catalog number
        /// does not have a valid upstream connection port for the current Bus type.</exception>
        /// <exception cref="ComponentNameCollisionException"><c>name</c> already exists on the current Bus instance.</exception>
        /// <seealso cref="New(L5Sharp.Core.ComponentName,L5Sharp.Core.CatalogNumber,System.Net.IPAddress,byte,string?,L5Sharp.ICatalogService?)"/>
        /// <seealso cref="New(L5Sharp.Core.ComponentName,L5Sharp.Core.ModuleDefinition,string?)"/>
        public Module New(ComponentName name, CatalogNumber catalogNumber,
            byte slot = default, IPAddress? ipAddress = null,
            string? description = null, ICatalogService? catalogService = null) =>
            NewModule(name, catalogNumber, slot, ipAddress, description, catalogService);

        /// <summary>
        /// Creates a new <see cref="Module"/> with the provided name and catalog number, and assigns it to the address
        /// determined using the provided IP address and/or slot number.
        /// </summary>
        /// <param name="name">The name of the module to create.</param>
        /// <param name="catalogNumber">The catalog number of the module to create.</param>
        /// <param name="ipAddress">The IP Address of the module.</param>
        /// <param name="slot">The slot number of the module. If not provided will default to 0.</param>
        /// <param name="description">The optional description for the module.</param>
        /// <param name="catalogService">The optional service provider that is responsible for retrieving
        /// the <see cref="ModuleDefinition"/> for the specified catalog number. By default this will use the
        /// <see cref="ModuleCatalog"/> built in service provider.</param>
        /// <returns>The <see cref="Module"/> instance that was created using the provided parameters.</returns>
        /// <exception cref="ModuleNotFoundException"><c>catalogNumber</c> could not be found by the catalog service provider.</exception>
        /// <exception cref="InvalidOperationException">The Bus instance is full and can not add new modules.</exception>
        /// <exception cref="ArgumentException">The module definition obtained for the specified catalog number
        /// does not have a valid upstream connection port for the current Bus type.</exception>
        /// <exception cref="ComponentNameCollisionException"><c>name</c> already exists on the current Bus instance.</exception>
        /// <seealso cref="New(L5Sharp.Core.ComponentName,L5Sharp.Core.CatalogNumber,byte,System.Net.IPAddress?,string?,L5Sharp.ICatalogService?)"/>
        /// <seealso cref="New(L5Sharp.Core.ComponentName,L5Sharp.Core.ModuleDefinition,string?)"/>
        public Module New(ComponentName name, CatalogNumber catalogNumber, IPAddress ipAddress, byte slot = default,
            string? description = null, ICatalogService? catalogService = null) =>
            NewModule(name, catalogNumber, slot, ipAddress, description, catalogService);

        /// <summary>
        /// Creates a new <see cref="Module"/> with the provided name and module definition,
        /// and assigns it to the <see cref="Bus"/> collection at the address configured on the connecting port definition.
        /// </summary>
        /// <param name="name">The name of the module to create.</param>
        /// <param name="definition">The definition of the module to create.</param>
        /// <param name="description">The optional description for the module.</param>
        /// <returns>The <see cref="Module"/> instance that was created using the provided parameters.</returns>
        /// <exception cref="ArgumentNullException"><c>definition</c> is null.</exception>
        /// <exception cref="ArgumentException"><c>definition</c> does not have a configured upstream port that
        /// matches the bus <see cref="Type"/>.</exception>
        public Module New(ComponentName name, ModuleDefinition definition, string? description = null)
        {
            if (definition is null)
                throw new ArgumentNullException(nameof(definition));

            var connecting = definition.Ports.FirstOrDefault(p => p.Upstream && p.Type == Type && !p.DownstreamOnly);

            if (connecting is null)
                throw new ArgumentException(
                    $"The provided definition does not have an upstream connection port of type '{Type}'.");

            return NewModule(name, definition, connecting.Address, description);
        }


        /// <inheritdoc />
        public IEnumerator<Module> GetEnumerator() => _modules.Values.GetEnumerator();

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

            var downstreamAddress = DetermineUpstreamAddress(slot, ipAddress);
            ConfigureDownstreamPort(definition, downstreamAddress);

            return NewModule(name, definition, upstreamAddress, description);
        }

        private Module NewModule(ComponentName name, ModuleDefinition definition, string address,
            string? description = null)
        {
            var module = new Module(name, definition, _port.Module.Name, _port.Id, description);

            ValidateModule(module);

            _modules.Add(address, module);

            return module;
        }

        private string NextAvailable()
        {
            if (IsFull)
                throw new InvalidOperationException("The current Bus is full and can not assign additional modules.");

            if (IsChassis)
            {
                var range = IsFixed ? Size : byte.MaxValue;

                return Enumerable.Range(byte.MinValue, range)
                    .Except(_modules.Keys.Select(int.Parse))
                    .First()
                    .ToString();
            }

            if (IsEthernet)
            {
                //todo need to create way to generate next available IP on default 192.168.1.1 network
                return IPAddress.Any.ToString();
            }

            throw new InvalidOperationException(
                "The current Bus is neither an Ethernet nor Chassis Bus and can not determine a next available address");
        }

        private void ValidateModule(Module module)
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