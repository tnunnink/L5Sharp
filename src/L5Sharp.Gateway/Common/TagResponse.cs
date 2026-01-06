using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Core;
using L5Sharp.Gateway.Abstractions;
using libplctag.NativeImport;

namespace L5Sharp.Gateway.Common;

/// <summary>
/// Represents the result of a tag-related operation performed by an <see cref="IPlcClient"/>.
/// </summary>
public class TagResponse
{
    /// <summary>
    /// A collection of tags associated with the response. Each tag should represent an atomic member of the
    /// tag or set of tags that were involved in the operation that produced this response object.
    /// </summary>
    private readonly List<TagName> _tags = [];

    /// <summary>
    /// A flat list of specific member results. To save memory, we only store failures here.
    /// </summary>
    private readonly List<(TagName Tag, TagStatus Result)> _errors = [];

    /// <summary>
    /// Creates a new <see cref="TagResponse"/> object indicating the result of the operation.
    /// </summary>
    private TagResponse(TimeSpan duration)
    {
        Timestamp = DateTime.UtcNow;
        Duration = duration;
    }

    /// <summary>
    /// Indicates whether the tag operation completed successfully without errors.
    /// </summary>
    public bool Success => _errors.Count == 0;

    /// <summary>
    /// Represents the result status of the tag operation, typically indicating success or
    /// the first encountered failure.
    /// </summary>
    public TagStatus Result => _errors.Count > 0 ? _errors[0].Result : TagStatus.Ok;

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
    /// A collection of tags associated with the result of a tag-related operation.
    /// Provides access to the tags directly involved in the operation.
    /// </summary>
    public IEnumerable<TagName> Tags => _tags;

    /// <summary>
    /// Gets a collection of errors encountered during the tag-related operation.
    /// Each error is represented as a tuple containing the tag name and the corresponding error message.
    /// </summary>
    public IEnumerable<(TagName, string)> Errors => _errors.Select(e => (Member: e.Tag, GetError(e.Result))).ToList();

    /// <summary>
    /// Creates a new <see cref="TagResponse"/> object with a status of <see cref="TagStatus.NoData"/> for the specified tag names.
    /// </summary>
    /// <param name="tagNames">An array of <see cref="TagName"/> objects for which there is no data available.</param>
    /// <returns>A <see cref="TagResponse"/> object containing the specified tag names with a <see cref="TagStatus.NoData"/> status.</returns>
    public static TagResponse NoData(params TagName[] tagNames)
    {
        var response = new TagResponse(TimeSpan.Zero);
        response._errors.AddRange(tagNames.Select(t => (t, TagStatus.NoData)));
        return response;
    }

    /// <summary>
    /// Aggregates the results of tag operations and creates a new <see cref="TagResponse"/> object containing the operation details.
    /// </summary>
    /// <param name="results">A list of tuples where each tuple represents a tag and its corresponding operation status.</param>
    /// <param name="duration">The time duration of the tag operation.</param>
    /// <returns>A <see cref="TagResponse"/> object containing the aggregated operation details, including tags and errors.</returns>
    public static TagResponse Aggregate(List<(TagName, TagStatus)> results, TimeSpan duration)
    {
        var response = new TagResponse(duration);

        foreach (var (member, status) in results)
        {
            response._tags.Add(member);
            if (status >= 0) continue;
            response._errors.Add((member, status));
        }

        return response;
    }

    /// <summary>
    /// Decodes the error message corresponding to the provided <see cref="TagStatus"/> value.
    /// </summary>
    /// <param name="status">The result code for which the error message needs to be decoded.</param>
    /// <returns>A string representing the decoded error message.</returns>
    //todo I need to figure out how to replace this.
    private static string GetError(TagStatus status) => plctag.plc_tag_decode_error((int)status);
}