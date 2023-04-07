using System.Xml.Linq;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization;

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
            Address = element.TryGetValue<Address>(L5XName.Address) ?? Address.None,
            Type = element.TryGetValue<string>(L5XName.Type) ?? string.Empty,
            Upstream = element.TryGetValue<bool?>(L5XName.Upstream) ?? false,
            BusSize = element.Element(L5XName.Bus)?.TryGetValue<byte?>(L5XName.Size) ?? default
        };
    }
}