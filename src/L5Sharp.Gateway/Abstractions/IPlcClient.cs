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
    /// Reads the value of a specified tag from the PLC asynchronously.
    /// </summary>
    /// <param name="tagName">The name of the tag to be read from the PLC.</param>
    /// <param name="token">A cancellation token that can be used to cancel the operation before completion.</param>
    /// <typeparam name="TData">The type of the tag data, which must inherit from <see cref="LogixData"/>
    /// and have a parameterless constructor.</typeparam>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a <see cref="TagResult"/>
    /// with the tag data read from the PLC.
    /// </returns>
    Task<TagResult> ReadTag<TData>(TagName tagName, CancellationToken token = default)
        where TData : LogixData, new();

    /// <summary>
    /// Reads the value of a specified tag from the PLC asynchronously.
    /// </summary>
    /// <param name="tag">The tag to be read from the PLC, including its name and metadata.</param>
    /// <param name="token">A cancellation token that can be used to cancel the operation before completion.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a <see cref="TagResult"/>
    /// with the tag data read from the PLC.
    /// </returns>
    Task<TagResult> ReadTag(Tag tag, CancellationToken token = default);

    /// <summary>
    /// Reads the values of multiple specified tags from the PLC asynchronously.
    /// </summary>
    /// <param name="tags">A collection of tags to be read from the PLC.</param>
    /// <param name="token">A cancellation token that can be used to cancel the operation before completion.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a <see cref="TagResults"/>
    /// object with the tag data read from the PLC for all specified tags.
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
    /// <remarks>
    /// This method will overwrite all data of the specified tag using the provided data instance. Use caution when
    /// writing to complex tag structures as this could lead to overwriting members with default values. To perform
    /// more granular writes on complex tags, use the <c>UpdateTag</c> methods.
    /// </remarks>
    Task<TagResult> WriteTag<TData>(TagName tagName, TData data, CancellationToken token = default)
        where TData : LogixData, new();

    /// <summary>
    /// Writes a specified tag asynchronously by applying an update operation to the existing tag data.
    /// </summary>
    /// <param name="tagName">The name of the tag to be written.</param>
    /// <param name="update">An action that specifies the update operation to be applied to the tag data. Note that
    /// all members of the specified data structure will be overwritten, and not just the members configured in the
    /// action delegate.</param>
    /// <param name="token">A cancellation token that can be used to cancel the operation before completion.</param>
    /// <typeparam name="TData">The type of the tag data, which must inherit from <see cref="LogixData"/>.</typeparam>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a <see cref="TagResults"/>
    /// indicating the result of the write operation.
    /// </returns>
    /// <remarks>
    /// This method will overwrite all data of the specified tag using the provided data instance. Use caution when
    /// writing to complex tag structures as this could lead to overwriting members with default values. To perform
    /// more granular writes on complex tags, use the <c>UpdateTag</c> methods.
    /// </remarks>
    Task<TagResult> WriteTag<TData>(TagName tagName, Action<TData> update, CancellationToken token = default)
        where TData : LogixData, new();

    /// <summary>
    /// Writes the value for the specified tag to the PLC asynchronously.
    /// </summary>
    /// <param name="tag">The tag to be written to the PLC, including its name and metadata.</param>
    /// <param name="token">A cancellation token that can be used to cancel the operation before completion.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the status of the
    /// tag write operation, including any associated metadata.
    /// </returns>
    /// <remarks>
    /// This method will overwrite all data of the specified tag using the provided data instance. Use caution when
    /// writing to complex tag structures as this could lead to overwriting members with default values. To perform
    /// more granular writes on complex tags, use the <c>UpdateTag</c> methods.
    /// </remarks>
    Task<TagResult> WriteTag(Tag tag, CancellationToken token = default);

    /// <summary>
    /// Writes the values for the provided tag collection to the PLC asynchronously.
    /// </summary>
    /// <param name="tags">A collection of tags to be written to the PLC.</param>
    /// <param name="token">A cancellation token that can be used to cancel the operation before completion.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a <see cref="TagResults"/>
    /// object indicating the result of the write operation, including success status and any errors encountered.
    /// </returns>
    /// <remarks>
    /// This method will overwrite all data of the specified tag using the provided data instance. Use caution when
    /// writing to complex tag structures as this could lead to overwriting members with default values. To perform
    /// more granular writes on complex tags, use the <c>UpdateTag</c> methods.
    /// </remarks>
    Task<TagResults> WriteTags(IEnumerable<Tag> tags, CancellationToken token = default);

    /// <summary>
    /// Updates the specified tag on the PLC asynchronously by writing the provided collection of member name-value pairs.
    /// </summary>
    /// <param name="tagName">The base tag name of the tag whose members are to be updated.</param>
    /// <param name="updates">A collection of tuples containing the member names and their corresponding
    /// new <see cref="AtomicData"/> values. This supports immediate or nested member paths (e.g., Member[0].Value.12)
    /// </param>
    /// <param name="token">A cancellation token that can be used to cancel the operation before completion.</param>
    /// <typeparam name="TData">The type of the tag data, which must inherit from <see cref="LogixData"/>
    /// and have a parameterless constructor.</typeparam>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a <see cref="TagResult"/>
    /// indicating the result of the update operation.
    /// </returns>
    /// <remarks>
    /// This method is meant for patching complex tag structure without overwriting a data structure with default
    /// values. Only the members provided in the <paramref name="updates"/> collection will be written to. Upon
    /// completion, the returned <see cref="TagResult"/> will also only contain the updated data for the provided members,
    /// and default values for all other members. If you do not need to be selective about overwriting tag members,
    /// use the <c>WriteTag</c> methods.
    /// </remarks>
    Task<TagResult> UpdateTag<TData>(TagName tagName, IReadOnlyList<(TagName Member, AtomicData Value)> updates,
        CancellationToken token = default) where TData : LogixData, new();

    /// <summary>
    /// Updates the specified tag on the PLC asynchronously by writing the provided collection of member name-value pairs.
    /// </summary>
    /// <param name="tag">The tag to be updated.</param>
    /// <param name="updates"> A collection of tuples containing the member names and their corresponding
    /// new <see cref="AtomicData"/> values. This supports immediate or nested member paths (e.g., Member[0].Value.12)
    /// </param>
    /// <param name="token">A cancellation token that can be used to cancel the operation before completion.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a <see cref="TagResult"/>
    /// indicating the result of the update operation.
    /// </returns>
    /// <remarks>
    /// This method is meant for patching complex tag structure without overwriting a data structure with default
    /// values. Only the members provided in the <paramref name="updates"/> collection will be written to. Upon
    /// completion, the returned <see cref="TagResult"/> will also only contain the updated data for the provided members,
    /// and default values for all other members. If you do not need to be selective about overwriting tag members,
    /// use the <c>WriteTag</c> methods.
    /// </remarks>
    Task<TagResult> UpdateTag(Tag tag, IReadOnlyList<(TagName Member, AtomicData Value)> updates,
        CancellationToken token = default);

    /// <summary>
    /// Updates the specified tag on the PLC asynchronously by applying an update action and writing the modified
    /// value back to the PLC.
    /// </summary>
    /// <param name="tagName">The name of the tag to be updated.</param>
    /// <param name="update">An action that specifies the update operation to be applied to the tag data.</param>
    /// <param name="token">A cancellation token that can be used to cancel the operation before completion.</param>
    /// <typeparam name="TData">The type of the tag data, which must inherit from <see cref="LogixData"/>
    /// and have a parameterless constructor.</typeparam>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a <see cref="TagResult"/>
    /// indicating the result of the update operation.
    /// </returns>
    /// <remarks>
    /// This method is meant for patching complex tag structure without overwriting a data structure with default
    /// values. Only the members provided in the <paramref name="update"/> action will be written to. Upon
    /// completion, the returned <see cref="TagResult"/> will also only contain the updated data for the provided members,
    /// and default values for all other members. If you do not need to be selective about overwriting tag members,
    /// use the <c>WriteTag</c> methods.
    /// </remarks>
    Task<TagResult> UpdateTag<TData>(TagName tagName, Action<TData> update, CancellationToken token = default)
        where TData : LogixData, new();

    /// <summary>
    /// Updates a specified tag on the PLC by performing an update operation asynchronously.
    /// </summary>
    /// <param name="tag">The tag to be updated on the PLC.</param>
    /// <param name="update">An action that specifies the update operation to be applied to the tag data.</param>
    /// <param name="token">A cancellation token that can be used to cancel the operation before completion.</param>
    /// <typeparam name="TData">The type of the data associated with the tag.</typeparam>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a <see cref="TagResult"/>
    /// indicating the result of the update operation.
    /// </returns>
    /// <remarks>
    /// This method is meant for patching complex tag structure without overwriting a data structure with default
    /// values. Only the members provided in the <paramref name="update"/> action will be written to. Upon
    /// completion, the returned <see cref="TagResult"/> will also only contain the updated data for the provided members,
    /// and default values for all other members. If you do not need to be selective about overwriting tag members,
    /// use the <c>WriteTag</c> methods.
    /// </remarks>
    Task<TagResult> UpdateTag<TData>(Tag tag, Action<TData> update, CancellationToken token = default)
        where TData : LogixData, new();

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
    /// Continuously polls a specified tag on the PLC for a specific duration and returns the final result
    /// once the duration expires.
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
    /// Continuously polls a specified tag on the PLC for a specific duration and returns the final result
    /// once the duration expires.
    /// </summary>
    /// <param name="tag">The tag object that specifies the PLC tag to be polled.</param>
    /// <param name="duration">The total duration over which the tag should be polled.</param>
    /// <param name="token">A cancellation token that can be used to cancel the operation before completion.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the final <see cref="TagResult"/>
    /// of the tag polling operation.
    /// </returns>
    Task<TagResult> PollTag(Tag tag, TimeSpan duration, CancellationToken token = default);

    /// <summary>
    /// Continuously polls a specified tag on the PLC until the provided predicate condition is satisfied
    /// and returns the final result.
    /// </summary>
    /// <param name="tagName">The name of the tag to be polled on the PLC.</param>
    /// <param name="predicate">A function that evaluates the tag value and returns true when the desired condition is met.</param>
    /// <param name="token">A cancellation token that can be used to cancel the operation before completion.</param>
    /// <typeparam name="TData">The data type of the tag being polled. Must inherit from <see cref="LogixData"/>
    /// and have a parameterless constructor.</typeparam>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the final <see cref="TagResult"/>
    /// of the tag polling operation once the predicate is satisfied.
    /// </returns>
    Task<TagResult> PollTag<TData>(TagName tagName, Func<TData, bool> predicate, CancellationToken token = default)
        where TData : LogixData, new();

    /// <summary>
    /// Continuously polls a specified tag on the PLC until the provided predicate condition is satisfied
    /// and returns the final result.
    /// </summary>
    /// <param name="tag">The tag object that specifies the PLC tag to be polled.</param>
    /// <param name="predicate">A function that evaluates the tag value and returns true when the desired condition is met.</param>
    /// <param name="token">A cancellation token that can be used to cancel the operation before completion.</param>
    /// <typeparam name="TData">The data type of the tag being polled. Must inherit from <see cref="LogixData"/>
    /// and have a parameterless constructor.</typeparam>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the final <see cref="TagResult"/>
    /// of the tag polling operation once the predicate is satisfied.
    /// </returns>
    Task<TagResult> PollTag<TData>(Tag tag, Func<TData, bool> predicate, CancellationToken token = default)
        where TData : LogixData, new();

    /// <summary>
    /// Polls the specified tag asynchronously until the predicate is satisfied or the duration expires and returns
    /// the final result in either case.
    /// </summary>
    /// <param name="tagName">The name of the tag to be polled on the PLC.</param>
    /// <param name="predicate">A function that evaluates the tag value and returns true when the desired condition is met.</param>
    /// <param name="duration">The maximum duration to spend polling the tag value before returning.</param>
    /// <param name="token">A cancellation token that can be used to cancel the operation before completion.</param>
    /// <typeparam name="TData">The type of logix data returned by the tag. Must derive from <see cref="LogixData"/>
    /// and have a parameterless constructor.</typeparam>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the final <see cref="TagResult"/>
    /// of the tag polling operation.
    /// </returns>
    Task<TagResult> PollTag<TData>(TagName tagName, Func<TData, bool> predicate, TimeSpan duration,
        CancellationToken token = default) where TData : LogixData, new();

    /// <summary>
    /// Polls the provided tag asynchronously until the predicate is satisfied or the duration expires and returns
    /// the final result in either case.
    /// </summary>
    /// <param name="tag">The tag object that specifies the PLC tag to be polled.</param>
    /// <param name="predicate">A function that evaluates the tag value and returns true when the desired condition is met.</param>
    /// <param name="duration">The maximum duration to spend polling the tag value before returning.</param>
    /// <param name="token">A cancellation token to cancel the polling operation before completion.</param>
    /// <typeparam name="TData">The type of logix data returned by the tag. Must derive from <see cref="LogixData"/>
    /// and have a parameterless constructor.</typeparam>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the final <see cref="TagResult"/>
    /// of the tag polling operation.
    /// </returns>
    Task<TagResult> PollTag<TData>(Tag tag, Func<TData, bool> predicate, TimeSpan duration,
        CancellationToken token = default) where TData : LogixData, new();
}