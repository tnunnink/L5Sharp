using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Core;
using libplctag.NativeImport;

namespace L5Sharp.Gateway;

/// <summary>
/// Provides extension methods for working with tags in a Rockwell Logix-based PLC environment.
/// </summary>
public static class Extensions
{
    /// <summary>
    /// Converts an integer code to its corresponding <see cref="TagResult"/> value.
    /// </summary>
    /// <param name="code">The integer code to be converted to a <see cref="TagResult"/>.</param>
    /// <returns>The <see cref="TagResult"/> that corresponds to the provided code.</returns>
    public static TagResult AsResult(this int code)
    {
        return (TagResult)code;
    }

    /// <summary>
    /// Retrieves the current status of the specified <see cref="Tag"/>.
    /// </summary>
    /// <param name="tag">The <see cref="Tag"/> for which the status is being requested.</param>
    /// <returns>The <see cref="TagStatus"/> representing the current status of the provided <see cref="Tag"/>.</returns>
    /// <exception cref="InvalidOperationException">Thrown when no status object is available for the specified tag.</exception>
    public static TagStatus GetStatus(this Tag tag)
    {
        var status = tag.Serialize().Annotation<TagStatus>();

        if (status is null)
        {
            throw new InvalidOperationException($"No status object is available for tag: '{tag.Name}'");
        }

        /*var result = plctag.plc_tag_status(status.Handle).AsResult();
        status.*/

        return status;
    }

    /// <summary>
    /// Creates a real-time watch for the provided collection of tags in a specified PLC configuration.
    /// </summary>
    /// <param name="tags">A collection of <see cref="Tag"/> objects to monitor.</param>
    /// <param name="ip">The IP address of the target PLC to connect to.</param>
    /// <param name="slot">The slot number of the controller module. Defaults to 0 if not specified.</param>
    /// <param name="rate">The refresh interval in milliseconds for reading tag updates. Defaults to 1000 milliseconds.</param>
    /// <returns>An instance of <see cref="ITagWatch"/> that can be used to monitor and manage updates to the specified tags.</returns>
    public static ITagWatch Watch(this IEnumerable<Tag> tags, string ip, ushort slot = 0, int rate = 1000)
    {
        //todo var composite = new CompositeDisposable();
        using var client = new PlcClient(ip, slot, new PlcOptions { ReadInterval = rate });
        return client.CreateWatch(tags.ToList());
    }

    /// <summary>
    /// Determines the fully qualified tag name based on the provided tag's scope and name.
    /// </summary>
    /// <param name="tag">The tag for which the name needs to be determined.</param>
    /// <returns>Returns the fully qualified tag name as a string.</returns>
    internal static TagName DetermineTagName(this Tag tag)
    {
        if (tag.Scope.IsProgram)
        {
            return $"Program:{tag.Scope.Container}.{tag.TagName}";
        }

        return tag.TagName;
    }

    /// <summary>
    /// Reads data from the specified tag handle and updates the tag value and status accordingly.
    /// </summary>
    /// <param name="tag">The tag instance to be updated with the read data.</param>
    /// <param name="handle">The handle of the tag to read data from.</param>
    internal static TagStatus Refresh(this Tag tag, int handle)
    {
        // Create a byte buffer to read out data from.
        var size = plctag.plc_tag_get_size(handle);
        var buffer = new byte[size];
        var result = plctag.plc_tag_get_raw_bytes(handle, 0, buffer, size).AsResult();
        //todo should we handle the result of this? From docs a lot of the errors seem like they would be operational but idk

        // We only update the value if the bytes are different
        var current = tag.Serialize().Annotation<TagStatus>();
        if (current?.Data is null || !current.Data.SequenceEqual(buffer))
        {
            // Update the underlying tag data.
            tag.Value.Update(buffer, 0);
        }

        // Attach the current status on the tag element as an annotation so users can read the info.
        var status = new TagStatus(handle, tag.TagName, result, buffer);
        tag.SetStatus(status);
        return status;
    }

    /// <summary>
    /// Updates the status of a <see cref="Tag"/> by attaching the provided <see cref="TagStatus"/> as an annotation.
    /// </summary>
    /// <param name="tag">The <see cref="Tag"/> object to which the status will be applied.</param>
    /// <param name="status">The <see cref="TagStatus"/> to be assigned to the tag.</param>
    internal static void SetStatus(this Tag tag, TagStatus status)
    {
        tag.Serialize().RemoveAnnotations<TagStatus>();
        tag.Serialize().AddAnnotation(status);
    }
}