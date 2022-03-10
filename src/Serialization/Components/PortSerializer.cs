using System;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Extensions;
using L5Sharp.L5X;

namespace L5Sharp.Serialization.Components
{
    /// <summary>
    /// A <see cref="IL5XSerializer{T}"/> for the <see cref="Port"/> component.
    /// </summary>
    internal class PortSerializer : IL5XSerializer<PortDefinition>
    {
        private static readonly XName ElementName = L5XElement.Port.ToString();

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
            
            var bus = new XElement(L5XElement.Bus.ToString());
            bus.AddAttribute(component, c => c.BusSize, p => p.BusSize > 0, "Size");
            element.Add(bus);
            
            return element;
        }

        /// <inheritdoc />
        public PortDefinition Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");

            var id = element.GetAttribute<Port, int>(c => c.Id);
            var address = element.GetAttribute<Port, string>(c => c.Address) ?? string.Empty;
            var type = element.GetAttribute<Port, string>(c => c.Type) ?? string.Empty;
            var upstream = element.GetAttribute<Port, bool>(c => c.Upstream);
            var busSize = element.Element(L5XElement.Bus.ToString())?.GetAttribute<Bus, byte>(b => b.Size) ?? 0;
            
            return new PortDefinition(id, type, upstream, address, busSize);
        }
    }
}