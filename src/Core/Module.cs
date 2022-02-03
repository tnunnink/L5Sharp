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
            ParentModule = string.Empty;
            State = KeyingState.CompatibleModule;
            Ports = new PortCollection(definition.Ports);
        }

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
            ParentModule = string.Empty;
            State = KeyingState.CompatibleModule;
            Ports = new PortCollection(definition.Ports);
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
        public string ParentModule { get; }

        /// <inheritdoc />
        public int ParentModPortId { get; }

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
    }
}