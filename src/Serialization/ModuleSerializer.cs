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

            var module = (Module)component;
            var ports = new XElement(LogixNames.Ports);
            ports.Add(module.Ports.Select(p => _context.Serializer.Serialize(p)));
            element.Add(ports);

            var communications = new XElement(Communications);
            //config
            //connections....

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
            var catalogNumber = element.GetAttribute<IModule, string>(c => c.CatalogNumber);
            var vendor = element.GetAttribute<IModule, ushort>(c => c.Vendor);
            var productType = element.GetAttribute<IModule, ushort>(c => c.ProductType);
            var productCode = element.GetAttribute<IModule, ushort>(c => c.ProductCode);
            var major = element.Attribute("Major")?.Value;
            var minor = element.Attribute("Minor")?.Value;
            var revision = Revision.Parse($"{major}.{minor}");
            var parentModule = element.GetAttribute<IModule, string>(c => c.ParentModule);
            var parentModPortId = element.GetAttribute<IModule, int>(c => c.ParentModPortId);
            var inhibited = element.GetAttribute<Module, bool>(c => c.Inhibited);
            var majorFault = element.GetAttribute<Module, bool>(c => c.MajorFault);
            var safetyEnabled = element.GetAttribute<Module, bool>(c => c.SafetyEnabled);
            var state = element.Element("EKey")?.GetAttribute<Module, KeyingState>(c => c.State);

            var ports = element.Descendants(LogixNames.Port).Select(e =>
            {
                var serializer = new PortSerializer();
                return serializer.Deserialize(e);
            });
            
            //config
            //connection
            //input
            //output

            return new Module(name, catalogNumber!, vendor, productType, productCode, revision,
                parentModule!, parentModPortId, ports, state, inhibited, majorFault, safetyEnabled,
                description: description);
        }
    }
}