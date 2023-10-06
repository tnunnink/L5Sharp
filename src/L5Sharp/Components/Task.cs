using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Common;
using L5Sharp.Enums;
using L5Sharp.Utilities;

namespace L5Sharp.Components;

/// <summary>
/// A logix <c>Task</c> component. Contains the properties that comprise the L5X Task element.
/// </summary>
/// <remarks>
/// Observe these guidelines when defining a task:<br/>
///     • Tasks must be defined after programs and before controller objects.<br/>
///     • There is a maximum of 32 tasks.<br/>
///     • There is one continuous task only.<br/>
///     • A program can be scheduled under one task only.<br/>
///     • Scheduled programs must be defined (must exist).<br/>
/// </remarks>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
public class Task : LogixComponent<Task>
{
    /// <summary>
    /// Creates a new <see cref="Task"/> with default values.
    /// </summary>
    /// <remarks>By default uses <see cref="TaskType.Periodic"/>, 10ms <see cref="Priority"/>, 10ms <see cref="Rate"/>,
    /// and 500ms <see cref="Watchdog"/>.</remarks>
    public Task()
    {
        Type = TaskType.Periodic;
        Priority = new TaskPriority(10);
        Rate = new ScanRate(10);
        Watchdog = new Watchdog(500);
    }

    /// <summary>
    /// Creates a new <see cref="Task"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    public Task(XElement element) : base(element)
    {
    }

    /// <summary>
    /// Gets the type of the task component (Continuous, Periodic, Event).
    /// </summary>
    /// <value>A <see cref="Enums.TaskType"/> enum representing the type of the task.</value>
    public TaskType? Type
    {
        get => GetRequiredValue<TaskType>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// The scan priority of the task component. Default of 10.
    /// </summary>
    /// <value>>A <see cref="TaskPriority"/> value type representing the <see cref="int"/> priority of the task.</value>
    public TaskPriority Priority
    {
        get => GetValue<TaskPriority>();
        set => SetValue(value);
    }

    /// <summary>
    /// The scan rate (ms) of the task component. Default of 10.
    /// </summary>
    /// <value>>A <see cref="ScanRate"/> value type representing the <see cref="float"/> rate of the task.</value>
    public ScanRate? Rate
    {
        get => GetValue<ScanRate?>();
        set => SetValue(value);
    }

    /// <summary>
    /// The watchdog rate (ms) of the task component. Default of 500.
    /// </summary>
    /// <value>A <see cref="Watchdog"/> value type representing the <see cref="float"/> watchdog of the task.</value>
    public Watchdog Watchdog
    {
        get => GetValue<Watchdog>();
        set => SetValue(value);
    }

    /// <summary>
    /// The value indicating whether the task is inhibited.
    /// </summary>
    /// <value><c>true</c> if the task is inhibited; otherwise <c>false</c>.</value>
    public bool InhibitTask
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    /// <summary>
    /// The value indicating whether the task is set to disable updating output values.
    /// </summary>
    /// <value><c>true</c> if the task has disabled update outputs; otherwise <c>false</c>.</value>
    public bool DisableUpdateOutputs
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    /// <summary>
    /// The trigger for the event task. Only used for event tasks.
    /// </summary>
    /// <value>
    /// A <see cref="TaskEventTrigger"/> value indicating what triggers the task. Returns <c>null</c> for non-event tasks.
    /// </value>
    public TaskEventTrigger? EventTrigger
    {
        get => GetValue<TaskEventTrigger>(L5XName.EventInfo.XName());
        set => SetValue(value, L5XName.EventInfo.XName());
    }
    
    /// <summary>
    /// The tag name that the event task consumes. Only used for event tasks.
    /// </summary>
    /// <value>
    /// A <see cref="TagName"/> value indicating what tag to consume. Returns <c>null</c> for non-event tasks.
    /// </value>
    /// <remarks>Only used for event tasks with a Consumed Tag trigger or a Module Input Data State Change trigger.</remarks>
    public TagName? EventTag
    {
        get => GetValue<TagName>(L5XName.EventInfo.XName());
        set => SetValue(value, L5XName.EventInfo.XName());
    }
    
    /// <summary>
    /// The value indicating whether timeouts are enabled for the event task. Only used for event tasks.
    /// </summary>
    /// <value>
    /// If the task is an event type task, <c>true</c> indicating that timeouts are enabled, <c>false</c> to indicate
    /// they are disabled. Returns <c>null</c> for non-event tasks.
    /// </value>
    public bool? EnableTimeout
    {
        get => GetValue<bool>(L5XName.EventInfo.XName());
        set => SetValue(value, L5XName.EventInfo.XName());
    }

    /// <summary>
    /// The collection of program names that are scheduled to the task.
    /// </summary>
    /// <value>A <see cref="IEnumerable{T}"/> containing the string program names.</value>
    /// <remarks>This member just returns the read only list of scheduled programs. To modify the list, use </remarks>
    public IEnumerable<string> ScheduledPrograms =>
        Element.Descendants(L5XName.ScheduledProgram).Select(e => e.LogixName());

    /// <summary>
    /// Adds the provided program name to the underlying list of <see cref="ScheduledPrograms"/>.
    /// </summary>
    /// <param name="program">The name of the program to schedule.</param>
    public void Schedule(string program)
    {
        var element = new XElement(L5XName.ScheduledProgram, new XAttribute(L5XName.Name, program));

        if (Element.Element(L5XName.ScheduledPrograms) is null)
            Element.Add(new XElement(L5XName.ScheduledPrograms));

        Element.Element(L5XName.ScheduledPrograms)!.Add(element);
    }

    /// <summary>
    /// Removes the specified program name from the underlying list of <see cref="ScheduledPrograms"/>
    /// </summary>
    /// <param name="program">The name of the program to cancel.</param>
    public void Cancel(string program)
    {
        var scheduled = Element.Element(L5XName.ScheduledPrograms);

        if (scheduled is null) return;

        scheduled.Elements(L5XName.ScheduledProgram).FirstOrDefault(p => p.LogixName() == program)?.Remove();

        if (!scheduled.Elements().Any())
            scheduled.Remove();
    }
}