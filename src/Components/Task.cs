using System.Collections.Generic;
using L5Sharp.Attributes;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Serialization;

namespace L5Sharp.Components
{
    /// <summary>
    /// 
    /// </summary>
    /// <footer>
    /// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
    /// `Logix 5000 Controllers Import/Export`</a> for more information.
    /// </footer>
    [LogixSerializer(typeof(TaskSerializer))]
    public class Task : ILogixComponent
    {
        /// <inheritdoc />
        public string Name { get; set; } = string.Empty;

        /// <inheritdoc />
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets the type of the task component (Continuous, Periodic, Event).
        /// </summary>
        /// <value>A <see cref="Enums.TaskType"/> enum representing the type of the task.</value>
        public TaskType Type { get; set; } = TaskType.Periodic;

        /// <summary>
        /// The scan priority of the task component. Default of 10.
        /// </summary>
        /// <value>>A <see cref="TaskPriority"/> value type representing the <see cref="int"/> priority of the task.</value>
        public TaskPriority Priority { get; set; } = new(10);

        /// <summary>
        /// The scan rate (ms) of the task component. Default of 10.
        /// </summary>
        /// <value>>A <see cref="ScanRate"/> value type representing the <see cref="float"/> rate of the task.</value>
        public ScanRate? Rate { get; set; } = new(10);

        /// <summary>
        /// The watchdog rate (ms) of the task component. Default of 500.
        /// </summary>
        /// <value>>A <see cref="Watchdog"/> value type representing the <see cref="float"/> watchdog of the task.</value>
        public Watchdog Watchdog { get; set; } = new(500);

        /// <summary>
        /// The value indicating whether the task is inhibited.
        /// </summary>
        /// <value>A <see cref="bool"/>; <c>true</c> if the task is inhibited; otherwise <c>false</c>.</value>
        public bool InhibitTask { get; set; }

        /// <summary>
        /// The value indicating whether the task is set to disable updating output values.
        /// </summary>
        /// <value>A <see cref="bool"/>; <c>true</c> if the task has disabled update outputs; otherwise <c>false</c>.</value>
        public bool DisableUpdateOutputs { get; set; }

        /// <summary>
        /// The collection of program names that are scheduled to the task.
        /// </summary>
        /// <value>A <see cref="List{T}"/> containing the string program names.</value>
        public List<string> ScheduledPrograms { get; set; } = new();
    }
}