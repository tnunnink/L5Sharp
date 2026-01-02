using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Channels;
using L5Sharp.Core;
using libplctag.NativeImport;
using Task = System.Threading.Tasks.Task;

namespace L5Sharp.Gateway;

/// <summary>
/// Represents a system for monitoring and managing a collection of tags, typically used in a PLC environment.
/// This class allows for asynchronous communication and event handling of tag-related operations.
/// </summary>
internal class TagWatch : ITagWatch
{
    /// <summary>
    /// Indicates whether the <see cref="TagWatch"/> instance has been disposed.
    /// Used to prevent multiple disposals and ensure proper resource cleanup.
    /// </summary>
    private bool _disposed;

    /// <summary>
    /// Represents the base URI used for communication or identification purposes within the <see cref="TagWatch"/> instance.
    /// It serves as the foundational reference for interacting with related tag operations or endpoints.
    /// </summary>
    private readonly string _baseUri;

    /// <summary>
    /// 
    /// </summary>
    private readonly PlcOptions _options;

    /// <summary>
    /// Specifies the interval, in milliseconds, at which tag values are read and processed
    /// in the <see cref="TagWatch"/> instance. This value determines the frequency of tag
    /// polling and influences the responsiveness of the system.
    /// </summary>
    private readonly int _readInterval;

    /// <summary>
    /// Holds a collection of <see cref="Tag"/> instances keyed by their unique handle values.
    /// Facilitates efficient lookup and management of tags within the <see cref="TagWatch"/> class.
    /// </summary>
    private readonly ConcurrentDictionary<int, Tag> _tags = [];

    /// <summary>
    /// Maintains a collection of callback actions that are executed
    /// when a tag-related event occurs. Each action in this collection
    /// represents a subscriber to tag change notifications.
    /// </summary>
    private readonly ConcurrentDictionary<Guid, Action<Tag>> _subscribers = [];

    /// <summary>
    /// A delegate used as a callback function for handling PLC tag-related events.
    /// This delegate is invoked by the underlying <see cref="libplctag.NativeImport.plctag"/> library
    /// when a specific event occurs, facilitating communication between the library and the client.
    /// </summary>
    private readonly plctag.callback_func_ex _callbackDelegate;

    /// <summary>
    /// An internal channel used to handle communication of <see cref="TagMessage"/> instances
    /// between the tag monitoring system and the asynchronous message processing logic.
    /// The channel ensures a thread-safe, single-writer, single-reader pattern for efficient data flow.
    /// </summary>
    private readonly Channel<TagMessage> _channel;

    /// <summary>
    /// Represents a token source used to signal and manage cancellation of the asynchronous operations
    /// executed within the <see cref="TagWatch"/> instance. Ensures proper termination and resource
    /// cleanup during the stopping of the tag monitoring process.
    /// </summary>
    private CancellationTokenSource? _cancellation;

    /// <summary>
    /// Represents the background task responsible for processing tag messages asynchronously.
    /// Manages the lifecycle of the task, including starting, running, and stopping it.
    /// </summary>
    private Task? _processor;

    /// <summary>
    /// Represents a class that monitors and manages a collection of PLC tags.
    /// Provides functionality for starting and stopping tag monitoring, subscribing to tag events,
    /// and handling tag event callbacks.
    /// </summary>
    internal TagWatch(ICollection<Tag> tags, string baseUri, PlcOptions options)
    {
        _callbackDelegate = TagEventCallback;
        _baseUri = baseUri;
        _readInterval = options.ReadInterval;
        _options = options;

        var capacity = Math.Max(100, tags.Count * 10);

        _channel = Channel.CreateBounded<TagMessage>(new BoundedChannelOptions(capacity)
        {
            FullMode = BoundedChannelFullMode.DropOldest,
            SingleWriter = true,
            SingleReader = true
        });

        foreach (var tag in tags)
        {
            RegisterTag(tag);
        }
    }

    ~TagWatch()
    {
        Dispose();
    }

    /// <inheritdoc />
    public bool IsRunning { get; private set; }

    /// <inheritdoc />
    public int RefreshRate
    {
        get => _readInterval;
        set => UpdateReadInterval(value);
    }

    /// <inheritdoc />
    public IEnumerable<Tag> Tags => _tags.Values;

    /// <inheritdoc />
    public Task StartAsync(CancellationToken token = default)
    {
        ThrowIfDisposed();

        if (IsRunning)
            return Task.CompletedTask;

        _cancellation = new CancellationTokenSource();
        _processor = Task.Run(() => ProcessMessagesAsync(_cancellation.Token), token);

        IsRunning = true;
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public async Task StopAsync(CancellationToken token = default)
    {
        ThrowIfDisposed();

        if (!IsRunning) return;

        _cancellation?.Cancel();

        try
        {
            if (_processor is not null)
            {
                await _processor;
            }
        }
        catch (OperationCanceledException)
        {
        }
        finally
        {
            _cancellation?.Dispose();
            _cancellation = null;
            _processor = null;
            IsRunning = false;
        }
    }

    /// <inheritdoc />
    public IDisposable Subscribe(Action<Tag> callback)
    {
        ThrowIfDisposed();

        if (callback is null)
            throw new ArgumentNullException(nameof(callback));

        var id = Guid.NewGuid();
        _subscribers.TryAdd(id, callback);

        return new Unsubscriber(() => _subscribers.TryRemove(id, out _));
    }

    /// <inheritdoc />
    public void Dispose()
    {
        if (_disposed) return;

        foreach (var handle in _tags.Keys)
        {
            plctag.plc_tag_destroy(handle);
        }

        _disposed = true;
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Registers a PLC tag for monitoring, enabling callback notifications for tag events.
    /// </summary>
    /// <param name="tag">The tag to be registered for watch operations. This must be a valid PLC tag object.</param>
    /// <exception cref="TagException">
    /// Thrown when the callback registration fails or cannot process the given tag due to configuration
    /// or operational constraints.
    /// </exception>
    private void RegisterTag(Tag tag)
    {
        var tagName = tag.DetermineTagName();
        var path = $"{_baseUri}&name={tagName}&auto_sync_read_ms={RefreshRate}";

        var result = plctag.plc_tag_create_ex(path, _callbackDelegate, IntPtr.Zero, _options.Timeout).AsResult();
        TagException.ThrowIfRequested(result, _options.ExceptionCodes, tagName);
        if (result <= 0) return;

        var handle = (int)result;
        tag.SetStatus(TagStatus.Pending(handle));
        _tags.TryAdd(handle, tag);
    }

    /// <summary>
    /// Handles PLC tag events by processing the provided handle, event ID, and status ID.
    /// </summary>
    /// <param name="handle">The handle of the tag associated with the event.</param>
    /// <param name="eventId">The identifier for the event type that occurred.</param>
    /// <param name="statusId">The status code for the operation or event that triggered the callback.</param>
    /// <param name="data"></param>
    private void TagEventCallback(int handle, int eventId, int statusId, IntPtr data)
    {
        var message = new TagMessage(handle, eventId.AsAction(), statusId.AsResult());
        _channel.Writer.TryWrite(message);
    }

    /// <summary>
    /// Processes incoming tag messages asynchronously and performs updates or actions based on the messages received.
    /// This method reads messages from a channel and updates associated tags accordingly.
    /// </summary>
    /// <param name="token">A cancellation token used to signal the operation to cancel message processing.</param>
    /// <returns>A task that represents the asynchronous operation of processing tag messages.</returns>
    private async Task ProcessMessagesAsync(CancellationToken token)
    {
        await foreach (var message in _channel.Reader.ReadAllAsync(token))
        {
            if (!_tags.TryGetValue(message.Handle, out var tag)) continue;

            // Update the watched tag value and trigger notify subscribers.
            if (message.Action == TagAction.ReadCompleted)
            {
                tag.Read(message.Handle);
                NotifySubscribers(tag);
            }
        }
    }

    /// <summary>
    /// Notifies all subscribed clients with the given tag object.
    /// Iterates through the list of subscribers and invokes their callback actions, passing the tag as a parameter.
    /// </summary>
    /// <param name="tag">The tag object containing updated information to notify subscribers about.</param>
    private void NotifySubscribers(Tag tag)
    {
        // We iterate over the Values (the delegates)
        foreach (var callback in _subscribers.Values)
        {
            try
            {
                callback(tag);
            }
            catch (Exception)
            {
                // TODO: Handle subscriber exceptions
            }
        }
    }

    /// <summary>
    /// Updates the read interval for all monitored tags in the current instance.
    /// Applies the new interval to tag handles by configuring the underlying PLC attributes.
    /// </summary>
    /// <param name="value">The new read interval value, in milliseconds, to apply to all tags.</param>
    private void UpdateReadInterval(int value)
    {
        foreach (var handle in _tags.Keys)
        {
            var result = plctag.plc_tag_set_int_attribute(handle, "auto_sync_read_ms", value).AsResult();
            TagException.ThrowIfRequested(result, []);
        }
    }

    /// <summary>
    /// Ensures that the <see cref="PlcClient"/> instance has not been disposed before proceeding with the operation.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the <see cref="PlcClient"/> has already been disposed and can no longer be used.
    /// </exception>
    private void ThrowIfDisposed()
    {
        if (_disposed)
        {
            throw new InvalidOperationException("The client has been disposed and can no longer perform actions.");
        }
    }

    /// <summary>
    /// Provides a mechanism to manage and handle the unsubscription of tag-related callbacks in a monitoring system.
    /// This class is used internally to dispose of subscriptions when they are no longer needed.
    /// </summary>
    private class Unsubscriber(Action unsubscribe) : IDisposable
    {
        public void Dispose() => unsubscribe();
    }
}