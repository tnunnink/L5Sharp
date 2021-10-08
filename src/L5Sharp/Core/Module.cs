using System.Collections.Generic;
using System.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Enums;
using L5Sharp.Utilities;

namespace L5Sharp.Core
{
    public class Module : IComponent
    {
        private string _name;
        private readonly Dictionary<int, Port> _ports = new Dictionary<int, Port>();
        private readonly Dictionary<string, Connection> _connections = new Dictionary<string, Connection>();

        internal Module(Module parent, string name, string catalogNumber, string vendor, int productType,
            int productCode, Revision revision, KeyingState state, IEnumerable<Port> ports, string description = null,
            byte mode = 0, bool inhibited = false, bool majorFault = false, bool safetyEnabled = false)
        {
            Parent = parent;
            ParentPortId = parent.Ports.FirstOrDefault()?.Id ?? 0;
            
            Name = name;
            CatalogNumber = catalogNumber;
            Vendor = vendor;
            ProductType = productType;
            ProductCode = productCode;
            Revision = revision;
            
            Description = description ?? string.Empty;
            State = state == null ? KeyingState.CompatibleModule : state;

            Mode = mode;
            Inhibited = inhibited;
            MajorFault = majorFault;
            SafetyEnabled = safetyEnabled;
        }

        public string Name
        {
            get => _name;
            set
            {
                Validate.Name(value);
                _name = value;
            }
        }

        public IEnumerable<Port> Ports => _ports.Values.AsEnumerable();
        public KeyingState State { get; set; }
        public string Description { get; set; }
        public string CatalogNumber { get; }
        public string Vendor { get; }
        public int ProductType { get; }
        public int ProductCode { get; }
        public Revision Revision { get; }
        public byte Mode { get; }
        public Module Parent { get; }
        public int ParentPortId { get; }
        public bool Inhibited { get; }
        public bool MajorFault { get; }
        public bool SafetyEnabled { get; }
        public IEnumerable<Connection> Connections => _connections.Values.AsEnumerable();
    }
}