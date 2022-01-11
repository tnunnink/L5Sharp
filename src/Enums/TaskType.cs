using System;
using System.Linq;
using System.Xml.Linq;
using Ardalis.SmartEnum;
using L5Sharp.Common;
using L5Sharp.Core;
using L5Sharp.Extensions;

namespace L5Sharp.Enums
{
    /// <summary>
    /// Provides an enumeration of all Logix Task types.
    /// Value must be <b>Continuous</b>, <b>Periodic</b>, or <b>Event</b>.
    /// </summary>
    public abstract class TaskType : SmartEnum<TaskType, string>
    {
        private TaskType(string name, string value) : base(name, value)
        {
        }

        /// <summary>
        /// Represents a Continuous <see cref="TaskType"/>, or a task that is configured to run continuously.
        /// </summary>
        public static readonly TaskType Continuous = new ContinuousTaskType();

        /// <summary>
        /// Represents a Periodic <see cref="TaskType"/>, or a task that is configured to run at a specified rate.
        /// </summary>
        public static readonly TaskType Periodic = new PeriodicTaskType();

        /// <summary>
        /// Represents an Event <see cref="TaskType"/>, or a task that is configured to run when a specified event occurs.
        /// </summary>
        public static readonly TaskType Event = new EventTaskType();

        internal abstract ITask FromElement(XElement element);

        private class ContinuousTaskType : TaskType
        {
            public ContinuousTaskType() : base(nameof(Continuous), nameof(Continuous).ToUpper())
            {
            }

            internal override ITask FromElement(XElement element)
            {
                if (element is null)
                    throw new ArgumentNullException(nameof(element));

                var name = element.GetComponentName();
                var description = element.GetComponentDescription();
                var priority = element.GetAttribute<ITask, TaskPriority>(t => t.Priority);
                var watchdog = element.GetAttribute<ITask, Watchdog>(t => t.Watchdog);
                var disableUpdateOutputs = element.GetAttribute<ITask, bool>(t => t.DisableUpdateOutputs);
                var inhibitTask = element.GetAttribute<ITask, bool>(t => t.InhibitTask);
                var programs = element.Descendants("ScheduledProgram").Select(e => e.GetComponentName());

                return new ContinuousTask(name, default, priority, watchdog, disableUpdateOutputs,
                    inhibitTask, programs, description);
            }
        }

        private class PeriodicTaskType : TaskType
        {
            public PeriodicTaskType() : base(nameof(Periodic), nameof(Periodic).ToUpper())
            {
            }

            internal override ITask FromElement(XElement element)
            {
                if (element is null)
                    throw new ArgumentNullException(nameof(element));

                var name = element.GetComponentName();
                var description = element.GetComponentDescription();
                var rate = element.GetAttribute<ITask, ScanRate>(t => t.Rate);
                var priority = element.GetAttribute<ITask, TaskPriority>(t => t.Priority);
                var watchdog = element.GetAttribute<ITask, Watchdog>(t => t.Watchdog);
                var disableUpdateOutputs = element.GetAttribute<ITask, bool>(t => t.DisableUpdateOutputs);
                var inhibitTask = element.GetAttribute<ITask, bool>(t => t.InhibitTask);
                var programs = element.Descendants("ScheduledProgram").Select(e => e.GetComponentName());

                return new PeriodicTask(name, description, rate, priority, watchdog,
                    disableUpdateOutputs, inhibitTask, programs);
            }
        }

        private class EventTaskType : TaskType
        {
            public EventTaskType() : base(nameof(Event), nameof(Event).ToUpper())
            {
            }

            internal override ITask FromElement(XElement element)
            {
                if (element is null)
                    throw new ArgumentNullException(nameof(element));

                var name = element.GetComponentName();
                var description = element.GetComponentDescription();
                var rate = element.GetAttribute<ITask, ScanRate>(t => t.Rate);
                var priority = element.GetAttribute<ITask, TaskPriority>(t => t.Priority);
                var watchdog = element.GetAttribute<ITask, Watchdog>(t => t.Watchdog);
                var disableUpdateOutputs = element.GetAttribute<ITask, bool>(t => t.DisableUpdateOutputs);
                var inhibitTask = element.GetAttribute<ITask, bool>(t => t.InhibitTask);
                var programs = element.Descendants("ScheduledProgram").Select(e => e.GetComponentName());

                var eventInfo = element.Element(LogixNames.EventInfo);
                var eventTrigger = eventInfo?.GetAttribute<IEventTask, TaskEventTrigger>(t => t.EventTrigger);
                var enableTimeout = eventInfo?.GetAttribute<IEventTask, bool>(t => t.EnableTimeout) ?? false;
                var eventTag = eventInfo?.GetAttribute<IEventTask, string?>(t => t.EventTag);

                return new EventTask(name, description, rate, priority, watchdog,
                    disableUpdateOutputs, inhibitTask, programs, eventTrigger, enableTimeout, eventTag);
            }
        }
    }
}