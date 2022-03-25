using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace L5Sharp.Core
{
    /// <inheritdoc />
    public class ModuleCollection : IModuleCollection
    {
        private readonly Bus? _bus;

        internal ModuleCollection(Bus? bus)
        {
            _bus = bus;
        }

        /// <inheritdoc />
        public IModule? AtAddress(string address) => _bus?[address];

        /// <inheritdoc />
        public IModule? Named(ComponentName name) => _bus?.SingleOrDefault(m => m.Name == name);

        /// <inheritdoc />
        public IModule? New(ComponentName name, CatalogNumber catalogNumber, byte slot, string? description = null,
            ICatalogService? catalogService = null) =>
            _bus?.Create(name, catalogNumber, slot, null, description, catalogService);

        /// <inheritdoc />
        public IModule? New(ComponentName name, CatalogNumber catalogNumber, IPAddress? ipAddress,
            string? description = null, ICatalogService? catalogService = null) =>
            _bus?.Create(name, catalogNumber, 0, ipAddress, description, catalogService);

        /// <inheritdoc />
        public IModule? New(ComponentName name, CatalogNumber catalogNumber, byte slot = default,
            IPAddress? ipAddress = null, string? description = null, ICatalogService? catalogService = null)
            => _bus?.Create(name, catalogNumber, slot, ipAddress, description, catalogService);

        /// <inheritdoc />
        public IModule? New(ComponentName name, ModuleDefinition definition, string? description = null) => 
            _bus?.Create(name, definition, description);

        /// <inheritdoc />
        public bool Remove(ComponentName name) => _bus is not null && _bus.Remove(name);

        /// <inheritdoc />
        public IEnumerator<IModule> GetEnumerator() =>
            _bus?.GetEnumerator() ?? Enumerable.Empty<IModule>().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}