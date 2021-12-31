using System.Collections.Generic;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp
{
    /// <summary>
    /// Represents a Logix <b>Task</b> component.
    /// </summary>
    /// <remarks>
    /// <para>
    /// <see cref="ITask"/> contains all the configurable properties associated with any given <see cref="Enums.TaskType"/>.
    /// </para>
    /// <para>
    /// <list type="bullet">
    /// <listheader>The following guidelines must be observed when defining a <see cref="ITask"/> component:</listheader>
    /// <item>Only one <see cref="Enums.TaskType.Continuous"/> task per context.</item>
    /// <item>A <see cref="IProgram"/> can be scheduled under one task only.</item>
    /// <item><see cref="ScheduledPrograms"/> must be defined in the context.</item>
    /// </list>
    /// </para>
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
        /// Gets the value indicating whether the <see cref="ITask"/> is inhibited.
        /// </summary>
        bool InhibitTask { get; }
        
        /// <summary>
        /// Gets the value indicating whether the <see cref="ITask"/> is disabling updates to output.
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
        /// Gets the collection of <see cref="Core.ComponentName"/> that represent the set of <see cref="IProgram"/>
        /// objects scheduled for the current <see cref="ITask"/> instance.
        /// </summary>
        /// <remarks>
        /// This collection may be mutated independently of an L5X, or outside the scope of a <see cref="LogixContext"/>.
        /// However, when attempting to update the entity for a given <see cref="LogixContext"/> instance,
        /// validation will be performed to ensure the set of <see cref="ScheduledPrograms"/> follows the
        /// Logix guidelines for updating a <see cref="ITask"/> component.
        /// </remarks>
        IList<ComponentName> ScheduledPrograms { get; }
    }
}