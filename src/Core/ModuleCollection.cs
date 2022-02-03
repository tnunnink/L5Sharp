using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace L5Sharp.Core
{
    /// <inheritdoc />
    public class ModuleCollection : IModuleCollection
    {
        private readonly IModule _parent;
        private readonly IEnumerable<Port> _ports;

        internal ModuleCollection(IModule parent)
        {
            _parent = parent;
            _ports = _parent.Ports.Where(p => !p.Upstream && p.Bus is not null);
        }

        private Port DefaultPort => _ports.First();

        /// <inheritdoc />
        public void Add(ComponentName name, ModuleDefinition definition, string? description = null)
        {
            /*if (name is null)
                throw new ArgumentNullException(nameof(name));

            if (definition is null)
                throw new ArgumentNullException(nameof(definition));

            var ports = GenerateConnectingPorts(definition.Ports.ToList());

            var module = new Module(name, definition.CatalogNumber, definition.Vendor, definition.ProductType,
                definition.ProductCode, definition.Revisions.Max(), _parent.Name, DefaultPort.Id, ports);

            DefaultPort.Bus?.Add(module);*/
        }

        /*private IEnumerable<Port> GenerateConnectingPorts(List<Port> ports)
        {
            if (ports is null)
                throw new ArgumentNullException(nameof(ports));

            var connectingPorts = new List<Port>();

            var targetPort = ports.FirstOrDefault(p => p.Type == DefaultPort.Type);

            if (targetPort is null)
                throw new ArgumentException(
                    $"No port of type '{DefaultPort.Type}' has been identified in the provided port collection.");

            //targetPort.AssignAddress();

            var downstreamPorts = ports.Where(p => p.Id != targetPort.Id)
                .Select(p => new Port(p.Id, p.Type, false, p.Address, p.Bus?.Size));

            connectingPorts.Add(targetPort);
            connectingPorts.AddRange(downstreamPorts);

            return connectingPorts;
        }*/

        /// <inheritdoc />
        public IEnumerator<IModule> GetEnumerator() => _ports.SelectMany(p => p.Bus).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}