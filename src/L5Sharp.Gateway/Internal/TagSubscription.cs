using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Core;
using L5Sharp.Gateway.Abstractions;
using L5Sharp.Gateway.Common;

namespace L5Sharp.Gateway.Internal;

internal class TagSubscription : ITagSubscription
{
    private const string GlobalKey = "*";
    private readonly ICollection<TagWatch> _watches;
    private readonly Action<TagWatch> _unsubscriber;
    private readonly ConcurrentDictionary<string, Action<Tag>> _changeCallbacks = [];
    private readonly ConcurrentDictionary<string, Action<Tag, TagStatus>> _errorCallbacks = [];

    public TagSubscription(ICollection<TagWatch> watches, Action<TagWatch> unsubscriber)
    {
        _watches = watches ?? throw new ArgumentNullException(nameof(watches));
        _unsubscriber = unsubscriber ?? throw new ArgumentNullException(nameof(unsubscriber));

        RegisterWatches(_watches);
    }

    /// <inheritdoc />
    public bool IsActive => _watches.Any(x => !x.IsIdle);

    /// <inheritdoc />
    public bool HasErrors => _watches.Any(x => x.IsErrored);

    /// <inheritdoc />
    public TagStatus Status => _watches.Min(x => x.Status);

    /// <inheritdoc />
    public DateTime LastUpdate => _watches.Max(x => x.Timestamp);

    /// <inheritdoc />
    public int UpdateCount => _watches.Sum(x => x.Updates);

    /// <inheritdoc />
    public IEnumerable<Tag> Tags => _watches.Select(x => x.Tag).ToArray();

    /// <inheritdoc />
    public IEnumerable<TagError> Errors =>
        _watches.Where(x => x.Status < 0).Select(x => new TagError(x.Tag.TagName, x.Status)).ToArray();

    /// <inheritdoc />
    public Tag GetTag(TagName tagName)
    {
        var watch = _watches.SingleOrDefault(t => t.Tag.TagName == tagName);

        if (watch is null)
            throw new KeyNotFoundException("");

        return watch.Tag;
    }

    /// <inheritdoc />
    public bool TryGetTag(TagName tagName, out Tag tag)
    {
        var watch = _watches.SingleOrDefault(t => t.Tag.TagName == tagName);

        if (watch is null)
        {
            tag = null!;
            return false;
        }

        tag = watch.Tag;
        return true;
    }

    /// <inheritdoc />
    public TagStatus GetStatus(TagName tagName)
    {
        var watch = _watches.SingleOrDefault(t => t.Tag.TagName == tagName);

        if (watch is null)
            return TagStatus.NotFound;

        return watch.Status;
    }

    /// <inheritdoc />
    public ITagSubscription OnChange(Action<Tag> callback)
    {
        if (callback is null) throw new ArgumentNullException(nameof(callback));
        _changeCallbacks.AddOrUpdate(GlobalKey, callback, (_, _) => callback);
        return this;
    }

    /// <inheritdoc />
    public ITagSubscription OnChange(TagName tagName, Action<Tag> callback)
    {
        if (callback is null) throw new ArgumentNullException(nameof(callback));
        _changeCallbacks.AddOrUpdate(tagName, callback, (_, _) => callback);
        return this;
    }

    /// <inheritdoc />
    public ITagSubscription OnError(Action<Tag, TagStatus> callback)
    {
        if (callback is null) throw new ArgumentNullException(nameof(callback));
        _errorCallbacks.AddOrUpdate(GlobalKey, callback, (_, _) => callback);
        return this;
    }

    /// <inheritdoc />
    public ITagSubscription OnError(TagName tagName, Action<Tag, TagStatus> callback)
    {
        if (callback is null) throw new ArgumentNullException(nameof(callback));
        _errorCallbacks.AddOrUpdate(tagName, callback, (_, _) => callback);
        return this;
    }

    /// <inheritdoc />
    public void Dispose()
    {
        foreach (var watch in _watches)
        {
            watch.Changed -= OnWatchChanged;
            watch.Errored -= OnWatchErrored;
            _unsubscriber(watch);
        }

        _watches.Clear();
        _changeCallbacks.Clear();
        _errorCallbacks.Clear();
    }

    /// <summary>
    /// Registers a collection of tag watches and subscribes to their change and error events.
    /// </summary>
    /// <param name="watches">The collection of tag watches to be registered.</param>
    private void RegisterWatches(ICollection<TagWatch> watches)
    {
        foreach (var watch in watches)
        {
            watch.Changed += OnWatchChanged;
            watch.Errored += OnWatchErrored;
        }
    }

    /// <summary>
    /// Handles the event triggered when the state of a watched tag changes.
    /// Updates the timestamp of the last change and invokes registered callbacks for the changed tag or global subscriptions.
    /// </summary>
    /// <param name="tag">The tag that has experienced a change.</param>
    private void OnWatchChanged(Tag tag)
    {
        if (_changeCallbacks.TryGetValue(GlobalKey, out var global))
        {
            global.Invoke(tag);
        }

        if (_changeCallbacks.TryGetValue(tag.TagName, out var callback))
        {
            callback.Invoke(tag);
        }
    }

    /// <summary>
    /// Handles the error event for a watched tag by invoking registered error callbacks.
    /// </summary>
    /// <param name="tag">The tag that encountered an error.</param>
    /// <param name="status">The status information associated with the error.</param>
    private void OnWatchErrored(Tag tag, TagStatus status)
    {
        if (_errorCallbacks.TryGetValue(GlobalKey, out var global))
        {
            global.Invoke(tag, status);
        }

        if (_errorCallbacks.TryGetValue(tag.TagName, out var callback))
        {
            callback.Invoke(tag, status);
        }
    }
}