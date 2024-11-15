using System;

namespace L5Sharp.Core;

/// <summary>
/// Represents a component type.
/// </summary>
public class ReferenceType : LogixEnum<ReferenceType, string>
{
    private ReferenceType(string name, string value) : base(name, value)
    {
    }

    /// <summary>
    /// Represents a <b>DataType</b> <see cref="ReferenceType"/>.
    /// </summary>
    public static readonly ReferenceType DataType = new(nameof(DataType), nameof(DataType));

    /// <summary>
    /// Represents a <b>Instruction</b> <see cref="ReferenceType"/>.
    /// </summary>
    public static readonly ReferenceType Instruction = new(nameof(Instruction), nameof(Instruction));

    /// <summary>
    /// Represents a <b>Module</b> <see cref="ReferenceType"/>.
    /// </summary>
    public static readonly ReferenceType Module = new(nameof(Module), nameof(Module));

    /// <summary>
    /// Represents a <b>Tag</b> <see cref="ReferenceType"/>.
    /// </summary>
    public static readonly ReferenceType Tag = new(nameof(Tag), nameof(Tag));

    /// <summary>
    /// Represents a <b>Program</b> <see cref="ReferenceType"/>.
    /// </summary>
    public static readonly ReferenceType Program = new(nameof(Program), nameof(Program));

    /// <summary>
    /// Represents a <b>Routine</b> <see cref="ReferenceType"/>.
    /// </summary>
    public static readonly ReferenceType Routine = new(nameof(Routine), nameof(Routine));

    /// <summary>
    /// Represents a <b>Task</b> <see cref="ReferenceType"/>.
    /// </summary>
    public static readonly ReferenceType Task = new(nameof(Task), nameof(Task));

    /// <summary>
    /// Retrieves the corresponding ReferenceType based on the provided LogixComponent.
    /// </summary>
    /// <param name="component">The LogixComponent to get the ReferenceType for.</param>
    /// <returns>The ReferenceType corresponding to the LogixComponent.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the component is not recognized.</exception>
    public static ReferenceType FromComponent(LogixComponent component)
    {
        return component switch
        {
            Core.DataType => DataType,
            AddOnInstruction => Instruction,
            Core.Module => Module,
            Core.Tag => Tag,
            Core.Program => Program,
            Core.Routine => Routine,
            Core.Task => Task,
            _ => throw new ArgumentOutOfRangeException(nameof(component), component, null)
        };
    }
}