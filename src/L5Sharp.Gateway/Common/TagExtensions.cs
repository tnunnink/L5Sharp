using L5Sharp.Core;

namespace L5Sharp.Gateway.Common;

/// <summary>
/// Provides extension methods for working with tags in a Rockwell Logix-based PLC environment.
/// </summary>
public static class TagExtensions
{
    /// <summary>
    /// Converts an integer code to its corresponding <see cref="TagStatus"/> value.
    /// </summary>
    /// <param name="code">The integer code to be converted to a <see cref="TagStatus"/>.</param>
    /// <returns>The <see cref="TagStatus"/> that corresponds to the provided code.</returns>
    public static TagStatus AsResult(this int code)
    {
        return (TagStatus)code;
    }

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