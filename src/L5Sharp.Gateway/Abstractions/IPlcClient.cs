using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using L5Sharp.Core;
using L5Sharp.Gateway.Common;

namespace L5Sharp.Gateway.Abstractions;

/// <summary>
/// Defines the operations to interact with a PLC for reading, writing, and monitoring tags,
/// as well as verifying connectivity.
/// </summary>
public interface IPlcClient : IDisposable
{
    /// <summary>
    /// Verifies the connectivity to the PLC by performing a ping operation asynchronously.
    /// </summary>
    /// <param name="token">A cancellation token that can be used to cancel the operation before completion.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result indicates whether the ping operation
    /// was successful.
    /// </returns>
    Task<bool> Ping(CancellationToken token = default);

    /// <summary>
    /// Reads the value of a specified tag from the PLC asynchronously and returns the response.
    /// </summary>
    /// <param name="tagName">The unique name of the tag to be read from the PLC.</param>
    /// <param name="token">A cancellation token that can be used to cancel the operation before completion.</param>
    /// <typeparam name="TData">
    /// The data type of the tag being read. This type must derive from <see cref="LogixData"/>
    /// and have a parameterless constructor.
    /// </typeparam>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the response for the read tag operation.
    /// </returns>
    Task<TagResult> ReadTag<TData>(TagName tagName, CancellationToken token = default)
        where TData : LogixData, new();

    /// <summary>
    /// Reads the specified tag value from the PLC asynchronously.
    /// </summary>
    /// <param name="tag">The tag to be read from the PLC, including its name and metadata.</param>
    /// <param name="token">A cancellation token that can be used to cancel the operation before completion.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the result of the
    /// tag read operation, including the status and associated metadata.
    /// </returns>
    Task<TagResult> ReadTag(Tag tag, CancellationToken token = default);

    /// <summary>
    /// Reads multiple tags from the PLC asynchronously.
    /// </summary>
    /// <param name="tags">A collection of tags to be read from the PLC.</param>
    /// <param name="token">A cancellation token that can be used to cancel the operation before completion.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the response
    /// indicating the success of the read operation for the provided tags.
    /// </returns>
    Task<TagResults> ReadTags(IEnumerable<Tag> tags, CancellationToken token = default);

    /// <summary>
    /// Writes a specified value to the specified tag within the PLC asynchronously.
    /// </summary>
    /// <param name="tagName">The name of the tag to be written.</param>
    /// <param name="data">The value to be written to the specified tag.</param>
    /// <param name="token">A cancellation token that can be used to cancel the operation before completion.</param>
    /// <typeparam name="TData">The type of the data to write, which must be a subclass of <see cref="LogixData"/>.</typeparam>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a <see cref="TagResults"/>
    /// indicating the status of the write operation.
    /// </returns>
    Task<TagResult> WriteTag<TData>(TagName tagName, TData data, CancellationToken token = default)
        where TData : LogixData, new();

    /// <summary>
    /// Writes a specified tag asynchronously by applying an update operation to the existing tag data.
    /// </summary>
    /// <param name="tagName">The name of the tag to be written.</param>
    /// <param name="update">An action that specifies the update operation to be applied to the tag data.</param>
    /// <param name="token">A cancellation token that can be used to cancel the operation before completion.</param>
    /// <typeparam name="TData">The type of the tag data, which must inherit from <see cref="LogixData"/>.</typeparam>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a <see cref="TagResults"/>
    /// indicating the result of the write operation.
    /// </returns>
    Task<TagResult> WriteTag<TData>(TagName tagName, Action<TData> update, CancellationToken token = default)
        where TData : LogixData, new();

    /// <summary>
    /// Writes the specified tag value to the PLC asynchronously.
    /// </summary>
    /// <param name="tag">The tag to be written to the PLC, including its name and metadata.</param>
    /// <param name="token">A cancellation token that can be used to cancel the operation before completion.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the status of the
    /// tag write operation, including any associated metadata.
    /// </returns>
    Task<TagResult> WriteTag(Tag tag, CancellationToken token = default);

    /// <summary>
    /// Writes multiple tags to the PLC asynchronously.
    /// </summary>
    /// <param name="tags">A collection of tags to be written to the PLC.</param>
    /// <param name="token">A cancellation token that can be used to cancel the operation before completion.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a <see cref="TagResults"/>
    /// object indicating the result of the write operation, including success status and any errors encountered.
    /// </returns>
    Task<TagResults> WriteTags(IEnumerable<Tag> tags, CancellationToken token = default);

    /// <summary>
    /// Starts monitoring a specified tag from the PLC asynchronously and provides continuous updates on its value changes.
    /// </summary>
    /// <param name="tagName">The name of the tag to be monitored.</param>
    /// <param name="token">A cancellation token that can be used to cancel the monitoring operation before completion.</param>
    /// <typeparam name="TData">
    /// The data type of the tag being monitored. This type must derive from <see cref="LogixData"/>
    /// and have a parameterless constructor.
    /// </typeparam>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains an <see cref="TagMonitor"/>
    /// instance that can be used to subscribe to tag updates and manage the monitoring lifecycle.
    /// </returns>
    Task<TagMonitor> MonitorTag<TData>(TagName tagName, CancellationToken token = default)
        where TData : LogixData, new();

    /// <summary>
    /// Starts monitoring a specified tag from the PLC asynchronously and provides continuous updates on its value changes.
    /// </summary>
    /// <param name="tag">The tag to be monitored from the PLC, including its name and metadata.</param>
    /// <param name="token">A cancellation token that can be used to cancel the monitoring operation before completion.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains an <see cref="TagMonitor"/>
    /// instance that can be used to subscribe to tag updates and manage the monitoring lifecycle.
    /// </returns>
    Task<TagMonitor> MonitorTag(Tag tag, CancellationToken token = default);

    /// <summary>
    /// Starts monitoring multiple tags from the PLC asynchronously and provides continuous updates on their value changes.
    /// </summary>
    /// <param name="tags">A collection of tags to be monitored from the PLC.</param>
    /// <param name="token">A cancellation token that can be used to cancel the monitoring operation before completion.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains an <see cref="TagMonitor"/>
    /// instance that can be used to subscribe to tag updates and manage the monitoring lifecycle for all specified tags.
    /// </returns>
    Task<TagMonitor> MonitorTags(IEnumerable<Tag> tags, CancellationToken token = default);

    /// <summary>
    /// Continuously polls a specified tag on the PLC at a defined interval for a specific duration.
    /// </summary>
    /// <param name="tagName">The name of the tag to be polled on the PLC.</param>
    /// <param name="duration">The total duration over which the tag should be polled.</param>
    /// <param name="token">A cancellation token that can be used to cancel the operation before completion.</param>
    /// <typeparam name="TData">The data type of the tag being polled. Must inherit from <see cref="LogixData"/>.</typeparam>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the final <see cref="TagResult"/>
    /// of the tag polling operation.
    /// </returns>
    Task<TagResult> PollTag<TData>(TagName tagName, TimeSpan duration, CancellationToken token = default)
        where TData : LogixData, new();

    /// <summary>
    /// Continuously polls a specified tag on the PLC for data asynchronously over a given duration.
    /// </summary>
    /// <param name="tag">The tag object that specifies the PLC tag to be polled.</param>
    /// <param name="duration">The duration for which the polling operation should run.</param>
    /// <param name="token">A cancellation token that can be used to cancel the operation before completion.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the final <see cref="TagResult"/>
    /// of the tag polling operation.
    /// </returns>
    Task<TagResult> PollTag(Tag tag, TimeSpan duration, CancellationToken token = default);

    /// <summary>
    /// Polls the specified tag asynchronously until the predicate is satisfied or the timeout occurs.
    /// </summary>
    /// <param name="tagName">The name of the tag to be polled on the PLC.</param>
    /// <param name="predicate">A function that evaluates the tag value and returns true when the desired condition is met.</param>
    /// <param name="timeout">An optional timeout to limit the polling duration. If not specified, the polling duration is indefinite.</param>
    /// <param name="token">A cancellation token that can be used to cancel the operation before completion.</param>
    /// <typeparam name="TData">The type of logix data returned by the tag. Must derive from <see cref="LogixData"/> and have a parameterless constructor.</typeparam>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the final <see cref="TagResult"/>
    /// of the tag polling operation.
    /// </returns>
    Task<TagResult> PollTag<TData>(TagName tagName, Func<TData, bool> predicate, TimeSpan? timeout = null,
        CancellationToken token = default) where TData : LogixData, new();

    /// <summary>
    /// Polls the specified tag asynchronously until the predicate is satisfied or the timeout occurs.
    /// </summary>
    /// <param name="tag">The tag to poll from the PLC.</param>
    /// <param name="predicate">A function that evaluates the tag value and returns true when the desired condition is met.</param>
    /// <param name="timeout">An optional timeout to limit the polling duration. If not specified, the polling duration is indefinite.</param>
    /// <param name="token">A cancellation token to cancel the polling operation before completion.</param>
    /// <typeparam name="TData">The type of logix data returned by the tag. Must derive from <see cref="LogixData"/> and have a parameterless constructor.</typeparam>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the final <see cref="TagResult"/>
    /// of the tag polling operation.
    /// </returns>
    Task<TagResult> PollTag<TData>(Tag tag, Func<TData, bool> predicate, TimeSpan? timeout = null,
        CancellationToken token = default) where TData : LogixData, new();
}