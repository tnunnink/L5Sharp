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
    /// Represents a condition used to determine whether the monitoring process for tags should be canceled.
    /// This is a function that evaluates a <see cref="Tag"/> instance and returns a boolean indicating
    /// whether the condition for cancellation has been met.
    /// </summary>
    private Func<Tag, bool>? _cancellationCondition;

    /// <summary>
    /// Represents a class that monitors and manages a collection of PLC tags.
    /// Provides functionality for starting and stopping tag monitoring, subscribing to tag events,
    /// and handling tag event callbacks.
    /// </summary>
    internal TagWatch(ICollection<Tag> tags, string baseUri, PlcOptions options)
    {
        RefreshRate = options.ReadInterval;

        _callbackDelegate = TagEventCallback;
        _baseUri = baseUri;
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
    public int RefreshRate { get; }

    /// <inheritdoc />
    public bool IsRunning { get; private set; }

    /// <inheritdoc />
    public IEnumerable<Tag> Tags => _tags.Values;

    /// <inheritdoc />
    public void Start()
    {
        ThrowIfDisposed();
        ThrowIfRunning();

        _cancellation = new CancellationTokenSource();
        _processor = Task.Run(() => ProcessMessagesAsync(_cancellation.Token));

        IsRunning = true;
    }

    /// <inheritdoc />
    public async Task Stop(CancellationToken token = default)
    {
        ThrowIfDisposed();
        _cancellation?.Cancel();
        if (_processor is not null) await _processor;
    }

    /// <inheritdoc />
    public Task RunFor(int period, CancellationToken token = default)
    {
        ThrowIfDisposed();
        ThrowIfRunning();

        _cancellation = CreateLinkedCancellation(token, period);
        _processor = Task.Run(() => ProcessMessagesAsync(_cancellation.Token), token);

        IsRunning = true;

        //Return the task so the caller can await the result.
        return _processor;
    }

    /// <inheritdoc />
    public Task RunWhile(Func<Tag, bool> predicate, CancellationToken token = default)
    {
        ThrowIfDisposed();
        ThrowIfRunning();

        _cancellationCondition = predicate ?? throw new ArgumentNullException(nameof(predicate));
        _cancellation = CreateLinkedCancellation(token);
        _processor = Task.Run(() => ProcessMessagesAsync(_cancellation.Token), token);

        IsRunning = true;

        //Return the task so the caller can await the result.
        return _processor;
    }

    /// <inheritdoc />
    public IDisposable Subscribe(Action<Tag> callback)
    {
        ThrowIfDisposed();
        ThrowIfRunning();

        if (callback is null)
            throw new ArgumentNullException(nameof(callback));

        var id = Guid.NewGuid();
        _subscribers.TryAdd(id, callback);

        return new Unsubscriber(() => _subscribers.TryRemove(id, out _));
    }

    public void PollAt(int rate)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void Dispose()
    {
        if (_disposed) return;

        if (IsRunning)
        {
            _cancellation?.Cancel();
            ResetWatch();
        }

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
        TagException.ThrowIfRequested(result, _options.ThrowOn, tagName);
        if (result <= 0) return;

        var handle = (int)result;
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
        var message = new TagMessage(handle, eventId, statusId);
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
        try
        {
            await foreach (var message in _channel.Reader.ReadAllAsync(token))
            {
                if (!_tags.TryGetValue(message.Handle, out var tag)) continue;

                if (message.Event == TagEvent.ReadCompleted)
                {
                    tag.ReadValue(message.Handle);
                    NotifySubscribers(tag);
                    if (ShouldCancel(tag)) break;
                }
            }
        }
        catch (OperationCanceledException)
        {
        }
        finally
        {
            ResetWatch();
        }
    }

    /// <summary>
    /// Notifies all subscribed clients with the given tag object.
    /// Iterates through the list of subscribers an invokes their callback actions, passing the tag as a parameter.
    /// </summary>
    /// <param name="tag">The tag object containing updated information to notify subscribers about.</param>
    private void NotifySubscribers(Tag tag)
    {
        foreach (var callback in _subscribers.Values)
        {
            callback(tag);
        }
    }

    /// <summary>
    /// Determines whether the given tag meets the cancellation condition.
    /// This method evaluates a function, if defined, to determine if monitoring for the tag should be canceled.
    /// </summary>
    /// <param name="tag">The tag object to evaluate against the cancellation condition.</param>
    /// <returns>True if the cancellation condition is met for the specified tag; otherwise, false.</returns>
    private bool ShouldCancel(Tag tag)
    {
        return _cancellationCondition?.Invoke(tag) == true;
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
    /// Resets the state of the tag monitoring system to its initial state by disposing of existing resources,
    /// clearing cancellation tokens, and resetting associated properties.
    /// Ensures that the monitoring process can be safely reinitialized or stopped without residual state conflicts.
    /// </summary>
    private void ResetWatch()
    {
        _cancellation?.Dispose();
        _cancellation = null;
        _cancellationCondition = null;
        _processor = null;
        IsRunning = false;
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
    /// Ensures that the tag watch is not currently running before proceeding with the operation.
    /// Throws an exception if the tag watch is in a running state.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the tag watch is already running and the operation cannot be performed.
    /// </exception>
    private void ThrowIfRunning()
    {
        if (IsRunning)
        {
            throw new InvalidOperationException("Tag watch is already running.");
        }
    }

    /// <summary>
    /// Creates a linked <see cref="CancellationTokenSource"/> which combines a new cancellation token with the specified token.
    /// This allows for coordinated cancellation across multiple dependent tokens.
    /// </summary>
    /// <param name="token">The external <see cref="CancellationToken"/> to link with the new cancellation token source.</param>
    /// <returns>A new <see cref="CancellationTokenSource"/> that is linked to the specified token.</returns>
    private static CancellationTokenSource CreateLinkedCancellation(CancellationToken token)
    {
        var local = new CancellationTokenSource();
        return CancellationTokenSource.CreateLinkedTokenSource(local.Token, token);
    }

    /// <summary>
    /// Creates a linked CancellationTokenSource that combines a provided cancellation token and a timeout.
    /// The resulting CancellationTokenSource will be canceled if either the provided token is canceled
    /// or the specified timeout elapses.
    /// </summary>
    /// <param name="token">The existing cancellation token to be linked.</param>
    /// <param name="timeout">The timeout, in milliseconds, after which the token will be canceled.</param>
    /// <returns>A linked CancellationTokenSource that combines the provided token and timeout.</returns>
    private static CancellationTokenSource CreateLinkedCancellation(CancellationToken token, int timeout)
    {
        var local = new CancellationTokenSource(timeout);
        return CancellationTokenSource.CreateLinkedTokenSource(local.Token, token);
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