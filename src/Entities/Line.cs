using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Entities;

/// <summary>
/// Represents a Line of Structured Text, or the logic content that is contained by the <see cref="StRoutine"/> component.
/// </summary>
public sealed class Line : ILogixCode
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
    
    /// <inheritdoc />
    public override string ToString() => Text;
}