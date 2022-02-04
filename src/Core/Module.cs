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
            CatalogNumber catalogNumber, Vendor vendor, ProductType productType, ushort productCode, Revision revision,
            IEnumerable<PortDefinition> ports, string? parentModule, int parentModPortId, Port? parentPort,
            KeyingState? state, bool inhibited, bool majorFault, bool safetyEnabled,
            Connection? connection,
            ITag<IComplexType>? config, ITag<IComplexType>? input, ITag<IComplexType>? output,
            string? description)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? string.Empty;
            CatalogNumber = catalogNumber;
            Vendor = vendor;
            ProductType = productType;
            ProductCode = productCode;
            Revision = revision;
            State = state ?? KeyingState.CompatibleModule;
            Inhibited = inhibited;
            MajorFault = majorFault;
            SafetyEnabled = safetyEnabled;
            Ports = ConfigurePorts(parentPort, ports);
            ParentPort = parentModule == name ? Ports[parentModPortId] : parentPort;
            Connection = connection;
            Config = config;
            Input = input;
            Output = output;
        }

        internal Module(ComponentName name, ModuleDefinition definition, Port parentPort, string? description = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? string.Empty;

            if (definition is null)
                throw new ArgumentNullException(nameof(definition));

            CatalogNumber = definition.CatalogNumber;
            Vendor = definition.Vendor;
            ProductType = definition.ProductType;
            ProductCode = definition.ProductCode;
            Revision = definition.Revisions.Max();
            State = KeyingState.CompatibleModule;
            ParentPort = parentPort;
            Ports = ConfigurePorts(parentPort, definition.Ports);
        }

        public Module(ComponentName name, ModuleDefinition definition, string? description = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? string.Empty;

            if (definition is null)
                throw new ArgumentNullException(nameof(definition));

            CatalogNumber = definition.CatalogNumber;
            Vendor = definition.Vendor;
            ProductType = definition.ProductType;
            ProductCode = definition.ProductCode;
            Revision = definition.Revisions.Max();
            State = KeyingState.CompatibleModule;
            Ports = new PortCollection(this, definition.Ports);
        }

        /// <summary>
        /// Creates a new <see cref="IModule"/> object with the provided name and catalog number.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="catalogNumber"></param>
        /// <param name="description"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <remarks>
        /// This method will use the <see cref="LogixCatalog"/> to lookup the specified catalog number.
        /// </remarks>
        public Module(ComponentName name, CatalogNumber catalogNumber, string? description = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? string.Empty;

            var catalog = new LogixCatalog();
            var definition = catalog.Lookup(catalogNumber);

            if (definition is null)
                throw new ArgumentException(
                    $"Could not find the specified catalog number '{catalogNumber}' in the catalog service file.");

            CatalogNumber = definition.CatalogNumber;
            Vendor = definition.Vendor;
            ProductType = definition.ProductType;
            ProductCode = definition.ProductCode;
            Revision = definition.Revisions.Max();
            State = KeyingState.CompatibleModule;
            Ports = new PortCollection(this, definition.Ports);
        }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public string Description { get; }

        /// <inheritdoc />
        public CatalogNumber CatalogNumber { get; }

        /// <inheritdoc />
        public Vendor Vendor { get; }

        /// <inheritdoc />
        public ProductType ProductType { get; }

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
        public Port? ParentPort { get; }

        /// <inheritdoc />
        public PortCollection Ports { get; }

        /// <inheritdoc />
        public Connection? Connection { get; }

        /// <inheritdoc />
        public ITag<IComplexType>? Config { get; }

        /// <inheritdoc />
        public ITag<IDataType>? Input { get; }

        /// <inheritdoc />
        public ITag<IDataType>? Output { get; }

        private int? GetSlotNumber() =>
            Ports.ChassisPort is not null ? int.Parse(Ports.ChassisPort.Address) : null;

        private IPAddress? GetIpAddress() =>
            Ports.NetworkPort is not null ? IPAddress.Parse(Ports.NetworkPort.Address) : null;

        private PortCollection ConfigurePorts(Port? parentPort, IEnumerable<PortDefinition> portDefinitions)
        {
            var ports = portDefinitions.ToList();

            if (parentPort is null)
                return new PortCollection(this, ports);

            var upstream = ports.FirstOrDefault(p => p.Type == parentPort.Type && !p.DownstreamOnly);

            //todo custom exception?
            if (upstream is null)
                throw new ArgumentException(
                    $"The provided PortDefinitions are incompatible with parent port of type {parentPort.Type}. " +
                    $"Supply at least one upstream capable port of type {parentPort.Type} in order to configure a connection.");

            upstream.Upstream = true;

            var downstream = ports.Where(p => p.Id != upstream.Id).ToList();
            downstream.ForEach(p => p.Upstream = false);

            return new PortCollection(this, ports);
        }
    }
}