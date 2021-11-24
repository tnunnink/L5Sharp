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
        /// Get the type of the <c>Task</c>.
        /// </summary>
        TaskType Type { get; }
        
        /// <summary>
        /// Get the priority setting of the <c>Task</c>.
        /// </summary>
        TaskPriority Priority { get; }
        
        /// <summary>
        /// The scan rate setting of the <c>Task</c>.
        /// </summary>
        ScanRate Rate { get; }
        
        /// <summary>
        /// The watch dog setting of the <c>Task</c>.
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
        /// A collection of program names that are scheduled for the current <c>Task</c>.
        /// </summary>
        IEnumerable<string> ScheduledPrograms { get; }

        /// <summary>
        /// Add a program to the <c>Task's</c> <see cref="ScheduledPrograms"/> collection.
        /// </summary>
        /// <param name="name">The name of the <c>ScheduledProgram</c> to add.</param>
        void ScheduleProgram(string name);
        
        /// <summary>
        /// Removes a program from the <c>Task's</c> <see cref="ScheduledPrograms"/> collection.
        /// </summary>
        /// <param name="name">The name of the <c>ScheduledProgram</c> to remove.</param>
        void RemoveProgram(string name);
    }
}