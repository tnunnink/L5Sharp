namespace L5Sharp.Enums;

/// <summary>
/// An enumeration of known Logix diagram element types found in a FBD or SFC routine.
/// </summary>
public sealed class ParameterType : LogixEnum<ParameterType, string>
{
    private ParameterType(string name, string value) : base(name, value)
    {
    }

    
    /// <summary>
    /// Indicates that a parameter is an <c>Input</c> <see cref="ParameterType"/>.
    /// </summary>
    /// <value>A <see cref="ParameterType"/> option.</value>
    public static readonly ParameterType Input = new(nameof(Input), nameof(Input));
    
    /// <summary>
    /// Indicates that a parameter is an <c>Output</c> <see cref="ParameterType"/>.
    /// </summary>
    /// <value>A <see cref="ParameterType"/> option.</value>
    public static readonly ParameterType Output = new(nameof(Output), nameof(Output));
}