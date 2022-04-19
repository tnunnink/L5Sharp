using System;
using System.Xml.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Core;
using L5Sharp.L5X;

namespace L5Sharp.Serialization
{
    /// <summary>
    /// A <see cref="IL5XSerializer{T}"/> for the <see cref="Port"/> component.
    /// </summary>
    internal class PortSerializer : L5XSerializer<Port>
    {
        private static readonly XName ElementName = L5XName.Port;

        /// <inheritdoc />
        public override XElement Serialize(Port component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);
            element.Add(new XAttribute(L5XName.Id, component.Id));
            element.Add(new XAttribute(L5XName.Address, component.Address));
            element.Add(new XAttribute(L5XName.Type, component.Type));
            element.Add(new XAttribute(L5XName.Upstream, component.Upstream));

            if (component.Upstream) return element;

            var bus = new XElement(L5XName.Bus);
            if (component.BusSize > 0)
                bus.Add(new XAttribute(L5XName.Size, component.BusSize));
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

            var id = element.Attribute(L5XName.Id)?.Value.Parse<int>() ?? default;
            var address = element.Attribute(L5XName.Address)?.Value.Parse<PortAddress>();
            var type = element.Attribute(L5XName.Type)?.Value!;
            var upstream = element.Attribute(L5XName.Upstream)?.Value.Parse<bool>() ?? default;

            var busSize = element.Element(L5XName.Bus)
                ?.Attribute(L5XName.Size)
                ?.Value.Parse<byte>() ?? default;

            return new Port(id, type, address, upstream, busSize);
        }
    }
}