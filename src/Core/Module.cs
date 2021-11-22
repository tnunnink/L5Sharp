using System.Collections.Generic;
using L5Sharp.Abstractions;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    public class Module : IModule
    {
        internal Module(string name, string catalogNumber, ushort vendor, ushort productType,
            ushort productCode, Revision revision, KeyingState state, string description = null, bool inhibited = false,
            bool majorFault = false, bool safetyEnabled = false)
        {
            Name = name;
            Description = description;
            CatalogNumber = catalogNumber;
            Vendor = vendor;
            ProductType = productType;
            ProductCode = productCode;
            Revision = revision;
            State = state != null ? state : KeyingState.CompatibleModule;
            Inhibited = inhibited;
            MajorFault = majorFault;
            SafetyEnabled = safetyEnabled;
        }

        public ComponentName Name { get; }
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
        public ITag<IModuleDefined> Config { get; }
        public ITag<IModuleDefined> Input { get; }
        public ITag<IModuleDefined> Output { get; }
    }
}