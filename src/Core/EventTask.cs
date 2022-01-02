using System;
using System.Collections.Generic;
using L5Sharp.Abstractions;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    /// <summary>
    /// Represents a Logix Event Task component.
    /// </summary>
    public sealed class EventTask : TaskBase, IEventTask
    {
        internal EventTask(ComponentName name, string? description = null,
            ScanRate rate = default, TaskPriority priority = default, Watchdog watchdog = default,
            bool disableUpdateOutputs = false, bool inhibitTask = false, IEnumerable<string>? scheduledPrograms = null,
            TaskEventTrigger? eventTrigger = null, bool enableTimeout = false, string? eventTag = null) 
            : base(name, description, rate, priority, watchdog, disableUpdateOutputs, inhibitTask, scheduledPrograms)
        {
            EventTrigger = eventTrigger ?? TaskEventTrigger.EventInstructionOnly;
            EnableTimeout = enableTimeout;
            EventTag = eventTag;
        }

        /// <summary>
        /// Creates a new <see cref="EventTask"/> with the provided name an optional description.
        /// </summary>
        /// <param name="name">The name of the <see cref="EventTask"/>.</param>
        /// <param name="description">The description of the <see cref="EventTask"/>.</param>
        /// <exception cref="ArgumentNullException">name is null.</exception>
        public EventTask(ComponentName name, string? description = null) :
            this(name, description, new ScanRate(10), new TaskPriority(10), new Watchdog(500))
        {
        }

        /// <inheritdoc />
        public override TaskType Type => TaskType.Event;

        /// <inheritdoc />
        public TaskEventTrigger EventTrigger { get; }

        /// <inheritdoc />
        public bool EnableTimeout { get; }

        /// <inheritdoc />
        public string? EventTag { get; }
    }
}