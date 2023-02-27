using System.Xml.Linq;
using L5Sharp.Common;
using L5Sharp.Core;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization
{
    /// <summary>
    /// A logix serializer that performs serialization of <see cref="Port"/> components.
    /// </summary>
    public class PortSerializer : ILogixSerializer<Port>
    {
        /// <inheritdoc />
        public XElement Serialize(Port obj)
        {
            Check.NotNull(obj);

            var element = new XElement(typeof(Port).GetLogixName());
            
            element.AddValue(obj, p => p.Id);
            element.AddValue(obj, p => p.Address);
            element.AddValue(obj, p => p.Type);
            element.AddValue(obj, p => p.Upstream);

            if (obj.Upstream) return element;

            var bus = new XElement(L5XName.Bus);
            if (obj.BusSize > 0)
                bus.Add(new XAttribute(L5XName.Size, obj.BusSize));
            element.Add(bus);

            return element;
        }

        /// <inheritdoc />
        public Port Deserialize(XElement element)
        {
            Check.NotNull(element);
            
            return new Port
            {
                Id = element.GetValue<int>(L5XName.Id),
                Address = element.GetValue<Address>(L5XName.Address),
                Type = element.GetValue<string>(L5XName.Type),
                Upstream = element.GetValue<bool>(L5XName.Upstream),
                BusSize = element.Element(L5XName.Bus)?.GetValue<byte>(L5XName.Size) ?? default
            };
        }
    }
}