using System.Xml.Linq;
using L5Sharp.Core;

namespace L5Sharp.Elements;

/// <summary>
/// Represents a Line of Structured Text,
/// </summary>
public sealed class Line : LogixElement<Line>
{
    /// <inheritdoc />
    public Line()
    {
        Number = 0;
        Text = NeutralText.Empty;
    }

    /// <inheritdoc />
    public Line(XElement element) : base(element)
    {
    }

    
    public int Number
    {
        get => GetValue<int>();
        set => SetValue(value);
    }

    
    public NeutralText Text
    {
        get => Element.Value;
        set => Element.SetValue(new XCData(value.ToString()));
    }

    /// <inheritdoc />
    public override string ToString() => Text;
}