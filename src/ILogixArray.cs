using System.Collections.Generic;
using L5Sharp.Core;
using L5Sharp.Types;

namespace L5Sharp;

/// <summary>
/// A generic interface representing a logic array type. This is needed to provided the co-variance of the generic type
/// argument so we can use pattern matching for determine the <see cref="ILogixType"/> at runtime.
/// </summary>
/// <typeparam name="TLogixType">The <see cref="ILogixType"/> the array contains.</typeparam>
public interface ILogixArray<out TLogixType> : ILogixType, IEnumerable<TLogixType> where TLogixType : ILogixType
{
    /// <summary>
    /// Gets the dimensions of the the array.
    /// </summary>
    /// <value>A <see cref="Core.Dimensions"/> value representing the array dimensions.</value>
    Dimensions Dimensions { get; }

    /// <summary>
    /// Gets a collection of <see cref="Member"/> that represent the elements of the array.
    /// </summary>
    /// <returns>A <see cref="IEnumerable{T}"/> containing <see cref="Member"/> object.</returns>
    IEnumerable<Member> Elements { get; }

    /// <summary>
    /// Gets the <see cref="ILogixType"/> instance at the specified index.
    /// </summary>
    /// <param name="x">The index of the array element</param>
    TLogixType this[ushort x] { get; }

    /// <summary>
    /// Gets the <see cref="ILogixType"/> instance at the specified index.
    /// </summary>
    /// <param name="x">The x index of the array element</param>
    /// <param name="y">The y index of the array element</param>
    TLogixType this[ushort x, ushort y] { get; }

    /// <summary>
    /// Gets the <see cref="ILogixType"/> instance at the specified index.
    /// </summary>
    /// <param name="x">The x index of the array element</param>
    /// <param name="y">The y index of the array element</param>
    /// <param name="z">The z index of the array element</param>
    TLogixType this[ushort x, ushort y, ushort z] { get; }
}