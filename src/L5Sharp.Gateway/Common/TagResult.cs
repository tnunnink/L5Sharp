using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using L5Sharp.Core;
using L5Sharp.Gateway.Abstractions;

namespace L5Sharp.Gateway.Common;

/// <summary>
/// Represents the result for a single tag operation executed by the <see cref="IPlcClient"/>.
/// This class provides detailed information about the operation outcome, including success status,
/// timestamp, duration, and the associated tag data.
/// </summary>
public class TagResult
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TagResult"/> class with the specified tag, status, and duration.
    /// </summary>
    /// <param name="tag">The tag associated with the operation result.</param>
    /// <param name="errors"></param>
    /// <param name="duration">The time span representing how long the operation took to execute.</param>
    private TagResult(Tag tag, TagError[] errors, TimeSpan duration)
    {
        Tag = tag ?? throw new ArgumentNullException(nameof(tag));
        Errors = new ReadOnlyCollection<TagError>(errors);
        Success = Errors.Count == 0;
        Status = Errors.Count > 0 ? Errors.Min(e => e.Status) : TagStatus.Ok;
        Timestamp = DateTime.UtcNow;
        Duration = duration;
    }

    /// <summary>
    /// Gets a value indicating whether the tag operation completed successfully.
    /// Returns true if the operation status is non-negative (successful or informational);
    /// otherwise, returns false indicating an error occurred.
    /// </summary>
    public bool Success { get; }

    /// <summary>
    /// Gets the status of the tag operation indicating its current state or outcome.
    /// The status provides information about the success, failure, or progress of the operation.
    /// </summary>
    public TagStatus Status { get; }

    /// <summary>
    /// Gets the timestamp representing the exact UTC date and time when the tag operation was completed.
    /// This property provides a reliable reference for when the operation was executed,
    /// aiding in logging and performance analysis.
    /// </summary>
    public DateTime Timestamp { get; }

    /// <summary>
    /// Gets the duration of the tag operation execution as a time span.
    /// Represents the amount of time taken to execute the operation from start to finish.
    /// </summary>
    public TimeSpan Duration { get; }

    /// <summary>
    /// Gets the tag associated with the operation.
    /// Represents the specific tag targeted during the tag operation, providing access to its metadata,
    /// value, and configuration details.
    /// </summary>
    public Tag Tag { get; }

    /// <summary>
    /// Gets a collection of errors associated with each tag member of the tag operation.
    /// Each error encapsulates the tag name and its corresponding status, providing insights into
    /// the issues that occurred during the operation.
    /// </summary>
    public IReadOnlyCollection<TagError> Errors { get; }

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
    /// <returns>
    /// Returns <c>true</c> if an error is associated with the specified <see cref="TagName"/>; otherwise, <c>false</c>.
    /// </returns>
    public bool HasError(TagName tagName) => Errors.Any(e => e.TagName == tagName);

    /// <summary>
    /// Creates a new instance of <see cref="TagResult"/> with the specified tag, collection of errors, and operation duration.
    /// </summary>
    /// <param name="tag">The tag associated with the operation result.</param>
    /// <param name="errors">An array of <see cref="TagError"/> objects representing errors that occurred during the operation.</param>
    /// <param name="duration">The time span representing how long the operation took to execute. Defaults to <see cref="TimeSpan.Zero"/>.</param>
    /// <returns>A new <see cref="TagResult"/> instance containing the operation details.</returns>
    public static TagResult Create(Tag tag, TagError[] errors, TimeSpan duration = default)
    {
        return new TagResult(tag, errors, duration);
    }

    /// <summary>
    /// Creates a new instance of the <see cref="TagResult"/> class with the specified tag, status, and duration.
    /// </summary>
    /// <param name="tag">The tag associated with the operation result.</param>
    /// <param name="status">The status representing the outcome of the operation.</param>
    /// <param name="duration">The time span representing how long the operation took to execute. Defaults to <see cref="TimeSpan.Zero"/> if not specified.</param>
    /// <returns>A new <see cref="TagResult"/> instance representing the result of the operation.</returns>
    public static TagResult Create(Tag tag, TagStatus status, TimeSpan duration = default)
    {
        TagError[] errors = status < 0 ? [new TagError(tag.TagName, status)] : [];
        return new TagResult(tag, errors, duration);
    }
}