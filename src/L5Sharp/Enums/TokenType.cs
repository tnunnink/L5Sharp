namespace L5Sharp.Enums;

/// <summary>
/// A set of all known token types for a <c>NeutralText</c> string. This is not an L5X type but an enum that is used
/// by the library to parse a <c>NeutralText</c> string into a <see cref="Token"/> collection.
/// </summary>
public sealed class TokenType : LogixEnum<TokenType, string>
{
    private TokenType(string name, string value) : base(name, value)
    {
    }

    /// <summary>
    /// Represents a <c>Keyword</c> token type.
    /// </summary>
    public static readonly TokenType Keyword = new(nameof(Keyword), nameof(Keyword));
    
    /// <summary>
    /// Represents a <c>Instruction</c> token type.
    /// </summary>
    public static readonly TokenType Instruction = new(nameof(Instruction), nameof(Instruction));
    
    /// <summary>
    /// Represents a <c>Operator</c> token type.
    /// </summary>
    public static readonly TokenType Operator = new(nameof(Operator), nameof(Operator));
    
    /// <summary>
    /// Represents a <c>TagName</c> token type.
    /// </summary>
    public static readonly TokenType TagName = new(nameof(TagName), nameof(TagName));
    
    /// <summary>
    /// Represents a <c>Immediate</c> token type.
    /// </summary>
    public static readonly TokenType Immediate = new(nameof(Immediate), nameof(Immediate));
}