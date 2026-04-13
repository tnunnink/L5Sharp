using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using L5Sharp.Core;
using L5Sharp.Gateway.Abstractions;
using L5Sharp.Gateway.Common;

namespace L5Sharp.Gateway.Extensions;

/// <summary>
/// Provides extension methods for working with the L5Sharp framework, enabling convenient operations
/// such as uploading and managing tags in PLC projects using the provided client.
/// </summary>
public static class L5XExtensions
{
    /// <summary>
    /// Provides extension methods for interacting with L5X project instances, enabling functionality
    /// such as uploading tags from the project to a connected PLC controller.
    /// </summary>
    extension(L5X project)
    {
        /// <summary>
        /// Uploads all public tags from the L5X project to the PLC by reading their
        /// current values from the controller.
        /// </summary>
        /// <param name="client">The PLC client used to communicate with the controller.</param>
        /// <param name="token">A cancellation token to cancel the upload operation.</param>
        /// <returns>A task that represents the asynchronous upload operation containing a <see cref="TagResults"/> with the results.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="client"/> is null.</exception>
        public Task<TagResults> UploadAsync(IPlcClient client, CancellationToken token = default)
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
        /// <param name="client">The PLC client used to communicate with the controller.</param>
        /// <param name="predicate">A function to filter which tags should be uploaded.</param>
        /// <param name="token">A cancellation token to cancel the upload operation.</param>
        /// <returns>A task that represents the asynchronous upload operation containing a <see cref="TagResults"/> with the results.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="client"/> or <paramref name="predicate"/> is null.</exception>
        public Task<TagResults> UploadAsync(IPlcClient client, Func<Tag, bool> predicate, CancellationToken token = default)
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
        /// <param name="client">The PLC client used to communicate with the controller.</param>
        /// <param name="scope">The scope that filters which tags should be uploaded (e.g., controller or program scope).</param>
        /// <param name="token">A cancellation token to cancel the upload operation.</param>
        /// <returns>A task that represents the asynchronous upload operation containing a <see cref="TagResults"/> with the results.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="client"/> is null.</exception>
        public Task<TagResults> UploadAsync(IPlcClient client, Scope scope, CancellationToken token = default)
        {
            if (client is null) throw new ArgumentNullException(nameof(client));

            var tags = project.Query<Tag>().Where(t => t.Scope == scope && t.IsPublic()).ToArray();
            var response = client.ReadTags(tags, token);
            return response;
        }
    }
}