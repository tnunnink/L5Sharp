using L5Sharp.Core;

namespace L5Sharp.Model;

/// <summary>
/// A record structure for the <see cref="Rung"/> component.
/// </summary>
public record RungInfo()
{
    public RungInfo(Rung rung) : this()
    {
        Number = rung.Number;
        Type = rung.Type?.Name ?? RungType.Normal;
        Text = rung.Text;
        Comment = rung.Comment;
        Container = rung.Scope.Container;
        Routine = rung.Routine?.Name;
    }

    public int Number { get; init; }
    public string Type { get; init; } = RungType.Normal;
    public string Text { get; init; } = string.Empty;
    public string? Comment { get; init; }
    public string? Container { get; init; }
    public string? Routine { get; init; }

    /// <summary>
    /// Defines an implicit operator that converts a <see cref="Rung"/> instance to a <see cref="RungInfo"/> instance.
    /// </summary>
    /// <param name="rung">The <see cref="Rung"/> instance to be converted.</param>
    /// <returns>A new <see cref="RungInfo"/> instance initialized with the properties of the provided <see cref="Rung"/>.</returns>
    public static implicit operator RungInfo(Rung rung) => new(rung);
}