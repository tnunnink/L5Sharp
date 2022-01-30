using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    /// <inheritdoc />
    public class Module : IModule
    {
        internal Module(string name,
            string catalogNumber, ushort vendor, ushort productType, ushort productCode, Revision revision,
            string parentModule, int parentModPortId,
            IEnumerable<Port> ports, KeyingState? state = null,
            bool inhibited = false, bool majorFault = false, bool safetyEnabled = false,
            Connection? connection = null,
            ITag<IComplexType>? config = null, ITag<IComplexType>? input = null, ITag<IComplexType>? output = null,
            string? description = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? string.Empty;
            CatalogNumber = catalogNumber;
            Vendor = vendor;
            ProductType = productType;
            ProductCode = productCode;
            Revision = revision;
            ParentModule = parentModule;
            ParentModPortId = parentModPortId;
            State = state ?? KeyingState.CompatibleModule;
            Inhibited = inhibited;
            MajorFault = majorFault;
            SafetyEnabled = safetyEnabled;
            Ports = new PortCollection(ports);
            Connection = connection;
            Config = config;
            Input = input;
            Output = output;

            var children = Ports.Where(p => !p.Upstream && p.Bus is not null).ToList();
            Modules = children.Count > 0 ? new ModuleCollection(children) : null;
        }

        /// <summary>
        /// Creates a new <see cref="IModule"/> with the provided name, definition, and port.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="definition"></param>
        /// <param name="port"></param>
        /// <param name="description"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public Module(ComponentName name, ModuleDefinition definition, Port port, string? description = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? string.Empty;
            CatalogNumber = definition.CatalogNumber;
            Vendor = definition.Vendor.Id;
            ProductType = definition.ProductType.Id;
            ProductCode = definition.ProductType.Code;
            Revision = definition.Revision;
            State = KeyingState.CompatibleModule;
            ParentModule = string.Empty;
            Ports = new PortCollection(new []{port});
        }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public string Description { get; }

        /// <inheritdoc />
        public string CatalogNumber { get; }

        /// <inheritdoc />
        public ushort Vendor { get; }

        /// <inheritdoc />
        public ushort ProductType { get; }

        /// <inheritdoc />
        public ushort ProductCode { get; }

        /// <inheritdoc />
        public Revision Revision { get; }

        /// <inheritdoc />
        public bool Inhibited { get; }

        /// <inheritdoc />
        public bool MajorFault { get; }

        /// <inheritdoc />
        public bool SafetyEnabled { get; }

        /// <inheritdoc />
        public KeyingState State { get; }

        /// <inheritdoc />
        public int? Slot => GetSlotNumber();

        /// <inheritdoc />
        public IPAddress? IP => GetIpAddress();

        /// <inheritdoc />
        public string ParentModule { get; }

        /// <inheritdoc />
        public int ParentModPortId { get; }

        /// <inheritdoc />
        public PortCollection Ports { get; }

        /// <inheritdoc />
        public Connection? Connection { get; }

        /// <inheritdoc />
        public IModuleCollection? Modules { get; }

        /// <inheritdoc />
        public ITag<IComplexType>? Config { get; }

        /// <inheritdoc />
        public ITag<IDataType>? Input { get; }

        /// <inheritdoc />
        public ITag<IDataType>? Output { get; }


        private int? GetSlotNumber()
        {
            foreach (var port in Ports.Where(port => port.Type != "Ethernet"))
            {
                if (int.TryParse(port.Address, out var slot))
                    return slot;
            }

            return null;
        }

        private IPAddress? GetIpAddress()
        {
            foreach (var port in Ports.Where(port => port.Type == "Ethernet"))
            {
                if (IPAddress.TryParse(port.Address, out var address))
                    return address;
            }

            return null;
        }
    }
}