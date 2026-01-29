using System;
using System.Runtime.InteropServices;
using L5Sharp.Gateway.Abstractions;
using L5Sharp.Gateway.Common;

namespace L5Sharp.Gateway.Internal;

/// <summary>
/// Represents a handle for identifying and interacting with a specific tag in the system.
/// </summary>
internal readonly record struct TagHandle
{
    /// <summary>
    /// Represents a handle for identifying and interacting with a specific tag in the system.
    /// </summary>
    public TagHandle(int tagId, IntPtr tagPtr)
    {
        TagId = tagId;
        TagPtr = tagPtr;
    }

    /// <summary>
    /// Gets the unique identifier for the tag handle, which is used to reference and interact with
    /// a specific tag within the system.
    /// </summary>
    public int TagId { get; }

    /// <summary>
    /// Gets the unmanaged pointer to the memory address representing the name of the tag.
    /// This pointer is used internally for low-level operations involving tag name references.
    /// </summary>
    public IntPtr TagPtr { get; }

    /// <summary>
    /// Indicates whether the tag handle is valid by checking if the tag identifier is greater than zero
    /// and the pointer to the tag is not null.
    /// </summary>
    public bool IsValid => TagId > 0 && TagPtr != IntPtr.Zero;

    /// <summary>
    /// Frees the memory allocated for the tag and destroys it in the provided tag service.
    /// </summary>
    /// <param name="service">The tag service responsible for managing the lifecycle of the tag.</param>
    /// <returns>The status indicating the result of the operation.</returns>
    public TagStatus Free(ITagService service)
    {
        Marshal.FreeHGlobal(TagPtr);
        var status = service.Destroy(TagId).AsStatus();
        return status;
    }

    /// <summary>
    /// Creates a new <see cref="TagHandle"/> instance that represents an error state.
    /// </summary>
    /// <param name="errorCode">The error code associated with the invalid tag handle.</param>
    /// <returns>A <see cref="TagHandle"/> instance with an error code and no valid pointer.</returns>
    public static TagHandle Error(int errorCode)
    {
        return new TagHandle(errorCode, IntPtr.Zero);
    }
}