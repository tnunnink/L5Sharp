using System;

namespace L5Sharp.Core;

/// <summary>
/// A configurable property of a <see cref="Task"/> that controls the order in which the Logix Controller
/// will scan the given Task.
/// </summary>
/// <remarks>
/// <see cref="TaskPriority"/> is a simple byte value that must be between 1 and 15.
/// Attempting to set the <see cref="TaskPriority"/> to a value outside that range will result in an
/// <see cref="ArgumentOutOfRangeException"/>.
/// This parameter will control the scan order of task components as related to other tasks.
/// </remarks>
public readonly struct TaskPriority : IEquatable<TaskPriority>, ILogixParsable<TaskPriority>
{
    private readonly byte _value;

    /// <summary>
    /// Creates a new instance of <see cref="TaskPriority"/> with the provided value.
    /// </summary>
    /// <param name="value">The value of the priority. Must be a value between 1 and 15.</param>
    /// <exception cref="ArgumentOutOfRangeException">priority is less than 1 -or- greater than 15.</exception>
    public TaskPriority(byte value)
    {
        if (value is < 1 or > 15)
            throw new ArgumentOutOfRangeException(nameof(value), "Priority must be value between 1 and 15");

        _value = value;
    }

    /// <summary>
    /// Converts a <see cref="TaskPriority"/> to a <see cref="byte"/>.
    /// </summary>
    /// <param name="priority">The value to convert.</param>
    /// <returns>A <see cref="byte"/> value.</returns>
    public static implicit operator byte(TaskPriority priority) => priority._value;

    /// <summary>
    /// Converts a <see cref="byte"/> to a <see cref="TaskPriority"/>.
    /// </summary>
    /// <param name="priority">The value to convert.</param>
    /// <returns>A <see cref="TaskPriority"/> value.</returns>
    public static implicit operator TaskPriority(byte priority) => new(priority);

    /// <summary>
    /// Parses a string into a <see cref="TaskPriority"/> value.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <returns>The <see cref="TaskPriority"/> representing the parsed value.</returns>
    public static TaskPriority Parse(string value) => byte.Parse(value);

    /// <summary>
    /// Tries to parse a string into a <see cref="TaskPriority"/> value.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <returns>A <see cref="TaskPriority"/> representing the parsed value if successful; Otherwise; <c>default</c>.</returns>
    public static TaskPriority TryParse(string? value) =>
        byte.TryParse(value, out var result) ? new TaskPriority(result) : default;

    /// <inheritdoc />
    public bool Equals(TaskPriority other) => _value == other._value;

    /// <inheritdoc />
    public override string ToString() => _value.ToString();

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj switch
        {
            TaskPriority other => _value.Equals(other._value),
            ValueType value => _value.Equals(value),
            _ => false
        };
    }

    /// <inheritdoc />
    public override int GetHashCode() => _value.GetHashCode();

    /// <summary>
    /// Determines if the provided objects are equal.
    /// </summary>
    /// <param name="left">An object to compare.</param>
    /// <param name="right">An object to compare.</param>
    /// <returns>true if the provided objects are equal; otherwise, false.</returns>
    public static bool operator ==(TaskPriority left, TaskPriority right) => Equals(left, right);

    /// <summary>
    /// Determines if the provided objects are not equal.
    /// </summary>
    /// <param name="left">An object to compare.</param>
    /// <param name="right">An object to compare.</param>
    /// <returns>true if the provided objects are not equal; otherwise, false.</returns>
    public static bool operator !=(TaskPriority left, TaskPriority right) => !Equals(left, right);
}