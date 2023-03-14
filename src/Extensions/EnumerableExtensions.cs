using System.Collections.Generic;
using System.Linq;
using L5Sharp.Core;

namespace L5Sharp.Extensions;

/// <summary>
/// 
/// </summary>
public static class EnumerableExtensions
{
    /// <summary>
    /// Returns all referenced tag names and their corresponding list of <see cref="NeutralText"/> logic references in the
    /// L5X file.
    /// </summary>
    /// <param name="logic">A collection of <see cref="NeutralText"/> rung logic.</param>
    /// <returns>A <see cref="Dictionary{TKey,TValue}"/> where each tag name is a key and it's corresponding value is
    /// a <see cref="List{T}"/> containing all the logic referencing the tag found in the file.</returns>
    /// <remarks>
    /// This is useful for performing quick lookup of references to tag names.
    /// </remarks>
    public static Dictionary<TagName, List<NeutralText>> ToTagLookup(this IEnumerable<NeutralText> logic)
    {
        var references = logic.SelectMany(t => t.References());

        var results = new Dictionary<TagName, List<NeutralText>>();

        foreach (var reference in references)
        {
            if (!results.ContainsKey(reference.Key))
            {
                results.Add(reference.Key, new List<NeutralText> { reference.Value });
                continue;
            }

            results[reference.Key].Add(reference.Value);
        }

        return results;
    }
    
    /// <summary>
    /// Returns all referenced instructions and their corresponding list of <see cref="NeutralText"/> logic references
    /// in the L5X file.
    /// </summary>
    /// <param name="logic">A collection of <see cref="NeutralText"/> rung logic.</param>
    /// <returns>A <see cref="Dictionary{TKey,TValue}"/> where each instruction is a key and it's corresponding value is
    /// a <see cref="List{T}"/> containing all the logic referencing the instruction found in the file.</returns>
    /// <remarks>
    /// This is useful for performing quick lookup of references to instructions.
    /// </remarks>
    public static Dictionary<string, List<NeutralText>> ToInstructionLookup(this IEnumerable<NeutralText> logic)
    {
        var references = logic.SelectMany(t => t.Split());

        var results = new Dictionary<string, List<NeutralText>>();

        foreach (var reference in references)
        {
            var key = reference.Keys().FirstOrDefault();
            if (key is null) continue;

            if (!results.ContainsKey(key))
                results.Add(key, new List<NeutralText> { reference });
            
            results[key].Add(reference);
        }

        return results;
    }
}