namespace L5Sharp.Components;

/// <summary>
/// Represents a Line of Structured Text, or the logic content that is contained by the <see cref="StRoutine"/> component.
/// </summary>
public sealed class Line
{
    /// <summary>
    /// The name of the program in which the <c>Rung</c> is contained.
    /// </summary>
    /// <value>A <see cref="string"/> representing the name of the containing program.</value>
    /// <remarks>
    /// This is only used in deserialization of a <see cref="Rung"/> component.
    /// This helper property makes it easier to filter rungs. This property is not serialized back to an L5X file.
    /// </remarks>
    public string Program { get; set; } = string.Empty;

    /// <summary>
    /// The name of the routine in which the <c>Rung</c> is contained.
    /// </summary>
    /// <value>A <see cref="string"/> representing the name of the containing routine.</value>
    /// <remarks>
    /// This is only used in deserialization of a <see cref="Rung"/> component.
    /// This helper property makes it easier to filter rungs. This property is not serialized back to an L5X file.
    /// </remarks>
    public string Routine { get; set; } = string.Empty;
    
    /// <summary>
    /// The <c>Line</c> number or index of the line's position within it's containing <c>Routine</c>.
    /// </summary>
    public int Number { get; set; }

    /// <summary>
    /// The structured text of the Line.
    /// </summary>
    /// <value>A <see cref="string"/> representing the text of the line.</value>
    public string Text { get; set; } = string.Empty;
    
    /// <inheritdoc />
    public override string ToString() => Text;
}