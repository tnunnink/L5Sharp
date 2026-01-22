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
    /// Watches a tag on the PLC for changes asynchronously and returns a subscription to monitor its value.
    /// </summary>
    /// <param name="tagName">The name of the tag to watch on the PLC.</param>
    /// <param name="token">A cancellation token that can be used to cancel the operation before completion.</param>
    /// <typeparam name="TData">
    /// The data type of the tag being watched. This type must derive from <see cref="LogixData"/>
    /// and have a parameterless constructor.
    /// </typeparam>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains an <see cref="ITagSubscription"/>
    /// that can be used to monitor changes to the tag value and register callbacks for change and error events.
    /// </returns>
    Task<ITagSubscription> WatchTag<TData>(TagName tagName, CancellationToken token = default)
        where TData : LogixData, new();

    /// <summary>
    /// Watches a tag on the PLC for changes asynchronously and returns a subscription to monitor its value.
    /// </summary>
    /// <param name="tag">The tag to watch on the PLC, including its name and metadata.</param>
    /// <param name="token">A cancellation token that can be used to cancel the operation before completion.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains an <see cref="ITagSubscription"/>
    /// that can be used to monitor changes to the tag value and register callbacks for change and error events.
    /// </returns>
    Task<ITagSubscription> WatchTag(Tag tag, CancellationToken token = default);

    /// <summary>
    /// Watches multiple tags on the PLC for changes asynchronously and returns a subscription to monitor their values.
    /// </summary>
    /// <param name="tags">A collection of tags to watch on the PLC.</param>
    /// <param name="token">A cancellation token that can be used to cancel the operation before completion.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains an <see cref="ITagSubscription"/>
    /// that can be used to monitor changes to the tag values and register callbacks for change and error events.
    /// </returns>
    Task<ITagSubscription> WatchTags(IEnumerable<Tag> tags, CancellationToken token = default);
}