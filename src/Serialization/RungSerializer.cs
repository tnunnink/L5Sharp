using System.Linq;
using System.Xml.Linq;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization;

/// <summary>
/// A logix serializer that performs serialization of <see cref="Rung"/> components.
/// </summary>
public class RungSerializer : ILogixSerializer<Rung>
{
    /// <inheritdoc />
    public XElement Serialize(Rung obj)
    {
        Check.NotNull(obj);

        var element = new XElement(L5XName.Rung);

        element.AddValue(obj, r => r.Number);
        element.AddValue(obj, r => r.Type);
        element.AddText(obj, r => r.Comment);
        element.AddText(obj.Text.ToString(), L5XName.Text);
            
        return element;
    }

    /// <inheritdoc />
    public Rung Deserialize(XElement element)
    {
        Check.NotNull(element);

        return new Rung
        {
            Container = element.Ancestors(L5XName.Program).FirstOrDefault()?.LogixName() ?? string.Empty,
            Routine = element.Ancestors(L5XName.Routine).FirstOrDefault()?.LogixName() ?? string.Empty,
            Number = element.GetValue<int>(L5XName.Number),
            Type = element.TryGetValue<RungType>(L5XName.Type) ?? RungType.Normal,
            Comment = element.Element(L5XName.Comment)?.Value ?? string.Empty,
            Text = element.Element(L5XName.Text)?.Value ?? NeutralText.Empty
        };
    }
}