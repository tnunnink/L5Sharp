using System;
using System.Collections.Generic;

namespace L5Sharp.Core;

/// <summary>
/// Represents case-insensitive text that can be tokenized into neutral tokens for parsing operations.
/// This class provides string comparison using ordinal case-insensitive rules and supports tokenization
/// of Logix programming language syntax, including operators, identifiers, literals, and structural elements.
/// </summary>
public class NeutralText
{
    /// <summary>
    /// The underlying text value stored in this NeutralText instance.
    /// </summary>
    private readonly string _text;

    /// <summary>
    /// Initializes a new instance of the <see cref="NeutralText"/> class with the specified text value.
    /// </summary>
    /// <param name="text">The text value to wrap. Cannot be null.</param>
    public NeutralText(string text)
    {
        _text = text ?? throw new ArgumentNullException(nameof(text));
    }

    /// <summary>
    /// Tokenizes the text into a sequence of neutral tokens representing operators, identifiers, literals,
    /// structural elements, and string literals. Whitespace is ignored during tokenization.
    /// </summary>
    /// <returns>An enumerable sequence of <see cref="NeutralToken"/> instances representing the parsed tokens,
    /// terminated with an EOF token.</returns>
    /// <exception cref="ArgumentException">Thrown when an unrecognized character is encountered during tokenization.</exception>
    public IEnumerable<NeutralToken> Tokenize()
    {
        var position = 0;

        while (position < _text.Length)
        {
            var current = _text[position];

            if (char.IsWhiteSpace(current))
            {
                position++;
                continue;
            }

            var token = current switch
            {
                // Handle comments first
                '/' when PeekNext(_text, position) is '/' => ConsumeComment(_text, ref position),
                '/' when PeekNext(_text, position) is '*' => ConsumeComment(_text, ref position),
                // Handle special case 2 character operators
                ':' when PeekNext(_text, position) is '=' => Consume(_text, ref position, 2),
                '<' or '>' when PeekNext(_text, position) is '=' => Consume(_text, ref position, 2),
                '<' when PeekNext(_text, position) is '>' => Consume(_text, ref position, 2),
                '*' when PeekNext(_text, position) is '*' => Consume(_text, ref position, 2),
                // Handle all other single character operators
                _ when IsOperator(current) => Consume(_text, ref position),
                _ when IsStructural(current) => Consume(_text, ref position),
                _ when IsString(current) => ConsumeString(_text, ref position),
                _ when char.IsDigit(current) => ConsumeWhile(_text, ref position, IsLiteral),
                _ when char.IsLetter(current) || current is '_' => ConsumeWhile(_text, ref position, IsIdentifier),
                _ => throw new ArgumentException(
                    $"Unexpected character '{current}' at position {position} of text: {_text}")
            };

            yield return token;
        }

        yield return new NeutralToken(TokenType.EOF, string.Empty, position);
        yield break;

        bool IsOperator(char c) => c is '+' or '-' or '/' or '*' or '=' or '<' or '>';
        bool IsStructural(char c) => c is '(' or ')' or '[' or ']' or ',' or '.' or ':' or ';' or '?';
        bool IsString(char c) => c is '\'';
        bool IsLiteral(char c) => char.IsLetterOrDigit(c) || c is '#' or '.';
        bool IsIdentifier(char c) => char.IsLetterOrDigit(c) || c is '_';
    }

    /// <summary>
    /// Consumes a specified number of characters from the given text, starting at the current position,
    /// and creates a <see cref="NeutralToken"/> representing the consumed text.
    /// </summary>
    /// <param name="text">The source text to consume characters from.</param>
    /// <param name="position">A reference to the current position within the text, updated as characters are consumed.</param>
    /// <param name="count">The number of characters to consume, with a default value of 1.</param>
    /// <returns>A <see cref="NeutralToken"/> representing the consumed text and its type.</returns>
    private static NeutralToken Consume(string text, ref int position, int count = 1)
    {
        var start = position;

        while (position < text.Length && position - start < count)
            position++;

        var token = text.Substring(start, position - start);
        var type = TokenType.FromToken(token);
        return new NeutralToken(type, token, start);
    }

    /// <summary>
    /// Consumes characters from the input text starting from the given position until the specified condition is no longer met,
    /// and returns a parsed <see cref="NeutralToken"/>.
    /// </summary>
    /// <param name="text">The input text being parsed.</param>
    /// <param name="position">
    /// A reference to the current index within the <paramref name="text"/>. This value will be updated to point to the position
    /// immediately after the last character consumed.
    /// </param>
    /// <param name="condition">
    /// A function that determines the condition for consuming characters. Characters will continue to be consumed
    /// as long as this function returns <see langword="true"/>.
    /// </param>
    /// <returns>
    /// A <see cref="NeutralToken"/> representing the consumed characters, including their type, value, and position within the text.
    /// </returns>
    private static NeutralToken ConsumeWhile(string text, ref int position, Func<char, bool> condition)
    {
        var start = position;

        while (position < text.Length && condition.Invoke(text[position]))
            position++;

        var token = text.Substring(start, position - start);
        var type = TokenType.FromToken(token);
        return new NeutralToken(type, token, start);
    }

    /// <summary>
    /// Processes a substring within the provided text, starting from the current position, and consumes characters
    /// until the closing quote of a string literal is reached.
    /// </summary>
    /// <param name="text">The input text from which the string is consumed. Cannot be null.</param>
    /// <param name="position">
    /// A reference to the current position in the text being processed. The position will be updated to point
    /// to the next character after the closing quote upon method completion.
    /// </param>
    /// <returns>A <see cref="NeutralToken"/> representing the consumed string literal, including its type, value, and starting index.</returns>
    private static NeutralToken ConsumeString(string text, ref int position)
    {
        var start = position;
        position++; // Consume opening quote before detecting closing quote

        while (position < text.Length)
        {
            // Closing quote without a previous escape character is the terminal position for the string.
            if (text[position] is '\'' && text[position - 1] is not '$')
            {
                position++;
                break;
            }

            position++;
        }

        var token = text.Substring(start, position - start);
        return new NeutralToken(TokenType.Literal, token, start);
    }

    /// <summary>
    /// Consumes a comment from the provided text starting at the given position.
    /// Supports both single-line ('//') and multi-line ('/* */') comment formats.
    /// </summary>
    /// <param name="text">The text to parse, containing the comment. Cannot be null or empty.</param>
    /// <param name="position">
    /// The current position in the text where the comment begins.
    /// This value will be updated to reflect the position after the comment is consumed.
    /// </param>
    /// <returns>
    /// A <see cref="NeutralToken"/> representing the consumed comment, including its type, value, and starting index.
    /// </returns>
    private static NeutralToken ConsumeComment(string text, ref int position)
    {
        var isMultiLine = text[position + 1] == '*';
        var start = position;
        position += 2; // skip /*

        while (position < text.Length)
        {
            if (isMultiLine && text[position] is '*' && PeekNext(text, position) is '/')
            {
                position += 2;
                break;
            }

            if (!isMultiLine && text[position] is '\n' or '\r')
            {
                position++;
                break;
            }

            position++;
        }

        var token = text.Substring(start, position - start);
        return new NeutralToken(TokenType.Comment, token, start);
    }

    /// <summary>
    /// Retrieves the character at the specified position plus one in the given text,
    /// or returns the minimum value of the <see cref="char"/> type if the index is out of range.
    /// </summary>
    /// <param name="index">The base zero position of the character to peek at.</param>
    /// <param name="text">The string from which the character is to be retrieved.</param>
    /// <returns>The character at the position <paramref name="index"/> + 1 in the given string,
    /// or <see cref="char.MinValue"/> if the position is out of the text's bounds.</returns>
    private static char PeekNext(string text, int index) => index + 1 < text.Length ? text[index + 1] : char.MinValue;


    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj switch
        {
            string text => StringComparer.OrdinalIgnoreCase.Equals(_text, text),
            NeutralText other => StringComparer.OrdinalIgnoreCase.Equals(_text, other._text),
            _ => false
        };
    }

    /// <inheritdoc />
    public override int GetHashCode() => StringComparer.OrdinalIgnoreCase.GetHashCode(_text);

    /// <inheritdoc />
    public override string ToString() => _text;

    /// <summary>
    /// Implicitly converts a <see cref="NeutralText"/> instance to its underlying <see cref="string"/> value.
    /// </summary>
    /// <param name="text">The <see cref="NeutralText"/> instance to convert.</param>
    /// <returns>The underlying string value of the <see cref="NeutralText"/>.</returns>
    public static implicit operator string(NeutralText text) => text.ToString();

    /// <summary>
    /// Implicitly converts a <see cref="string"/> value to a new <see cref="NeutralText"/> instance.
    /// </summary>
    /// <param name="text">The string value to convert.</param>
    /// <returns>A new <see cref="NeutralText"/> instance wrapping the provided string.</returns>
    public static implicit operator NeutralText(string text) => new(text);
}