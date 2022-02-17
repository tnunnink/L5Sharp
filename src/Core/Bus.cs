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
        /// Creates a new <see cref="Module"/> with the provided name and catalog number, and assigns it to the specified
        /// address on the <see cref="Bus"/> collection.
        /// </summary>
        /// <param name="name">The name of the module to create.</param>
        /// <param name="catalogNumber">The catalog number of the module to create.</param>
        /// <param name="address">The address at which to place the module on the Bus (Slot/IP/Node).</param>
        /// <param name="description">The optional description for the module.</param>
        /// <param name="catalogService">The optional service provider that is responsible for retrieving
        /// the <see cref="ModuleDefinition"/> for the specified catalog number. By default this will use the
        /// <see cref="LogixCatalog"/> built in service provider.</param>
        /// <returns>The <see cref="Module"/> instance that was created using the provided parameters.</returns>
        public Module New(ComponentName name, CatalogNumber catalogNumber, string address, string? description = null,
            ICatalogService? catalogService = null)
        {
            var catalog = catalogService ?? new LogixCatalog();
            var definition = catalog.Lookup(catalogNumber);

            address = IsAvailable(address) ? address : NextAvailable();
            ConfigurePort(definition, address);

            var module = new Module(name, definition, _port.Module.Name, _port.Id, description);

            ValidateModule(module);

            _modules.Add(address, module);
            return module;
        }
        
        private void ConfigurePort(ModuleDefinition definition, string address)
        {
            var connecting = definition.Ports.FirstOrDefault(p => p.Type == _port.Type && !p.DownstreamOnly);

            if (connecting is null)
                throw new ArgumentException(
                    $"The module definition for '{definition.CatalogNumber}' does not contain a valid connecting " +
                    $"port of type '{_port.Type}' for the current Bus.");

            connecting.Upstream = true;
            connecting.Address = address;
        }

        private bool IsAvailable(string? address)
        {
            if (string.IsNullOrEmpty(address) || _modules.ContainsKey(address))
                return false;

            if (IsChassis && byte.TryParse(address, out var slot) && (!IsFixed || slot < Size))
                return true;

            return IsEthernet && address.IsIPv4();
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
                    $"The provide module upstream port of type '{module.Ports.Connecting()?.Type}' must match Bus type '{Type}'.");

            if (!IsAvailable(module.Ports.Connecting()?.Address))
                throw new ArgumentException(
                    $"The provided module upstream port address '{module.Ports.Connecting()?.Address}' is not a valid available address for the Bus.");
        }

        /// <inheritdoc />
        public IEnumerator<Module> GetEnumerator() => _modules.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}