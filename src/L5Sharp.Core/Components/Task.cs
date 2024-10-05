using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;


namespace L5Sharp.Core;

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
public sealed class Task : LogixComponent<Task>
{
    /// <inheritdoc />
    protected override List<string> ElementOrder =>
    [
        L5XName.Description,
        L5XName.EventInfo,
        L5XName.ScheduledPrograms,
    ];
    
    /// <summary>
    /// Creates a new <see cref="Task"/> with default values.
    /// </summary>
    /// <remarks>By default uses <see cref="TaskType.Periodic"/>, 10ms <see cref="Priority"/>, 10ms <see cref="Rate"/>,
    /// and 500ms <see cref="Watchdog"/>.</remarks>
    public Task() : base(L5XName.Task)
    {
        Type = TaskType.Periodic;
        Priority = new TaskPriority(10);
        Rate = new ScanRate(10);
        Watchdog = new Watchdog(500);
        InhibitTask = false;
        DisableUpdateOutputs = false;
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
    /// Creates a new <see cref="Task"/> initialized with the provided name and optional type.
    /// </summary>
    /// <param name="name">The name of the Task.</param>
    /// <param name="type">The <see cref="TaskType"/> of the Task.</param>
    public Task(string name, TaskType? type = default) : this()
    {
        Name = name;
        Type = type ?? TaskType.Periodic;
    }

    /// <summary>
    /// Gets the type of the task component (Continuous, Periodic, Event).
    /// </summary>
    /// <value>A <see cref="TaskType"/> enum representing the type of the task.</value>
    public TaskType Type
    {
        get => GetRequiredValue<TaskType>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// The <see cref="ComponentClass"/> value indicating whether this component is a standard or safety type component.
    /// </summary>
    /// <value>A <see cref="Core.ComponentClass"/> option representing class of the component.</value>
    /// <remarks>
    /// Specify the class of the task. This attribute applies only to safety controller projects.
    /// </remarks>
    public ComponentClass? Class
    {
        get => GetValue<ComponentClass>();
        set => SetValue(value);
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
    public ScanRate Rate
    {
        get => GetValue<ScanRate>();
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
    /// Retrieves a collection of <see cref="Program"/> components that are scheduled to this <see cref="Task"/>.
    /// </summary>
    /// <value>A <see cref="IEnumerable{T}"/> containing <see cref="Program"/> component objects schedule to this task.</value>
    /// <remarks>
    /// This is an extension to the type and uses the attached L5X file to retrieve the program components.
    /// Therefore if this task is not attached it will return and empty collection. Also if no program exists with the
    /// scheduled name, this will return an empty collection.
    /// </remarks>
    public IEnumerable<Program> Programs =>
        L5X?.Programs.Where(p => Scheduled.Any(s => s == p.Name)) ?? Enumerable.Empty<Program>();

    /// <summary>
    /// The collection of program names that are scheduled to the task.
    /// </summary>
    /// <value>A <see cref="IEnumerable{T}"/> containing the string program names.</value>
    /// <remarks>This member just returns the read only list of scheduled programs. To modify the list, use
    /// the methods <see cref="Schedule"/> or <see cref="Cancel"/>.</remarks>
    public IEnumerable<string> Scheduled =>
        Element.Descendants(L5XName.ScheduledProgram).Select(e => e.LogixName());

    /// <inheritdoc />
    public override L5X Export(Revision? softwareRevision = null)
    {
        throw new NotSupportedException("Task components do not support the export function.");
    }

    /// <inheritdoc />
    public override void Delete()
    {
        if (Element.Parent is null) return;

        foreach (var program in Programs)
        {
            program.Delete();
        }

        Element.Remove();
    }

    /// <summary>
    /// Adds the provided program name to the underlying list of <see cref="Scheduled"/>.
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
    /// Removes the specified program name from the underlying list of <see cref="Scheduled"/> programs.
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

    /// <summary>
    /// Adds the provided <see cref="Program"/> to the underlying L5X document and schedules the program to this Task
    /// instance. 
    /// </summary>
    /// <param name="program">The program to add to this Task.</param>
    public void AddProgram(Program program)
    {
        if (program is null) throw new ArgumentNullException(nameof(program));
        if (L5X is null) throw new InvalidOperationException("Can not add program to detached Task component.");
        
        L5X.Programs.Add(program);
        Schedule(program.Name);
    }
}