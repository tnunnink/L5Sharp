using System;
using System.Collections.Generic;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    /// <inheritdoc />
    public class Module : IModule
    {
        internal Module(string name, string? description, string catalogNumber, ushort vendor, ushort productType,
            ushort productCode, Revision revision, string parentModule, int parentModPortId,
            bool inhibited, bool majorFault, bool safetyEnabled, KeyingState state, IEnumerable<Port> ports,
            IEnumerable<Connection>? connections = null, ITag<IComplexType>? config = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? string.Empty;
            CatalogNumber = catalogNumber;
            Vendor = vendor;
            ProductType = productType;
            ProductCode = productCode;
            Revision = revision;
            ParentModPortId = parentModPortId;
            ParentModule = parentModule;
            Inhibited = inhibited;
            MajorFault = majorFault;
            SafetyEnabled = safetyEnabled;
            State = state;
            Ports = ports;
            Connections = connections;
            Config = config;
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