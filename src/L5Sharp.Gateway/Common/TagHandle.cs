using System;

namespace L5Sharp.Gateway.Common;

/// <summary>
/// Represents a handle for identifying and interacting with a specific tag in the system.
/// </summary>
public readonly struct TagHandle
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
}