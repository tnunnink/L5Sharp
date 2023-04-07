using System.Linq;
using System.Xml.Linq;
using L5Sharp.Components;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization;

/// <summary>
/// A logix serializer that performs serialization of <see cref="QuickWatchList"/> components.
/// </summary>
public class QuickWatchListSerializer : ILogixSerializer<QuickWatchList>
{
    /// <inheritdoc />
    public XElement Serialize(QuickWatchList obj)
    {
        Check.NotNull(obj);

        var element = new XElement(L5XName.QuickWatchList);

        element.AddValue(obj, p => p.Name);

        var tags = new XElement(L5XName.WatchTag);
        tags.Add(obj.Select(t =>
            new XElement(L5XName.WatchTag,
                new XAttribute(L5XName.Specifier, t.Specifier),
                new XAttribute(L5XName.Scope, t.Scope))));
        element.Add(tags);

        return element;
    }

    /// <inheritdoc />
    public QuickWatchList Deserialize(XElement element)
    {
        Check.NotNull(element);

        var tags = element.Descendants(L5XName.WatchTag)
            .Select(t => new WatchTag
            {
                Specifier = t.TryGetValue<string>(L5XName.Specifier) ?? string.Empty,
                Scope = t.TryGetValue<string>(L5XName.Specifier) ?? string.Empty
            }).ToList();

        return new QuickWatchList(tags);
    }
}