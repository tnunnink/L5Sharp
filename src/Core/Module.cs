using System;
using System.Collections.Generic;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    /// <inheritdoc />
    public class Module : IModule
    {
        internal Module(string name,
            string catalogNumber, ushort vendor, ushort productType, ushort productCode, Revision revision,
            IEnumerable<Port> ports,
            string? parentModule = null, int parentModPortId = 0,
            bool inhibited = false, bool majorFault = false, bool safetyEnabled = false,
            KeyingState? state = null,
            IEnumerable<Connection>? connections = null,
            ITag<IComplexType>? config = null,
            string? description = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? string.Empty;
            CatalogNumber = catalogNumber;
            Vendor = vendor;
            ProductType = productType;
            ProductCode = productCode;
            Revision = revision;
            ParentModule = parentModule ?? string.Empty;
            ParentModPortId = parentModPortId;
            Inhibited = inhibited;
            MajorFault = majorFault;
            SafetyEnabled = safetyEnabled;
            State = state ?? KeyingState.CompatibleModule;
            Ports = ports;
            Connections = connections ?? new List<Connection>();
            Config = config;
        }

        /// <summary>
        /// Creates a new <see cref="Module"/> with the provided name and 
        /// </summary>
        /// <param name="name">The name of the module.</param>
        /// <param name="catalogNumber"></param>
        /// <param name="vendor"></param>
        /// <param name="productType"></param>
        /// <param name="productCode"></param>
        /// <param name="revision"></param>
        /// <param name="ports"></param>
        public Module(ComponentName name, string catalogNumber, ushort vendor,
            ushort productType, ushort productCode, Revision revision, IEnumerable<Port> ports)
            : this(name, catalogNumber, vendor, productType, productCode, revision, ports, string.Empty)
        {
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
        public string ParentModule { get; }

        /// <inheritdoc />
        public int ParentModPortId { get; }

        /// <inheritdoc />
        public bool Inhibited { get; }

        /// <inheritdoc />
        public bool MajorFault { get; }

        /// <inheritdoc />
        public bool SafetyEnabled { get; }

        /// <inheritdoc />
        public KeyingState State { get; }

        /// <inheritdoc />
        public IEnumerable<Port> Ports { get; }

        /// <inheritdoc />
        public IEnumerable<Connection> Connections { get; }

        /// <inheritdoc />
        public ITag<IComplexType> Config { get; }
    }
}