using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using L5Sharp.Core;

namespace L5Sharp.Gateway;

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
    /// Creates a watch to monitor a collection of tags for changes in their values or statuses.
    /// </summary>
    /// <param name="tags">A collection of tags to be monitored for changes.</param>
    /// <returns>An instance of <c>ITagWatch</c> that allows starting, stopping, and subscribing to tag updates.</returns>
    ITagWatch WatchTags(ICollection<Tag> tags);
}