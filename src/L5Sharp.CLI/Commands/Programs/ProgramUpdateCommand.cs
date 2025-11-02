using CliFx.Attributes;
using JetBrains.Annotations;
using L5Sharp.CLI.Internal;
using L5Sharp.Core;

namespace L5Sharp.CLI.Commands.Programs;

[PublicAPI]
[Command("program update", Description = "")]
public class ProgramUpdateCommand : MutateCommand
{
    [CommandParameter(0, Name = "name", Description = "The name of the program(s) to configure (supports wildcards).")]
    public string? Name { get; set; }

    [CommandOption("task", Description = "The task to assign the program to.")]
    public string? Task { get; set; }

    [CommandOption("type", Description = "The type of program to create (e.g. Normal, Equipment, etc.).")]
    public string? Type { get; set; }

    [CommandOption("main", Description = "The name of the main routine for the program.")]
    public string? MainRoutine { get; set; }

    [CommandOption("fault", Description = "The name of the fault routine for the program.")]
    public string? FaultRoutine { get; set; }

    [CommandOption("folder", Description = "Configures the program to be used as a folder container.")]
    public bool Folder { get; set; }

    [CommandOption("disable", Description = "Creates the program in a disabled state.")]
    public bool Disable { get; set; }

    [CommandOption("desc", Description = "The description of the program.")]
    public string? Description { get; set; }

    protected override void Mutate(L5X project)
    {
        var programs = project.Programs
            .FilterByPattern(p => p.Name, Name)
//            .FilterByExpression()(p => p.Name, Name)
            .ToList();

        programs.ForEach(p =>
        {
            if (Type is not null) p.Type = ProgramType.Parse(Type);
            if (MainRoutine is not null) p.MainRoutineName = MainRoutine;
            if (FaultRoutine is not null) p.FaultRoutineName = FaultRoutine;
            p.Disabled = Disable;
            p.UseAsFolder = Folder;
            if (Description is not null) p.Description = Description;
            if (Task is not null) p.Schedule(Task);
        });
    }
}