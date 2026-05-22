using System;
using System.Collections.Generic;

namespace L5Sharp.Core;

/// <summary>
/// Provides a stream-based interface for sequentially processing tokens from neutral text.
/// Maintains position state and supports lookahead operations for token-by-token parsing.
/// </summary>
public class NeutralStream : IDisposable
{
    /// <summary>
    /// The internal collection of tokens to be processed by this stream.
    /// </summary>
    private readonly IEnumerator<NeutralToken> _enumerator;

    /// <summary>
    /// Initializes a new instance of the <see cref="NeutralStream"/> class with the specified neutral text.
    /// </summary>
    /// <param name="text">The neutral text to tokenize and stream.</param>
    public NeutralStream(NeutralText text)
    {
        _enumerator = text.Tokenize().GetEnumerator();
    }

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
        if (_enumerator.MoveNext())
        {
            token = _enumerator.Current;
            return true;
        }

        token = _enumerator.Current;
        return false;
    }

    /// <summary>
    /// Returns the current token without consuming it or advancing the stream position.
    /// If the stream is at the end, it returns an end-of-file token.
    /// </summary>
    /// <returns>The token at the current position, or an end-of-file token if no more tokens are available.</returns>
    public NeutralToken Peek() => _enumerator.Current;

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
    public bool Advance(int count = 1)
    {
        if (count < 0)
            throw new ArgumentException("Count cannot be negative. Use Reset() to move to the beginning of the stream.",
                nameof(count));

        var index = 0;

        while (index < count && _enumerator.MoveNext())
        {
            index++;
        }

        return index == count;
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
    public bool Seek(Func<NeutralToken, bool> predicate)
    {
        while (_enumerator.MoveNext())
        {
            if (predicate(_enumerator.Current))
                return true;
        }

        return false;
    }

    /// <summary>
    /// Resets the stream's position to the beginning of the token sequence.
    /// </summary>
    /// <remarks>
    /// This operation enables reprocessing of tokens from the start of the stream.
    /// </remarks>
    public void Reset()
    {
        _enumerator.Reset();
    }

    /// <summary>
    /// Determines whether the current token matches the specified token type.
    /// </summary>
    /// <param name="type">The token type to match against the current token.</param>
    /// <returns><c>true</c> if the current token matches the specified token type; otherwise, <c>false</c>.</returns>
    public bool Match(TokenType type) => _enumerator.Current.Type == type;

    /// <summary>
    /// Releases all resources used by the current instance of the <see cref="NeutralStream"/> class.
    /// </summary>
    public void Dispose()
    {
        _enumerator.Dispose();
    }
}