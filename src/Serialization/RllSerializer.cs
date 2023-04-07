using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Components;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization;

/// <summary>
/// A logix serializer that performs serialization for the <c>Rll</c> content for a <see cref="Routine"/> component.
/// </summary>
public class RllSerializer : ILogixSerializer<IEnumerable<Rung>>
{
    private readonly RungSerializer _rungSerializer = new();
        
    /// <inheritdoc />
    public XElement Serialize(IEnumerable<Rung> obj)
    {
        Check.NotNull(obj);

        var element = new XElement(L5XName.RLLContent);
        element.Add(obj.Select(r => _rungSerializer.Serialize(r)));
        return element;
    }

    /// <inheritdoc />
    public IEnumerable<Rung> Deserialize(XElement element)
    {
        Check.NotNull(element);
            
        var rungs = element.Descendants(L5XName.Rung).Select(e => _rungSerializer.Deserialize(e));
        return new List<Rung>(rungs);
    }
}