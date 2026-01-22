using System;
using System.Collections.Generic;
using L5Sharp.Core;
using L5Sharp.Gateway.Common;

namespace L5Sharp.Gateway.Abstractions;

/// <summary>
/// Represents a subscription to one or more PLC tags that monitors and tracks value changes and errors.
/// Provides access to tag values, status information, and allows registration of callbacks for change and error events.
/// </summary>
public interface ITagSubscription : IDisposable
{
    /// <summary>
    /// Gets a value indicating whether any of the subscribed tags are currently active (not idle).
    /// </summary>
    bool IsActive { get; }

    /// <summary>
    /// Gets a value indicating whether any of the subscribed tags have errors (status less than 0).
    /// </summary>
    bool HasErrors { get; }

    /// <summary>
    /// Gets the overall status of the subscription, representing the minimum status across all subscribed tags.
    /// </summary>
    TagStatus Status { get; }

    /// <summary>
    /// Gets the UTC timestamp of the last value change detected across any of the subscribed tags.
    /// Returns <see cref="DateTime.MinValue"/> if no changes have occurred.
    /// </summary>
    DateTime LastUpdate { get; }

    /// <summary>
    /// Gets the total count of value changes detected across all subscribed tags since the subscription started.
    /// </summary>
    int UpdateCount { get; }

    /// <summary>
    /// Gets a collection of all tags that are part of this subscription.
    /// </summary>
    IEnumerable<Tag> Tags { get; }

    /// <summary>
    /// Gets a collection of all tags that currently have errors, along with their error status.
    /// </summary>
    IEnumerable<TagError> Errors { get; }

    /// <summary>
    /// Retrieves the tag with the specified name from the subscription.
    /// </summary>
    /// <param name="tagName">The name of the tag to retrieve.</param>
    /// <returns>The tag with the specified name.</returns>
    /// <exception cref="KeyNotFoundException">Thrown when the tag with the specified name is not found in the subscription.</exception>
    Tag GetTag(TagName tagName);

    /// <summary>
    /// Attempts to retrieve the tag with the specified name from the subscription.
    /// </summary>
    /// <param name="tagName">The name of the tag to retrieve.</param>
    /// <param name="tag">When this method returns, contains the tag with the specified name if found; otherwise, null.</param>
    /// <returns><c>true</c> if the tag was found; otherwise, <c>false</c>.</returns>
    bool TryGetTag(TagName tagName, out Tag tag);

    /// <summary>
    /// Gets the current status of the tag with the specified name.
    /// </summary>
    /// <param name="tagName">The name of the tag to get the status for.</param>
    /// <returns>The status of the specified tag, or <see cref="TagStatus.NotFound"/> if the tag is not part of the subscription.</returns>
    TagStatus GetStatus(TagName tagName);

    /// <summary>
    /// Registers a callback to be invoked whenever any tag in the subscription changes value.
    /// </summary>
    /// <param name="callback">The action to invoke when any tag value changes, receiving the changed tag.</param>
    /// <returns>The current subscription instance for method chaining.</returns>
    ITagSubscription OnChange(Action<Tag> callback);

    /// <summary>
    /// Registers a callback to be invoked when the specified tag changes value.
    /// </summary>
    /// <param name="tagName">The name of the tag to monitor for changes.</param>
    /// <param name="callback">The action to invoke when the tag value changes, receiving the changed tag.</param>
    /// <returns>The current subscription instance for method chaining.</returns>
    ITagSubscription OnChange(TagName tagName, Action<Tag> callback);

    /// <summary>
    /// Registers a callback to be invoked whenever any tag in the subscription encounters an error.
    /// </summary>
    /// <param name="callback">The action to invoke when any tag encounters an error, receiving the tag and error status.</param>
    /// <returns>The current subscription instance for method chaining.</returns>
    ITagSubscription OnError(Action<Tag, TagStatus> callback);

    /// <summary>
    /// Registers a callback to be invoked when the specified tag encounters an error.
    /// </summary>
    /// <param name="tagName">The name of the tag to monitor for errors.</param>
    /// <param name="callback">The action to invoke when the tag encounters an error, receiving the tag and error status.</param>
    /// <returns>The current subscription instance for method chaining.</returns>
    ITagSubscription OnError(TagName tagName, Action<Tag, TagStatus> callback);
}