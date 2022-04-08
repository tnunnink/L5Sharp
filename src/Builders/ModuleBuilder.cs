using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Builders
{
    internal class ModuleBuilder : IModuleBuilder, IModuleCatalogNumberStep
    {
        private readonly ComponentName _name;
        private CatalogNumber _catalogNumber;
        private Vendor _vendor;
        private ProductType _productType;
        private ushort _productCode;
        private Revision _revision;
        private List<Port> _ports;
        private string _parentModule;
        private int _parentPortId;
        private bool _inhibited;
        private bool _majorFault;
        private bool _safetyEnabled;
        private KeyingState _keyingState;
        private string _description;
        
        public ModuleBuilder(ComponentName name)
        {
            _name = name;
            _catalogNumber = new CatalogNumber("UNKNOWN");
            _vendor = Vendor.Unknown;
            _productType = ProductType.Unknown;
            _productCode = 0;
            _revision = new Revision();
            _ports = new List<Port>();
            _parentModule = string.Empty;
            _keyingState = KeyingState.CompatibleModule;
            _description = string.Empty;
        }

        public IModule Create() =>
            new Module(_name, _catalogNumber, _vendor, _productType, _productCode, _ports, _revision,
                _parentModule, _parentPortId, _keyingState, _inhibited, _majorFault, _safetyEnabled, _description);

        public IModuleBuilder WithCatalog(CatalogNumber catalogNumber)
        {
            var catalogService = new ModuleCatalog();
            var definition = catalogService.Lookup(catalogNumber);

            _catalogNumber = definition.CatalogNumber;
            _vendor = definition.Vendor;
            _productType = definition.ProductType;
            _productCode = definition.ProductCode;
            _ports = definition.Ports.ToList();
            _revision = definition.Revisions.Max();
            
            return this;
        }

        public IModuleBuilder WithConnectionTo(IModule parent)
        {
            _parentModule = parent.Name;
            _parentPortId = parent.Ports.Downstream().FirstOrDefault()?.Id ?? default;
            
            
            return this;
        }

        public IModuleBuilder WithIP(IPAddress ip)
        {
            var port = _ports.FirstOrDefault(p => p.Type == "Ethernet");
            return this;
        }

        public IModuleBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public IModuleBuilder WithRevision(Revision revision)
        {
            _revision = revision;
            return this;
        }

        public IModuleBuilder WithSlot(byte slot)
        {
            //todo configure ports
            return this;
        }

        public IModuleBuilder IsInhibited()
        {
            _inhibited = true;
            return this;
        }

        public IModuleBuilder MajorFaults()
        {
            _majorFault = true;
            return this;
        }

        public IModuleBuilder IsSafetyEnabled()
        {
            _safetyEnabled = true;
            return this;
        }

        public IModuleBuilder WithState(KeyingState keyingState)
        {
            _keyingState = keyingState;
            return this;
        }
    }
}