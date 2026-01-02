using System;
using System.Collections.Generic;
using libplctag.NativeImport;

namespace L5Sharp.Gateway;

/// <summary>
/// Represents errors that occur during the processing of PLC tags.
/// </summary>
public class TagException : Exception
{
    /// <summary>
    /// Represents an exception that occurs during a tag operation in a PLC context.
    /// </summary>
    /// <param name="result">The status representing the specific error encountered during the tag operation.</param>
    /// <param name="tagName">The optional name of the tag associated with the operation that caused the exception.</param>
    private TagException(TagResult result, string? tagName = null) : base(BuildMessage(result))
    {
        Result = result;
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
    public TagResult Result { get; }

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
    /// <param name="result">The status of the tag operation to evaluate for errors.</param>
    /// <param name="exceptionCode"></param>
    /// <param name="tagName">The optional name of the tag associated with the operation.</param>
    public static void ThrowIfRequested(TagResult result, HashSet<TagResult> exceptionCode, string? tagName = null)
    {
        if (result >= 0) return;
        if (!exceptionCode.Contains(result)) return;

        throw new TagException(result, tagName);
    }

    /// <summary>
    /// Constructs a detailed error message based on the specified tag status.
    /// </summary>
    /// <param name="result">The status representing the specific error encountered during the tag operation.</param>
    /// <returns>A string containing the formatted error message for the tag operation.</returns>
    private static string BuildMessage(TagResult result)
    {
        var error = plctag.plc_tag_decode_error((int)result);
        return $"Tag operation failed with error: {error}";
    }
}