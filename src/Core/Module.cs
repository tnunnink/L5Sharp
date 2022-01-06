using System;
using System.Collections.Generic;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    public class Module : IModule
    {
        internal Module(string name, string? description, string catalogNumber, ushort vendor, ushort productType,
            ushort productCode, Revision revision, Module parent, int parentModPortId, string parentModule,
            bool inhibited, bool majorFault, bool safetyEnabled, KeyingState state, IEnumerable<Port> ports,
            IEnumerable<Connection> connections)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? string.Empty;
            CatalogNumber = catalogNumber;
            Vendor = vendor;
            ProductType = productType;
            ProductCode = productCode;
            Revision = revision;
            Parent = parent;
            ParentModPortId = parentModPortId;
            ParentModule = parentModule;
            Inhibited = inhibited;
            MajorFault = majorFault;
            SafetyEnabled = safetyEnabled;
            State = state;
            Ports = ports;
            Connections = connections;
        }

        public string Name { get; }
        public string Description { get; }
        public string CatalogNumber { get; }
        public ushort Vendor { get; }
        public ushort ProductType { get; }
        public ushort ProductCode { get; }
        public Revision Revision { get; }
        public Module Parent { get; }
        public int ParentModPortId { get; }
        public string ParentModule { get; }
        public bool Inhibited { get; }
        public bool MajorFault { get; }
        public bool SafetyEnabled { get; }
        public KeyingState State { get; }
        public IEnumerable<Port> Ports { get; }
        public IEnumerable<Connection> Connections { get; }
        public ITag<IComplexType> Config { get; }
    }
}