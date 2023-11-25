namespace L5Sharp.Enums;

/// <summary>
/// This is a library construct to help define the direction of a parameter for an instruction of function block.
/// </summary>
public sealed class Direction : LogixEnum<Direction, string>
{
    private Direction(string name, string value) : base(name, value)
    {
    }
    
    /// <summary>
    /// Indicates that a Direction is an <c>Input</c> <see cref="Direction"/>.
    /// </summary>
    /// <value>A <see cref="Direction"/> option.</value>
    public static readonly Direction Input = new(nameof(Input), nameof(Input));
    
    /// <summary>
    /// Indicates that a Direction is an <c>Output</c> <see cref="Direction"/>.
    /// </summary>
    /// <value>A <see cref="Direction"/> option.</value>
    public static readonly Direction Output = new(nameof(Output), nameof(Output));
}