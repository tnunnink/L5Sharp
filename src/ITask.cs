using System.Collections.Generic;
using System.Xml.Serialization;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp
{
    /// <summary>
    /// Represents a Logix <c>Task</c> component and the properties associated with configuring a Task.
    /// </summary>
    /// <remarks>
    /// <c>Task</c> can be a <see cref="L5Sharp.Enums.TaskType.Continuous"/>,
    /// <see cref="L5Sharp.Enums.TaskType.Periodic"/>, or <see cref="L5Sharp.Enums.TaskType.Event"/> type Task.
    /// </remarks>
    /// <footer>
    /// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
    /// `Logix 5000 Controllers Import/Export`</a> for more information.
    /// </footer> 
    public interface ITask : ILogixComponent
    {
        /// <summary>
        /// Get the <see cref="Enums.TaskType"/> value of the <see cref="ITask"/>.
        /// </summary>
        TaskType Type { get; }
        
        /// <summary>
        /// Get the <see cref="Core.TaskPriority"/> value of the <see cref="ITask"/>.
        /// </summary>
        TaskPriority Priority { get; }
        
        /// <summary>
        /// Get the <see cref="Core.ScanRate"/> value of the <see cref="ITask"/>.
        /// </summary>
        ScanRate Rate { get; }
        
        /// <summary>
        /// Get the <see cref="Core.Watchdog"/> value of the <see cref="ITask"/>.
        /// </summary>
        Watchdog Watchdog { get; }
        
        /// <summary>
        /// Setting that indicates the <c>Task</c> is inhibited.
        /// </summary>
        bool InhibitTask { get; }
        
        /// <summary>
        /// Setting that indicates the <c>Task</c> is disabling updated to outputs.
        /// </summary>
        bool DisableUpdateOutputs { get; }
        
        /// <summary>
        /// Gets the <see cref="TaskEventInfo"/> object that contains the event configuration for the <see cref="ITask"/>.
        /// </summary>
        /// <remarks>
        /// <see cref="EventInfo"/> is only available for the <see cref="Enums.TaskType.Event"/> task types.
        /// </remarks>
        TaskEventInfo? EventInfo { get; }
        
        /// <summary>
        /// Gets the collection of program names that are scheduled for the current <see cref="ITask"/>.
        /// </summary>
        IEnumerable<string> ScheduledPrograms { get; }
    }
}