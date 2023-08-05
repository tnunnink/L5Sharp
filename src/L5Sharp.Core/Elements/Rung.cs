using System;
using System.Xml.Linq;
using L5Sharp.Common;
using L5Sharp.Enums;

namespace L5Sharp.Elements;

/// <summary>
/// A Logix <c>Rung</c> element containing the properties for a L5X Rung component.
/// </summary>
public sealed class Rung : LogixElement
{
    /// <summary>
    /// Creates a new <see cref="Rung"/> with default values.
    /// </summary>
    public Rung()
    {
        Number = 0;
        Type = RungType.Normal;
        Text = NeutralText.Empty;
    }

    /// <summary>
    /// Creates a new <see cref="Rung"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    public Rung(XElement element) : base(element)
    {
    }

    /// <summary>
    /// Creates a new <see cref="Rung"/> initialized with the provided <see cref="NeutralText"/>.
    /// </summary>
    /// <param name="text">The <see cref="NeutralText"/> representing the rung logic.</param>
    /// <param name="comment">The optional string comment of the rung. Default is <c>null</c> (no comment).</param>
    /// <remarks>This will initialize <see cref="Number"/> to '0' and <see cref="Type"/> to 'Normal'.
    /// When importing, Logix ignores the rung number and imports Rung's in order of the container sequence,
    /// meaning, its really only necessary to specify valid text, which is why this constructor is available,
    /// allowing concise construction of a <c>Rung</c> object.</remarks>
    public Rung(NeutralText text, string? comment = null)
    {
        Number = 0;
        Type = RungType.Normal;
        Text = text;
        Comment = comment;
    }

    /// <summary>
    /// The zero based number indicating the position of the <see cref="Rung"/> within the containing routine.
    /// </summary>
    public int Number
    {
        get => GetValue<int>();
        set => SetValue(value);
    }

    /// <summary>
    /// The <see cref="RungType"/>, indicating edit state option of the rung.
    /// </summary>
    public RungType? Type
    {
        get => GetValue<RungType>();
        set => SetValue(value);
    }

    /// <summary>
    /// The logic of the <see cref="Rung"/> as a <see cref="NeutralText"/> value.
    /// </summary>
    public NeutralText Text
    {
        get => GetProperty<NeutralText>() ?? NeutralText.Empty;
        set => SetProperty(value);
    }

    /// <summary>
    /// The text comment of the <see cref="Rung"/>.
    /// </summary>
    /// <value>A <see cref="string"/> containing the text comment of the Rung.</value>
    public string? Comment
    {
        get => GetProperty<string>();
        set => SetProperty(value);
    }

    /// <inheritdoc />
    public override string ToString() => Text;
}