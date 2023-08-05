using System.Collections.Generic;
using L5Sharp.Common;

namespace L5Sharp.Extensions;

/// <summary>
/// A class containing extensions for the <see cref="NeutralText"/> class.
/// </summary>
public static class NeutralTextExtensions
{
    /// <summary>
    /// Returns all referenced tag names and their corresponding list of <see cref="NeutralText"/> logic references in
    /// the current collection of <see cref="NeutralText"/>.
    /// </summary>
    /// <param name="text">A collection of <see cref="NeutralText"/> rung logic.</param>
    /// <returns>A <see cref="Dictionary{TKey,TValue}"/> where each tag name is a key and it's corresponding value is
    /// a <see cref="List{T}"/> containing all the logic referencing the tag found in the file.</returns>
    /// <remarks>
    /// This is useful for performing quick lookup of logic references by tag name.
    /// </remarks>
    public static Dictionary<TagName, List<NeutralText>> ToTagLookup(this IEnumerable<NeutralText> text)
    {
        var results = new Dictionary<TagName, List<NeutralText>>();

        foreach (var line in text)
        {
            var tags = line.Tags();

            foreach (var tag in tags)
            {
                if (!results.ContainsKey(tag))
                {
                    results.Add(tag, new List<NeutralText> { line });
                    continue;
                }

                results[tag].Add(line);
            }
        }

        return results;
    }
}