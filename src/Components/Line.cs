namespace L5Sharp.Components;

/// <summary>
/// Represents a Line of Structured Text, or the logic content that is contained by the <see cref="StRoutine"/> component.
/// </summary>
public sealed class Line
{
    /// <summary>
    /// The <c>Line</c> number or index of the line's position within it's containing <c>Routine</c>.
    /// </summary>
    public int Number { get; set; }
    
    /// <summary>
    /// The structured text of the Line.
    /// </summary>
    /// <value>A <see cref="string"/> representing the text of the line.</value>
    public string Text { get; set; }
    
    /// <inheritdoc />
    public override string ToString() => Text;
}