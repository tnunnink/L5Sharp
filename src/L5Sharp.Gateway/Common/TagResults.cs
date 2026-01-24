using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using L5Sharp.Core;

namespace L5Sharp.Gateway.Common;

/// <summary>
/// Represents a collection of <see cref="TagResult"/>, encapsulating the overall success status, metadata, and
/// corresponding tag data.
/// </summary>
/// <remarks>
/// This class provides information about the outcome of a tag operation, including individual results, any errors,
/// and operation timing details.
/// </remarks>
public class TagResults : IReadOnlyCollection<TagResult>
{
    private readonly TagResult[] _results;

    private TagResults(TagResult[] results)
    {
        if (results is null)
            throw new ArgumentNullException(nameof(results));

        if (results.Length == 0)
            throw new ArgumentException("Results collection cannot be empty.", nameof(results));

        _results = results;
        Tags = _results.Select(r => r.Tag);
        Errors = new ReadOnlyCollection<TagError>(_results.SelectMany(r => r.Errors).ToArray());
        Success = _results.All(r => r.Success);
        Status = _results.Min(e => e.Status);
        Timestamp = DateTime.UtcNow;
        Duration = TimeSpan.FromMilliseconds(_results.Average(r => r.Duration.TotalMilliseconds));
    }

    /// <summary>
    /// Indicates whether all tag operations in the collection were completed successfully.
    /// Returns true if every tag result resulted in success; otherwise, returns false if any operation failed.
    /// </summary>
    public bool Success { get; }

    /// <summary>
    /// Represents the overall status of the tag operation results within the collection.
    /// This will represent the minimum status for all tag results in the collection.
    /// </summary>
    public TagStatus Status { get; }

    /// <summary>
    /// Gets the timestamp indicating when the tag operation results were recorded.
    /// The value is expressed in Coordinated Universal Time (UTC).
    /// </summary>
    public DateTime Timestamp { get; }

    /// <summary>
    /// Represents the time elapsed during the execution of all tag-related operations in the collection.
    /// The value is calculated as the average duration of individual operations included in the results.
    /// </summary>
    public TimeSpan Duration { get; }

    /// <summary>
    /// A collection of tags associated with the result of a tag-related operation.
    /// Provides access to the tags directly involved in the operation.
    /// </summary>
    public IEnumerable<Tag> Tags { get; }

    /// <summary>
    /// A collection of errors represented as a sequence of tuples containing <see cref="TagName"/> and its associated <see cref="TagStatus"/>.
    /// Provides details about the tags and their processing status, specifically for operations resulting in errors.
    /// </summary>
    public IReadOnlyCollection<TagError> Errors { get; }

    /// <summary>
    /// Gets the total number of tag results contained in the response.
    /// </summary>
    public int Count => _results.Length;

    /// <summary>
    /// Determines if the specified status exists within the error list.
    /// </summary>
    /// <param name="status">The <see cref="TagStatus"/> to check for in the error list.</param>
    /// <returns>True if the status exists in the error list; otherwise, false.</returns>
    public bool HasError(TagStatus status) => Errors.Any(e => e.Status == status);

    /// <summary>
    /// Determines if the specified <see cref="TagName"/> has an associated error in the tag operation results.
    /// </summary>
    /// <param name="tagName">The <see cref="TagName"/> to check for errors.</param>
    /// <returns>Returns <c>true</c> if an error is associated with the specified <see cref="TagName"/>; otherwise, <c>false</c>.</returns>
    public bool HasError(TagName tagName) => Errors.Any(e => e.TagName == tagName);

    /// <summary>
    /// Retrieves the result associated with the specified tag name from the collection of results.
    /// </summary>
    /// <param name="tagName">The <see cref="TagName"/> for which the result is to be retrieved.</param>
    /// <returns>The <see cref="TagResult"/> associated with the specified tag name.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the collection does not contain a result for the specified tag name.
    /// </exception>
    public TagResult GetResult(TagName tagName)
    {
        var result = _results.SingleOrDefault(r => r.Tag.TagName == tagName);

        if (result is null)
            throw new InvalidOperationException($"Response does not contain result for tag: '{tagName}'");

        return result;
    }

    /// <inheritdoc />
    public IEnumerator<TagResult> GetEnumerator()
    {
        return _results.Cast<TagResult>().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <summary>
    /// Aggregates an array of <see cref="TagResult"/> into a single <see cref="TagResults"/> instance.
    /// </summary>
    /// <param name="results">The array of <see cref="TagResult"/> to aggregate into the result set.</param>
    /// <returns>A <see cref="TagResults"/> instance representing the aggregated results of the input.</returns>
    public static TagResults Aggregate(TagResult[] results)
    {
        return new TagResults(results);
    }
}