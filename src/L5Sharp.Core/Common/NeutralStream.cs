using System.Collections.Generic;
using System.Linq;

namespace L5Sharp.Core;

/// <summary>
/// Provides a stream-based interface for sequentially processing tokens from neutral text.
/// Maintains position state and supports lookahead operations for token-by-token parsing.
/// </summary>
public class NeutralStream
{
    /// <summary>
    /// The internal collection of tokens to be processed by this stream.
    /// </summary>
    private readonly List<NeutralToken> _tokens;

    /// <summary>
    /// The zero-based index of the current position in the token stream.
    /// </summary>
    private int _currentIndex;

    /// <summary>
    /// Initializes a new instance of the <see cref="NeutralStream"/> class with the specified token sequence.
    /// </summary>
    /// <param name="tokens">The sequence of tokens to process. The tokens are materialized into an internal list.</param>
    public NeutralStream(IEnumerable<NeutralToken> tokens)
    {
        _tokens = tokens.ToList();
    }

    /// <summary>
    /// Consumes the current token from the stream and advances the position to the next token.
    /// </summary>
    /// <returns>The token at the current position before advancing.</returns>
    public NeutralToken Consume() => ConsumeToken();

    /// <summary>
    /// Returns the current token without consuming it or advancing the stream position.
    /// If the stream is at the end, returns an end-of-file token.
    /// </summary>
    /// <returns>The token at the current position, or an end-of-file token if no more tokens are available.</returns>
    public NeutralToken Peek() => GetToken();

    /// <summary>
    /// Checks if the current token matches the specified token type without consuming it.
    /// </summary>
    /// <param name="type">The token type to compare against the current token.</param>
    /// <returns><c>true</c> if the current token's type matches the specified type; otherwise, <c>false</c>.</returns>
    public bool Match(TokenType type) => GetToken().Type == type;

    /// <summary>
    /// Indicates whether the stream has reached the end of the token collection
    /// or encountered an end-of-file (EOF) token.
    /// </summary>
    public bool Ended => _currentIndex >= _tokens.Count || GetToken().Type == TokenType.EOF;

    /// <summary>
    /// Consumes the current token from the stream, advances the position to the next token, and returns the consumed token.
    /// If the stream is at the end, it returns an end-of-file token.
    /// </summary>
    /// <returns>The token at the current position before advancing, or an end-of-file token if the stream is at the end.</returns>
    private NeutralToken ConsumeToken()
    {
        var token = GetToken();

        if (_currentIndex < _tokens.Count)
        {
            _currentIndex++;
        }

        return token;
    }

    /// <summary>
    /// Retrieves the token at the current stream position without advancing the position.
    /// If the current position exceeds the number of available tokens, an end-of-file token is returned.
    /// </summary>
    /// <returns>The token at the current position, or an end-of-file token if no more tokens are available.</returns>
    private NeutralToken GetToken()
    {
        if (_currentIndex < _tokens.Count)
            return _tokens[_currentIndex];

        if (_tokens.Count == 0)
            return NeutralToken.EOF(0);

        var last = _tokens[_tokens.Count - 1];
        return NeutralToken.EOF(last.Index + last.Length);
    }
}