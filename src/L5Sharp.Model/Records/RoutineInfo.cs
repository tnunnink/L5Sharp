using L5Sharp.Core;

namespace L5Sharp.Model;

/// <summary>
/// A record structure for the <see cref="Routine"/> component.
/// </summary>
public record RoutineInfo()
{
    public RoutineInfo(Routine routine) : this()
    {
        Name = routine.Name;
        Description = routine.Description;
        Type = routine.Type.Name;
        Program = routine.Program?.Name;
        OnlineEditType = routine.OnlineEditType?.Name;
        SheetSize = routine.SheetSize?.Name;
        SheetOrientation = routine.SheetOrientation?.Name;
    }

    public string Name { get; init; } = "NewRoutine";
    public string? Description { get; init; }
    public string Type { get; init; } = RoutineType.RLL;
    public string? Program { get; init; }
    public string? OnlineEditType { get; init; }
    public string? SheetSize { get; init; }
    public string? SheetOrientation { get; init; }

    /// <summary>
    /// Defines an implicit operator to convert a <see cref="Routine"/> object
    /// to a <see cref="RoutineInfo"/> object.
    /// </summary>
    /// <param name="routine">The <see cref="Routine"/> instance to be converted.</param>
    /// <returns>A new <see cref="RoutineInfo"/> instance initialized with the specified <see cref="Routine"/> data.</returns>
    public static implicit operator RoutineInfo(Routine routine) => new(routine);
}