using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.L5X;

namespace L5Sharp.Serialization.Components
{
    internal class ModuleSerializer : L5XSerializer<IModule>
    {
        private static readonly XName ElementName = L5XElement.Module.ToString();
        private readonly PortSerializer _portSerializer;
        private readonly ConnectionSerializer _connectionSerializer;
        private readonly ModuleTagSerializer _configTagSerializer;

        public ModuleSerializer()
        {
            _portSerializer = new PortSerializer();
            _connectionSerializer = new ConnectionSerializer();
            _configTagSerializer = new ModuleTagSerializer(L5XElement.ConfigTag.ToString(), "C");
        }

        public override XElement Serialize(IModule component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);
            element.AddComponentName(component.Name);
            element.AddComponentDescription(component.Description);
            element.Add(new XAttribute(L5XAttribute.CatalogNumber.ToString(), component.CatalogNumber));
            element.Add(new XAttribute(L5XAttribute.Vendor.ToString(), component.Vendor.Id));
            element.Add(new XAttribute(L5XAttribute.ProductType.ToString(), component.ProductType.Id));
            element.Add(new XAttribute(L5XAttribute.ProductCode.ToString(), component.ProductCode));
            element.Add(new XAttribute(L5XAttribute.Major.ToString(), component.Revision.Major));
            element.Add(new XAttribute(L5XAttribute.Minor.ToString(), component.Revision.Minor));
            element.Add(new XAttribute(L5XAttribute.ParentModule.ToString(), component.ParentModule));
            element.Add(new XAttribute(L5XAttribute.ParentModPortId.ToString(), component.ParentPortId));
            element.Add(new XAttribute(L5XAttribute.Inhibited.ToString(), component.Inhibited));
            element.Add(new XAttribute(L5XAttribute.MajorFault.ToString(), component.MajorFault));
            element.Add(new XAttribute(L5XAttribute.SafetyEnabled.ToString(), component.SafetyEnabled));

            var keyingState = new XElement(L5XElement.EKey.ToString());
            keyingState.Add(new XAttribute(L5XAttribute.State.ToString(), component.State));
            element.Add(keyingState);

            var ports = new XElement(L5XElement.Ports.ToString());
            ports.Add(component.Ports.Select(p => _portSerializer.Serialize(p)));
            element.Add(ports);

            var communications = new XElement(L5XElement.Communications.ToString());

            if (component.Tags.Config is not null)
            {
                var config = _configTagSerializer.Serialize(component.Tags.Config);
                communications.Add(config);
            }

            var connections = new XElement(L5XElement.Connections.ToString());
            connections.Add(component.Connections.Select(c => _connectionSerializer.Serialize(c)));
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
            var catalogNumber = element.Attribute(L5XAttribute.CatalogNumber.ToString())?.Value.Parse<CatalogNumber>();
            var vendor = element.Attribute(L5XAttribute.Vendor.ToString())?.Value.Parse<Vendor>();
            var productType = element.Attribute(L5XAttribute.ProductType.ToString())?.Value.Parse<ProductType>();
            var productCode = element.Attribute(L5XAttribute.ProductCode.ToString())?.Value.Parse<ushort>() ?? default;
            var major = element.Attribute(L5XAttribute.Major.ToString())?.Value;
            var minor = element.Attribute(L5XAttribute.Minor.ToString())?.Value;
            var revision = Revision.Parse($"{major}.{minor}");
            var parentModule = element.Attribute(L5XAttribute.ParentModule.ToString())?.Value;
            var parentModPortId = element.Attribute(L5XAttribute.ParentModPortId.ToString())
                ?.Value.Parse<int>() ?? default;
            var inhibited = element.Attribute(L5XAttribute.Inhibited.ToString())?.Value.Parse<bool>() ?? default;
            var majorFault = element.Attribute(L5XAttribute.MajorFault.ToString())?.Value.Parse<bool>() ?? default;
            var safetyEnabled = element.Attribute(L5XAttribute.SafetyEnabled.ToString())?.Value.Parse<bool>() ??
                                default;
            var state = element.Element(L5XElement.EKey.ToString())?.Attribute(L5XAttribute.State.ToString())
                ?.Value.Parse<KeyingState>();

            var ports = element.Descendants(L5XElement.Port.ToString())
                .Select(e => _portSerializer.Deserialize(e))
                .ToList();

            var configElement = element.Descendants(L5XElement.ConfigTag.ToString()).FirstOrDefault();
            var config = configElement is not null ? _configTagSerializer.Deserialize(configElement) : null;

            var connections = element.Descendants(L5XElement.Connection.ToString())
                .Select(e => _connectionSerializer.Deserialize(e))
                .ToList();

            var modules = element.Ancestors(L5XElement.Modules.ToString()).First()
                .Descendants(L5XElement.Module.ToString())
                .Where(e => e.Attribute(L5XAttribute.ParentModule.ToString())?.Value == name
                            && e.ComponentName() != name)
                .Select(Deserialize)
                .ToList();

            return new Module(name, description, catalogNumber!, vendor!, productType!, productCode, revision,
                parentModule, parentModPortId, state, inhibited, majorFault, safetyEnabled, config,
                ports, connections, modules);
        }
    }
}