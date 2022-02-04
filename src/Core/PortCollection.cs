using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace L5Sharp.Core
{
    /// <summary>
    /// A collection of <see cref="Port"/> objects that are maintained by a given <see cref="IModule"/>.
    /// </summary>
    public class PortCollection : IEnumerable<Port>
    {
        private readonly List<Port> _ports;

        internal PortCollection(IModule module, IEnumerable<PortDefinition> portDefinitions)
        {
            _ports = new List<Port>(portDefinitions.Select(d => new Port(module, d)));
        }

        /// <summary>
        /// Gets the first port having a parsable slot for the address.
        /// </summary>
        public Port? ChassisPort => _ports.FirstOrDefault(p => int.TryParse(p.Address, out _));
        
        /// <summary>
        /// Gets the first port having a parsable IP for the address.
        /// </summary>
        public Port? NetworkPort => _ports.FirstOrDefault(p => IPAddress.TryParse(p.Address, out _));

        /// <summary>
        /// Gets the upstream connecting port of the <see cref="PortCollection"/>.
        /// </summary>
        public Port Upstream => _ports.First(p => p.Upstream);

        /// <summary>
        /// Gets all downstream ports of the <see cref="PortCollection"/>.
        /// </summary>
        public IEnumerable<Port> Downstream => _ports.Where(p => !p.Upstream);

        /// <summary>
        /// Gets the <see cref="Port"/> with the specified id.  
        /// </summary>
        /// <param name="id"></param>
        public Port? this[int id] => _ports.SingleOrDefault(p => p.Id == id);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Port? Default() => _ports.FirstOrDefault(p => !p.Upstream);

        /// <inheritdoc />
        public IEnumerator<Port> GetEnumerator() => _ports.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}