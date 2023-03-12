using System.Linq;
using System.Xml.Linq;
using L5Sharp.Components;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization;

/// <summary>
/// A logix serializer that performs serialization of <see cref="StRoutine"/> components.
/// </summary>
public class StRoutineSerializer : ILogixSerializer<StRoutine>
{
    /// <inheritdoc />
    public XElement Serialize(StRoutine obj)
    {
        Check.NotNull(obj);

        var element = new XElement(L5XName.Routine);

        element.AddValue(obj, r => r.Name);
        element.AddText(obj, r => r.Description);
        element.AddValue(obj, r => r.Type);

        var content = new XElement(L5XName.STContent);
        content.Add(obj.Content.Select(l =>
            new XElement(L5XName.Line, new XAttribute(L5XName.Number, l.Number), new XCData(l.Text))));
        element.Add(content);

        return element;
    }

    /// <inheritdoc />
    public StRoutine Deserialize(XElement element)
    {
        Check.NotNull(element);

        return new StRoutine
        {
            Name = element.LogixName(),
            Description = element.LogixDescription(),
            Content = element.Descendants(L5XName.Line)
                .Select(e => new Line { Number = e.GetValue<int>(L5XName.Number), Text = e.Value }).ToList()
        };
    }
}