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
        /// Gets the Ethernet type port if one exists for the current <see cref="PortCollection"/>.
        /// </summary>
        /// <remarks>
        /// The Ethernet port represents a port for which connections are made using Ethernet/IP network.
        /// Not all modules will have an Ethernet type port, hence why the port is nullable reference. 
        /// </remarks>
        public Port? Ethernet => _ports.Values.FirstOrDefault(p => p.Type == "Ethernet");
        
        /// <summary>
        /// Gets the Ethernet type port if one exists for the current <see cref="PortCollection"/>.
        /// </summary>
        /// <remarks>
        /// The Chassis port represents a port for which connections are made on a rack or backplane.
        /// Not all modules will have an Chassis type port, hence why the port is nullable reference.
        /// This port is effectively the opposite of <see cref="Ethernet"/>, meaning it will return the first port where
        /// the port type is not 'Ethernet'. We are calling this 'Chassis' port since it will typically refer to the backplane
        /// or rack. But note that this is not always the case. The first non ethernet type port may be something like ControlNet.
        /// In all cases the ports should refer to byte based addressing, as opposed to IP based addressing.
        /// </remarks>
        public Port? Chassis => _ports.Values.FirstOrDefault(p => p.Type != "Ethernet");

        /// <inheritdoc />
        public IEnumerator<Port> GetEnumerator() => _ports.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}