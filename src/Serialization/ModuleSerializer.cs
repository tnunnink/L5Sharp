using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization
{
    internal class ModuleSerializer : L5XSerializer<IModule>
    {
        private readonly LogixInfo? _document;
        private static readonly XName ElementName = L5XName.Module;

        private PortSerializer PortSerializer => _document is not null
            ? _document.Serializers.Get<PortSerializer>()
            : new PortSerializer();
        
        private ConnectionSerializer ConnectionSerializer => _document is not null
            ? _document.Serializers.Get<ConnectionSerializer>()
            : new ConnectionSerializer(_document);
        
        private ConfigTagSerializer ConfigTagSerializer => _document is not null
            ? _document.Serializers.Get<ConfigTagSerializer>()
            : new ConfigTagSerializer(_document);

        public ModuleSerializer(LogixInfo? document = null)
        {
            _document = document;
        }

        public override XElement Serialize(IModule component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);
            element.AddComponentName(component.Name);
            element.AddComponentDescription(component.Description);
            element.Add(new XAttribute(L5XName.CatalogNumber, component.CatalogNumber));
            element.Add(new XAttribute(L5XName.Vendor, component.Vendor.Id));
            element.Add(new XAttribute(L5XName.ProductType, component.ProductType.Id));
            element.Add(new XAttribute(L5XName.ProductCode, component.ProductCode));
            element.Add(new XAttribute(L5XName.Major, component.Revision.Major));
            element.Add(new XAttribute(L5XName.Minor, component.Revision.Minor));
            element.Add(new XAttribute(L5XName.ParentModule, component.ParentModule));
            element.Add(new XAttribute(L5XName.ParentModPortId, component.ParentPortId));
            element.Add(new XAttribute(L5XName.Inhibited, component.Inhibited));
            element.Add(new XAttribute(L5XName.MajorFault, component.MajorFault));
            element.Add(new XAttribute(L5XName.SafetyEnabled, component.SafetyEnabled));

            var keyingState = new XElement(L5XName.EKey);
            keyingState.Add(new XAttribute(L5XName.State, component.Keying));
            element.Add(keyingState);

            var ports = new XElement(L5XName.Ports);
            ports.Add(component.Ports.Select(p => PortSerializer.Serialize(p)));
            element.Add(ports);

            var communications = new XElement(L5XName.Communications);

            if (component.Config is not null)
            {
                var config = ConfigTagSerializer.Serialize(component.Config);
                communications.Add(config);
            }

            var connections = new XElement(L5XName.Connections);
            connections.Add(component.Connections.Select(c => ConnectionSerializer.Serialize(c)));
            communications.Add(connections);

            element.Add(communications);

            return element;
        }

        public override IModule Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");

            var name = element.ComponentName();
            var description = element.ComponentDescription();
            var catalogNumber = element.Attribute(L5XName.CatalogNumber)?.Value.Parse<CatalogNumber>();
            var vendor = element.Attribute(L5XName.Vendor)?.Value.Parse<Vendor>();
            var productType = element.Attribute(L5XName.ProductType)?.Value.Parse<ProductType>();
            var productCode = element.Attribute(L5XName.ProductCode)?.Value.Parse<ushort>() ?? default;
            var major = element.Attribute(L5XName.Major)?.Value;
            var minor = element.Attribute(L5XName.Minor)?.Value;
            var revision = Revision.Parse($"{major}.{minor}");
            var parentModule = element.Attribute(L5XName.ParentModule)?.Value;
            var parentModPortId = element.Attribute(L5XName.ParentModPortId)
                ?.Value.Parse<int>() ?? default;
            var inhibited = element.Attribute(L5XName.Inhibited)?.Value.Parse<bool>() ?? default;
            var majorFault = element.Attribute(L5XName.MajorFault)?.Value.Parse<bool>() ?? default;
            var safetyEnabled = element.Attribute(L5XName.SafetyEnabled)?.Value.Parse<bool>() ??
                                default;
            var state = element.Element(L5XName.EKey)?.Attribute(L5XName.State)
                ?.Value.Parse<ElectronicKeying>();

            var ports = element.Descendants(L5XName.Port)
                .Select(e => PortSerializer.Deserialize(e))
                .ToList();

            var configElement = element.Descendants(L5XName.ConfigTag).FirstOrDefault();
            var config = configElement is not null ? ConfigTagSerializer.Deserialize(configElement) : null;

            var connections = element.Descendants(L5XName.Connection)
                .Select(e => ConnectionSerializer.Deserialize(e))
                .ToList();

            var modules = element.Ancestors(L5XName.Modules)
                .FirstOrDefault()
                ?.Descendants(L5XName.Module)
                .Where(e => e.Attribute(L5XName.ParentModule)?.Value == name
                            && e.Attribute(L5XName.Name) is not null
                            && e.ComponentName() != name)
                .Select(Deserialize)
                .ToList();

            return new Module(name, catalogNumber!, vendor!, productType!, productCode, revision, ports,
                parentModule, parentModPortId, state, inhibited, majorFault, safetyEnabled, config,
                connections, modules, description);
        }
    }
}