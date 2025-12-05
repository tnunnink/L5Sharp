using L5Sharp.Core;

namespace L5Sharp.CLI.Records;

/// <summary>
/// A record structure for the <see cref="Program"/> component.
/// </summary>
public record ProgramInfo()
{
    public ProgramInfo(Program program) : this()
    {
        Name = program.Name;
        Description = program.Description;
        Type = program.Type.Name;
        Class = program.Class?.Name ?? ComponentClass.Standard;
        MainRoutine = program.MainRoutineName;
        FaultRoutine = program.FaultRoutineName;
        Disabled = program.Disabled;
        Folder = program.UseAsFolder;
        TestEdits = program.TestEdits;
        Parent = program.Parent?.Name;
        Task = program.Task?.Name;
    }

    public string Name { get; init; } = "NewProgram";
    public string? Description { get; init; }
    public string? Type { get; init; } = ProgramType.Normal;
    public string? Class { get; init; } = ComponentClass.Standard;
    public string? MainRoutine { get; init; }
    public string? FaultRoutine { get; init; }
    public bool Disabled { get; init; }
    public bool Folder { get; init; }
    public bool TestEdits { get; init; }
    public string? Parent { get; init; }
    public string? Task { get; init; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="program"></param>
    public void Update(Program program)
    {
        program.Description = Description ?? program.Description;
        program.Type = Type is not null ? ProgramType.Parse(Type) : program.Type;
        program.MainRoutineName = MainRoutine ?? program.MainRoutineName;
        program.FaultRoutineName = FaultRoutine ?? program.FaultRoutineName;
        program.Disabled = Disabled;
        program.UseAsFolder = Folder;
        program.TestEdits = TestEdits;
        program.Schedule(Task);
    }

    /// <summary>
    /// Defines an implicit conversion from a <see cref="Program"/> object to a <see cref="ProgramInfo"/> object.
    /// </summary>
    /// <param name="program">The <see cref="Program"/> instance to be converted.</param>
    /// <returns>A new <see cref="ProgramInfo"/> object containing the converted program information.</returns>
    public static implicit operator ProgramInfo(Program program) => new(program);

    /// <summary>
    /// Defines an implicit conversion from a <see cref="Program"/> object to a <see cref="ProgramInfo"/> object.
    /// </summary>
    /// <param name="info">The <see cref="Program"/> instance to be converted.</param>
    /// <returns>A new <see cref="ProgramInfo"/> object containing the converted program information.</returns>
    public static implicit operator Program(ProgramInfo info) => new()
    {
        Name = info.Name,
        Description = info.Description,
        Type = info.Type is not null ? ProgramType.Parse(info.Type) : ProgramType.Normal,
        Class = info.Class is not null ? ComponentClass.Parse(info.Class) : null,
        MainRoutineName = info.MainRoutine,
        FaultRoutineName = info.FaultRoutine,
        Disabled = info.Disabled,
        UseAsFolder = info.Folder,
        TestEdits = info.TestEdits
    };
}