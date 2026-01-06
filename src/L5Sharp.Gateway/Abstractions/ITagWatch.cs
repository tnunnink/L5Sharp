using System;
using System.Collections.Generic;
using System.Threading;
using L5Sharp.Core;
using Task = System.Threading.Tasks.Task;

namespace L5Sharp.Gateway.Abstractions;

/// <summary>
/// Defines methods for monitoring and managing updates to PLC tags in real-time.
/// </summary>
public interface ITagWatch : IDisposable
{
    /// <summary>
    /// Gets the interval, in milliseconds, at which the tag values are read from the PLC. This property defines
    /// the time between successive poll requests to retrieve updated tag data during active monitoring.
    /// </summary>
    int RefreshRate { get; }

    /// <summary>
    /// Gets a value indicating whether the tag watch is currently active and processing updates.
    /// </summary>
    bool IsRunning { get; }

    /// <summary>
    /// Gets the collection of tags currently being monitored by the tag watch. This collection contains all tags
    /// that have been added for surveillance and allows interaction with their states or values.
    /// </summary>
    IEnumerable<Tag> Tags { get; }

    /// <summary>
    /// Initiates the monitoring and background processing of PLC tag updates.
    /// Ensures that the tag data is kept current by starting the underlying update mechanism.
    /// </summary>
    void Start();

    /// <summary>
    /// Stops the background processing of tag updates. Native polling may continue, 
    /// but updates will no longer be applied to the Tag instances until started again.
    /// </summary>
    Task Stop(CancellationToken token = default);

    /// <summary>
    /// Executes the tag monitoring process for a specified duration.
    /// </summary>
    /// <param name="period">The duration, in milliseconds, for which to run the monitoring process.</param>
    /// <param name="token">The cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous monitoring operation.</returns>
    Task RunFor(int period, CancellationToken token = default);

    /// <summary>
    /// Continuously monitors and processes tags while the specified predicate evaluates to true.
    /// </summary>
    /// <param name="predicate">A function that tests each tag to determine whether to continue processing.</param>
    /// <param name="token">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous execution of the monitoring operation.</returns>
    Task RunWhile(Func<Tag, bool> predicate, CancellationToken token = default);

    /// <summary>
    /// Subscribes to tag updates, allowing the provided callback to execute whenever a tag is updated.
    /// </summary>
    /// <param name="callback">A callback action to be invoked when a tag update occurs.</param>
    IDisposable Subscribe(Action<Tag> callback);

    /// <summary>
    /// Sets the polling rate for monitoring PLC tag updates.
    /// Adjusts the frequency, in milliseconds, at which tag updates are polled.
    /// </summary>
    /// <param name="rate">The desired polling rate, in milliseconds. Must be a positive integer.</param>
    void PollAt(int rate);
}