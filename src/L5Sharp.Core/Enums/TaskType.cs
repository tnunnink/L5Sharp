﻿namespace L5Sharp.Core;

/// <summary>
/// Provides an enumeration of all Logix Task types.
/// Value must be <see cref="Continuous"/>, <see cref="Periodic"/>, or <see cref="Event"/>.
/// </summary>
public sealed class TaskType : LogixEnum<TaskType, string>
{
    private TaskType(string name, string value) : base(name, value)
    {
    }

    /// <summary>
    /// Represents a Continuous <see cref="TaskType"/>, or a task that is configured to run continuously.
    /// </summary>
    public static readonly TaskType Continuous = new(nameof(Continuous), nameof(Continuous).ToUpper());

    /// <summary>
    /// Represents a Periodic <see cref="TaskType"/>, or a task that is configured to run at a specified rate.
    /// </summary>
    public static readonly TaskType Periodic = new(nameof(Periodic), nameof(Periodic).ToUpper());

    /// <summary>
    /// Represents an Event <see cref="TaskType"/>, or a task that is configured to run when a specified event occurs.
    /// </summary>
    public static readonly TaskType Event = new(nameof(Event), nameof(Event).ToUpper());
}