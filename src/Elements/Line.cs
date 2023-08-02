using System;
using System.Xml.Linq;
using L5Sharp.Core;

namespace L5Sharp.Elements;

/// <summary>
/// A Logix <c>Line</c> element containing the properties for a L5X Line component.
/// </summary>
public sealed class Line : LogixElement
{
    /// <summary>
    /// Creates a new <see cref="Line"/> with default values.
    /// </summary>
    public Line()
    {
        Number = 0;
        Text = NeutralText.Empty;
    }

    /// <summary>
    /// Creates a new <see cref="Line"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    public Line(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The zero based number indicating the position of the <see cref="Line"/> within the containing routine.
    /// </summary>
    public int Number
    {
        get => GetValue<int>();
        set => SetValue(value);
    }

    /// <summary>
    /// The logic of the <see cref="Line"/> as a <see cref="NeutralText"/> value.
    /// </summary>
    public NeutralText Text
    {
        get => Element.Value;
        set => Element.SetValue(new XCData(value.ToString()));
    }

    /// <inheritdoc />
    public override string ToString() => Text;
}