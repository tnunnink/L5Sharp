using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using L5Sharp.Core;
using L5Sharp.Gateway.Abstractions;

namespace L5Sharp.Gateway.Common;

/// <summary>
/// Represents the result of a tag-related operation performed by an <see cref="IPlcClient"/>.
/// </summary>
public class TagResults : IReadOnlyCollection<TagResult>
{
    private readonly TagResult[] _results;

    private TagResults(TagResult[] results)
    {
        _results = results ?? throw new ArgumentNullException(nameof(results));

        if (_results.Length == 0)
            throw new ArgumentException("Results collection cannot be empty.", nameof(results));

        Tags = _results.Select(r => r.Tag);
        Errors = new ReadOnlyCollection<TagError>(_results.SelectMany(r => r.Errors).ToArray());
        Success = _results.All(r => r.Success);
        Status = _results.Min(e => e.Status);
        Timestamp = DateTime.UtcNow;
        Duration = TimeSpan.FromMilliseconds(_results.Average(r => r.Duration.Milliseconds));
    }

    /// <summary>
    /// Indicates whether the tag operation completed successfully without errors.
    /// </summary>
    public bool Success { get; }

    /// <summary>
    /// Gets the overall result status of the tag operation.
    /// Returns the status of the first error encountered if any errors occurred during the operation;
    /// otherwise, returns <see cref="TagStatus.Ok"/> indicating successful completion.
    /// </summary>
    public TagStatus Status { get; }

    /// <summary>
    /// The timestamp representing the date and time when the operation was completed.
    /// </summary>
    public DateTime Timestamp { get; }

    /// <summary>
    /// The duration of the operation represented as a <see cref="TimeSpan"/>.
    /// This property indicates the elapsed time required to complete the operation,
    /// providing insight into the performance and timing of the process.
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