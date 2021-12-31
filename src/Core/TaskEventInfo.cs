using System.Diagnostics.Tracing;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    /// <summary>
    /// Represents the data associated with a <see cref="EventTask"/> type instance.
    /// </summary>
    public class TaskEventInfo
    {
        /// <summary>
        /// Creates a new <see cref="TaskEventInfo"/> instance with the provided data.
        /// </summary>
        /// <param name="eventTrigger">The </param>
        /// <param name="enableTimeout"></param>
        public TaskEventInfo(TaskEventTrigger eventTrigger, bool enableTimeout)
        {
            EventTrigger = eventTrigger;
            EnableTimeout = enableTimeout;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public TaskEventTrigger EventTrigger { get; }
        /// <summary>
        /// 
        /// </summary>
        public bool EnableTimeout { get; }
    }
}