using System;

namespace L5Sharp.Core;

/// <summary>
/// Represents a parsed token from neutral text, containing the token's type classification,
/// raw text value, and position information for error reporting and parsing operations.
/// </summary>
public readonly struct NeutralToken
{
    /// <summary>
    /// Creates a new NeutralToken with the specified type, value, and position information.
    /// </summary>
    /// <param name="type">The type classification of the token (e.g., Identifier, Operator).</param>
    /// <param name="value">The raw text content of the token as it appears in the source.</param>
    /// <param name="index">The zero-based starting position of the token in the original source string.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="type"/> is null.</exception>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is null.</exception>
    public NeutralToken(TokenType type, string value, int index)
    {
        Type = type ?? throw new ArgumentNullException(nameof(type));
        Value = value ?? throw new ArgumentNullException(nameof(value));
        Index = index;
    }

    /// <summary>
    /// Gets the type classification of the token, represented as an instance of <see cref="TokenType"/>.
    /// This property specifies the role or category of the token within the parsed text,
    /// such as identifier, literal, operator, or other token types.
    /// </summary>
    public TokenType Type { get; }

    /// <summary>
    /// Gets the raw text content of the token as it was parsed from the source.
    /// This property holds the exact string value of the token, which may represent
    /// identifiers, literals, operators, or other syntactical elements in the original text.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Gets the zero-based starting position of the token in the original source string.
    /// This property provides the positional information required for error reporting,
    /// diagnostics, and locating the token within the parsed text.
    /// </summary>
    public int Index { get; }

    /// <summary>
    /// Gets the total number of characters in the raw text content represented by the token.
    /// This property provides the length of the <see cref="Value"/> associated
    /// with the token, which can be useful for text processing or validation.
    /// </summary>
    public int Length => Value.Length;

    /// <summary>
    /// Returns a string representation of the token showing its type, value, and position.
    /// </summary>
    /// <returns>A formatted string in the format "[Type] Value (at Index)".</returns>
    public override string ToString() => $"[{Type.Name}] {Value} (at {Index})";

    /// <summary>
    /// Creates an end-of-file (EOF) NeutralToken with an optional position index.
    /// </summary>
    /// <param name="index">The zero-based position index where the EOF token is created. Default is -1.</param>
    /// <returns>A NeutralToken that represents the EOF, with an empty value and the specified index.</returns>
    public static NeutralToken EOF(int index = -1) => new(TokenType.EOF, string.Empty, index);
}