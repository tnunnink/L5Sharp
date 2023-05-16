using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Serialization;

namespace L5Sharp.Components;

/// <summary>
/// A Logix <c>Rung</c> component. This entity type contains the properties for a rung of ladder logic which are
/// elements of the <c>Rll</c> routine type. This type also implements <see cref="ILogixCode"/>.
/// </summary>
[LogixSerializer(typeof(RungSerializer))]
public sealed class Rung : ILogixCode
{
    /// <inheritdoc />
    public Scope Scope { get; set; } = Scope.Null;

    /// <inheritdoc />
    public string Container { get; set; } = string.Empty;
        
    /// <inheritdoc />
    public string Routine { get; set; } = string.Empty;

    /// <inheritdoc />
    public int Number { get; set; }

    /// <inheritdoc />
    public NeutralText Text { get; set; } = NeutralText.Empty;

    /// <summary>
    /// The <c>Rung</c> type, indicating edit information of the rung.
    /// </summary>
    public RungType Type { get; set; } = RungType.Normal;

    /// <summary>
    /// The comment of the <c>Rung</c>.
    /// </summary>
    /// <value>A <see cref="string"/> containing the text comment of the Rung.</value>
    public string Comment { get; set; } = string.Empty;

    /// <inheritdoc />
    public override string ToString() => Text;
}