using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.L5X;
using L5Sharp.Serialization.Data;

namespace L5Sharp.Serialization.Components
{
    internal class ModuleSerializer : IL5XSerializer<IModule>
    {
        private const string Communications = "Communications";
        private static readonly XName ElementName = L5XElement.Module.ToString();
        private readonly IL5XSerializer<PortDefinition> _portSerializer;
        private readonly IL5XSerializer<Connection> _connectionSerializer;
        private readonly IL5XSerializer<IComplexType> _structureSerializer;

        public ModuleSerializer(L5XContext? context = null)
        {
            _portSerializer = context is not null
                ? context.Serializers.GetSerializer<PortDefinition>(typeof(PortDefinition))
                : new PortSerializer();

            _connectionSerializer = context is not null
                ? context.Serializers.GetSerializer<Connection>(typeof(ConnectionSerializer))
                : new ConnectionSerializer();

            _structureSerializer = context is not null
                ? context.Serializers.GetSerializer<IComplexType>(typeof(StructureSerializer))
                : new StructureSerializer(context);
        }

        public XElement Serialize(IModule component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);

            element.AddAttribute(component, c => c.Name);
            element.AddElement(component, c => c.Description);
            element.AddAttribute(component, c => c.CatalogNumber);
            element.AddAttribute(component, c => c.Vendor.Id, nameOverride: "Vendor");
            element.AddAttribute(component, c => c.ProductType.Id, nameOverride: "ProductType");
            element.AddAttribute(component, c => c.ProductCode);
            element.AddAttribute(component, c => c.Revision.Major, nameOverride: "Major");
            element.AddAttribute(component, c => c.Revision.Minor, nameOverride: "Minor");
            element.AddAttribute(component, c => c.ParentModule);
            element.AddAttribute(component, c => c.ParentPortId, nameOverride: "ParentModPortId");
            element.AddAttribute(component, c => c.Inhibited);
            element.AddAttribute(component, c => c.MajorFault);
            element.AddAttribute(component, c => c.SafetyEnabled);

            var keyingState = new XElement("EKey");
            keyingState.AddAttribute(component, c => c.State);
            element.Add(keyingState);

            var ports = new XElement(L5XElement.Ports.ToString());
            ports.Add(component.Ports.Select(p =>
            {
                var serializer = new PortSerializer();
                var size = p.Bus?.Size ?? 0;
                return serializer.Serialize(new PortDefinition(p.Id, p.Type, p.Upstream, p.Address, size));
            }));
            element.Add(ports);

            var communications = new XElement(Communications);

            if (component.Tags.Config is not null)
            {
                var structureSerializer = new StructureSerializer();
                var config = structureSerializer.Serialize((IComplexType)component.Tags.Config.DataType);
                communications.Add(config);
            }

            var connections = new XElement(L5XElement.Connections.ToString());
            connections.Add(component.Connections.Select(c =>
            {
                var serializer = new ConnectionSerializer();
                return serializer.Serialize(c);
            }));
            communications.Add(connections);

            element.Add(communications);

            return element;
        }

        public IModule Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");

            var name = element.ComponentName();
            var description = element.ComponentDescription();
            var catalogNumber = element.GetAttribute<IModule, CatalogNumber>(c => c.CatalogNumber);
            var vendor = element.GetAttribute<IModule, Vendor>(c => c.Vendor);
            var productType = element.GetAttribute<IModule, ProductType>(c => c.ProductType);
            var productCode = element.GetAttribute<IModule, ushort>(c => c.ProductCode);
            var major = element.Attribute("Major")?.Value;
            var minor = element.Attribute("Minor")?.Value;
            var revision = Revision.Parse($"{major}.{minor}");
            var parentModule = element.GetAttribute<IModule, string>(c => c.ParentModule);
            int.TryParse(element.Attribute("ParentModPortId")?.Value, out var parentModPortId);
            var inhibited = element.GetAttribute<IModule, bool>(c => c.Inhibited);
            var majorFault = element.GetAttribute<IModule, bool>(c => c.MajorFault);
            var safetyEnabled = element.GetAttribute<IModule, bool>(c => c.SafetyEnabled);
            var state = element.Element("EKey")?.GetAttribute<IModule, KeyingState>(c => c.State);

            var ports = element.Descendants(L5XElement.Port.ToString()).Select(e =>
            {
                var serializer = new PortSerializer();
                return serializer.Deserialize(e);
            }).ToList();

            var configType = element.Descendants("ConfigTag").Select(e =>
            {
                var structureSerializer = new StructureSerializer();
                return structureSerializer.Deserialize(e.Descendants(L5XElement.Structure.ToString()).First());
            }).FirstOrDefault();

            var slot = ports.Where(p => !p.Upstream && p.Type != "Ethernet" && int.TryParse(p.Address, out _))
                .Select(p => p.Address)
                .FirstOrDefault();

            var configTagName = slot is not null ? $"{parentModule}:{slot}:C" : $"{name}:C";
            var config = configType is not null ? new Tag<IDataType>(configTagName, configType) : null;

            var connections = element.Descendants(L5XElement.Connection.ToString()).Select(e =>
            {
                var serializer = new ConnectionSerializer();
                return serializer.Deserialize(e);
            }).AsEnumerable();

            return new Module(name, description, catalogNumber!, vendor!, productType!, productCode, revision,
                ports, parentModule, parentModPortId, state, inhibited, majorFault,
                safetyEnabled, config, connections);
        }
    }
}