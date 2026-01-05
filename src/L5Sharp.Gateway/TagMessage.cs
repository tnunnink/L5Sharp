namespace L5Sharp.Gateway;

/// <summary>
/// Represents a message containing information about a tag operation or event.
/// The tag message includes a handle to uniquely identify the tag instance,
/// the action performed on the tag, and the result of the action.
/// </summary>
public readonly struct TagMessage(int handle, int @event, int status)
{
    /// <summary>
    /// Gets the unique identifier associated with the tag instance.
    /// This handle serves as a key used to dereference the tag what this message is associated with.
    /// </summary>
    public int Handle { get; } = handle;

    /// <summary>
    /// Gets the action performed on a tag associated with the tag message.
    /// This action represents an operation or state change, such as reading, writing,
    /// creation, destruction, or abortion, uniquely identifying the type of tag event.
    /// </summary>
    public int Event { get; } = @event;

    /// <summary>
    /// Gets the result of the tag operation associated with the message.
    /// This result indicates the outcome or status of the action performed on the tag.
    /// </summary>
    public int Status { get; } = status;
}