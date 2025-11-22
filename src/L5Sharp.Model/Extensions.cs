using L5Sharp.Core;
using Task = L5Sharp.Core.Task;

namespace L5Sharp.Model;

public static class Extensions
{
    /// <summary>
    /// Converts the specified <see cref="ILogixComponent"/> instance to its corresponding information object.
    /// </summary>
    /// <param name="component">The <see cref="ILogixComponent"/> instance to be converted.</param>
    /// <returns>An object representing the information associated with the provided <see cref="ILogixComponent"/>.</returns>
    /// <exception cref="NotSupportedException">Thrown if the provided <see cref="ILogixComponent"/> type is not supported.</exception>
    public static object ToInfo(this ILogixComponent component)
    {
        return component switch
        {
            DataType x => new DataTypeInfo(x),
            AddOnInstruction x => new AddOnInstructionInfo(x),
            Module x => new ModuleInfo(x),
            Tag x => new TagInfo(x),
            Program x => new ProgramInfo(x),
            Routine x => new RoutineInfo(x),
            Task x => new TaskInfo(x),
            _ => throw new NotSupportedException(
                $"No supported info object for the provided type '{component.GetType()}'")
        };
    }

    /// <summary>
    /// Converts the specified <see cref="Controller"/> instance to a <see cref="ControllerInfo"/> object.
    /// </summary>
    /// <param name="instance">The <see cref="Controller"/> instance to convert.</param>
    /// <returns>A <see cref="ControllerInfo"/> object containing information about the program.</returns>
    public static ControllerInfo ToInfo(this Controller instance)
    {
        return new ControllerInfo(instance);
    }

    /// <summary>
    /// Converts the specified <see cref="DataType"/> instance to a <see cref="DataTypeInfo"/> object.
    /// </summary>
    /// <param name="instance">The <see cref="DataType"/> instance to convert.</param>
    /// <returns>A <see cref="DataTypeInfo"/> object containing information about the program.</returns>
    public static DataTypeInfo ToInfo(this DataType instance)
    {
        return new DataTypeInfo(instance);
    }

    /// <summary>
    /// Converts the specified collection of <see cref="DataType"/> instances to a collection of
    /// <see cref="DataTypeInfo"/> objects.
    /// </summary>
    /// <param name="items">The collection of <see cref="DataType"/> instances to convert.</param>
    /// <returns>An enumerable collection of <see cref="DataTypeInfo"/> objects containing information about the programs.</returns>
    public static IEnumerable<DataTypeInfo> ToInfo(this IEnumerable<DataType> items)
    {
        return items.Select(x => new DataTypeInfo(x));
    }

    /// <summary>
    /// Converts the specified <see cref="DataTypeMember"/> instance to a <see cref="DataTypeMemberInfo"/> object.
    /// </summary>
    /// <param name="instance">The <see cref="DataTypeMember"/> instance to convert.</param>
    /// <returns>A <see cref="DataTypeInfo"/> object containing information about the program.</returns>
    public static DataTypeMemberInfo ToInfo(this DataTypeMember instance)
    {
        return new DataTypeMemberInfo(instance);
    }

    /// <summary>
    /// Converts the specified collection of <see cref="DataTypeMember"/> instances to a collection of
    /// <see cref="DataTypeMemberInfo"/> objects.
    /// </summary>
    /// <param name="items">The collection of <see cref="DataTypeMember"/> instances to convert.</param>
    /// <returns>An enumerable collection of <see cref="DataTypeMemberInfo"/> objects containing information about the programs.</returns>
    public static IEnumerable<DataTypeMemberInfo> ToInfo(this IEnumerable<DataTypeMember> items)
    {
        return items.Select(x => new DataTypeMemberInfo(x));
    }

    /// <summary>
    /// Converts the specified <see cref="AddOnInstruction"/> instance to a <see cref="AddOnInstructionInfo"/> object.
    /// </summary>
    /// <param name="instance">The <see cref="AddOnInstruction"/> instance to convert.</param>
    /// <returns>A <see cref="AddOnInstructionInfo"/> object containing information about the program.</returns>
    public static AddOnInstructionInfo ToInfo(this AddOnInstruction instance)
    {
        return new AddOnInstructionInfo(instance);
    }

    /// <summary>
    /// Converts the specified collection of <see cref="AddOnInstruction"/> instances to a collection
    /// of <see cref="AddOnInstructionInfo"/> objects.
    /// </summary>
    /// <param name="items">The collection of <see cref="AddOnInstruction"/> instances to convert.</param>
    /// <returns>An enumerable collection of <see cref="AddOnInstructionInfo"/> objects containing information about the programs.</returns>
    public static IEnumerable<AddOnInstructionInfo> ToInfo(this IEnumerable<AddOnInstruction> items)
    {
        return items.Select(x => new AddOnInstructionInfo(x));
    }

    /// <summary>
    /// Converts the specified <see cref="Module"/> instance to a <see cref="ModuleInfo"/> object.
    /// </summary>
    /// <param name="instance">The <see cref="Module"/> instance to convert.</param>
    /// <returns>A <see cref="ModuleInfo"/> object containing information about the program.</returns>
    public static ModuleInfo ToInfo(this Module instance)
    {
        return new ModuleInfo(instance);
    }

    /// <summary>
    /// Converts the specified collection of <see cref="Module"/> instances to a collection
    /// of <see cref="ModuleInfo"/> objects.
    /// </summary>
    /// <param name="items">The collection of <see cref="Module"/> instances to convert.</param>
    /// <returns>An enumerable collection of <see cref="ModuleInfo"/> objects containing information about the programs.</returns>
    public static IEnumerable<ModuleInfo> ToInfo(this IEnumerable<Module> items)
    {
        return items.Select(x => new ModuleInfo(x));
    }

    /// <summary>
    /// Converts the specified <see cref="Tag"/> instance to a <see cref="TagInfo"/> object.
    /// </summary>
    /// <param name="instance">The <see cref="Tag"/> instance to convert.</param>
    /// <returns>A <see cref="TagInfo"/> object containing information about the program.</returns>
    public static TagInfo ToInfo(this Tag instance)
    {
        return new TagInfo(instance);
    }

    /// <summary>
    /// Converts the specified collection of <see cref="Tag"/> instances to a collection of <see cref="TagInfo"/> objects.
    /// </summary>
    /// <param name="items">The collection of <see cref="Tag"/> instances to convert.</param>
    /// <returns>An enumerable collection of <see cref="TagInfo"/> objects containing information about the programs.</returns>
    public static IEnumerable<TagInfo> ToInfo(this IEnumerable<Tag> items)
    {
        return items.Select(p => new TagInfo(p));
    }

    /// <summary>
    /// Converts the specified <see cref="Program"/> instance to a <see cref="ProgramInfo"/> object.
    /// </summary>
    /// <param name="instance">The <see cref="Program"/> instance to convert.</param>
    /// <returns>A <see cref="ProgramInfo"/> object containing information about the program.</returns>
    public static ProgramInfo ToInfo(this Program instance)
    {
        return new ProgramInfo(instance);
    }

    /// <summary>
    /// Converts the specified collection of <see cref="Program"/> instances to a collection of <see cref="ProgramInfo"/> objects.
    /// </summary>
    /// <param name="items">The collection of <see cref="Program"/> instances to convert.</param>
    /// <returns>An enumerable collection of <see cref="ProgramInfo"/> objects containing information about the programs.</returns>
    public static IEnumerable<ProgramInfo> ToInfo(this IEnumerable<Program> items)
    {
        return items.Select(p => new ProgramInfo(p));
    }

    /// <summary>
    /// Converts the specified <see cref="Routine"/> instance to a <see cref="RoutineInfo"/> object.
    /// </summary>
    /// <param name="instance">The <see cref="Routine"/> instance to convert.</param>
    /// <returns>A <see cref="RoutineInfo"/> object containing information about the program.</returns>
    public static RoutineInfo ToInfo(this Routine instance)
    {
        return new RoutineInfo(instance);
    }

    /// <summary>
    /// Converts the specified collection of <see cref="Routine"/> instances to a collection of <see cref="RoutineInfo"/> objects.
    /// </summary>
    /// <param name="items">The collection of <see cref="Routine"/> instances to convert.</param>
    /// <returns>An enumerable collection of <see cref="RoutineInfo"/> objects containing information about the programs.</returns>
    public static IEnumerable<RoutineInfo> ToInfo(this IEnumerable<Routine> items)
    {
        return items.Select(x => new RoutineInfo(x));
    }

    /// <summary>
    /// Converts the specified <see cref="Task"/> instance to a <see cref="TaskInfo"/> object.
    /// </summary>
    /// <param name="instance">The <see cref="Task"/> instance to convert.</param>
    /// <returns>A <see cref="TaskInfo"/> object containing information about the program.</returns>
    public static TaskInfo ToInfo(this Task instance)
    {
        return new TaskInfo(instance);
    }

    /// <summary>
    /// Converts the specified collection of <see cref="Task"/> instances to a collection of <see cref="TaskInfo"/> objects.
    /// </summary>
    /// <param name="items">The collection of <see cref="Task"/> instances to convert.</param>
    /// <returns>An enumerable collection of <see cref="TaskInfo"/> objects containing information about the programs.</returns>
    public static IEnumerable<TaskInfo> ToInfo(this IEnumerable<Task> items)
    {
        return items.Select(x => new TaskInfo(x));
    }

    /// <summary>
    /// Converts the specified <see cref="Rung"/> instance to a <see cref="RungInfo"/> object.
    /// </summary>
    /// <param name="instance">The <see cref="Rung"/> instance to convert.</param>
    /// <returns>A <see cref="RungInfo"/> object containing information about the program.</returns>
    public static RungInfo ToInfo(this Rung instance)
    {
        return new RungInfo(instance);
    }

    /// <summary>
    /// Converts the specified collection of <see cref="Rung"/> instances to a collection of <see cref="RungInfo"/> objects.
    /// </summary>
    /// <param name="items">The collection of <see cref="Rung"/> instances to convert.</param>
    /// <returns>An enumerable collection of <see cref="RungInfo"/> objects containing information about the programs.</returns>
    public static IEnumerable<RungInfo> ToInfo(this IEnumerable<Rung> items)
    {
        return items.Select(x => new RungInfo(x));
    }
}