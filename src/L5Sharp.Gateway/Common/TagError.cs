using L5Sharp.Core;

namespace L5Sharp.Gateway.Common;

/// <summary>
/// Represents an error related to a specific tag, providing information about the tag name and its status.
/// </summary>
public record TagError(TagName TagName, TagStatus Error)
{
    /// <summary>
    /// Gets the name of the tag associated with the current tag error.
    /// </summary>
    public TagName TagName { get; } = TagName;

    /// <summary>
    /// Gets the status associated with the current tag error.
    /// </summary>
    public TagStatus Error { get; } = Error;
}