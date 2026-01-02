using System;
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
    internal TagStatus(int handle, TagResult result)
    {
        Handle = handle;
        Result = result;
        Timestamp = DateTime.UtcNow;
    }

    /// <summary>
    /// Represents a unique identifier assigned to a tag operation, enabling the tracking and interaction
    /// with the operation within the PLC system.
    /// </summary>
    public int Handle { get; }

    /// <summary>
    /// Represents the outcome or status of a PLC tag operation.
    /// </summary>
    public TagResult Result { get; }

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
    /// Creates a new <see cref="TagStatus"/> object with a pending result for the specified handle.
    /// </summary>
    /// <param name="handle">The handle associated with the tag operation.</param>
    /// <returns>A <see cref="TagStatus"/> object representing the pending state of the operation.</returns>
    public static TagStatus Pending(int handle) => new(handle, TagResult.Pending);
}