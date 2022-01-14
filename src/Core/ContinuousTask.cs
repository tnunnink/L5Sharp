using System.Collections.Generic;
using L5Sharp.Abstractions;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    /// <summary>
    /// Represents a Logix Continuous Task component.
    /// </summary>
    public sealed class ContinuousTask : TaskBase
    {
        internal ContinuousTask(ComponentName name, ScanRate rate = default,
            TaskPriority priority = default, Watchdog watchdog = default, bool disableUpdateOutputs = false,
            bool inhibitTask = false, IEnumerable<string>? scheduledPrograms = null, string? description = null)
            : base(name, description, rate, priority, watchdog, disableUpdateOutputs, inhibitTask, scheduledPrograms)
        {
        }

        /// <summary>
        /// Creates a new <see cref="ContinuousTask"/> with the provided name.
        /// </summary>
        /// <param name="name">The name of the task.</param>
        public ContinuousTask(ComponentName name)
            : this(name, new ScanRate(10), new TaskPriority(10), new Watchdog(500))
        {
        }

        /// <summary>
        /// Creates a new <see cref="ContinuousTask"/> with the provided name and description.
        /// </summary>
        /// <param name="name">The name of the task.</param>
        /// <param name="description">The description of the task.</param>
        public ContinuousTask(ComponentName name, string description)
            : this(name, new ScanRate(10), new TaskPriority(10), new Watchdog(500), description: description)
        {
        }

        /// <inheritdoc />
        public override TaskType Type => TaskType.Continuous;
    }
}