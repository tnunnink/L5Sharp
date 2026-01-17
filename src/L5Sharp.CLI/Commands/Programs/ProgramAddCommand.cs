using CliFx.Attributes;
using CliFx.Exceptions;
using L5Sharp.CLI.Internal;
using L5Sharp.Core;

namespace L5Sharp.CLI.Commands.Programs;

[Command("program add", Description = "Adds a new program to the project.")]
public class ProgramAddCommand : MutateCommand
{
    [CommandParameter(0, Name = "name", Description = "The name of the program to create.")]
    public string Name { get; init; } = string.Empty;

    [CommandOption("type", Description = "The type of program to create (e.g. Normal, Equipment, etc.).")]
    public string? Type { get; init; }

    [CommandOption("main", Description = "The name of the main routine for the program.")]
    public string? MainRoutine { get; init; }

    [CommandOption("fault", Description = "The name of the fault routine for the program.")]
    public string? FaultRoutine { get; init; }

    [CommandOption("folder", Description = "Configures the program to be used as a folder container.")]
    public bool Folder { get; init; }

    [CommandOption("disabled", Description = "Creates the program in a disabled state.")]
    public bool Disabled { get; init; }

    [CommandOption("desc", Description = "The description of the program.")]
    public string? Description { get; init; }

    protected override void Mutate(L5X project)
    {
        if (project.Contains(Reference.To<Program>(Name)))
            throw new CommandException($"Project already contains a program: '{Name}'", ExitCodes.ComponentNotFound);

        var program = new Program(Name)
        {
            Type = Type is not null ? ProgramType.Parse(Type) : ProgramType.Normal,
            MainRoutineName = MainRoutine,
            FaultRoutineName = FaultRoutine,
            UseAsFolder = Folder,
            Disabled = Disabled,
            Description = Description
        };

        project.Add(program);
    }
}