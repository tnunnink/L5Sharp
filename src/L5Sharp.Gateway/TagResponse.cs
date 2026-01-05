using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Core;
using libplctag.NativeImport;

namespace L5Sharp.Gateway;

/// <summary>
/// Represents the result of a tag-related operation performed by an <see cref="IPlcClient"/>.
/// </summary>
public class TagResponse
{
    /// <summary>
    /// A flat list of specific member results. To save memory, we typically only store failures here.
    /// </summary>
    private readonly List<(TagName Member, TagResult Result)> _errors = [];

    /// <summary>
    /// Creates a new <see cref="TagResponse"/> object indicating the result of the operation.
    /// </summary>
    private TagResponse(Tag tag)
    {
        Tag = tag;
        Timestamp = DateTime.UtcNow;
    }

    /// <summary>
    /// The tag instance used to perform the operation.
    /// </summary>
    /// <remarks>
    /// This is the same instance provided to the API and will have updated tag data if read from the PLC. 
    /// </remarks>
    public Tag Tag { get; }

    /// <summary>
    /// Indicates whether the tag operation completed successfully without errors.
    /// </summary>
    public bool Success => _errors.Count == 0;

    /// <summary>
    /// Represents the result status of the tag operation, typically indicating success or
    /// the first encountered failure.
    /// </summary>
    public TagResult Status => _errors.Count > 0 ? _errors[0].Result : TagResult.Ok;

    /// <summary>
    /// The timestamp indicating when the tag response was created.
    /// This value is set to the current UTC time at the moment of initialization.
    /// </summary>
    public DateTime Timestamp { get; }

    /// <summary>
    /// Gets the duration of the operation represented by the tag response.
    /// </summary>
    /// <remarks>
    /// The duration is calculated as the time span between the initiation of the operation
    /// and the point when the response was generated, providing a measure of the operation's execution time.
    /// </remarks>
    public TimeSpan Duration { get; }

    /// <summary>
    /// Gets a collection of errors encountered during the tag-related operation.
    /// Each error is represented as a tuple containing the tag name and the corresponding error message.
    /// </summary>
    public IEnumerable<(TagName, string)> Errors => _errors.Select(e => (e.Member, GetError(e.Result)));

    /// <summary>
    /// Aggregates the results of multiple tag operations into a single <see cref="TagResponse"/> object.
    /// </summary>
    /// <param name="tag">The tag associated with the response.</param>
    /// <param name="results">A list of results containing tag names and their corresponding outcomes.</param>
    /// <returns>A <see cref="TagResponse"/> object representing the aggregated results.</returns>
    /// <exception cref="NotImplementedException">Thrown when the method is not implemented.</exception>
    public static TagResponse Aggregate(Tag tag, List<(TagName, TagResult)> results)
    {
        var response = new TagResponse(tag);

        foreach (var (member, status) in results)
        {
            if (status >= 0) continue;
            response._errors.Add((member, status));
        }

        return response;
    }

    /// <summary>
    /// Decodes the error message corresponding to the provided <see cref="TagResult"/> value.
    /// </summary>
    /// <param name="result">The result code for which the error message needs to be decoded.</param>
    /// <returns>A string representing the decoded error message.</returns>
    private static string GetError(TagResult result) => plctag.plc_tag_decode_error((int)result);
}