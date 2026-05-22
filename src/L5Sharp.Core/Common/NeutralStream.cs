using System;
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
    private readonly NeutralToken[] _tokens;

    /// <summary>
    /// The zero-based index of the current position in the token stream.
    /// </summary>
    private int _currentIndex;

    /// <summary>
    /// The zero-based index representing the last valid position in the internal token array.
    /// Used to determine the boundary for processing and prevent out-of-range access during parsing.
    /// </summary>
    private readonly int _endIndex;

    /// <summary>
    /// Initializes a new instance of the <see cref="NeutralStream"/> class with the specified token sequence.
    /// </summary>
    /// <param name="tokens">The sequence of tokens to process. The tokens are materialized into an internal list.</param>
    public NeutralStream(IEnumerable<NeutralToken> tokens)
    {
        _tokens = tokens.ToArray();
        _endIndex = _tokens.Length > 0 ? _tokens.Length - 1 : 0;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NeutralStream"/> class that provides a view into an existing token array
    /// starting at the specified index. This constructor enables memory-efficient stream slicing by reusing the underlying
    /// token array rather than creating a copy, which is particularly useful when skipping a known prefix sequence.
    /// </summary>
    /// <param name="tokens">The existing array of tokens to use as the underlying data source.</param>
    /// <param name="startIndex">The zero-based index at which this stream should begin reading tokens from the array.</param>
    public NeutralStream(NeutralToken[] tokens, int startIndex)
    {
        _tokens = tokens;
        _currentIndex = startIndex;
        _endIndex = _tokens.Length > 0 ? _tokens.Length - 1 : 0;
    }

    /// <summary>
    /// Gets an empty instance of the <see cref="NeutralStream"/> class.
    /// This static property provides a neutral stream initialized with no tokens.
    /// </summary>
    public static NeutralStream Empty => new([]);

    /// <summary>
    /// Gets the total number of tokens available in the current stream.
    /// </summary>
    public int Length => _tokens.Length;

    /// <summary>
    /// Attempts to read the next token from the stream.
    /// </summary>
    /// <param name="token">When this method returns, contains the token at the current position if the read was successful;
    /// otherwise, the default value.</param>
    /// <returns><c>true</c> if a token was successfully read and the stream is not at the end; otherwise, <c>false</c>.</returns>
    /// <remarks>
    /// If the stream has reached the end, this method returns <c>false</c> and the <paramref name="token"/>
    /// will be an EOF token.
    /// </remarks>
    public bool Read(out NeutralToken token)
    {
        token = CurrentToken();

        if (_currentIndex < _endIndex)
        {
            _currentIndex++;
            return true;
        }

        return false;
    }

    /// <summary>
    /// Returns the current token without consuming it or advancing the stream position.
    /// If the stream is at the end, it returns an end-of-file token.
    /// </summary>
    /// <returns>The token at the current position, or an end-of-file token if no more tokens are available.</returns>
    public NeutralToken Peek() => CurrentToken();

    /// <summary>
    /// Retrieves the next token from the stream without advancing the current position.
    /// If no more tokens are available, an EOF token is returned.
    /// </summary>
    /// <returns>The next <see cref="NeutralToken"/> in the stream, or an EOF token if the end of the stream is reached.</returns>
    public NeutralToken PeekNext() => NextToken();

    /// <summary>
    /// Retrieves the last processed token in the sequence relative to the current position.
    /// If no tokens have been processed, returns a default or ending token.
    /// </summary>
    /// <returns>The last processed <see cref="NeutralToken"/> or a default token if none exists.</returns>
    public NeutralToken PeekLast() => LastToken();

    /// <summary>
    /// Checks if the current token matches the specified token type without consuming it.
    /// </summary>
    /// <param name="type">The token type to compare against the current token.</param>
    /// <returns><c>true</c> if the current token's type matches the specified type; otherwise, <c>false</c>.</returns>
    public bool Match(TokenType type) => CurrentToken().Type == type;

    /// <summary>
    /// Determines whether the next token in the stream matches any of the specified token types.
    /// </summary>
    /// <param name="types">An array of token types to compare against the type of the next token.</param>
    /// <returns>Returns <c>true</c> if the next token matches one of the specified token types; otherwise, <c>false</c>.</returns>
    public bool HasNext(params TokenType[] types) => types.Contains(NextToken().Type);

    /// <summary>
    /// Determines if the last token matches any of the specified token types.
    /// </summary>
    /// <param name="types">The array of token types to match against the last token.</param>
    /// <returns>True if the last token matches one of the specified types, otherwise false.</returns>
    public bool HasLast(params TokenType[] types) => types.Contains(LastToken().Type);

    /// <summary>
    /// Attempts to move the current position in the token stream by the specified number of tokens.
    /// </summary>
    /// <param name="count">
    /// The number of tokens to move the position. Positive values move forward, and negative values move backward.
    /// </param>
    /// <returns>
    /// True if the operation successfully moved the position within the bounds of the token stream;
    /// false if the position was clamped to the start or end of the stream.
    /// </returns>
    public bool Seek(int count = 1) => SeekToken(count);

    /// <summary>
    /// Resets the stream's position to the beginning of the token sequence.
    /// </summary>
    /// <remarks>
    /// This operation enables reprocessing of tokens from the start of the stream.
    /// </remarks>
    public NeutralStream SeekBegin()
    {
        _currentIndex = 0;
        return this;
    }

    /// <summary>
    /// Moves the current position to the last token in the stream.
    /// </summary>
    /// <remarks>
    /// This operation enables consumption of tokens from the end of the stream.
    /// </remarks>
    public NeutralStream SeekEnd()
    {
        _currentIndex = _endIndex;
        return this;
    }

    /// <summary>
    /// Advances the stream forward by seeking the next token that matches the specified condition.
    /// </summary>
    /// <param name="predicate">
    /// A function to evaluate each token in the stream.
    /// The stream advances until a token satisfying the predicate is found.
    /// </param>
    /// <returns>
    /// Returns <c>true</c> if a matching token is found;
    /// otherwise, <c>false</c> if the end of the stream is reached without a match.
    /// </returns>
    public bool SeekForward(Func<NeutralToken, bool> predicate)
    {
        while (SeekToken(1))
        {
            var token = CurrentToken();
            if (predicate(token)) return true;
        }

        return false;
    }

    /// <summary>
    /// Advances the stream backward by seeking the previous token that matches the specified condition.
    /// </summary>
    /// <param name="predicate">
    /// A function to evaluate each token in the stream.
    /// The stream advances backward until a token satisfying the predicate is found.
    /// </param>
    /// <returns>
    /// Returns <c>true</c> if a matching token is found;
    /// otherwise, <c>false</c> if the beginning of the stream is reached without a match.
    /// </returns>
    public bool SeekBack(Func<NeutralToken, bool> predicate)
    {
        while (SeekToken(-1))
        {
            var token = CurrentToken();
            if (predicate(token)) return true;
        }

        return false;
    }

    /// <summary>
    /// Moves the current position in the token stream by the specified number of tokens.
    /// </summary>
    /// <param name="count">
    /// The number of tokens to move the position. Positive values move forward, and negative values move backward.
    /// </param>
    /// <returns>
    /// True if the position was successfully moved within the bounds of the token stream;
    /// false if the position was clamped to the start or end of the stream.
    /// </returns>
    private bool SeekToken(int count)
    {
        var position = _currentIndex + count;

        if (position < 0)
        {
            _currentIndex = 0;
            return false;
        }

        if (position >= _endIndex)
        {
            _currentIndex = _endIndex;
            return false;
        }

        _currentIndex = position;
        return true;
    }

    /// <summary>
    /// Retrieves the current token at the stream's current position.
    /// </summary>
    private NeutralToken CurrentToken() => _tokens[_currentIndex];

    /// <summary>
    /// Retrieves the next token in the stream without advancing the current position.
    /// If the current position is at the end of the stream, returns <see cref="NeutralToken.None"/>.
    /// </summary>
    private NeutralToken NextToken() => _currentIndex + 1 <= _endIndex ? _tokens[_currentIndex + 1] : NeutralToken.None;

    /// <summary>
    /// Retrieves the previous token from the token stream without advancing the current position.
    /// If the beginning of the token stream is reached, returns <see cref="NeutralToken.None"/>.
    /// </summary>
    private NeutralToken LastToken() => _currentIndex - 1 >= 0 ? _tokens[_currentIndex - 1] : NeutralToken.None;
}