using System;
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
    /// Converts an integer code to its corresponding <see cref="TagAction"/> value.
    /// </summary>
    /// <param name="code">The integer code to be converted to a <see cref="TagAction"/>.</param>
    /// <returns>The <see cref="TagAction"/> that corresponds to the provided code.</returns>
    public static TagAction AsAction(this int code)
    {
        return (TagAction)code;
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

        return status;
    }

    /// <summary>
    /// Attempts to retrieve the <see cref="TagStatus"/> associated with the provided <see cref="Tag"/>.
    /// </summary>
    /// <param name="tag">The <see cref="Tag"/> object from which the status is to be retrieved.</param>
    /// <param name="status">When this method returns, contains the <see cref="TagStatus"/> associated with the
    /// specified tag, if available; otherwise, <c>null</c>.</param>
    /// <returns><c>true</c> if the <see cref="TagStatus"/> was successfully retrieved; otherwise, <c>false</c>.</returns>
    public static bool TryGetStatus(this Tag tag, out TagStatus status)
    {
        var annotation = tag.Serialize().Annotation<TagStatus>();

        if (annotation is null)
        {
            status = null!;
            return false;
        }

        status = annotation;
        return true;
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
    internal static void Read(this Tag tag, int handle)
    {
        // Create a byte buffer to read out data from.
        var size = plctag.plc_tag_get_size(handle);
        var buffer = new byte[size];

        // Read the current byte array value and update the underlying tag data.
        var result = plctag.plc_tag_get_raw_bytes(handle, 0, buffer, size).AsResult();
        tag.Value.Update(buffer, 0);

        // Attach the current status on the tag element as an annotation so users can read the info.
        var status = new TagStatus(handle, result);
        tag.SetStatus(status);
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