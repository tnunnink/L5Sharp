using L5Sharp.Core;
using Task = L5Sharp.Core.Task;

namespace L5Sharp.Model;

/// <summary>
/// A record structure for the <see cref="Task"/> component.
/// </summary>
public record TaskInfo()
{
    public TaskInfo(Task task) : this()
    {
        Name = task.Name;
        Description = task.Description;
        Type = task.Type.Name;
        Class = task.Class?.Name ?? ComponentClass.Standard;
        Priority = task.Priority;
        Rate = task.Rate;
        Watchdog = task.Watchdog;
        Inhibited = task.InhibitTask;
        UpdateOutputs = !task.DisableUpdateOutputs;
        EventTrigger = task.EventTrigger?.Name;
        EventTag = task.EventTag?.ToString();
        EnableTimeout = task.EnableTimeout ?? false;
        Programs = task.Scheduled.ToArray();
    }

    public string Name { get; init; } = "NewTask";
    public string? Description { get; init; }
    public string Type { get; init; } = TaskType.Periodic;
    public string Class { get; init; } = ComponentClass.Standard;
    public int Priority { get; init; }
    public float Rate { get; init; }
    public float Watchdog { get; init; }
    public bool Inhibited { get; init; }
    public bool UpdateOutputs { get; init; }
    public string? EventTrigger { get; init; }
    public string? EventTag { get; init; }
    public bool EnableTimeout { get; init; }
    public string[] Programs { get; init; } = [];

    /// <summary>
    /// Defines an implicit conversion operator to convert a <see cref="Task"/> instance to a <see cref="TaskInfo"/>.
    /// </summary>
    /// <param name="task">The <see cref="Task"/> instance to be converted.</param>
    /// <returns>A new <see cref="TaskInfo"/> representing the provided <see cref="Task"/> instance.</returns>
    public static implicit operator TaskInfo(Task task) => new(task);
}