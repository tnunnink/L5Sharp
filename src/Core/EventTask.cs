using System.Collections.Generic;
using L5Sharp.Abstractions;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    /// <summary>
    /// Represents a Logix Event Task component.
    /// </summary>
    public sealed class EventTask : TaskBase
    {
        internal EventTask(ComponentName name, ScanRate rate = default,
            TaskPriority priority = default, Watchdog watchdog = default, bool disableUpdateOutputs = false,
            bool inhibitTask = false, IEnumerable<string>? scheduledPrograms = null, TaskEventTrigger? eventTrigger = null,
            bool enableTimeout = false, string? eventTag = null, string? description = null) 
            : base(name, description, rate, priority, watchdog, disableUpdateOutputs, inhibitTask, scheduledPrograms)
        {
            EventTrigger = eventTrigger ?? TaskEventTrigger.EventInstructionOnly;
            EnableTimeout = enableTimeout;
            EventTag = eventTag;
        }

        /// <summary>
        /// Creates a new <see cref="EventTask"/> with the provided name.
        /// </summary>
        /// <param name="name">The name of the task.</param>
        public EventTask(ComponentName name)
            : this(name, new ScanRate(10), new TaskPriority(10), new Watchdog(500))
        {
        }

        /// <summary>
        /// Creates a new <see cref="EventTask"/> with the provided name and description.
        /// </summary>
        /// <param name="name">The name of the task.</param>
        /// <param name="description">The description of the task.</param>
        public EventTask(ComponentName name, string description)
            : this(name, new ScanRate(10), new TaskPriority(10), new Watchdog(500), description: description)
        {
        }

        /// <inheritdoc />
        public override TaskType Type => TaskType.Event;

        /// <summary>
        /// Gets the <see cref="Enums.TaskEventTrigger"/> setting of the <see cref="EventTask"/> instance.
        /// </summary>
        public TaskEventTrigger EventTrigger { get; }

        /// <summary>
        /// Gets the value indicating whether the <see cref="EventTask"/> timeout setting is enabled.
        /// </summary>
        public bool EnableTimeout { get; }

        /// <summary>
        /// Gets the tag name that triggers the <see cref="EventTask"/>.
        /// </summary>
        public string? EventTag { get; }
    }
}