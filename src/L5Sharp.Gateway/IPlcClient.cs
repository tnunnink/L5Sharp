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
    /// Sends a ping request to verify connectivity with the PLC.
    /// </summary>
    /// <param name="token">A cancellation token that can be used to cancel the operation before completion.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an integer indicating the response or status of the ping request.</returns>
    ValueTask<bool> PingAsync(CancellationToken token = default);

    /// <summary>
    /// Reads the value of the specified tag from the PLC asynchronously.
    /// </summary>
    /// <param name="tag">The tag to be read from the PLC. This typically includes the name and data type.</param>
    /// <param name="token">A cancellation token that can be used to cancel the operation before completion.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the result of the tag read operation, including the value and associated metadata.</returns>
    ValueTask<TagResult> ReadAsync(Tag tag, CancellationToken token = default);

    /// <summary>
    /// Writes the specified tag value to the PLC asynchronously.
    /// </summary>
    /// <param name="tag">The tag to be written to the PLC, including its name and value.</param>
    /// <param name="token">A cancellation token that can be used to cancel the operation before completion.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the result of the tag write operation, including the status and associated metadata.</returns>
    ValueTask<TagResult> WriteAsync(Tag tag, CancellationToken token = default);

    /// <summary>
    /// Creates a watch to monitor a collection of tags for changes in their values or statuses.
    /// </summary>
    /// <param name="tags">A collection of tags to be monitored for changes.</param>
    /// <param name="name"></param>
    /// <returns>An instance of <c>ITagWatch</c> that allows starting, stopping, and subscribing to tag updates.</returns>
    ITagWatch CreateWatch(ICollection<Tag> tags, string? name = null);
}