using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;

namespace L5Sharp.Elements;

/// <summary>
/// Represents a Line of Structured Text, or the logic content that is contained by the <see cref="StRoutine"/> component.
/// </summary>
public sealed class Line : LogixElement<Rung>, ILogixCode
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

    /// <inheritdoc />
    public Scope Scope => Scope.FromElement(Element);

    /// <inheritdoc />
    public string Container => Element.Ancestors(L5XName.Program).FirstOrDefault()?.LogixName() ?? string.Empty;

    /// <inheritdoc />
    public string Routine => Element.Ancestors(L5XName.Routine).FirstOrDefault()?.LogixName() ?? string.Empty;

    /// <inheritdoc />
    public int Number
    {
        get => GetValue<int>();
        set => SetValue(value);
    }

    /// <inheritdoc />
    public NeutralText Text
    {
        get => Element.Value;
        set => Element.SetValue(new XCData(value.ToString()));
    }

    /// <inheritdoc />
    public override string ToString() => Text;
}