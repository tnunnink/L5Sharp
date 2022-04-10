using System;
using System.Linq;
using System.Net;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;

namespace L5Sharp.Builders
{
    internal class ModuleBuilder : IModuleBuilder, IModuleCatalogNumberStep
    {
        private readonly ComponentName _name;
        private bool _inhibited;
        private bool _majorFault;
        private bool _safetyEnabled;
        private ElectronicKeying? _keying;
        private string? _description;
        private IPAddress? _ip;
        private Revision? _revision;
        private byte _slot;
        private IModule? _parent;
        private CatalogNumber? _catalogNumber;

        internal ModuleBuilder(ComponentName name)
        {
            _name = name;
        }

        public IModule Create()
        {
            if (_catalogNumber is null)
                throw new ArgumentNullException(nameof(_catalogNumber));

            var catalogService = new ModuleCatalog();

            var definition = catalogService.Lookup(_catalogNumber);
            
            
            definition.ConfigureSlot(_slot);
            
            if (_ip is not null)
                definition.ConfigureIP(_ip);

            var parentModule = _parent?.Name ?? string.Empty;
            var parentPortId = _parent?.Ports.Downstream.FirstOrDefault()?.Id ?? default;
            var portType = _parent?.Ports.Downstream.FirstOrDefault()?.Type ?? string.Empty;
            
            if (!portType.IsEmpty())
                definition.ConfigureUpstream(portType);
            

            var module = new Module(_name, _catalogNumber,
                definition.Vendor, definition.ProductType, definition.ProductCode,
                _revision ?? definition.Revisions.Max(), definition.Ports.ToList(), parentModule, parentPortId,
                _keying, _inhibited, _majorFault, _safetyEnabled, description: _description);

            _parent?.Bus(parentPortId).Add(module);

            return module;
        }

        public IModuleBuilder WithCatalog(CatalogNumber catalogNumber)
        {
            _catalogNumber = catalogNumber;
            return this;
        }

        public IModuleBuilder WithParent(IModule parent)
        {
            _parent = parent;
            return this;
        }

        public IModuleBuilder WithIP(IPAddress ip)
        {
            _ip = ip;
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
            _slot = slot;
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

        public IModuleBuilder WithState(ElectronicKeying keying)
        {
            _keying = keying;
            return this;
        }
    }
}