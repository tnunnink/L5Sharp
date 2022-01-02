using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Abstractions
{
    /// <summary>
    /// A base class for all <see cref="ITask"/> components.
    /// </summary>
    public abstract class TaskBase : ITask, IEquatable<TaskBase>
    {
        internal TaskBase(ComponentName name, string? description = null,
            ScanRate rate = default, TaskPriority priority = default, Watchdog watchdog = default,
            bool disableUpdateOutputs = false, bool inhibitTask = false,
            IEnumerable<string>? scheduledPrograms = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? string.Empty;
            Rate = rate;
            Priority = priority;
            Watchdog = watchdog;
            DisableUpdateOutputs = disableUpdateOutputs;
            InhibitTask = inhibitTask;
            ScheduledPrograms = scheduledPrograms is not null
                ? new List<ComponentName>(scheduledPrograms.Select(p => new ComponentName(p)))
                : new List<ComponentName>();
        }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public string Description { get; }

        /// <inheritdoc />
        public abstract TaskType Type { get; }

        /// <inheritdoc />
        public TaskPriority Priority { get; }

        /// <inheritdoc />
        public ScanRate Rate { get; }

        /// <inheritdoc />
        public Watchdog Watchdog { get; }

        /// <inheritdoc />
        public bool InhibitTask { get; }

        /// <inheritdoc />
        public bool DisableUpdateOutputs { get; }

        /// <inheritdoc />
        public IEnumerable<ComponentName> ScheduledPrograms { get; }

        /// <inheritdoc />
        public bool Equals(TaskBase? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name && 
                   Description == other.Description && 
                   Priority.Equals(other.Priority) &&
                   Rate.Equals(other.Rate) &&
                   Watchdog.Equals(other.Watchdog) &&
                   InhibitTask == other.InhibitTask &&
                   DisableUpdateOutputs == other.DisableUpdateOutputs &&
                   ScheduledPrograms.SequenceEqual(other.ScheduledPrograms);
        }

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((TaskBase)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode() =>
            HashCode.Combine(Name, Description, Priority, Rate, Watchdog, InhibitTask, DisableUpdateOutputs,
                ScheduledPrograms);

        /// <summary>
        /// Determines if the provided objects are equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the provided objects are equal; otherwise, false.</returns>
        public static bool operator ==(TaskBase? left, TaskBase? right) => Equals(left, right);

        /// <summary>
        /// Determines if the provided objects are not equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the provided objects are not equal; otherwise, false.</returns>
        public static bool operator !=(TaskBase? left, TaskBase? right) => !Equals(left, right);
    }
}