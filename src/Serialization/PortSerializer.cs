using System;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Extensions;
using L5Sharp.Helpers;

namespace L5Sharp.Serialization
{
    /// <summary>
    /// A <see cref="IXSerializer{T}"/> for the <see cref="Port"/> component.
    /// </summary>
    internal class PortSerializer : IXSerializer<Port>
    {
        private readonly LogixContext _context;
        private static readonly XName ElementName = LogixNames.Port;

        public PortSerializer(LogixContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public XElement Serialize(Port component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);
            element.AddAttribute(component, c => c.Id);
            element.AddAttribute(component, c => c.Address);
            element.AddAttribute(component, c => c.Type);
            element.AddAttribute(component, c => c.Upstream);

            if (component.Bus is null) return element;

            var bus = new XElement(nameof(component.Bus));
            bus.AddAttribute(component, c => c.Bus!.Size, p => p.Bus is not null);
            element.Add(bus);

            return element;
        }

        /// <inheritdoc />
        public Port Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element name '{element.Name}' invalid. Expecting '{ElementName}'");

            var id = element.GetAttribute<Port, int>(c => c.Id);
            var address = element.GetAttribute<Port, string>(c => c.Address) ?? string.Empty;
            var type = element.GetAttribute<Port, string>(c => c.Type) ?? string.Empty;
            var upstream = element.GetAttribute<Port, bool>(c => c.Upstream);

            var busElement = element.Element(LogixNames.Bus);

            if (busElement is null)
                return new Port(id, address, type, upstream);

            var serializer = new BusSerializer(_context);
            var bus = serializer.Deserialize(busElement);

            return new Port(id, address, type, upstream, bus);
        }
    }
}