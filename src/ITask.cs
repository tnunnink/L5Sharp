using System.Collections.Generic;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp
{
    /// <summary>
    /// Represents a RSLogix Task component.
    /// </summary>
    public interface ITask : ILogixComponent
    {
        /// <summary>
        /// The type of the Task.
        /// </summary>
        TaskType Type { get; }
        
        /// <summary>
        /// The priority setting of the Task.
        /// </summary>
        TaskPriority Priority { get; set; }
        
        /// <summary>
        /// The scan rate setting of the Task.
        /// </summary>
        ScanRate Rate { get; set; }
        
        /// <summary>
        /// The watch dog setting of the Task.
        /// </summary>
        Watchdog Watchdog { get; set; }
        
        /// <summary>
        /// Setting that indicates the task is inhibited
        /// </summary>
        bool InhibitTask { get; set; }
        
        /// <summary>
        /// Setting that indicates the task is disabling updated to outputs
        /// </summary>
        bool DisableUpdateOutputs { get; set; }
        
        /// <summary>
        /// A collection of program names that are scheduled for the current task.
        /// </summary>
        IEnumerable<string> ScheduledPrograms { get; }
        
        
        void ScheduleProgram(string name);
        void RemoveProgram(string name);
    }
}