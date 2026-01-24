using System;
using System.Threading;
using L5Sharp.Core;
using L5Sharp.Gateway.Abstractions;
using L5Sharp.Gateway.Common;

namespace L5Sharp.Gateway.Internal;

/// <summary>
/// Represents a watcher for a specific tag, providing functionality to track and notify changes.
/// </summary>
internal class TagWatch(int handle, Tag tag, int pollRate, ITagService tagService, TagBuffer buffer) : IDisposable
{
    /// <summary>
    /// A private field that keeps track of the number of active subscribers
    /// monitoring a tag for changes. This value is incremented or decremented
    /// as subscribers are added or removed, ensuring an up-to-date count.
    /// </summary>
    private int _subscribers;

    /// <summary>
    /// An instance of <see cref="ITagService"/> used to interact with and manage tag operations.
    /// This service provides the core functionality to handle tag-related actions.
    /// </summary>
    private readonly ITagService _tagService = tagService;

    /// <summary>
    /// A private field that serves as the central mechanism for mapping and converting tag values
    /// between internal representations and external data handles. This buffer facilitates the reading
    /// and writing of values associated with specific tags, ensuring efficient and synchronized data operations.
    /// </summary>
    private readonly TagBuffer _buffer = buffer;

    /// <summary>
    /// A private field that specifies the polling interval in milliseconds used for automatic
    /// synchronization of tag values. This rate determines how frequently the tag's value is
    /// refreshed from the underlying tag service when active subscribers are present.
    /// </summary>
    private readonly int _pollRate = pollRate;

    /// <summary>
    /// A read-only property representing the handle identifier associated with a tag watcher.
    /// This value is used to uniquely reference and manage the lifecycle of a specific tag watch instance.
    /// </summary>
    public int Handle { get; } = handle;

    /// <summary>
    /// Represents the tag instance being monitored by this watch.
    /// This property holds the reference to the tag whose state and value changes are tracked.
    /// </summary>
    public Tag Tag { get; } = tag ?? throw new ArgumentNullException(nameof(tag));

    /// <summary>
    /// Gets the current operational status of the tag watch.
    /// This value is updated whenever the tag's state changes and reflects the most recent status
    /// received from the tag service. A negative value indicates an error condition.
    /// </summary>
    public TagStatus Status { get; private set; }

    /// <summary>
    /// Gets the timestamp of the last successful update for the associated tag.
    /// This value is updated whenever the tag's status changes or when its data is refreshed.
    /// Used to monitor the currency of the tag's data.
    /// </summary>
    public DateTime Timestamp { get; private set; } = DateTime.UtcNow;

    /// <summary>
    /// Represents the total time elapsed since the last status update for the associated tag.
    /// This property is automatically updated whenever a status change or notification occurs,
    /// reflecting the duration between the current and previous status updates.
    /// </summary>
    public TimeSpan Duration { get; private set; } = TimeSpan.Zero;

    /// <summary>
    /// Gets or sets the number of updates received for the associated tag.
    /// This property is incremented each time the tag's value is refreshed
    /// or modified, providing a cumulative count of changes.
    /// </summary>
    public int Updates { get; private set; }

    /// <summary>
    /// Indicates whether the tag watch is currently idle, meaning there are no active subscribers
    /// monitoring the associated tag for changes.
    /// </summary>
    public bool IsIdle => _subscribers == 0;

    /// <summary>
    /// Gets a value indicating whether the current tag is in an errored state.
    /// The errored state is determined by checking if the associated
    /// <see cref="TagStatus"/> has a value less than zero.
    /// </summary>
    public bool IsErrored => Status < 0;

    /// <summary>
    /// An event that is triggered whenever the associated tag's status is updated.
    /// Subscribers can handle this event to react to status changes of the tag,
    /// such as triggering actions or updating dependent systems.
    /// </summary>
    public event Action<TagWatch>? Updated;

    /// <summary>
    /// An event that is triggered whenever the associated <see cref="Tag"/> experiences a change.
    /// Subscribers to this event will be notified and provided with the instance that has undergone the modification.
    /// </summary>
    public event Action<TagWatch>? Changed;

    /// <summary>
    /// An event that is triggered whenever the associated <see cref="Tag"/> encounters an error condition.
    /// Subscribers to this event will be notified and provided with the tag instance and the status code
    /// indicating the specific error that occurred.
    /// </summary>
    public event Action<TagWatch>? Errored;

    /// <summary>
    /// Increments the subscriber count for the associated watch. If this call results in the first subscriber
    /// being added, it enables automatic synchronization of read operations for the associated tag based
    /// on the specified refresh rate.
    /// </summary>
    public void Increment()
    {
        Interlocked.Increment(ref _subscribers);

        if (_subscribers == 1)
        {
            var status = _tagService.SetAttribute(Handle, "auto_sync_read_ms", _pollRate);

            if (status < 0)
                throw new InvalidOperationException($"Unable to start auto sync service. Result: {status}");
        }
    }

    /// <summary>
    /// Decrements the subscriber count for the associated tag. If this call results in the subscriber count
    /// reaching zero, it disables automatic synchronization of read operations for the tag by setting the
    /// synchronization rate to zero.
    /// </summary>
    public void Decrement()
    {
        Interlocked.Decrement(ref _subscribers);

        if (_subscribers == 0)
        {
            var status = _tagService.SetAttribute(Handle, "auto_sync_read_ms", 0);

            if (status < 0)
                throw new InvalidOperationException($"Unable to stop auto sync service. Result: {status}");
        }
    }

    /// <summary>
    /// Notifies the watch of a status change for the associated tag. Updates the current status,
    /// timestamp, and update count, then triggers the appropriate event handler based on the status.
    /// If the status indicates an error (negative value), the <see cref="Errored"/> event is invoked.
    /// If the status is <see cref="TagStatus.Ok"/>, the tag's value is read from the buffer and the
    /// <see cref="Changed"/> event is invoked.
    /// </summary>
    /// <param name="status">The new status of the tag, indicating its current operational state.</param>
    public void Notify(TagStatus status)
    {
        Status = status;
        Duration = DateTime.UtcNow - Timestamp;
        Timestamp = DateTime.UtcNow;
        Updates++;

        Updated?.Invoke(this);

        switch (Status)
        {
            case < 0:
                Errored?.Invoke(this);
                break;
            case TagStatus.Ok:
                if (!_buffer.ReadValue(Tag, Handle)) break;
                Changed?.Invoke(this);
                break;
        }
    }

    /// <inheritdoc />
    public void Dispose()
    {
        // Stop the native polling immediately
        _tagService.SetAttribute(Handle, "auto_sync_read_ms", 0);

        // Clear the subscriber count
        _subscribers = 0;

        // Detach all managed event handlers to avoid memory leaks
        Updated = null;
        Changed = null;
        Errored = null;
    }
}