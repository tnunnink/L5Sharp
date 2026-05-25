using System;
using System.Collections.Generic;
using System.Linq;

namespace L5Sharp.Core;

/// <summary>
/// Represents the type of token identified during lexical analysis of Logix neutral text.
/// Used by the internal lexer to parse Logix code structures such as instructions, tag names, and expressions.
/// </summary>
public class TokenType : LogixEnum<TokenType, string>
{
    /// <summary>
    /// Represents a collection of token types corresponding to various operators
    /// used within the Logix neutral text framework. This collection aggregates
    /// all available operator tokens from the core operator definitions.
    /// </summary>
    private static readonly HashSet<string> Operators =
        new(Core.Operator.All().Select(x => x.Value), StringComparer.OrdinalIgnoreCase);

    private TokenType(string name, string value) : base(name, value)
    {
    }

    /// <summary>
    /// Represents a token type that indicates the absence of a token or a null state.
    /// This token type can be used as a default or uninitialized value in scenarios
    /// where no specific token type is applicable or has been defined.
    /// </summary>
    public static readonly TokenType None = new(nameof(None), nameof(None));

    /// <summary>
    /// Represents an undefined or unrecognized token type encountered during lexical analysis of Logix neutral text.
    /// Typically used as a placeholder when a token does not match any predefined token types.
    /// </summary>
    public static readonly TokenType Unknown = new(nameof(Unknown), nameof(Unknown));

    /// <summary>
    /// Represents an identifier token such as instruction names (XIC, ADD), tag names (MyTag), or AOI names (My_AOI).
    /// </summary>
    public static readonly TokenType Identifier = new(nameof(Identifier), nameof(Identifier));

    /// <summary>
    /// Represents a literal value token such as numeric literals (100, 16#FF) or string literals ('String').
    /// </summary>
    public static readonly TokenType Literal = new(nameof(Literal), nameof(Literal));

    /// <summary>
    /// Represents an operator token such as arithmetic (+, -, *, /), assignment (:=), or logical (AND, OR) operators.
    /// </summary>
    public static readonly TokenType Operator = new(nameof(Operator), nameof(Operator));

    /// <summary>
    /// Represents an opening parenthesis token '(' used for instruction arguments and expression grouping.
    /// </summary>
    public static readonly TokenType OpenParen = new(nameof(OpenParen), nameof(OpenParen));

    /// <summary>
    /// Represents a closing parenthesis token ')' used for instruction arguments and expression grouping.
    /// </summary>
    public static readonly TokenType CloseParen = new(nameof(CloseParen), nameof(CloseParen));

    /// <summary>
    /// Represents an opening bracket token '[' used for array indexing and branch logic in rungs.
    /// </summary>
    public static readonly TokenType OpenBracket = new(nameof(OpenBracket), nameof(OpenBracket));

    /// <summary>
    /// Represents a closing bracket token ']' used for array indexing and branch logic in rungs.
    /// </summary>
    public static readonly TokenType CloseBracket = new(nameof(CloseBracket), nameof(CloseBracket));

    /// <summary>
    /// Represents a comma token ',' used to separate instruction arguments or array dimensions.
    /// </summary>
    public static readonly TokenType Comma = new(nameof(Comma), nameof(Comma));

    /// <summary>
    /// Represents a dot token '.' used for member access in tag names and data structures.
    /// </summary>
    public static readonly TokenType Dot = new(nameof(Dot), nameof(Dot));

    /// <summary>
    /// Represents a token type corresponding to a colon (":") character in the Logix neutral text.
    /// </summary>
    public static readonly TokenType Colon = new(nameof(Colon), nameof(Colon));

    /// <summary>
    /// Represents a semicolon token ';' used to terminate instructions or rungs.
    /// </summary>
    public static readonly TokenType SemiColon = new(nameof(SemiColon), nameof(SemiColon));

    /// <summary>
    /// Represents a token type that corresponds to a question mark symbol ('?'). This symbol is found in some
    /// instruction text like timers or counters for unspecified arguments.
    /// </summary>
    public static readonly TokenType QuestionMark = new(nameof(QuestionMark), nameof(QuestionMark));

    /// <summary>
    /// Represents a token type that corresponds to comment notation within the Logix neutral text framework.
    /// This token is used to identify and parse comments in the input source during processing.
    /// </summary>
    public static readonly TokenType Comment = new(nameof(Comment), nameof(Comment));

    /// <summary>
    /// Represents the end-of-file token indicating the completion of input text parsing.
    /// </summary>
    public static readonly TokenType EOF = new(nameof(EOF), nameof(EOF));

    /// <summary>
    /// Determines the <see cref="TokenType"/> based on the given token string.
    /// </summary>
    /// <param name="token">The token string to be evaluated.</param>
    /// <returns>The determined <see cref="TokenType"/> of the provided token string.</returns>
    public static TokenType FromToken(string token)
    {
        if (string.IsNullOrEmpty(token) || token.Length == 0)
            return EOF;

        // Handle all known operator tokens first. These include math symbols and binary operators.
        if (Operators.Contains(token))
            return Operator;

        return token[0] switch
        {
            // Special end-of-line character.
            char.MinValue => EOF,

            // Known single character structure tokens
            '(' when token.Length == 1 => OpenParen,
            ')' when token.Length == 1 => CloseParen,
            '[' when token.Length == 1 => OpenBracket,
            ']' when token.Length == 1 => CloseBracket,
            ',' when token.Length == 1 => Comma,
            '.' when token.Length == 1 => Dot,
            ':' when token.Length == 1 => Colon,
            ';' when token.Length == 1 => SemiColon,
            '?' when token.Length == 1 => QuestionMark,

            // Verify balanced quotes for string literals
            '\'' when token.Length >= 2 && token[token.Length - 1] == '\'' => Literal,

            // Literals start with a digit (100, 0.123, 16#FF, 2#1011, etc.)
            _ when char.IsDigit(token[0]) => Literal,

            // Identifiers start with a letter or underscore
            _ when char.IsLetter(token[0]) || token[0] == '_' => Identifier,

            _ => Unknown
        };
    }
}