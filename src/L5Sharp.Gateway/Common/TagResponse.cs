using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Core;
using L5Sharp.Gateway.Abstractions;

namespace L5Sharp.Gateway.Common;

/// <summary>
/// Represents the result of a tag-related operation performed by an <see cref="IPlcClient"/>.
/// </summary>
public class TagResponse
{
    /// <summary>
    /// Represents a collection of tags alongside their associated statuses that encountered errors during a tag operation.
    /// </summary>
    private readonly List<(TagName Tag, TagStatus Status)> _errors = [];

    /// <summary>
    /// Creates a new <see cref="TagResponse"/> object indicating the result of the operation.
    /// </summary>
    private TagResponse(IEnumerable<Tag> tags, TimeSpan duration)
    {
        Tags = tags;
        Timestamp = DateTime.UtcNow;
        Duration = duration;
    }

    /// <summary>
    /// Indicates whether the tag operation completed successfully without errors.
    /// </summary>
    public bool Success => _errors.Count == 0;

    /// <summary>
    /// Gets the overall result status of the tag operation.
    /// Returns the status of the first error encountered if any errors occurred during the operation;
    /// otherwise, returns <see cref="TagStatus.Ok"/> indicating successful completion.
    /// </summary>
    public TagStatus Status => _errors.Count > 0 ? _errors.First().Status : TagStatus.Ok;

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
    public IEnumerable<TagError> Errors => _errors.Select(e => new TagError(e.Tag, e.Status)).ToArray();

    /// <summary>
    /// Retrieves the <see cref="Tag"/> associated with the specified <see cref="TagName"/> from the response.
    /// Throws an <see cref="InvalidOperationException"/> if the tag is not found.
    /// </summary>
    /// <param name="tagName">The <see cref="TagName"/> to search for.</param>
    /// <returns>The <see cref="Tag"/> associated with the specified tag name.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the response does not contain a result for the specified tag name.</exception>
    public Tag GetTag(TagName tagName)
    {
        var tag = Tags.FirstOrDefault(t => t.TagName == tagName);

        if (tag is null)
            throw new InvalidOperationException($"Response does not contain result for tag: '{tagName}'");

        return tag;
    }

    /// <summary>
    /// Attempts to retrieve the <see cref="Tag"/> associated with the specified <see cref="TagName"/> from the response.
    /// </summary>
    /// <param name="tagName">The <see cref="TagName"/> to search for.</param>
    /// <param name="tag">When this method returns, contains the <see cref="Tag"/> associated with the specified tag name if found; otherwise, <c>null</c>.</param>
    /// <returns><c>true</c> if the tag was found; otherwise, <c>false</c>.</returns>
    public bool TryGetTag(TagName tagName, out Tag tag)
    {
        var match = Tags.FirstOrDefault(t => t.TagName == tagName);

        if (match is not null)
        {
            tag = match;
            return true;
        }

        tag = null!;
        return false;
    }

    /// <summary>
    /// Determines if the specified status exists within the error list.
    /// </summary>
    /// <param name="status">The <see cref="TagStatus"/> to check for in the error list.</param>
    /// <returns>True if the status exists in the error list; otherwise, false.</returns>
    public bool HasError(TagStatus status) => _errors.Any(e => e.Status == status);

    /// <summary>
    /// Determines if the specified <see cref="TagName"/> has an associated error in the tag operation results.
    /// </summary>
    /// <param name="tagName">The <see cref="TagName"/> to check for errors.</param>
    /// <returns>Returns <c>true</c> if an error is associated with the specified <see cref="TagName"/>; otherwise, <c>false</c>.</returns>
    public bool HasError(TagName tagName) => _errors.Any(e => e.Tag == tagName);

    /// <summary>
    /// Creates a <see cref="TagResponse"/> indicating that no data is available for the provided tags.
    /// </summary>
    /// <param name="tags">The collection of tags for which no data is available.</param>
    /// <returns>A <see cref="TagResponse"/> object indicating the absence of data for the specified tags.</returns>
    public static TagResponse NoData(Tag[] tags)
    {
        var response = new TagResponse(tags, TimeSpan.Zero);
        response._errors.AddRange(tags.Select(t => (t.TagName, TagStatus.NoData)));
        return response;
    }

    /// <summary>
    /// Creates a new <see cref="TagResponse"/> object indicating duplicate tags and their statuses within
    /// the specified tag collection.
    /// </summary>
    /// <param name="tags">An array of tags to evaluate.</param>
    /// <param name="duplicates">An array of duplicate tag names found in the collection.</param>
    /// <returns>A <see cref="TagResponse"/> object containing information about duplicate tags.</returns>
    public static TagResponse Duplicate(Tag[] tags, TagName[] duplicates)
    {
        var response = new TagResponse(tags, TimeSpan.Zero);
        response._errors.AddRange(duplicates.Select(t => (t, TagStatus.Duplicate)));
        return response;
    }

    /// <summary>
    /// Aggregates the provided tag data and results into a <see cref="TagResponse"/> object,
    /// including any errors identified during the operation.
    /// </summary>
    /// <param name="tags">The collection of <see cref="Tag"/> objects being processed.</param>
    /// <param name="results">A list of tuples containing <see cref="TagName"/> and <see cref="TagStatus"/> pairs representing the processing outcomes of individual tags.</param>
    /// <param name="duration">The total time taken to process the tags.</param>
    /// <returns>A <see cref="TagResponse"/> object encapsulating the aggregation results and any errors that occurred.</returns>
    public static TagResponse Aggregate(Tag[] tags, List<(TagName, TagStatus)> results, TimeSpan duration)
    {
        var response = new TagResponse(tags, duration);

        foreach (var (member, status) in results)
        {
            if (status >= 0) continue;
            response._errors.Add((member, status));
        }

        return response;
    }
}