using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace L5Sharp.Core
{
    /// <summary>
    /// A collection of <see cref="Port"/> objects that are maintained by a given <see cref="Module"/>.
    /// </summary>
    public class PortCollection : IEnumerable<Port>
    {
        private readonly Dictionary<int, Port> _ports;

        internal PortCollection(Module module, IEnumerable<PortDefinition> portDefinitions)
        {
            _ports = portDefinitions.Select(d => new Port(module, d)).ToDictionary(p => p.Id);
        }

        /// <summary>
        /// Gets a Port with the specified id.
        /// </summary>
        /// <param name="id">The id of the port to retrieve.</param>
        /// <exception cref="KeyNotFoundException">id does not exist in the current collection.</exception>
        public Port this[int id] => _ports[id];

        /// <summary>
        /// Gets the first port, or port with id equal to '1', of the collection.
        /// </summary>
        /// <returns>A <see cref="Port"/> instance with id equal to '1'.</returns>
        public Port Primary() => _ports[1];

        /// <summary>
        /// Gets the second port, or port with id equal to '2', of the collection.
        /// </summary>
        /// <returns>A <see cref="Port"/> instance with id equal to '2' if one exists; otherwise, null.</returns>
        public Port? Secondary() => _ports.ContainsKey(2) ? _ports[2] : null;

        /// <summary>
        /// Gets the upstream connecting <see cref="Port"/> in the collection.
        /// </summary>
        /// <returns>A <see cref="Port"/> instance that is designated as upstream if one exists; otherwise, null.</returns>
        /// <remarks>
        /// Each module should contain zero or one upstream ports. The upstream port represents the port designated
        /// as the connection to a parent module upstream of the current module. The connecting port is used to
        /// define the hierarchical structure of the module tree.
        /// </remarks>
        public Port? Connecting() => _ports.Values.SingleOrDefault(p => p.Upstream);

        /// <summary>
        /// Gets the first local downstream <see cref="Port"/> in the collection.
        /// </summary>
        /// <returns>A <see cref="Port"/> instance that is designated as downstream if one exists; otherwise, null.</returns>
        /// <remarks>
        /// Each module may contain zero or more downstream ports. Each downstream port of a module represents a port
        /// that has a bus on which other modules can be connected to. Use this Local port to create modules that
        /// are children of the current port or module. Note that many modules may not have a local downstream port, but
        /// very few will have more than one. 
        /// </remarks>
        public Port? Local() => _ports.Values.FirstOrDefault(p => !p.Upstream);

        /// <summary>
        /// Gets the IP address of an Ethernet type port if it is available.
        /// </summary>
        /// <returns></returns>
        public byte? GetSlotAddress()
        {
            var port = _ports.Values.FirstOrDefault(p => p.Type != "Ethernet");
            return port is not null && byte.TryParse(port.Address, out var slot) ? slot : null;
        }

        /// <summary>
        /// Gets the IP address of an Ethernet type port if it is available.
        /// </summary>
        /// <returns>The <see cref="IPAddress"/> of the Ethernet port if one is defined; otherwise, null.</returns>
        public IPAddress? GetIPAddress()
        {
            var port = _ports.Values.FirstOrDefault(p => p.Type == "Ethernet");
            return port is not null && IPAddress.TryParse(port.Address, out var ipAddress) ? ipAddress : null;
        }

        /// <inheritdoc />
        public IEnumerator<Port> GetEnumerator() => _ports.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}