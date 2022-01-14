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
    public abstract class TaskBase : ITask
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
            ScheduledPrograms = scheduledPrograms ?? Enumerable.Empty<string>();
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
        public IEnumerable<string> ScheduledPrograms { get; }
    }
}