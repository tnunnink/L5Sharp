using System.Linq;
using System.Xml.Linq;
using L5Sharp.Components;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization;

/// <summary>
/// A logix serializer that performs serialization of <see cref="Line"/> components.
/// </summary>
public class LineSerializer : ILogixSerializer<Line>
{
    /// <inheritdoc />
    public XElement Serialize(Line obj)
    {
        Check.NotNull(obj);

        var element = new XElement(L5XName.Line);

        element.AddValue(obj, r => r.Number);
        element.Add(new XCData(obj.Text));

        return element;
    }

    /// <inheritdoc />
    public Line Deserialize(XElement element)
    {
        Check.NotNull(element);

        return new Line
        {
            Program = element.Ancestors(L5XName.Program).FirstOrDefault()?.LogixName() ?? string.Empty,
            Routine = element.Ancestors(L5XName.Routine).FirstOrDefault()?.LogixName() ?? string.Empty,
            Number = element.GetValue<int>(L5XName.Number),
            Text = element.Value
        };
    }
}