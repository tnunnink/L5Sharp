using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using L5Sharp.Core;
using L5Sharp.Gateway.Abstractions;
using L5Sharp.Gateway.Common;
// ReSharper disable ConvertToExtensionBlock

namespace L5Sharp.Gateway.Extensions;

/// <summary>
/// Provides extension methods for <see cref="L5X"/> instances that enable uploading tag values
/// from a PLC controller to update the L5X project data.
/// </summary>
public static class L5XExtensions
{
    /// <summary>
    /// Reads current values of all public tags from the PLC controller and returns the results.
    /// </summary>
    /// <param name="project">The L5X project containing the tags to upload.</param>
    /// <param name="client">The PLC client used to communicate with the controller.</param>
    /// <param name="token">A cancellation token to cancel the upload operation.</param>
    /// <returns>A task that represents the asynchronous upload operation containing a <see cref="TagResults"/> with the results.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="client"/> is null.</exception>
    public static Task<TagResults> UploadAsync(this L5X project, IPlcClient client, CancellationToken token = default)
    {
        if (client is null) throw new ArgumentNullException(nameof(client));

        var tags = project.Query<Tag>().Where(t => t.IsPublic()).ToArray();
        var response = client.ReadTags(tags, token);
        return response;
    }

    /// <summary>
    /// Reads current values of public tags that match the specified predicate from the PLC controller and returns the results.
    /// </summary>
    /// <param name="project">The L5X project containing the tags to upload.</param>
    /// <param name="client">The PLC client used to communicate with the controller.</param>
    /// <param name="predicate">A function to filter which tags should be uploaded.</param>
    /// <param name="token">A cancellation token to cancel the upload operation.</param>
    /// <returns>A task that represents the asynchronous upload operation containing a <see cref="TagResults"/> with the results.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="client"/> or <paramref name="predicate"/> is null.</exception>
    public static Task<TagResults> UploadAsync(this L5X project, IPlcClient client, Func<Tag, bool> predicate, CancellationToken token = default)
    {
        if (client is null) throw new ArgumentNullException(nameof(client));
        if (predicate is null) throw new ArgumentNullException(nameof(predicate));

        var tags = project.Query(predicate).Where(t => t.IsPublic()).ToArray();
        var response = client.ReadTags(tags, token);
        return response;
    }

    /// <summary>
    /// Reads current values of all public tags within the specified scope from the PLC controller and returns the results.
    /// </summary>
    /// <param name="project">The L5X project containing the tags to upload.</param>
    /// <param name="client">The PLC client used to communicate with the controller.</param>
    /// <param name="scope">The scope that filters which tags should be uploaded (e.g., controller or program scope).</param>
    /// <param name="token">A cancellation token to cancel the upload operation.</param>
    /// <returns>A task that represents the asynchronous upload operation containing a <see cref="TagResults"/> with the results.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="client"/> is null.</exception>
    public static Task<TagResults> UploadAsync(this L5X project, IPlcClient client, Scope scope, CancellationToken token = default)
    {
        if (client is null) throw new ArgumentNullException(nameof(client));

        var tags = project.Query<Tag>().Where(t => t.Scope == scope && t.IsPublic()).ToArray();
        var response = client.ReadTags(tags, token);
        return response;
    }
}