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
    Task<TagResponse> ReadTag<TData>(TagName tagName, CancellationToken token = default)
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
    Task<TagResponse> ReadTag(Tag tag, CancellationToken token = default);

    /// <summary>
    /// Reads multiple tags from the PLC asynchronously.
    /// </summary>
    /// <param name="tags">A collection of tags to be read from the PLC.</param>
    /// <param name="token">A cancellation token that can be used to cancel the operation before completion.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the response
    /// indicating the success of the read operation for the provided tags.
    /// </returns>
    Task<TagResponse> ReadTags(IEnumerable<Tag> tags, CancellationToken token = default);

    /// <summary>
    /// Writes a specified value to the specified tag within the PLC asynchronously.
    /// </summary>
    /// <param name="tagName">The name of the tag to be written.</param>
    /// <param name="data">The value to be written to the specified tag.</param>
    /// <param name="token">A cancellation token that can be used to cancel the operation before completion.</param>
    /// <typeparam name="TData">The type of the data to write, which must be a subclass of <see cref="LogixData"/>.</typeparam>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a <see cref="TagResponse"/>
    /// indicating the status of the write operation.
    /// </returns>
    Task<TagResponse> WriteTag<TData>(TagName tagName, TData data, CancellationToken token = default)
        where TData : LogixData, new();

    /// <summary>
    /// Writes a specified tag asynchronously by applying an update operation to the existing tag data.
    /// </summary>
    /// <param name="tagName">The name of the tag to be written.</param>
    /// <param name="update">An action that specifies the update operation to be applied to the tag data.</param>
    /// <param name="token">A cancellation token that can be used to cancel the operation before completion.</param>
    /// <typeparam name="TData">The type of the tag data, which must inherit from <see cref="LogixData"/>.</typeparam>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a <see cref="TagResponse"/>
    /// indicating the result of the write operation.
    /// </returns>
    Task<TagResponse> WriteTag<TData>(TagName tagName, Action<TData> update, CancellationToken token = default)
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
    Task<TagResponse> WriteTag(Tag tag, CancellationToken token = default);

    /// <summary>
    /// Writes multiple tags to the PLC asynchronously.
    /// </summary>
    /// <param name="tags">A collection of tags to be written to the PLC.</param>
    /// <param name="token">A cancellation token that can be used to cancel the operation before completion.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a <see cref="TagResponse"/>
    /// object indicating the result of the write operation, including success status and any errors encountered.
    /// </returns>
    Task<TagResponse> WriteTags(IEnumerable<Tag> tags, CancellationToken token = default);

    /// <summary>
    /// Subscribes to a tag on the PLC and monitors it for changes asynchronously.
    /// </summary>
    /// <param name="tagName">The name of the tag to monitor.</param>
    /// <param name="onChange">An optional action that is triggered when a change in the tag value is detected.</param>
    /// <param name="token">A cancellation token that can be used to cancel the monitoring operation.</param>
    /// <typeparam name="TData">The type of data expected for the tag being monitored. It must inherit from <see cref="LogixData"/>.</typeparam>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result is an <see cref="IDisposable"/> instance
    /// that can be used to stop monitoring the tag.
    /// </returns>
    Task<IDisposable> WatchTag<TData>(TagName tagName, Action<Tag>? onChange = null, CancellationToken token = default)
        where TData : LogixData, new();

    /// <summary>
    /// Monitors the specified PLC tag for changes and executes a callback action when the tag value changes.
    /// </summary>
    /// <param name="tag">The tag to be monitored for changes.</param>
    /// <param name="onChange">An optional callback action to invoke when the tag value changes. If null, changes will still be monitored without invoking a callback.</param>
    /// <param name="token">A cancellation token to cancel the monitoring operation.</param>
    /// <returns>
    /// A task that represents the asynchronous monitoring operation.
    /// The task result is an <see cref="IDisposable"/> object that can be used to stop monitoring the tag.
    /// </returns>
    Task<IDisposable> WatchTag(Tag tag, Action<Tag>? onChange = null, CancellationToken token = default);

    /// <summary>
    /// Monitors the specified collection of PLC tags for changes asynchronously and invokes a callback action
    /// when a tag's value changes.
    /// </summary>
    /// <param name="tags">A collection of tags to monitor for value changes.</param>
    /// <param name="onChange">An optional callback action that is invoked when a tag value changes, receiving the changed tag as a parameter.</param>
    /// <param name="token">A cancellation token that can be used to cancel the monitoring operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result is a disposable object
    /// that can be used to stop the monitoring of the tags.
    /// </returns>
    Task<IDisposable> WatchTags(IEnumerable<Tag> tags, Action<Tag>? onChange = null, CancellationToken token = default);
}