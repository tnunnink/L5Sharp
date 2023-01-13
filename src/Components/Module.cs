using System.Collections.Generic;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Components
{
    public class Module : ILogixComponent
    {
        /// <inheritdoc />
        public string Name { get; set; } = string.Empty;
        
        /// <inheritdoc />
        public string Description { get; set; } = string.Empty;

        public string CatalogNumber { get; set; } = string.Empty;

        public Vendor Vendor { get; set; } = Vendor.Unknown;
        
        public ProductType ProductType { get; } = ProductType.Unknown;
        
        public ushort ProductCode { get; set; } = 0;

        public Revision Revision { get; set; } = new();
        public string ParentModule { get; set; } = string.Empty;
        
        public int ParentPortId { get; set; } = 0;

        public bool Inhibited { get; set; } = false;

        public bool MajorFault { get; set; } = false;

        public bool SafetyEnabled { get; set; } = false;

        public ElectronicKeying Keying { get; set; } = ElectronicKeying.CompatibleModule;

        public PortCollection Ports { get; set; }
        
        public Tag? Config { get; set; }

        public List<ModuleConnection> Connections { get; }
        public XElement Serialize()
        {
            throw new System.NotImplementedException();
        }

        public void Deserialize(XElement element)
        {
            throw new System.NotImplementedException();
        }
    }
}