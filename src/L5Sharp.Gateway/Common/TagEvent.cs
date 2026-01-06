namespace L5Sharp.Gateway.Common;

/// <summary>
/// Represents the various state changes or actions that can occur with a tag during its lifecycle.
/// </summary>
internal static class TagEvent
{
    /// <summary>
    /// Represents the event that triggers when a read operation has started for a tag.
    /// </summary>
    public const int ReadStarted = 1;

    /// <summary>
    /// Represents the event that triggers when a read operation for a tag has been successfully completed.
    /// </summary>
    public const int ReadCompleted = 2;

    /// <summary>
    /// Represents the event that triggers when a write operation has started for a tag.
    /// </summary>
    public const int WriteStarted = 3;

    /// <summary>
    /// Represents the event that triggers when a write operation has successfully completed for a tag.
    /// </summary>
    public const int WriteCompleted = 4;

    /// <summary>
    /// Represents the event that occurs when an operation involving a tag is canceled or terminated before completion.
    /// </summary>
    public const int Aborted = 5;

    /// <summary>
    /// Represents the event that occurs when a tag is destroyed.
    /// </summary>
    public const int Destroyed = 6;

    /// <summary>
    /// Represents the event that triggers when a tag is created.
    /// </summary>
    public const int Created = 7;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="event"></param>
    /// <returns></returns>
    public static bool IsComplete(int @event)
    {
        return @event is ReadCompleted or WriteCompleted or Created;
    }
}