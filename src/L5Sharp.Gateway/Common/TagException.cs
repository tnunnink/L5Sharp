using System;
using System.Collections.Generic;

namespace L5Sharp.Gateway.Common;

/// <summary>
/// Represents errors that occur during the processing of PLC tags.
/// </summary>
public class TagException : Exception
{
    /// <summary>
    /// Represents an exception that occurs during a tag operation in a PLC context.
    /// </summary>
    /// <param name="status">The status representing the specific error encountered during the tag operation.</param>
    /// <param name="tagName">The optional name of the tag associated with the operation that caused the exception.</param>
    public TagException(TagStatus status, string? tagName = null) : base(BuildMessage(status))
    {
        Status = status;
        TagName = tagName;
    }

    /// <summary>
    /// Gets the status associated with the tag exception.
    /// </summary>
    /// <remarks>
    /// This property indicates the current status code that represents the error thrown.
    /// It provides details relevant to the operation or condition that led to the error, aiding in
    /// troubleshooting and debugging scenarios involving tag-related exceptions.
    /// </remarks>
    public TagStatus Status { get; }

    /// <summary>
    /// Gets the name of the tag associated with the exception.
    /// </summary>
    /// <remarks>
    /// This property provides the name of the tag that caused the exception to be thrown.
    /// It is useful for debugging or logging purposes to identify the specific tag related
    /// to the encountered issue.
    /// </remarks>
    public string? TagName { get; }

    /// <summary>
    /// Throws a <see cref="TagException"/> if the provided tag operation status indicates an error.
    /// </summary>
    /// <param name="status">The status of the tag operation to evaluate for errors.</param>
    /// <param name="exceptionCode"></param>
    /// <param name="tagName">The optional name of the tag associated with the operation.</param>
    public static void ThrowIfRequested(TagStatus status, HashSet<TagStatus> exceptionCode, string? tagName = null)
    {
        if (status >= 0) return;
        if (!exceptionCode.Contains(status)) return;

        throw new TagException(status, tagName);
    }

    /// <summary>
    /// Constructs a detailed error message based on the specified tag status.
    /// </summary>
    /// <param name="status">The status representing the specific error encountered during the tag operation.</param>
    /// <returns>A string containing the formatted error message for the tag operation.</returns>
    private static string BuildMessage(TagStatus status)
    {
        return $"Tag operation failed with error: {status}";
    }
}