using System;
using System.Threading;
using L5Sharp.Core;
using L5Sharp.Gateway.Abstractions;
using L5Sharp.Gateway.Common;

namespace L5Sharp.Gateway.Internal;

/// <summary>
/// Represents a watcher for a specific tag, providing functionality to track and notify changes.
/// </summary>
internal class TagWatch(int handle, Tag tag, int refreshRate, ITagService tagService, TagBuffer buffer) : IDisposable
{
    /// <summary>
    /// A private field that keeps track of the number of active subscribers
    /// monitoring a tag for changes. This value is incremented or decremented
    /// as subscribers are added or removed, ensuring an up-to-date count.
    /// </summary>
    private int _subscribers;

    /// <summary>
    /// A private field representing the tag being monitored by the TagWatch instance.
    /// It holds a reference to the associated <see cref="Tag"/> object, enabling access to its properties
    /// and ensuring that operations within TagWatch are performed on the correct tag.
    /// </summary>
    private readonly Tag _tag = tag ?? throw new ArgumentNullException(nameof(tag));

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
    /// A private field that specifies the interval, in milliseconds, at which the tag's state is automatically
    /// synchronized. This value is initialized during object construction and determines the frequency of
    /// updates in the watcher service.
    /// </summary>
    private readonly int _refreshRate = refreshRate;

    /// <summary>
    /// A read-only property representing the handle identifier associated with a tag watcher.
    /// This value is used to uniquely reference and manage the lifecycle of a specific tag watch instance.
    /// </summary>
    public int Handle { get; } = handle;

    /// <summary>
    /// Represents the current operational state or condition of the tag being monitored.
    /// This property reflects changes in the tag's lifecycle or monitoring status,
    /// providing insight into its active or inactive state during runtime.
    /// </summary>
    public TagStatus Status { get; private set; }

    /// <summary>
    /// Gets the current number of active subscribers for the associated tag. The value represents how many entities
    /// are currently monitoring or relying on the changes of the tag through this instance.
    /// This property is automatically managed by incrementing or decrementing when subscription operations are performed.
    /// </summary>
    public int Subscribers => _subscribers;

    /// <summary>
    /// Indicates whether the tag watch is currently idle, meaning there are no active subscribers
    /// monitoring the associated tag for changes.
    /// Returns true if there are no subscribers; otherwise, false.
    /// </summary>
    public bool IsIdle => _subscribers == 0;

    /// <summary>
    /// An event that is triggered whenever the associated <see cref="Tag"/> experiences a change.
    /// Subscribers to this event will be notified and provided with the <see cref="Tag"/> instance
    /// that has undergone the modification.
    /// </summary>
    public event Action<Tag>? OnChanged;

    /// <summary>
    /// Increments the subscriber count for the associated watch. If this call results in the first subscriber
    /// being added, it enables automatic synchronization of read operations for the associated tag based
    /// on the specified refresh rate.
    /// </summary>
    public void Increment()
    {
        Interlocked.Increment(ref _subscribers);

        // This would indicate that we just started the watch and need to enable auto sync.
        if (_subscribers == 1)
        {
            _tagService.SetAttribute(Handle, "auto_sync_read_ms", _refreshRate);
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

        // No more subscribers means stop the auto sync for the tag.
        if (_subscribers == 0)
        {
            _tagService.SetAttribute(Handle, "auto_sync_read_ms", 0);
        }
    }

    /// <summary>
    /// Notifies the system of a status change for the associated tag. This triggers a value read operation
    /// and invokes the change event if the tag's value has been altered.
    /// </summary>
    /// <param name="status">The new status of the tag to be processed and monitored for changes.</param>
    public void Notify(TagStatus status)
    {
        //todo can we add a read if changed or something? We want to only invoke the event on change in value.
        Status = status;

        if (Status == TagStatus.Ok)
        {
            _buffer.ReadValue(_tag, Handle);
            OnChanged?.Invoke(_tag);
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
        OnChanged = null;
    }
}