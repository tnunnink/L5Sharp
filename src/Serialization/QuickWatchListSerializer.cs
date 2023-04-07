using System.Xml.Linq;
using L5Sharp.Components;

namespace L5Sharp.Serialization;

/// <summary>
/// A logix serializer that performs serialization of <see cref="QuickWatchList"/> components.
/// </summary>
public class QuickWatchListSerializer : ILogixSerializer<QuickWatchList>
{
    /// <inheritdoc />
    public XElement Serialize(QuickWatchList obj)
    {
        throw new System.NotImplementedException();
    }

    /// <inheritdoc />
    public QuickWatchList Deserialize(XElement element)
    {
        throw new System.NotImplementedException();
    }
}