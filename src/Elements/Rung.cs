using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Elements;

/// <summary>
/// A Logix <c>Rung</c> component. 
/// </summary>
public sealed class Rung : LogixElement<Rung>
{
    /// <inheritdoc />
    public Rung()
    {
        Number = 0;
        Type = RungType.Normal;
        Text = NeutralText.Empty;
    }

    /// <inheritdoc />
    public Rung(XElement element) : base(element)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public int Number
    {
        get => GetValue<int>();
        set => SetValue(value);
    }

    /// <summary>
    /// The <c>Rung</c> type, indicating edit information of the rung.
    /// </summary>
    public RungType? Type
    {
        get => GetValue<RungType>();
        set => SetValue(value);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <exception cref="L5XException"></exception>
    public NeutralText Text
    {
        get => GetProperty<NeutralText>() ?? NeutralText.Empty;
        set => SetProperty(value);
    }

    /// <summary>
    /// The comment of the <c>Rung</c>.
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