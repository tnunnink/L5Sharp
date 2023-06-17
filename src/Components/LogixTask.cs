using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;

namespace L5Sharp.Components;

/// <summary>
/// A logix <c>Task</c> component. Contains the properties that comprise the L5X Task element.
/// </summary>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
[XmlType(L5XName.Task)]
public class LogixTask : LogixComponent<LogixTask>
{
    /// <inheritdoc />
    public LogixTask()
    {
        Type = TaskType.Periodic;
        Priority = new TaskPriority(10);
        Rate = new ScanRate(10);
        Watchdog = new Watchdog(500);
    }

    /// <inheritdoc />
    public LogixTask(XElement element) : base(element)
    {
    }

    /// <summary>
    /// Gets the type of the task component (Continuous, Periodic, Event).
    /// </summary>
    /// <value>A <see cref="Enums.TaskType"/> enum representing the type of the task.</value>
    public TaskType Type
    {
        get => GetValue<TaskType>() ?? throw new L5XException(Element);
        set => SetValue(value.Value);
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
    /// <value>>A <see cref="Watchdog"/> value type representing the <see cref="float"/> watchdog of the task.</value>
    public Watchdog Watchdog
    {
        get => GetValue<Watchdog>();
        set => SetValue(value);
    }

    /// <summary>
    /// The value indicating whether the task is inhibited.
    /// </summary>
    /// <value>A <see cref="bool"/>; <c>true</c> if the task is inhibited; otherwise <c>false</c>.</value>
    public bool InhibitTask
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    /// <summary>
    /// The value indicating whether the task is set to disable updating output values.
    /// </summary>
    /// <value>A <see cref="bool"/>; <c>true</c> if the task has disabled update outputs; otherwise <c>false</c>.</value>
    public bool DisableUpdateOutputs
    {
        get => GetValue<bool>();
        set => SetValue(value);
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
        
        //Remove is no programs are scheduled
        if (!scheduled.Elements().Any())
            scheduled.Remove();
    }
}