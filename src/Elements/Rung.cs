using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;

namespace L5Sharp.Elements;

/// <summary>
/// A Logix <c>Rung</c> component. This entity type contains the properties for a rung of ladder logic which are
/// elements of the <c>Rll</c> routine type. This type also implements <see cref="ILogixCode"/>.
/// </summary>
public sealed class Rung : LogixElement<Rung>, ILogixCode
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
        get => GetProperty<NeutralText>() ?? throw new L5XException(Element);
        set => SetProperty(value);
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