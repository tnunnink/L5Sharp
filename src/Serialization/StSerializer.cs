using System.Linq;
using System.Xml.Linq;
using L5Sharp.Components;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization;

/// <summary>
/// A logix serializer that performs serialization of <see cref="St"/> components.
/// </summary>
public class StSerializer : ILogixSerializer<St>
{
    private readonly LineSerializer _lineSerializer = new();

    /// <inheritdoc />
    public XElement Serialize(St obj)
    {
        Check.NotNull(obj);

        Check.NotNull(obj);

        var element = new XElement(L5XName.STContent);
        element.Add(obj.Select(l => _lineSerializer.Serialize(l)));
        return element;
    }

    /// <inheritdoc />
    public St Deserialize(XElement element)
    {
        Check.NotNull(element);

        var lines = element.Descendants(L5XName.Line).Select(e => _lineSerializer.Deserialize(e));
        return new St(lines);
    }
}