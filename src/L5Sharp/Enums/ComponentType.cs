using L5Sharp.Utilities;

namespace L5Sharp.Enums;

public class ComponentType : LogixEnum<ComponentType, string>
{
    private ComponentType(string name, string value) : base(name, value)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public virtual string ContainerName => $"{Name}s";

    /// <summary>
    /// 
    /// </summary>
    public virtual bool IsTopLevel => Name != Routine.Name;

    /// <summary>
    /// Represents a <b>DataType</b> <see cref="ComponentType"/>.
    /// </summary>
    public static readonly ComponentType DataType = new(nameof(DataType), L5XName.DataType);

    /// <summary>
    /// Represents a <b>Module</b> <see cref="ComponentType"/>.
    /// </summary>
    public static readonly ComponentType Module = new(nameof(Module), L5XName.Module);

    /// <summary>
    /// Represents a <b>AddOnInstruction</b> <see cref="ComponentType"/>.
    /// </summary>
    public static readonly ComponentType AddOnInstruction =
        new(nameof(AddOnInstruction), L5XName.AddOnInstructionDefinition);

    /// <summary>
    /// Represents a <b>Tag</b> <see cref="ComponentType"/>.
    /// </summary>
    public static readonly ComponentType Tag = new(nameof(Tag), L5XName.Tag);

    /// <summary>
    /// Represents a <b>Program</b> <see cref="ComponentType"/>.
    /// </summary>
    public static readonly ComponentType Program = new(nameof(Program), L5XName.Program);
    
    /// <summary>
    /// Represents a <b>Routine</b> <see cref="ComponentType"/>.
    /// </summary>
    public static readonly ComponentType Routine = new(nameof(Routine), L5XName.Routine);

    /// <summary>
    /// Represents a <b>Task</b> <see cref="ComponentType"/>.
    /// </summary>
    public static readonly ComponentType Task = new(nameof(Task), L5XName.Task);

    /// <summary>
    /// Represents a <b>Trend</b> <see cref="ComponentType"/>.
    /// </summary>
    public static readonly ComponentType Trend = new(nameof(Trend), L5XName.Trend);

    /// <summary>
    /// Represents a <b>WatchList</b> <see cref="ComponentType"/>.
    /// </summary>
    public static readonly ComponentType WatchList = new(nameof(WatchList), L5XName.QuickWatchList);
}