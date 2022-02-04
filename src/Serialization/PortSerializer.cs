using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Extensions;
using L5Sharp.Helpers;

namespace L5Sharp.Serialization
{
    /// <summary>
    /// A <see cref="IXSerializer{T}"/> for the <see cref="Port"/> component.
    /// </summary>
    internal class PortSerializer : IXSerializer<PortDefinition>
    {
        private static readonly XName ElementName = LogixNames.Port;

        /// <inheritdoc />
        public XElement Serialize(PortDefinition component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);
            element.AddAttribute(component, c => c.Id);
            element.AddAttribute(component, c => c.Address);
            element.AddAttribute(component, c => c.Type);
            element.AddAttribute(component, c => c.Upstream);

            if (component.Upstream) return element;
            
            var bus = new XElement(LogixNames.Bus);
            bus.AddAttribute(component, c => c.BusSize, p => p.BusSize > 0);
            element.Add(bus);
            
            return element;
        }

        /// <inheritdoc />
        public PortDefinition Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element name '{element.Name}' invalid. Expecting '{ElementName}'");

            var id = element.GetAttribute<Port, int>(c => c.Id);
            var address = element.GetAttribute<Port, string>(c => c.Address) ?? string.Empty;
            var type = element.GetAttribute<Port, string>(c => c.Type) ?? string.Empty;
            var upstream = element.GetAttribute<Port, bool>(c => c.Upstream);
            int.TryParse(element.Element(LogixNames.Bus)?.Attribute("Size")?.Value, out var size);
            
            return new PortDefinition(id, type, upstream, address, size);
        }
    }
}