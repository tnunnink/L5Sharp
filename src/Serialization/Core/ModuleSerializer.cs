using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Common;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;

namespace L5Sharp.Serialization.Core
{
    internal class ModuleSerializer : IXSerializer<IModule>
    {
        private static readonly XName ElementName = LogixNames.Module;

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
            
            //ports
            
            //communications
            //  config tag
            //  connections

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
            var ports = element.Descendants(LogixNames.Port).Select(e => e.Deserialize<Port>());

            throw new NotImplementedException();
        }
    }
}