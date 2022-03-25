using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace L5Sharp.Core
{
    /// <summary>
    /// A collection of <see cref="Port"/> objects that are maintained by a given <see cref="IModule"/> component.
    /// </summary>
    /// <remarks>
    /// This collection is 
    /// </remarks>
    public class PortCollection : IEnumerable<Port>
    {
        private readonly Dictionary<int, Port> _ports;

        /// <summary>
        /// Creates a new <see cref="PortCollection"/> initialized with the provided set of ports.
        /// </summary>
        /// <param name="ports">An <see cref="IEnumerable{T}"/> containing the ports to initialize the collection.</param>
        /// <exception cref="ArgumentException">ports has multiple upstream ports.</exception>
        public PortCollection(IList<Port> ports)
        {
            if (ports is null)
                throw new ArgumentNullException(nameof(ports));

            if (ports.Count == 0)
                throw new ArgumentException("The provided port collection must have at least one port instance.");
            
            if (ports.Count(p => p.Upstream) > 1)
                throw new ArgumentException("The provided port collection must have zero or one upstream ports.");
            
            _ports = ports.ToDictionary(p => p.Id);
        }

        /// <summary>
        /// Returns a Port with the specified id.
        /// </summary>
        /// <param name="id">The id of the port to retrieve.</param>
        /// <exception cref="KeyNotFoundException">id does not exist in the current collection.</exception>
        public Port this[int id] => _ports[id];

        /// <summary>
        /// Returns the only upstream <see cref="Port"/> in the collection, if one exists.
        /// </summary>
        /// <returns>A <see cref="Port"/> with the upstream value set to <c>true</c>, if one exists; otherwise, null.</returns>
        /// <remarks>
        /// Each module should contain zero or one upstream ports. The upstream port represents the port designated
        /// as the connection to a parent module upstream of the current module. The upstream port is used to
        /// define the hierarchical structure of the module tree.
        /// </remarks>
        public Port? Upstream() => _ports.Values.SingleOrDefault(p => p.Upstream);

        /// <summary>
        /// Returns the all downstream <see cref="Port"/> in the collection.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> containing all ports with the upstream value set to <c>false</c>.
        /// If none exist, an empty collection.</returns>
        /// <remarks>
        /// Each module may contain zero or more downstream ports. Each downstream port of a module represents a port
        /// that has a corresponding <see cref="Bus"/>, on which other modules can be connected to.
        /// Note that many modules may not have a local downstream port, but very few will have more than one. 
        /// </remarks>
        public IEnumerable<Port> Downstream() => _ports.Values.Where(p => !p.Upstream);

        /// <summary>
        /// Returns the "Backplane" type <see cref="Port"/> in the collection, if one exists.
        /// </summary>
        /// <remarks>
        /// The <i>Backplane</i> port represents a port on which connections are made in a rack or chassis.
        /// Not all modules will have an backplane type port.
        /// From analyzing the different port types, there appear to be two conceivable delineations. Ports that are
        /// network based, having IPs for the address property, and 'Ethernet' for the type, and ports that are perhaps
        /// rack or chassis based, having <c>byte</c> number for the address property (which applies to many different types).
        /// <c>Backplane</c> refers to the latter, since it will typically refer to a port in which devices are
        /// connected in a rack. Therefore, this port is simply the first non-Ethernet type port.
        /// </remarks>
        public Port? Backplane() => _ports.Values.FirstOrDefault(p => p.Type != "Ethernet");

        /// <summary>
        /// Returns the Ethernet type <see cref="Port"/> in the collection, if one exists.
        /// </summary>
        /// <remarks>
        /// The <i>Ethernet</i> port represents a port on which connections are made in a network using Ethernet/IP.
        /// Not all modules will have an Ethernet type port, but none should have more than one.
        /// From analyzing the different port types, there appear to be two conceivable delineations. Ports that are
        /// network based, having IPs for the address property, and 'Ethernet' for the type, and ports that are perhaps
        /// rack or chassis based, having <c>byte</c> number for the address property (which applies to many different types).
        /// <see cref="Ethernet"/> refers to the former. Therefore, this port is simply the first Ethernet type port.
        /// </remarks>
        public Port? Ethernet() => _ports.Values.FirstOrDefault(p => p.Type == "Ethernet");

        /// <inheritdoc />
        public IEnumerator<Port> GetEnumerator() => _ports.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}