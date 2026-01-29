using L5Sharp.Core;

namespace L5Sharp.Gateway.Internal;

/// <summary>
/// Represents a unique key for identifying asynchronous PLC operations, composed of a tag name and an event identifier.
/// This structure is used internally to track and manage concurrent operations on PLC tags.
/// </summary>
/// <param name="Tag">The name of the tag associated with the operation.</param>
/// <param name="EventId">The event identifier that distinguishes the type of operation (e.g., read, write, create).</param>
internal readonly record struct OperationKey(TagName Tag, int EventId)
{
    /// <summary>
    /// Gets the name of the tag associated with this operation key.
    /// </summary>
    public TagName Tag { get; } = Tag;

    /// <summary>
    /// Gets the event identifier that represents the type of operation being performed.
    /// </summary>
    // ReSharper disable once UnusedMember.Local used by equality check
    public int EventId { get; } = EventId;

    /// <summary>
    /// Creates a new <see cref="OperationKey"/> for a tag creation operation.
    /// </summary>
    /// <param name="tagName">The name of the tag for which the operation key is being created.</param>
    /// <returns>An <see cref="OperationKey"/> representing a tag creation operation.</returns>
    public static OperationKey Create(TagName tagName) => new(tagName, TagEvent.Created);

    /// <summary>
    /// Creates a new <see cref="OperationKey"/> for a tag read operation.
    /// </summary>
    /// <param name="tagName">The name of the tag for which the operation key is being created.</param>
    /// <returns>An <see cref="OperationKey"/> representing a tag read completion operation.</returns>
    public static OperationKey Read(TagName tagName) => new(tagName, TagEvent.ReadCompleted);

    /// <summary>
    /// Creates a new <see cref="OperationKey"/> for a tag write operation.
    /// </summary>
    /// <param name="tagName">The name of the tag for which the operation key is being created.</param>
    /// <returns>An <see cref="OperationKey"/> representing a tag write completion operation.</returns>
    public static OperationKey Write(TagName tagName) => new(tagName, TagEvent.WriteCompleted);
}