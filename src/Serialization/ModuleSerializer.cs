using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Helpers;

namespace L5Sharp.Serialization
{
    internal class ModuleSerializer : IXSerializer<IModule>
    {
        private readonly LogixContext _context;
        private const string Communications = "Communications";
        private static readonly XName ElementName = LogixNames.Module;

        public ModuleSerializer(LogixContext context)
        {
            _context = context;
        }

        public XElement Serialize(IModule component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);
            
            element.AddAttribute(component, c => c.Name);
            element.AddAttribute(component, c => c.CatalogNumber);
            element.AddAttribute(component, c => c.Vendor);
            element.AddAttribute(component, c => c.ProductType);
            element.AddAttribute(component, c => c.ProductCode);
            element.AddAttribute(component, c => c.Revision.Major, nameOverride: "Major");
            element.AddAttribute(component, c => c.Revision.Minor, nameOverride: "Minor");
            element.AddAttribute(component, c => c.ParentModule);
            element.AddAttribute(component, c => c.ParentModPortId);
            element.AddAttribute(component, c => c.Inhibited);
            element.AddAttribute(component, c => c.MajorFault);
            element.AddAttribute(component, c => c.SafetyEnabled);

            var keyingState = new XElement("EKey");
            keyingState.AddAttribute(component, c => c.State);
            element.Add(keyingState);
            
            var ports = new XElement(nameof(component.Ports));
            ports.Add(component.Ports.Select(p => _context.Serializer.Serialize(p)));
            element.Add(ports);

            var communications = new XElement(Communications);

            var config = _context.Serializer.Serialize(component.Config);
            communications.Add(config);
            
            var connections = new XElement(nameof(component.Connections));
            connections.Add(component.Connections.Select(c => _context.Serializer.Serialize(c)));
            communications.Add(connections);
            
            element.Add(communications);

            return element;
        }

        public IModule Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element name '{element.Name}' invalid. Expecting '{ElementName}'");

            var name = element.GetComponentName();
            var description = element.GetComponentDescription();
            var catalogNumber = element.GetAttribute<Module, string>(c => c.CatalogNumber);
            var vendor = element.GetAttribute<Module, ushort>(c => c.Vendor);
            var productType = element.GetAttribute<Module, ushort>(c => c.ProductType);
            var productCode = element.GetAttribute<Module, ushort>(c => c.ProductCode);
            var parentModule = element.GetAttribute<Module, string>(c => c.ParentModule);
            var parentModPortId = element.GetAttribute<Module, int>(c => c.ParentModPortId);
            var inhibited = element.GetAttribute<Module, bool>(c => c.Inhibited);
            var majorFault = element.GetAttribute<Module, bool>(c => c.MajorFault);
            var safetyEnabled = element.GetAttribute<Module, bool>(c => c.SafetyEnabled);
            var state = element.Element("EKey")?.GetAttribute<Module, KeyingState>(c => c.State);
            var ports = element.Descendants(LogixNames.Port).Select(e =>
            {
                var serializer = new PortSerializer();
                return serializer.Deserialize(e);
            });

            var communications = element.Element(Communications);
            var config = _context.Serializer.Serialize(communications?.Element("ConfigTag"));

            return new Module(name, catalogNumber!, vendor, productType, productCode, new Revision(), ports,
                parentModule, parentModPortId, inhibited, majorFault, safetyEnabled, state);
        }
    }
}