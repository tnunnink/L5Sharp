using L5Sharp.Core;

namespace L5Sharp.Gateway.Internal;

/// <summary>
/// Provides extension methods for working with tags in a Rockwell Logix-based PLC environment.
/// </summary>
internal static class TagExtensions
{
    /// <summary>
    /// Determines the fully qualified tag name based on the provided tag's scope and name.
    /// </summary>
    /// <param name="tag">The tag for which the name needs to be determined.</param>
    /// <returns>Returns the fully qualified tag name as a string.</returns>
    internal static TagName DetermineTagName(this Tag tag)
    {
        if (tag.Scope.IsProgram)
        {
            return $"Program:{tag.Scope.Container}.{tag.TagName}";
        }

        return tag.TagName;
    }
}