using System.Collections.Generic;

namespace L5Sharp.Core;

/// <summary>
/// Provides extension methods for working with neutral text tokens and converting them
/// into streamable representations for processing and manipulation.
/// </summary>
public static class NeutralTextExtensions
{
    /// <summary>
    /// Converts a collection of neutral tokens into a NeutralStream for sequential processing.
    /// </summary>
    /// <param name="tokens">The collection of <see cref="NeutralToken"/> instances to convert into a stream.</param>
    /// <returns>A <see cref="NeutralStream"/> that wraps the provided tokens for sequential access and manipulation.</returns>
    public static NeutralStream ToStream(this IEnumerable<NeutralToken> tokens)
    {
        return new NeutralStream(tokens);
    }

    /// <summary>
    /// Converts an array of neutral tokens into a <see cref="NeutralStream"/> for sequential processing,
    /// starting from the specified index within the token array.
    /// </summary>
    /// <param name="tokens">The array of <see cref="NeutralToken"/> instances to convert into a stream.</param>
    /// <param name="startIndex">The starting index within the token array to begin the stream. Defaults to 0 if not provided.</param>
    /// <returns>A <see cref="NeutralStream"/> that wraps the specified tokens for sequential access and manipulation, starting at the given index.</returns>
    public static NeutralStream ToStream(this NeutralToken[] tokens, int startIndex = 0)
    {
        return new NeutralStream(tokens, startIndex);
    }
}