using System.Collections.Generic;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Components
{
    /// <summary>
    /// 
    /// </summary>
    public class Task
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; } = string.Empty;
        
        public string Description { get; set; } = string.Empty;
        
        public TaskType Type { get; } = TaskType.Periodic;

        public TaskPriority Priority { get; set; } = new(10);
        
        public ScanRate Rate { get; set; } = new(10);

        public Watchdog Watchdog { get; } = new(500);
        
        public bool InhibitTask { get; set; }
        
        public bool DisableUpdateOutputs { get; set; }
        
        public List<string> ScheduledPrograms { get; set; }

        public TaskEventInfo? EventInfo { get; set; }
    }
}