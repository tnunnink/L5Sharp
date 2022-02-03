using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using L5Sharp.Exceptions;

namespace L5Sharp.Core
{
    /// <summary>
    /// A Logix <see cref="Bus"/> is an object that represents collection of <see cref="IModule"/> components assigned to 
    /// a specific <see cref="Port"/>.
    /// </summary>
    public sealed class Bus : IEnumerable<IModule>
    {
        private readonly Port _port;
        private readonly Dictionary<string, IModule> _modules;

        private static readonly List<string> NetworkTypes = new()
        {
            "Ethernet",
            "ControlNet",
            "DeviceNet",
            "RIO"
        };

        internal Bus(Port port, int size)
        {
            _port = port ?? throw new ArgumentNullException(nameof(port));
            _modules = new Dictionary<string, IModule>();
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
        public int Size { get; }

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
        public bool IsChassis => !NetworkTypes.Contains(Type) && _modules.Keys.All(k => int.TryParse(k, out _));

        /// <summary>
        /// Gets a value indicating whether the <see cref="Bus"/> contains Modules with IP based addresses.
        /// </summary>
        public bool IsNetwork => NetworkTypes.Contains(Type) && _modules.Keys.All(k => IPAddress.TryParse(k, out _));

        /// <summary>
        /// Gets a Module with the specified address (slot or IP) from the <see cref="Bus"/>.
        /// </summary>
        /// <param name="address">The string address value that represents the slot or IP of the device to get.</param>
        public IModule this[string address] => _modules[address];

        /// <summary>
        /// Adds a new <see cref="IModule"/> instance to the current <see cref="Bus"/> using the provided name and catalog number to lookup and
        /// created the specified Module.
        /// </summary>
        /// <param name="name">The name of the Module to add to the Bus.</param>
        /// <param name="catalogNumber">The catalog number of the Module to create.</param>
        /// <param name="slot">The slot number to assign the Module to on the current chassis.</param>
        /// <param name="description">The optional description of the Module.</param>
        /// <exception cref="InvalidOperationException"></exception>
        public void AddModule(ComponentName name, CatalogNumber catalogNumber, int slot, string? description = null)
        {
            if (!IsChassis)
                throw new InvalidOperationException(
                    "The current Bus is not a chassis type Bus and can not add Modules by slot number.");

            var address = IsValidAvailableAddress(slot) ? slot.ToString() : NextAvailableSlot();

            AddModule(name, catalogNumber, address, description);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="catalogNumber"></param>
        /// <param name="ipAddress"></param>
        /// <param name="description"></param>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public void AddModule(ComponentName name, CatalogNumber catalogNumber, IPAddress ipAddress,
            string? description = null)
        {
            if (!IsNetwork)
                throw new InvalidOperationException(
                    "The current Bus is not a network type Bus and can not add Modules by IP address.");

            var address = IsValidAvailableAddress(ipAddress)
                ? ipAddress.ToString()
                : throw new ArgumentException(
                    $"The provided address '{ipAddress}' is not valid or available for the current Bus.");

            AddModule(name, catalogNumber, address, description);
        }

        private void AddModule(ComponentName name, CatalogNumber catalogNumber, string address, string? description)
        {
            ValidateName(name);

            var definition = GetDefinition(catalogNumber);
            var ports = GenerateConnectingPorts(definition.Ports.ToList(), name, address);

            var module = new Module(name, definition.CatalogNumber, definition.Vendor, definition.ProductType,
                definition.ProductCode, definition.Revisions.Max(), _port.ModuleName, _port.Id,
                ports, description: description);

            _modules[address] = module;
        }

        private ModuleDefinition GetDefinition(CatalogNumber catalogNumber)
        {
            if (catalogNumber is null)
                throw new ArgumentNullException(nameof(catalogNumber));

            var catalog = new LogixCatalog();

            var definition = catalog.Lookup(catalogNumber);

            if (definition is null)
                throw new ArgumentException(
                    $"The provided catalog number '{catalogNumber}' was not found in the catalog service file.");
            
            var port = definition.Ports.FirstOrDefault(p => p.Type == Type && p.Upstream);
            
            if (port is null)
                throw new ArgumentException(
                    $"No upstream port of type '{Type}' defined for module '{catalogNumber}'.");

            return definition;
        }

        private IEnumerable<Port> GenerateConnectingPorts(IReadOnlyCollection<Port> ports, string moduleName,
            string address)
        {
            var connectingPorts = new List<Port>();

            var port = ports.First(p => p.Type == Type && p.Upstream);

            var upstreamPort = new Port(port.Id, port.Type, true, address, moduleName: moduleName);

            var downstreamPorts = ports.Where(p => p.Id != port.Id)
                .Select(p => new Port(p.Id, p.Type, false, p.Address, moduleName: moduleName));

            connectingPorts.Add(upstreamPort);
            connectingPorts.AddRange(downstreamPorts);

            return connectingPorts;
        }

        private void ValidateName(ComponentName name)
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name));

            if (_modules.Any(m => m.Value.Name == name))
                throw new ComponentNameCollisionException(name, typeof(IModule));
        }

        private string NextAvailableSlot()
        {
            if (IsFull)
                throw new InvalidOperationException("The current Bus is full and can not assign additional modules.");

            var max = IsFixed ? Size : int.MaxValue;

            return Enumerable.Range(0, max).Except(_modules.Keys.Select(int.Parse)).First().ToString();
        }

        private bool IsValidAvailableAddress(int slot) => IsFixed
            ? slot >= 0 && slot < Size && !_modules.ContainsKey(slot.ToString())
            : slot >= 0 && !_modules.ContainsKey(slot.ToString());

        private bool IsValidAvailableAddress(IPAddress? ipAddress) =>
            ipAddress is not null && !_modules.ContainsKey(ipAddress.ToString());

        /// <inheritdoc />
        public IEnumerator<IModule> GetEnumerator() => _modules.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}