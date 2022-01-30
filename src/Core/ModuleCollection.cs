using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace L5Sharp.Core
{
    /// <inheritdoc />
    public class ModuleCollection : IModuleCollection
    {
        private readonly IEnumerable<Port> _ports;

        internal ModuleCollection(IEnumerable<Port> ports)
        {
            _ports = ports;
        }

        /// <inheritdoc />
        public IModule? this[int slot] => DefaultBus()[slot];

        /// <inheritdoc />
        public void Add(ComponentName name, ModuleDefinition definition, string? description = null, Port? port = null)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public bool Remove(ComponentName name)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public bool Remove(int slot)
        {
            throw new System.NotImplementedException();
        }

        private Bus DefaultBus() => _ports.First().Bus!;

        private Bus GetBus(int id) => _ports.SingleOrDefault(p => p.Id == id)?.Bus!;

        /// <inheritdoc />
        public IEnumerator<IModule> GetEnumerator()
        {
            return _ports.SelectMany(p => p.Bus).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}