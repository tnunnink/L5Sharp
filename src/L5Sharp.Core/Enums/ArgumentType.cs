namespace L5Sharp.Core;

/// <summary>
/// Represents a specific type of argument that can be used within the Logix environment.
/// This class acts as a strongly typed, immutable enumeration of possible argument types.
/// </summary>
public sealed class ArgumentType : LogixEnum<ArgumentType, string>
{
    private ArgumentType(string name, string value) : base(name, value)
    {
    }

    /// <summary>
    /// Determines the <see cref="ArgumentType"/> of the provided value.
    /// </summary>
    /// <param name="value">The input string to evaluate for its argument type.</param>
    /// <returns>
    /// The <see cref="ArgumentType"/> that corresponds to the provided value.
    /// Returns <see cref="ArgumentType.Empty"/> if the input is null or empty,
    /// <see cref="ArgumentType.String"/> if the input is enclosed with single quotes,
    /// <see cref="ArgumentType.Atomic"/> if the input infers a valid radix/numeric format,
    /// <see cref="ArgumentType.Tag"/> if the input matches a valid tag name,
    /// <see cref="ArgumentType.Expression"/> if the input contains expression characters,
    /// or <see cref="ArgumentType.Unknown"/> otherwise.
    /// </returns>
    public static ArgumentType Of(string value)
    {
        if (string.IsNullOrEmpty(value)) return Empty;
        if (value.StartsWith('\'') && value.EndsWith('\'')) return String;
        if (Radix.TryInfer(value, out _)) return Atomic;
        if (TagName.IsTag(value)) return Tag;
        if (value.IndexOfAny(['=', '>', '<', '+', '-', '*', '/', '(', ')']) >= 0) return Expression;
        return Unknown;
    }

    /// <summary>
    /// Represents an argument type that is specifically empty.
    /// This value is used when no argument type has been defined or assigned.
    /// </summary>
    public static readonly ArgumentType Empty = new(nameof(Empty), nameof(Empty));

    /// <summary>
    /// Represents an unknown argument type.
    /// This value is used when the argument type cannot be determined or is unspecified.
    /// </summary>
    public static readonly ArgumentType Unknown = new(nameof(Unknown), nameof(Unknown));

    /// <summary>
    /// Represents an atomic argument type.
    /// This value is used for arguments that are indivisible or fundamental in nature.
    /// </summary>
    public static readonly ArgumentType Atomic = new(nameof(Atomic), nameof(Atomic));

    /// <summary>
    /// Represents an argument type that indicates a string value.
    /// This value is used for arguments specifically defined as textual data.
    /// </summary>
    public static readonly ArgumentType String = new(nameof(String), nameof(String));

    /// <summary>
    /// Represents an argument type that is specifically a tag.
    /// This value is used for arguments defined as a reference to a specific tag.
    /// </summary>
    public static readonly ArgumentType Tag = new(nameof(Tag), nameof(Tag));

    /// <summary>
    /// Represents an argument type that is an expression.
    /// This value is used for arguments that consist of a calculable or evaluable expression.
    /// </summary>
    public static readonly ArgumentType Expression = new(nameof(Expression), nameof(Expression));
}