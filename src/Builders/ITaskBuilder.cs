using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Builders
{
    /// <summary>
    /// Component Builder that is responsible for creating new instances of an <see cref="ITask"/>
    /// </summary>
    public interface ITaskBuilder : IComponentBuilder<ITask>
    {
        /// <summary>
        /// Sets the description of the Task 
        /// </summary>
        /// <param name="description">The description of the <see cref="ITask"/></param>
        /// <returns>The current instance of the <see cref="ITaskBuilder"/></returns>
        ITaskBuilder WithDescription(string description);
        
        /// <summary>
        /// Configure the <see cref="TaskType"/> of the Task
        /// </summary>
        /// <param name="type">The <see cref="TaskType"/> of the <see cref="ITask"/></param>
        /// <returns>The current instance of the <see cref="ITaskBuilder"/></returns>
        ITaskBuilder OfType(TaskType type);
        
        /// <summary>
        /// Sets the priority of the Task
        /// </summary>
        /// <param name="priority">value of the priority</param>
        /// <returns>The current instance of the <see cref="ITaskBuilder"/></returns>
        ITaskBuilder WithPriority(TaskPriority priority);
        
        
        ITaskBuilder WithRate(ScanRate rate);
        ITaskBuilder WithWatchdog(Watchdog watchdog);
        ITaskBuilder IsInhibited();
        ITaskBuilder DisablesUpdateOutputs();
        ITaskBuilder ScheduleProgram(string programName);
    }
}