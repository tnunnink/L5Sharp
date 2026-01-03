using System;
using L5Sharp.Core;
using libplctag.NativeImport;

namespace L5Sharp.Gateway;

/// <summary>
/// Represents the result of a tag-related operation performed by an <see cref="IPlcClient"/>.
/// </summary>
public class TagStatus
{
    /// <summary>
    /// Creates a new <see cref="TagStatus"/> object indicating the result of the operation.
    /// </summary>
    internal TagStatus(int handle, TagName tagName, TagResult result, byte[]? data = null)
    {
        Handle = handle;
        TagName = tagName;
        Result = result;
        Data = data ?? [];
        Timestamp = DateTime.UtcNow;
    }

    /// <summary>
    /// Represents a unique identifier assigned to a tag operation, enabling the tracking and interaction
    /// with the operation within the PLC system.
    /// </summary>
    public int Handle { get; }

    /// <summary>
    /// Represents the name of a tag used associated with the current status.
    /// </summary>
    public TagName TagName { get; }

    /// <summary>
    /// Represents the outcome or status of a PLC tag operation.
    /// </summary>
    public TagResult Result { get; }

    /// <summary>
    /// Represents the binary data associated with a tag operation result, providing access
    /// to the raw payload returned by the PLC system.
    /// </summary>
    public byte[] Data { get; }

    /// <summary>
    /// Gets the UTC timestamp indicating when the tag operation result was created.
    /// </summary>
    public DateTime Timestamp { get; }

    /// <summary>
    /// Indicates whether the tag operation completed successfully without errors.
    /// </summary>
    public bool IsGood => Result == TagResult.Ok;

    /// <summary>
    /// Indicates whether the tag operation encountered an error or failed to complete successfully.
    /// </summary>
    public bool IsBad => Result < 0;

    /// <summary>
    /// Gets a detailed error message corresponding to the current <see cref="TagResult"/> of the operation.
    /// </summary>
    /// <returns>A string containing the error message related to the operation's status.</returns>
    public string GetError() => plctag.plc_tag_decode_error((int)Result);

    /// <summary>
    /// Creates a new <see cref="TagStatus"/> object representing a pending state for the specified tag.
    /// </summary>
    /// <param name="handle">The handle associated with the tag operation.</param>
    /// <param name="tagName">The <see cref="TagName"/> of the tag being processed.</param>
    /// <returns>A <see cref="TagStatus"/> object in the pending state.</returns>
    public static TagStatus Pending(int handle, TagName tagName) => new(handle, tagName, TagResult.Pending);
}