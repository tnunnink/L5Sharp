using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Helpers;

namespace L5Sharp.Serialization
{
    internal class ModuleSerializer : IXSerializer<Module>
    {
        private const string Communications = "Communications";
        private static readonly XName ElementName = LogixNames.Module;

        public XElement Serialize(Module component)
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

            var ports = new XElement(LogixNames.Ports);
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

            var connections = new XElement(LogixNames.Connections);
            connections.Add(component.Connections.Select(c =>
            {
                var serializer = new ConnectionSerializer();
                return serializer.Serialize(c);
            }));
            communications.Add(connections);

            element.Add(communications);

            return element;
        }

        public Module Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element name '{element.Name}' invalid. Expecting '{ElementName}'");

            var name = element.GetComponentName();
            var description = element.GetComponentDescription();
            var catalogNumber = element.GetAttribute<Module, CatalogNumber>(c => c.CatalogNumber);
            var vendor = element.GetAttribute<Module, Vendor>(c => c.Vendor);
            var productType = element.GetAttribute<Module, ProductType>(c => c.ProductType);
            var productCode = element.GetAttribute<Module, ushort>(c => c.ProductCode);
            var major = element.Attribute("Major")?.Value;
            var minor = element.Attribute("Minor")?.Value;
            var revision = Revision.Parse($"{major}.{minor}");
            var parentModule = element.GetAttribute<Module, string>(c => c.ParentModule);
            int.TryParse(element.Attribute("ParentModPortId")?.Value, out var parentModPortId);
            var inhibited = element.GetAttribute<Module, bool>(c => c.Inhibited);
            var majorFault = element.GetAttribute<Module, bool>(c => c.MajorFault);
            var safetyEnabled = element.GetAttribute<Module, bool>(c => c.SafetyEnabled);
            var state = element.Element("EKey")?.GetAttribute<Module, KeyingState>(c => c.State);

            var ports = element.Descendants(LogixNames.Port).Select(e =>
            {
                var serializer = new PortSerializer();
                return serializer.Deserialize(e);
            }).ToList();

            var configType = element.Descendants("ConfigTag").Select(e =>
            {
                var structureSerializer = new StructureSerializer();
                return structureSerializer.Deserialize(e.Descendants(LogixNames.Structure).First());
            }).FirstOrDefault();

            var slot = ports.Where(p => !p.Upstream && p.Type != "Ethernet" && int.TryParse(p.Address, out _))
                .Select(p => p.Address)
                .FirstOrDefault();

            var configTagName = slot is not null ? $"{parentModule}:{slot}:C" : $"{name}:C";
            var config = configType is not null ? new Tag<IDataType>(configTagName, configType) : null;

            var connections = element.Descendants(LogixNames.Connection).Select(e =>
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