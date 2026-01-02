namespace L5Sharp.Gateway;

/// <summary>
/// Represents the various state changes or actions that can occur with a tag during its lifecycle.
/// </summary>
public enum TagAction
{
    /// <summary>
    /// Represents the event that triggers when a read operation has started for a tag.
    /// </summary>
    ReadStarted,

    /// <summary>
    /// Represents the event that triggers when a read operation for a tag has been successfully completed.
    /// </summary>
    ReadCompleted,

    /// <summary>
    /// Represents the event that triggers when a write operation has started for a tag.
    /// </summary>
    WriteStarted,

    /// <summary>
    /// Represents the event that triggers when a write operation has successfully completed for a tag.
    /// </summary>
    WriteCompleted,

    /// <summary>
    /// Represents the event that occurs when an operation involving a tag is canceled or terminated before completion.
    /// </summary>
    Aborted,

    /// <summary>
    /// Represents the event that occurs when a tag is destroyed.
    /// </summary>
    Destroyed,

    /// <summary>
    /// Represents the event that triggers when a tag is created.
    /// </summary>
    Created
}