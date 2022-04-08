using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace L5Sharp.Core
{
    /// <summary>
    /// A collection of <see cref="Bus"/> objects that are maintained by a given <see cref="IModule"/> component.
    /// </summary>
    public class BusCollection : IEnumerable<Bus>
    {
        private readonly PortCollection _ports;
        private readonly Dictionary<Port, Bus> _buses;

        /// <summary>
        /// Creates a new <see cref="BusCollection"/> with the provided ports and parent module.
        /// </summary>
        /// <param name="ports">The set of port for which to create a bus for. A new bus is created for each downstream
        /// port in the collection.</param>
        /// <param name="module">The parent module of all child modules in the bus collection.</param>
        /// <exception cref="ArgumentNullException">ports is null.</exception>
        public BusCollection(PortCollection ports, IModule module)
        {
            _ports = ports ?? throw new ArgumentNullException(nameof(ports));
            _buses = new Dictionary<Port, Bus>();

            foreach (var port in ports.Downstream())
            {
                var bus = new Bus(port, module);
                _buses.Add(port, bus);
            }
        }

        /// <summary>
        /// Creates a new <see cref="BusCollection"/> with the provided parameters.
        /// </summary>
        /// <param name="ports">The set of port for which to create a bus for. A new bus is created for each downstream
        /// port in the collection.</param>
        /// <param name="module">The parent module of all child modules in the bus collection.</param>
        /// <param name="children">The collection of child module to initialize the bus collection with.</param>
        /// <exception cref="ArgumentNullException">ports is null.</exception>
        public BusCollection(PortCollection ports, IModule module, ICollection<IModule> children)
        {
            _ports = ports ?? throw new ArgumentNullException(nameof(ports));
            _buses = new Dictionary<Port, Bus>();

            foreach (var port in ports.Downstream())
            {
                var bus = new Bus(port, module);
                var modules = children.Where(c => c.ParentPortId == port.Id);
                bus.AddMany(modules);
                _buses.Add(port, bus);
            }
        }
        
        /// <summary>
        /// Returns the <see cref="Bus"/> with the specified port. 
        /// </summary>
        /// <param name="port">The <see cref="Port"/> that corresponds to the bus to get.</param>
        /// <returns>If the collection contains a port with the specified id, then the <see cref="Bus"/> for the
        /// specified port; otherwise, null.</returns>
        public Bus this[Port port] => _buses[port];

        /// <summary>
        /// Gets the first <see cref="Bus"/> available in the collection.
        /// </summary>
        /// <returns>If the collection contains any items, then the first <see cref="Bus"/> available on the collection;
        /// otherwise, null</returns>
        /// <remarks>
        /// </remarks>
        public Bus? Default() => _buses.FirstOrDefault().Value;
        
        /// <summary>
        /// Gets the single backplane or chassis <see cref="Bus"/> available in the collection, if one exists. 
        /// </summary>
        /// <returns>If the collection contains a single non Ethernet type bus port, then the <see cref="Bus"/>
        /// that corresponds to that port; otherwise, null.</returns>
        /// <remarks>
        /// Some modules may have multiple bus ports (i.e. Controllers)... 
        /// </remarks>
        public Bus? Backplane()
        {
            var port = _ports.Backplane();
            return port is not null && !port.Upstream ? _buses[port] : default;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Bus? Ethernet()
        {
            var port = _ports.Ethernet();
            return port is not null && !port.Upstream ? _buses[port] : default;
        }

        /// <summary>
        /// Gets all <see cref="IModule"/> components from all <see cref="Bus"/> objects in the collection. 
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> containing <see cref="IModule"/> from all bus objects in the
        /// collection.</returns>
        /// <remarks>
        /// ...
        /// </remarks>
        public IEnumerable<IModule> Modules() => _buses.SelectMany(b => b.Value);

        /// <inheritdoc />
        public IEnumerator<Bus> GetEnumerator() => _buses.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}