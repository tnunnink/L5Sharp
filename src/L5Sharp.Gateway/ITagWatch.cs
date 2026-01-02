using System;
using System.Collections.Generic;
using System.Threading;
using L5Sharp.Core;
using Task = System.Threading.Tasks.Task;

namespace L5Sharp.Gateway;

/// <summary>
/// Defines methods for monitoring and managing updates to PLC tags in real-time.
/// </summary>
public interface ITagWatch : IDisposable
{
    /// <summary>
    /// Gets a value indicating whether the tag watch is currently active and processing updates.
    /// </summary>
    bool IsRunning { get; }

    /// <summary>
    /// Gets the interval, in milliseconds, at which the tag values are read from the PLC. This property defines
    /// the time between successive poll requests to retrieve updated tag data during active monitoring.
    /// </summary>
    int RefreshRate { get; }

    /// <summary>
    /// Gets the collection of tags currently being monitored by the tag watch. This collection contains all tags
    /// that have been added for surveillance and allows interaction with their states or values.
    /// </summary>
    IEnumerable<Tag> Tags { get; }

    /// <summary>
    /// Starts the background processing of tag updates from the PLC.
    /// </summary>
    Task StartAsync(CancellationToken token = default);

    /// <summary>
    /// Stops the background processing of tag updates. Native polling may continue, 
    /// but updates will no longer be applied to the Tag instances until started again.
    /// </summary>
    Task StopAsync(CancellationToken token = default);

    /// <summary>
    /// Subscribes to tag updates, allowing the provided callback to execute whenever a tag is updated.
    /// </summary>
    /// <param name="callback">A callback action to be invoked when a tag update occurs.</param>
    IDisposable Subscribe(Action<Tag> callback);
}