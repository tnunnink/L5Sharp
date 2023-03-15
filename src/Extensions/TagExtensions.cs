using System;
using System.Linq.Expressions;
using L5Sharp.Components;
using L5Sharp.Core;

namespace L5Sharp.Extensions;

/// <summary>
/// A collection of built in extensions for the <see cref="Tag"/> component.
/// </summary>
public static class TagExtensions
{
    //todo perhaps add later....
    /*/// <summary>
    /// Returns the nested <c>TagMember</c> using a specified selector expression.
    /// </summary>
    /// <param name="tag">The current <see cref="ILogixTag"/> component.</param>
    /// <param name="selector">The selector expression </param>
    /// <typeparam name="TLogicType">The type of the current tag's logic type.</typeparam>
    /// <returns>A <see cref="TagMember"/> representing the nested member of the tag structure.</returns>
    /// <exception cref="ArgumentException">The specified logic type does not match the actual type of the tag.</exception>
    /// <exception cref="InvalidOperationException">The selector expression did not find a member at the specified path.</exception>
    public static TagMember Member<TLogicType>(this ILogixTag tag, Expression<Func<TLogicType, ILogixType>> selector)
    {
        if (tag.Data is not TLogicType)
            throw new ArgumentException(
                $"The current tag type {tag.Data.GetType()} does not match the specified type {typeof(TLogicType)}");

        var parameter = selector.Parameters[0].Name;
        var path = selector.ToString().Replace($"{parameter} => {parameter}.", string.Empty);
        var tagName = new TagName(path);

        return tag.Member(tagName) ??
               throw new InvalidOperationException($"No member found for the specified path {path}.");
    }*/
}