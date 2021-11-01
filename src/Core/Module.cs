using System.Collections.Generic;
using System.Linq;
using System.Net;
using L5Sharp.Abstractions;
using L5Sharp.Enums;
using L5Sharp.Utilities;

namespace L5Sharp.Core
{
    public class Module : LogixComponent, IModule
    {
        private readonly Dictionary<int, Port> _ports = new Dictionary<int, Port>();
        private readonly Dictionary<string, Connection> _connections = new Dictionary<string, Connection>();

        public Module(string name, string catalogNumber, ushort vendor, ushort productType,
            ushort productCode, Revision revision, KeyingState state, string description = null, bool inhibited = false,
            bool majorFault = false, bool safetyEnabled = false)
            : base(name, description)
        {
            CatalogNumber = catalogNumber;
            Vendor = vendor;
            ProductType = productType;
            ProductCode = productCode;
            Revision = revision;

            State = state == null ? KeyingState.CompatibleModule : state;

            Inhibited = inhibited;
            MajorFault = majorFault;
            SafetyEnabled = safetyEnabled;
        }


        public string CatalogNumber { get; }
        public ushort Vendor { get; }
        public ushort ProductType { get; }
        public ushort ProductCode { get; }
        public Revision Revision { get; }
        public Module Parent { get; }
        public string ParentModule { get; }
        public int ParentPortId { get; }
        public bool Inhibited { get; }
        public bool MajorFault { get; }
        public bool SafetyEnabled { get; }
        public KeyingState State { get; }
        public byte CassisSize { get; }
        public IPAddress Address { get; }
        public byte Slot { get; }
        public IEnumerable<IModule> Modules { get; }
        public IEnumerable<Connection> Connections { get; }
        public void SetCatalogNumber(string catalogNumber)
        {
            throw new System.NotImplementedException();
        }

        public void SetVendor(string vendor)
        {
            throw new System.NotImplementedException();
        }
    }
}