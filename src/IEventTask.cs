using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEventTask : ITask
    {
        /// <summary>
        /// Gets the <see cref="Enums.TaskEventTrigger"/> setting of the <see cref="EventTask"/> instance.
        /// </summary>
        public TaskEventTrigger EventTrigger { get; }
        
        /// <summary>
        /// Gets the value indicating whether the <see cref="EventTask"/> timeout setting is enabled.
        /// </summary>
        public bool EnableTimeout { get; }

        /// <summary>
        /// todo not sure if I should make this a <see cref="ITag{TDataType}"/> or just keep as a string. 
        /// </summary>
        public string? EventTag { get; }
    }
}