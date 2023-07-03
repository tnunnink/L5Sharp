using L5Sharp.Components;

namespace L5Sharp.Enums;

/// <summary>
/// An enumeration of all <see cref="TaskEventTrigger"/> options for a given <see cref="LogixTask"/>.
/// </summary>
public class TaskEventTrigger : LogixEnum<TaskEventTrigger, string>
{
    private TaskEventTrigger(string name, string value) : base(name, value)
    {
    }

    /// <summary>
    /// Represents an AxisHome <see cref="TaskEventTrigger"/> value.
    /// </summary>
    public static readonly TaskEventTrigger AxisHome = new(nameof(AxisHome), "Axis Home");
        
    /// <summary>
    /// Represents an AxisWatch <see cref="TaskEventTrigger"/> value.
    /// </summary>
    public static readonly TaskEventTrigger AxisWatch = new(nameof(AxisWatch), "Axis Watch");

    /// <summary>
    /// Represents an AxisRegistration1 <see cref="TaskEventTrigger"/> value.
    /// </summary>
    public static readonly TaskEventTrigger AxisRegistration1 =
        new(nameof(AxisRegistration1), "Axis Registration 1");

    /// <summary>
    /// Represents an AxisRegistration2 <see cref="TaskEventTrigger"/> value.
    /// </summary>
    public static readonly TaskEventTrigger AxisRegistration2 =
        new(nameof(AxisRegistration2), "Axis Registration 2");

    /// <summary>
    /// Represents an MotionGroupExecution <see cref="TaskEventTrigger"/> value.
    /// </summary>
    public static readonly TaskEventTrigger MotionGroupExecution =
        new(nameof(MotionGroupExecution), "Motion Group Execution");

    /// <summary>
    /// Represents an EventInstructionOnly <see cref="TaskEventTrigger"/> value.
    /// </summary>
    public static readonly TaskEventTrigger EventInstructionOnly =
        new(nameof(EventInstructionOnly), "EVENT Instruction Only");

    /// <summary>
    /// Represents an ModuleInputDataStateChange <see cref="TaskEventTrigger"/> value.
    /// </summary>
    public static readonly TaskEventTrigger ModuleInputDataStateChange =
        new(nameof(ModuleInputDataStateChange), "Module Input Data State Change");

    /// <summary>
    /// Represents an ConsumedTag <see cref="TaskEventTrigger"/> value.
    /// </summary>
    public static readonly TaskEventTrigger ConsumedTag = new(nameof(ConsumedTag), "Consumed Tag");
        
    /// <summary>
    /// Represents an WindowsEvent <see cref="TaskEventTrigger"/> value.
    /// </summary>
    public static readonly TaskEventTrigger WindowsEvent = new(nameof(WindowsEvent), "Windows Event");
}