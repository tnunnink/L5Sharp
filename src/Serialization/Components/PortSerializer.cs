using System;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.L5X;

namespace L5Sharp.Serialization.Components
{
    /// <summary>
    /// A <see cref="IL5XSerializer{T}"/> for the <see cref="Port"/> component.
    /// </summary>
    internal class PortSerializer : L5XSerializer<Port>
    {
        private static readonly XName ElementName = L5XElement.Port.ToString();

        /// <inheritdoc />
        public override XElement Serialize(Port component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);
            element.Add(new XAttribute(L5XAttribute.Id.ToString(), component.Id));
            element.Add(new XAttribute(L5XAttribute.Address.ToString(), component.Address));
            element.Add(new XAttribute(L5XAttribute.Type.ToString(), component.Type));
            element.Add(new XAttribute(L5XAttribute.Upstream.ToString(), component.Upstream));

            if (component.Upstream) return element;

            var bus = new XElement(L5XElement.Bus.ToString());
            if (component.BusSize > 0)
                bus.Add(new XAttribute(L5XAttribute.Size.ToString(), component.BusSize));
            element.Add(bus);

            return element;
        }

        /// <inheritdoc />
        public override Port Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");

            var id = element.Attribute(L5XAttribute.Id.ToString())?.Value.Parse<int>() ?? default;
            var address = element.Attribute(L5XAttribute.Address.ToString())?.Value;
            var type = element.Attribute(L5XAttribute.Type.ToString())?.Value!;
            var upstream = element.Attribute(L5XAttribute.Upstream.ToString())?.Value.Parse<bool>() ?? default;

            var busSize = element.Element(L5XElement.Bus.ToString())
                ?.Attribute(L5XAttribute.Size.ToString())
                ?.Value.Parse<byte>() ?? default;

            return new Port(id, type, address, upstream, busSize: busSize);
        }
    }
}