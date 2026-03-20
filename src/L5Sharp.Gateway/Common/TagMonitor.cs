using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Core;
using L5Sharp.Gateway.Abstractions;
using L5Sharp.Gateway.Internal;

namespace L5Sharp.Gateway.Common;

/// <summary>
/// Represents a monitor for a collection of PLC tags, enabling observation of tag updates, status,
/// and errors, as well as providing mechanisms for subscribing to various tag events.
/// </summary>
public class TagMonitor : IDisposable
{
    private const string GlobalKey = "*";
    private readonly ICollection<TagWatch> _watches;
    private readonly Action<TagWatch> _unsubscriber;
    private readonly ConcurrentDictionary<string, Action<TagResult>> _updateCallbacks = [];
    private readonly ConcurrentDictionary<string, Action<TagResult>> _errorCallbacks = [];
    private readonly ConcurrentDictionary<string, Action<TagResult>> _changeCallbacks = [];

    internal TagMonitor(ICollection<TagWatch> watches, Action<TagWatch> unsubscriber)
    {
        _watches = watches ?? throw new ArgumentNullException(nameof(watches));
        _unsubscriber = unsubscriber ?? throw new ArgumentNullException(nameof(unsubscriber));

        RegisterWatches(_watches);
    }

    /// <summary>
    /// Gets a value indicating whether any of the monitored tags are currently active (polling for updates).
    /// </summary>
    /// <remarks>
    /// This should only become false if the <see cref="IPlcClient"/> that created the monitor is
    /// disposed before this monitor instance.
    /// </remarks>
    public bool IsActive => _watches.Any(x => !x.IsIdle);

    /// <summary>
    /// Gets a value indicating whether any of the monitored tags are currently in an error state.
    /// </summary>
    /// <remarks>
    /// This property evaluates all monitored tags and returns true if any tag has encountered an error.
    /// An error state is determined based on the <see cref="TagWatch.Status"/> of each tag being less than zero.
    /// </remarks>
    public bool HasErrors => _watches.Any(x => x.IsErrored);

    /// <summary>
    /// Gets the current status of the monitored tags, reflecting the least favorable status among them.
    /// </summary>
    /// <remarks>
    /// The returned status is derived by evaluating all monitored tags and selecting the one with the lowest status value.
    /// This can provide insight into the overall health or state of the monitored system.
    /// </remarks>
    public TagStatus Status => _watches.Min(x => x.Status);

    /// <summary>
    /// Gets the most recent timestamp from the monitored tags.
    /// </summary>
    /// <remarks>
    /// The timestamp represents the latest update time among all the tags currently being monitored.
    /// It is determined by identifying the maximum <c>Timestamp</c> value from the underlying tag watches.
    /// </remarks>
    public DateTime Timestamp => _watches.Max(x => x.Timestamp);

    /// <summary>
    /// Gets the average rate at which the monitored tags are being updated.
    /// </summary>
    /// <remarks>
    /// The property calculates the average duration across all active tag watches, representing the polling frequency.
    /// A lower value indicates faster update cycles, while a higher value represents slower update intervals.
    /// </remarks>
    public TimeSpan Rate => TimeSpan.FromMilliseconds(_watches.Average(x => x.Duration.TotalMilliseconds));

    /// <summary>
    /// Gets the total number of updates received across all monitored tags within the current instance.
    /// </summary>
    /// <remarks>
    /// This value represents the cumulative count of updates processed for the monitored tags
    /// and can be used to evaluate the activity level or usage of the monitor over time.
    /// </remarks>
    public int Updates => _watches.Sum(x => x.Updates);

    /// <summary>
    /// Gets a collection of tags currently being monitored by the system.
    /// </summary>
    /// <remarks>
    /// The collection includes distinct base tags grouped by their tag name,
    /// representing the unique set of tags actively tracked for updates or changes.
    /// </remarks>
    public IEnumerable<Tag> Tags =>
        _watches.Select(x => x.Tag.Base).GroupBy(t => t.TagName).Select(t => t.First()).ToArray();

    /// <summary>
    /// Gets a collection of errors associated with the monitored tags.
    /// </summary>
    /// <remarks>
    /// Each error in the collection contains information about the tag that encountered the issue
    /// and the related error status.
    /// </remarks>
    public IEnumerable<TagError> Errors =>
        _watches.Where(x => x.Status < 0).Select(x => new TagError(x.Tag.TagName, x.Status)).ToArray();

    /// <summary>
    /// Retrieves the result of the specified tag based on the current monitoring data.
    /// </summary>
    /// <param name="tagName">
    /// The name of the tag for which the result is requested.
    /// Supports both member and base tag syntax.
    /// </param>
    /// <returns>
    /// A <see cref="TagResult"/> representing the aggregated result of the monitored tag,
    /// including any associated errors and the monitoring duration.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown if no tags with the specified name are currently being monitored.
    /// </exception>
    public TagResult GetResult(TagName tagName)
    {
        var watches = _watches.Where(w => w.Tag.TagName.IsMemberOrSelf(tagName)).ToArray();

        if (watches.Length == 0)
            throw new ArgumentException($"No tags with name '{tagName}' are being monitored.");

        // Get the single common denominator for all tags, aggregate the errors and duration, and build the result.
        var tag = watches.First().Tag.Base.Members(t => t.TagName.Equals(tagName)).First();
        var errors = watches.Where(w => w.Status < 0).Select(w => new TagError(w.Tag.TagName, w.Status)).ToArray();
        var duration = TimeSpan.FromMilliseconds(watches.Average(w => w.Duration.TotalMilliseconds));

        return TagResult.Create(tag, errors, duration);
    }

    /// <summary>
    /// Retrieves the current status of the specified tag being monitored.
    /// </summary>
    /// <param name="tagName">
    /// The tag name for which the monitoring status is requested.
    /// Supports both member and base tag syntax.
    /// </param>
    /// <returns>A <see cref="TagStatus"/> representing the current status of the tag being monitored.</returns>
    /// <exception cref="ArgumentException">
    /// Thrown if no tags with the specified name are being monitored.
    /// </exception>
    public TagStatus GetStatus(TagName tagName)
    {
        var watches = _watches.Where(t => t.Tag.TagName.IsMemberOrSelf(tagName)).ToArray();

        if (watches.Length == 0)
            throw new ArgumentException($"No tags with name '{tagName}' are being monitored.");

        return watches.Min(w => w.Status);
    }

    /// <summary>
    /// Registers a callback invoked whenever any monitored tag member is updated (polled),
    /// regardless of whether its value changed.
    /// </summary>
    /// <param name="callback">
    /// The callback to invoke with the latest tag result. For composite tags (e.g., UDTs, arrays), the callback
    /// is raised per atomic member, so a single logical tag may produce multiple invocations.
    /// </param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="callback"/> is null.</exception>
    /// <remarks>
    /// Callbacks are executed synchronously on the thread that processes tag updates. Keep callbacks fast and
    /// offload long-running work to avoid slowing down update delivery.
    /// </remarks>
    public void OnUpdate(Action<TagResult> callback)
    {
        if (callback is null)
            throw new ArgumentNullException(nameof(callback));

        _updateCallbacks.AddOrUpdate(GlobalKey, callback, (_, _) => callback);
    }

    /// <summary>
    /// Registers a callback invoked whenever the specified tag is updated (polled),
    /// regardless of whether its value changed.
    /// </summary>
    /// <param name="tagName">
    /// The tag to monitor. If <paramref name="tagName"/> is a member (atomic) tag name, the callback is invoked only for that member,
    /// and the payload contains that member tag.
    /// If <paramref name="tagName"/> is a valid base tag name (of a matching member in the monitor),
    /// the callback is invoked when any atomic member of that base tag is updated, and the payload contains the base tag.
    /// </param>
    /// <param name="callback">
    /// The callback to invoke with the latest tag result. The payload will depend on the <paramref name="tagName"/>
    /// the callback is registered to.
    /// </param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="callback"/> is null.</exception>
    /// <remarks>
    /// Callbacks are executed synchronously on the thread that processes tag updates. Keep callbacks fast and
    /// offload long-running work to avoid slowing down update delivery.
    /// </remarks>
    public void OnUpdate(TagName tagName, Action<TagResult> callback)
    {
        if (callback is null)
            throw new ArgumentNullException(nameof(callback));

        _updateCallbacks.AddOrUpdate(tagName, callback, (_, _) => callback);
    }

    /// <summary>
    /// Registers a callback invoked whenever any monitored tag member experiences a value change.
    /// </summary>
    /// <param name="callback">
    /// The callback to invoke with the latest tag result. For composite tags (e.g., UDTs, arrays), the callback
    /// is raised per atomic member, so a single logical tag may produce multiple invocations.
    /// </param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="callback"/> is null.</exception>
    /// <remarks>
    /// Callbacks are executed synchronously on the thread that processes tag updates. Keep callbacks fast and
    /// offload long-running work to avoid slowing down update delivery.
    /// </remarks>
    public void OnChange(Action<TagResult> callback)
    {
        if (callback is null)
            throw new ArgumentNullException(nameof(callback));

        _changeCallbacks.AddOrUpdate(GlobalKey, callback, (_, _) => callback);
    }

    /// <summary>
    /// Registers a callback invoked whenever the specified tag experiences a value change.
    /// </summary>
    /// <param name="tagName">
    /// The tag to monitor. If <paramref name="tagName"/> is a member (atomic) tag name, the callback is invoked only for that member,
    /// and the payload contains that member tag.
    /// If <paramref name="tagName"/> is a valid base tag name (of a matching member in the monitor),
    /// the callback is invoked when any atomic member of that base tag changes, and the payload contains the base tag.
    /// </param>
    /// <param name="callback">
    /// The callback to invoke with the latest tag result. The payload will depend on the <paramref name="tagName"/>
    /// the callback is registered to.
    /// </param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="callback"/> is null.</exception>
    /// <remarks>
    /// Callbacks are executed synchronously on the thread that processes tag updates. Keep callbacks fast and
    /// offload long-running work to avoid slowing down update delivery.
    /// </remarks>
    public void OnChange(TagName tagName, Action<TagResult> callback)
    {
        if (callback is null)
            throw new ArgumentNullException(nameof(callback));

        _changeCallbacks.AddOrUpdate(tagName, callback, (_, _) => callback);
    }

    /// <summary>
    /// Registers a callback invoked whenever any monitored tag member encounters an error.
    /// </summary>
    /// <param name="callback">
    /// The callback to invoke with the latest tag result. For composite tags (e.g., UDTs, arrays), the callback
    /// is raised per atomic member, so a single logical tag may produce multiple invocations.
    /// </param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="callback"/> is null.</exception>
    /// <remarks>
    /// Callbacks are executed synchronously on the thread that processes tag updates. Keep callbacks fast and
    /// offload long-running work to avoid slowing down update delivery.
    /// </remarks>
    public void OnError(Action<TagResult> callback)
    {
        if (callback is null)
            throw new ArgumentNullException(nameof(callback));

        _errorCallbacks.AddOrUpdate(GlobalKey, callback, (_, _) => callback);
    }

    /// <summary>
    /// Registers a callback invoked whenever the specified tag encounters an error.
    /// </summary>
    /// <param name="tagName">
    /// The tag to monitor. If <paramref name="tagName"/> is a member (atomic) tag name, the callback is invoked only for that member,
    /// and the payload contains that member tag.
    /// If <paramref name="tagName"/> is a valid base tag name (of a matching member in the monitor),
    /// the callback is invoked when any atomic member of that base tag errors, and the payload contains the base tag.
    /// </param>
    /// <param name="callback">
    /// The callback to invoke with the latest tag result. The payload will depend on the <paramref name="tagName"/>
    /// the callback is registered to.
    /// </param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="callback"/> is null.</exception>
    /// <remarks>
    /// Callbacks are executed synchronously on the thread that processes tag updates. Keep callbacks fast and
    /// offload long-running work to avoid slowing down update delivery.
    /// </remarks>
    public void OnError(TagName tagName, Action<TagResult> callback)
    {
        if (callback is null)
            throw new ArgumentNullException(nameof(callback));

        _errorCallbacks.AddOrUpdate(tagName, callback, (_, _) => callback);
    }

    /// <summary>
    /// Releases all resources, unsubscribes from tag watch events, and clears all registered callbacks.
    /// </summary>
    public void Dispose()
    {
        foreach (var watch in _watches)
        {
            watch.Updated -= OnWatchUpdated;
            watch.Changed -= OnWatchChanged;
            watch.Errored -= OnWatchErrored;
            _unsubscriber(watch);
        }

        _watches.Clear();
        _updateCallbacks.Clear();
        _errorCallbacks.Clear();
        _changeCallbacks.Clear();
    }

    /// <summary>
    /// Registers a collection of tag watches and subscribes to their change and error events.
    /// </summary>
    /// <param name="watches">The collection of tag watches to be registered.</param>
    private void RegisterWatches(ICollection<TagWatch> watches)
    {
        foreach (var watch in watches)
        {
            watch.Updated += OnWatchUpdated;
            watch.Changed += OnWatchChanged;
            watch.Errored += OnWatchErrored;
        }
    }

    /// <summary>
    /// Handles updates to a specific tag watch and invokes registered callbacks with the updated tag information.
    /// </summary>
    /// <param name="watch">The tag watch instance that triggered the update event.</param>
    private void OnWatchUpdated(TagWatch watch)
    {
        var memberResult = TagResult.Create(watch.Tag, watch.Status, watch.Duration);

        // If a global callback is registered, pass any member result tag we receive.
        if (_updateCallbacks.TryGetValue(GlobalKey, out var globalCallback))
            globalCallback.Invoke(memberResult);

        // If the user registered a callback for this member tag, send a result with the matching member tag.
        if (_updateCallbacks.TryGetValue(watch.Tag.TagName, out var memberCallback))
            memberCallback.Invoke(memberResult);

        // If the user registered a callback for the base tag, send a result with the member's base tag.
        if (_updateCallbacks.TryGetValue(watch.Tag.Base.TagName, out var baseCallback))
        {
            var baseResult = TagResult.Create(watch.Tag.Base, watch.Status, watch.Duration);
            baseCallback.Invoke(baseResult);
        }
    }

    /// <summary>
    /// Handles the event triggered when a tag watch experiences a change in its state or value.
    /// Generates results for the changed tag and invokes the appropriate registered callbacks.
    /// </summary>
    /// <param name="watch">The tag watch instance that has changed.</param>
    private void OnWatchChanged(TagWatch watch)
    {
        var memberResult = TagResult.Create(watch.Tag, watch.Status, watch.Duration);

        // If a global callback is registered, pass any member result tag we receive.
        if (_changeCallbacks.TryGetValue(GlobalKey, out var globalCallback))
            globalCallback.Invoke(memberResult);

        // If the user registered a callback for this member tag, send a result with the matching member tag.
        if (_changeCallbacks.TryGetValue(watch.Tag.TagName, out var memberCallback))
            memberCallback.Invoke(memberResult);

        // If the user registered a callback for the base tag, send a result with the member's base tag.
        if (_changeCallbacks.TryGetValue(watch.Tag.Base.TagName, out var baseCallback))
        {
            var baseResult = TagResult.Create(watch.Tag.Base, watch.Status, watch.Duration);
            baseCallback.Invoke(baseResult);
        }
    }

    /// <summary>
    /// Handles the error event for a tag watch by invoking registered error callbacks with the relevant tag result data.
    /// </summary>
    /// <param name="watch">The tag watch instance that encountered an error.</param>
    private void OnWatchErrored(TagWatch watch)
    {
        var memberResult = TagResult.Create(watch.Tag, watch.Status, watch.Duration);

        // If a global callback is registered, pass any member result tag we receive.
        if (_errorCallbacks.TryGetValue(GlobalKey, out var globalCallback))
            globalCallback.Invoke(memberResult);

        // If the user registered a callback for this member tag, send a result with the matching member tag.
        if (_errorCallbacks.TryGetValue(watch.Tag.TagName, out var memberCallback))
            memberCallback.Invoke(memberResult);

        // If the user registered a callback for the base tag, send a result with the member's base tag.
        if (_errorCallbacks.TryGetValue(watch.Tag.Base.TagName, out var baseCallback))
        {
            var baseResult = TagResult.Create(watch.Tag.Base, watch.Status, watch.Duration);
            baseCallback.Invoke(baseResult);
        }
    }
}