using System.Collections;
using System.Collections.Generic;

namespace L5Sharp.Core
{
    /// <summary>
    /// A collection of <see cref="Port"/> objects that are maintained by a given <see cref="IModule"/>.
    /// </summary>
    public class PortCollection : IEnumerable<Port>
    {
        private readonly List<Port> _ports;

        internal PortCollection(IEnumerable<Port> ports)
        {
            _ports = new List<Port>(ports);
        }

        /// <summary>
        /// Gets the port with the specified id or index.
        /// </summary>
        /// <param name="id">The in</param>
        public Port? this[int id] => _ports[id];

        /// <inheritdoc />
        public IEnumerator<Port> GetEnumerator()
        {
            return _ports.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}