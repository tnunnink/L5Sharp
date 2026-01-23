using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using L5Sharp.Core;
using L5Sharp.Gateway.Abstractions;
using L5Sharp.Gateway.Common;

namespace L5Sharp.Gateway;

/// <summary>
/// Provides extension methods for working with the L5Sharp framework, enabling convenient operations
/// such as uploading and managing tags in PLC projects using the provided client.
/// </summary>
public static class Extensions
{
    /// <summary>
    /// Uploads all public tags from the L5X project to the PLC by reading their
    /// current values from the controller.
    /// </summary>
    /// <param name="project">The L5X project containing the tags to upload.</param>
    /// <param name="client">The PLC client used to communicate with the controller.</param>
    /// <param name="token">A cancellation token to cancel the upload operation.</param>
    /// <returns>A task that represents the asynchronous upload operation, containing a <see cref="TagResults"/> with the results.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="client"/> is null.</exception>
    public static Task<TagResults> Upload(this L5X project, IPlcClient client,
        CancellationToken token = default)
    {
        if (client is null) throw new ArgumentNullException(nameof(client));

        var tags = project.Query<Tag>().Where(t => t.IsPublic()).ToArray();
        var response = client.ReadTags(tags, token);
        return response;
    }

    /// <summary>
    /// Uploads public tags from the L5X project that match the specified predicate by reading their
    /// current values from the controller.
    /// </summary>
    /// <param name="project">The L5X project containing the tags to upload.</param>
    /// <param name="client">The PLC client used to communicate with the controller.</param>
    /// <param name="predicate">A function to filter which tags should be uploaded.</param>
    /// <param name="token">A cancellation token to cancel the upload operation.</param>
    /// <returns>A task that represents the asynchronous upload operation, containing a <see cref="TagResults"/> with the results.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="client"/> or <paramref name="predicate"/> is null.</exception>
    public static Task<TagResults> Upload(this L5X project, IPlcClient client, Func<Tag, bool> predicate,
        CancellationToken token = default)
    {
        if (client is null) throw new ArgumentNullException(nameof(client));
        if (predicate is null) throw new ArgumentNullException(nameof(predicate));

        var tags = project.Query(predicate).Where(t => t.IsPublic()).ToArray();
        var response = client.ReadTags(tags, token);
        return response;
    }

    /// <summary>
    /// Uploads all public tags from the L5X project within the specified scope by reading their
    /// current values from the controller.
    /// </summary>
    /// <param name="project">The L5X project containing the tags to upload.</param>
    /// <param name="client">The PLC client used to communicate with the controller.</param>
    /// <param name="scope">The scope that filters which tags should be uploaded (e.g., controller or program scope).</param>
    /// <param name="token">A cancellation token to cancel the upload operation.</param>
    /// <returns>A task that represents the asynchronous upload operation, containing a <see cref="TagResults"/> with the results.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="client"/> is null.</exception>
    public static Task<TagResults> Upload(this L5X project, IPlcClient client, Scope scope,
        CancellationToken token = default)
    {
        if (client is null) throw new ArgumentNullException(nameof(client));

        var tags = project.Query<Tag>().Where(t => t.Scope == scope && t.IsPublic()).ToArray();
        var response = client.ReadTags(tags, token);
        return response;
    }

    /// <summary>
    /// Creates a snapshot of the L5X project by uploading all public tag values from the controller and saving
    /// the project to the specified path.
    /// </summary>
    /// <param name="project">The L5X project containing the tags to snapshot.</param>
    /// <param name="client">The PLC client used to communicate with the controller.</param>
    /// <param name="savePath">The file path where the project snapshot should be saved.</param>
    /// <param name="token">A cancellation token to cancel the snapshot operation.</param>
    /// <returns>A task that represents the asynchronous snapshot operation, containing a <see cref="TagResults"/> with the results.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="client"/> is null.</exception>
    public static async Task<TagResults> Snapshot(this L5X project, IPlcClient client, string savePath,
        CancellationToken token = default)
    {
        if (client is null) throw new ArgumentNullException(nameof(client));

        var tags = project.Query<Tag>().Where(t => t.IsPublic()).ToArray();
        var response = await client.ReadTags(tags, token);

        if (response.Success)
        {
            project.Save(savePath);
        }

        return response;
    }
}